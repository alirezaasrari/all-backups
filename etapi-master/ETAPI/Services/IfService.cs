using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class IfService : IIfService
    {
        private readonly DataContext _DataContext;

        public IfService(DataContext dataContext)
        {
            _DataContext = dataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddIf(If ifobject)
        {
            If _if = new()
            {
                IdIf = ifobject.IdIf,
                IdVariable = ifobject.IdVariable,
                Next_ID_FormChart = ifobject.Next_ID_FormChart,
                ValueVariable = ifobject.ValueVariable
            };
            _DataContext.FLW_tbl_If.Add(_if);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteIf(int ifid)
        {
            var _form = _DataContext.FLW_tbl_If.FirstOrDefault(n => n.IdIf == ifid);
            if (_form != null)
            {
                _DataContext.Remove(_form);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<If>> GetIf()
        {
            return _DataContext.FLW_tbl_If.ToListAsync();
        }

        public async Task<If?> GetIfById(int ifid)
        {
            var _if = await _DataContext.FLW_tbl_If.FirstOrDefaultAsync(n => n.IdIf == ifid);
            if (_if != null) { return _if; }
            return null;
        }

        public async Task<If?> UpdateIf(IfDto ifobject, int ifid)
        {
            var _if = await _DataContext.FLW_tbl_If.FirstOrDefaultAsync(n => n.IdIf == ifid);
            if (_if != null)
            {
                _if.IdVariable = ifobject.IdVariable;
                _if.ValueVariable = ifobject.ValueVariable;
                _if.Next_ID_FormChart = ifobject.Next_ID_FormChart;
                _DataContext.SaveChanges();
                return _if;
            }
            return null;
        }
    }
}
