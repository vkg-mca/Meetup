using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meetup.Entities.Models;
using Microsoft.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meetup.Repository
{
    public class MeetupDetailRepository : IMeetupDetailRepository
    {
        private readonly MeetupDbContext _context;
        public MeetupDetailRepository(MeetupDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MeetupDetail>> GetAllAsync()
        {
            return await _context.MeetupDetail.ToListAsync();
        }

        public async Task<MeetupDetail> GetAsync(int identity)
        {
            return await _context.MeetupDetail.FindAsync(identity);
        }

        public async Task UpdateAsync(int identity, MeetupDetail entity)
        {
            if (identity != entity.Id)
                throw new AccessViolationException(
                    $"Source entity id {entity.Id} and target entity id {identity} do not match");
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(MeetupDetail entity) 
        {
             _context.MeetupDetail.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int identity)
        {
            var entity = await _context.MeetupDetail.FindAsync(identity);
            if (entity == null) return;
            _context.MeetupDetail.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
