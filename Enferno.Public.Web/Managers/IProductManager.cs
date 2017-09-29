using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enferno.Public.Web.ViewModels;

namespace Enferno.Public.Web.Managers
{
    public interface IProductManager
    {
        ProductViewModel GetProduct(int id);
    }
}
