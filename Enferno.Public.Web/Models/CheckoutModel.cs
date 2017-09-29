using System;
using System.Collections.Generic;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public abstract class CheckoutModelBase
    {
        public BasketModel Basket { get; set; }
        public CheckoutBuyerModel Buyer { get; set; }
        public ICheckoutPayerModel Payer { get; set; }
        public ICheckoutShipToModel ShipTo { get; set; }
        public IEnumerable<DeliveryMethodModel> DeliveryMethods { get; set; }
        public IEnumerable<PaymentMethodModel> PaymentMethods { get; set; }
        public string OrderReference { get; set; }
    }

    [Serializable]
    public class CheckoutModel<TPayer, TShipTo> : CheckoutModelBase
        where TPayer : ICheckoutPayerModel
        where TShipTo : ICheckoutShipToModel
    {
        public new TPayer Payer { get; set; }
        public new TShipTo ShipTo { get; set; }
    }

    [Serializable]
    public class CompanyCheckoutModel : CheckoutModel<CheckoutCompanyPayerModel, CheckoutCompanyShipToModel>
    {
        
    }

    [Serializable]
    public class PrivateCheckoutModel : CheckoutModel<CheckoutPrivatePayerModel, CheckoutPrivateShipToModel>
    {
        
    }
}
