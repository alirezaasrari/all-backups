using TerminalWebApi.Utilities;
using TerminalWebApi;
using Microsoft.EntityFrameworkCore;
using TerminalWebApi.Service.Interfaces;
using TerminalWebApi.Model;

namespace SahmabZoneCalculator.Service
{
    public class TerminalService: ITerminalService
    {
        private readonly DataContext _Context;
        public TerminalService(DataContext Context)
        {
            _Context = Context;
        }
        public string Success = Textes.successfull;

        public async Task<List<TerminalData>> GetAll()
        {
            return await _Context.NewTerminal.OrderByDescending(l => l.Date).ToListAsync();
        }

        //public async Task<List<TerminalData>> GetById(int Id)
        //{
        //    return await _Context.NewTerminal.Where(a => a.DataLoggerSerialInDecimal == Id).ToListAsync();
        //}

        public async Task<List<TerminalError>> GetAllError()
        {
            return await _Context.NewTerminalError.OrderByDescending(l => l.Date).ToListAsync();
        }

        public async Task<List<TerminalIoError>> GetAllIoError()
        {
            return await _Context.NewTerminalIoError.OrderByDescending(l => l.Date).ToListAsync();
        }

        public async Task<List<TerminalSocketError>> GetAllSocketError()
        {
            return await _Context.NewTerminalSocketError.OrderByDescending(l => l.Date).ToListAsync();
        }

    }
}


