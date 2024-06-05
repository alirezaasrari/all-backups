using ETAPI.Models;
using ETAPI.Models.Dto;
namespace ETAPI.Interfaces.Interfaces
{
    public interface IFormChartService
    {
        public Task AddFormChart(FormChart formchart);
        public Task<List<FormChart>> GetFormChart();
        public Task<FormChart?> GetFormChartById(int formcahrtid);
        public Task<FormChart?> UpdateFormChart(FormChartDto formchart, int formcahrtid);
        public Task<string?> DeleteFormChart(int formchartid);
    }
}
