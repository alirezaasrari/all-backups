using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface ITaeedService
    {
        public Task AddTaeed(Taeed taeed);
        public Task<List<Taeed>> GetTaeed();
        public Task<Taeed?> GetTaeedById(int taeedid);
        public Task<Taeed?> UpdateTaeed(TaeedDto taeed, int taeedid);
        public Task<string?> DeleteTaeed(int taeedid);
    }
}
