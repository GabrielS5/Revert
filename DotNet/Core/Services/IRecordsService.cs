﻿using Core.Entities;
using Core.Entities.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IRecordsService
    {
        Task Insert(Record record);
        Task<IQueryable<Record>> GetAll(RecordsQuery query);
        Task<Record> GetById(Guid id);
        Task<Record> GetLatest();
        Task Delete(Guid id);
        Task<List<Keyword>> GetKeywords(Record record);
    }
}
