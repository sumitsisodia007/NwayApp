using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.CustomRenderer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : ContentView
    {
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
                                                      typeof(string),
                                                      typeof(CustomEntry),
                                                      defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),
                                                            typeof(string), typeof(CustomEntry),
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: (bindable, oldVal, newval) =>
                                                            {
                                                                var matEntry = (CustomEntry)bindable;
                                                                
                                                                matEntry.imgEntry.Placeholder = (string)newval;
                                                            });
        public static BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),
                                                            typeof(bool),
                                                            typeof(CustomEntry),
                                                            defaultValue: false,
                                                            propertyChanged: (bindable, oldVal, newVal) =>
                                                            {
                                                                var matEntry = (CustomEntry)bindable;
                                                                matEntry.imgEntry.IsPassword = (bool)newVal;
                                                            });
        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard),
                                                            typeof(Keyboard),
                                                            typeof(CustomEntry),
                                                            defaultValue: Keyboard.Default,
                                                            propertyChanged: (bindable, oldVal, newVal) =>
                                                            {
                                                                var matEntry = (CustomEntry)bindable;
                                                                matEntry.imgEntry.Keyboard = (Keyboard)newVal;
                                                            });
        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor),
                                                             typeof(Color),
                                                             typeof(CustomEntry),
                                                             defaultValue: Color.Accent);
        public Color AccentColor
        {
            get
            {
                return (Color)GetValue(AccentColorProperty);
            }
            set
            {
                SetValue(AccentColorProperty, value);
            }
        }

        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard)GetValue(KeyboardProperty);
            }
            set
            {
                SetValue(KeyboardProperty, value);
            }
        }

        public bool IsPassword
        {
            get
            {
                return (bool)GetValue(IsPasswordProperty);
            }
            set
            {
                SetValue(IsPasswordProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
        public CustomEntry()
        {
            InitializeComponent();
            imgEntry.BindingContext = this;
            imgEntry.Focused += async (s, a) =>
            {
                BottomBorder.HeightRequest = 2.5;
                BottomBorder.BackgroundColor = AccentColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (string.IsNullOrEmpty(imgEntry.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(

                        HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200)
                     );
                    imgEntry.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            };
            imgEntry.Unfocused += async (s, a) =>
            {
                BottomBorder.HeightRequest = 1;
                BottomBorder.BackgroundColor = Color.Gray;
                if (string.IsNullOrEmpty(imgEntry.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200)
                     );
                    imgEntry.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                }
            };
        }
    }
}