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
            clientAuth.AuthMethod = AuthMethod.Bearer;
            
            Console.WriteLine($"Auth Plugin: {context?.Properties["authplugin"]}");
            if (context?.Properties["authplugin"]?.ToString() == "jwtAuthByUsefulTeam")
            {
                clientAuth.JWTPlugin = JWTPlugin.JWTAuthByUsefulTeam;
            }
            else
            {
                clientAuth.JWTPlugin = JWTPlugin.JWTAuthByEnriqueChavez;
            }
            await clientAuth.Auth.RequestJWTokenAsync(ApiCredentials.Username, ApiCredentials.Password);

            return clientAuth;
        }

        public static WordPressClient GetWordPressClient()
        {
            return new WordPressClient(ApiCredentials.WordPressUri);
        }
    }
}
