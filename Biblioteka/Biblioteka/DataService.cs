﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Biblioteka
{
    public class DataService
    {
       // private DataRepository dataRepository1;

        internal IDataRepository dataRepository { get; set; }

     //   internal DataService(IDataRepository dataRepository) => this.dataRepository = dataRepository;

        public DataService(DataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public Dictionary<int, Katalog> GetAllKatalog()
        {
            return dataRepository.GetAllKatalog();
        }
        private List<Client> GetAllClient()
        {
            return dataRepository.GetAllClient();
        }
        public ObservableCollection<Zdarzenie> GetAllZdarzenia()
        {
            return dataRepository.GetAllZdarzenia();
        }
        public List<OpisStanu> GetAllOpisStanu()
        {
            return dataRepository.GetAllOpisStanu();
        }

        public void AddClient(Client client)
        {
            dataRepository.AddClient(client);
        }

        // utworzenie katalogu tworzy tez opis stanu
        public void AddKatalog(Katalog katalog, uint cenaKatalogu)
        {
            dataRepository.AddKatalog(katalog);
            int myKey = 0;
            foreach(KeyValuePair<int,Katalog> kat in dataRepository.GetAllKatalog())
            {
                if (kat.Value.Equals(katalog))
                {
                    myKey = kat.Key;
                }
            }
            
            
            dataRepository.AddOpisStanu(new OpisStanu(myKey, katalog, 0, cenaKatalogu));
        }

        // zdarzenie zmienia tez opis stanu ( dodaje ilosc egzemplarzy jesli kupujemy, zmiejsza ilosc egzemplarzy jesli sprzedajemy)
        public void AddZdarzenie(Zdarzenie zdarzenie)
        {
            dataRepository.AddZdarzenie(zdarzenie);
            OpisStanu updatedOpisStanu = new OpisStanu(zdarzenie.opisStanu.opisuStanuId, zdarzenie.opisStanu.katalog, zdarzenie.opisStanu.iloscEgzemplarzy + zdarzenie.GetIlosc(), zdarzenie.opisStanu.cena);
            dataRepository.UpdateOpisStanu(zdarzenie.opisStanu.opisuStanuId, updatedOpisStanu);
        }

        public ObservableCollection<Zdarzenie> GetAllZdarzeniaThisClient(int clientID)
        {

            ObservableCollection<Zdarzenie> zdarzenies = dataRepository.GetAllZdarzenia();
            ObservableCollection<Zdarzenie> resultZdarzenies = new ObservableCollection<Zdarzenie>();
            foreach (Zdarzenie zdarzenie in zdarzenies)
            {
                if(zdarzenie.GetClient().ClientId == clientID)
                {
                    resultZdarzenies.Add(zdarzenie);
                }
            }
            return resultZdarzenies;
        }

        public ObservableCollection<Zdarzenie> GetAllZdarzeniaThisDay(DateTime dateTime)
        {
            ObservableCollection<Zdarzenie> zdarzenies = dataRepository.GetAllZdarzenia();
            ObservableCollection<Zdarzenie> resultZdarzenies = new ObservableCollection<Zdarzenie>();
            foreach (Zdarzenie zdarzenie in zdarzenies)
            {
                if (zdarzenie.GetDateZdarzenia().Day.Equals(dateTime.Day))
                {
                    resultZdarzenies.Add(zdarzenie);
                }
            }
            return resultZdarzenies;
        }

        public HashSet<Katalog> GetAllKatalogBuyByThisClient(int clientID)
        {

            ObservableCollection<Zdarzenie> zdarzeniesThisClient = GetAllZdarzeniaThisClient(clientID);
            HashSet<Katalog> resultKatalog = new HashSet<Katalog>();
            foreach (Zdarzenie zdarzenie in zdarzeniesThisClient)
            {
                resultKatalog.Add(findKatalogByZdarzenie(zdarzenie));
            }
            return resultKatalog;
        }

        public HashSet<Katalog> GetAllKatalogBuyByThisClientAndThisDay(int clientID,DateTime dateTime)
        {

            ObservableCollection<Zdarzenie> zdarzeniesThisClient = GetAllZdarzeniaThisClient(clientID);
            ObservableCollection<Zdarzenie> zdarzeniesThisDay = GetAllZdarzeniaThisDay(dateTime);
            HashSet<Katalog> resultKatalog = new HashSet<Katalog>();
            foreach (Zdarzenie zdarzenieClient in zdarzeniesThisClient)
            {
                foreach(Zdarzenie zdarzenieDay in zdarzeniesThisDay)
                {
                    if (zdarzenieDay.zdarzeniaId.Equals(zdarzenieClient.zdarzeniaId))
                    {
                        resultKatalog.Add(findKatalogByZdarzenie(zdarzenieDay));
                    }
                }
                        

            }
            return resultKatalog;
        }

        public List<String> GetEventChange()
        {
            return dataRepository.GetEventChangeList();
        }

        private Katalog findKatalogByZdarzenie(Zdarzenie zdarzenie)
        {
            return zdarzenie.opisStanu.katalog;
        }
      

    }
}
