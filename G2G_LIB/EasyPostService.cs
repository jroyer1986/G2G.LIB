using G2G_LIB.Global;
using G2G_LIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB
{
    public class EasyPostService
    {
        static HttpClient client = new HttpClient();

        public static async Task<string> GetShippingLabelURL(string absenceId, Address_EP myToAddress)
        {
            string shippingLabelUrl = "";

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                Address_EP toAddress;
                Address_EP fromAddress;
                Parcel_EP parcel;
                Shipment_EP shipment;

                absenceId = Helpers.ParseStringToRemoveBrackets(absenceId);
                Absence _absenceService = new Absence();

                string username = GlobalVars.GetEasyPostApiKey();
                string password = "";

                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                if (client.BaseAddress != null)
                {
                    client.Dispose();
                    client = new HttpClient();
                }
                client.BaseAddress = new Uri("https://api.easypost.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);

                //CREATE TO ADDRESS
                try
                {
                    Uri url = await CreateAddressAsync(myToAddress);
                    string addressId = url.OriginalString.Remove(0, url.OriginalString.IndexOf("addresses"));

                    toAddress = await GetAddressByIdAsync(addressId);
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    Exception e = new Exception(ex.Message + " / Failed to create To Address");
                    throw e;
                }

                //CREATE FROM ADDRESS
                try
                {
                    Address_EP address = new Address_EP
                    {
                        Company = "DEALSHIELD RETURNS DEPARTMENT",
                        Street1 = "2002 SUMMIT BLVD",
                        Street2 = "STE 1000",
                        City = "BROOKHAVEN",
                        State = "GA",
                        Zip = "30319",
                        Country = "US",
                        Phone = "855-246-5556"
                    };

                    var url = await CreateAddressAsync(address);
                    string addressId = url.OriginalString.Remove(0, url.OriginalString.IndexOf("addresses"));

                    fromAddress = await GetAddressByIdAsync(addressId);
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    Exception e = new Exception(ex.Message + " / Failed to create From Address");
                    throw e;
                }

                //CREATE PARCEL
                try
                {
                    Parcel_EP newParcel = new Parcel_EP()
                    {
                        Weight = "3",
                        Predefined_Package = "FedExEnvelope"
                    };

                    //FOR TESTING
                    //Parcel_EP newParcel = new Parcel_EP()
                    //{
                    //    Weight = "3",
                    //    Width = "8",
                    //    Length = "11",
                    //    Height = ".25"
                    //};

                    var url = await CreateParcelAsync(newParcel);
                    string parcelId = url.OriginalString.Remove(0, url.OriginalString.IndexOf("parcels"));

                    parcel = await GetParcelByIdAsync(parcelId);
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    Exception e = new Exception(ex.Message + " / Failed to create Parcel");
                    throw e;
                }

                //CREATE SHIPMENT
                try
                {
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("label_format", "PDF");

                    Shipment_EP newShipment = new Shipment_EP()
                    {
                        Parcel = parcel,
                        To_Address = toAddress,
                        From_Address = fromAddress,
                        Reference = "ShipmentRef",
                        Options = options
                    };

                    var url = await CreateShipmentAsync(newShipment);
                    string shipmentId = url.OriginalString.Remove(0, url.OriginalString.IndexOf("shipments"));

                    shipment = await GetShipmentByIdAsync(shipmentId);

                    if (shipment != null && shipment.Rates.Count > 0)
                    {
                        //search shipment for cheapest rate
                        ShipmentRate_EP cheapestRate = shipment.GetLowestRate("FedEx", "STANDARD_OVERNIGHT");
                        if (cheapestRate != null)
                        {
                            try
                            {
                                //buy rate
                                Shipment_EP puchasedShipment = await BuyShipmentRate(shipment.Id, cheapestRate);
                                shippingLabelUrl = puchasedShipment.Postage_Label.Label_Url;

                                client.Dispose();

                                _absenceService.UpdateDisbursementShippingLabel(shippingLabelUrl, absenceId);
                            }
                            catch (Exception ex)
                            {
                                client.Dispose();
                                Exception e = new Exception(ex.Message + " / Failed to buy Postage Label");
                                throw e;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    Exception e = new Exception(ex.Message + " / Failed to create Shipment");
                    throw e;
                }
            }
            catch (Exception ex)
            {
                client.Dispose();
                Exception e = new Exception(ex.Message + " / Something went wrong.");
                throw e;
            }
            finally
            {
                client.Dispose();
            }

            client.Dispose();
            return shippingLabelUrl;
        }

        #region CREATE
        static async Task<Uri> CreateAddressAsync(Address_EP newAddress)
        {
            string mystring = newAddress.FormattedParameterString();
            HttpResponseMessage response = await client.PostAsync("addresses?" + mystring, null);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var address = await response.Content.ReadAsAsync<Address_EP>();
                //var address = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Uri> CreateParcelAsync(Parcel_EP newParcel)
        {
            string myString = newParcel.FormattedParameterString();
            HttpResponseMessage response = await client.PostAsync("parcels?" + myString, null);
            response.EnsureSuccessStatusCode();

            if(response.IsSuccessStatusCode)
            {
                // Parse the response body
                var savedParcel = await response.Content.ReadAsAsync<Parcel_EP>();
                //var savedParcel = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return response.Headers.Location;
        }

        static async Task<Uri> CreateShipmentAsync(Shipment_EP newShipment)
        {
            string myString = newShipment.FormattedParameterString();
            HttpResponseMessage response = await client.PostAsync("shipments?" + myString, null);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var savedShipment = await response.Content.ReadAsAsync<Shipment_EP>();
                //var savedShipment = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return response.Headers.Location;
        }
        #endregion

        #region GET
        static async Task<Address_EP> GetAddressByIdAsync(string path)
        {
            Address_EP address = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                address = await response.Content.ReadAsAsync<Address_EP>();
                //address = await response.Content.ReadAsStringAsync();

            }
            return address;
        }

        static async Task<Parcel_EP> GetParcelByIdAsync(string path)
        {
            Parcel_EP parcel = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                parcel = await response.Content.ReadAsAsync<Parcel_EP>();
            }
            return parcel;
        }

        static async Task<Shipment_EP> GetShipmentByIdAsync(string path)
        {
            Shipment_EP shipment = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                shipment = await response.Content.ReadAsAsync<Shipment_EP>();
            }
            return shipment;
        }
        #endregion

        #region BUY
        static async Task<Shipment_EP> BuyShipmentRate(string shipmentId, ShipmentRate_EP rate)
        {
            Shipment_EP shipment = null;
            HttpResponseMessage response = await client.PostAsync("shipments/" + shipmentId + "/buy?rate[id]=" + rate.Id, null);
            if (response.IsSuccessStatusCode)
            {
                shipment = await response.Content.ReadAsAsync<Shipment_EP>();
            }
            return shipment;
        }
        #endregion
    }
}
