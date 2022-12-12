using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Devart.Data.MySql;
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
                Permissions = @operator.Entitles.Select(x=>x.Entitle.Name).ToList(),
                LastLogin = GetLastLogin(sessionable, @operator)
            }).ToList();
        }

        //Private
        private string GetLastLogin(ISessionable sessionable, Operator @operator)
        {
            //Progress
            Trace.WriteLine($"{@operator.Name} - {@operator.FullName}", "Progress");

            //Result
            DateTime result = DateTime.MinValue;

            //Execute
            using (Connection connection = sessionable.Session.Login.Database.OpenConnection(DatabaseType.Operational))
            {
                using (MySqlDataReader response = (MySqlDataReader)connection.ExecuteCommand(ExecuteMode.Reader, $"SELECT Time FROM ChangeInfos WHERE Operator = {@operator.ID} AND Type = 7 ORDER BY Time DESC LIMIT 1"))
                {
                    while (response.Read())
                    {
                        if (response.IsDBNull(0)) continue;
                        result = response.GetDateTime(0);
                        break;
                    }

                    response.Close();
                }
            }

            return result == DateTime.MinValue ? "Brak logowania" : result.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
