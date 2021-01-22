using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models.Requests
{
    public class EditNoteRequest
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
