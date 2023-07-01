using CompositeDictStatusToInterfaceDI.CalculateStatusRules;
using DictStatusToInterfaceDI.CalculateStatusRules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.CompositeDictionary;

namespace Templates
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.TryAddSingleton<CancelledTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<OnRouteToTerminalTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<InCityAreaWithoutCallTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<InUnloadedAreaTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<InWaitingAreaTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<LeavingWithCargoTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<UnloadedTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<LoadedTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<ArrivedTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<LeavingWaitingAreaTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<IdleInCityAreaTerminalTimesllotVehicleStatusRule>();
            services.TryAddSingleton<DeletedTerminalTimeslotVehicleStatusRule>();
            services.TryAddSingleton<RedirectedToAnotherTerminalOrReturnToLoadingTerminalTimeslotVehicleStatusRule>();

            services.AddSingleton<ICalculateTerminalTimeslotVehicleStatusService>(sp =>
            new CompositeCalculateTerminalTimeslotVehicleStatusService(
                new Dictionary<TerminalVehicleStatus, ICalculateTerminalTimeslotVehicleStatusService>()
            {
                            { TerminalVehicleStatus.Cancelled, sp.GetRequiredService<CancelledTerminalTimeslotVehicleStatusRule>()},
                            { TerminalVehicleStatus.OnRouteToUnloadingArea ,sp.GetRequiredService<OnRouteToTerminalTerminalTimeslotVehicleStatusRule>()},
                            { TerminalVehicleStatus.InCityAreaWithoutCall ,sp.GetRequiredService<InCityAreaWithoutCallTerminalTimeslotVehicleStatusRule>()},
                            { TerminalVehicleStatus.InUnloadingArea,sp.GetRequiredService<InUnloadedAreaTerminalTimeslotVehicleStatusRule>()  },
                            { TerminalVehicleStatus.InWaitingArea ,sp.GetRequiredService<InWaitingAreaTerminalTimeslotVehicleStatusRule>() },
                            { TerminalVehicleStatus.LeavingWithCargo,sp.GetRequiredService<LeavingWithCargoTerminalTimeslotVehicleStatusRule>() },
                            { TerminalVehicleStatus.Unloaded,sp.GetRequiredService<UnloadedTerminalTimeslotVehicleStatusRule>() },
                            { TerminalVehicleStatus.Loaded, sp.GetRequiredService<LoadedTerminalTimeslotVehicleStatusRule>()},
                            { TerminalVehicleStatus.Arriving, sp.GetRequiredService<ArrivedTerminalTimeslotVehicleStatusRule>()},
                            { TerminalVehicleStatus.LeavingWaitingArea, sp.GetRequiredService<LeavingWaitingAreaTerminalTimeslotVehicleStatusRule>() },
                            { TerminalVehicleStatus.IdleInCity, sp.GetRequiredService<IdleInCityAreaTerminalTimesllotVehicleStatusRule>() },
                            { TerminalVehicleStatus.Deleted, sp.GetRequiredService<DeletedTerminalTimeslotVehicleStatusRule>()},
                            {TerminalVehicleStatus.ReturnToLoading, sp.GetRequiredService<RedirectedToAnotherTerminalOrReturnToLoadingTerminalTimeslotVehicleStatusRule>() },
                            {TerminalVehicleStatus.VehicleRedirectedToAnotherTerminal, sp.GetRequiredService<RedirectedToAnotherTerminalOrReturnToLoadingTerminalTimeslotVehicleStatusRule>() }
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
