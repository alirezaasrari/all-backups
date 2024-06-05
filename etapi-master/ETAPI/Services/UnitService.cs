using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class UnitService : IUnitService
    {
        private readonly DataContext _DataContext;
        public UnitService(DataContext DataContext) 
        {
           _DataContext = DataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddUnit(Unit unit)
        {
            Unit _unit = new()
            {
                IdUnit = unit.IdUnit,
                NameUnit = unit.NameUnit,
                ActiveUnit = unit.ActiveUnit,
            };
            _DataContext.ET_tbl_Unit.Add(_unit);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteUnit(int unitid)
        {
            var _unit = _DataContext.ET_tbl_Unit.FirstOrDefault(n => n.IdUnit == unitid);
            if (_unit != null)
            {
                _DataContext.Remove(_unit);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public async Task<Unit?> GetById(int unitid)
        {
            var _unit = await _DataContext.ET_tbl_Unit.FirstOrDefaultAsync(n => n.IdUnit == unitid);
            if (_unit != null) { return _unit; }
            return null;
        }

        public Task<List<Unit>> GetUnit()
        {
            return _DataContext.ET_tbl_Unit.ToListAsync();
        }

        public async Task<Unit?> UpdateUnit(UnitDto unit, int unitid)
        {
            var _unit = await _DataContext.ET_tbl_Unit.FirstOrDefaultAsync(n => n.IdUnit == unitid);
            if (_unit != null)
            {
                _unit.NameUnit = unit.NameUnit;
                _unit.ActiveUnit = unit.ActiveUnit;
                await _DataContext.SaveChangesAsync();
                return _unit;
            }
            return null;
        }
    }
}
