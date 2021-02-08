using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Domain.DataTransferObjects
{
    public class PriceUpdateDTO
    {
        public int CategoryId { get; set; }

        public int PercentageValue { get; set; }

        public PriceUpdateType PriceUpdateType { get; set; }
    }
}
