using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }
        public GroupsController(IHttpClientFactory clientFactory)
        {
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GroupClient(httpClient);
        }

        private GroupClient Client { get; }

        public async Task<IActionResult> Index()
        {
            ICollection<Group> groups = await Client.GetAllAsync();
            return View(groups);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(GroupInput groupInput)
        {
            ActionResult result = View(groupInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GroupClient(httpClient);
                var createdAuthor = await client.PostAsync(groupInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            var fetchedGroup = await client.GetAsync(id);

            return View(fetchedGroup);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GroupInput GroupInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GroupClient(httpClient);
            var updatedGroup = await client.PutAsync(id, GroupInput);

            return RedirectToAction(nameof(Index));
        }
    }
}

