using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RoomManagementClient.Helpers;
using RoomManagment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomManagementClient.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        IRegionManager regionManager;

        public LoginViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        private string loginUserName;
        private string loginPassword;
        public string LoginUserName
        {
            get { return loginUserName; }
            set { SetProperty(ref loginUserName, value); }
        }
        
        public string LoginPassword
        {
            get { return loginPassword; }
            set { SetProperty(ref loginPassword, value); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string registrationLogin;
        public string RegistrationLogin
        {
            get { return registrationLogin; }
            set { SetProperty(ref registrationLogin, value); }
        }

        private string registrationPassword;
        public string RegistrationPassword
        {
            get { return registrationPassword; }
            set { SetProperty(ref registrationPassword, value); }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }



        private DelegateCommand cancelCommand;
        public DelegateCommand CancelCommand =>
            cancelCommand ?? (cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        void ExecuteCancelCommand()
        {
            ClearLogin();
            ClearRegistraton();
        }

        private DelegateCommand loginCommand;
        public DelegateCommand LoginCommand =>
            loginCommand ?? (loginCommand = new DelegateCommand(ExecuteLoginCommand));

        void ExecuteLoginCommand()
        {
            UserModel userModel = new UserModel
            {
                Login = LoginUserName,
                Password = LoginPassword
            };
            try
            {
                string response = WebApiConsumer.ConsumePostAsJsonAsync("login", userModel);
                if (!string.IsNullOrEmpty(response))
                {
                    UserModel user = JsonConvert.DeserializeObject<UserModel>(response);
                    CurrentUserHelper.Instance.SetUserDetails(user);
                    var currentRegion = regionManager.Regions["LoginViewRegion"];
                    if (currentRegion != null)
                    {                        
                        currentRegion.RequestNavigate(new Uri("Dashboard", UriKind.Relative));
                    }
                    ClearLogin();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("There is a problem while login. Please try again");
            }

        }

        private DelegateCommand registrationCommand;
        public DelegateCommand RegistrationCommand =>
            registrationCommand ?? (registrationCommand = new DelegateCommand(ExecuteRegistrationCommand));

        void ExecuteRegistrationCommand()
        {
            if(RegistrationPassword != ConfirmPassword)
            {
                MessageBox.Show("Password and confirmation password do not match.");
                return;
            }
            UserModel userModel = new UserModel
            {
                Login = RegistrationLogin,
                Password = RegistrationPassword,
                UserName = Username                
            };
            try
            {
                string response = WebApiConsumer.ConsumePostAsJsonAsync("users", userModel);
                if (!string.IsNullOrEmpty(response))
                {
                    UserModel user = JsonConvert.DeserializeObject<UserModel>(response);
                    CurrentUserHelper.Instance.SetUserDetails(user);
                    ClearRegistraton();
                    var currentRegion = regionManager.Regions["LoginViewRegion"];
                    if (currentRegion != null)
                    {
                        currentRegion.RequestNavigate(new Uri("Dashboard", UriKind.Relative)); 
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("There is a problem on Registration. Please try again");
            }
        }

        void ClearRegistraton()
        {
            Username = string.Empty;
            ConfirmPassword = string.Empty;
            RegistrationLogin = string.Empty;
            RegistrationPassword = string.Empty;
        }

        void ClearLogin()
        {
            LoginUserName = string.Empty;
            LoginPassword = string.Empty;
        }
    }
}
