using DessertsKoma_Customers.Models;
using DessertsKoma_Customers.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DessertsKoma_Customers.Controllers
{
    public class HomeController : Controller
    {
        private readonly DessertsKomaContext _context;
        private readonly TopService _topService;
        private readonly DessertsInBasketService _dessertsInBasketService;
        private readonly DessertsTypesService _dessertsTypesService;
        private readonly DessertsInOrderService _dessertsInOrderService;
        private static byte[] userImage;
        private static string userImageName;
        private static string userImageShortName;

        public HomeController(DessertsKomaContext context, TopService topService, DessertsInBasketService dessertsInBasketService, DessertsTypesService dessertsTypesService, DessertsInOrderService dessertsInOrderService)
        {
            _context = context;
            _topService = topService;
            _dessertsInBasketService = dessertsInBasketService;
            _dessertsTypesService = dessertsTypesService;
            _dessertsInOrderService = dessertsInOrderService;
        }

        public IActionResult GetImage(long id)
        {
            var image = _context.Изображения.FirstOrDefault(d => d.Номер == id);
            if (image.Данные != null)
            {
                return File(image.Данные, "image/png"); // или другой MIME-тип изображения
            }
            return NotFound();
        }

        public IActionResult GetIsUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            if (nameClaim == null)
                return Content("false");
            else
                return Content("true");
        }

        public IActionResult GetIsEmployee()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            if (nameClaim != null)
            {
                var user = _context.Пользователи
                    .Where(x => x.Логин == nameClaim.Value)
                    .Include(x => x.РольNavigation)
                    .First();
                if (user.РольNavigation.Номер == 1 || user.РольNavigation.Номер == 2)
                    return Content("true");
                else
                    return Content("false");
            }
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult GetDesserts()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var desserts = _dessertsInBasketService.GetДесертыВКорзине(
                    _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер);
            var newDesserts = desserts.Select(x => x.ДесертNavigation.Номер).ToList();
            return Json(newDesserts);
        }

        [HttpGet]
        public IActionResult GetDiscount()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var discount = _dessertsInBasketService.GetСкидка(
                    _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер);
            return Json(discount);
        }

        [HttpGet]
        public IActionResult GetIngredientName(long ing)
        {
            var ingredient = _context.Ингредиенты.FirstOrDefault(x => x.Номер == ing).Название;
            return Content(ingredient);
        }

        [HttpGet]
        public IActionResult GetIngredientsInDessert()
        {
            long des = Convert.ToInt64(Request.Query["des"]);
            var ingredients = _context.ИнгредиентыВдесерте
                .Where(x => x.Десерт == des)
                .Include(d => d.ИнгредиентNavigation)
                .ToList();
            var result = "Состав: ";
            foreach(var item in ingredients)
            {
                result += item.ИнгредиентNavigation.Название + ", ";
            }
            string newResult = result.Substring(0, result.Length - 2);
            return Content(newResult);
        }

        [HttpGet]
        public IActionResult GetBasketCost()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var model = _dessertsInBasketService.GetДесертыВКорзине(
                    _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер);
            double cost = 0;
            model.ForEach(x => cost += Convert.ToDouble(x.ДесертNavigation.Стоимость * x.Количество));
            int discount = _dessertsInBasketService.GetСкидка(_context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер);
            return Json(new { Cost = cost, Discount = discount });
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                DessertsModel = _topService.GetTopДесерты(),
                TypesModel = _dessertsTypesService.GetТипыДесертов()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(long dessertId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            if (nameClaim != null)
            {
                var user = _context.Пользователи.FirstOrDefault(u => u.Логин == nameClaim.Value);
                var usid = user.Номер;
                var bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == usid);
                if (bask == null)
                {
                    var basket = new Корзина
                    {
                        Пользователь = usid
                    };
                    _context.Корзина.Add(basket);
                    _context.SaveChanges();
                    bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == usid);
                }
                var des = new ДесертыВкорзине
                {
                    Корзина = bask.Номер,
                    Десерт = dessertId,
                    Количество = 1
                };
                _context.ДесертыВкорзине.Add(des);
                _context.SaveChanges();
                return Content("true");
            }
            else
            {
                return StatusCode(401);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dessert()
        {
            long des = Convert.ToInt64(Request.Query["id"]);
            var desserts = _context.Десерты
                .Where(x => x.Номер == des)
                .Include(d => d.ИзображениеNavigation)
                .Include(d => d.ТипNavigation)
                .ThenInclude(t => t.ЕдиницаИзмеренияNavigation)
                .ToList();
            return View(desserts);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Пользователи model, bool RememberMe = false)
        {
            var user = _context.Пользователи.FirstOrDefault(u => u.Логин == model.Логин && u.Пароль == model.Пароль);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль! ");
                return View(model);
            }
            else
            {
                var claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Name, user.Логин)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = RememberMe 
                };
                await HttpContext.SignInAsync("Cookie", claimsPrincipal, authProperties);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Пользователи model, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                if (model.Пароль != model.ПодтверждениеПароля)
                {
                    ModelState.AddModelError("ПодтвердитьПароль", "Пароли не совпадают");
                    return View(model);
                }

                _context.Пользователи.Add(model);
                _context.SaveChanges();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Логин)
                };
                var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe
                };

                await HttpContext.SignInAsync("Cookie", claimPrincipal, authProperties);

                string cookieValue = HttpContext.Request.Cookies["name"];
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Desserts()
        {
            var model = new MyViewModel {
                TypesModel = _dessertsTypesService.GetТипыДесертов(),
                DessertsModel = _dessertsTypesService.GetAllДесерты(),
                TypesCountsModel = _dessertsTypesService.GetТипыДесертовCount(),
                MinCostModel = _dessertsTypesService.GetНаимСтоимость(),
                MaxCostModel = _dessertsTypesService.GetНаибСтоимость(),
                AllDessertsCount = _dessertsTypesService.GetAllДесертыCount()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Desserts(long dessertId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            if (nameClaim != null)
            {
                var user = _context.Пользователи.FirstOrDefault(u => u.Логин == nameClaim.Value);
                var usid = user.Номер;
                var bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == usid);
                if (bask == null)
                {
                    var basket = new Корзина
                    {
                        Пользователь = usid
                    };
                    _context.Корзина.Add(basket);
                    _context.SaveChanges();
                    bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == usid);
                }
                var des = new ДесертыВкорзине
                {
                    Корзина = bask.Номер,
                    Десерт = dessertId,
                    Количество = 1
                };
                _context.ДесертыВкорзине.Add(des);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookie");
            return Content("true");
        }

        public IActionResult Basket()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var model = _dessertsInBasketService.GetДесертыВКорзине(
                    _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер);
            return View(model);
        }

        public IActionResult Account()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var user = _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value).Номер;
            var orders = _dessertsInOrderService.GetЗаказы(user);
            foreach (var order in orders)
            {
                if (order.ДатаВыдачи < DateTime.Now.Date && order.СтатусNavigation.Номер < 5)
                {
                    order.Статус = 6;
                }
            }
            _context.SaveChanges();
            var model = new OrdersViewModel
            {
                OrdersModel = _dessertsInOrderService.GetЗаказы(user),
                DessertsModel = _dessertsInOrderService.GetДесертыВЗаказах(user),
                UserModel = _context.Пользователи.Include(u => u.ИзображениеNavigation).Include(u => u.РольNavigation).FirstOrDefault(x => x.Логин == nameClaim.Value)
            };
            
            return View(model);
        }

        public IActionResult GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            Пользователи user = _context.Пользователи.Include(u => u.ИзображениеNavigation).Include(u => u.РольNavigation).FirstOrDefault(x => x.Логин == nameClaim.Value);
            var result = new
            {
                Image = user.Изображение,
                Name = user.Имя,
                Login = user.Логин,
                Phone = user.НомерТелефона,
                Password = user.Пароль
            };

            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteDessertInBasket(int id)
        {
            var dessert = _context.ДесертыВкорзине.FirstOrDefault(u => u.Номер == id);
            if (dessert == null)
            {
                return NotFound();
            }
            _context.ДесертыВкорзине.Remove(dessert);
            _context.SaveChanges();

            return Content("true");
        }

        [HttpPost]
        public IActionResult ChangeDessertCountInBasket(int id, int count)
        {
            var dessertInBask = _context.ДесертыВкорзине.FirstOrDefault(u => u.Номер == id);

            if (dessertInBask != null)
            {
                var dessert = _context.Десерты.FirstOrDefault(u => u.Номер == dessertInBask.Десерт);
                dessertInBask.Количество = count;
                _context.SaveChanges();

                return Json(count * dessert.Стоимость);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult SetUserImage(IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string fileName = file.FileName;
                    string shortFileName = Path.GetFileNameWithoutExtension(fileName); ;
                    userImage = fileBytes;
                    userImageName = fileName;
                    userImageShortName = shortFileName;

                    return Ok();
                }
            }
            else
            {
                return BadRequest("Файл не был предоставлен");
            }
        }

        public async void ChangeClaim(string login)
        {
            await HttpContext.SignOutAsync("Cookie");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookie", claimsPrincipal);
        }

        [HttpPost]
        public IActionResult ChangeUserData(string image, string name, string login, string password, string phone)
        {
            string can = "no";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var user = _context.Пользователи.FirstOrDefault(u => u.Логин == nameClaim.Value);

            if (image == "yes")
            { 
                var imag = _context.Изображения.FirstOrDefault(u => u.Номер == user.Изображение);
                var img = new Изображения
                {
                    ИмяФайла = userImageName,
                    Название = userImageShortName,
                    Данные = userImage
                };
                _context.Изображения.Add(img);
                _context.SaveChanges();
                user.Изображение = img.Номер;
                _context.Изображения.Remove(imag);
                _context.SaveChanges();

                can = "yes";
            }

            if (name != user.Имя)
            {
                user.Имя = name;
                can = "yes";
            }

            if (login != user.Логин)
            {
                user.Логин = login;
                can = "yes";
                ChangeClaim(login);
            }

            if (!string.IsNullOrEmpty(password))
            {
                user.Пароль = password;
                can = "yes";
            }

            if (phone != user.НомерТелефона)
            {
                user.НомерТелефона = phone;
                can = "yes";
            }

            if (can == "yes")
                 _context.SaveChanges();

            return Content(can);
        }


        [HttpGet]
        public IActionResult GetDiscountFromBD(string phrase)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var user = _context.Пользователи.FirstOrDefault(u => u.Логин == nameClaim.Value);
            var bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == user.Номер);

            var sale = _dessertsInBasketService.GetСкидка(user.Номер);

            var discount = _context.Скидка.FirstOrDefault(u => u.Фраза == phrase);

            if (discount != null)
            {
                if (sale != 0)
                {
                    if (sale > discount.Процент)
                        return Json(sale);
                }

                bask.Скидка = discount.Номер;
                _context.SaveChanges();
                return Json(discount.Процент);
            }

            return NotFound();
        }

        public IActionResult Checkout()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var model = _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(string date, string phone, double cost)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var nameClaim = identity?.FindFirst(ClaimTypes.Name);
            var model = _context.Пользователи.FirstOrDefault(x => x.Логин == nameClaim.Value);

            if (phone != model.НомерТелефона)
            {
                model.НомерТелефона = phone;
                _context.SaveChanges();
            }
            var order = new Заказы();
            order.Пользователь = model.Номер;
            order.ДатаЗаказа = DateTime.Now.Date;
            order.Статус = 1;
            order.ДатаВыдачи = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            order.Стоимость = cost;
            if (_dessertsInBasketService.GetСкидкаNumber(model.Номер) == 0)
                order.Скидка = null;
            else
                order.Скидка = _dessertsInBasketService.GetСкидкаNumber(model.Номер);

            _context.Заказы.Add(order);
            _context.SaveChanges();

            var dess = _dessertsInBasketService.GetДесертыВКорзине(model.Номер);

            foreach (var des in dess)
            {
                var desInOrder = new ДесертыВзаказе
                {
                    Десерт = des.ДесертNavigation.Номер,
                    Заказ = order.Номер,
                    Количество = des.Количество
                };
                _context.ДесертыВзаказе.Add(desInOrder);
                _context.ДесертыВкорзине.Remove(des);
                _context.SaveChanges();
            }

            var bask = _context.Корзина.FirstOrDefault(x => x.Пользователь == model.Номер);
            bask.Скидка = null;
            _context.SaveChanges();
            return Ok();
        }
    }
}
