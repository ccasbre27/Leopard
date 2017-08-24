using Leopard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leopard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Group> groups = new List<Group>();

            for (int i = 0; i < 200; i++)
            {
                groups.Add(new Group() { Name = "Programación #" + i, WhatsAppURL = "www.google.com" });
            }
            groups.Add(new Group() { Name = "Fundamentos de Programación Web", WhatsAppURL = "www.google.com" });
            
            groups.Add(new Group() { Name = "Principios de Administración", WhatsAppURL = "www.google.com" });

            GroupViewModel viewModel = new GroupViewModel()
            {
                Grupo = new Group(),
                Grupos = groups
            
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(Group group)
        {
            return View("Index");
        }

        public ActionResult About()
        {

            return View();
        }
    }
}