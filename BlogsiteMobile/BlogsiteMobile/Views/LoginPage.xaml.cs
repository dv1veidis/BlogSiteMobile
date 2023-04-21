using BlogsiteMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.Properties["IsLoggedIn"] = false;
            App.Current.SavePropertiesAsync();
        }
        private async void OnLabelClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
        public LoginPage()
        {
            try
            {  
                InitializeComponent();
                this.BindingContext = new LoginViewModel();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}