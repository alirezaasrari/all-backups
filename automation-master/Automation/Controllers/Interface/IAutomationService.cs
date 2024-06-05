using Automation.Model;
using Automation.Model.Dto;

namespace Automation.Controllers.Interface
{
    public interface IAutomationService
    {
        public Task AddVariable(AutomationModel variable);
        public Task<List<AutomationModel>> GetVariable();
        public Task<AutomationModel?> GetVariableById(int variableid);
        public Task<AutomationModel?> UpdateVariable(AutomationDto variable, int variableid);
        public Task<string?> DeleteVariable(int variableid);
    }
}
