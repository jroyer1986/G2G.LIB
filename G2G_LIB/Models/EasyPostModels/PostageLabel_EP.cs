using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class PostageLabel_EP
    {
        public string Id { get; set; }
        public string Label_Url { get; set; }

        public PostageLabel_EP() { }
        public PostageLabel_EP(string id, string labelUrl)
        {
            Id = id;
            Label_Url = labelUrl;
        }
    }
}
