namespace NotUseful.CSharp.Mvc.GenericController
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    /// <summary>
    /// dotnet core startup
    /// <seealso cref="https://docs.asp.net/en/latest/fundamentals/startup.html"/>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// add Logging,Mvc
        /// AddContext : add ObjectContext with test data
        /// AddRepository : add IRepository[T] for Controller
        /// AddGenericController : add GenericController into Mvc
        /// </summary>
        /// <param name="services">IServiceCollection can add service we need</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging()
                    .AddContext()
                    .AddRepository()
                    .AddMvc()
                    .AddGenericController();
        }

        /// <summary>
        /// use Mvc in application pipe
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
