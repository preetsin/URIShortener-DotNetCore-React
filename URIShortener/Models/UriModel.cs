using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URIShortener.Models
{
    public class UriModel
    {
        public int Id { get; set; }
        public string Original { get; set; }
        public string Alias { get; set; }
    }
}
