using System;
using System.Collections.Generic;
using System.Linq;
using Marrodent.UprawnieniaRaporty.Enova.Interfaces;
using Marrodent.UprawnieniaRaporty.Models;
using Soneta.Business;
using Soneta.Business.App;

namespace Marrodent.UprawnieniaRaporty.Enova.Controllers
{
    public sealed class MapController : IMapController
    {
        //Public
        public ICollection<OperatorExcelRow> GetOperatorExcelRows(ISessionable sessionable, ICollection<Operator> operators)
        {
            return operators.Select(@operator => new OperatorExcelRow
            {
                Name = @operator.Name, 
                FullName = @operator.FullName, 
                Permissions = @operator.Entitles.Select(x=>x.Entitle.Name).ToList()
            }).ToList();
        }

        //Private
        private DateTime GetLastLogin(ISessionable sessionable, int id)
        {

        }
    }
}
