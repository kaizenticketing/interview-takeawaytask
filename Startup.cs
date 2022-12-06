using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Apps.Channels.Areas.Account.Models.Home.Mappers;
using Ticketing.Apps.Channels.Infrastructure.Accessors;
using Ticketing.Services.Customers;

#nullable enable

namespace Ticketing.Apps.Channels
{
	public class Startup
	{
		private readonly IConfiguration configuration;
		private readonly IHostEnvironment hostEnvironment;

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment)
		{
			this.configuration = configuration;
			this.hostEnvironment = hostEnvironment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// configure IHttpContextAccessor to get hold of HttpContext
			services.AddHttpContextAccessor();
			services.AddResponseCompression();

			// configure routing
			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});

			// configure mvc w/global filters
			services
				.AddMvc(/*setupAction ?? ((options) => { })*/)
				.AddControllersAsServices() // resolve controllers using autofac
				.AddMvcOptions(options =>
				{
				// allow optional FromBody ..=null parameters
				options.AllowEmptyInputInBodyModelBinding = true;
				})
				.AddJsonOptions(options => // configure system.text.json with our 'defaults'
			{
				})
				.AddCookieTempDataProvider(options => // use cookies for temp data
			{
					options.Cookie.Name = "mvcTempData"; // default = .AspNetCore.Mvc.CookieTempDataProvider
			});
		}

		public void ConfigureContainer(ContainerBuilder b)
		{
			b.RegisterType<ChannelContextAccessor>().AsImplementedInterfaces().SingleInstance();
			b.RegisterType<PurchasingCustomerContextAccessor>().AsImplementedInterfaces().SingleInstance();
			b.RegisterType<AccountMapper>().AsSelf().AsImplementedInterfaces().SingleInstance();
			b.RegisterType<AccountService>().AsSelf().AsImplementedInterfaces().SingleInstance();
		}

		public void Configure(
			IApplicationBuilder app,
			IHostApplicationLifetime applicationLifetime,
			ILogger<Startup> log)
		{
			// add response compression middleware (doesn't work under dotnet watch)
			if (Environment.GetEnvironmentVariable("DOTNET_WATCH") == null)
				app.UseResponseCompression();

			// enable end-point routing
			app.UseRouting();

			// enable mvc & signalr middleware
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}");
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}


