using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;

using DHTMLX.Scheduler;// Reference is needed for the scheduler
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;
using DHTMLX.Scheduler.Controls;

using Task_Calendar.Models;

namespace Task_Calendar.Controllers
{

    public class HomeController : Controller
    { 
        private CalendarContext db = new CalendarContext(); //We want to store db context as a private property inside of class
        public ActionResult Index() //Updated configuration of the scheduler(Index action)
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;
            

            //If customer's wishing time can be displayed the first and last hour for the callendar as in below:
            //scheduler.Config.first_hour = 6;
            //scheduler.Config.last_hour = 20;

            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month); // It significantly decreases the loading time, which could be pain, if your calendar has a large amount of events.

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data(DateTime from, DateTime to)
        {
            var apps = db.Appointments.Where(e=> e.StartDate < to && e.EndDate >= from).ToList();
            return new SchedulerAjaxData(apps);

            //The client side expects data to be loaded as a JSON string, as an array of event objects.
            //Used a ShcedulerAjaxData helper class in order to return our model in the format that can be recognized by the scheduler.
        }


        //Configured the client side to send data and update requests to server
        public ActionResult Save(int? id, FormCollection actionValues)
        {
            
            var action = new DataAction(actionValues);

            try
            {
                //approach wit the helper is simplest
                var changedEvent = DHXEventsHelper.Bind<Appointment>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        db.Appointments.Add(changedEvent);
                        break;
                    default: //Udate
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }catch(Exception a)
            {
                action.Type = DataActionTypes.Error;

            }
            return (new AjaxSaveResponse(action));
        }
        protected override void Dispose(bool disposing)
            {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            base.Dispose(disposing);
            }

        
        
        

            
        
    }
}