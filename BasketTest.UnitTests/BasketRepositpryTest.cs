using BasketTest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasketTest.Core.Interfaces;
using Moq;
using BasketTest.Core.ApiModels;
using Moq.Language;
using BasketTest.Persistence.DataStore;
using BasketTest.Persistence.Repository;

namespace BasketTest.UnitTests
{
    [TestClass]
    public class BasketRepositoryTest
    {
        private IEnumerable<Basket> _fakeBaskets;
        public BasketRepositoryTest()
        {
            _fakeBaskets = new List<Basket>(GetFakeBaskets());
        }
        public IEnumerable<Basket> GetFakeBaskets()
        {
            var fakeBaskets = new List<Basket>
            {
                new Basket {TransactionNumber  =Guid.Parse("0002b3be-4f0b-418a-b7dd-670494981a89"), NumberOfPassengers=null,Domain=1,AgentId=null,
                            ReferrerUrl="",CreatedDateTime = DateTime.Parse("08/08/2016 00:00:00"),SelectedCurrency="GBP",ReservationSystem="Tarus"},
                new Basket {TransactionNumber  = Guid.Parse("000840f3-e60c-4359-9ad5-b5de77c801c4"), NumberOfPassengers=null,Domain=10,AgentId=null,
                            ReferrerUrl="https://dev.titantravel.test/destinations/north-america/usa/california/california-and-the-golden-west",
                                CreatedDateTime = DateTime.Parse("04/07/2018 00:00:00"),SelectedCurrency="GBP",ReservationSystem="Tarus"},
                 new Basket {TransactionNumber  = Guid.Parse("001e0ada-0e08-4880-bbf6-ff84f1fe1075"), NumberOfPassengers=2,Domain=1,AgentId=null,
                            ReferrerUrl="http://uat9.cms.travel.saga.co.uk/Ocean-Cruises/Where-we-cruise/Canaries/Canary-Islands-and-Cape-Verde.aspx?availability=2&duration=1-999&boardbasis=FB",
                     CreatedDateTime = DateTime.Parse("14/10/2016 00:00:00"),SelectedCurrency="GBP",ReservationSystem="Tarus"},
                       new Basket {TransactionNumber  = Guid.Parse("002f5a9f-b148-461c-a598-fc3f8ba5c4b6"), NumberOfPassengers=2,Domain=10,AgentId=null,
                            ReferrerUrl="https://uat4.titantravel.test/destinations/europe/italy/scenic-lake-orta-and-a-matterhorn-rail-journey?availability=2&boardbasis=HB",
                     CreatedDateTime = DateTime.Parse("17/05/2018 00:00:00"),SelectedCurrency="GBP",ReservationSystem="Tarus"},

            };
            return fakeBaskets;
        }

        [TestMethod]
        public void GetBasket_Return_TheBasketWith_TransactionNumberSpecified()
        {
            //Arrange
            Mock<IDataStoreContext> dataContext = new Mock<IDataStoreContext>();
            dataContext.Setup(ds => ds.Baskets).Returns(_fakeBaskets);
            var basketRepo = new BasketRepository(dataContext.Object);

            //Act
            var resource = basketRepo.GetBasket(Guid.Parse("0002b3be-4f0b-418a-b7dd-670494981a89"));

            //Assert
            Assert.IsNotNull(resource);
            Assert.AreEqual(Guid.Parse("0002b3be-4f0b-418a-b7dd-670494981a89"), resource.TransactionNumber);
        }


        [TestMethod]
        public void GetAllBasket_WithDomain_Filters_Return_AllBasketsWithSpecifiedDomainNumber()
        {
            // Arrange
            Mock<IDataStoreContext> dataContext = new Mock<IDataStoreContext>();
            dataContext.Setup(ds => ds.Baskets).Returns(_fakeBaskets);

            var basketRepo = new BasketRepository(dataContext.Object);
            //Act
            var resource = basketRepo.GetAllBasket(10);

            //Assert
            Assert.IsNotNull(resource);
            Assert.AreEqual(2, resource.Count());
        }

        [TestMethod]
        public void GetAllBasket_WithOutDomain_Filters_Return_AllBaskets()
        {
            // Arrange
            Mock<IDataStoreContext> dataContext = new Mock<IDataStoreContext>();
            dataContext.Setup(ds => ds.Baskets).Returns(_fakeBaskets);

            var basketRepo = new BasketRepository(dataContext.Object);

            //Act
            var resource = basketRepo.GetAllBasket();

            //Assert
            Assert.IsNotNull(resource);
            Assert.AreEqual(4, resource.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetBasket_Return_Throws_Exception_When_Empty_TransactioNumber_Is_Specified()
        {
            //Arrange
            Mock<IDataStoreContext> dataContext = new Mock<IDataStoreContext>();
            dataContext.Setup(ds => ds.Baskets).Returns(_fakeBaskets);
            var basketRepo = new BasketRepository(dataContext.Object);

            //Act
            var resource = basketRepo.GetBasket(Guid.Empty);
        }
    }

    
}
