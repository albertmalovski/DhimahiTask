using System.Linq;
using API.Errors;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<ILanguageRepository, LanguageRepository>();
      services.AddScoped<ICountryRepository, CountryRepository>();
      services.AddScoped<ICurrencyRepository, CurrencyRepository>();
      services.AddScoped<IContinentRepository, ContinentRepository>();
      services.AddScoped<ILanguageService, LanguageService>();
      services.AddScoped<IContinentService, ContinentService>();
      services.AddScoped<ICurrencyService, CurrencyService>();
      services.AddScoped<ICountryService, CountryService>();
      //services.AddScoped(typeof(IRepositoryBase<>), (typeof(GenericRepository<>)));
      

            services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = actionContext =>
              {
                var errors = actionContext.ModelState
                          .Where(e => e.Value.Errors.Count > 0)
                          .SelectMany(x => x.Value.Errors)
                          .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                  Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
              };
      });

      return services;
    }
  }
}