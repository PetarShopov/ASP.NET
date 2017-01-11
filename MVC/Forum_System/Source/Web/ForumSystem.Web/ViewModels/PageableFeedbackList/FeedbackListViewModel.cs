using System.Collections.Generic;

namespace ForumSystem.Web.ViewModels.PageableFeedbackList
{
    public class FeedbackListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}