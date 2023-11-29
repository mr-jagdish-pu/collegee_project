using Microsoft.EntityFrameworkCore;
using testTaskManagement.Modal;

namespace testTaskManagement.EfContext;

public class DataContext :DbContext
{
    public DataContext(DbContextOptions <DataContext> options) : base (options){}
    
    public DbSet<TasksModal> TasksModals { get; set; }
    public DbSet<UsersModal> UserModales { get; set; }

}