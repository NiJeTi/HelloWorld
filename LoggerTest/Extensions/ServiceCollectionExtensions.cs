﻿using LoggerTest.Configurations;
using LoggerTest.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoggerTest.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNameHolder(this IServiceCollection services, IConfiguration configuration)
        {
            var nameHolderConfiguration = new NameHolderConfiguration();
            configuration.Bind("NameHolder", nameHolderConfiguration);

            var nameHolder = new NameHolder(nameHolderConfiguration.DefaultName);

            return services.AddSingleton(nameHolder);
        }
    }
}