using InternalDAL.Repositories.Abstract;
using InternalDAL.Services.Abstract;
using InternalDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternalDAL.Repositories;
using InternalDAL;
using Moq;
using InternalDAL.Models;

namespace TestProject1
{
    public class ContactsServiceTests
    {
        private IContactsService _contactsService;
        private Mock<IContactsRepository> _moqContactsRepository;

        [SetUp]
        public void Setup()
        {
            _moqContactsRepository = new Mock<IContactsRepository>();
            _contactsService = new ContactsService(_moqContactsRepository.Object);
        }

        [Test]
        public void AddOrUpdateRangeAsync_Test()
        {
            // a
            var contactsList = new List<Contact>
            {
                new Contact
                {
                    FirstName = "FirstNameUpdated",
                    LastName = "LastNameUpdated",
                    Email = "existemail@mail.com"
                },
                new Contact
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Email = "notexistemail@mail.com"
                }
            };

            var existedContact = new Contact
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "existemail@mail.com"
            };
            _moqContactsRepository.Setup(x => x.FindContactByEmailAsync(contactsList[0].Email)).Returns(Task.FromResult(existedContact));

            // a
            var result = _contactsService.AddOrUpdateRangeAsync(contactsList).Result;

            // a _mockUserRepository.Verify(x => x.Get(It.IsAny<Func<User, bool>>()), Times.Once);
            _moqContactsRepository.Verify(x => x.FindContactByEmailAsync(contactsList[0].Email), Times.Once);
            _moqContactsRepository.Verify(x => x.FindContactByEmailAsync(contactsList[1].Email), Times.Once);

            _moqContactsRepository.Verify(x => x.Update(contactsList[0]), Times.Once);
            _moqContactsRepository.Verify(x => x.Update(contactsList[1]), Times.Never);

            _moqContactsRepository.Verify(x => x.Create(contactsList[0]), Times.Never);
            _moqContactsRepository.Verify(x => x.Create(contactsList[1]), Times.Once);

            _moqContactsRepository.Verify(x => x.SaveAsync(), Times.Once);

            Assert.Contains(new KeyValuePair<string, string>("existemail@mail.com", "Updated contact"), result);
            Assert.Contains(new KeyValuePair<string, string>("notexistemail@mail.com", "Created contact"), result);
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
