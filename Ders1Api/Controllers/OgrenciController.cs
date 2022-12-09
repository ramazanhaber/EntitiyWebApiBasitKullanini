using Ders1Api.Entities;
using Ders1Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
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
                //Thread.Sleep(5000);
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


        public GenelModel ogrenciGuncelle(OgrenciTable ogrenciTable,string adres="")
        {
            GenelModel model = new GenelModel();
            try
            {
                ogrenciContext.Entry(ogrenciTable).State = System.Data.Entity.EntityState.Modified;


                if (adres!="")
                {
                    var detay = ogrenciContext.OgrenciDetail.Where(x => x.ogrenciId == ogrenciTable.id).FirstOrDefault();
                    if (detay == null)// ekleme yapcam
                    {
                        OgrenciDetail ogrenciDetail = new OgrenciDetail();
                        ogrenciDetail.adres = adres;
                        ogrenciDetail.ogrenciId = ogrenciTable.id;

                        ogrenciContext.OgrenciDetail.Add(ogrenciDetail);
                    }
                    else // guncelleme yapicam
                    {
                        detay.adres = adres;
                        ogrenciContext.Entry(detay).State = System.Data.Entity.EntityState.Modified;

                    }
                }


                ogrenciContext.SaveChanges();



            }
            catch (Exception ex)
            {
                model.success = false;
                model.mesaj = "HATA ! " + ex.Message;
            }


            return model;
        }

        [HttpPost]
        public GenelModel ogrenciSil(int id)
        {
            GenelModel model = new GenelModel();
            try
            {
                var ogrenci = ogrenciContext.OgrenciTable.Where(x => x.id == id).FirstOrDefault();
                if (ogrenci != null)
                {
                    ogrenciContext.OgrenciTable.Remove(ogrenci);
                    ogrenciContext.SaveChanges();
                }


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