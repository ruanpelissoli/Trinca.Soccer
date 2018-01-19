using System;
using Xamarin.Forms;

namespace Trinca.Soccer.App.CustomControls
{
    public class FormattedNumberEntry : Entry
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(int), typeof(FormattedNumberEntry), 0);

        public decimal Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public bool ShouldReactToTextChanges { get; set; }

        public FormattedNumberEntry()
        {
            ShouldReactToTextChanges = true;
        }

        public static decimal Parse(string input)
        {
            if (input == null) return 0;

            var number = 0;
            var multiply = 1;

            for (var i = input.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(input[i])) continue;

                number += (input[i] - '0') * (multiply);
                multiply *= 10;
            }
            return number;
        }
    }
}
