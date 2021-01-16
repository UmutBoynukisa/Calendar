using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DHTMLX.Scheduler;

namespace Task_Calendar.Models
{
    public class Appointment
    {
        [Key] //-> Identify field in DB. 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //-> an expicit Primary Key in DB


        //Specified which properties of model scheduler will be used as te mandatory id,text and start/end dates.
        [DHXJson(Alias ="id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        public string Description { get; set; }

        [DHXJson(Alias = "start_date")]
        public DateTime StartDate { get; set; }

        [DHXJson(Alias = "end_date")]
        public DateTime EndDate { get; set; }
    }
}