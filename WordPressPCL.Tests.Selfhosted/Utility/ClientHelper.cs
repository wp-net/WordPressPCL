using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Utility;

public static class ClientHelper
{
    public static async Task<WordPressClient> GetAuthenticatedWordPressClient(TestContext context)
    {
        WordPressClient clientAuth = new(ApiCredentials.WordPressUri);

        Console.WriteLine($"Auth Plugin: {context?.Properties["authplugin"]}");
        if (context?.Properties["authplugin"]?.ToString() == "jwtAuthByUsefulTeam")
        {
            clientAuth.Auth.UseBearerAuth(JWTPlugin.JWTAuthByUsefulTeam);
        }
        else {
            clientAuth.Auth.UseBearerAuth(JWTPlugin.JWTAuthByEnriqueChavez);
        }
        await clientAuth.Auth.RequestJWTokenAsync(ApiCredentials.Username, ApiCredentials.Password);

        return clientAuth;
    }

    public static WordPressClient GetWordPressClient()
    {
        return new WordPressClient(ApiCredentials.WordPressUri);
    }
}
