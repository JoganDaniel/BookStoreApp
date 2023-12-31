using BookStoreBussiness.Bussiness;
using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using BookStoreRepository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserBussiness, UserBussiness>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookBussiness, BookBussiness>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IWishlistBusiness, WishlistBusiness>();
            services.AddScoped<ICartBusiness, CartBusiness>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICustomerDetailsBusiness, CustomerDetailsBusiness>();
            services.AddScoped<ICustomerDetailsRepository, CustomerDetailsRepository>();
            services.AddScoped<IFeedbackBusiness, FeedbackBusiness>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderSummaryBusiness, OrderSummaryBusiness>();
            services.AddScoped<IOrderSummaryRepository, OrderSummaryRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookStoreApp",
                    Version = "v1",
                    Description = "BookStore App"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                             }
                         },
                          new string[]{}
}
                 });

            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            services.AddDistributedRedisCache(Options =>
            {
                Options.Configuration = "localhost:6379";
                Options.InstanceName = "BookStorecache";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           );

            app.UseAuthentication();
           

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreApp");
            });
        }
    }
}
