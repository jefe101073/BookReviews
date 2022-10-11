using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterfaces
{
    public interface IAPIClientService
    {
        Task<string> CallAPIGetAsync(string apiUrl);

        Task<string> CallAPIPostAsync(string apiUrl, HttpContent content);
    }
}
