using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using ETAPI.Interfaces.Interfaces;

namespace ETAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormChartController : ControllerBase
    {
        private readonly IFormChartService _FormChartService;
        public FormChartController(IFormChartService formChartService)
        {
            _FormChartService = formChartService;
        }
        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;
        [HttpGet("get-formchart")]
        public async Task<ActionResult<List<FormChart>>> GetFormCharts()
        {
            try
            {
                var FormChart = await _FormChartService.GetFormChart();
                if (FormChart == null)
                {
                    return NotFound(Nullresponse);
                }

                return Ok(FormChart);
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
        [HttpPost("add-formchart")]
        public async Task<IActionResult> AddFormchart([FromBody] FormChart formchart)
        {
            try
            {
                await _FormChartService.AddFormChart(formchart);
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
        [HttpGet("get-formchart-by-id/{id}")]
        public async Task<IActionResult> GetFormchartById(int id)
        {
            try
            {
                var formchart = await _FormChartService.GetFormChartById(id);
                if (formchart == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(formchart);
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

        [HttpPut("update-formchart/{id}")]
        public async Task<IActionResult> UpdateFormchartById([FromBody] FormChartDto formchart, int id)
        {
            try
            {
                var updatedformchart = await _FormChartService.UpdateFormChart(formchart, id);
                if (updatedformchart == null)
                {
                    return NotFound(Nullresponse);
                }
                return Ok(updatedformchart);
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

        [HttpDelete("delete-formchart")]
        public async Task<IActionResult> RemoveFormchart(int id)
        {
            try
            {
                var deletecase = await _FormChartService.DeleteFormChart(id);
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
