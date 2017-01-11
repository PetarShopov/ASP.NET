namespace ForumSystem.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.ViewModels.Home;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public HomeController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var model = this.posts.All().Project().To<IndexBlogPostViewModel>();

            return this.View(model);
        }
    }
}