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
    /// Interaction logic for Tower.xaml
    /// </summary>
    public partial class Tower : UserControl
    {

        private int TotalRows { get { return this.Container.RowDefinitions.Count(); } }

        public Stack<Disk> Disks {get; set;}

        public delegate void ClickHandler(object sender, MouseButtonEventArgs e);
        public event ClickHandler Click;

        public Tower()
        {
            InitializeComponent();
            Disks = new Stack<Disk>(MainWindow.DiskCapacity);
            for (int i = 0; i < MainWindow.DiskCapacity; i++)
            {
                Container.RowDefinitions.Add(new RowDefinition());
            }
        }

        internal bool TryPush(Disk disk)
        {
            if(Disks.Count<=0 || Disks.Peek() > disk)
            {
                Disks.Push(disk);
                Grid.SetRow(disk, MainWindow.DiskCapacity - Disks.Count);
                Container.Children.Add(disk);
                return true;
            }
            return false;
        }

        private void ClickPass(object sender, MouseButtonEventArgs e)
        {
            Click(sender, e);
        }

        internal bool TryPop(out Disk n)
        {
            n = new Disk();
            if (Disks.Count <= 0)
                return false;
            n = Disks.Pop();
            Container.Children.Remove(n);
            return true;
        }

        internal bool TryPeek(out Disk diskArg)
        {
            diskArg = new Disk();
            if (Disks.Count <= 0)
                return false;
            diskArg = Disks.Peek();
            return true;
        }
    }
}
