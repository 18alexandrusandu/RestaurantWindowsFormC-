using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.BLL;
namespace WindowsFormsApp3.UI
{
    public partial class ReportUI : Form
    {


       public  ExportService exportService;
       public ComenziService comenziService;
        public ReportUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FileName_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportService = new ExportCsvService();
            exportService.export(textBox2.Text, textBox1.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportService = new ExportXmlService();
            exportService.export(textBox2.Text,textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string raport= comenziService.generateRaportComenzi(DateTime.Parse(dateTimePicker1.Value.ToString()),DateTime.Parse(dateTimePicker2.Value.ToString()));
            textBox2.Text = raport;
        }
    }
}
