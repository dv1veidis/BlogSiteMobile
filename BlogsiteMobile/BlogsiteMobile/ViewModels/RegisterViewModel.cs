using BlogsiteMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using BlogsiteMobile.Models;
using BlogsiteMobile.Services;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GalaSoft.MvvmLight.Views;

namespace BlogsiteMobile.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public RegisterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

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

        private string _email;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
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

        public ICommand RegisterCommand { get; private set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(Register);
        }

        private async void Register()
        {
            if (Validate())
            {
                 if(App.UserRepository.getUserInfo(UserName, Email))
                {
                    User user = new User();
                    user.Name = UserName;
                    user.Email = Email;
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);
                    byte[] hashedPasswordBytes = new SHA256Managed().ComputeHash(passwordBytes);
                    string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
                    user.Password = hashedPassword;

                    if(App.UserRepository.CreateUser(user) == 1)
                    {
                       await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                    }
                }
            }
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

