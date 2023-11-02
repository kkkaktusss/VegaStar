using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using VegaStar;
using VegaStar.Entity;

namespace VegaStar.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        //[HttpPost]
        //[SwaggerOperation(Summary = "Создать новую сущность", Description = "Описание операции создания новой сущности.")]
        //[SwaggerResponse(201, "Создано успешно", typeof(User))] // Замените YourModel на вашу модель данных
        //public IActionResult Create([FromForm] User model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest(); // Возврат HTTP 400 Bad Request, если модель недействительна
        //    }

        //    try
        //    {
        //        // Добавление модели в контекст данных
        //        _context.Users.Add(model);

        //        // Сохранение изменений в базе данных
        //        _context.SaveChanges();

        //        // Генерация URI для созданного ресурса
        //        var locationUri = Url.Action("GetById", new { id = model.UserId }); // "GetById" - метод контроллера

        //        // Возврат HTTP 201 Created с URI и созданным ресурсом
        //        return CreatedAtAction("GetById", new { id = model.UserId }, model);
        //    }

        //    catch (Exception ex)
        //    {
        //        // Возврат HTTP 500 Internal Server Error в случае ошибки
        //        return StatusCode(500, ex.Message);
        //    }

        //    //// Здесь добавьте код для обработки и сохранения данных модели в базе данных

        //    //// Возвращаем HTTP статус "Created" и созданный объект
        //    //return CreatedAtAction(nameof( ), new { id = model.UserId }, model);
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .ToListAsync();

            //var user = _context.Users;
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetById(int id)
        {
            //var user = _context
            //    .Find<User>(id);
            var user = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string Login, [FromForm] string Password, [FromForm] string userGroupCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Создайте объект UserGroup с полученным Code
            var userGroup = new UserGroup
            {
               
                Code = userGroupCode,
                Description = "BLa bla description usergroup"
                // Другие атрибуты UserGroup
            };

            var userState = new UserState
            {
                Code = "Active" ,// Задайте нужное значение кода UserState
                Description = "Bla bla description userstate"
            };

            // Создайте объект User с участием UserGroup
            var user = new User
            {
                Login = Login,
                Password = Password,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UserGroup = userGroup,
                UserState = userState
                // Другие необходимые атрибуты
            };


            //var user = new User
            //{
            //    Login = Login,
            //    Password = Password
            //    //UserState.Code = Code
            //    // Другие необходимые атрибуты
            //};
            _context.Add(user);
            await Task.Delay(5000); // 5 секунд = 5000 миллисекунд

            // Проверка существующих значений полей в базе данных
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            var existingUser2 = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .FirstOrDefaultAsync(u => u.UserGroup.Code == user.UserGroup.Code);

            if (existingUser != null)
            {
                // Запись уже существует, выполните необходимые действия, например, верните конфликт или обновите существующую запись.
                // Возвращение конфликта:
                return Conflict("Пользователь с такими данными уже существует");
            }

            if (existingUser2 != null && existingUser2.UserGroup.Code =="Admin")
            {
                // Запись уже существует, выполните необходимые действия, например, верните конфликт или обновите существующую запись.
                // Возвращение конфликта:
                return Conflict("Вы не можете стать админом");
            }

            await _context.SaveChangesAsync();
            return Ok(user);
            //return CreatedAtRoute(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var existingUser = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .FirstOrDefaultAsync(u => u.UserId == id);
            //_context.Users.Find(id);

            if (existingUser == null)
            {
                return NotFound(); // Возвращаем 404 Not Found, если ресурс не найден.
            }

            // Применяем изменения к существующему пользователю
            if (existingUser != null && existingUser.UserState != null)
            {
                existingUser.UserState.Code = "Blocked";
            }

            //existingUser.UserState.Code = "Blocked";


            await _context.SaveChangesAsync();

            return Ok(existingUser); // Возвращаем обновленного пользователя.
        }

    }
    //    [HttpGet("{id}")]
    //public IActionResult GetUserById(int id)
    //{
    //        User user = _userService.GetById(id);
    //        if (user == null)
    //        {
    //            return NotFound(); // Возврат HTTP 404 Not Found, если пользователь не найден
    //        }
    //        return Ok(user); // Возврат HTTP 200 OK с данными пользователя
    //}



}

