using AnonymousPoll.Business.Contracts;
using AnonymousPoll.Business.Implementations;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Implementations;
using AnonymousPoll.DataAccess.Contracts;
using AnonymousPoll.DataAccess.Implementations;
using Autofac;

namespace AnonymousPoll
{
    internal class Program
    {
        private static IContainer RegisterDependency()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<BusinessLogic>().As<IBusinessLogic>();
            builder.RegisterType<TxtAccess>().As<IDataAccess>();
            builder.RegisterType<StudentLogic>().As<IStudentLogic>();
            return builder.Build();
        }

        private static void Main()
        {
            RegisterDependency().Resolve<Application>().Run();
        }
    }
}
