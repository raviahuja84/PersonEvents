using PersonEvents_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Repository.cs
{
    public interface IEventsRepository
    {
        //PersonEvents PersonEvents { get; }

        Task RegisterEvent(string name, DateTime dob, string eventData);

        Task DeleteEvent(string name, DateTime dob, string eventData);

        PersonEvents GetPersonEvents(string name, DateTime dob);

        IList<PersonEvents> GetPersonEvents();
    }
}
