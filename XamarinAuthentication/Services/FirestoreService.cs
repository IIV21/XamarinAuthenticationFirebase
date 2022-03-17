using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAuthentication.Services
{
    public static class FirestoreService
    {
        private static IFirestore _cloud = CrossCloudFirestore.Current.Instance;
        public static async Task<bool> InsertUserInFirestore(Models.UserModel user)
        {
            await _cloud
                .Collection(Models.UserModel.CollectionPath)
                .Document(user.Id).SetAsync(user);
            return true;
        }
        public static async Task<Models.UserModel> GetFirestoreUser(string id)
        {
            IDocumentSnapshot document = await _cloud
                                        .Collection(Models.UserModel.CollectionPath)
                                        .Document(id)
                                        .GetAsync();

            Models.UserModel user = document.ToObject<Models.UserModel>();

            return user;
        }
    }
}
