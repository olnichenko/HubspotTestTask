using ExternalDAL.Abstract;
using ExternalDAL.Models;
using Newtonsoft.Json;

namespace ExternalDAL.Api
{
    public class HubspotApi : BaseApi, IHubspotApi
    {
        // Must be specified in the settings file
        protected const string _hubspotApiUrl = "https://api.hubapi.com/";
        protected const string _hubspotAllContactsApiUrl = _hubspotApiUrl + "contacts/v1/lists/all/contacts/all";
        public virtual async Task<IEnumerable<ContactModel>> GetAllContactsAsync(string apiKey)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {apiKey}" }
            };
            var jsonResult = await GetJsonDataAsync(_hubspotAllContactsApiUrl, headers);

            dynamic resultData = JsonConvert.DeserializeObject(jsonResult);

            if (resultData.status?.ToString() == "error")
            {
                throw new Exception(resultData.message.ToString());
            }
            var contacts = new List<ContactModel>();

            foreach (dynamic item in resultData.contacts)
            {
                var contact = new ContactModel
                {
                    FirstName = item.properties.firstname.value.ToString(),
                    LastName = item.properties.lastname.value.ToString()
                };
                foreach (var ident in item["identity-profiles"][0].identities)
                {
                    if(ident.type.ToString().ToLower() == "email")
                    {
                        contact.Email = ident.value;
                        break;
                    }
                }
                contacts.Add(contact);
            }

            return contacts;
        }
    }
}
