using ExternalDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDAL.Abstract
{
    public interface IHubspotApi : IApi
    {
        Task<IEnumerable<ContactModel>> GetAllContactsAsync(string apiKey);
    }
}
