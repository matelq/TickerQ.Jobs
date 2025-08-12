using TickerQ.Jobs.Application;
using TickerQ.Jobs.Data;
using TickerQ.Jobs.Web;

namespace TickerQ.Jobs.Node;

public class Program
{
    public const TickerQHostType HostType = TickerQHostType.Node;

    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddData(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddWeb<TickerDataContext>(HostType);

        var host = builder.Build();

        var scope = host.Services.CreateScope();
        await using var dataContext = scope.ServiceProvider.GetRequiredService<TickerDataContext>();
        await dataContext.MigrateAsync();

        host.UseWeb(HostType);

        host.Run();
    }
}
