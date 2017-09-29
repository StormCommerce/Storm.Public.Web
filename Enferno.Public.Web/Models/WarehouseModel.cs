using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class WarehouseModel
    {
        public int LocationId { get; set; }
        public int WarehouseId { get; set; }
        public int StoreId { get; set; }
        public OnHandStatusModel OnHand { get; set; }
    }
}
