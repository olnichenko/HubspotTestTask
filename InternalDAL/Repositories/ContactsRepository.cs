using InternalDAL.Models;
using InternalDAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Repositories
{
    public class ContactsRepository : RepositoryBase<Contact, long>, IContactsRepository
    {
        public ContactsRepository(HubspotDataDbContext hubspotDbContext) : base(hubspotDbContext)
        {
        }

        public async Task<Contact> FindContactByEmailAsync(string email)
        {
            var contact = await FindByCondition(x => x.Email == email).SingleOrDefaultAsync();
            return contact;
        }
    }
}
