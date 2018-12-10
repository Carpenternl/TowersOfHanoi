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
        public Tower[] Towers { get; set; }
        public Disk[] Disks { get; set; }

        public Tower Origin { get; set; }

        public static int TowerCapacity { get; set; }
        public static int DiskCapacity { get; set; }
        public Color DiskOriginColor { get { return Colors.Green; } }

        public Color DefaultDiskBorderColor { get { return Colors.Black; } }

        private Disk CurrentDiskOrigin { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EndGame(object sender, RoutedEventArgs e)
        {
            //Todo Cleanup
            this.Towers = null;
            this.Disks = null;
            this.TowerPanel.Children.Clear();
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
            Towers = new Tower[TowerCapacity];
            for (int i = 0; i < TowerCapacity; i++)
            {
                Towers[i] = new Tower();
                Towers[i].Click += TowerClick;
                Towers[i].MouseEnter += CheckTower;
                this.TowerPanel.Children.Add(Towers[i]);
            }
            this.Disks = new Disk[DiskCapacity];
            for (int i = DiskCapacity - 1; i >= 0; i--)
            {

                Disks[i] = new Disk(i + 1);
                bool stacked = Towers[0].TryPush(Disks[i]);
            }
        }

        private void checktowerEnd(object sender, MouseEventArgs e)
        {
        }

        private void CheckTower(object sender, MouseEventArgs e)
        {
            if (CurrentDiskOrigin is null)
                return;
            Tower target = sender as Tower;
            Disk TargetDisk;
            if (target.TryPeek(out TargetDisk))
            {
                if (TargetDisk.Weight < CurrentDiskOrigin.Weight)
                {
                    UpdateUI(CurrentDiskOrigin.WeightDisp, Colors.Transparent, Colors.Red);
                }
                else
                {
                    UpdateUI(CurrentDiskOrigin.WeightDisp, Colors.Green, Colors.Blue);
                }
            }
            else
            {
                UpdateUI(CurrentDiskOrigin.WeightDisp, Colors.Green, Colors.Blue);
            }
        }

        // Tower interaction logic
        private void TowerClick(object sender, MouseButtonEventArgs e)
        {
            //Get the Caller
            Tower Destination = sender as Tower;
            Disk TopDisk;
            // We should pick an origin if we haven't already
            if (Origin is null)
            {
                SetOrigin(Destination);
                if (Destination.TryPeek(out TopDisk))
                {
                    CurrentDiskOrigin = TopDisk;
                    UpdateUI(CurrentDiskOrigin.WeightDisp, Colors.Green, Colors.Blue);
                }
            }
            else
            {
                Disk DiskToMove;
                if (Origin != Destination && Origin.TryPop(out DiskToMove))
                {
                    Disk DiskBase;
                    if (Destination.TryPeek(out DiskBase))
                    {
                        if (DiskBase > DiskToMove)
                        {
                            Destination.TryPush(DiskToMove);
                        }
                        else
                        {
                            Origin.TryPush(DiskToMove);
                        }
                    }
                    else
                    {
                        Destination.TryPush(DiskToMove);
                    }
                }
                UpdateUI(CurrentDiskOrigin.WeightDisp, Colors.Transparent, Colors.Black);
                CurrentDiskOrigin = null;
                Origin = null;

                if (Towers[Towers.Length-1].Disks.Count == Disks.Length)
                {
                    MessageBox.Show("SUCCESS");
                    EndGame(this, e);

                }
            }
        }

        private void UpdateUI(Control element, Color fill, Color border)
        {
            element.Background = new  SolidColorBrush(fill);
            element.BorderBrush = new SolidColorBrush(border);
        }

        private bool UnsetOrigin(Tower target)
        {
            Disk N;
            if (target.TryPeek(out N))
            {
                Label ManipulateTarget = N.WeightDisp;
                ManipulateTarget.BorderBrush = new SolidColorBrush(DefaultDiskBorderColor);
                Origin = null;
                return true;
            }
            return false;
        }

        private bool SetOrigin(Tower target)
        {
            Disk N;
            if (target.TryPop(out N))
            {
                //Label ManipulateTarget = N.WeightDisp;
                //ManipulateTarget.BorderBrush = new SolidColorBrush(DiskOriginColor);
                //ManipulateTarget.BorderThickness = new Thickness(3.0);
                Origin = target;
                target.TryPush(N);
                return true;
            }
            return false;
        }
    }
}

