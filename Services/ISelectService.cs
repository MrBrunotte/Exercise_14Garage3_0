using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Garage3.Services
{
    public interface ISelectService
    {
        Task<IEnumerable<SelectListItem>> TypeAsync();
    }
}
