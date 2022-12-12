using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                LastLogin = GetLastLogin(sessionable, @operator.ID)
            }).ToList();
        }

        //Private
        private DateTime GetLastLogin(ISessionable sessionable, int id)
        {
            //Result
            DateTime result = DateTime.MinValue;

            //Execute
            using (Connection connection = sessionable.Session.Login.Database.OpenConnection(DatabaseType.Operational))
            {
                using (MySqlDataReader response = (MySqlDataReader)connection.ExecuteCommand(ExecuteMode.Reader, $"SELECT MAX(Time) FROM ChangeInfos WHERE Operator = {id} AND Type = 7"))
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

            return result;
        }
    }
}
