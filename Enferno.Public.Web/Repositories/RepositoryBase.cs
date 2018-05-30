using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient;
using Enferno.StormApiClient.ExposeProxy;
using Enferno.Web.StormUtils;
using Unity.Interception.Utilities;


namespace Enferno.Public.Web.Repositories
{
    public abstract class RepositoryBase
    {
        public const string Ignored = "ignored";
        public const int InvalidId = -1;

        protected IAccessClient MyClient;

        public IAccessClient GetBatch()
        {
            return MyClient ?? new AccessClient();
        }

        protected static string StatusSeed(string statusSeed)
        {
            if (statusSeed == null) return null;
            return !statusSeed.Equals(Ignored) ? statusSeed : StormContext.Configuration.ProductStatusIdSeed;
        }

        protected static string AssortmentSeed(string assortmentSeed)
        {
            if (assortmentSeed == null) return null;
            return !assortmentSeed.Equals(Ignored) ? assortmentSeed : StormContext.Configuration.AssortmentIdSeed;
        }

        protected static string DivisionSeed(string divisionSeed)
        {
            if (divisionSeed == null) return null;
            return !divisionSeed.Equals(Ignored) ? divisionSeed : StormContext.DivisionId.HasValue ? StormContext.DivisionId.ToString() : null;
        }

        protected static string PricelistSeed(string pricelistSeed)
        {
            if (pricelistSeed == null) return null;
            return !pricelistSeed.Equals(Ignored) ? pricelistSeed : StormContext.PriceListIdSeed;
        }

        protected static string CultureCode(string cultureCode)
        {
            if (cultureCode == null) return null;
            return !cultureCode.Equals(Ignored) ? cultureCode : StormContext.CultureCode;
        }

        protected static string Customer(int? customerId)
        {
            if(customerId == InvalidId) return StormContext.CustomerId.HasValue ? StormContext.CustomerId.ToString() : null;
            return customerId.HasValue ? GetNullableInt(customerId) : null;
        }

        protected static string Company(int? companyId)
        {
            if (companyId == InvalidId) return StormContext.CompanyId.HasValue ? StormContext.CompanyId.ToString() : null;
            return companyId.HasValue ? GetNullableInt(companyId) : null;
        }

        protected static string Currency(int? currencyId)
        {
            if (currencyId == InvalidId) return XmlConvert.ToString(StormContext.CurrencyId);
            return currencyId.HasValue ? GetNullableInt(currencyId) : null;
        }

        protected string Parametrics(ICollection<ProductListModel.ListParametric> parametrics, string cultureCode)
        {
            if (parametrics == null || parametrics.Count == 0) return null;
            var buff = new StringBuilder();
            var separator = "";
            foreach (var parametric in parametrics)
            {
                buff.Append(separator);
                var p = ApplicationDictionary.Instance.ParametricInfo(parametric.Id, CultureCode(cultureCode));
                if (p.Type == ParametricType.MultiValue)
                {
                    buff.AppendFormat("M{0}_{1}", parametric.Id, parametric.ValueId);
                }
                else if (p.Type == ParametricType.ListValue)
                {
                    buff.AppendFormat("L{0}_{1}", parametric.Id, parametric.ValueId);
                }
                else
                {
                    buff.AppendFormat("V{0}_{1}", parametric.Id, parametric.ValueFrom);
                    if (!string.IsNullOrWhiteSpace(parametric.ValueTo)) buff.AppendFormat("-{0}", parametric.ValueFrom);
                }
                separator = "*";
            }

            return buff.ToString();
        }

        protected static string GetAccessoryTypeSeed(AccessoryType[] accessoryTypes)
        {
            return accessoryTypes == null || accessoryTypes.Length == 0 ? null : accessoryTypes.Select(t => XmlConvert.ToString((int)t)).JoinStrings(",");
        }

        protected static string GetNullableInt(int? i)
        {
            return i?.ToString();
        }

        protected static string GetNullableBool(bool? b)
        {
            return b?.ToString();
        }

        protected int AccountId(int? accountId)
        {
            const int defaultAccountId = 1;
            return accountId ?? StormContext.AccountId.GetValueOrDefault(defaultAccountId);
        }
    }
}
