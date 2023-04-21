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

        public NewBlogViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(title);
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
            BlogPost blogPost = new BlogPost()
            {
                BlogPostTitle = BlogPostTitle,
                Text = Text,
            };

            if(1 == await BlogPostStore.AddBlogPost(blogPost))
            {
                await Shell.Current.GoToAsync("..");

            }
            else
            {
                Console.WriteLine("Sadness");
            }
            // This will pop the current page off the navigation stack
        }
    }
}
