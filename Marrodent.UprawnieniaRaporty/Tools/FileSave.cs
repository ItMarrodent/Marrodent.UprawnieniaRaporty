using System.Windows.Forms;

namespace Marrodent.UprawnieniaRaporty.Tools
{
    public static class FileSave
    {
        public static string GetPath()
        {
            using (SaveFileDialog dialog = new SaveFileDialog { Filter = "plik xlsx (*.xlsx)|*.xlsx", FileName = "RaportUprawnienOperatorow.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }

            return string.Empty;
        }
    }
}
