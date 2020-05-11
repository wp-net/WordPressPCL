using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    public static class ClientHelper
    {
        public static async Task<WordPressClient> GetAuthenticatedWordPressClient(AuthMethod method = AuthMethod.JWT)
        {
            var clientAuth = new WordPressClient(ApiCredentials.WordPressUri)
            {
                AuthMethod = AuthMethod.JWT
            };
            await clientAuth.RequestJWToken(ApiCredentials.Username, ApiCredentials.Password);

            return clientAuth;
        }

        public static WordPressClient GetWordPressClient()
        {
            return new WordPressClient(ApiCredentials.WordPressUri);
        }
    }
}
