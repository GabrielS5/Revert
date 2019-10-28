using API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IRecordsRepository
    {
        Task Insert(Record record);
        Task<IEnumerable<Record>> GetAll();
        Task<Record> GetById(Guid id);
        Task Delete(Guid id);
    }
}
