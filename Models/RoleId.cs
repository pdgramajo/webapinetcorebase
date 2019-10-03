using System.ComponentModel.DataAnnotations;
namespace webapinetcorebase.Models
{
    public class RoleId
    {
        [Required]
        public string Id { get; set; }
    }
}