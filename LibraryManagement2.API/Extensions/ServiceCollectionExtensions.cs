using AutoMapper;
using LibraryManagement2.AutoMapper.Profiles;
using LibraryManagement2.Business.Interfaces;

using LibraryManagement2.Business.Services;
using LibraryManagement2.Data.Repositories;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Integration.Auth;

namespace LibraryManagement2.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            
           services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

           
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(BookMappingProfile).Assembly);


            return services;
        }
    }
}
