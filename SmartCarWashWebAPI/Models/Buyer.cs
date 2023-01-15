using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarWashWebAPI.Models
{
    public class Buyer : BaseModel
    {
        public string Name { get; set; }
        [NotMapped]
        public ICollection<int> SalesIds 
        { 
            get
            {
                List<int> result = new List<int>();
                if (Sales != null)
                {
                    foreach(Sale sale in Sales)
                    {
                        result.Add(sale.Id);
                    }
                }

                return result;
            }
        } 
        public ICollection<Sale> Sales { get; set; }

        public bool ShouldSerializeSales()
        {
            return false;
        }
    }
}
