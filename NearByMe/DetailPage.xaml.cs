using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NearByMe.Models;
using NearByMe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NearByMe
{
    public partial class DetailPage : ContentPage
    {
        public Place place { get; set; }
        public DetailPage(Place PlaceDetail)
        {
            InitializeComponent();
            //ObservableCollection<FoodImages> imageSources = new ObservableCollection<FoodImages>();
            //imageSources.Add(new FoodImages("Food1"));
            //imageSources.Add(new FoodImages("Food2"));
            //imageSources.Add(new FoodImages("Food3"));
            //imageSources.Add(new FoodImages("Food4"));


            place = PlaceDetail;
            GetPLaceDetail(place.place_id);
            var CurrentPosition = new Position(PlaceDetail.geometry.location.lat, PlaceDetail.geometry.location.lng);

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(CurrentPosition, Distance.FromMeters(500)));
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = CurrentPosition,
                Label = "custom pin",
                Address = "custom detail info"
            };
            MyMap.Pins.Add(pin);
        }

        public async void GetPLaceDetail(string place_id)
        {
            var result = await NearByService.GetPlacesDetailAsync(place_id);
            if(result != null)
            {
                imgSlider.ItemsSource = result.photos;
            }
        }

        void Back_Tapped(object sender, System.EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
    public class FoodImages
    {
        public FoodImages(string name)
        {
            ImageName = name;
        }
        public string ImageName { get; set; }
    }
}
