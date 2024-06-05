using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class VariableService : IVariableService
    {
        private readonly DataContext _DataContext;

        public VariableService(DataContext dataContext)
        {
            _DataContext = dataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddVariable(Variable variable)
        {
            Variable _variable = new()
            {
                IdVariable = variable.IdVariable,
                ValueVariable = variable.ValueVariable,
                ID_FormChart = variable.ID_FormChart,
                NameVariable = variable.NameVariable,
            };
            _DataContext.FLW_tbl_Variable.Add(_variable);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteVariable(int variableid)
        {
            var _variable = await _DataContext.FLW_tbl_Variable.FirstOrDefaultAsync(n => n.IdVariable == variableid);
            if (_variable != null)
            {
                _DataContext.Remove(_variable);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<Variable>> GetVariable()
        {
            return _DataContext.FLW_tbl_Variable.ToListAsync();
        }

        public async Task<Variable?> GetVariableById(int variableid)
        {
            var _variable = await _DataContext.FLW_tbl_Variable.FirstOrDefaultAsync(n => n.IdVariable == variableid);
            if (_variable != null) { return _variable; }
            return null;
        }

        public async Task<Variable?> UpdateVariable(VariableDto variable, int variableid)
        {
            var _variable = await _DataContext.FLW_tbl_Variable.FirstOrDefaultAsync(n => n.IdVariable == variableid);
            if (_variable != null)
            {
                _variable.ValueVariable = variable.ValueVariable;
                _variable.ID_FormChart = variable.ID_FormChart;
                _variable.NameVariable = variable.NameVariable;

                await _DataContext.SaveChangesAsync();
                return _variable;
            }
            return null;
        }
    }
}
