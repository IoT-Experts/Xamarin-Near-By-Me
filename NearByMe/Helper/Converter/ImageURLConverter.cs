using System;
using System.Globalization;
using Xamarin.Forms;

namespace NearByMe.Helper.Converter
{
    public class ImageURLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
                return "noimage.png";//"http://denrakaev.com/wp-content/uploads/2015/03/no-image.png";

            var photoURl = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=500&photoreference="+ ((string)value) +"&key=" + App.API_Key;
            return photoURl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
