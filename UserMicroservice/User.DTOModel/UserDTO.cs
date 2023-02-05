using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace UserMicro.DTO
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Mobile { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Gender { get; set; }

        public int? UserType { get; set; }

        public string? ImageUrl { get; set; }

    }
}
