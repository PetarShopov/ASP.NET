using ForumSystem.Web.Infrastructure.Mapping;
using System;
using AutoMapper;
using ForumSystem.Web.Infrastructure;

namespace ForumSystem.Web.ViewModels.PageableFeedbackList
{
    public class FeedbackViewModel : IMapFrom<ForumSystem.Data.Models.Feedback>, IHaveCustomMappings
    {
        ISanitizer sanitizer;

        public FeedbackViewModel()
        {
            sanitizer = new HtmlSanitizerAdapter();
        }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => this.sanitizer.Sanitize(this.Content);

        public DateTime CreatedIOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ForumSystem.Data.Models.Feedback, FeedbackViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}