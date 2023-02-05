using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicro.DTO
{
    public class RegisterModel
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Mobile { get; set; }
        public string? VerifyCode { get; set; }
        public string? Salt { get; set; }
    }
}
