using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class FormService : IFormService
    {
        private readonly DataContext _Context;
        public FormService(DataContext Context)
        {
            _Context = Context;
        }
        public string Success = Textes.successfull;

        public async Task AddForm(Form form)
        {
            Form _form = new()
            {
                ID_Form = form.ID_Form,
                FormName = form.FormName,
                Url = form.Url,
            };
            _Context.FLW_tbl_Forms.Add(_form);
            await _Context.SaveChangesAsync();
        }

        public async Task<string?> DeleteForm(int formid)
        {
            var _form = _Context.FLW_tbl_Forms.FirstOrDefault(n => n.ID_Form == formid);
            if (_form != null)
            {
                _Context.Remove(_form);
                await _Context.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public async Task<List<Form>> GetForms()
        {
            return await _Context.FLW_tbl_Forms.Include(a => a.FormUnits).ToListAsync();
        }

        public async Task<Form?> GetFormById(int formid)
        {
            var _form = await _Context.FLW_tbl_Forms.FirstOrDefaultAsync(n => n.ID_Form == formid);
            if (_form != null) {  return _form; }
            return null;
        }

        public async Task<Form?> UpdateForm(int formid, FormDto form)
        {
            var _form = await _Context.FLW_tbl_Forms.FirstOrDefaultAsync(n => n.ID_Form == formid);
            if (_form != null)
            {
                _form.FormName = form.FormName;
                _form.Url = form.Url;

                await _Context.SaveChangesAsync();
                return _form;
            }
            return null;
        }
    }
}