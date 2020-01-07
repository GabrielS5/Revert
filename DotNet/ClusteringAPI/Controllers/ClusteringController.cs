using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Entities.Models;
using Core.Entities.Queries;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClusteringAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusteringController : ControllerBase
    {
        private readonly IRecordsService service;
        private readonly IMapper mapper;
        private readonly IClusteringService _clusteringService;
        private readonly ITranslateService _translateService;

        public ClusteringController(
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

        [Authorize]
        [HttpGet("process")]
        public async Task<ActionResult> Translate([FromQuery]RecordModel model)
        {
            var records = (await service.GetAll(new RecordsQuery())).ToList();
            var record = mapper.Map<RecordModel, Record>(model);
            record.Keywords = await service.GetKeywords(record);

            var result = _clusteringService.Cluster(records, record);

            return Ok(new { Diagnosis = result });
        }
    }
}
