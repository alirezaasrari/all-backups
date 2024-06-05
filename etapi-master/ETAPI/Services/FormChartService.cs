using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using ETAPI.Models.Dto;

namespace ETAPI.Interfaces
{
    public class FormChartService : IFormChartService
    {
        private readonly DataContext _DataContext;
        public FormChartService(DataContext dataContext)
        {  
            _DataContext = dataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddFormChart(FormChart formchart)
        {
            FormChart _formchart = new()
            {
                ID_FormChart = formchart.ID_FormChart,
                ID_FormUnit = formchart.ID_FormUnit,
                ID_PersonelChart = formchart.ID_PersonelChart,
                ID_FormChartNext = formchart.ID_FormChartNext,
                LevelTaeed = formchart.LevelTaeed,
                ID_Chart_Janeshin = formchart.ID_Chart_Janeshin,
                IsJaneshin = formchart.IsJaneshin,
                IsIf = formchart.IsIf,
                Ronevesht = formchart.Ronevesht,
                NameMarhale = formchart.NameMarhale,
                IdMarhale = formchart.IdMarhale,
                RowMarhale = formchart.RowMarhale,
                IsEnd = formchart.IsEnd,
                DynamicIdChart = formchart.DynamicIdChart,
            };
            _DataContext.FLW_tbl_FormChart.Add(_formchart);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteFormChart(int formchartid)
        {
            var _formchart = _DataContext.FLW_tbl_FormChart.FirstOrDefault(n => n.ID_FormChart == formchartid);
            if (_formchart != null)
            {
                _DataContext.Remove(_formchart);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<FormChart>> GetFormChart()
        {
            return _DataContext.FLW_tbl_FormChart.ToListAsync();
        }

        public async Task<FormChart?> GetFormChartById(int formcahrtid)
        {
            var _formchart = await _DataContext.FLW_tbl_FormChart.FirstOrDefaultAsync(n => n.ID_FormChart == formcahrtid);
            if (_formchart != null) { return _formchart; }
            return null;
        }

        public async Task<FormChart?> UpdateFormChart(FormChartDto formchart, int formcahrtid)
        {
            var _formchart = await _DataContext.FLW_tbl_FormChart.FirstOrDefaultAsync(n => n.ID_FormChart == formcahrtid);
            if (_formchart != null)
            {
                _formchart.ID_FormUnit = formchart.ID_FormUnit;
                _formchart.ID_PersonelChart = formchart.ID_PersonelChart;
                _formchart.ID_FormChartNext = formchart.ID_FormChartNext;
                _formchart.LevelTaeed = formchart.LevelTaeed;
                _formchart.ID_Chart_Janeshin = formchart.ID_Chart_Janeshin;
                _formchart.IsJaneshin = formchart.IsJaneshin;
                _formchart.IsIf = formchart.IsIf;
                _formchart.Ronevesht = formchart.Ronevesht;
                _formchart.NameMarhale = formchart.NameMarhale;
                _formchart.IdMarhale = formchart.IdMarhale;
                _formchart.RowMarhale = formchart.RowMarhale;
                _formchart.IsEnd = formchart.IsEnd;
                _formchart.DynamicIdChart = formchart.DynamicIdChart;

                await _DataContext.SaveChangesAsync();
                return _formchart;
            }
            return null;
        }
    }
}
