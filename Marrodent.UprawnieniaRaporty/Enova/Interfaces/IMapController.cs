using System.Collections.Generic;
using Marrodent.UprawnieniaRaporty.Models;
using Soneta.Business;
using Soneta.Business.App;

namespace Marrodent.UprawnieniaRaporty.Enova.Interfaces
{
    public interface IMapController
    {
        ICollection<OperatorExcelRow> GetOperatorExcelRows(ISessionable sessionable, ICollection<Operator> operators);
    }
}