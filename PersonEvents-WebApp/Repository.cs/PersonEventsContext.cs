using Microsoft.EntityFrameworkCore;
using PersonEvents_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.Repository.cs
{
    public class PersonEventsContext : DbContext
    {
        public PersonEventsContext(DbContextOptions<PersonEventsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Person { get; set; }

        public virtual DbSet<Events> Events { get; set; }
    }
}
