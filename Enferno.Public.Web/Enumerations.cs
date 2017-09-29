
namespace Enferno.Public.Web
{
    public enum AccessoryType
    {
        Checkout = 1,
        Focus = 2,
        Standard = 3,
        Hidden = 4,
        Service = 5,
        Insurance = 6
    }

    public enum BasketStatus
    {
        SalesEdit = 1,
        Basket = 3,
        Saved = 9,
    }

    public enum OnHandStatus
    {
        Ok = 1,
        Warning = 2,
        Error = 3,
    }

    public enum FilterType
    {
        Range = 1,
        List = 2,
        Bool = 3,
    }

    public enum FilterCategory
    {
        Category = 1,
        Manufacturer = 2,
        Price = 3,
        Flag = 4,
        OnHand = 5,
    }

    public enum ProductFileType
    {
        DefaultImage = 1,
        VariantImage = 2,
        Document = 6,
        EmbeddedVideo = 10,
        AdditionalImage = 11,
        ProductManual = 13,
        ExternalImage = 14,
        EnvironmentalImage = 15,
        InternalImage = 17,
        IllustrationOrSymbol = 18,
        Logotype = 19,
        DetailImage = 20,
        AlternateImage = 21,
        Text = 22,
        ModelInformationSalesText = 23,
        AssemblyInstruction = 24,
        FactSheet = 25,
        Broschure = 26,
        ExternalTrademark = 27,
        CareInstructions = 28,
        Symbols = 29,
        QualityDocument = 30,
        Illustration = 31,
        PackningInstruction = 32,
        DrawingUnderlay = 33,
        TestProtocol = 34,
        CutThrughImage = 35,
        PriceSheet = 36,
        DiscountFactSheet = 37,
        ProductCloseUp = 38,
        MultiProductImage = 39,
        InstallationImage = 40,
        MultiProductImageSerie = 41,
        MaterialSample = 42,
    }

    public enum FileType
    {
        Jpg = 1,
        Gif = 2,
        Pdf = 3,
        Mp4 = 4,
        Embedded = 6,
        Swf = 7,
        Png = 8,
        Tif = 9,
        Eps = 14,
    }

    public enum ExternalFileType
    {
        Jpg = 10,
        Eps = 11,
        Mp4 = 12,
        Pdf = 13,
    }

    public enum ProductStatus
    {
        Active = 1,
        Coming = 2,
        Expiring = 3,
        Hidden = 4,
        Inactive = 5,
        NotActivated = 6,
    }

    public enum ProductType
    {
        /// <summary>
        /// Erp Standard
        /// </summary>
        Standard = 1,
        Refurbished = 2,
        Freight = 3,
        Insurance = 4,
        GiftCertificate = 5,
        Download = 7,
        Structure = 8,
        Invoice = 9,
        Demo = 10,
        Service = 11,
        Installation = 12,
        /// <summary>
        /// Standard
        /// </summary>
        Standard2 = 14,
        ErpExtended = 15,
        ErpStandardNoStock = 16,
        ErpPackage = 17,
        Donations = 18,
        Brand = 19,
        EnvironmentalFee = 20,
        PackingFee = 21,
        Configuration = 22,
    }

    public enum PaymentStatus
    {
        Ok,
        Error
    }
}
