using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meetup.Entities.Models;

namespace Meetup.Repository
{
    public interface IMeetupDetailRepository: IRepository<int, MeetupDetail>
    {
    }
}
