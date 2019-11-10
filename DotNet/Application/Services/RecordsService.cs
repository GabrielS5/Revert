using Core.Entities;
using Core.Entities.Queries;
using Core.Repositories;
using Core.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class RecordsService : IRecordsService
    {
        private readonly IRecordsRepository recordsRepository;

        public RecordsService(IRecordsRepository recordsRepository)
        {
            this.recordsRepository = recordsRepository;
        }

        public async Task Delete(Guid id)
        {
            await recordsRepository.Delete(id);
        }

        public async Task<IQueryable<Record>> GetAll(RecordsQuery query)
        {
            return await recordsRepository.GetAll(query);
        }

        public async Task<Record> GetById(Guid id)
        {
            return await recordsRepository.GetById(id);
        }

        public async Task Insert(Record record)
        {
            await recordsRepository.Insert(record);
        }
    }
}
