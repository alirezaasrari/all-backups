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
    public class CodeFormXController : ControllerBase
    {
        private readonly ICodeFormXService _CodeFormXService;
        public CodeFormXController(ICodeFormXService CodeFormXService)
        {
            _CodeFormXService = CodeFormXService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;
        [HttpGet("get-codeformx")]
        public async Task<ActionResult<List<CodeFormx>>> GetCodeFormX()
        {
            try
            {
                var CodeFormX = await _CodeFormXService.GetCodeFormX();
                if (CodeFormX.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(CodeFormX);
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
        [HttpPost("add-codeformx")]
        public async Task<IActionResult> Addcodeformx([FromBody] CodeFormx codeformx)
        {
            try
            {
                await _CodeFormXService.AddCodeFormX(codeformx);
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
        [HttpGet("get-codeformx-by-id/{id}")]
        public async Task<IActionResult> GetcodeformxById(int id)
        {
            try
            {
                var CodeFormX = await _CodeFormXService.GetCodeFormXById(id);
                if (CodeFormX == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(CodeFormX);
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

        [HttpPut("update-codeformx/{id}")]
        public async Task<IActionResult> UpdatecodeformxById([FromBody] CodeFormXDto codeformx, int id)
        {
            try
            {
                var updatedCodeFormX = await _CodeFormXService.UpdateCodeFormX(codeformx, id);
                if (updatedCodeFormX == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedCodeFormX);
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

        [HttpDelete("delete-codeformx")]
        public async Task<IActionResult> Removecodeformx(int id)
        {
            try
            {
                var deletecase = await _CodeFormXService.DeleteCodeFormX(id);
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

