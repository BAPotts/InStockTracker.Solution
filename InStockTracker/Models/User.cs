using Microsoft.AspNetCore.Identity;

namespace InStockTracker.Models
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}