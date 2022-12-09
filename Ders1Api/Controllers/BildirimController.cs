using Ders1Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ders1Api.Controllers
{
    public class BildirimController : ApiController
    {

        [HttpPost]
        public bool sendPush(string baslik,string icerik,string link)
        {

            try
            {
                BildirimHerkeseGonderModel.Notification notification = new BildirimHerkeseGonderModel.Notification();
                notification.title = baslik;
                notification.priority = "high";
                notification.body = icerik;
                notification.image = link;
                notification.content_available = true;


                BildirimHerkeseGonderModel model = new BildirimHerkeseGonderModel();
                model.to = "/topics/all";
                model.notification = notification;

                Api2 api = new Api2();
                api.requestPostJsonAndroidVeIos("https://fcm.googleapis.com/fcm/send", model);
                return true;    
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}