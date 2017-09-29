using System;
using System.Collections.Generic;
using System.Linq;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            Flags = new List<int>();
            Files = new List<FileViewModel>();
            _variantParametrics = new List<ParametricViewModel>();
            _variants = new List<VariantViewModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string DescriptionHeader { get; set; }
        public List<int> Flags { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasurement { get; set; }
        public OnHandStatus OnHandStatus { get; set; }
        public string OnHandStatusText1 { get; set; }
        public string OnHandStatusText2 { get; set; }
        public decimal OnHandStatusCount { get; set; }
        public decimal DisplayPrice { get; set; }
        public decimal CatalogPrice { get; set; }
        public decimal VatRate { get; set; }
        public int PriceListId { get; set; }
        public decimal RecommendedQuantity { get; set; }
        public int StatusId { get; set; }
        public string SubHeader { get; set; }
        public string UniqueName { get; set; }
        public List<FileViewModel> Files { get; set; }

        private List<ParametricViewModel> _variantParametrics; 
        public List<ParametricViewModel> VariantParametrics
        {
            get { return _variantParametrics; }
            set { _variantParametrics = value; }
        }

        private List<VariantViewModel> _variants;
        public List<VariantViewModel> Variants
        {
            get { return _variants; }
            set { _variants = value; }
        }

        public FileViewModel DefaultImage
        {
            get { return Files.First(file => file.Type == ProductFileType.DefaultImage); }
        }

        public IEnumerable<FileViewModel> AdditionalImages
        {
            get { return Files.Where(file => file.Type == ProductFileType.AdditionalImage); }
        } 

        public bool HasVariantParametric(int parametricId, IEnumerable<ParametricViewModel> parametricsToChooseFrom = null)
        {
            if (parametricsToChooseFrom == null)
                parametricsToChooseFrom = VariantParametrics;

            return parametricsToChooseFrom.Any(variantParametric => variantParametric.Id == parametricId);
        } 

        public IEnumerable<ParametricValueViewModel> GetDistinctValuesForVariantParametric(int parametricId)
        {
            var variantParametricsToChooseFrom = VariantParametrics;

            if (HasVariantsButNoVariantParametrics())
                variantParametricsToChooseFrom = GetVariantParametricsForVariantsWithNoVariantParametrics();

            if (!HasVariantParametric(parametricId, variantParametricsToChooseFrom)) return new List<ParametricValueViewModel>();

            var variantParametric = variantParametricsToChooseFrom.Single(parametric => parametric.Id == parametricId);
            return variantParametric.Values;

        }

        private List<VariantViewModel> GetVariantsForVariantsWithNoVariantParametrics()
        {
            var variantsWithFakeParametricAdded = _variants.Select(variant =>
            {
                var parametric = new ParametricViewModel
                {
                    Id = -1,
                    Values = new List<ParametricValueViewModel>
                    {
                        new ParametricValueViewModel {Value = variant.Name}
                    }
                };

                variant.VariantParametrics = new List<ParametricViewModel> {parametric};

                return variant;
            });
            return new List<VariantViewModel>(variantsWithFakeParametricAdded);
        } 

        private List<ParametricViewModel> GetVariantParametricsForVariantsWithNoVariantParametrics()
        {
            var fakeParametric = new ParametricViewModel() { Id = -1 };
            var fakeParametricValues = _variants.Select(variant => new ParametricValueViewModel() {Value = variant.Name}).ToList();

            fakeParametric.Values = fakeParametricValues;

            return new List<ParametricViewModel>(){fakeParametric} ;
        }

        private bool HasVariantsButNoVariantParametrics()
        {
            return _variants.Count > 1 && _variantParametrics.Count == 0;
        }

    }
}
