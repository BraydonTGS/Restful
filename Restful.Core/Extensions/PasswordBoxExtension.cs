using System.Windows;
using System.Windows.Controls;

namespace Restful.Core.Extensions
{
    /// <summary>
    /// Provides attached properties for enabling data binding on PasswordBox controls.
    /// 
    /// Thank you: https://www.wpftutorial.net/PasswordBox.html
    /// </summary>
    public class PasswordBoxExtension
    {
        /// <summary>
        /// Identifies the Password attached property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxExtension),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        /// <summary>
        /// Identifies the Attach attached property.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxExtension),
                new PropertyMetadata(false, Attach));

        /// <summary>
        /// Identifies the IsUpdating attached property.
        /// </summary>
        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxExtension));

        #region Attach
        /// <summary>
        /// Sets the value of the Attach attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <param name="value">The value to set.</param>
        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        /// <summary>
        /// Gets the value of the Attach attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <returns>The current value of the Attach property.</returns>
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        #endregion

        #region Password
        /// <summary>
        /// Sets the value of the Password attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <param name="value">The password to set.</param>
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Gets the value of the Password attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <returns>The current value of the Password property.</returns>
        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        #endregion

        #region IsUpdating
        /// <summary>
        /// Gets the value of the IsUpdating attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <returns>True if the password is being updated, otherwise false.</returns>
        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        /// <summary>
        /// Sets the value of the IsUpdating attached property.
        /// </summary>
        /// <param name="dp">The dependency object.</param>
        /// <param name="value">True if the password is being updated, otherwise false.</param>
        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles changes to the Password attached property.
        /// </summary>
        /// <param name="sender">The dependency object.</param>
        /// <param name="e">The event arguments.</param>
        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordChanged;

                if (!GetIsUpdating(passwordBox))
                {
                    passwordBox.Password = (string)e.NewValue;
                }

                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        /// <summary>
        /// Handles changes to the Attach attached property.
        /// </summary>
        /// <param name="sender">The dependency object.</param>
        /// <param name="e">The event arguments.</param>
        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                if ((bool)e.OldValue)
                {
                    passwordBox.PasswordChanged -= PasswordChanged;
                }

                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordChanged;
                }
            }
        }

        /// <summary>
        /// Handles the PasswordChanged event of the PasswordBox.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetIsUpdating(passwordBox, true);
                SetPassword(passwordBox, passwordBox.Password);
                SetIsUpdating(passwordBox, false);
            }
        }
        #endregion
    }
}
