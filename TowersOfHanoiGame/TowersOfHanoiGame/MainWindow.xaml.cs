using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TowersOfHanoiGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int TowerCapacity { get; set; }
        public static int DiskCapacity { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EndGame(object sender, RoutedEventArgs e)
        {
            //Todo Cleanup

            ToggleButtons(Visibility.Collapsed, Visibility.Visible, true);
        }

        private void ToggleButtons(Visibility EndButton, Visibility StartButton, bool SettingsPanelState)
        {
            GameEnd.Visibility = EndButton;
            GameStart.Visibility = StartButton;
            SettingsPanel.IsEnabled = SettingsPanelState;
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            ToggleButtons(Visibility.Visible, Visibility.Collapsed, false);
            TowerCapacity = (int)TowerSlider.Value;
            DiskCapacity = (int)HeightSlider.Value;
        }
    }
}
