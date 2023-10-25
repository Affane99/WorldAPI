using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Infrastucture;
using World.Application.Models;
using World.Infrastucture.Mail;

namespace World.Infrastucture
{
    public static class InfrastuctureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastucureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
