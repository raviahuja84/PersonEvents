using Microsoft.AspNetCore.SignalR;
using PersonEvents_WebApp.Hubs;
using PersonEvents_WebApp.Models;
using PersonEvents_WebApp.Repository.cs;
using System;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;

namespace PersonEvents_WebApp.SqlTableDependencies
{
    public class EventsDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        private readonly IEventsRepository _repository;
        private readonly IHubContext<PersonEventsHub> _hubContext;
        private SqlTableDependency<Events> _tableDependency;

        public EventsDatabaseSubscription(IEventsRepository repository, IHubContext<PersonEventsHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            Console.WriteLine("Configure - Begin.");

            _tableDependency = new SqlTableDependency<Events>(connectionString, null, null, null, null, /*null,*/ DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<Events> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity
                var changedEntity = e.Entity;

                // TO DO -- RKA ???
                _hubContext.Clients.All.SendAsync("UpdatePersonEvents", _repository.GetPersonEvents());
            }
        }

        #region IDisposable

        ~EventsDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
