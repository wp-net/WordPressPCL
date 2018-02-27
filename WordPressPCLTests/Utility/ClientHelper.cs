using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCLTests.Utility;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    public static class ClientHelper
    {
        private static WordPressClient _client;
        private static WordPressClient _clientAuth;

        public static async Task<WordPressClient> GetAuthenticatedWordPressClient(AuthMethod method = AuthMethod.JWT)
        {
            if(_clientAuth == null)
            {
                _clientAuth = new WordPressClient(ApiCredentials.WordPressUri)
                {
                    /*client.Username = ApiCredentials.Username;
                    client.Password = ApiCredentials.Password;*/
                    AuthMethod = AuthMethod.JWT
                };
                await _clientAuth.RequestJWToken(ApiCredentials.Username, ApiCredentials.Password);
            }

            return _clientAuth;
        }

        public static WordPressClient GetWordPressClient()
        {
            if(_client == null)
                _client = new WordPressClient(ApiCredentials.WordPressUri);
            return _client;
        }
    }
}
