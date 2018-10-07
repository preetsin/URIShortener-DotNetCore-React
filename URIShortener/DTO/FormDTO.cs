using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URIShortener.DTO
{
    public class FormDTO
    {
        [Url]
        public string Uri { get; set; }

        public string Alias { get; set; }
    }
}
