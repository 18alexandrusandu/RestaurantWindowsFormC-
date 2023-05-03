using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
   public class ExportXmlService:ExportService

    {
        public void export(String content, String filename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml-stylesheet type=\"text/xsl\" href=\"dco.xsl\"?>");



            string[] strings = content.Split(new string[] { "\\n" }, StringSplitOptions.None);


            bool tagStarted = false;
            string StartedTag = "";

            foreach (string line in strings)
            {

                string[] strings2 = line.Split(new string[] { " " }, StringSplitOptions.None);

                foreach (string s in strings) { 

                    if (s.Contains(":"))
                {

                    if (tagStarted == false)
                    {
                        stringBuilder.Append("<");
                        string saved = s.Split(':')[0];
                        stringBuilder.Append(saved);
                        stringBuilder.Append(">");
                        StartedTag = saved;
                        tagStarted = true;


                    }else
                    {
                        tagStarted = false;

                        stringBuilder.Append("</");
                        stringBuilder.Append(StartedTag);
                        stringBuilder.Append(">");
                        stringBuilder.Append(Environment.NewLine);

                        stringBuilder.Append("<");
                        string saved = s.Split(':')[0];
                        stringBuilder.Append(saved);
                        stringBuilder.Append(">");
                        StartedTag = saved;
                        tagStarted = true;

                    }

                }else
                    {
                        stringBuilder.Append(s);


                    }





                    

                    if (s.Contains("\\n"))
                    {
                        stringBuilder.Append(Environment.NewLine);
                    }
                    else
                        stringBuilder.Append(",");
                }




            }



            XDocument doc = XDocument.Parse(stringBuilder.ToString());
            doc.Save(filename+".xml");

        }

    }
}
