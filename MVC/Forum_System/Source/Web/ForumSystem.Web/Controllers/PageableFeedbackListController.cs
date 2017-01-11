using AutoMapper.QueryableExtensions;
using ForumSystem.Data.Common.Repository;
using ForumSystem.Data.Models;
using ForumSystem.Web.ViewModels.PageableFeedbackList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ForumSystem.Web.Controllers
{
    [Authorize]
    public class PageableFeedbackListController : Controller
    {
        const int ItemsPerPage = 4;
        IDeletableEntityRepository<Feedback> feedbacks;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        [HttpGet]
        public ActionResult Index(int id = 1)
        {
            FeedbackListViewModel viewModel;
            if (this.HttpContext.Cache["Feedback_page_" + id] != null)
            {
                viewModel = (FeedbackListViewModel)this.HttpContext.Cache["Feedback_page_" + id];
            }
            else
            {
                var page = id;
                var allItemsCount = this.feedbacks.All().Count();
                var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
                var itemsToSkip = (page - 1) * ItemsPerPage;
                var feedbacks = this.feedbacks.All()
                    .OrderBy(x => x.CreatedOn)
                    .ThenBy(x => x.Id)
                    .Skip(itemsToSkip).Take(ItemsPerPage)
                    .Project().To<FeedbackViewModel>().ToList();

                viewModel = new FeedbackListViewModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    Feedbacks = feedbacks
                };

                this.HttpContext.Cache["Feedback_page_" + id] = viewModel;
            }

            return this.View(viewModel);
        }
    }
}