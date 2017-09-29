using System;

namespace Enferno.Public.Web.ViewModels
{
    [Serializable]
    public class FileViewModel : BaseViewModel
    {
        public ProductFileType Type { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
    }
}
