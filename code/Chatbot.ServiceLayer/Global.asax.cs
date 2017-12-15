using Autofac;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Chatbot.Mapping;
using Chatbot.DataAccessLayer;
using log4net;
using log4net.Config;
using Chatbot.DataAccessLayer.Context;
using Chatbot.BusinessLayer.Interfaces;
using Chatbot.Common.Interfaces;
using Chatbot.DataAccessLayer.Interfaces;
using Chatbot.Common.PluginManager;

namespace Chatbot
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            log.Info("Starting Application");
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
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<ServiceBusinessMappingProfile>();
                        cfg.AddProfile<BusinessDataAccessMappingProfile>();
                    })
                )
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            #endregion

            // or: builder.RegisterType<BusinessLayer>().AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<BusinessLayer.MessagingLogic>().As<IMessagingLogic>().SingleInstance();
            builder.RegisterType<BusinessLayer.SessionLogic>().As<ISessionLogic>().SingleInstance();
            builder.RegisterType<BusinessLayer.PluginConfigurationLogic>().As<IPluginConfigurationLogic>().SingleInstance();
            builder.RegisterType<BusinessLayer.SpeechAPITokenLogic>().As<ISpeechAPITokenLogic>().SingleInstance();

            builder.RegisterType<DataAccessLayer.Repositories.MessageRepository>().As<IRepository<DataAccessLayer.Entities.Message>>().SingleInstance();
            builder.RegisterType<DataAccessLayer.Repositories.SessionKeyRepository>().As<IRepository<DataAccessLayer.Entities.SessionKey>>().SingleInstance();
            builder.RegisterType<DataAccessLayer.Repositories.PluginConfigurationRepository>().As<IRepository<DataAccessLayer.Entities.PluginConfiguration>>().SingleInstance();

            builder.RegisterType<PluginManager>().As<IPluginManager>().SingleInstance();

            builder.RegisterType<ChatbotContext>().SingleInstance();

            //builder.RegisterType<DataAccessLayer.DataAccessLayer>().As<IDataAccessLayer>().SingleInstance();

            //builder.RegisterModule<DataAccessLayer.DataAccessLayer.Module>();
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
