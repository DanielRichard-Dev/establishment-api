﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using establishment_models.Establishment;
using establishment_service.Establishment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace establishment_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        public EstablishmentService _establishmentService { get; set; }

        public IConfiguration _configuration { get; set; }

        public EstablishmentController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _establishmentService = new EstablishmentService(_configuration);
        }

        [HttpPost]
        public ActionResult CreateEstablishment([FromBody]EstablishmentModel establishment)
        {
            try
            {
                var id = _establishmentService.CreateEstablishment(establishment);

                return Ok(new { Created = true, IdInsered = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}