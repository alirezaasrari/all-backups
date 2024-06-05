using ETAPI.Models;
using ETAPI.Models.Dto;

namespace ETAPI.Interfaces.Interfaces
{
    public interface ICodeFormXService
    {
        public Task AddCodeFormX(CodeFormx codeformx);
        public Task<List<CodeFormx>> GetCodeFormX();
        public Task<CodeFormx?> GetCodeFormXById(int codeformxid);
        public Task<CodeFormx?> UpdateCodeFormX(CodeFormXDto codeformx, int codeformxid);
        public Task<string?> DeleteCodeFormX(int codeformxid);
    }
}
