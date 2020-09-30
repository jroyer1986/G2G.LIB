using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class Message_EP
    {
        public string Carrier { get; set; }
        public string Carrier_Account_Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public Message_EP() { }
        public Message_EP(string carrier, string carrierAccountId, string type, string message)
        {
            Carrier = carrier;
            Carrier_Account_Id = carrierAccountId;
            Type = type;
            Message = message;
        }
    }
}
