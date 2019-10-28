using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
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

        public async Task<IEnumerable<Record>> GetAll()
        {
            return context.Records.AsEnumerable();
        }

        public async Task<Record> GetById(Guid id)
        {
            return await context.Records.Where(w => w.Id == id).FirstOrDefaultAsync();
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
