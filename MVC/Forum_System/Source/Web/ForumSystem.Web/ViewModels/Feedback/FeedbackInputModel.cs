using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ForumSystem.Web.ViewModels.Feedback
{
    public class FeedbackInputModel
    {   
        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}