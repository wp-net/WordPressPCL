using Windows.UI.Xaml.Controls;
using WordPressPCL;
using WordPressPCL.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordPressUWPTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public WordPressClient client;
        public MainPage()
        {
            this.InitializeComponent();
            TestPosts();
        }

        public async void TestPosts()
        {
            client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;

            var posts = await client.ListPosts(true);
            var post = await client.GetPost("1");


            var newpost = new Post()
            {
                Title = new Title()
                {
                    Raw = "new test post"
                }
            };
            //var newpostresponse = await client.CreatePost(newpost);


            var comments = await client.ListComments();
            var comment = await client.GetComment("3");

            var currentUser = await client.GetCurrentUser();
        }

        private void authButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //client.DoOAuth();
        }
    }
}
