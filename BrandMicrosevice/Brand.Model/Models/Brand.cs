using System;
using System.Collections.Generic;

namespace BrandMicro.Model.Models
{
    public partial class Brand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Letter { get; set; }
    }
}
