using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Application.BaseTypes.Queries;
using FastDrink.Application.Common.Models;
using FastDrink.Application.Common.Settings;
using FastDrink.Application.Products.Commands;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FastDrink.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        var cloudinarySettings = new CloudinarySettings();

        configuration.Bind(nameof(jwtSettings), jwtSettings);
        configuration.Bind(nameof(cloudinarySettings), cloudinarySettings);
        services.AddSingleton(jwtSettings);
        services.AddSingleton(cloudinarySettings);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddRegisterGenericType<Category>();
        services.AddRegisterGenericType<Container>();
        services.AddRegisterGenericType<Brand>();

        services.AddRegisterProductDetails<AlcoholDetails>();
        services.AddRegisterProductDetails<BeerDetails>();
        services.AddRegisterProductDetails<EnergyDrinkDetails>();
        services.AddRegisterProductDetails<FlavorDetails>();
        services.AddRegisterProductDetails<SodaDetails>();
        services.AddRegisterProductDetails<WaterDetails>();
        services.AddRegisterProductDetails<WineDetails>();

        return services;
    }

    private static void AddRegisterGenericType<T>(this IServiceCollection services) where T : BaseType
    {
        services.AddScoped(typeof(IRequestHandler<CreateBaseTypeCommand<T>, Result>), typeof(CreateBaseTypeCommandHandler<T>));
        services.AddScoped(typeof(IRequestHandler<UpdateBaseTypeCommand<T>, Result>), typeof(UpdateBaseTypeCommandHandler<T>));
        services.AddScoped(typeof(IRequestHandler<DeleteBaseTypeCommand<T>, Result>), typeof(DeleteBaseTypeCommandHanlder<T>));
        services.AddScoped(typeof(IRequestHandler<GetAllBaseTypeQuery<T>, IList<BaseType>>), typeof(GetAllBaseTypeQueryHandler<T>));
        services.AddScoped(typeof(IRequestHandler<GetByIdBaseTypeQuery<T>, T?>), typeof(GetByIdBaseTypeQueryHandler<T>));
    }

    private static void AddRegisterProductDetails<T>(this IServiceCollection services) where T : BaseDetails
    {
        services.AddScoped(typeof(IRequestHandler<CreateDetailsCommand<T>, Result>), typeof(CreateDetailsCommandHandler<T>));
        services.AddScoped(typeof(IRequestHandler<UpdateDetailsCommand<T>, Result>), typeof(UpdateDetailsCommandHandler<T>));
        services.AddScoped(typeof(IRequestHandler<DeleteDetailsCommand<T>, Result>), typeof(DeleteDetailsCommandHandler<T>));
    }
}
