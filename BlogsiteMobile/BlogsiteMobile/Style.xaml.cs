using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogsiteMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Style : ResourceDictionary
    {
        public Style()
        {
            InitializeComponent();
        }
    }
}