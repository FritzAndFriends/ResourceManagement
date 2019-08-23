using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Fritz.ResourceManagement.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Fritz.ResourceManagement.Scheduling;

namespace Fritz.ResourceManagement.Web
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
			});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMyDatabase(Configuration));
			services.AddDefaultIdentity<MyUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddDbContext<Models.MyDbContext>(options =>
				options.UseMyDatabase(Configuration));

			services.AddTransient<ScheduleManager>();

			// Cheer 100 ultramark 31/05/2019 
			// Cheer 400 cpayette 24/07/19 
			// Cheer 100 pharewings 25/07/19 

			services.AddMvc().AddNewtonsoftJson();
			services.AddRazorPages()
				.AddNewtonsoftJson();

			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
						new[] { "application/octet-stream" });
			});


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{

			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseBlazorDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseCookiePolicy();

			app.UseRouting();

			// TODO: Remove?
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseClientSideBlazorFiles<WebClient.Startup>();

			app.UseEndpoints(endpoints =>
			{
				//// TODO: Remove?
				endpoints.MapRazorPages();

				endpoints.MapDefaultControllerRoute();
				endpoints.MapFallbackToClientSideBlazor<WebClient.Startup>("index.html");

			});
		}
	}
}
