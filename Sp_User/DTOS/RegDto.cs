using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sp_User.DTOS
{
    public class RegDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        [Compare(nameof(PassWord),ErrorMessage ="密码输入不一致")]
        public string ConfigrmPassWord { get; set; }
        [Required]
        public string Mobile { get; set; }
    }
}
