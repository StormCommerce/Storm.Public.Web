
using AutoMapper;
using Enferno.Public.Web.Models;
using Enferno.StormApiClient.Products;

namespace Enferno.Public.Web.Builders
{
    public class ManufacturerBuilder : BuilderBase
    {

        public ManufacturerBuilder()
        {
        }

        public ManufacturerBuilder(ISiteRules rules) : base(rules)
        {
        }

        public ManufacturerModel BuildManufacturerModel(Manufacturer manufacturer)
        {
            return manufacturer == null ? null : Mapper.Map<ManufacturerModel>(manufacturer);
        }
    }
}
