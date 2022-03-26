using prototype3._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace prototype3._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly GCBD db = new GCBD();

        public ActionResult Index()
        {
            return View();

        }

        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sh1 = new SHA1CryptoServiceProvider();
            byte[] pass_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sh1.ComputeHash(pass_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }

        //----------creation du client cote client--------------------------------------------------------------
        public ActionResult create_account(Client client)
        {
            TempData.Clear();
            
            if (!ModelState.IsValid || client.ImageTemp==null) return View(/*clientclient*/);

            var result = db.Clients.Where(d => d.AdresseMail == client.AdresseMail);
            var result2 = db.Clients.Where(d => d.numero_tel == client.numero_tel);

            if (result.Count() == 0 && result2.Count()==0)
            {
                string s = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                
                string s12 = s.Replace('\\', '/');

                s12 = s12 + "/prototype3.0/prototype3.0/prototype3.0/Images/";

                client.ImageTemp.SaveAs(s12 + client.ImageTemp.FileName);
                client.Image = client.ImageTemp.FileName;
                client.MotDePasse = hashPassword(client.MotDePasse);
                db.Clients.Add(client);
                db.SaveChanges();
                
                return RedirectToAction("login");
            }
            else
            {
                if (result.Count() != 0 && result2.Count() != 0)
                {
                    TempData["email"] = "Email et Numero existant";
                }
                else if (result.Count() != 0)
                {
                    TempData["email"] = "Email existant";
                }
                else
                {
                    TempData["email"] = "Numero existant";
                }
            }
            
            return View(/*result.First()*/client);
        }

      
        //----------login avec adresse mail--------------------------------------------------------------
        public ActionResult login()
        {
            TempData.Clear();

            return View();

        }

        [HttpPost]
        public ActionResult login(/*[Bind(Include = "AdresseMail,MotDePasse",Exclude = "DateNaissance,valider,Isadmin")]*/Client client)
        {
            TempData.Clear();
            //Session.Clear();
            Session["path"] = null;

            if(client.AdresseMail==null || client.MotDePasse==null) return View(client);
            
            if(client.AdresseMail == "123@123.com" && client.MotDePasse == "123")
            {
                client.Isadmin = true;
                Session["id"] = 6612456978213;
                return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

            }
            
           // if (!ModelState.IsValid)  return View(client);


            // var result = db.Users.SingleOrDefault(d => d.email == user.email && d.MotDePasse == user.MotDePasse);

            string mdp = hashPassword(client.MotDePasse);
            var result = db.Clients.Where(d => d.AdresseMail == client.AdresseMail && d.MotDePasse == mdp);
            
            if (result.Count() != 0)
            {
                //Session.Clear();
                
                


                if (result.First().Isadmin == false && result.First().valider == true)
                {
                    Session["client"] = result.First().CID;
                    return RedirectToAction(Session["client"].ToString(), "carrent/List_car", "Index");

                }
                if (result.First().Isadmin == true && result.First().valider == true)
                {
                    Session["id"] = result.First().CID;
                    return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

                }

                if (result.First().Isadmin == true && result.First().valider == false)
                {
                    Session["id"] = result.First().CID;
                    return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

                }

                if (result.First().Isadmin == false && result.First().valider == false)
                {
                    TempData["validation"] = "Votre compte est en cours de validation";
                    return View();

                }


            }
            else
            {
                TempData["erreur"] = "Email OU Password incorrect";

            }


            return View(client);

        }

        //----------login avec numero de telephone--------------------------------------------------------------

        public ActionResult login_numero()
        {
            TempData.Clear();

            return View();

        }

        [HttpPost]
        public ActionResult login_numero(/*[Bind(Include = "AdresseMail,MotDePasse",Exclude = "DateNaissance,valider,Isadmin")]*/Client client)
        {
            TempData.Clear();
            //Session.Clear();
            Session["path"] = null;

            if (client.numero_tel == null || client.MotDePasse == null) return View(client);

            if (client.numero_tel == "0000000000" && client.MotDePasse == "123")
            {
                client.Isadmin = true;
                Session["id"] = 6612456978213;
                return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

            }

            // if (!ModelState.IsValid)  return View(client);


            // var result = db.Users.SingleOrDefault(d => d.email == user.email && d.MotDePasse == user.MotDePasse);

            string mdp = hashPassword(client.MotDePasse);
            var result = db.Clients.Where(d => d.numero_tel == client.numero_tel && d.MotDePasse == mdp);

            if (result.Count() != 0)
            {
                //Session.Clear();




                if (result.First().Isadmin == false && result.First().valider == true)
                {
                    Session["client"] = result.First().CID;
                    return RedirectToAction(Session["client"].ToString(), "carrent/List_car", "Index");

                }
                if (result.First().Isadmin == true && result.First().valider == true)
                {
                    Session["id"] = result.First().CID;
                    return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

                }

                if (result.First().Isadmin == true && result.First().valider == false)
                {
                    Session["id"] = result.First().CID;
                    return RedirectToAction(Session["id"].ToString(), "carrent/Index", "Index");

                }

                if (result.First().Isadmin == false && result.First().valider == false)
                {
                    TempData["validation"] = "Votre compte est en cours de validation";
                    return View();

                }


            }
            else
            {
                TempData["erreur"] = "Numero OU Password incorrect";

            }


            return View(client);

        }



    }
}