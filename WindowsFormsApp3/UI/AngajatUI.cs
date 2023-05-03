using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.BLL;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.UI
{
    public partial class AngajatUI : Form
    {
       public ComenziService comenziService;
       public  MenuService menuService;
        List<int> quantities;
        List<MenuItemEntity> produse;


        public AngajatUI(ComenziService comenziService, MenuService menuService)
        {
            InitializeComponent();
            produse = new List<MenuItemEntity>();
            quantities = new List<int>();
            this.comenziService = comenziService;
            this.menuService = menuService;
            updateListMenu();
            updateComenziList();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ComandaEntity cmd=comenziService.adaugaComanda(produse, quantities);
            if(cmd!=null)
            {

                textBox2.Text = cmd.pretTotal.ToString();
                SuccesUI succes = new SuccesUI();
                succes.label4.Text = "Adaugare comanda";
                succes.Show();
               
                produse = new List<MenuItemEntity>();
                quantities = new List<int>();
                updateListProduct();
                updateComenziList();

            }
            else
            {
                ErrorUI error = new ErrorUI();
                
                error.Show();



            }

         

           



        }

        private void maskedTextBox1_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }




        void updateListProduct()
        {

            listView1.Items.Clear();
            if (produse != null)
            {
                for (int i = 0; i < produse.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems.Add(produse[i].pret.ToString());
                    if (quantities != null)
                        item.SubItems.Add(quantities[i].ToString());
                    item.Text = produse[i].numePreparat;

                    listView1.Items.Add(item);
                }
            }



        }
       public void updateListMenu()
        {
            List<MenuItemEntity> menu = menuService.getListaAllProducts();
            listView2.Items.Clear();
            for (int i = 0; i < menu.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(menu[i].pret.ToString());
                item.SubItems.Add(menu[i].stoc.ToString());
                item.Text = menu[i].numePreparat;

                listView2.Items.Add(item);
            }




        }
        public void updateComenziList()
        {

            List<ComandaEntity> comenzi = comenziService.getAllComenzi();

           



            listView3.Items.Clear();
            for (int i = 0; i < comenzi.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(comenzi[i].status.ToString());
                item.SubItems.Add(comenzi[i].pretTotal.ToString());
                item.SubItems.Add(comenzi[i].dateAndHour.ToString());


                string compoundedComand = "";
                int j = 0;
                foreach(MenuItemEntity mie in comenzi[i].produseList)

                {

                    compoundedComand+= mie.ToString() + " X" + comenzi[i].quantities[j] + ",";
                        j++;
                }
                

                item.SubItems.Add(compoundedComand);
               


                item.Text = comenzi[i].id.ToString();

                listView3.Items.Add(item);
            }




        }





        private void button2_Click(object sender, EventArgs e)
        {


            MenuItemEntity m = menuService.getMenuItemForCommand(textBox5.Text,int.Parse(numericUpDown1.Value.ToString()));
             if(m!=null)
            {

                produse.Add(m);
                quantities.Add(int.Parse(numericUpDown1.Value.ToString()));
                updateListProduct();




            }
            else
            {
                ErrorUI error = new ErrorUI();
                error.Show();
            }
            


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
    
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AngajatUI_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comenziService.modifyStatusComanda(long.Parse(textBox3.Text), comboBox1.Text);
            updateComenziList();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComandaEntity comanda= comenziService.getComandaById(long.Parse(textBox3.Text));
            if (comanda != null)
            {
                comboBox1.Text = comanda.status;
                dateTimePicker1.Value = comanda.dateAndHour;
                textBox2.Text = comanda.pretTotal.ToString();

                produse = comanda.produseList;
                quantities = comanda.quantities;

                updateListProduct();
            }else
            {
                ErrorUI error = new ErrorUI();
                error.Show();
            }



        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            produse = new List<MenuItemEntity>();
            quantities = new List<int>();
            updateListProduct();
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Text = "";

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
