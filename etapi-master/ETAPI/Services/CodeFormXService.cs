using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces
{
    public class CodeFormXService : ICodeFormXService
    {
        private readonly DataContext _DataContext;
        public CodeFormXService(DataContext DataContext) 
        { 
         _DataContext = DataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddCodeFormX(CodeFormx codeformx)
        {
            CodeFormx _codeformx = new()
            {
                IdCodeFormX = codeformx.IdCodeFormX,
                CodeFormX = codeformx.CodeFormX,
                ID_FormUnit = codeformx.ID_FormUnit,
            };
             _DataContext.FLW_tbl_CodeFormX.Add(_codeformx);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteCodeFormX(int codeformxid)
        {
            var _codeformxid =await _DataContext.FLW_tbl_CodeFormX.FirstOrDefaultAsync(n => n.IdCodeFormX == codeformxid);
            if (_codeformxid != null)
            {
                _DataContext.Remove(_codeformxid);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public async Task<List<CodeFormx>> GetCodeFormX()
        {
            return await _DataContext.FLW_tbl_CodeFormX.ToListAsync();
        }

        public async Task<CodeFormx?> GetCodeFormXById(int codeformxid)
        {
            var _codeformx = await _DataContext.FLW_tbl_CodeFormX.FirstOrDefaultAsync(n => n.IdCodeFormX == codeformxid);
            if (_codeformx != null)
            {
                return _codeformx; 
            }
            return null;
        }

        public async Task<CodeFormx?> UpdateCodeFormX(CodeFormXDto codeformx, int codeformxid)
        {
            var _codeformx =await _DataContext.FLW_tbl_CodeFormX.FirstOrDefaultAsync(n => n.IdCodeFormX == codeformxid);
            if (_codeformx != null)
            {
                _codeformx.CodeFormX = _codeformx.CodeFormX;
                _codeformx.ID_FormUnit = codeformx.ID_FormUnit;

                await _DataContext.SaveChangesAsync();
                return _codeformx;
            }
            return null;
        }
    }
}
