using System;
using System.Collections.Generic;

namespace UserMicro.Model.Models
{
    public partial class User
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateTime { get; set; }
        public string Salt { get; set; }
    }
}
