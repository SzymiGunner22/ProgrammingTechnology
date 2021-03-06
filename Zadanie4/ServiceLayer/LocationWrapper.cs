﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class LocationWrapper
    {
        private Location location;

        public LocationWrapper(Location location)
        {
            this.location = location;
        }

        internal Location getLocation()
        {
            return this.location;
        }
        public short LocationID
        {
            get
            {
                return location.LocationID;
            }
        }

        public string Name
        {
            get
            {
                return location.Name;
            }
            set
            {
                location.Name = value;
            }
        }

        public decimal CostRate
        {
            get
            {
                return location.CostRate;
            }
            set
            {
                location.CostRate = value;
            }
        }

        public decimal Availability
        {
            get
            {
                return location.Availability;
            }
            set
            {
                location.Availability = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return location.ModifiedDate;
            }
            set
            {
                location.ModifiedDate = value;
            }
        }





       
    }
}
