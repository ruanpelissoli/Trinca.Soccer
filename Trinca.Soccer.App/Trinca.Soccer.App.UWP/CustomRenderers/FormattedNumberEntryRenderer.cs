using System.ComponentModel;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.CustomControls;
using Trinca.Soccer.App.UWP.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(FormattedNumberEntry), typeof(FormattedNumberEntryRenderer))]
namespace Trinca.Soccer.App.UWP.CustomRenderers
{
    public class FormattedNumberEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.TextChanged -= Control_TextChanged;
            }
            if (e.NewElement != null)
            {
                Control.TextChanged += Control_TextChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(FormattedNumberEntry.Value)))
            {
                var element = ((FormattedNumberEntry)Element);
                // 5. Format number, and place the formatted text in newText
                var newText = $"{element.Value.ToString(Strings.DecimalFormat)}";

                // 6. Set the Text property of our control to newText
                Control.Text = newText;

            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }

        }

        private void Control_TextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs e)
        {

            var element = ((FormattedNumberEntry)Element);

            // 1. Stop listening for changes on our control Text property
            if (!element.ShouldReactToTextChanges) return;
            element.ShouldReactToTextChanges = false;

            // 2. Get the current cursor position
            var cursorPosition = Control.SelectionStart;

            // 3. Take the control’s text, lets name it oldText
            var oldText = Control.Text;

            // 4. Parse oldText into a number, lets name it number
            var number = FormattedNumberEntry.Parse(oldText);
            element.Value = number;

            // 5. Format number, and place the formatted text in newText
            var newText = $"{number.ToString(Strings.DecimalFormat)}";

            // 6. Set the Text property of our control to newText
            Control.Text = newText;

            // 7. Calculate the new cursor position
            var change = -1 * (oldText.Length - newText.Length);
            if (cursorPosition + change < 0)
                change = 0;

            // 8. Set the new cursor position
            Control.SelectionStart = cursorPosition + change;

            // 9. Start listening for changes on our control’s Text property
            element.ShouldReactToTextChanges = true;

        }
    }
}
