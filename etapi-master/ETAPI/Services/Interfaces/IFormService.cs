using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IFormService
    {
        public Task AddForm(Form form);
        public Task<List<Form>> GetForms();
        public Task<Form?> GetFormById(int formid);
        public Task<Form?> UpdateForm(int formid, FormDto form);
        public Task<string?> DeleteForm(int formid);
    }
}
