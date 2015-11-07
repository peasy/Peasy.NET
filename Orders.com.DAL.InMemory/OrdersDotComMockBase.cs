using Peasy.Core;
using Orders.com.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peasy;

namespace Orders.com.DAL.InMemory
{
    public class OrdersDotComMockBase<DTO> : MockDataProxyBase<DTO, long> where DTO : IDomainObject<long>
    {
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
