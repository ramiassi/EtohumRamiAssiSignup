using EtohumRamiAssiSignup.Services;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(EtohumRamiAssiSignup.Startup))]
namespace EtohumRamiAssiSignup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Email queue service
            services.AddSingleton<IEmailQueueService,EmailQueueService>(c => new EmailQueueService());
        }
    }
}
