using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IIfService
    {
        public Task AddIf(If ifobject);
        public Task<List<If>> GetIf();
        public Task<If?> GetIfById(int ifid);
        public Task<If?> UpdateIf(IfDto ifobject, int ifid);
        public Task<string?> DeleteIf(int ifid);
    }
}
