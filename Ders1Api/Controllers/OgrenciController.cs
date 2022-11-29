using Ders1Api.Entities;
using Ders1Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ders1Api.Controllers
{
    public class OgrenciController : ApiController
    {

        OgrenciDBEntities ogrenciContext = new OgrenciDBEntities();

        public GenelModel getOgrenciList()
        {
            GenelModel model = new GenelModel();
            try
            {
                model.OgrenciData = ogrenciContext.OgrenciTable.ToList();

            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }

        public GenelModel getOgrenciDetailList()
        {
            GenelModel model = new GenelModel();
            try
            {
                string query = @"select ogrenci.*,detay.adres from OgrenciTable as ogrenci
left join OgrenciDetail as detay on ogrenci.id=detay.ogrenciId";

                model.OgrenciDetailData = model.getQueryToDataTable(query, ogrenciContext);


            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public string isimDuzenle(string isim)
        {
            return isim + " düzeltilmiş isim";
        }


        public GenelModel ogrenciEkle(OgrenciTable ogrenciTable)
        {
            GenelModel model = new GenelModel();
            try
            {
                ogrenciContext.OgrenciTable.Add(ogrenciTable);
                ogrenciContext.SaveChanges();

            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }


        public GenelModel ogrenciGuncelle(OgrenciTable ogrenciTable)
        {
            GenelModel model = new GenelModel();
            try
            {
                ogrenciContext.Entry(ogrenciTable).State = System.Data.Entity.EntityState.Modified;
                ogrenciContext.SaveChanges();

            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }


        public GenelModel ogrenciAddOrUpdate(OgrenciTable ogrenciTable)
        {
            GenelModel model = new GenelModel();
            try
            {
                if (ogrenciTable.id == 0) ogrenciContext.OgrenciTable.Add(ogrenciTable);
                else ogrenciContext.Entry(ogrenciTable).State = System.Data.Entity.EntityState.Modified;
                ogrenciContext.SaveChanges();
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