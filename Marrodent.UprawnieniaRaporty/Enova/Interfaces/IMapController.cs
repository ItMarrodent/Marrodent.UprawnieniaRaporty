using System.Collections.Generic;
using Marrodent.UprawnieniaRaporty.Models;
using Soneta.Business.App;

namespace Marrodent.UprawnieniaRaporty.Enova.Interfaces
{
    public interface IMapController
    {
        ICollection<OperatorExcelRow> GetOperatorExcelRows(ICollection<Operator> operators);
    }
}