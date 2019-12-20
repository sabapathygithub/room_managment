using Prism.Regions;
using RoomManagementClient.Helpers;
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

namespace RoomManagementClient.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard(IRegionManager regionManager)
        {
            InitializeComponent();            
            if (regionManager == null)
            {
                throw new ArgumentNullException(nameof(regionManager));
            }
            regionManager.RegisterViewWithRegion("RoomViewRegion", typeof(RoomView));
            regionManager.RegisterViewWithRegion("GuestViewRegion", typeof(GuestView));
            regionManager.RegisterViewWithRegion("RoomEntryRegion", typeof(RoomEntry));
            this.Loaded += Dashboard_Loaded;
        }

        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            AddRoomTabItem.Visibility = CurrentUserHelper.Instance.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
