using Newtonsoft.Json;

namespace MercuryTours
{
    public class RegistrationInformation
    {
        public ContactInformation ContactInformation { get; set; }
        public MailingInformation MailingInformation { get; set; }
        public UserInformation UserInformation { get; set; }
    }

    public class ContactInformation
    {
        [JsonProperty("firstName", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", Required = Required.Always)]
        public string LastName { get; set; }
        
        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }
        
        [JsonProperty("email", Required = Required.Always)]
        public string Email { get; set; }
    }

    public class MailingInformation
    {
        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }

        [JsonProperty("city", Required = Required.Always)]
        public string City { get; set; }

        [JsonProperty("state", Required = Required.Always)]
        public string State { get; set; }

        [JsonProperty("postalCode", Required = Required.Always)]
        public string PostalCode { get; set; }

        [JsonProperty("country", Required = Required.Always)]
        public string Country { get; set; }
    }

    public class UserInformation
    {
        [JsonProperty("userName", Required = Required.Always)]
        public string UserName { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string Password { get; set; }

        [JsonProperty("confirmPassword", Required = Required.Always)]
        public string ConfirmPassword { get; set; }
    }
}
