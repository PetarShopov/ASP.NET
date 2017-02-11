using AutoMapper;
using LiveBet.Data.Common.Contracts;
using LiveBet.Data.Models;
using LiveBet.Services.Data.Contracts;
using LiveBet.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveBet.Controllers
{
    public class HomeController : BaseController
    {
        private IUrlToDBService urlToDBService;

        public HomeController(IData data, IUrlToDBService urlToDBService)
            : base(data)
        {
            this.urlToDBService = urlToDBService;
        }
        public ActionResult Index()
        {
            ICollection<Sport> sportss = urlToDBService.GetData();

            //Get from Database
            var sports = this.Data.Sports.All();

            //Get from RAM
            //var sport = sportss.ToArray();
            //var sports = new Sport[3];
            //sports[0] = sport[2];
            //sports[1] = sport[3];
            //sports[2] = sport[8];

            //Refresh and get new data on every 60s
            //Response.AddHeader("Refresh", "60");
            return this.View(sports);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}