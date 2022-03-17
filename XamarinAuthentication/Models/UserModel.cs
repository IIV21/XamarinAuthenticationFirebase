using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinAuthentication.Models
{
    public class UserModel
    {
        public static string CollectionPath = "UserDetails";

        [Id]
        public string Id { get; set; }
        [MapTo("email")]
        public string Email { get; set; }
        [MapTo("name")]
        public string Name { get; set; }
    }
}
