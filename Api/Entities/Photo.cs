using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    [Table("Photo")]
    public class Photo
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}