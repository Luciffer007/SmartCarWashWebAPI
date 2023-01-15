using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SmartCarWashWebAPI.Models
{
    public class Sale : BaseModel
    {
        [NotMapped]
        public string Date 
        { 
            get
            {
                return DateTime.ToString("MM/dd/yyyy");
            }
        }
        [NotMapped]
        public string Time
        {
            get
            {
                return DateTime.ToString("HH:mm");
            }
        }
        public DateTime DateTime { get; set; }
        public int SalesPointId { get; set; }
        public Buyer Buyer { get; set; }
        public int? BuyerId { get; set; }
        [NotMapped]
        public float TotalAmount
        {
            get
            {
                float result = 0f;
                if (SalesData != null)
                {
                    foreach (SaleData sale in SalesData)
                    {
                        result += sale.ProductIdAmount;
                    }

                    return result;
                }

                return 0;
            }
        }
        public ICollection<SaleData> SalesData { get; set; }

        public bool ShouldSerializeDateTime()
        {
            return false;
        }
    }

    public class SaleData
    {
        public Sale Sale { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductIdAmount { get; set; }

        public bool ShouldSerializeSaleId()
        {
            return false;
        }
    }
}
