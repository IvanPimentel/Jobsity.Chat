using Jobsity.Chat.Application.ViewModels.ChatRoom.SignalR;
using Microsoft.AspNetCore.Builder;

namespace Jobsity.Chat.WebApi.SignalR
{
    public static class ConfigureSignalR
    {
        public static IApplicationBuilder ConfigureEndPoinsSignalR(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatRoomMessageHub>("/ChatRoomMessageNotify");
                endpoints.MapHub<ChatRoomHub>("/ChatRoomNotify");
            });
            return app;
        }
    }
}
