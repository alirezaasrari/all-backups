using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class FormUnitService : IFormUnitService
    {
        private readonly DataContext _DataContext;
        public FormUnitService(DataContext dataContext)
        {
            _DataContext = dataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddFormUnit(FormUnit formunit)
        {
            FormUnit _formunit = new()
            {
                ID_FormUnit = formunit.ID_FormUnit,
                ID_Unit = formunit.ID_Unit,
                TittleForm = formunit.TittleForm,
                ID_Form = formunit.ID_Form,
            };
            _DataContext.FLW_tbl_FormUnit.Add(_formunit);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteFormUnit(int formunitid)
        {
            var _formunit = _DataContext.FLW_tbl_FormUnit.FirstOrDefault(n => n.ID_FormUnit == formunitid);
            if (_formunit != null)
            {
                _DataContext.Remove(_formunit);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<FormUnit>> GetFormUnit()
        {
            return _DataContext.FLW_tbl_FormUnit.Include(formunit => formunit.Form).ToListAsync();
        }

        public async Task<FormUnit?> GetFormUnitById(int formunitid)
        {
            var _formunit = await _DataContext.FLW_tbl_FormUnit.FirstOrDefaultAsync(n => n.ID_FormUnit == formunitid);
            if (_formunit != null) { return _formunit; }
            return null;
        }

        public async Task<FormUnit?> UpdateFormUnit(FormUnitDto formunit, int formunintid)
        {
            var _formunit = await _DataContext.FLW_tbl_FormUnit.FirstOrDefaultAsync(n => n.ID_Form == formunintid);
            if (_formunit != null)
            {
                _formunit.TittleForm = formunit.TittleForm;
                _formunit.ID_Form = formunit.ID_Form;
                _formunit.ID_Unit = formunit.ID_Form;

                await _DataContext.SaveChangesAsync();
                return _formunit;
            }
            return null;
        }
    }
}
