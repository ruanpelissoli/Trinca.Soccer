using System.Windows.Input;
using Xamarin.Forms;

namespace Trinca.Soccer.App.CustomControls
{
    public class ListView : Xamarin.Forms.ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(nameof(ItemTappedCommand),
                typeof(ICommand),
                typeof(ListView));

        public ICommand ItemTappedCommand
        {
            get => (ICommand)GetValue(ItemTappedCommandProperty);
            set => SetValue(ItemTappedCommandProperty, value);
        }

        public ListView()
        {
            Initialize();
        }

        public ListView(ListViewCachingStrategy cachingStrategy)
            : base(cachingStrategy)
        {
            Initialize();
        }

        private void Initialize()
        {
            ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ItemTappedCommand != null && ItemTappedCommand.CanExecute(null))
                ItemTappedCommand.Execute(e.Item);
        }
    }
}
