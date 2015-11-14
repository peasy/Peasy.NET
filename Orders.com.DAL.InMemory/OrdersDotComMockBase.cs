using Peasy.Core;
using Orders.com.Extensions;
using System.Linq;
using Peasy;
using Peasy.DataProxy.InMemory;
using System.Collections.Generic;
using Peasy.Exception;

namespace Orders.com.DAL.InMemory
{
    public class OrdersDotComMockBase<DTO> : InMemoryDataProxyBase<DTO, long> where DTO : IDomainObject<long>
    {
        public override DTO GetByID(long id)
        {
            try
            {
                return base.GetByID(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new DomainObjectNotFoundException("The current item was not found", ex);
            }
        }

        protected override long GetNextID()
        {
            if (Data.Values.Any())
                return Data.Values.Max(c => c.ID) + 1;

            return 1;
        }

        public override IVersionContainer IncrementVersion(IVersionContainer versionContainer)
        {
            versionContainer.IncrementVersionByOne();
            return versionContainer;
        }
    }
}
