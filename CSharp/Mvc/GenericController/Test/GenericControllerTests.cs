namespace NotUseful.CSharp.Mvc.GenericController
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Model.Entity;
    using Xunit;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    /// <summary>
    /// generic controller test scenario ref by <seealso cref="Microsoft.AspNetCore.Mvc.Tests"/>
    /// </summary>
    public class GenericControllerTests
    {
        /// <summary>
        /// cache service provider with startup and build
        /// </summary>
        private static IServiceProvider services;
        /// <summary>
        /// use startup to build service provider 
        /// </summary>
        static GenericControllerTests()
        {
            var collection = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(collection);
            services = collection.BuildServiceProvider();
        }
        /// <summary>
        /// this test to prove GenericControllerFeatureProvider will add 
        /// all kind of open generic controller's close version to ControllerFeaturn
        /// </summary>
        [Fact]
        public void ControllerFeature_should_have_all_kind_of_close_generic_type_controller()
        {
            var parts = new ApplicationPart[] { new AssemblyPart(typeof(UserController<>).GetTypeInfo().Assembly) };
            var feature = new ControllerFeature();
            new GenericControllerFeatureProvider().PopulateFeature(parts,feature);
            Assert.Equal(3,feature.Controllers.Count);
            var controllers = feature.Controllers;
            var actualSummary = controllers.Contains(typeof(UserController<User>).GetTypeInfo())
                                && controllers.Contains(typeof(UserController<Owner>).GetTypeInfo())
                                && controllers.Contains(typeof(UserController<Admin>).GetTypeInfo());
            Assert.True(actualSummary);
        }

        /// <summary>
        /// this test to prove each ControllerModel will has current RouteAttribute.Template 
        /// with all controller we add from GenericControllerFeatureProvider
        /// and all kind of open generic controller's close version is what GenericControllerFeatureProvider add
        /// </summary>
        /// <param name="model">each controller model we add</param>
        [Theory, MemberData("GetControllerModels")]
        public void ControllerModel_should_has_current_routing_template(ControllerModel model)
        {
            var type = model.ControllerType;
            var template = type.GetCustomAttribute<RouteAttribute>().Template;
            var genericName = type.GenericTypeArguments.First().Name;
            var controllerName = type.Name.Split(new string[] { "Controller" },StringSplitOptions.RemoveEmptyEntries).First();
            var actual = model.Selectors.First().AttributeRouteModel.Template;
            var expect = template.Replace("[controller]",controllerName).Replace("[generic]",genericName);
            Assert.Equal(expect,actual);
        }

        /// <summary>
        /// create close generic we expect then used by MemberData
        /// </summary>
        /// <returns>type of MemberData needs</returns>
        public static IEnumerable<object[]> GetControllerModels()
        {
            var option = new MvcOptions();
            var app = new ApplicationModel();
            app.Controllers.Add(new ControllerModel(typeof(UserController<User>).GetTypeInfo(),new List<Object>()));
            app.Controllers.Add(new ControllerModel(typeof(UserController<Owner>).GetTypeInfo(),new List<Object>()));
            app.Controllers.Add(new ControllerModel(typeof(UserController<Admin>).GetTypeInfo(),new List<Object>()));
            app.Controllers.ToList().ForEach(x => x.Selectors.Add(new SelectorModel() { AttributeRouteModel = new AttributeRouteModel() { Template = "[controller]/[generic]" }}));
            option.Conventions.Add(new GenericControllerModelConvention());

            option.Conventions[0].Apply(app);

            foreach(var controller in app.Controllers)
            {
                yield return new object[] { controller };
            }
        }
    }
}
