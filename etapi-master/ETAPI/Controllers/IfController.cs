using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IfController : ControllerBase
    {
        private readonly IIfService _service;

        public IfController(IIfService service)
        {
            _service = service;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;
        [HttpGet("get-if")]
        public async Task<ActionResult<List<If>>> GetIfs()
        {
            try
            {
                var If = await _service.GetIf();
                if (If == null)
                {
                    return NotFound(Nullresponse);
                }

                return Ok(If);
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
        [HttpPost("add-if")]
        public async Task<IActionResult> Addif([FromBody] If ifobj)
        {
            try
            {
                await _service.AddIf(ifobj);
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
        [HttpGet("get-if-by-id/{id}")]
        public async Task<IActionResult> GetifById(int id)
        {
            try
            {
                var ifobj = await _service.GetIfById(id);
                if (ifobj == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(ifobj);
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

        [HttpPut("update-if/{id}")]
        public async Task<IActionResult> UpdateifById(int id, [FromBody] IfDto ifobj)
        {
            try
            {
                var updatedif = await _service.UpdateIf(ifobj, id);
                if (updatedif == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedif);
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
         
        [HttpDelete("delete-if")]
        public async Task<IActionResult> Removeif(int id)
        {
            try
            {
                var deletecase = await _service.DeleteIf(id);
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
