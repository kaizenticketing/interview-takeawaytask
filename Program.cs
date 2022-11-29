#nullable enable

using Kaizen.AspNetCore;

namespace Ticketing.Apps.Channels;

public class Program
{
	public static int Main(string[] args)
	{
		try
		{
			// starts web hosting & blocks until shutdown
			var host = Host.CreateDefaultBuilder(args)
				.ConfigureCommonWebHost<Startup>(args, defaultHttpPort: 5000)
				.Build();

			// start the host
			host.Run();
			return 0;
		}
		catch (Exception ex)
		{
			// NOTE: just in case - it does happen
			Console.WriteLine(ex.ToString());
			return 1; // non-zero = failure
		}
	}
}
