using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
   public class ExportCsvService:ExportService
    {
      public void export(String content, String filename)
        {
            var csv = new StringBuilder();



            string[] strings = content.Split(new string[] {" "}, StringSplitOptions.None);

            foreach (string s in strings)
            {
                if(!s.Contains(":"))
                {

                    csv.Append(s);

                    if (s.Contains("\\n"))
                    {
                        csv.Append(Environment.NewLine);
                    }
                    else
                        csv.Append(",");
                }
                



            }



            File.WriteAllText(filename + ".csv", csv.ToString());









        }
    }
}
