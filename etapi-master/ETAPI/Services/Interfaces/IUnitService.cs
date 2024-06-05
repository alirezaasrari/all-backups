using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IUnitService
    {
        public Task AddUnit(Unit unit);
        public Task<List<Unit>> GetUnit();
        public Task<Unit?> GetById(int unitid);
        public Task<Unit?> UpdateUnit(UnitDto unit, int unitid);
        public Task<string?> DeleteUnit(int unitid);
    }
}
