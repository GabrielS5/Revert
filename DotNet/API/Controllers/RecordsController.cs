using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsRepository repository;

        public RecordsController(IRecordsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> Get()
        {
            return Ok(await repository.GetAll());
        }


        [HttpGet("{id}")]
        public async  Task<ActionResult<Record>> Get(Guid id)
        {
            return Ok(await repository.GetById(id));
        }
    }
}
