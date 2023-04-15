using InternalDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Services.Abstract
{
    public interface IContactsService
    {
        Task<Dictionary<string, string>> AddOrUpdateRangeAsync(IEnumerable<Contact> contacts);
    }
}
