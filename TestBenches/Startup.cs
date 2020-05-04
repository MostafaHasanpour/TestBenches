using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestBenches.Controllers;

namespace TestBenches
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Person, PersonDto>().
                ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name)).
                ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Family)).
                ForMember(dest => dest.LastAddress, opt => opt.MapFrom(src => src.Addresses.LastOrDefault().Value)).
                ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.Family}")).
                ForMember(dest => dest.Age, opt => opt.MapFrom(src => (int)(DateTime.Now.Subtract(src.BirthDate ?? DateTime.Now).TotalDays / 365)));
        }
    }
}
