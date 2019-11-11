using AspectCore;
using AutoMapper;
using Core.Entities;
using Core.Entities.Models;
using Core.Entities.Queries;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Log]
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
            return Ok(await service.GetAll(query));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> Get(Guid id)
        {
            return Ok(await service.GetById(id));
        }

        [HttpPost]
        public async Task Post([FromBody]RecordModel model)
        {


            await service.Insert(mapper.Map<RecordModel, Record>(model));
        }
    }
}
