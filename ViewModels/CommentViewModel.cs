using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearchGate.ViewModels
{
    public class CommentViewModel
    {
        public string CommentText { get; set; }
        public int ResearchId { get; set; }
        public int CommentId { get; set; }
    }
}