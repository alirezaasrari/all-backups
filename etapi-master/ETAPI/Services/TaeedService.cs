using ETAPI.Context;
using ETAPI.Models;
using ETAPI.Models.Dto;
using ETAPI.Interfaces.Interfaces;
using ETAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ETAPI.Interfaces
{
    public class TaeedService : ITaeedService
    {
        private readonly DataContext _DataContext;
        public TaeedService(DataContext DataContext) 
        {
          _DataContext = DataContext;
        }
        public string Success = Textes.successfull;
        public async Task AddTaeed(Taeed taeed)
        {
            Taeed _taeed = new()
            {
                IdTaeed = taeed.IdTaeed,
                IdCodeFormX = taeed.IdCodeFormX,
                CheckTaeed = taeed.CheckTaeed,
                DateInsert = taeed.DateInsert,
                DateRead = taeed.DateRead,
                DateTaeed = taeed.DateTaeed,
                DescPrev = taeed.DescPrev,
                DescTaeed = taeed.DescTaeed,
                HasOpen = taeed.HasOpen,
                IdFormChart = taeed.IdFormChart,
                IdPersonelChart = taeed.IdPersonelChart,
                IdUserInsert = taeed.IdUserInsert,
                IdUserTaeed = taeed.IdUserTaeed,
                IsActive = taeed.IsActive,
                IsSendSms = taeed.IsSendSms,
                RoneveshtType = taeed.RoneveshtType
            };
            _DataContext.FLW_tbl_Taeed.Add(_taeed);
            await _DataContext.SaveChangesAsync();
        }

        public async Task<string?> DeleteTaeed(int taeedid)
        {
            var _taeed = _DataContext.FLW_tbl_Taeed.FirstOrDefault(n => n.IdTaeed == taeedid);
            if (_taeed != null)
            {
                _DataContext.Remove(_taeed);
                await _DataContext.SaveChangesAsync();
                return Success;
            }
            return null;
        }

        public Task<List<Taeed>> GetTaeed()
        {
            return _DataContext.FLW_tbl_Taeed.Where(a => a.CheckTaeed == 3).ToListAsync();
        }

        public async Task<Taeed?> GetTaeedById(int taeedid)
        {
            var _taeed = await _DataContext.FLW_tbl_Taeed.FirstOrDefaultAsync(n => n.IdTaeed == taeedid);
            if (_taeed != null) { return _taeed; }
            return null;
        }

        public async Task<Taeed?> UpdateTaeed(TaeedDto taeed, int taeedid)
        {
            var _taeed = await _DataContext.FLW_tbl_Taeed.FirstOrDefaultAsync(n => n.IdTaeed == taeedid);
            if (_taeed != null)
            {
                _taeed.IdCodeFormX = taeed.IdCodeFormX;
                _taeed.CheckTaeed = taeed.CheckTaeed;
                _taeed.DateInsert = taeed.DateInsert;
                _taeed.DateRead = taeed.DateRead;
                _taeed.DateTaeed = taeed.DateTaeed;
                _taeed.DescPrev = taeed.DescPrev;
                _taeed.DescTaeed = taeed.DescTaeed;
                _taeed.HasOpen = taeed.HasOpen;
                _taeed.IdFormChart = taeed.IdFormChart;
                _taeed.IdPersonelChart = taeed.IdPersonelChart;
                _taeed.IdUserInsert = taeed.IdUserInsert;
                _taeed.IdUserTaeed = taeed.IdUserTaeed;
                _taeed.IsActive = taeed.IsActive;
                _taeed.IsSendSms = taeed.IsSendSms;
                _taeed.RoneveshtType = taeed.RoneveshtType;
                await _DataContext.SaveChangesAsync();
                return _taeed;
            }
            return null;
        }
    }
}
