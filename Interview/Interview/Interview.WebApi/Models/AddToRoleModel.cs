using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.WebApi.Models
{
    public class AddToRoleModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
