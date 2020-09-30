using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class Shipment_EP
    {
        public string Id { get; set; }
        public Address_EP From_Address { get; set; }
        public Address_EP To_Address { get; set; }
        public Parcel_EP Parcel { get; set; }
        public string Reference { get; set; }
        public Dictionary<string, object> Options { get; set; }
        public List<ShipmentRate_EP> Rates { get; set; }
        public List<Message_EP> Messages { get; set; }
        public PostageLabel_EP Postage_Label { get; set; }

        public Shipment_EP()
        {

        }
        public Shipment_EP(string id, Address_EP fromAddress, Address_EP toAddress, Parcel_EP parcel, string reference, List<ShipmentRate_EP> rates, List<Message_EP> messages, PostageLabel_EP postageLabel, Dictionary<string, object> options = null)
        {
            Id = id;
            From_Address = fromAddress;
            To_Address = toAddress;
            Parcel = parcel;
            Reference = reference;
            Options = options;
            Rates = rates;
            Messages = messages;
            Postage_Label = postageLabel;
        }

        public ShipmentRate_EP GetLowestRate(string carrier, string service)
        {
            ShipmentRate_EP cheapestRate = null;

            foreach (ShipmentRate_EP rate in Rates)
            {
                if(rate.Carrier == carrier)
                {
                    if(rate.Service == service)
                    {
                        if(cheapestRate == null)
                        {
                            cheapestRate = rate;
                        }
                        else
                        {
                            if(rate.Rate < cheapestRate.Rate)
                            {
                                cheapestRate = rate;
                            }
                        }
                    }
                }
            }

            cheapestRate = Rates.FirstOrDefault();

            return cheapestRate;
        }
        public string FormattedParameterString()
        {
            string formattedString = "";

            int counter = 0;
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.Name != "Id" && prop.Name != "Messages" && prop.Name != "Postage_Label" && prop.GetValue(this) != null)
                {
                    //handle from_address.Id
                    if(prop.Name == "From_Address")
                    {
                        if (counter > 0)
                        {
                            string propString = "&shipment[from_address][id]=" + From_Address.Id;
                            formattedString += propString;
                        }
                        else
                        {
                            string propString = "shipment[from_address][id]=" + From_Address.Id;
                            formattedString += propString;
                        }
                    }
                    //handle to_address.Id
                    else if(prop.Name == "To_Address")
                    {
                        if (counter > 0)
                        {
                            string propString = "&shipment[to_address][id]=" + To_Address.Id;
                            formattedString += propString;
                        }
                        else
                        {
                            string propString = "shipment[to_address][id]=" + To_Address.Id;
                            formattedString += propString;
                        }
                    }
                    //handle parcel.Id
                    else if(prop.Name == "Parcel")
                    {
                        if (counter > 0)
                        {
                            string propString = "&shipment[parcel][id]=" + Parcel.Id;
                            formattedString += propString;
                        }
                        else
                        {
                            string propString = "shipment[parcel][id]=" + Parcel.Id;
                            formattedString += propString;
                        }
                    }
                    //handle dictionary
                    else if(prop.Name == "Options")
                    {
                        foreach(var option in Options)
                        {
                            if (counter > 0)
                            {
                                string propString = "&shipment[options][" + option.Key.ToLower() + "]=" + option.Value;
                                formattedString += propString;
                            }
                            else
                            {
                                string propString = "shipment[options][" + option.Key.ToLower() + "]=" + option.Value;
                                formattedString += propString;
                            }
                        }
                    }
                    //handle list
                    else if(prop.Name == "Rates")
                    {

                    }
                    //handle remainder
                    else
                    {
                        if (counter > 0)
                        {
                            string propString = "&shipment[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
                            formattedString += propString;
                        }
                        else
                        {
                            string propString = "shipment[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
                            formattedString += propString;
                        }
                    }
                    counter++;
                }
            }
            return formattedString;
        }
    }
}
