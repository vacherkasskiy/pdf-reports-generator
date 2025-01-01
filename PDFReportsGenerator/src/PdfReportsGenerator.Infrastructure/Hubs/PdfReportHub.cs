using Microsoft.AspNetCore.SignalR;
using PdfReportsGenerator.Infrastructure.Hubs.Interfaces;
using Serilog;

namespace PdfReportsGenerator.Infrastructure.Hubs;

internal class PdfReportHub : Hub<IPdfReportHub>
{
    public override Task OnConnectedAsync()
    {
        Log.Logger.Information($"Connected to PDF Reports Generator. Connection Id: {Context.ConnectionId}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Log.Logger.Information($"Disconnected from PDF Reports Generator. Connection Id: {Context.ConnectionId}");

        return base.OnDisconnectedAsync(exception);
    }
}