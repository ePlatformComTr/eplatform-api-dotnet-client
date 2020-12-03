using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders.Base;

namespace ePlatform.Api.eBelge.Invoice.Tests.Builders
{
    public class AddressBookModelBuilder : BuilderBase<AddressBookModel, AddressBookModelBuilder>
    {
        public override AddressBookModelBuilder CreateWithDefaultValues()
        {
            _concreteObject = new AddressBookModel()
            {
                Alias = "urn:mail:defaulttest11pk@medyasoft.com.tr",
                IdentificationNumber = "1234567801",
                ReceiverPersonSurName = "Medyasoft Test",
                Name = "Test Kurum Üç",
                ReceiverCity = "İstanbul",
                ReceiverDistrict = "Üsküdar",
                ReceiverCountry = "Türkiye"
                // ReceiverCountryId = 1
            };

            return this;
        }
    }
}
