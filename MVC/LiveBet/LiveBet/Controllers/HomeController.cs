namespace LiveBet.Controllers
{
    using Data;
    using LiveBet.Data.Common.Contracts;
    using LiveBet.Data.Models;
    using LiveBet.Services.Data.Contracts;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
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
            if (!this.Data.Sports.All().Any())
            {
               urlToDBService.InitialRequest();
            }
            else
            {
                urlToDBService.UpdateDatabase();
            }

            var sport = this.Data.Sports.All().AsNoTracking();

            Response.AddHeader("Refresh", "60");
            return this.View(sport);
        }

        public ActionResult DisplaySelectedEvent(string id)
        {
            int myId = int.Parse(id);
            Sport model = this.Data.Sports.All().Where(m => m.Id == myId).FirstOrDefault();
            return PartialView("_EventsPartial", model);
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