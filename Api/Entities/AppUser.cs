
using System.ComponentModel.DataAnnotations;

namespace Api.Entities
{
    public class AppUser
    {
        
        public int Id { get; set; }
       
        public string UserName { get; set; }
        public Nullable<System.DateTime>CreatedDate{get;set;}

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }

}



