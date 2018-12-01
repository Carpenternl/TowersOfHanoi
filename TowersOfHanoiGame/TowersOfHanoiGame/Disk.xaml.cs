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
            this.WeightDisp.Content = i;
            c.Width = new GridLength(i, GridUnitType.Star);
            LeftCol.Width = RightCol.Width = new GridLength((MainWindow.DiskCapacity - i+1)/2, GridUnitType.Star);
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
