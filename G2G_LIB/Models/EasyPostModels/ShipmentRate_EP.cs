using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class ShipmentRate_EP
    {
        public string Id { get; set; }
        public string Carrier { get; set; }
        public string Service { get; set; }
        public decimal Rate { get; set; }

        public ShipmentRate_EP() { }
        public ShipmentRate_EP(string id, string carrier, string service, decimal rate)
        {
            Id = id;
            Carrier = carrier;
            Service = service;
            Rate = rate;
        }
        public string FormattedParameterString()
        {
            string formattedString = "";

            int counter = 0;
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.Name != "Id" && prop.GetValue(this) != null)
                {
                    if (counter > 0)
                    {
                        string propString = "&address[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
                        formattedString += propString;
                    }
                    else
                    {
                        string propString = "address[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
                        formattedString += propString;
                    }
                    counter++;
                }
            }
            return formattedString;
        }
    }
}
