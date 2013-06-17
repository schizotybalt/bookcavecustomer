using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookCaveClient.Models
{
    public class SearchParams
    {
        public string title { get; set; }
        public string author { get; set; }

        public bool hasAwards { get; set; }

        public int agelow { get; set; }
        public int agehigh { get; set; }

        public int skilllow { get; set; }
        public int skillhigh { get; set; }

        public string keywords { get; set; }

        public string isbn13 { get; set; }

    }

    
    public class UploadRequest
    {
        [DataType(DataType.MultilineText)]
        public string lexilelines { get; set; }
    }
}