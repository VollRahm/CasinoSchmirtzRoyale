using Casino_Schmirtz_Royale.CustomUIElements;
using Casino_Schmirtz_Royale.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Casino_Schmirtz_Royale
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
            GameManager.Initialize(this);
            Handler.Initialize(this);
            ConsoleHandler.Initialize(this);
        }

        private async void respinBtn_Click(object sender, RoutedEventArgs e)
        {
            respinBtn.IsEnabled = false;
            await GameManager.DoSpin();
            respinBtn.IsEnabled = true;
        }
    }
}