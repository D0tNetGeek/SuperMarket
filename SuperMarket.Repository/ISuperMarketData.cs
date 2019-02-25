using System.Collections.Generic;

namespace SuperMarket.Repository
{
    public interface ISuperMarketData
    {
        List<ProductDto> DisplayAvailableItems();
    }
}
