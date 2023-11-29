using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json.Nodes;
using testTaskManagement.EfContext;
using testTaskManagement.Modal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testTaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public TaskController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }




        /*   [HttpGet]
           public IEnumerable<string> Get()
           {
               return new string[] { "value1", "value2" };
           }
        */

        [HttpGet("taskId")]
        public async Task<IActionResult> GetAllTaskOfUser(Guid UserId)
        {
            var a = await _dataContext.UserModales.FindAsync
                (UserId);
            if (a == null)
            {
                var ab = new { Error = "Provided ID is not registered" };
                return Unauthorized(ab);

            }

            List<TasksModal> allUserTask =
                await _dataContext.TasksModals.Where(task => task.userId == UserId).ToListAsync();
            return allUserTask.Count > 0 ? Ok(allUserTask) : NotFound();





        }


        [HttpPost]
        [Route("post")]

        public async Task<IActionResult> AddTask([FromBody] Tasks taskModel)
        {
            //to check if userid exist or not
            var requesterId = taskModel.userId;
            var a = await _dataContext.UserModales.FindAsync
                (requesterId);
            if (a == null)
            {
                var ab = new { Error = "Provided ID is not registered" };
                return Unauthorized(ab);

            }
            //  var row = await _dataContext.UserModals.FirstOrDefaultAsync<UsersModal>


            var obj = new TasksModal();
            obj.taskId = Guid.NewGuid();
            obj.taskName = taskModel.taskName;
            obj.taskDescription = taskModel.taskDescription;
            obj.dueDate = taskModel.dueDate;
            obj.userId = taskModel.userId;


            _dataContext.TasksModals.Add(obj);
            _dataContext.SaveChanges();




            return Ok(obj);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Put( Guid taskId,  [FromBody] Tasks taskModel)
        {
            var requesterId = taskModel.userId;
            var user = await _dataContext.UserModales.FindAsync
                (requesterId);
            if (user == null)
            {
                var ab = new { Error = "Provided ID is not registered" };
                return Unauthorized(ab);

            }

            var task = await _dataContext.TasksModals.FindAsync(taskId);
            if (task == null)
                return NotFound();
            
      
            task.userId = task.userId;
            task.taskDescription = taskModel.taskDescription;
            task.taskName = taskModel.taskName;
            task.dueDate = taskModel.dueDate;
            _dataContext.TasksModals.Update(task);

            _dataContext.SaveChanges();
            return Ok();




        }


        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid userId, Guid taskId)
        {
            var requesterId = userId;
            var user = await _dataContext.UserModales.FindAsync(requesterId);

            if (user == null)
            {
                var ab = new { Error = "Provided ID is not registered" };
                return Unauthorized(ab);
            }

            var task = await _dataContext.TasksModals.FindAsync(taskId);

            if (task == null)
                return NotFound();

            _dataContext.TasksModals.Remove(task);
            await _dataContext.SaveChangesAsync();

            return Ok("Requested data successfully removed");
        }
    }

    [Route("user/auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserAuthController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> loginUser( [FromBody] Users users)
        {
           
            var row = await _dataContext.UserModales.Where(user => user.userName == users.userName && user.hashedPassword == users.password).FirstOrDefaultAsync();
         return   row == null ? Unauthorized() : Ok(row);


        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> createUser([FromBody] Users user)
        {
         var   dc = _dataContext;
       var isExist=  await  dc.UserModales.AnyAsync(u => u.userName == user.userName);
       if (isExist)
       {
           return Unauthorized("Username exist there");
       }   
            
            var userData = new UsersModal
            {
                userId = Guid.NewGuid(),
                userName = user.userName,
                hashedPassword = user.password
            };
         await   dc.UserModales.AddAsync(userData);
         await   dc.SaveChangesAsync();
            return Ok(userData);
        }


    }
    

}
