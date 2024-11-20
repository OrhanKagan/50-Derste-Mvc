using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p3)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p3.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p3.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);

        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.STOK = p1.STOK;
            urun.FIYAT = p1.FIYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}