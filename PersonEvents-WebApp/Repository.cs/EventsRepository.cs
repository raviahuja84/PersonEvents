using PersonEvents_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Repository.cs
{
    public class EventsRepository : IEventsRepository
    {
        private Func<PersonEventsContext> _contextFactory;

        public EventsRepository(Func<PersonEventsContext> context)
        {
            _contextFactory = context;
        }

        public Task RegisterEvent(string name, DateTime dob, string eventData)
        {
            using (var context = _contextFactory.Invoke())
            {
                if (context.Events.Any(x => x.PersonName == name 
                                         && x.PersonDOB == dob))
                {
                    var currentEvent = context.Events.FirstOrDefault(x => x.PersonName == name && x.PersonDOB == dob);
                    currentEvent.Description += ("|" + eventData);
                    context.Update(currentEvent);
                }
                else
                {
                    context.Add(new Events { 
                        EventID = Guid.NewGuid(), 
                        PersonName = name,
                        PersonDOB = dob,
                        Description = eventData
                    });
                }

                context.SaveChanges();
            }

            return Task.FromResult(true);
        }

        public Task DeleteEvent(string name, DateTime dob, string eventData)
        {
            using (var context = _contextFactory.Invoke())
            {
                var currentEvent = context.Events.FirstOrDefault(x => x.PersonName == name && x.PersonDOB == dob);

                if (currentEvent.Description.Contains(eventData))
                {
                    currentEvent.Description.Replace(eventData, "").Replace("||", "|");
                    context.Update(currentEvent);
                }

                context.SaveChanges();
            }

            return Task.FromResult(true);
        }

        public PersonEvents GetPersonEvents(string name, DateTime dob)
        {
            PersonEvents personEvents = null;
            using (var context = _contextFactory.Invoke())
            {
                personEvents = new PersonEvents();
                personEvents.Person = context.Person.FirstOrDefault(x => x.Name == name && x.DOB == dob);
                var description = context.Events.FirstOrDefault(x => x.PersonName == name && x.PersonDOB == dob)?.Description;

                if (!string.IsNullOrEmpty(description))
                {
                    var eventsTokens = description?.Split('|');

                    if (eventsTokens.Length > 0)
                        personEvents.Events = eventsTokens.ToList();

                }
            }

            return personEvents;
        }

        public IList<PersonEvents> GetPersonEvents()
        {
            var personsEvents = new List<PersonEvents>();
            using (var context = _contextFactory.Invoke())
            {
                foreach (var person in context.Person)
                {
                    var personEvents = new PersonEvents();
                    personEvents.Person = person;

                    var description = context.Events.FirstOrDefault(x => x.PersonName == person.Name && x.PersonDOB == person.DOB)?.Description;
                    if (!string.IsNullOrEmpty(description))
                    {
                        var eventsTokens = description?.Split('|');

                        if (eventsTokens.Length > 0)
                            personEvents.Events = eventsTokens.ToList();
                    }

                    personsEvents.Add(personEvents);
                }
            }

            return personsEvents;
        }
    }
}
