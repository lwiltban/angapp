using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AngService;

namespace AngApp.Controllers
{
    public class ActionController : Controller
    {
        /// <summary>
        /// Returns a list of users based on the passed in id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private IEnumerable<AngModel.User> GetUsers(int? id)
        {
            RepoService repo = new RepoService();

            var users = repo.GetUsers(id);
            return users;
        }

        /// <summary>
        /// Returns all the wise admin users based on the passed in id
        /// in a json formatted string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public string JsonUsers(int? id)
        {
            var users = GetUsers(id);
            string json = new JavaScriptSerializer().Serialize(users);
            return json;
        }

        /// <summary>
        /// Returns all the wise admin users based on the passed in id
        /// in an html view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HtmlUsers(int? id)
        {
            var users = GetUsers(id);
            ViewBag.items = users;
            return View();
        }

        //
        // POST: /Action/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Action/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Action/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
