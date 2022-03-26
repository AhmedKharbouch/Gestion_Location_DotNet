using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;
using prototype3._0.Models;
using System.Text;
using System.Net;

namespace prototype3._0.Controllers
{
    public class carrentController : Controller
    {
        private readonly GCBD db = new GCBD();
        // GET: carrent

        public ActionResult Index()
        {
            if(Session["id"]==null)
            {
                return RedirectToAction("login","Account");
            }
                return View(db.Clients.ToList());
        }
        public ActionResult List_car()
        {
            return View(db.Voiture.ToList());
        }
        public ActionResult Index_Cat()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            // List<Categories> list_cat = db.Categories.ToList();

            return View(db.Categories.ToList());
        }
        public ActionResult Index_Car()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            // List<Voiture> list_cat = db.Voiture.ToList();

            return View(db.Voiture.ToList());
        }

        public ActionResult Meslocations()
        {

            Client client = db.Clients.Find(Session["client"]);
            if(client==null)
            {
                return RedirectToAction("List_car");
            }

            return View(db.Location.ToList().Where(cl=> cl.CID == client.CID));
        }

        public ActionResult Index_Model()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            // List<Modeles> list_cat = db.Modeles.ToList();

            return View(db.Modeles.ToList());
        }

/*
        public ActionResult Index_H(int? id)
        {

            /* 
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
            
            TempData.Clear();
            Client user = null;
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                user = db.Clients.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);
                user = db.Clients.Find(id);
                if (user == null)
                {
                    return HttpNotFound("erreur1");
                }
            }

            // Session.Clear();

            return View(db.Clients.Where(d => d.AdresseMail == user.AdresseMail).ToList());
        }
*/
/*
        public ActionResult Index_Admin()
        {
            Client user = null;
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                user = db.Clients.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

            }



            Session.Clear();
            return View(db.Clients.ToList());
        }
*/

       /* public ActionResult Create_client()
         {


             return View();


         }
        public ActionResult Create_client(Client client)
        {
            if (!ModelState.IsValid) return View();

            db.Clients.Add(client);
            db.SaveChanges();

            return RedirectToAction("Index");


        }*/


        // Fonction de hashage pour le mdp
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sh1 = new SHA1CryptoServiceProvider();
            byte[] pass_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sh1.ComputeHash(pass_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }

        public ActionResult Create_client1(Client client)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (!ModelState.IsValid && client.ImageTemp==null) return View();


            var result = db.Clients.Where(d => d.AdresseMail == client.AdresseMail);

            if (result.Count() == 0)
            {
                client.ImageTemp.SaveAs(@"C:\Users\ahmed\OneDrive\Bureau\prototype3.0\prototype3.0\prototype3.0\Images\" + client.ImageTemp.FileName);
                client.Image = client.ImageTemp.FileName;
                client.MotDePasse = hashPassword(client.MotDePasse);
                db.Clients.Add(client);
                db.SaveChanges();
                Session["id"] = client.CID;
                return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");


            }
            else
            {
                TempData["email"] = "Email existant";
            }

            return View(/*result.First()*/client);
        }

        public ActionResult Modifier_client(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }
        
        [HttpPost]
        public ActionResult Modifier_client(Client client)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (!ModelState.IsValid) return View();

            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Modifier_voiture(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }

            Session["img"] = voiture.Image;
            return View(voiture);
        }

        [HttpPost]
        public ActionResult Modifier_voiture(Voiture voiture)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (voiture.car_ID!=0 && voiture.Cat_ID != 0 && voiture.model_ID != 0 && voiture.num_immatri != null && voiture.loca_jounalier >0 && voiture.circulation!=null)
            {
                if (voiture.ImageTemp == null)
                {


                    voiture.Image = Session["img"].ToString();
                    Session["img"] = null;
                }
                else
                {
                    voiture.ImageTemp.SaveAs(@"C:\Users\ahmed\OneDrive\Bureau\prototype3.0\prototype3.0\prototype3.0\Images\" + voiture.ImageTemp.FileName);
                    voiture.Image = voiture.ImageTemp.FileName;
                }
            

            db.Entry(voiture).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_car");
            }
            return View(voiture);
        }

        public ActionResult Modifier_modele(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var modele = db.Modeles.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }

            return View(modele);
        }

        [HttpPost]
        public ActionResult Modifier_modele(Modeles modeles)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (!ModelState.IsValid) return View();

            db.Entry(modeles).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index_Model");
        }




      
        public ActionResult Info_client(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        public ActionResult List_car_info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }

            return View(voiture);
        }

        public ActionResult Info_car(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }

            return View(voiture);
        }

        public ActionResult Info_modele(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Modeles modeles = db.Modeles.Find(id);
            if (modeles == null)
            {
                return HttpNotFound();
            }

            return View(modeles);
        }

        public ActionResult Create_categories(Categories categories)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (!ModelState.IsValid) return View();

            db.Categories.Add(categories);
            db.SaveChanges();

            return RedirectToAction("Index_Cat");
        }

        public ActionResult Details(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        public ActionResult Create_voiture(Voiture voiture)
       {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }

            if (!ModelState.IsValid) return View();
            if(voiture.ImageTemp==null || voiture.loca_jounalier <= 0) return View();
            voiture.ImageTemp.SaveAs(@"C:\Users\ahmed\OneDrive\Bureau\prototype3.0\prototype3.0\prototype3.0\Images\" + voiture.ImageTemp.FileName);
            voiture.Image = voiture.ImageTemp.FileName;
            try
            {
            db.Voiture.Add(voiture);
            db.SaveChanges();
            db.Dispose();
            }
            catch (HttpException)
            {
                return View();
            }
            

            return RedirectToAction("Index_Car");
        }





        public ActionResult Create_modeles(Modeles modeles)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (!ModelState.IsValid) return View();

            db.Modeles.Add(modeles);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


       /* public ActionResult Create_location(Location location)
        {
            if (!ModelState.IsValid) return View();

            if(location.type_location=="longue duree")
            {
                location.prix_location = location.Voiture.loca_jounalier * 0.6 * 30;
            }
            db.Location.Add(location);
            db.SaveChanges();
            db.Dispose();

            return RedirectToAction("Index");
        }*/

        public ActionResult Get_idcar(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Voiture Voiture = db.Voiture.Find(id);
            if (Voiture == null)
            {
                return HttpNotFound();
            }

            Session["voiture"] = Voiture.car_ID;
            
            return RedirectToAction("Client_location");

        }

        public ActionResult Client_location(Location location)
        {
            if (location.date_debut<DateTime.UtcNow || location.date_fin<DateTime.UtcNow || location.date_fin<location.date_debut || location.type_location==null || Session["client"]==null ) return View();

            Voiture Voiture = db.Voiture.Find(Session["voiture"]);
            Client  client = db.Clients.Find(Session["client"]);

            TimeSpan duration = location.date_fin - location.date_debut;

            if (location.type_location == "longue duree")
            {
                location.prix_location = Voiture.loca_jounalier * 0.6 * 30;
            }
            else
            {
                location.prix_location = Voiture.loca_jounalier*(duration.Days+1) ;
            }
            location.Client = client;
            location.Voiture = Voiture;
            location.car_ID = Voiture.car_ID;
            location.CID = client.CID;
            
            db.Location.Add(location);
            db.SaveChanges();

            return RedirectToAction("List_car");
        }

        public ActionResult Supprimer_client(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        [HttpPost, ActionName("Supprimer_client")]
        public ActionResult ConfirmerSupprimer_client(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            Client client = db.Clients.Find(id);

            List<Location> t = new List<Location>(db.Location.ToList().Where(cl => cl.car_ID == id));

            if (t.Count != 0)
            {
                foreach (var s in t)
                {

                    db.Location.Remove(s);
                    db.SaveChanges();
                }
            }


            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Supprimerlocation(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound("erreur ici");
            }

            db.Location.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Meslocations");

        }

        
        public ActionResult Info_location(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = db.Location.Find(id);
            if (location == null)
            {
                return HttpNotFound("erreur ici");
            }

            return View(location);
        }

        public ActionResult Supprimer_categorie(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }

            return View(categories);
        }

        [HttpPost, ActionName("Supprimer_categorie")]
        public ActionResult ConfirmerSupprimer_categorie(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            Categories categories = db.Categories.Find(id);

            List<Voiture> t = new List<Voiture>(  db.Voiture.ToList().Where(cl => cl.Cat_ID == id));
            
            if(t.Count!=0)
            {
            foreach(var s in t)
            {
                List<Location> t1 = new List<Location>(db.Location.ToList().Where(cl => cl.car_ID == s.car_ID));
                    if (t1.Count != 0)
                    {
                        foreach (var s1 in t1)
                        {
                            db.Location.Remove(s1);
                            db.SaveChanges();
                        }
                    }
                   db.Voiture.Remove(s);
                  db.SaveChanges();
            }
            }
           

            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Supprimer_modele(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Modeles modeles = db.Modeles.Find(id);
            if (modeles == null)
            {
                return HttpNotFound();
            }

            return View(modeles);
        }

        [HttpPost, ActionName("Supprimer_modele")]
        public ActionResult ConfirmerSupprimer_modele(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            Modeles modeles = db.Modeles.Find(id);

            List<Voiture> t = new List<Voiture>(db.Voiture.ToList().Where(cl => cl.Cat_ID == id));

            if (t.Count != 0)
            {
                foreach (var s in t)
                {
                    List<Location> t1 = new List<Location>(db.Location.ToList().Where(cl => cl.car_ID == s.car_ID));
                    if (t1.Count != 0)
                    {
                        foreach (var s1 in t1)
                        {
                            db.Location.Remove(s1);
                            db.SaveChanges();
                        }
                    }
                    db.Voiture.Remove(s);
                    db.SaveChanges();
                }
            }


            db.Modeles.Remove(modeles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult Supprimer_voiture(int? id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }

            return View(voiture);
        }

        [HttpPost, ActionName("Supprimer_voiture")]
        public ActionResult ConfirmerSupprimer_voiture(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            Voiture voiture = db.Voiture.Find(id);

            List<Location> t = new List<Location>(db.Location.ToList().Where(cl => cl.car_ID == id));

            if (t.Count != 0)
            {
                foreach (var s in t)
                {
                   
                    db.Location.Remove(s);
                    db.SaveChanges();
                }
            }


            db.Voiture.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session["client"] = null;

            return RedirectToAction("List_car", "carrent");

        }
        public ActionResult Logout_admin()
        {
            Session["id"] = null;

            return RedirectToAction("login", "Account");

        }


    }
}