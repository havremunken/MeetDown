﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetDown.Core.Utility;
using Raven.Client;

namespace MeetDown.Web.Controllers
{
    /// <summary>
    /// En enkel controller som brukes for å legge inn litt data i databasen etter en reset
    /// </summary>
    public class SeedController : Controller
    {
        #region Fields

        private readonly DatabaseSeeder _seeder;

        #endregion

        #region Constructor

        public SeedController(DatabaseSeeder seeder)
        {
            _seeder = seeder;
        }

        #endregion

        public ActionResult Seed()
        {
            _seeder.PerformSeed();

            return RedirectToAction("Index", "Home");
        }

    }
}