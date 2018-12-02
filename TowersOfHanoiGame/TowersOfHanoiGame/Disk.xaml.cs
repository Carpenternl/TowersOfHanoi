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
    /// Interaction logic for Disk.xaml
    /// </summary>
    public partial class Disk : UserControl
    {
        internal int Weight;

        public Disk()
        {
            InitializeComponent();
        }

        public Disk(int i)
        {
            InitializeComponent();
            this.Weight = i;
            this.WeightDisp.Content = Weight;
            int Maxweight = MainWindow.DiskCapacity + 1;
            double CenterRatio =(double)Weight / (double)Maxweight;
            double OuterRatio = 1 - CenterRatio;
            c.Width = new GridLength((1/OuterRatio)*2, GridUnitType.Star);
            LeftCol.Width = RightCol.Width = new GridLength((1/CenterRatio), GridUnitType.Star);
        }
        #region '<' Operator
        public static bool operator >(Disk a, Disk b)
        {
            if (a.Weight > b.Weight)
                return true;
            return false;

        }
        public static bool operator <(Disk a, Disk b)
        {
            if (a.Weight < b.Weight)
                return true;
            return false;
        }
        #endregion
    }
}
