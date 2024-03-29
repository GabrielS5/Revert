﻿using AutoMapper;
using Core.Entities;
using Core.Entities.Models;
using Core.Entities.Queries;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingController : ControllerBase
    {
        private readonly IRecordsService service;
        private readonly IMapper mapper;
        private readonly IClusteringService _clusteringService;
        private readonly ITranslateService _translateService;

        public ProcessingController(
            IRecordsService service,
            IMapper mapper,
            IClusteringService clusteringService,
            ITranslateService translateService)
        {
            this.service = service;
            this.mapper = mapper;
            this._clusteringService = clusteringService;
            this._translateService = translateService;
        }

        [HttpGet("translate")]
        public async Task<ActionResult> Translate([FromQuery]string text)
        {
            var result = await _translateService.Translate(text);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]RecordModel model)
        {
            var records = (await service.GetAll(new RecordsQuery())).ToList();
            var record = mapper.Map<RecordModel, Record>(model);
            record.Keywords = await service.GetKeywords(record);

            var result = _clusteringService.Cluster(records, record);

            return Ok(new { Diagnosis = result });
        }
    }
}
