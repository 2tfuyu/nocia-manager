using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nocia_manager
{
    class FirebaseDB
    {

        private static FirebaseDB instance;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "prJA3HbKTp3XrZtwjo9Pmg7MjGEOH7EhcQN7rn8N",
            BasePath = "https://nocia-3b13c.firebaseio.com"
        };

        IFirebaseClient client;

        public FirebaseDB()
        {
            this.client = new FireSharp.FirebaseClient(config);
            instance = this;
        }

        public async Task<FirebaseResponse> GetAllNews()
        {
            return await client.GetAsync("news/");
        }

        public async Task<SetResponse> AddNews(News data)
        {
            return await client.SetAsync("news/" + data.title, data);
        }

        public async Task<FirebaseResponse> DeleteNews(String title)
        {
            return await client.DeleteAsync("news/" + title);
        }

        public static FirebaseDB GetInstance()
        {
            return instance;
        }
    }
}
