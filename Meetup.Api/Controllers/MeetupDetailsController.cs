using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Meetup.Entities.Models;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace Meetup.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupDetailsController : ControllerBase
    {
        private readonly MeetupDbContext _context;

        public MeetupDetailsController(MeetupDbContext context)
        {
            _context = context;
        }

        // GET: api/MeetupDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetupDetail>>> GetMeetupDetail()
        {
            return await _context.MeetupDetail.ToListAsync();
        }

        // GET: api/MeetupDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetupDetail>> GetMeetupDetail(int id)
        {
            var meetupDetail = await _context.MeetupDetail.FindAsync(id);

            if (meetupDetail == null)
            {
                return NotFound();
            }

            return meetupDetail;
        }

        // PUT: api/MeetupDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetupDetail(int id, MeetupDetail meetupDetail)
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

        // POST: api/MeetupDetails
        [HttpPost]
        public async Task<ActionResult<MeetupDetail>> PostMeetupDetail(MeetupDetail meetupDetail)
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

            return CreatedAtAction("GetMeetupDetail", new { id = meetupDetail.Id }, meetupDetail);
        }

        // DELETE: api/MeetupDetails/5
        [HttpDelete("{id}")]
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

        private bool MeetupDetailExists(int id)
        {
            return _context.MeetupDetail.Any(e => e.Id == id);
        }
    }
}
