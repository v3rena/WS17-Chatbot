using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Chatbot.Interfaces;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chatbot.DataAccessLayer;
using AutoMapper;
using System.Collections.Generic;
using System;
using Chatbot.Mapping;

namespace Chatbot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CreateMasterContainer();
        }

        private void CreateMasterContainer()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(WebApiApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // requires Autofac.WebApi2
            // required for System.Web.Http.ApiController
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            #region AutoMapper

            builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile)).As<Profile>();

            builder
                .Register(c => 
                    new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                )
                .AsSelf()
                .SingleInstance();
            
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            #endregion

            // or: builder.RegisterType<BusinessLayer>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<BusinessLayer.BusinessLayer>().As<IBusinessLayer>().InstancePerRequest();

            builder.RegisterType<PluginManager.PluginManager>().As<IPluginManager>().SingleInstance();

            builder.RegisterModule<MockDAL.Module>();
            //builder.RegisterModule((IModule) Activator.CreateInstance(Type.GetType("Chatbot.DataAccessLayer.MockDAL+Module")));

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // requires Autofac.WebApi2
            // required for System.Web.Http.ApiController
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
