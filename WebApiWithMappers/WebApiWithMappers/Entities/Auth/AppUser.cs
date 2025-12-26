using Microsoft.AspNetCore.Identity;

namespace WebApiWithMappers.Entities.Auth
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
