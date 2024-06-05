using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormUnitController : ControllerBase
    {
        private readonly IFormUnitService _FormUnitService;
        public FormUnitController(IFormUnitService FormUnitService)
        {
            _FormUnitService = FormUnitService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;

        [HttpGet("get-formunit")]
        public async Task<ActionResult<List<FormUnit>>> GetFormUnits()
        {
            try
            {
                var FormUnits = await _FormUnitService.GetFormUnit();
                if (FormUnits.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(FormUnits);
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    return BadRequest(ineerexception.Message);
                }
                else
                {
                    return BadRequest(Badrequest);
                };
            }

        }
        [HttpPost("add-formunit")]
        public async Task<IActionResult> AddFormunit([FromBody] FormUnit formunit)
        {
            try
            {
                await _FormUnitService.AddFormUnit(formunit);
                return Ok(Succussfull);
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    return BadRequest(ineerexception.Message);
                }
                else
                {
                    return BadRequest(Badrequest);
                };
            }

        }
        [HttpGet("get-formunit-by-id/{id}")]
        public async Task<IActionResult> GetFormunitById(int id)
        {
            try
            {
                var formunit = await _FormUnitService.GetFormUnitById(id);
                if (formunit == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(formunit);
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    return BadRequest(ineerexception.Message);
                }
                else
                {
                    return BadRequest(Badrequest);
                };
            }

        }

        [HttpPut("update-formunit/{id}")]
        public async Task<IActionResult> UpdateFormunitById([FromBody] FormUnitDto formunit, int id)
        {
            try
            {
                var updatedformunit = await _FormUnitService.UpdateFormUnit(formunit, id);
                if (updatedformunit == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedformunit);
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    return BadRequest(ineerexception.Message);
                }
                else
                {
                    return BadRequest(Badrequest);
                };
            }

        }

        [HttpDelete("delete-formunit")]
        public async Task<IActionResult> RemoveFormunit(int id)
        {
            try
            {
                var deletecase = await _FormUnitService.DeleteFormUnit(id);
                if (deletecase == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(Succussfull);
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    return BadRequest(ineerexception.Message);
                }
                else
                {
                    return BadRequest(Badrequest);
                };
            }

        }
    }
}
