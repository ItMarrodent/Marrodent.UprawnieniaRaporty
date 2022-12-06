using System.Collections.Generic;
using System.Linq;
using Marrodent.UprawnieniaRaporty.Enova.Interfaces;
using Marrodent.UprawnieniaRaporty.Models;
using Soneta.Business.App;
using Soneta.Business.Db;

namespace Marrodent.UprawnieniaRaporty.Enova.Controllers
{
    public sealed class MapController : IMapController
    {
        public ICollection<OperatorExcelRow> GetOperatorExcelRows(ICollection<Operator> operators)
        {
            return operators.Select(@operator => new OperatorExcelRow
            {
                Name = @operator.Name, 
                FullName = @operator.FullName, 
                Permissions = @operator.Entitles.Select(x=>x.Entitle.Name).ToList()
            }).ToList();
        }
    }
}
