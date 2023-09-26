using scada_back.Handlers;
using scada_back.Services;

namespace snus_back
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScanService scanService)
        {

            app.UseRouting();
            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws/updateTag")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<TagHandler>();
                        await handler.HandleWebSocket(webSocket, "tag");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else if (context.Request.Path == "/ws/updateAlarm")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<AlarmHandler>();
                        await handler.HandleWebSocket(webSocket, "alarm");
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });


            RunOnApplicationStart();
            scanService.Run();
        }

        private void RunOnApplicationStart()
        {
        }
    }
}

}
