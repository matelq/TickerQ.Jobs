using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TickerQ.Jobs.Web;

public static class ServiceRegistry
{
    public static void AddWeb<TTickerDataContext>(this IServiceCollection services, TickerQHostType hostType)
        where TTickerDataContext : DbContext
    {
        services.AddTickerQHost<TTickerDataContext>(hostType);
    }

    public static void UseWeb(this IApplicationBuilder app, TickerQHostType hostType)
    {
        app.UseHostTickerQ(hostType);
    }

    public static void UseWeb(this IHost app, TickerQHostType hostType)
    {
        app.UseHostTickerQ(hostType);
    }
}
