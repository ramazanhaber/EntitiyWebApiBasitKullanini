using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ders1Api.Models
{
    public class BildirimHerkeseGonderModel
    {
        public class Notification
        {
            public string body { get; set; }
            public string title { get; set; }
            public string image { get; set; }
            public bool content_available { get; set; }
            public string priority { get; set; }
        }

        public string to { get; set; }
        public Notification notification { get; set; }
    }
}