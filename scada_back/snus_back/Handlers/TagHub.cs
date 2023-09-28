using Microsoft.AspNetCore.SignalR;
using scada_back.Models;

namespace scada_back.Handlers
{
    public class TagHub : Hub
    {
        public TagHub() { }

        public async Task SendRecord(TagRecord tagRecord)
        {
            await Clients.All.SendAsync("record", tagRecord);
        }
    }
}
