using BlogsiteMobile.Models;
using BlogsiteMobile.Services;
using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Command LoginCommand { get; private set; }
        private string _userName;
        [Required(ErrorMessage = "Username is required.")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string _password;
        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private async void OnLoginClicked(object obj)
        {
            if (Validate())
            {
                User user = App.UserRepository.FindUser(_userName, _password);
                if (user == null)
                {
                    Error= "Account doesn't exist";
                    //await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                   Application.Current.MainPage = new AppShell();
                }
            }
            
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
        }
        private bool Validate()
        {
            var context = new ValidationContext(this, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, context, results, true))
            {
                Error = results.First().ErrorMessage;
                return false;
            }
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
