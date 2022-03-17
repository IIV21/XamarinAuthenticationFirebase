using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinAuthentication.Views;

namespace XamarinAuthentication.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {
        public string WebAPIkey = "AIzaSyD_lywXDSx9ZLysvvDfyHZttej7uqQuJHQ";

        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterPage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginPageViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Email) ||
                    string.IsNullOrEmpty(Password) ||
                    Password.Length <= 6)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Some fields are empty", "Ok", "Cancel");
                }
                else
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                    try
                    {
                        var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);
                        var content = await auth.GetFreshAuthAsync();
                        var serializer = JsonConvert.SerializeObject(content);
                        var user = auth.User;
                        Xamarin.Essentials.Preferences.Set("FirebaseRefreshToken", serializer);
                        await App.Current.MainPage.DisplayAlert("Error", serializer, "Ok", "Cancel");

                    }

                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Some fields are empty", "Ok", "Cancel");
                    }
                }
            });
            GoToRegisterPage = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            });
        }
    }
}
