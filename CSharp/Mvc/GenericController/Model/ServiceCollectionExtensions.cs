namespace NotUseful.CSharp.Mvc.GenericController
{
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using Model.Entity;

    /// <summary>
    /// ObjectContext and Repository helper
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension Method with IServiceCollection for ObjectContext
        /// can easier to add ObjectContext with data into IServiceCollection
        /// </summary>
        /// <param name="services">add service we need</param>
        /// <returns>return IServiceCollection for Fluent api</returns>
        public static IServiceCollection AddContext(this IServiceCollection services)
            => services.AddSingleton<ObjectContext>()
                        .AddOptions()
                        .Configure<ObjectContext>(
                            x => x.Add(new User[]{
                                new User() {Id = 0, Name = "Kevin" },
                                new User() {Id = 1, Name = "Bill" },
                            }).Add(new Owner[]{
                                new Owner() {Id = 2, Name = "Peter", HasCount = 2},
                                new Owner() {Id = 3, Name = "David", HasCount = 5},
                            }).Add(new Admin[] {
                                new Admin() {Id = 4, Name = "Jina",Power = "Do Any thing I Want"}
                            })
                    );
        /// <summary>
        /// Exntension Method with IServiceCollection for IRepository[T]
        /// register Repository open generic type to IRepository open generic type
        /// this will fit all kind of generic type where generic parameter type is match 
        /// </summary>
        /// <param name="services">add service we need</param>
        /// <returns>return IServiceCollection for Fluent api</returns>
        public static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
    }
}

