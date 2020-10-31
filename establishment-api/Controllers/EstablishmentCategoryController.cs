using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using establishment_models.Establishment;
using establishment_service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace establishment_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentCategoryController : ControllerBase
    {
        public IEstablishmentCategoryService _establishmentCategoryService { get; set; }

        public EstablishmentCategoryController(IEstablishmentCategoryService _establishmentCategoryService)
        {
            this._establishmentCategoryService = _establishmentCategoryService;
        }

        [HttpPost]
        public ActionResult CreateEstablishment([FromBody]EstablishmentCategoryModel establishmentCategory)
        {
            try
            {
                var id = _establishmentCategoryService.CreateEstablishmentCategory(establishmentCategory);

                return Ok(new { Created = true, IdInsered = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetEstablishmentCategory()
        {
            try
            {
                var _establishmentCategory = _establishmentCategoryService.GetEstablishmentCategoryModels();

                return Ok(new { EstablishmentCategoryModel = _establishmentCategory });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateEstablishment(EstablishmentCategoryModel establishmentCategory)
        {
            try
            {
                var sucess = _establishmentCategoryService.SaveEstablishmentCategory(establishmentCategory);

                return Ok(sucess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteEstablishment(EstablishmentCategoryModel establishmentCategory)
        {
            try
            {
                var sucess = _establishmentCategoryService.RemoveEstablishmentCategory(establishmentCategory);

                return Ok(sucess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}