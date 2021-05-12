using Microsoft.AspNetCore.SignalR;
using PersonEvents_WebApp.Repository.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Hubs
{
    public class PersonEventsHub : Hub
    {
        private readonly IEventsRepository _repository;

        public PersonEventsHub(IEventsRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterEvent(string name, DateTime dob, string eventData)
        {
            await _repository.RegisterEvent(name, dob, eventData);
            await Clients.All.SendAsync("UpdatePersonEvents", _repository.GetPersonEvents(name, dob));
        }

        public async Task DeleteEvent(string name, DateTime dob, string eventData)
        {
            await _repository.DeleteEvent(name, dob, eventData);
            await Clients.All.SendAsync("UpdatePersonEvents", _repository.GetPersonEvents(name, dob));
        }
    }
}
