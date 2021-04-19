using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    public static class ClientHelper
    {
        public static async Task<WordPressClient> GetAuthenticatedWordPressClient(TestContext context)
        {
            var clientAuth = new WordPressClient(ApiCredentials.WordPressUri);

            if (context?.Properties["authmode"]?.ToString() == "jwtauth")
            {
                Console.WriteLine("AuthMethod.JWTAuth");
                clientAuth.AuthMethod = AuthMethod.JWTAuth;
            }
            else
            {
                Console.WriteLine("AuthMethod.JWT");
                clientAuth.AuthMethod = AuthMethod.JWT;
            }
            await clientAuth.RequestJWToken(ApiCredentials.Username, ApiCredentials.Password);

            return clientAuth;
        }

        public static WordPressClient GetWordPressClient()
        {
            return new WordPressClient(ApiCredentials.WordPressUri);
        }
    }
}
