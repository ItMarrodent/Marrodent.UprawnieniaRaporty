using System.Collections.Generic;

namespace Marrodent.UprawnieniaRaporty.Models
{
    public sealed class OperatorExcelRow
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
