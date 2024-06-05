using Automation.Controllers.Interface;
using Automation.Model;
using Automation.Model.Dto;
using Automation.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Automation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
            private readonly IAutomationService _VariableService;

            public AutomationController(IAutomationService variableService)
            {
                _VariableService = variableService;
            }
            public string Nullresponse = Textes.emptyornullresponse;
            public string Badrequest = Textes.badrequest;
            public string Succussfull = Textes.successfull;
            [HttpGet("get-variable")]
            public async Task<ActionResult<List<AutomationModel>>> GetVariables()
            {
                try
                {
                    var Variables = await _VariableService.GetVariable();
                    if (Variables == null)
                    {
                        return NotFound(Nullresponse);
                    }

                    return Ok(Variables);
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
            [HttpPost("add-variable")]
            public async Task<IActionResult> Addvariable([FromBody] AutomationModel variable)
            {
                try
                {
                    await _VariableService.AddVariable(variable);
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
            [HttpGet("get-variable-by-id/{id}")]
            public async Task<IActionResult> GetvariableById(int id)
            {
                try
                {
                    var variable = await _VariableService.GetVariableById(id);
                    if (variable == null)
                    {
                        return NotFound(Nullresponse);
                    }
                    return Ok(variable);
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

            [HttpPut("update-variable/{id}")]
            public async Task<IActionResult> UpdatevariableById(int id, [FromBody] AutomationDto  variable)
            {
                try
                {
                    var updatedvar = await _VariableService.UpdateVariable(variable, id);
                    if (updatedvar == null)
                    {
                        return NotFound(Nullresponse);
                    }
                    return Ok(updatedvar);
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

            [HttpDelete("delete-variable")]
            public async Task<IActionResult> Removevariable(int id)
            {
                try
                {
                    var deletecase = await _VariableService.DeleteVariable(id);
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
