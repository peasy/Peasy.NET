using System;
using System.Linq;
using System.Reflection;

namespace Peasy.Extensions
{
    public static class IServiceExtensions
    {
        private static bool ImplementsInterface (this IService service, Type genericInterfaceType)
        {
            return service.GetType ().GetTypeInfo ().ImplementedInterfaces.Any (i =>
                i.GetTypeInfo ().IsGenericType && i.GetTypeInfo ().GetGenericTypeDefinition () == genericInterfaceType);
        }

        public static bool SupportsGetAllCommand (this IService service)
        {
            return service.ImplementsInterface (typeof(ISupportGetAllCommand<>));
        }

        public static bool SupportsGetByIDCommand(this IService service)
        {
            return service.ImplementsInterface(typeof(ISupportGetByIDCommand<,>));
        }

        public static bool SupportsInsertCommand(this IService service)
        {
            return service.ImplementsInterface(typeof(ISupportInsertCommand<>));
        }

        public static bool SupportsUpdateCommand(this IService service)
        {
            return service.ImplementsInterface(typeof(ISupportUpdateCommand<>));
        }

        public static bool SupportsDeleteCommand(this IService service)
        {
            return service.ImplementsInterface(typeof(ISupportDeleteCommand<>));
        }
    }
}
