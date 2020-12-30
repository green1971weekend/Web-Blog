using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on data from front-end razor pages.
    /// </summary>
    public class CommentViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public int MainCommentId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
