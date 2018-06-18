using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Hosted.Utility
{
    public static class ClientHelper
    {
        private static WordPressClient _client;

        public static WordPressClient GetWordPressClient()
        {
            if(_client == null)
                _client = new WordPressClient(ApiCredentials.WordPressUri, "");
            return _client;
        }
    }
}
