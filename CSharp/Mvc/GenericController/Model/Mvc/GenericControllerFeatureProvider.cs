namespace NotUseful.CSharp.Mvc.GenericController
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;
    using System;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// implement IApplicationFeatureProvider[ControllerFeature]
    /// add all kind of open generic controller's close version to ControllerFeature
    /// and IApplicationFeatureProvider will give controller type we add to other servicecs
    /// </summary>
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private static IList<TypeInfo> controllers = new List<TypeInfo>();
        /// <summary>
        /// populate feature
        /// </summary>
        /// <param name="parts">AssemblyPart will in here</param>
        /// <param name="feature">save new close generic type in here</param>
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            if(!controllers.Any())                                                                              //make sure controller feature has controller
            {
                var types = (parts.First(x => x is AssemblyPart) as AssemblyPart).Types;                        // Find AssemblyPart for all type
                controllers = types                                                                             // cache controllers type
                    .Where(x => x.IsGenericTypeDefinition                                                       // get open generic type
                            && x.GenericTypeParameters.Length == 1                                              // have only one generic (if more then one you need,plz composite to one)
                            && typeof(Controller).IsAssignableFrom(x.AsType())                                  // base on Controller
                    ).SelectMany(x => x.GenericTypeParameters.First()                                           // get generic param
                        .GetTypeInfo().GetGenericParameterConstraints()                                         // get constraints
                        .SelectMany(y => types                                                                  // find constraint type from all type
                            .Where(z => y.GetTypeInfo().IsAssignableFrom(z))                                    // find all can assign to
                        ).Distinct()                                                                            // distinct found types
                        .Select(z => x.MakeGenericType(z.AsType()).GetTypeInfo()))                              // make open generic to close for all types matching constraint
                        .ToList();
            }
            controllers.Except(feature.Controllers).ToList().ForEach(x => feature.Controllers.Add(x));          // add controller type that feature dont have
        }
    }
}
