using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sp_User.DTOS
{
    public class RegDto
    {
        [Required(ErrorMessage = "姓名必填")]
        public string UserName { get; set; }
        [MaxLength(20,ErrorMessage = "密码大于20位"), MinLength(5,ErrorMessage = "密码小于5位")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "确认密码必填")]
        [Compare(nameof(PassWord),ErrorMessage ="两次密码输入不一致")]
        public string ConfigrmPassWord { get; set; }
        [Required(ErrorMessage = "手机号码必填")]
        [RegularExpression(@"^1[3458][0-9]{9}$",ErrorMessage ="手机号码格式不正确")]
        public string Mobile { get; set; }
    }
}
