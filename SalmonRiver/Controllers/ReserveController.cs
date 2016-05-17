﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalmonRiver;
using SalmonRiver.Models;

namespace SalmonRiver.Controllers
{
    public class ReserveController : Controller
    {
        private SalmonRiverEntities db = new SalmonRiverEntities();

        // GET: Reserve
        public ActionResult Index()
        {
            var holds = db.Holds.Include(h => h.Date);
            return View(holds.ToList());
        }

        [HttpPost]
        public ActionResult Index(BookNowViewModel bookNow)
        {
            if (bookNow.Start < DateTime.Today || bookNow.End < DateTime.Today || bookNow.End < bookNow.Start)
            {
                return RedirectToAction("Index", "Home", new { error = (int)Errors.BookNow_InvalidStartOrEndDate });
            }

            DateTime start = bookNow.Start;
            DateTime end = bookNow.End;
            int guests = bookNow.Guests;

            List<DateTime> requiredDates = new List<DateTime>();

            DateTime startRef = start.Date;
            while (startRef <= end)
            {
                requiredDates.Add(startRef);
                startRef = startRef.AddDays(1);
            }

            DateTime holdExpires =DateTime.Now.ToUniversalTime().AddMinutes(5);

            List<Hold> holds = db.Dates.Where(i=>requiredDates.Contains(i.Date1) && i.IsActive)
                .Select(i=> new Hold(){
                     DateID = i.DateID,
                      Expiration = holdExpires
                })
                .ToList();

            if(holds.Count() < requiredDates.Count()){
                ViewBag.Error = "SOME_DATES_UNAVAILABLE";
                return View();
            }

            DateTime expiration = DateTime.Now.ToUniversalTime();

            var check = db.Holds.Where(i => requiredDates.Contains(i.Date.Date1) && expiration <= i.Expiration).Count();

            if (check > 0)
            {
                // one or more of the days selected is already being held

                ViewBag.Error = "BEING_HELD";
                return View();
            }
            else
            {
                // hold for this user.
                var dates = db.Holds.AddRange(holds).ToList();
                Session["Holds"] = dates;

                return View(new TemporaryReservationViewModel()
                {
                    Dates = dates.Select(i => i.Date.Date1).ToList(),
                    Expires = holdExpires,
                    GuestCount = guests
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
