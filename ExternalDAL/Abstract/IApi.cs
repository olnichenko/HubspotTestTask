using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDAL.Abstract
{
    public interface IApi
    {
        Task<string> GetJsonDataAsync(string url, Dictionary<string, string>? headers);
    }
}
