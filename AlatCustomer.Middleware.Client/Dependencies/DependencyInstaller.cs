using AlatCustomer.Middleware.Core;
using AlatCustomer.Middleware.Core.Fakes;
using AlatCustomer.Middleware.Core.Implementations;
using AlatCustomer.Middleware.Core.Models;
using AlatCustomer.Middleware.Core.Processors;
using AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor;
using AlatCustomer.Middleware.Core.Repository;
using AlatCustomer.Middleware.Core.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlatCustomer.Middleware.Client.Dependencies
{
    public static class DependencyInstaller
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Services 
            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMessageProvider, MessageProvider>();

            // DAOs
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            if (configuration.GetValue<bool>("SystemSettings:UseFake"))
            {
                //FakeProcessors
                services.AddScoped<IMessagePackProvider, FakeMessagePackProvider>();
                services.AddScoped<IOtpProcessor, FakeOtpProcessor>();
                services.AddScoped<IBankProcessor, FakeBankProcessor>();
            }
            else
            {
                //Processors
                services.AddScoped<IMessagePackProvider, FakeMessagePackProvider>();
                services.AddScoped<IOtpProcessor, FakeOtpProcessor>();
                //services.AddScoped<IBankProcessor, BankProcessor>();
                services.AddScoped<IBankProcessor, FakeBankProcessor>();
            }

            //Filters
            services.Configure<SystemSettings>(opt => configuration.GetSection("SystemSettings").Bind(opt));
            services.Configure<MessagePackSettings>(opt => configuration.GetSection("MessagePackSettings").Bind(opt));
            services.Configure<BankProcessorSettings>(opt => configuration.GetSection("BankProcessorSettings").Bind(opt));


            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        }
    }
}
