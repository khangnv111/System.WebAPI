namespace System.WebAPI;

using System.EntityFramework.Commands.Auth;
using System.EntityFramework.Commands.Groups;
using System.EntityFramework.Commands.Permission;
using System.EntityFramework.Commands.Users;
using System.EntityFramework.Responsitories.BillingGiftCode;
using System.EntityFramework.Responsitories.Users;
using System.Models.Model.Admin;
using System.Models.ViewModel.User;
using System.Utils.Auth;
using System.WebAPI.Commands;
using System.WebAPI.Mappers;
using System.WebAPI.Repositories;
using System.WebAPI.Services;
using System.WebAPI.ViewModels;
using Boxed.Mapping;

/// <summary>
/// <see cref="IServiceCollection"/> extension methods add project services.
/// </summary>
/// <remarks>
/// AddSingleton - Only one instance is ever created and returned.
/// AddScoped - A new instance is created and returned for each request/response cycle.
/// AddTransient - A new instance is created and returned each time.
/// </remarks>
internal static class ProjectServiceCollectionExtensions
{
    public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
        services
            .AddSingleton<DeleteCarCommand>()
            .AddSingleton<GetCarCommand>()
            .AddSingleton<GetCarPageCommand>()
            .AddSingleton<PatchCarCommand>()
            .AddSingleton<PostCarCommand>()
            .AddSingleton<PutCarCommand>()
        // Users
        .AddScoped<UserGetListCommand>()
        .AddScoped<UserAddCommand>()
        .AddScoped<UserGetInfoCommand>()
        // Group
        .AddScoped<GroupGetListCommand>()
        .AddScoped<GroupUpdateCommand>()
        .AddScoped<GroupAddCommand>()
        .AddScoped<GroupConfigPermissionCommand>()
        // Permission
        .AddScoped<PermissionAddCommand>()
        .AddScoped<PermissionUpdateCommand>()
        .AddScoped<PermissionRemoveCommand>()
        .AddScoped<PermissionGetListCommand>()
        // Auth
        .AddScoped<CmsLoginCommand>();

    public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
        services
            .AddSingleton<IMapper<Models.Car, Car>, CarToCarMapper>()
            .AddSingleton<IMapper<Models.Car, SaveCar>, CarToSaveCarMapper>()
            .AddSingleton<IMapper<SaveCar, Models.Car>, CarToSaveCarMapper>()
            .AddSingleton<IMapper<SaveGroup, Group>, GroupMapper>()
            .AddAutoMapper(typeof(Startup));

    public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
        services
            .AddSingleton<ICarRepository, CarRepository>()
        .AddScoped<IUserReponsitory, UserReponsitory>()
        .AddScoped<IGroupReponsitory, GroupReponsitory>()
        .AddScoped<IPermissionReponsitory, PermissionReponsitory>()
        .AddScoped<IGiftCodeReponsitory, GiftCodeReponsitory>();

    public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
        services
            .AddSingleton<IClockService, ClockService>()
            .AddSingleton<JwtTokenServices>();
}
