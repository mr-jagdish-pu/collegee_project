using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testTaskManagement.Modal;
[Table("Users")]
[PrimaryKey(nameof(userId))]

public class UsersModal
{
   public Guid  userId { get; set; }
   [Required]
   [MaxLength(255)] // Adjust the maximum length as needed
  
   public String  userName { get; set; }
   public String hashedPassword { get; set; }
}