using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace UserMicro.DTO
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserInfoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string? Name { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        [Compare("NewPassword", ErrorMessage = "两次密码输入不一致")]
        public string? Password { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入新密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string? NewPassword { get; set; }

        [Display(Name = "手机号")]
        [Required(ErrorMessage = "手机号不能为空")]
        [PhoneValiData]
        public string? Mobile { get; set; }

        public DateTime Birthday { get; set; }
        public string? BirthdayRemark
        {
            get
            {
                return Birthday.ToString("yyyy-MM-dd");
            }
        }

    }
}
