using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NearByMe
{
    public partial class App : Application
    {
        public const string API_Key = "AIzaSyDkp7eNYThyLFBoT3la-sPzg_W1sW4YtCE"; //"AIzaSyBhtWZGad3_yprj93smYPpmosIdT7bR8_A";
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new  MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
