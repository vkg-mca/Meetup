﻿using System;

namespace Meetup.Entities.Models
{
    public partial class MeetupDetail
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Topic { get; set; }
        public int? ParticipantsCount { get; set; }
    }
}
