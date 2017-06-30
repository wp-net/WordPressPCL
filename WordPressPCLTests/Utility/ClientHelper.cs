using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;

namespace WordPressPCLTests.Utility
{
    public class ClientHelper
    {
        public static async Task<WordPressClient> GetAuthenticatedWordPressClient(AuthMethod method = AuthMethod.JWT)
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            /*client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;*/
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken(ApiCredentials.Username,ApiCredentials.Password);

            return client;
        }
    }
}
