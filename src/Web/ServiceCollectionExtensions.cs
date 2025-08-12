using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.DependencyInjection.Hosting;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ.Utilities.Enums;

namespace TickerQ.Jobs.Web;

public static class ServiceCollectionExtensions
{
    public static void AddTickerQHost<TTickerDataContext>(this IServiceCollection services, TickerQHostType hostType)
        where TTickerDataContext : DbContext
    {
        var instanceIdentifier = Guid.NewGuid().ToString();
        Console.WriteLine(instanceIdentifier);

        services.AddTickerQ(opt =>
        {
            if (hostType == TickerQHostType.Node)
            {
                opt.SetInstanceIdentifier(instanceIdentifier);
            }
            //

            // Define the DbContext to use for storing Tickers.
            opt.AddOperationalStore<TTickerDataContext>(efOpt =>
            {
                efOpt.UseModelCustomizerForMigrations(); // Applies custom model customization only during EF Core migrations
                efOpt.CancelMissedTickersOnApplicationRestart(); // Useful in distributed mode
            }); // Enables EF-backed storage

            if (hostType == TickerQHostType.Dashboard)
            {
                opt.AddDashboard(basePath: "/tickerq-dashboard");
                opt.AddDashboardBasicAuth();
            }
        });
    }

    public static void UseHostTickerQ(this IHost app, TickerQHostType hostType)
    {
        switch (hostType)
        {
            case TickerQHostType.Node:
                // Нода должна обрабатывать джобы
                app.UseTickerQ(TickerQStartMode.Immediate);
                break;

            case TickerQHostType.Dashboard:
                // Дэшборд не должен обрабатывать джобы вовсе
                app.UseTickerQ(TickerQStartMode.Manual);
                break;

            default:
                throw new NotImplementedException();
        }
    }

    public static void UseHostTickerQ(this IApplicationBuilder app, TickerQHostType hostType)
    {
        switch (hostType)
        {
            case TickerQHostType.Node:
                // Нода должна обрабатывать джобы
                app.UseTickerQ(TickerQStartMode.Immediate);
                break;

            case TickerQHostType.Dashboard:
                // Дэшборд не должен обрабатывать джобы вовсе
                app.UseTickerQ(TickerQStartMode.Manual);
                break;

            default:
                throw new NotImplementedException();
        }
    }
}
