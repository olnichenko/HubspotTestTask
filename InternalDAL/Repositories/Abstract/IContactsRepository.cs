using InternalDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Repositories.Abstract
{
    public interface IContactsRepository : IRepositoryBase<Contact, long>
    {
        Task<Contact> FindContactByEmailAsync(string email);
    }
}
