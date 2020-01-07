using AutoMapper;
using Core.Entities;
using Core.Entities.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KeywordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IRecordsService service;
        private readonly IMapper mapper;
        private readonly IClusteringService _clusteringService;
        private readonly ITranslateService _translateService;

        public KeywordsController(
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

        [HttpGet("process")]
        [Authorize]
        public async Task<ActionResult> Translate([FromQuery]RecordModel model)
        {
            var record = mapper.Map<RecordModel, Record>(model);
            var result = await service.GetKeywords(record);

            return Ok(result);
        }
    }
}
