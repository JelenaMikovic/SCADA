using Microsoft.AspNetCore.SignalR;
using scada_back.Models;

namespace scada_back.HandlersHandlers
{
    public class AlarmHub : Hub
    {
        public AlarmHub() { }

        public async Task SendAlarm(Alarm alarm)
        {
            await Clients.All.SendAsync("alarm", alarm);
        }
    }
}
