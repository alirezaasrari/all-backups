using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IFormUnitService
    {
        public Task AddFormUnit(FormUnit formunit);
        public Task<List<FormUnit>> GetFormUnit();
        public Task<FormUnit?> GetFormUnitById(int formunitid);
        public Task<FormUnit?> UpdateFormUnit(FormUnitDto formunit, int formunintid);
        public Task<string?> DeleteFormUnit(int formunitid);
    }
}
