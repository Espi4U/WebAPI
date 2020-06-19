using FamilyFinance.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Behaviors
{
    public class EmptyEntryValidatorBehavior : Behavior<ExEntry>
    {
        ExEntry control;
        string _placeHolder;
        Xamarin.Forms.Color _placeHolderColor;

        protected override void OnAttachedTo(ExEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            bindable.PropertyChanged += OnPropertyChanged;
            control = bindable;
            _placeHolder = bindable.Placeholder;
            _placeHolderColor = bindable.PlaceholderColor;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                ((ExEntry)sender).IsBorderErrorVisible = false;
            }
        }

        protected override void OnDetachingFrom(ExEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            bindable.PropertyChanged -= OnPropertyChanged;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ExEntry.IsBorderErrorVisibleProperty.PropertyName && control != null)
            {
                if (control.IsBorderErrorVisible)
                {
                    control.Placeholder = control.ErrorText;
                    control.PlaceholderColor = control.BorderErrorColor;
                    control.Text = string.Empty;
                }

                else
                {
                    control.Placeholder = _placeHolder;
                    control.PlaceholderColor = _placeHolderColor;
                }

            }
        }
    }
}
