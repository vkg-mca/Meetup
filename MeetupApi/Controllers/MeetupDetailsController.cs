using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Meetup.Entities.Models;
using Meetup.Repository;
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
        private readonly IMeetupDetailRepository _repo;


        public MeetupDetailsController(MeetupDbContext context, IMeetupDetailRepository repo)
        {
            _context = context;
            _repo = repo;
        }


        [HttpGet]
        public async Task<IEnumerable<MeetupDetail>> Get()
        {
            return await _repo.GetAllAsync();
        }


        [HttpGet("{id:required}")]
        public async Task<ActionResult<MeetupDetail>> GetByIdAsync(int id)
        {
            var meetup = await _repo.GetAsync(id);

            if (meetup == null)
                return NotFound();

            return meetup;
        }


        [HttpPut("{id:required}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MeetupDetail meetup)
        {
            if (id != meetup.Id)
                return BadRequest();
            await _repo.UpdateAsync(id, meetup);
            return Accepted();
        }


        [HttpPost]
        public async Task<ActionResult<MeetupDetail>> CreateAsync([FromBody] MeetupDetail meetup)
        {
            try
            {
                await _repo.CreateAsync(meetup);
            }
            catch (DbUpdateException)
            {
                if (MeetupDetailExists(meetup.Id))
                    return Conflict();
            }
            return CreatedAtAction("GetByIdAsync", new { id = meetup.Id }, meetup);
        }


        [HttpDelete("{id:required}")]
        public async Task<ActionResult<MeetupDetail>> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
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
