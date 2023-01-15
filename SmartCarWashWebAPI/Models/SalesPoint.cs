using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartCarWashWebAPI.Models
{
    public class SalesPoint : BaseModel
    {
        public string Name { get; set; }
        public ICollection<ProvidedProduct> ProvidedProducts { get; set; }
    }

    public class ProvidedProduct
    {
        public SalesPoint SalesPoint { get; set; }
        public int ProductId { get; set; }
        public int SalesPointId { get; set; }
        public int ProductQuantity { get; set; }

        public bool ShouldSerializeSalesPointId()
        {
            return false;
        }
    }
}
