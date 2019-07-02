using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NearByMe.Models;
using NearByMe.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NearByMe
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        ObservableCollection<string> PlaceTypes = new ObservableCollection<string>();
        Xamarin.Essentials.Location location;
        Placemark Address;
        public MainPage()
        {
            InitializeComponent();
            PlaceTypes = new ObservableCollection<string>() { "accounting", "airport", "amusement_park", "aquarium", "art_gallery", "atm", "bakery", "bank", "bar", "beauty_salon", "bicycle_store", "book_store", "bowling_alley", "bus_station", "cafe", "campground", "car_dealer", "car_rental", "car_repair", "car_wash", "casino", "cemetery", "church", "city_hall", "clothing_store", "convenience_store", "courthouse", "dentist", "department_store", "doctor", "electrician", "electronics_store", "embassy", "fire_station", "florist", "funeral_home", "furniture_store", "gas_station", "gym", "hair_care", "hardware_store", "hindu_temple", "home_goods_store", "hospital", "insurance_agency", "jewelry_store", "laundry", "lawyer", "library", "liquor_store", "local_government_office", "locksmith", "lodging", "meal_delivery", "meal_takeaway", "mosque", "movie_rental", "movie_theater", "moving_company", "museum", "night_club", "painter", "park", "parking", "pet_store", "pharmacy", "physiotherapist", "plumber", "police", "post_office", "real_estate_agency", "restaurant", "roofing_contractor", "rv_park", "school", "shoe_store", "shopping_mall", "spa", "stadium", "storage", "store", "subway_station", "supermarket", "synagogue", "taxi_stand", "train_station", "transit_station", "travel_agency", "veterinary_care", "zoo" };
            PlaceTypeList.ItemsSource = PlaceTypes;

            GetCurrentLocationDetail();
        }

        public async Task GetCurrentLocationDetail()
        {
            //Get current Location

            location = await Geolocation.GetLastKnownLocationAsync();

            var placemark = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

            Address = placemark.FirstOrDefault();

            CurrentCity.Text = "Current City : " + Address.Locality + ", " + Address.CountryName;

        }


        void PlaceList_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var PlaceDetail = (Place)e.SelectedItem;

                this.Navigation.PushAsync(new DetailPage(PlaceDetail));
            }
            catch (Exception ex)
            {

            }
        }

        void SelectCategory(object sender, System.EventArgs e)
        {
            CetegoryPopup.IsVisible = !CetegoryPopup.IsVisible;
            BlurBG.IsVisible = !BlurBG.IsVisible;
        }

        //void PlaceList_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    var item = (string)e.SelectedItem;
        //    CategoryName.Text = item;
        //    CategoryName.TextColor = Color.Black;
        //    //throw new NotImplementedException();
        //}

        //void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        //{
        //    var data = e.Item;
        //    //throw new NotImplementedException();
        //}
        async void PlaceType_ItemSelected(object sender, System.EventArgs e)
        {
            Label lblClicked = (Label)sender;
            CategoryName.Text = lblClicked.Text;
            CategoryName.TextColor = Color.Black;
            CetegoryPopup.IsVisible = !CetegoryPopup.IsVisible;
            BlurBG.IsVisible = !BlurBG.IsVisible;

            //var city = Address?.FirstOrDefault().
            //CurrentCity.Text = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location.Latitude + "," + location.Longitude + "&radius=150000&type=" + CategoryName.Text.ToLower();

            var result = await NearByService.GetNearByPlacesAsync(location, CategoryName.Text);

            foreach (var item in result)
            {
                //FInd Distance
                Xamarin.Essentials.Location sourceCoordinates = location;
                Xamarin.Essentials.Location destinationCoordinates = new Xamarin.Essentials.Location(item.geometry.location.lat, item.geometry.location.lng); //new Location(destinationLocations.Latitude, destinationLocations.Longitude);
                double distance = Xamarin.Essentials.Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                item.distance = distance;
            }

            PlaceList.ItemsSource = result;

        }
        void BlurBG_Tapped(object sender, System.EventArgs e)
        {
            CetegoryPopup.IsVisible = !CetegoryPopup.IsVisible;
            BlurBG.IsVisible = !BlurBG.IsVisible;
        }
    }
}
