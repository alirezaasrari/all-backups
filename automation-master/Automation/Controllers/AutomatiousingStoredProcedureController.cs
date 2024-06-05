using Automation.Controllers.Interface;
using Automation.Model;
using Automation.Model.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Automation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomatiousingStoredProcedureController : ControllerBase
    {
        private readonly DataContext _context;

        public AutomatiousingStoredProcedureController(DataContext context)
        {
            _context = context;
        }

        // GET: api/YourController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutomationModel>>> Get()
        {
            return await _context.TabelName.FromSqlRaw("EXECUTE dbo.GetAllYourEntities").ToListAsync();
        }

        // GET: api/YourController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutomationModel>> Get(int id)
        {
            var entity = await _context.TabelName.FromSqlRaw("EXECUTE dbo.GetYourEntityById @Id", new SqlParameter("@Id", id)).SingleOrDefaultAsync();

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // POST: api/YourController
        [HttpPost]
        public async Task<ActionResult<AutomationModel>> Post([FromBody] AutomationModel entity)
        {
            await _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.CreateYourEntity @Name, @Description, ...",
                entity.Name, entity.Description, /* ...other parameters... */);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // PUT: api/YourController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AutomationModel entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            await _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.UpdateYourEntity @Id, @Name, @Description, ...",
                entity.Id, entity.Name, entity.Description, /* ...other parameters... */);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/YourController/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AutomationModel>> Delete(int id)
        {
            var entity = await _context.TabelName.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlRawAsync("EXECUTE dbo.DeleteYourEntity @Id", new SqlParameter("@Id", id));
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
