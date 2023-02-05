using System.ComponentModel.DataAnnotations;

namespace UserMicro.DTO
{
    public class PhoneValiDataAttribute : RegularExpressionAttribute
    {
        public PhoneValiDataAttribute() : base(@"^1[3458][0-9]{9}$")
        {
            ErrorMessage = "{0}不是合法的手机号，手机号是11位数字";
        }
    }
}
