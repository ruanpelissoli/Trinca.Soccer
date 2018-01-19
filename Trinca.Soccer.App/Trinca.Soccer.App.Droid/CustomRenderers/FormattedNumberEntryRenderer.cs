﻿using System.ComponentModel;
using Android.Content;
using Android.Text;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.CustomControls;
using Trinca.Soccer.App.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FormattedNumberEntry), typeof(FormattedNumberEntryRenderer))]
namespace Trinca.Soccer.App.Droid.CustomRenderers
{

    public class FormattedNumberEntryRenderer : EntryRenderer
    {
        public FormattedNumberEntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.AfterTextChanged -= Control_AfterTextChanged;
            }
            if (e.NewElement != null)
            {
                Control.AfterTextChanged += Control_AfterTextChanged;
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

        void Control_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
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
            var change = oldText.Length - newText.Length;

            // 8. Set the new cursor position
            Control.SetSelection(cursorPosition - change);

            // 9. Start listening for changes on our control’s Text property
            element.ShouldReactToTextChanges = true;
        }
    }
}