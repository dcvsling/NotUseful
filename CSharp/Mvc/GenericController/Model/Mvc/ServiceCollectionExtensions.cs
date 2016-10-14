namespace NotUseful.CSharp.Mvc.GenericController
{
    using Microsoft.Extensions.DependencyInjection;
    /// <summary>
    /// ObjectContext and Repository helper
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension Method with IMvcBuilder for Generic Controller
        /// i add a feature provider to MvcBuilder.FeatureProviders for add all kind of close generic controller from open kind
        /// and add a controller model convention to MvcOptions.Conventions for set RouteAttribute.Template what we want
        /// <param name="builder">add mvc setting we need</param>
        /// <returns>return IMvcBuilder for Fluent api</returns>
        /// </summary>
        public static IMvcBuilder AddGenericController(this IMvcBuilder builder)
        {
            builder.ConfigureApplicationPartManager(x => x.FeatureProviders.Add(new GenericControllerFeatureProvider()))
                   .AddMvcOptions(x => x.Conventions.Add(new GenericControllerModelConvention()));

            return builder;
        }
    }
}

