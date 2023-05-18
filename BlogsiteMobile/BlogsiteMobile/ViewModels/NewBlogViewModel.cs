using BlogsiteMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlogsiteMobile.ViewModels
{
    public class NewBlogViewModel : BaseViewModel
    {
        private string text;
        private string title;
        private string _selectedItem;

        public NewBlogViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(SelectedItem);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string BlogPostTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            if (App.Current.Properties["UserName"] is string userName && App.Current.Properties["UserId"] is int userId)
            {

                BlogPost blogPost = new BlogPost()
                {
                BlogPostTitle = BlogPostTitle,
                Text = Text,
                Author = (string)App.Current.Properties["UserName"],
                ApplicationUserId = (int)App.Current.Properties["UserId"],
                Category = _selectedItem
                };

            if(1 == await BlogPostStore.AddBlogPost(blogPost))
            {
                await Shell.Current.GoToAsync("..");

            }
            }
            else
            {
                Console.WriteLine("Sadness");
            }
            // This will pop the current page off the navigation stack
        }
    }
}
