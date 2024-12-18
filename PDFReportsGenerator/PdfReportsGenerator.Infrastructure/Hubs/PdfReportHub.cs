using Microsoft.AspNetCore.SignalR;
using PdfReportsGenerator.Infrastructure.Hubs.Interfaces;
using Serilog;

namespace PdfReportsGenerator.Infrastructure.Hubs;

public class PdfReportHub : Hub<IPdfReportHub>
{
    public override Task OnConnectedAsync()
    {
        Log.Logger.Information($"Connected to PDF Reports Generator. User Id: {Context.UserIdentifier}");
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Log.Logger.Information($"Disconnected from PDF Reports Generator. User Id: {Context.UserIdentifier}");
        
        return base.OnDisconnectedAsync(exception);
    }
}