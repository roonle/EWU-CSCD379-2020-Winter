using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        public IHttpClientFactory ClientFactory { get; }
        public GiftsController(IHttpClientFactory clientFactory)
        {
            HttpClient httpClient = clientFactory?.CreateClient("SecretSantaApi") ?? throw new ArgumentNullException(nameof(clientFactory));
            Client = new GiftClient(httpClient);
        }

        private GiftClient Client { get; }

        public async Task<IActionResult> Index()
        {
            ICollection<Gift> gifts = await Client.GetAllAsync();
            return View(gifts);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(GiftInput giftInput)
        {
            ActionResult result = View(giftInput);

            if (ModelState.IsValid)
            {
                HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

                var client = new GiftClient(httpClient);
                var createdAuthor = await client.PostAsync(giftInput);

                result = RedirectToAction(nameof(Index));
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            var fetchedGift = await client.GetAsync(id);

            return View(fetchedGift);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, GiftInput giftInput)
        {
            HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi");

            var client = new GiftClient(httpClient);
            var updatedGift = await client.PutAsync(id, giftInput);

            return RedirectToAction(nameof(Index));
        }
    }
}
    
