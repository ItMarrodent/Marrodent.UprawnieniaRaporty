using System.Collections.Generic;
using Marrodent.UprawnieniaRaporty.Models;

namespace Marrodent.UprawnieniaRaporty.Interfaces
{
    public interface IOperatorExcelController
    {
        void GenerateFile(string path);
    }
}