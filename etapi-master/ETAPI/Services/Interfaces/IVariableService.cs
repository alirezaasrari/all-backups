using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IVariableService
    {
        public Task AddVariable(Variable variable);
        public Task<List<Variable>> GetVariable();
        public Task<Variable?> GetVariableById(int variableid);
        public Task<Variable?> UpdateVariable(VariableDto variable, int variableid);
        public Task<string?> DeleteVariable(int variableid);
    }
}
