using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TerminalWebApi.Model;
using TerminalWebApi.Service;
using TerminalWebApi.Service.Interfaces;
using TerminalWebApi.Utilities;

namespace TerminalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalController : Controller
    {
        private readonly ITerminalService _TerminalService;
        private readonly TcpClientService _tcpClientService;

        public TerminalController(ITerminalService TerminalService, TcpClientService tcpClientService)
        {
            _TerminalService = TerminalService;
            _tcpClientService = tcpClientService;
        }

        public string Nullresponse = Textes.emptyornullresponse;
        public string Badrequest = Textes.badrequest;
        public string Succussfull = Textes.successfull;

        [HttpPost]
        public async Task<ActionResult> PostData([FromBody] PostDataForTcp inputData)
        {
            await _tcpClientService.SendMessageToServer(inputData);

            return Ok();
        }
        [HttpGet("terminaldata")]
        public async Task<ActionResult<List<TerminalData>>> GetTerminalData()
        {
            try
            {
                var TerminalData = await _TerminalService.GetAll();
                if (TerminalData.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(TerminalData);
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

        [HttpGet("terminaldata/{id}")]
        public async Task<ActionResult<List<TerminalData>>> GetTerminalDataById(long id)
        {
            try
            {
                var TerminalData = await _TerminalService.GetAll();
                var FilteredList = Search(TerminalData, id);
                if (FilteredList.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(FilteredList);
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

        [HttpGet("error")]
        public async Task<ActionResult<List<TerminalError>>> Geterror()
        {
            try
            {
                var TerminalErrorData = await _TerminalService.GetAllError();
                if (TerminalErrorData.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(TerminalErrorData);
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

        [HttpGet("ioerror")]
        public async Task<ActionResult<List<TerminalIoError>>> Getioerror()
        {
            try
            {
                var TerminalIoErrorData = await _TerminalService.GetAllIoError();
                if (TerminalIoErrorData.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(TerminalIoErrorData);
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

        [HttpGet("socketerror")]
        public async Task<ActionResult<List<TerminalSocketError>>> Getsocketerror()
        {
            try
            {
                var TerminalSocketErrorData = await _TerminalService.GetAllSocketError();
                if (TerminalSocketErrorData.IsNullOrEmpty())
                {
                    return NotFound(Nullresponse);
                }

                return Ok(TerminalSocketErrorData);
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
        private static List<TerminalData> Search(List<TerminalData> list, long num)
        {
            try
            {
                var newlist = new List<TerminalData>();
                foreach (TerminalData p in list)
                {
                    if (p.DataLoggerSerialInDecimal == num)
                    {
                        newlist.Add(p);
                    };
                }
                return newlist;
            }
            catch (Exception ex)
            {

                var ineerexception = ex.InnerException;
                if (ineerexception != null)
                {
                    Console.WriteLine(ineerexception.Message);
                    return new List<TerminalData>();
                }
                else
                {
                    Console.WriteLine("bad request");
                    return new List<TerminalData>();
                };
            }

        }
    }
}
