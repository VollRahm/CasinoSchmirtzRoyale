using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Casino_Schmirtz_Royale.CustomUIElements
{
    public static class Handler
    {
        private static MainWindow w;

        public static void Initialize(MainWindow w)
        {
            Handler.w = w;
            w.darkModeSwitch.Checked += new System.Windows.RoutedEventHandler(SwitchDarkMode);
            w.darkModeSwitch.Unchecked += new RoutedEventHandler(SwitchDarkMode);
        }

        private static void SwitchDarkMode(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;

            if ((bool)btn.IsChecked)
                EnableDarkMode();
            else
                DisableDarkMode();
        }

        private static void EnableDarkMode()
        {
            w.CanvasBackgroundBrush.Color = Color.FromRgb(35, 39, 42);
            w.InfoPanelBrush.Color = Color.FromRgb(44, 47, 51);
            w.balanceLbl.Foreground = new SolidColorBrush(Colors.White);
            w.darkModeLbl.Foreground = new SolidColorBrush(Colors.White);
        }

        private static void DisableDarkMode()
        {
            w.CanvasBackgroundBrush.Color = Colors.White;
            w.InfoPanelBrush.Color = Colors.Orange;
            w.balanceLbl.Foreground = new SolidColorBrush(Colors.Black);
            w.darkModeLbl.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
