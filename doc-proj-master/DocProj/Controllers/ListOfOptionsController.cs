using DocProj.Context;
using DocProj.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListOfOptionsController : ControllerBase
    {
        private readonly DataContext _Context;
        public ListOfOptionsController(DataContext context)
        {
            _Context = context;
        }
        [HttpGet("getlistofoptions")]
        public async Task<ActionResult<List<ListOfOption>>> GetListOfOptions()
        {
            try
            {
                var list = await _Context.ListOfOptions.Include(a => a.OptionDetail).ToListAsync();
                if (list != null)
                {
                    return Ok(list);
                }
                return NotFound();
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
                    return BadRequest();
                };
            }
        }
        [HttpGet("getoptionpdfbyid")]
        public async Task<ActionResult<List<byte[]>>> GetListOfOptionsPdf(int id)
        {
            try
            {
                var pdffile = await _Context.OptionDetail.Where(t => t.Id == id).FirstOrDefaultAsync();
                if (pdffile != null)
                {
                    return Ok(pdffile.PdfFile);
                }
                return NotFound();
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
                    return BadRequest();
                };
            }
        }
        [HttpPost("addoptiondetail")]
        public async Task<IActionResult> CreateOptionDetail([FromBody] OptionDetail option)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _Context.OptionDetail.Add(option);
                await _Context.SaveChangesAsync();
                return Ok(option);
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
                    return BadRequest();
                };
            }
        }
        [HttpPost("addlistofoption")]
        public async Task<IActionResult> CreateListOfOption([FromBody] ListOfOption list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _Context.ListOfOptions.Add(list);
                await _Context.SaveChangesAsync();
                return Ok(list);
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
                    return BadRequest();
                };
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListOfOption(int id)
        {
            try
            {
                var itemlist = await _Context.ListOfOptions.Include(o => o.OptionDetail).FirstOrDefaultAsync(o => o.Id == id);

                if (itemlist == null)
                {
                    return NotFound();
                }
                foreach (var item in itemlist.OptionDetail)
                {
                    _Context.OptionDetail.Remove(item);
                }
                _Context.ListOfOptions.Remove(itemlist);
                await _Context.SaveChangesAsync();
                return Ok();
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
                    return BadRequest();
                };
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListOfOption(int id, [FromBody] ListOfOption list)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existinglistitem = await _Context.ListOfOptions.Include(o => o.OptionDetail).FirstOrDefaultAsync(o => o.Id == id);

                if (existinglistitem == null)
                {
                    return NotFound();
                }
                existinglistitem.Name = list.Name;
                existinglistitem.OptionDetail = list.OptionDetail;
                await _Context.SaveChangesAsync();
                return Ok();
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
                    return BadRequest();
                };
            }
        }
    }
}
