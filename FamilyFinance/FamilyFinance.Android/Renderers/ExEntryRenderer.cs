using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FamilyFinance.Controls;
using FamilyFinance.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExEntry), typeof(ExEntryRenderer))]
namespace FamilyFinance.Droid.Renderers
{
	public class ExEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control == null || e.NewElement == null) return;

			UpdateBorders();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control == null) return;

			if (e.PropertyName == ExEntry.IsBorderErrorVisibleProperty.PropertyName)
				UpdateBorders();
		}

		void UpdateBorders()
		{
			GradientDrawable shape = new GradientDrawable();
			shape.SetShape(ShapeType.Rectangle);
			shape.SetCornerRadius(0);

			if (((ExEntry)this.Element).IsBorderErrorVisible)
			{
				shape.SetStroke(3, ((ExEntry)this.Element).BorderErrorColor.ToAndroid());
			}
			else
			{
				shape.SetStroke(3, Android.Graphics.Color.LightGray);
				this.Control.SetBackground(shape);
			}

			this.Control.SetBackground(shape);
        }
	}
}