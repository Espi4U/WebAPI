using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Controls
{
	public class ExEntry : Entry
	{
		public static readonly BindableProperty IsBorderErrorVisibleProperty =
			BindableProperty.Create(nameof(IsBorderErrorVisible), typeof(bool), typeof(ExEntry), false, BindingMode.TwoWay);

		public bool IsBorderErrorVisible
		{
			get { return (bool)GetValue(IsBorderErrorVisibleProperty); }
			set
			{
				SetValue(IsBorderErrorVisibleProperty, value);
			}
		}

		public static readonly BindableProperty BorderErrorColorProperty =
			BindableProperty.Create(nameof(BorderErrorColor), typeof(Xamarin.Forms.Color), typeof(ExEntry), Xamarin.Forms.Color.Transparent, BindingMode.TwoWay);

		public Xamarin.Forms.Color BorderErrorColor
		{
			get { return (Xamarin.Forms.Color)GetValue(BorderErrorColorProperty); }
			set
			{
				SetValue(BorderErrorColorProperty, value);
			}
		}

		public static readonly BindableProperty ErrorTextProperty =
		BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(ExEntry), string.Empty);

		public string ErrorText
		{
			get { return (string)GetValue(ErrorTextProperty); }
			set
			{
				SetValue(ErrorTextProperty, value);
			}
		}
	}
}
