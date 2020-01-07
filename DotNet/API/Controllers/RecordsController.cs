using Application.Aglomera;
using AutoMapper;
using Core.Entities;
using Core.Entities.Models;
using Core.Entities.Queries;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsService service;
        private readonly IMapper mapper;

        public RecordsController(IRecordsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Record>>> Get([FromQuery]RecordsQuery query)
        {
            var result = await service.GetAll(query);

            return Ok(mapper.Map<List<Record>, List<RecordModel>>(result.ToList()));
        }

        [HttpGet("updated")]
        public async Task<ActionResult> HasUpdated([FromQuery]DateTime date)
        {
            var result = await service.GetLatest();

            return Ok(date < result.CreationDate);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> Get(Guid id)
        {
            var result = await service.GetById(id);

            return Ok(mapper.Map<Record, RecordModel>(result));
        }

        [HttpPost]
        public async Task Post([FromBody]RecordModel model)
        {
            await service.Insert(mapper.Map<RecordModel, Record>(model));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> Delete(Guid id)
        {
            await service.Delete(id);

            return Ok();
        }
    }
}
