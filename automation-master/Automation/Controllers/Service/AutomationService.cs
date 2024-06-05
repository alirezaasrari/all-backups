using Automation.Controllers.Interface;
using Automation.Model;
using Automation.Model.Context;
using Automation.Model.Dto;
using Automation.Utility;
using Microsoft.EntityFrameworkCore;

namespace Automation.Controllers.Service
{
    public class AutomationService : IAutomationService
    {
        private readonly DataContext _DataContext;

        public AutomationService(DataContext dataContext)
        {
            _DataContext = dataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddVariable(AutomationModel variable)
        {
            AutomationModel _variable = new()
            {
                IdVariable = variable.IdVariable,
                ValueVariable = variable.ValueVariable,
                ID_FormChart = variable.ID_FormChart,
                NameVariable = variable.NameVariable,
            };
            _DataContext.TabelName.Add(_variable);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteVariable(int variableid)
        {
            var _variable = await _DataContext.TabelName.FirstOrDefaultAsync(n => n.IdVariable == variableid);
            if (_variable != null)
            {
                _DataContext.Remove(_variable);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<AutomationModel>> GetVariable()
        {
            return _DataContext.TabelName.ToListAsync();
        }

        public async Task<AutomationModel?> GetVariableById(int variableid)
        {
            var _variable = await _DataContext.TabelName.FirstOrDefaultAsync(n => n.IdVariable == variableid);
            if (_variable != null) { return _variable; }
            return null;
        }

        public async Task<AutomationModel?> UpdateVariable(AutomationDto variable, int variableid)
        {
            var _variable = await _DataContext.TabelName.FirstOrDefaultAsync(n => n.IdVariable == variableid);
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
