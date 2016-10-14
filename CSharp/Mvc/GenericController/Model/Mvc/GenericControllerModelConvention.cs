
namespace NotUseful.CSharp.Mvc.GenericController
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    /// <summary>
    /// implement IControllerModelConvention 
    /// this service will setting each controller routing constraint we want
    /// so we add route value case [generic] to RouteValues
    /// then template will replace word each route value and to match route path
    /// </summary>
    public class GenericControllerModelConvention : IControllerModelConvention
    {
        /// <summary>
        /// do what i need for each ControllerModel
        /// </summary>
        /// <param name="controller">each controller model</param>
        public void Apply(ControllerModel controller)
        {
            if(!controller.ControllerType.IsGenericType)
                return;
            var controllerHead = controller.ControllerType.Name
                        .Split(new string[] { nameof(Controller) },
                            StringSplitOptions.RemoveEmptyEntries)
                        .First();
            var generic = controller.ControllerType
                        .GenericTypeArguments
                        .First().Name
                        .Replace(controllerHead,string.Empty);
            generic = generic.Any() ? generic : controllerHead;
            controller.RouteValues
                .Add(new KeyValuePair<string,string>("controller",controllerHead));
            controller.RouteValues
                .Add(new KeyValuePair<string,string>("generic",generic));
            //-------------------------------------------------------------------
            // if you really need a special case, 
            // you can edit directly like follow code 
            //
            // controller.Selectors
            //    .ToList()
            //    .ForEach(x => x.AttributeRouteModel.Template = 
            //          x.AttributeRouteModel.Template
            //           .Replace("[generic]",generic)
            //           .Replace("[controller]",controllerHead));
            //-------------------------------------------------------------------
        }
    }
}
