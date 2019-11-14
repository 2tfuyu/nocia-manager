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
            AuthSecret = "",
            BasePath = ""
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
