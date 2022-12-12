using System;
using System.Collections.Generic;

namespace Marrodent.UprawnieniaRaporty.Models
{
    public sealed class OperatorExcelRow
    {
        //Public - string
        public string Name { get; set; }
        public string FullName { get; set; }

        //Public - datetime
        public DateTime LastLogin { get; set; }

        //Public - list
        public List<string> Permissions { get; set; }
    }
}
