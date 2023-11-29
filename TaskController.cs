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
        
        public TaskController (DataContext dataContext)
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
            if( a == null )
            {
                return Unauthorized();
            }
            else
            {
                return Ok(a);
            }
          //  List<TasksModal> allTasks =  await _dataContext.TasksModals.Where(task=> task.userId == id).ToListAsync();

            // print data
            //if(allTasks.Count > 0) { return Ok(allTasks); }
            
            //else { return NotFound("No Content found"); }
            
           
           
        }


        [HttpPost]
        [Route("post")]

        public async Task<IActionResult> AddTask([FromBody] Tasks taskModel)
        {
            //to check if userid exist or not
            var requesterId = taskModel.userId;
          //  var row = await _dataContext.UserModals.FirstOrDefaultAsync<UsersModal>

            
            var obj = new TasksModal();
            obj.taskName = taskModel.taskName;
            obj.taskDescription = taskModel.taskDescription;
            obj.dueDate = taskModel.dueDate; 
            

            _dataContext.TasksModals.Add(obj);
            _dataContext.SaveChanges();

           

          
            return Ok(obj);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
