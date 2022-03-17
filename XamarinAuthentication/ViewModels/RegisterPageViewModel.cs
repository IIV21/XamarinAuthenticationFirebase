using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinAuthentication.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel
    {
        public string WebAPIkey = "AIzaSyD_lywXDSx9ZLysvvDfyHZttej7uqQuJHQ";
        public ICommand RegisterCommand { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
      public  RegisterPageViewModel()
        {
            RegisterCommand = new Command(async () =>
                {
                    try
                    {
                        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                        var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                        string getToken = auth.FirebaseToken;

                        var user = auth.User;
                        Models.UserModel userModel = new Models.UserModel { Email = Email, Id = user.LocalId, Name = Name };

                       await Services.FirestoreService.InsertUserInFirestore(userModel);
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Some Error, retry", "Ok", "Cancel");
                    }
                }); 


        }
    }
}
