using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class BasketModel
    {
        public BasketModel()
        {
            Items = new List<BasketItemModel>();
            Freights = new List<BasketItemModel>();
            Payments = new List<BasketItemModel>();
            Promotions = new List<PromotionModel>();
        }

        public int Id { get; private set; }
        public List<BasketItemModel> Items { get; set; }
        public List<BasketItemModel> Freights { get; set; }
        public List<BasketItemModel> Payments { get; set; } 

        public List<PromotionModel> Promotions { get; set; }
        public string DiscountCode { get; set; }
        public int? CustomerId { get; set; }
        public int? CompanyId { get; set; }
        public OnHandStatusModel OnHand { get; set; }
        public string Comment { get; set; }
        public string OrderReference { get; set; }
    }
}
