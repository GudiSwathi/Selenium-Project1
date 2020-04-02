using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MercuryTours.DataModels;
using Newtonsoft.Json;

namespace MercuryTours.CoreHelpers
{
    public static class GetTestData
    {
        public static List<RegistrationInformation> GetRegistrationInformation()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "RegistrationInformationData.json");
            string jsonTestDataString = File.ReadAllText(filePath);

            List<RegistrationInformation> registrationInformation = JsonConvert.DeserializeObject<List<RegistrationInformation>>(jsonTestDataString);
            return registrationInformation;
        }

        public static List<SignOnInformation> GetSignOnInformation()
        {
            string filePath1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData", "SignOnUserDetails.json");
            string jsonDataString = File.ReadAllText(filePath1);

            List<SignOnInformation> signOnInformation = JsonConvert.DeserializeObject<List<SignOnInformation>>(jsonDataString);
            return signOnInformation;
        }
    }
}
