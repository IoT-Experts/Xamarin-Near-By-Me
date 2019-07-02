using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using NearByMe.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NearByMe.Services
{
    public class NearByService
    {
        public NearByService()
        {
        }
        public static async Task<List<Place>> GetNearByPlacesAsync(Xamarin.Essentials.Location location, string Type)
        {
            RootObject rootObject = null;
            var client = new HttpClient();
            CultureInfo In = new CultureInfo("en-IN");
            //string latitude = "21.202305";//position.Latitude.ToString(In);
            //string longitude = "72.801437";//position.Longitude.ToString(In);
            //string search = "hospital";
            //string Api_Key = "AIzaSyAm9ja67DHCyiQyzdbZow0d1XFQ8syh0LA"; // "AIzaSyDkp7eNYThyLFBoT3la-sPzg_W1sW4YtCE"; //"AIzaSyAm9ja67DHCyiQyzdbZow0d1XFQ8syh0LA"; //"AIzaSyB9aXHKKB_wIah3UQKybWzZak7RRoM1-FI";

        //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=21.2125959,72.8004598&radius=1500&type=restaurant&key=AIzaSyB9aXHKKB_wIah3UQKybWzZak7RRoM1-FI

            string restUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + ChangeLocationformat(location.Latitude.ToString()) + "," + ChangeLocationformat(location.Longitude.ToString()) + "&radius=150000&type=" + Type.ToLower() + "&key=" + App.API_Key;
            var uri = new Uri(restUrl);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                rootObject = JsonConvert.DeserializeObject<RootObject>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No web response", "Unable to retrieve information, please try again", "OK");
            }

            return rootObject.results;
        }

        public static string ChangeLocationformat(string location)
        {
            return location.Replace(',', '.');
        }

        public static async Task<PlaceDetail> GetPlacesDetailAsync(string placeid)
        {
            PlaceDetailRootObject rootObject = null;
            var client = new HttpClient();
            CultureInfo In = new CultureInfo("en-IN");

            //string Api_Key = "AIzaSyAm9ja67DHCyiQyzdbZow0d1XFQ8syh0LA"; //"AIzaSyB9aXHKKB_wIah3UQKybWzZak7RRoM1-FI";


            string restUrl = $"https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeid +  "&key=" + App.API_Key;
            var uri = new Uri(restUrl);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                rootObject = JsonConvert.DeserializeObject<PlaceDetailRootObject>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No web response", "Unable to retrieve information, please try again", "OK");
            }
            if (rootObject != null)
                return rootObject.result;
            else
                return null;
        }
    }

}
