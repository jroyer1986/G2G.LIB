using G2G_LIB.Models;
using G2G_LIB.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TestApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Address_EP address = new Address_EP
            {
                Company = "JUSTIN ROYER",
                Street1 = "1905 ANNIN STREET",
                City = "PHILADELPHIA",
                State = "PA",
                Zip = "19146",
                Country = "US",
                Phone = "111-222-3333"
            };
            string url = await G2G_LIB.EasyPostService.GetShippingLabelURL("asdf", address);
            Console.WriteLine(url);
            Console.ReadLine();

            //G2G_LIB.EasyPostService.Main();
        }
    }
}
