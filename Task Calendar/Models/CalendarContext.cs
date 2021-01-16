using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Task_Calendar.Models
{
    public class CalendarContext : DbContext
    {
        public CalendarContext() : base()
        {
            //Sets the database initializer to use for the given context type. 
            //The database initializer is called when a the given DbContext type is used to access a database for the first time. 
            //ConnectionStrings was added to Webconfig to join DB
            Database.SetInitializer(new CreateDatabaseIfNotExists<CalendarContext>());
        }
     public System.Data.Entity.DbSet<Task_Calendar.Models.Appointment> Appointments { get; set; }
    }
}