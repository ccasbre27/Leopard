using Leopard.Context;
using Leopard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Leopard.Controllers
{
    public class HomeController : Controller
    {

        LeopardContext db = new LeopardContext();

        public ActionResult Index()
        {   
            GroupViewModel viewModel = new GroupViewModel()
            {
                Grupo = new Group(),
                Grupos = GetGroups()

            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddGroup(GroupViewModel groupViewModel)
        {
            GroupViewModel viewModel = new GroupViewModel();

            

            if (!ModelState.IsValid)
            {
                viewModel.Result = new Result() { Status = false, Message = "Ha occurrido un error, verifique los valores ingresados" };
            }
            // verificamos si el grupo ya existe
            
            else
            {

                Group groupDuplicated = GetGroup(groupViewModel.Grupo.WhatsAppURL);

                if (groupDuplicated != null)
                {
                    ModelState.Clear();
                    viewModel.Result = new Result() { Status = false, Message = "El grupo ya existe, lo puede encontrar como: " + groupDuplicated.Name };
                }
                else
                {
                    groupViewModel.Grupo.CreatedDate = DateTime.Now;

                    try
                    {

                        db.Groups.Add(groupViewModel.Grupo);
                        await db.SaveChangesAsync();

                        viewModel.Result = new Result() { Status = true, Message = "El grupo se ha agregado exitosamente" };
                        ModelState.Clear();
                    }
                    catch (Exception e)
                    {

                        AddLog(e.ToString());

                        viewModel.Result = new Result() { Status = false, Message = "Ha occurrido un error, intente de nuevo" };
                        viewModel.Grupo.Name = groupViewModel.Grupo.Name;
                        viewModel.Grupo.WhatsAppURL = groupViewModel.Grupo.WhatsAppURL;
                    }
                    
                }

                viewModel.Grupos = GetGroups();
                

            }
            
            return View("Index",viewModel);
        }


        [ValidateAntiForgeryToken]
        public ActionResult CheckGroup(GroupViewModel groupViewModel)
        {
            GroupViewModel viewModel = new GroupViewModel();
            viewModel.Result = new Result();

            try
            {
                // se verifica que no se haya intentado infiltrar
                // si contiene código malicioso viene null
                if (String.IsNullOrEmpty(groupViewModel.Grupo.WhatsAppURL))
                {
                    viewModel.Result = new Result() { Status = false, Message = "Ha occurrido un error, intente de nuevo" };
                }
                else
                {
                    Group tempGroup = new Group();
                    
                    // buscamos el grupo
                    tempGroup = db.Groups.Where(e => e.WhatsAppURL == groupViewModel.Grupo.WhatsAppURL).FirstOrDefault();

                    // si es null no lo encontró
                    if (tempGroup == null)
                    {
                        viewModel.Result.Status = false;
                        viewModel.Result.Message = "El grupo no existe";
                    }
                    else
                    {
                        viewModel.Result.Status = true;
                        viewModel.Result.Message = "El grupo ya está agregado, lo puede encontrar como: " + tempGroup.Name;

                        
                    }

                    ModelState.Clear();
                }

                
            }
            catch (Exception e)
            {

                AddLog(e.ToString());

                viewModel.Result = new Result() { Status = false, Message = "Ha occurrido un error, intente de nuevo" };
                
                viewModel.Grupo.WhatsAppURL = groupViewModel.Grupo.WhatsAppURL;
            }
            finally
            {
                viewModel.Grupos = GetGroups();

            }


            return View("Index", viewModel);
        }

        public ActionResult About()
        {

            return View();
        }

        private IEnumerable <Group> GetGroups()
        {
            return db.Groups.ToList().Where(e => e.Approved == false && e.IsActive == true)
                .OrderBy(e => e.Name).AsEnumerable();
        }

        public Group GetGroup(string url)
        {
            Group group = null;

            try
            {
                // buscamos el grupo
                group = db.Groups.Where(e => e.WhatsAppURL == url).FirstOrDefault();

            }
            catch (Exception)
            {

                
            }

            return group;
        }

        private void AddLog(string message)
        {
            try
            {
                db.Logs.Add(new Log() { Message = message });
            }
            catch (Exception)
            {

                
            }
           
        }
    }
}