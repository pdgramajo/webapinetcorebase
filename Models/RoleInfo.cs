using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapinetcorebase.Models
{
    public class RoleInfo
    {
        [Required]
        public string Name { get; set; }
    }
}