using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Meetup.Entities.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace MeetupApi
{
    [Route("meetup")]
    [ApiController]
    public class MeetupDetailsController : ControllerBase
    {
        private readonly MeetupDbContext _context;

     
        public MeetupDetailsController(MeetupDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetupDetail>>> Get()
        {
            return await _context.MeetupDetail.ToListAsync();
        }

      
        [HttpGet("{id:required}")]
        public async Task<ActionResult<MeetupDetail>> GetById(int id)
        {
            var meetupDetail = await _context.MeetupDetail.FindAsync(id);

            if (meetupDetail == null)
            {
                return NotFound();
            }

            return meetupDetail;
        }

     
        [HttpPut("{id:required}")]
        public async Task<IActionResult> PutMeetupDetail(int id, [FromBody] MeetupDetail meetupDetail)
        {
            if (id != meetupDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(meetupDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetupDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

     
        [HttpPost]
        public async Task<ActionResult<MeetupDetail>> CreateAsync([FromBody] MeetupDetail meetupDetail)
        {
            _context.MeetupDetail.Add(meetupDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MeetupDetailExists(meetupDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetById", new { id = meetupDetail.Id }, meetupDetail);
        }

      
        [HttpDelete("{id:required}")]
        public async Task<ActionResult<MeetupDetail>> DeleteMeetupDetail(int id)
        {
            var meetupDetail = await _context.MeetupDetail.FindAsync(id);
            if (meetupDetail == null)
            {
                return NotFound();
            }
            _context.MeetupDetail.Remove(meetupDetail);
            await _context.SaveChangesAsync();

            return meetupDetail;
        }

        [HttpGet]
        [Route("health")]
        public async Task<ActionResult<string>> Health()
        {
            return "healthy";
        }
        [HttpGet]
        [Route("liveness")]
        public async Task<ActionResult<string>> Live()
        {
            return "live";
        }

        private bool MeetupDetailExists(int id)
        {
            return _context.MeetupDetail.Any(e => e.Id == id);
        }
    }
}
