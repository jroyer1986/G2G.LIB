using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class Address_EP
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Street2 { get; set; }
        public string Street1 { get; set; }
        public string Company { get; set; }

        public Address_EP() { }
        public Address_EP(string id, string company, string street1, string street2, string city, string state, string country, string zip, string phone)
        {
            Id = id;
            City = city;
            Phone = phone;
            Country = country;
            Zip = zip;
            State = state;
            Street1 = street1;
            Street2 = street2;
            Company = company;
        }
        public string FormattedParameterString()
        {
            string formattedString = "";

            int counter = 0;
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.Name != "Id" && prop.GetValue(this) != null)
                {
                    if(counter > 0)
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
