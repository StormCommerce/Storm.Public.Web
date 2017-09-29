using System;
using Enferno.StormApiClient.Expose;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class PaymentResponseModel
    {
        public PaymentStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public string RedirectUrl { get; set; }
        public string SuccessUrl { get; set; }
        public NameValues RedirectParameters { get; set; }
        public string HttpMethod { get; set; }
        public string OrderNo { get; set; }
    }
}
