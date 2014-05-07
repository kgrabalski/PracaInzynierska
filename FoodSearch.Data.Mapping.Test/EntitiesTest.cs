using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FoodSearch.Data.Mapping.Test
{
    [TestClass]
    public class EntitiesTest
    {
        private ISessionSource _sessionSource;
        private IRepositoryProvider _provider;

        [TestInitialize]
        public void FoodSearch_Data_Mapping_Test_Etities_InitializeTest()
        {
            _sessionSource = new SessionSource(Databases.FoodSearchTest);
            _provider = new FoodSearchRepositoryProvider(_sessionSource);
        }

        [TestMethod]
        public void EntitiesMap_Address()
        {
            var res = _provider.GetRepository<Address>().GetAll().List<Address>();
        }

        [TestMethod]
        public void EntitiesMap_City()
        {
            var res = _provider.GetRepository<City>().GetAll().List<City>();
        }

        [TestMethod]
        public void EntitiesMap_Cuisine()
        {
            var res = _provider.GetRepository<Cuisine>().GetAll().List<Cuisine>();
        }

        [TestMethod]
        public void EntitiesMap_DeliveryType()
        {
            var res = _provider.GetRepository<DeliveryType>().GetAll().List<DeliveryType>();
        }

        [TestMethod]
        public void EntitiesMap_DishGroup()
        {
            var res = _provider.GetRepository<DishGroup>().GetAll().List<DishGroup>();
        }

        [TestMethod]
        public void EntitiesMap_District()
        {
            var res = _provider.GetRepository<District>().GetAll().List<District>();
        }

        [TestMethod]
        public void EntitiesMap_Image()
        {
            var res = _provider.GetRepository<Image>().GetAll().Take(5).List<Image>();
        }

        [TestMethod]
        public void EntitiesMap_OpeningHour()
        {
            var res = _provider.GetRepository<OpeningHour>().GetAll().List<OpeningHour>();
        }

        [TestMethod]
        public void EntitiesMap_Opinion()
        {
            var res = _provider.GetRepository<Opinion>().GetAll().List<Opinion>();
        }

        [TestMethod]
        public void EntitiesMap_OrderDish()
        {
            var res = _provider.GetRepository<OrderDish>().GetAll().List<OrderDish>();
        }

        [TestMethod]
        public void EntitiesMap_Order()
        {
            var res = _provider.GetRepository<Order>().GetAll().List<Order>();
        }

        [TestMethod]
        public void EntitiesMap_PaymentHistory()
        {
            var res = _provider.GetRepository<PaymentHistory>().GetAll().List<PaymentHistory>();
        }

        [TestMethod]
        public void EntitiesMap_Payment()
        {
            var res = _provider.GetRepository<Payment>().GetAll().List<Payment>();
        }

        [TestMethod]
        public void EntitiesMap_RestaurantCuisine()
        {
            var res = _provider.GetRepository<RestaurantCuisine>().GetAll().List<RestaurantCuisine>();
        }

        [TestMethod]
        public void EntitiesMap_Restaurant()
        {
            var res = _provider.GetRepository<Restaurant>().GetAll().List<Restaurant>();
        }

        [TestMethod]
        public void EntitiesMap_Street()
        {
            var res = _provider.GetRepository<Street>().GetAll().List<Street>();
        }

        [TestMethod]
        public void EntitiesMap_User()
        {
            var res = _provider.GetRepository<User>().GetAll().List<User>();
        }

        [TestMethod]
        public void EntitiesMap_UserState()
        {
            var res = _provider.GetRepository<UserState>().GetAll().List<UserState>();
        }

        [TestMethod]
        public void EntitiesMap_UserType()
        {
            var res = _provider.GetRepository<UserType>().GetAll().List<UserType>();
        }

        //[TestMethod]
        //public void DodajObrazek()
        //{
        //    using (var stream = new FileStream("D:\\logo.png", FileMode.Open, FileAccess.Read))
        //    {
        //        byte[] imgData = new byte[stream.Length];
        //        stream.Read(imgData, 0, (int)stream.Length);
        //        int id = _provider.GetRepository<Image>().Save<int>(new Image() { ImageData = imgData });

        //    }
        //}
    }
}
