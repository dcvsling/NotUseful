namespace NotUseful.CSharp.Mvc.GenericController
{
    using System.IO;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    /// <summary>
    /// Entry Point Class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Web Site Entry Point
        /// 可直接執行後則完成Web Site架設
        /// <seealso cref="http://localhost:5000/User/User"/>
        /// </summary>
        /// <param name="args">not use</param>
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseUrls("http://localhost:5000","https://localhost:5001")
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
