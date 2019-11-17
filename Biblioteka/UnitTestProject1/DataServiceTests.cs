﻿using System;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class DataServiceTests
    {
        DataContext context = new DataContext();
        WypelnianieZPliku plik = new WypelnianieZPliku();

        [TestMethod]
        public void TestGetAllKatalog()
        {
          Dictionary<int, Katalog> expectedKatalog = new Dictionary<int, Katalog>();

            expectedKatalog.Add(1, new Katalog("Harry Potter", new AutorKsiazki("J.K", "Rowling"), "Fantasy"));
            expectedKatalog.Add(2, new Katalog("Pan Tadeusz", new AutorKsiazki("Adam", "Mickiewicz"), "Epopeja"));
            expectedKatalog.Add(3, new Katalog("Lalka", new AutorKsiazki("Boleslaw", "Prus"), "Dramat"));

          

  
          DataRepository dataRepository = new DataRepository(context, plik);
           DataService dataService = new DataService(dataRepository);
            Dictionary<int, Katalog> actual = dataService.GetAllKatalog();

          
               Assert.AreEqual(expectedKatalog[1], actual[1]);
            Assert.AreEqual(expectedKatalog[2], actual[2]);
            Assert.AreEqual(expectedKatalog[3], actual[3]);

        }
        [TestMethod]
        public void TestGetAllKatalogByThisClient()
        {

     
            Katalog k1 = new Katalog("Lalka", new AutorKsiazki("Boleslaw", "Prus"), "Dramat");
            Katalog k2 = new Katalog("Pan Tadeusz", new AutorKsiazki("Adam", "Mickiewicz"), "Epopeja");
            HashSet<Katalog> expectedHashSetKatalog = new HashSet<Katalog>();
            expectedHashSetKatalog.Add(k1);
            expectedHashSetKatalog.Add(k2);



            DataRepository dataRepository = new DataRepository(context, plik);
            DataService dataService = new DataService(dataRepository);
            HashSet<Katalog> actual = dataService.GetAllKatalogBuyByThisClient(1);


            Assert.AreEqual(expectedHashSetKatalog.SetEquals(actual),true);
           
            

        }
        [TestMethod]
        public void TestGetAllKatalogByThisClientAndThisDay()
        {

            Katalog k2 = new Katalog("Pan Tadeusz", new AutorKsiazki("Adam", "Mickiewicz"), "Epopeja");
            HashSet<Katalog> expectedHashSetKatalog = new HashSet<Katalog>();
            expectedHashSetKatalog.Add(k2);
        
            DataRepository dataRepository = new DataRepository(context, plik);
            DataService dataService = new DataService(dataRepository);
            HashSet<Katalog> actual = dataService.GetAllKatalogBuyByThisClientAndThisDay(1, DateTime.Parse("11.10.2018"));


            Assert.AreEqual(expectedHashSetKatalog.SetEquals(actual), true);

        }
        
             [TestMethod]
        public void TestGetAllZdarzeniaThisDay()
        {
            ObservableCollection<Zdarzenie> expectedZdarzenia = new ObservableCollection<Zdarzenie>();
            

            DataRepository dataRepository = new DataRepository(context, plik);
            DataService dataService = new DataService(dataRepository);

            Zdarzenie zd1 = new Sprzedaz(4, dataRepository.GetOpisStanuById(1), 1, 1, dataRepository.GetClientById(1), DateTime.Parse("11.10.2018"));
            expectedZdarzenia.Add(zd1);

            ObservableCollection<Zdarzenie> actual = dataService.GetAllZdarzeniaThisDay(DateTime.Parse("11.10.2018"));


            Assert.AreEqual(expectedZdarzenia.SequenceEqual(actual), true);

        }
        
                [TestMethod]
        public void TestGetAllZdarzeniaThisClient()
        {
            ObservableCollection<Zdarzenie> expectedZdarzenia = new ObservableCollection<Zdarzenie>();


            DataRepository dataRepository = new DataRepository(context, plik);
            DataService dataService = new DataService(dataRepository);
           
            Zdarzenie zd1 = new Sprzedaz(4, dataRepository.GetOpisStanuById(1), 1, 1, dataRepository.GetClientById(1), DateTime.Parse("11.10.2018"));
            Zdarzenie zd2 = new Sprzedaz(5, dataRepository.GetOpisStanuById(2), 1, 1, dataRepository.GetClientById(1), DateTime.Parse("12.10.2018"));
            expectedZdarzenia.Add(zd1);
            expectedZdarzenia.Add(zd2);

            ObservableCollection<Zdarzenie> actual = dataService.GetAllZdarzeniaThisClient(1);


            Assert.AreEqual(expectedZdarzenia.SequenceEqual(actual),true);


        }
    }
}