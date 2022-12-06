using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Marrodent.UprawnieniaRaporty.Controllers;
using Marrodent.UprawnieniaRaporty.Enova.Buttons;
using Marrodent.UprawnieniaRaporty.Enova.Controllers;
using Marrodent.UprawnieniaRaporty.Models;
using Marrodent.UprawnieniaRaporty.Tools;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.Db;

[assembly: Worker(typeof(OperatorButton), typeof(Operators))]

namespace Marrodent.UprawnieniaRaporty.Enova.Buttons
{
    public sealed class OperatorButton
    {
        [Action("Wyeksportuj uprawnienia operatorów", Mode = ActionMode.Progress, Icon = ActionIcon.Book, Priority = 1, Target = ActionTarget.Menu | ActionTarget.ToolbarWithText)]
        public void Export(Context cx)
        {
            //Get - file save path
            string path = FileSave.GetPath();
            if(string.IsNullOrEmpty(path)) return;

            //Get - data
            ICollection<OperatorExcelRow> data = new MapController().GetOperatorExcelRows(((View)cx[typeof(View)]).Cast<Operator>().Where(x=>!x.Locked).ToList());

            //Save - excel
            new OperatorExcelController(data).GenerateFile(path);

            //Open - excel
            Process.Start(path);
        }
    }
}
