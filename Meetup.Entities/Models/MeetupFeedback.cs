using System;
using System.Collections.Generic;
using System.Text;

namespace Meetup.Entities.Models
{
    public partial class MeetupFeedback
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public int SatisfactionIndex { get; set; }
        public DateTime? Date { get; set; }
    }
}
