using Ders1Api.Entities;
using Ders1Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ders1Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ResimController : ApiController
    {
        OgrenciDBEntities ogrenciContext = new OgrenciDBEntities();

        public GenelModel getResimList()
        {
            GenelModel model = new GenelModel();
            try
            {
                //Thread.Sleep(5000);
                model.resimData = ogrenciContext.Resim.ToList();


            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }
    }
}