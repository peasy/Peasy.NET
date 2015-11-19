using Peasy.Core;
using System;

namespace Orders.com.DAL.Http
{
    public abstract class OrdersDotComHttpProxyBase<T, TKey> : HttpServiceProxyBase<T, TKey> where T : IDomainObject<TKey>
    {
        protected virtual string BaseAddress
        {
            get
            {
                string hostSettingName = "apiHostNameAddress";
                var baseAddress = System.Configuration.ConfigurationManager.AppSettings[hostSettingName];
                if (baseAddress == null) throw new Exception(string.Format("The setting '{0}' in the AppSettings portion of the configuration file was not found.", hostSettingName));
                return baseAddress;
            }
        }
    }
}
