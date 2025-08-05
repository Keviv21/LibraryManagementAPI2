using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement2.AutoMapper.Profiles;
using LibraryManagement2.Business.Interfaces;

using LibraryManagement2.Business.Services;
using LibraryManagement2.Data.Entities;
using LibraryManagement2.Data.Repositories;
using LibraryManagement2.Data.Repositories.Interfaces;
using LibraryManagement2.Integration.Auth;
using LibraryManagement2.Shared.Validators.MainData;
using Microsoft.AspNetCore.Identity;

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


            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddFluentValidationAutoValidation(); 
            services.AddFluentValidationClientsideAdapters();


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



            return services;
        }
    }
}
