using Core.Entities;
using Core.Entities.Queries;
using Core.Repositories;
using Data.Aspects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    [Log]
    public class RecordsRepository : IRecordsRepository
    {
        private readonly AppDbContext context;

        public RecordsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Delete(Guid id)
        {
            var record = await GetById(id);
            context.Records.Remove(record);
            await Save();
        }

        public async Task<IQueryable<Record>> GetAll(RecordsQuery query)
        {
            var records = context.Records
                .Include(i => i.Keywords)
                .OrderByDescending(o => o.CreationDate)
                .AsQueryable();

            if (query.StartDate.HasValue)
            {
                records = records.Where(r => r.CreationDate >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                records = records.Where(r => r.CreationDate <= query.EndDate.Value);
            }

            return records;
        }

        public async Task<Record> GetById(Guid id)
        {
            return await context.Records
                .Include(i => i.Keywords)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Insert(Record record)
        {
            await context.AddAsync(record);
            await Save();
        }

        private async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
