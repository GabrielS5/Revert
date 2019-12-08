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
        private readonly ITranslateService translateService;
        private readonly IKeywordsService keywordsService;

        public RecordsController(IRecordsService service, IMapper mapper, ITranslateService translateService, IKeywordsService keywordsService)
        {
            this.service = service;
            this.mapper = mapper;
            this.translateService = translateService;
            this.keywordsService = keywordsService;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Record>>> Get([FromQuery]RecordsQuery query)
        {
            var result = await service.GetAll(query);

            return Ok(mapper.Map<List<Record>, List<RecordModel>>(result.ToList()));
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
