using System.Reflection;
using Trinca.Soccer.App.UWP.Renderers;
using Trinca.Soccer.App.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace Trinca.Soccer.App.UWP.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private CommandBar _commandBar;

        public CustomNavigationPageRenderer()
        {
            ElementChanged += OnElementChanged;
        }

        private void OnElementChanged(object sender, VisualElementChangedEventArgs e)
        {
            ElementChanged -= OnElementChanged;
            ContainerElement.Loaded += OnContainerElementLoaded;
        }

        private void OnContainerElementLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ContainerElement.Loaded -= OnContainerElementLoaded;
            _commandBar = typeof(PageControl).GetTypeInfo().GetDeclaredField("_commandBar").GetValue(ContainerElement) as CommandBar;
            var commandBarContent = ((Border)_commandBar.Content);
            var textBlock = commandBarContent.Child as TextBlock;

            if (textBlock != null)
            {
                textBlock.FontFamily = new FontFamily("/Assets/Rubik-Medium.ttf#Rubik-Medium");
            }
        }
    }
}
