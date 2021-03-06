﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Biblioteka
{
    public class DataRepository : IDataRepository
    {
        private DataContext data { get; set; }

        public DataRepository(DataContext dataContext, Fill fill)
        {
           
            this.data = dataContext;
            fill.fillIn(dataContext);
            data.zdarzenieObservableCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler
(CollectionChangedMethod);

        }

        public void clearData()
        {
            data = new DataContext();
        }

        public DataRepository(DataContext data)
        {
            this.data = data;
            data.zdarzenieObservableCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler
(CollectionChangedMethod);
        }
        //C.R.U.D ADD GET ALL GET UPDATE DELETE

        public void AddClient(Client client)
        {
            data.clientList.Add(client);
        }

        public void AddKatalog(Katalog katalog)
        {
            int length = data.dictionaryKatalog.Count;
            data.dictionaryKatalog[length]=katalog;
        }

        public void AddOpisStanu(OpisStanu opisStanu)
        {
            data.opisStanuList.Add(opisStanu);
        }

        public void AddZdarzenie(Zdarzenie zdarzenie)
        {
            data.zdarzenieObservableCollection.Add(zdarzenie);
        }

        public Dictionary<int, Katalog> GetAllKatalog()
        {
            return data.dictionaryKatalog;
        }

        public List<Client> GetAllClient()
        {
            return data.clientList;
        }

        public ObservableCollection<Zdarzenie> GetAllZdarzenia()
        {
            return data.zdarzenieObservableCollection;
        }

        public List<OpisStanu> GetAllOpisStanu()
        {
            return data.opisStanuList;
        }

        public Zdarzenie GetZdarzenieById(int zdarzeniaID)
        {
            foreach (Zdarzenie zd in data.zdarzenieObservableCollection)
            {
                if (zd.zdarzeniaId == zdarzeniaID)
                {
                    return zd;
                }
            }
            return null;
        }

        public Client GetClientById(int clientID)
        {
            foreach (Client cl in data.clientList)
            {
                if (cl.ClientId == clientID)
                {
                    return cl;
                }
            }
            return null;
        }

        public OpisStanu GetOpisStanuById(int opisStanuId)
        {
            foreach (OpisStanu opis in data.opisStanuList)
            {
                if (opis.opisuStanuId == opisStanuId)
                {
                    return opis;
                }
            }
            return null;
        }

        public Katalog GetKatalogById(int katalogId)
        {
            return data.dictionaryKatalog[katalogId];
        }

        public void UpdateClient(int id, Client client)
        {
            for (int i = 0; i < data.clientList.Count; i++)
            {
                if (data.clientList[i].ClientId == id)
                {
                    data.clientList[i] = client;
                }
            }
        }

        public void UpdateKatalog(int id, Katalog katalog)
        {
            data.dictionaryKatalog[id] = katalog;
        }

        public void UpdateOpisStanu(int id, OpisStanu opisStanu)
        {
            for (int i = 0; i < data.opisStanuList.Count; i++)
            {
                if (data.opisStanuList[i].opisuStanuId == id)
                {
                    data.opisStanuList[i] = opisStanu;
                }
            }
        }

        public void UpdateZdarzenie(int id, Zdarzenie zdarzenie)
        {
            for (int i = 0; i < data.zdarzenieObservableCollection.Count; i++)
            {
                if (data.zdarzenieObservableCollection[i].zdarzeniaId == id)
                {
                    data.zdarzenieObservableCollection[i] = zdarzenie;
                }
            }
        }

        public void DeleteClientByID(int id)
        {
            for (int i = 0; i < data.clientList.Count; i++)
            {
                if (data.clientList[i].ClientId == id)
                {
                    data.clientList.Remove(data.clientList[i]);
                }
            }

        }

        public void DeleteKatalogByID(int id)
        {
            data.dictionaryKatalog.Remove(id);
        }

        public void DeleteOpisStanuByID(int id)
        {
            for (int i = 0; i < data.opisStanuList.Count; i++)
            {
                if (data.opisStanuList[i].opisuStanuId == id)
                {
                    data.opisStanuList.Remove(data.opisStanuList[i]);
                }
            }
        }

        public void DeleteZdarzenieByID(int id)
        {
            for (int i = 0; i < data.zdarzenieObservableCollection.Count; i++)
            {
                if (data.zdarzenieObservableCollection[i].zdarzeniaId == id)
                {
                    data.zdarzenieObservableCollection.Remove(data.zdarzenieObservableCollection[i]);
                }
            }
        }
        public List<string> GetEventChangeList()
        {
            return data.eventChangeList;
        }
        public DataContext GetDataContext()
        {
            return this.data;
        }
        public void setClientsList(List<Client> clients)
        {
            data.clientList = clients;
        }
        public void setKatalogDict(Dictionary<int,Katalog> kat)
        {
            data.dictionaryKatalog = kat;
        }
        public void setZdarzeniaList(ObservableCollection<Zdarzenie> zdarzenia)
        {
            data.zdarzenieObservableCollection = zdarzenia;
        }
        public void setOpisStanuList(List<OpisStanu> opisStanuList)
        {
            data.opisStanuList = opisStanuList;
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
           
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {               
                data.eventChangeList.Add("Dodano nowe wydarzenie");
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                data.eventChangeList.Add("Nadpisano wydarzenie");
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                data.eventChangeList.Add("Usunieto wydarzenie");
            }
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                data.eventChangeList.Add("Przeniesiono wydarzenie");
            }
        }



    }
}