using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRTesting.Data;
using SignalRTesting.Hubs;
using SignalRTesting.Models;
using SignalRTesting.Models.ViewModel;
using System.Diagnostics;
using System.Security.Claims;

namespace SignalRTesting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHallowsHub;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> deathlyHallowsHub, ApplicationDbContext dbContext,
            IHubContext<OrderHub> orderHub)
        {
            _logger = logger;
            _deathlyHallowsHub = deathlyHallowsHub;
            _dbContext = dbContext;
            _orderHub = orderHub;
        }

        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random random = new Random();
            int index = random.Next(name.Length);

            Order order = new Order
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };
            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            await _orderHub.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }

        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _dbContext.Orders.ToList();
            return Json(new { data = productList });
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DeathlyHallowRace.ContainsKey(type))
            {
                SD.DeathlyHallowRace[type]++;
            }
            await _deathlyHallowsHub.Clients.All.SendAsync("updateDeathlyHallowsCount",
                SD.DeathlyHallowRace[SD.Cloak],
                SD.DeathlyHallowRace[SD.Stone],
                SD.DeathlyHallowRace[SD.Wand]);
            return Accepted();
        }
        public IActionResult Index()
        {
            Console.WriteLine(HubConnections.Users);
            return View();
        }
        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult DeathlyHallowRace()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }
        public IActionResult BasicChat()
        {
            return View();
        }
        [Authorize]
        public IActionResult Chat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVM = new ChatVM()
            {
                Rooms = _dbContext.ChatRooms.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId
            };
            return View(chatVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}