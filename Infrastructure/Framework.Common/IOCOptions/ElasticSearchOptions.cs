using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.IOCOptions
{
    public class ElasticSearchOptions
    {
        public List<string> Urls { get; set; }
        public string IndexName { get; set; }
    }
}
