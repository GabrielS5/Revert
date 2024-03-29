﻿using Core.Entities;
using Core.Entities.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IRecordsRepository
    {
        Task Insert(Record record);
        Task<IQueryable<Record>> GetAll(RecordsQuery query);
        Task<Record> GetById(Guid id);
        Task Delete(Guid id);
        Task<Record> GetLatest();
    }
}
