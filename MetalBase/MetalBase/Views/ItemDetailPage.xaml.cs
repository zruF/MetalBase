using MetalBase.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MetalBase.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}