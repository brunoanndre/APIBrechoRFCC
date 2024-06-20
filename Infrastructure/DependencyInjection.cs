using FluentValidation;
using Microsoft.EntityFrameworkCore;
using APIBrechoRFCC.Application.Validators;
using APIBrechoRFCC.Application.Mappings;
using APIBrechoRFCC.Infrastructure.Context;
using FluentValidation.AspNetCore;
using BrechoRFCC.Infrastructure.Repository;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Infrastructure.Interface;
using APIBrechoRFCC.Infrastructure.Services;
using APIBrechoRFCC.Infrastructure.Repository;

namespace APIBrechoRFCC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            //Validator
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
            services.AddFluentValidationAutoValidation();
            //Database
            services.AddDbContext<ECommerceDbContext>
                (opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            //Category
            services.AddScoped<ICRUDRepository<Category>, CategoryRepository>();
            //Product
            services.AddScoped(typeof(ProductRepository));
            //ProductVariant
            services.AddScoped<ICRUDRepository<ProductVariant>, ProductVariantRepository>();
            //ProductOption
            services.AddScoped<ICRUDRepository<ProductOption>, ProductOptionRepository>();
            //HomeBanner e HomeSection
            services.AddScoped(typeof(HomeRepository));
            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //Firebase
            services.AddScoped(typeof(FirebaseStorageService));
            
            return services;
        }

    }
}
