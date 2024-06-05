using TerminalWebApi.Model;

namespace TerminalWebApi.Service.Interfaces
{
    public interface ITerminalService
    {
        public Task<List<TerminalData>> GetAll();
        //public Task<List<TerminalData>> GetById(int Id);
        public Task<List<TerminalError>> GetAllError();
        public Task<List<TerminalIoError>> GetAllIoError();
        public Task<List<TerminalSocketError>> GetAllSocketError();
    }
}
