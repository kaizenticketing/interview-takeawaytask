using System.Net;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

#nullable enable

namespace Kaizen.AspNetCore;

public static class CommonHostBuilderExtensions
{
	public static IHostBuilder ConfigureCommonWebHost<TStartup>(
		this IHostBuilder hostBuilder,
		string[] args,
		int defaultHttpPort,
		string? appName = null,
		TimeSpan? shutdownTimeout = null) where TStartup : class
	{
		return hostBuilder
			.ConfigureCommonHost(args, appName, shutdownTimeout) // builds on top of the base
			.ConfigureWebHost(builder =>
			{
				builder
					.UseKestrel((context, options) =>
					{
						// configure the ports kestrel will run out when in DEVELOPMENT
						// .. as soon as ASPNETCORE_URLS/ASPNETCORE_HTTPS_PORT is set, this is ignored
						ConfigureKestrelListeningPorts(
							options,
							defaultHttpPort
						);
					})
					.PreferHostingUrls(
						// allow environment variables to overload the 'listens' above
						preferHostingUrls: true
					)
					.UseStartup<TStartup>();

				//

				// NOTE: both this AND the setting above are required to enforce this
				if (shutdownTimeout != null)
				{
					builder.UseShutdownTimeout(shutdownTimeout.Value);
				}
			}
		);

		//

		static void ConfigureKestrelListeningPorts(
			KestrelServerOptions kestrelOptions,
			int? httpPort)
		{
			// http 
			if (httpPort != null)
			{
				kestrelOptions.Listen(
					IPAddress.Any, // 0.0.0.0 (any) so that we can connect via WSL2, 
					httpPort.Value
				);
			}
		}
	}

	public static IHostBuilder ConfigureCommonHost(
		this IHostBuilder hostBuilder,
		string[]? args,
		string? appName = null,
		TimeSpan? shutdownTimeout = null)
	{
		return hostBuilder
			.ConfigureHostConfiguration((configBuilder) =>
			{
				// make sure even 'generic' hosts load environment variables from ASPNETCORE_...
				configBuilder.AddEnvironmentVariables(prefix: "DOTNET_");
				configBuilder.AddEnvironmentVariables(prefix: "ASPNETCORE_");

				if (args != null)
					configBuilder.AddCommandLine(args);
			})
			.ConfigureAppConfiguration((hostingContext, configBuilder) =>
			{
				var hostEnvironment = hostingContext.HostingEnvironment;

				configBuilder.AddEnvironmentVariables();

				if (args != null)
					configBuilder.AddCommandLine(args);
			})
			// .ConfigureSerilogLogging(
			// 	appName, fileLoggingMode, consoleLoggingMode, configureAdditionalSinks
			// )
			.ConfigureHostOptions(hostOptions =>
			{
				// if a shutdown timeout is specified we *must* set it in here
				// NOTE: if exceeded - this will not actually immediately shut down the process if a IHostedService is blocking - it will
				// .. 'just' trigger the cancellation token - the service still has to listen for it
				if (shutdownTimeout != null)
					hostOptions.ShutdownTimeout = shutdownTimeout.Value;
			})
			.UseServiceProviderFactory(new AutofacServiceProviderFactory());
	}
}
