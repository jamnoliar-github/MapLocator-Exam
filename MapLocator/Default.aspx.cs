using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MapLocator
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Here I used Degha Location as Main Location and Lat Long is : 21.622564, 87.523417
                GLatLng mainLocation = new GLatLng(10.2833322, 123.8999964);
                GMap1.setCenter(mainLocation, 12);

                XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "H", Color.Blue, Color.White, Color.Chocolate);
                GMap1.Add(new GMarker(mainLocation, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));

                List<Restaurant> restaurants = new List<Restaurant>();
                using (RestaurantsDBEntities1 dc = new RestaurantsDBEntities1())
                {
                    restaurants = dc.Restaurants.Where(a => a.Area.Equals("Cebu")).ToList();
                }

                ddlType.Items.Add(new ListItem("Select Restaurant Type", ""));
                ddlType.Items.FindByValue("").Attributes.Add("Disabled", "Disabled");
                

                ShowMap(restaurants, "");
            }
            
        }

        public void ShowMap(List<Restaurant> list, string val)
        {
            PinIcon p;
            GMarker gm;
            GInfoWindow win;

            GMap1.resetInfoWindows();

            
            foreach (var i in list)
            {
                p = new PinIcon(PinIcons.home, Color.Cyan);
                gm = new GMarker(new GLatLng(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude)),
                    new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));

   
                //win = new GInfoWindow(gm, i.Name + " </br>Specialty: " + i.Specialty + "  </br>" + " <a href='" + i.Url + "'>Read more...</a>", false, GListener.Event.mouseover);
                win = new GInfoWindow(gm, i.Name + " </br>Specialty: " + i.Specialty + "  </br>" + " <a href='' id='myid' runat='server' OnClick='ShowDirections(" + i.Latitude + ", " + i.Longitude + ")'>Get Directions </a> ", false, GListener.Event.mouseover);
                
                GMap1.Add(win);
                
                string items = i.Specialty.ToString();

                if (val == "")
                {
                    ddlType.Items.Add(items);
                }
                
                ddlType.Items.FindByValue(val).Attributes.Add("Selected","Selected");
                ddlType.Items.FindByValue("").Attributes.Remove("Text");
                ddlType.Items.FindByValue("").Attributes.Add("Text", "Select ALL");

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = ddlType.SelectedValue;
            
            List<Restaurant> restaurantType = new List<Restaurant>();
            using (RestaurantsDBEntities1 con = new RestaurantsDBEntities1())
            {
                if (type == "")
                {
                    restaurantType = con.Restaurants.Where(a => a.Area.Equals("Cebu")).ToList();
                    ddlType.Items.Clear();
                    ddlType.Items.Add(new ListItem("Select ALL", ""));
                }
                else
                {
                    restaurantType = con.Restaurants.Where(a => a.Specialty.Equals(type)).ToList();
                }
                
            }

            ShowMap(restaurantType, type);
        }

        public void GetDirections(String latitude, String longitude)
        {

            
            GLatLng mainLocation = new GLatLng(10.2833322, 123.8999964);
            GLatLng destination = new GLatLng(Convert.ToDouble(latitude), Convert.ToDouble(longitude));

            mainLocation.distanceFrom(destination);
        }
    }
}