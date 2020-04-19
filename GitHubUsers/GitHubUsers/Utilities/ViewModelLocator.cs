using System;
using Autofac;
using GitHubUsers.Services.OpenUri;
using GitHubUsers.Services.Repository;
using GitHubUsers.Services.Request;
using GitHubUsers.Services.User;
using GitHubUsers.ViewModels;

namespace GitHubUsers.Utilities
{
    public class ViewModelLocator
    {
        private static IContainer _container;
        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        protected ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RequestService>().As<IRequestService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<RepositoryService>().As<IRepositoryService>();
            builder.RegisterType<OpenUrlService>().As<IOpenUrlService>();
            
            builder.RegisterType<MainViewModel>();
            
            _container?.Dispose();

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
