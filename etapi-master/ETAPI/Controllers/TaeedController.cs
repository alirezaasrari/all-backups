using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaeedController : ControllerBase
    {
        private readonly ITaeedService _TaeedService;
        public TaeedController(ITaeedService TaeedService)
        {
            _TaeedService = TaeedService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;
        [HttpGet("get-taeed")]
        public async Task<ActionResult<List<Taeed>>> GetTaeed()
        {
            try
            {
                var Taeed = await _TaeedService.GetTaeed();
                if (Taeed == null)
                {
                    return NotFound(Nullresponse);
                }

                return Ok(Taeed);
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
        [HttpPost("add-taeed")]
        public async Task<IActionResult> AddFormchart([FromBody] Taeed taeed)
        {
            try
            {
                await _TaeedService.AddTaeed(taeed);
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
        [HttpGet("get-taeed-by-id/{id}")]
        public async Task<IActionResult> GetFormchartById(int id)
        {
            try
            {
                var taeed = await _TaeedService.GetTaeedById(id);
                if (taeed == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(taeed);
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

        [HttpPut("update-taeed/{id}")]
        public async Task<IActionResult> UpdateFormchartById([FromBody] TaeedDto taeed, int id)
        {
            try
            {
                var updatedtaeed = await _TaeedService.UpdateTaeed(taeed, id);
                if (updatedtaeed == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedtaeed);
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

        [HttpDelete("delete-taeed")]
        public async Task<IActionResult> RemoveFormchart(int id)
        {
            try
            {
                var deletecase = await _TaeedService.DeleteTaeed(id);
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
