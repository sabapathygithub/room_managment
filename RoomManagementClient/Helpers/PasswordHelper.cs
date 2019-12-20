using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RoomManagementClient.Helpers
{
    public class PasswordHelper : DependencyObject
    {
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(bool), typeof(PasswordHelper),
                new PropertyMetadata(false,
                                     Attach));

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(
                    bool),
                                                typeof(PasswordHelper),
                                                new FrameworkPropertyMetadata(false, OnIsFocusedPropertyChanged));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.RegisterAttached("ShowWatermark", typeof(bool), typeof(PasswordHelper), new UIPropertyMetadata(false));

        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.RegisterAttached("WatermarkText", typeof(string), typeof(PasswordHelper), new UIPropertyMetadata(string.Empty));

        private static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(PasswordHelper), new FrameworkPropertyMetadata(false));

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached(
                "IsUpdating",
                typeof(bool),
                typeof(PasswordHelper));

        public bool HasText
        {
            get
            {
                return (bool)GetValue(HasTextProperty);
            }
            set
            {
                SetValue(HasTextProperty, value);
            }
        }

        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static bool GetShowWatermark(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowWatermarkProperty);
        }

        public static string GetWatermarkText(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkTextProperty);
        }

        public static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUpdatingProperty, value);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        public static void SetShowWatermark(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowWatermarkProperty, value);
        }

        public static void SetWatermarkText(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkTextProperty, value);
        }

        private static void Attach(
            DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }
            else
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focus();
            }
        }

        private static void OnPasswordPropertyChanged(
            DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }

            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

    }
}
