using InternalDAL.Models;
using InternalDAL.Repositories;
using InternalDAL.Repositories.Abstract;
using InternalDAL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Services
{
    public class ContactsService : BaseService, IContactsService
    {
        protected readonly IContactsRepository _contactsRepository;
        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public ContactsService()
        {
            // Better use Dependency Injection
            _hubspotDataDbContext = new HubspotDataDbContext();
            _contactsRepository = new ContactsRepository(_hubspotDataDbContext);
        }

        public virtual async Task<Dictionary<string, string>> AddOrUpdateRangeAsync(IEnumerable<Contact> contacts)
        {
            var resultReport = new Dictionary<string, string>();
            foreach (var item in contacts)
            {
                var contact = await _contactsRepository.FindContactByEmailAsync(item.Email);
                string reportString;
                if (contact != null)
                {
                    item.Id = contact.Id;
                    _contactsRepository.Update(item);
                    reportString = "Updated contact";
                }
                else
                {
                    _contactsRepository.Create(item);
                    reportString = "Created contact";
                }
                resultReport.Add(item.Email, reportString);
            }

            await _contactsRepository.SaveAsync();

            return resultReport;
        }
    }
}
