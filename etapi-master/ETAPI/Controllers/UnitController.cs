using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _UnitService;
        public UnitController(IUnitService UnitService)
        {
            _UnitService = UnitService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;
        [HttpGet("get-unit")]
        public async Task<ActionResult<List<Unit>>> GetUnits()
        {
            try
            {
                var Units = await _UnitService.GetUnit();
                if (Units == null)
                {
                    return NotFound(Nullresponse);
                }

                return Ok(Units);
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
        [HttpPost("add-unit")]
        public async Task<IActionResult> Addunit([FromBody] Unit unit)
        {
            try
            {
                await _UnitService.AddUnit(unit);
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
        [HttpGet("get-unit-by-id/{id}")]
        public async Task<IActionResult> GetcodeformxById(int id)
        {
            try
            {
                var unit = await _UnitService.GetById(id);
                if (unit == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(unit);
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

        [HttpPut("update-unit/{id}")]
        public async Task<IActionResult> UpdatecodeformxById([FromBody] UnitDto unit, int id)
        {
            try
            {
                var updatedunit = await _UnitService.UpdateUnit(unit, id);
                if (updatedunit == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedunit);
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

        [HttpDelete("delete-unit")]
        public async Task<IActionResult> Removeunit(int id)
        {
            try
            {
                var deletecase = await _UnitService.DeleteUnit(id);
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
