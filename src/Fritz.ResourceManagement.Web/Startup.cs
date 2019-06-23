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
		  options.UseNpgsql(
			  Configuration.GetConnectionString("db")));
	  services.AddDefaultIdentity<MyUser>()
		  .AddEntityFrameworkStores<ApplicationDbContext>();

	  services.AddDbContext<Models.MyDbContext>(options =>
		  options.UseNpgsql(Configuration.GetConnectionString("db")));

	  services.AddSignalR();
	  services.AddServerSideBlazor();

	  services.AddScoped<ScheduleState>();
		services.AddScoped<ExpandedSchedule>();

	  //cheer ultramark 31/05/2019 100

	  services.AddHttpContextAccessor();
	  services.AddScoped<ClaimsPrincipal>(context => context.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User);


	  services.AddRazorPages()
		  .AddNewtonsoftJson();
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
	  if (env.IsDevelopment())
	  {
		app.UseDeveloperExceptionPage();
		app.UseDatabaseErrorPage();
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

	  app.UseAuthentication();
	  app.UseAuthorization();

	  app.UseEndpoints(endpoints =>
	  {
		endpoints.MapRazorPages();

		endpoints.MapBlazorHub();
		endpoints.MapFallbackToPage("/_Host");

	  });
	}
  }
}
