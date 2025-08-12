using TickerQ.Jobs.Application;
using TickerQ.Jobs.Data;
using TickerQ.Jobs.Web;

namespace TickerQ.Jobs.Dashboard;

public sealed class Startup
{
    private const TickerQHostType HostType = TickerQHostType.Dashboard;

    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddData(configuration);
        services.AddApplication();
        services.AddWeb<TickerDataContext>(HostType);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStatusCodePages();

        app.UseWeb(HostType);
    }
}
