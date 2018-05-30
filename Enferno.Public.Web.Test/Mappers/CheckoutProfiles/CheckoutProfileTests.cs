using System.Linq;
using AutoMapper;
using Enferno.Public.InversionOfControl;
using Enferno.Public.Web.Models;
using Enferno.Public.Web.Test.Mappers.ProductProfiles;
using Enferno.StormApiClient.Customers;
using Enferno.StormApiClient.Shopping;
using Enferno.Web.StormUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Enferno.Public.Web.Test.Mappers.CheckoutProfiles
{
    [TestClass]
    public class CheckoutProfileTests
    {
        [TestInitialize]
        public void Init()
        {
            IoC.RegisterType<ISiteRules, TestSiteRules>();

            var stormContext = MockRepository.GenerateMock<IStormContext>();
            stormContext.Stub(x => x.Configuration).Return(new StormConfigurationSection());
            stormContext.Stub(x => x.ShowPricesIncVat).Return(true);
            stormContext.Stub(x => x.CultureCode).Return("sv");
            StormContext.SetInstance(stormContext);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void MappingIsValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod, TestCategory("UnitTest")]
        public void CheckoutToCompanyCheckoutModelTest()
        {
            var checkout = new Checkout()
            {
                Basket = new Basket
                {
                    DiscountCode = "blaj",
                    Items = new BasketItemList {new BasketItem {Price = 12, VatRate = 12}},
                    AppliedPromotions = new PromotionList()
                },
                Buyer = new Customer
                {
                    Email = "test@enferno.se",
                    FirstName = "Test",
                    LastName = "Tester",
                    DeliveryAddresses = new AddressList
                    {
                        new Address
                        {
                            City = "Stockholm",
                            Country = "Sweden",
                            Zip = "66666"
                        },
                        new Address
                        {
                            City = "Stockholm",
                            Country = "Estonia",
                            Zip = "11111"
                        }
                    }
                },
                Payer = new Customer
                {
                    Companies = new CompanyList
                    {
                        new Company
                        {
                            Id = 10,
                            Code = "code",
                            Name = "Enferno AB",
                            OrgNo = "999999-0000",
                            Phone = "+1 800 20 45 67",
                            InvoiceAddress = new Address
                            {
                                City = "Stockholm",
                                Country =  "Sweden",
                                Zip = "62230"
                            }
                        }
                    }
                },
                ShipTo = new Customer
                {
                    FirstName = "sid",
                    LastName = "shipper",
                    Email = "sid.shipper@company.com",
                    CellPhone = "070123456",
                    DeliveryAddresses = new AddressList
                    {
                        new Address
                        {
                            City = "Stockholm",
                            Country = "Sweden",
                            Zip = "66666"
                        },
                        new Address
                        {
                            City = "Stockholm",
                            Country = "Estonia",
                            Zip = "11111"
                        }
                    }
                }
            };

            var checkoutModel = Mapper.Map<Checkout, CompanyCheckoutModel>(checkout);

            Assert.IsNotNull(checkoutModel.Basket);

            Assert.IsNotNull(checkoutModel.Buyer);
            //Assert.AreEqual(1, checkoutModel.Buyer.Companies.Count());
            //Assert.AreEqual(2, checkoutModel.Buyer.DeliveryAddresses.Count());
            Assert.AreEqual(checkoutModel.Buyer.Email, checkout.Buyer.Email);
        }

        [TestMethod, TestCategory("UnitTest")]
        public void CheckoutPrivateShipToModelCustomerTest()
        {           
            var model = new CheckoutPrivateShipToModel
            {
                CustomerId = 1,
                DeliveryAddress = new AddressModel {CareOf="CareOf", City = "City", Country = "Sverige", CountryId = 1, Line1 = "Line1", Line2 = "Line2", Zip = "Zip"},
                PersonInformation = new PersonInformationModel
                {
                    FirstName = "First", LastName = "Last", Email = "first.last@email.com", CellPhone = "070123456"
                },
            };

            var customer = Mapper.Map<CheckoutPrivateShipToModel, Customer>(model);

            Assert.IsNotNull(customer);
            Assert.AreEqual(model.CustomerId, customer.Id);
            AssertPersonalInfoModelEqualsCustomer(model.PersonInformation, customer);
            AssertAddressModelEquals(model.DeliveryAddress, customer.DeliveryAddresses.FirstOrDefault());            
        }

        [TestMethod, TestCategory("UnitTest")]
        public void CheckoutPrivatePayerToModelCustomerTest()
        {
            var model = new CheckoutPrivatePayerModel
            {
                CustomerId = 1,
                InvoiceAddress = new AddressModel { CareOf = "CareOf", City = "City", Country = "Sverige", CountryId = 1, Line1 = "Line1", Line2 = "Line2", Zip = "Zip" },
                PersonInformation = new PersonInformationModel
                {
                    FirstName = "First",
                    LastName = "Last",
                    Email = "first.last@email.com",
                    CellPhone = "070123456"
                },
            };

            var customer = Mapper.Map<CheckoutPrivatePayerModel, Customer>(model);

            Assert.IsNotNull(customer);
            Assert.AreEqual(model.CustomerId, customer.Id);
            AssertPersonalInfoModelEqualsCustomer(model.PersonInformation, customer);
            AssertAddressModelEquals(model.InvoiceAddress, customer.InvoiceAddress);
        }

        private void AssertAddressModelEquals(AddressModel deliveryAddress, Address address)
        {
            Assert.IsNotNull(address);
            Assert.AreEqual(deliveryAddress.CareOf, address.CareOf);
            Assert.AreEqual(deliveryAddress.Line1, address.Line1);
            Assert.AreEqual(deliveryAddress.Line2, address.Line2);
            Assert.AreEqual(deliveryAddress.Zip, address.Zip);
            Assert.AreEqual(deliveryAddress.City, address.City);
            Assert.AreEqual(deliveryAddress.CountryId, address.CountryId);
            Assert.AreEqual(deliveryAddress.Country, address.Country);
        }

        private void AssertPersonalInfoModelEqualsCustomer(PersonInformationModel personInformation, Customer customer)
        {
            Assert.AreEqual(personInformation.FirstName, customer.FirstName);
            Assert.AreEqual(personInformation.LastName, customer.LastName);
            Assert.AreEqual(personInformation.Email, customer.Email);
            Assert.AreEqual(personInformation.CellPhone, customer.CellPhone);
        }
    }
}
