using App2.APIService;
using App2.Model;
using App2.NativeMathods;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.PopUpPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupSettingView : ContentView
    {
        private readonly API _api = new API();
        private NavigationMdl _objNav = null;
        string tag = null;
        public PopupSettingView(string title1Text,string cancel,string expire)//, string title2Text, string entry1PlaceholderValue, string entry2PlaceholderValue, double sliderMinValue, double sliderMaxValue, string saveButtonText, string cancelButtonText)
        {
			InitializeComponent ();
            tag = title1Text;

            //TitleLabel1.Text = title1Text;
            //TitleLabel2.Text = title2Text;
            txtExpireDay.Placeholder = expire;
            txtCancelDay.Placeholder = cancel;
            //AgeSlider.Minimum = sliderMinValue;
            //AgeSlider.Maximum = sliderMaxValue;
            //SaveButton.Text = saveButtonText;
            //CancelButton.Text = cancelButtonText;

            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            txtExpireDay.TextChanged += txtExpireDay_TextChanged;
            txtCancelDay.TextChanged += txtCancelDay_TextChanged;
            //AgeSlider.ValueChanged += InputEntryOnValueChanged;
            MultipleDataResult = new MyDataModel();
        }
        // public event handler to expose 
        // the Save button's click event
        public EventHandler SaveButtonEventHandler { get; set; }

        // public event handler to expose 
        // the Cancel button's click event
        public EventHandler CancelButtonEventHandler { get; set; }

        // public string to expose the 
        // text Entry input's value
        public MyDataModel MultipleDataResult { get; set; }
        public static readonly BindableProperty ValidationLabelTextProperty =
            BindableProperty.Create(
                nameof(ValidationLabelText),
                typeof(string),
                typeof(PopupSettingView),
                string.Empty, BindingMode.OneWay, null,
                (bindable, value, newValue) =>
                {
                    ((PopupSettingView)bindable).ValidationLabel.Text = (string)newValue;
                });

        /// <summary>
        /// Gets or Sets the ValidationLabel Text
        /// </summary>
        public string ValidationLabelText
        {
            get
            {
                return (string)GetValue(ValidationLabelTextProperty);
            }
            set
            {
                SetValue(ValidationLabelTextProperty, value);
            }
        }



        public static readonly BindableProperty IsValidationLabelVisibleProperty =
            BindableProperty.Create(
                nameof(IsValidationLabelVisible),
                typeof(bool),
                typeof(PopupSettingView),
                false, BindingMode.OneWay, null,
                (bindable, value, newValue) =>
                {
                    if ((bool)newValue)
                    {
                        ((PopupSettingView)bindable).ValidationLabel.IsVisible = true;
                    }
                    else
                    {
                        ((PopupSettingView)bindable).ValidationLabel.IsVisible = false;
                    }
                });

        /// <summary>
        /// Gets or Sets if the ValidationLabel is visible
        /// </summary>
        public bool IsValidationLabelVisible
        {
            get
            {
                return (bool)GetValue(IsValidationLabelVisibleProperty);
            }
            set
            {
                SetValue(IsValidationLabelVisibleProperty, value);
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
              SaveButtonEventHandler?.Invoke(this, e);
            }


        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }
        private void txtExpireDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            MultipleDataResult.DayExpire =txtExpireDay.Text;
        }

        private void txtCancelDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            MultipleDataResult.DayCancel = txtCancelDay.Text;
        }

    }
    public class MyDataModel
    {
        public string DayExpire{ get; set; }
        public string DayCancel { get; set; }
        
    }
}