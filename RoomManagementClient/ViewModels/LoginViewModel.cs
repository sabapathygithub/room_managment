using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RoomManagementClient.Helpers;
using RoomManagment.Core.Models;
using System;
using System.Windows;

namespace RoomManagementClient.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region Fields
        IRegionManager regionManager;
        private string loginUserName;
        private string loginPassword;
        private string username;
        private string registrationLogin;
        private string registrationPassword;
        private string confirmPassword;
        private DelegateCommand cancelCommand;
        private DelegateCommand loginCommand;
        private DelegateCommand registrationCommand;
        #endregion

        #region Constructor

        public LoginViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the login user name.
        /// </summary>
        public string LoginUserName
        {
            get { return loginUserName; }
            set { SetProperty(ref loginUserName, value); }
        }

        /// <summary>
        /// Gets or sets the login password.
        /// </summary>
        public string LoginPassword
        {
            get { return loginPassword; }
            set { SetProperty(ref loginPassword, value); }
        }

        /// <summary>
        /// Gets or sets the user name for registration.
        /// </summary>
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        /// <summary>
        /// Gets or sets the registration login.
        /// </summary>
        public string RegistrationLogin
        {
            get { return registrationLogin; }
            set { SetProperty(ref registrationLogin, value); }
        }

        /// <summary>
        /// Gets or sets the password during registration.
        /// </summary>
        public string RegistrationPassword
        {
            get { return registrationPassword; }
            set { SetProperty(ref registrationPassword, value); }
        }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }
        #endregion

        #region Command

        public DelegateCommand CancelCommand =>
            cancelCommand ?? (cancelCommand = new DelegateCommand(ExecuteCancelCommand));

        public DelegateCommand LoginCommand =>
            loginCommand ?? (loginCommand = new DelegateCommand(ExecuteLoginCommand));

        public DelegateCommand RegistrationCommand =>
            registrationCommand ?? (registrationCommand = new DelegateCommand(ExecuteRegistrationCommand));

        #endregion

        #region Methods

        void ExecuteCancelCommand()
        {
            ClearLogin();
            ClearRegistraton();
        }

        async void ExecuteLoginCommand()
        {
            UserModel userModel = new UserModel
            {
                Login = LoginUserName,
                Password = LoginPassword
            };
            try
            {
                string response = await WebApiConsumer.ConsumePostAsJsonAsync("login", userModel);
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

       async  void ExecuteRegistrationCommand()
        {
            if (RegistrationPassword != ConfirmPassword)
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
                string response = await WebApiConsumer.ConsumePostAsJsonAsync("users", userModel);
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

        #endregion
    }
}
