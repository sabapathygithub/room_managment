using Prism.Regions;
using System;
using System.Windows;

namespace RoomManagementClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            if(regionManager == null)
            {
                throw new ArgumentNullException(nameof(regionManager));
            }
            regionManager.RegisterViewWithRegion("LoginViewRegion", typeof(LoginView));
            regionManager.RegisterViewWithRegion("LoginViewRegion", typeof(Dashboard));

        }
    }
}
