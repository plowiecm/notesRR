using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models.Requests
{
    public class AddNoteRequest
    {
        public string Title { get; set; }
        public string FormattedText { get; set; }
        public string ImageTargetId { get; set; }
    }
}
