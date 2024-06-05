using Microsoft.AspNetCore.Mvc;
using ETAPI.Models;
using ETAPI.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using ETAPI.Utilities;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _FormService;
        public FormController(IFormService FormService)
        {
            _FormService = FormService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;

        [HttpGet("get-form")]
        public async Task<ActionResult<List<Form>>> GetForms()
        {
            try
            {
                var Forms = await _FormService.GetForms();
                if (Forms.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(Forms);
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
        [HttpPost("add-form")]
        public async Task<IActionResult> AddForm([FromBody] Form form)
        {
            try
            {
                await _FormService.AddForm(form);
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
        [HttpGet("get-form-by-id/{id}")]
        public async Task<IActionResult> GetFormById(int id)
        {
            try
            {
                var form = await _FormService.GetFormById(id);
                if (form == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(form);
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

        [HttpPut("update-form/{id}")]
        public async Task<IActionResult> UpdateFormById(int id, [FromBody] FormDto form)
        {
            try
            {
                var updatedform = await _FormService.UpdateForm(id, form);
                if (updatedform == null) {  
                    return NotFound(Nullresponse);
                }
                return Ok(updatedform);
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

        [HttpDelete("delete-form")]
        public async Task<IActionResult> RemoveForm(int id)
        {
            try
            {
                var deletecase  = await _FormService.DeleteForm(id);
                if (deletecase == null) { 
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



