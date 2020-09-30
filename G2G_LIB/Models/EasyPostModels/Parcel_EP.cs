using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace G2G_LIB.Models
{
    public class Parcel_EP
    {
        public string Id { get; set; }
        public string Predefined_Package { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public Parcel_EP() { }
        public Parcel_EP(string id, string predefinedPackage, string weight)
        {
            Id = id;
            Predefined_Package = predefinedPackage;
            Weight = weight;
        }
        public string FormattedParameterString()
        {
            string formattedString = "";

            if(Predefined_Package == null)
            {
                formattedString = "parcel[weight]=" + Weight + "&parcel[length]=" + Length + "&parcel[width]=" + Width + "&parcel[height]=" + Height;
            }
            else
            {
                formattedString = "parcel[weight]=" + Weight + "&parcel[predefined_package]=" + Predefined_Package;
            }


            //int counter = 0;
            //foreach (PropertyInfo prop in this.GetType().GetProperties())
            //{
            //    if (prop.Name != "Id" && prop.GetValue(this) != null)
            //    {
            //        if (counter > 0)
            //        {
            //            string propString = "&parcel[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
            //            formattedString += propString;
            //        }
            //        else
            //        {
            //            string propString = "parcel[" + prop.Name.ToLower() + "]=" + prop.GetValue(this);
            //            formattedString += propString;
            //        }
            //        counter++;
            //    }
            //}
            return formattedString;
        }
    }
}
