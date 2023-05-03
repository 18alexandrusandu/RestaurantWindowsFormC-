using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.BLL;
using WindowsFormsApp3.Entities;
using System.Diagnostics;

namespace WindowsFormsApp3.UI
{
    public partial class AdminUI : Form
    {




        public UserService userService;
        public MenuService menuService;
        public ReportUI report;
        public StatisticsUI stat;

        List<UserEntity> users;
        List<MenuItemEntity> menu;
        bool StocChanged = false;
        bool PretChnaged = false;
        bool NameChanged = false;

        public AdminUI()
        {

            InitializeComponent();




        }





        public AdminUI(UserService userService, MenuService menuService)
        {
            InitializeComponent();

            this.menuService = menuService;
            this.userService = userService;



            menu = menuService.getAllMenu();
            users = userService.getAllUsers();
            updateListaUtilizatori();
            updateMenu();
            // Init();



        }
        void Init()
        {
            System.Timers.Timer timer = new System.Timers.Timer(10 * 1000);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += Reshresh;
            timer.Start();
        }

        private void Reshresh(object sender, ElapsedEventArgs e)
        {
            menu = menuService.getAllMenu();
            users = userService.getAllUsers();
            updateListaUtilizatori();
            updateMenu();
        }





        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void veziRapoarteProduseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ReportUI report2=new ReportUI();
            report2.comenziService = report.comenziService;


            report2.Show();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserEntity user = userService.creazaContUser(textBox3.Text, textBox4.Text, textBox5.Text, "ANGAJAT");
            Debug.WriteLine("Nou utilizator" + user);

            users.Add(user);
            updateListaUtilizatori();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            MenuItemEntity entity = menuService.creazaProdus(textBox8.Text, double.Parse(textBox6.Text), int.Parse(numericUpDown1.Value.ToString()));


            if (entity != null)
            {

                SuccesUI succes = new SuccesUI();
                succes.label4.Text = "Crearea Produsului";
                succes.Show();
                menu.Add(entity);
                updateMenu();





            }
            else
            {
                ErrorUI error = new ErrorUI();
                error.Show();
            }



        }


       public void updateListaUtilizatori()
        {

            listView1.Items.Clear();

            users = userService.getAllUsers();

            foreach (UserEntity u in users)
            {

                ListViewItem item = new ListViewItem();

                item.SubItems.Add(u.username);
                item.SubItems.Add(u.role);
                item.Text = u.name;

                listView1.Items.Add(item);

            }


        }
       public void updateMenu()
        {


            listView2.Items.Clear();
            menu = menuService.getAllMenu();

            foreach (MenuItemEntity m in menu)
            {


                ListViewItem item = new ListViewItem();
                item.SubItems.Add(m.pret.ToString());
                item.SubItems.Add(m.stoc.ToString());
                item.Text = m.numePreparat;

                listView2.Items.Add(item);

            }
        }




        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            StocChanged = true;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            PretChnaged = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (StocChanged)
            {
                if (menuService.updateStocProdus(textBox8.Text, int.Parse(numericUpDown1.Value.ToString())))
                {
                    SuccesUI succes = new SuccesUI();
                    succes.label4.Text = "Update stock";
                    succes.Show();
                }
                else
                {
                    ErrorUI error = new ErrorUI();
                    error.Show();

                }


                StocChanged = false;

            }
            if (PretChnaged && textBox6.Text!="")
            {

                PretChnaged = false;
                if (menuService.updatePretProdus(textBox8.Text, double.Parse(textBox6.Text)))
                {
                    SuccesUI succes2 = new SuccesUI();
                    succes2.label4.Text = "Update pret";
                    succes2.Show();
                }

            }
            else
            {
                ErrorUI error = new ErrorUI();
                error.Show();

            }

            if (NameChanged && textBox7.Text!="")
            {
                if (menuService.updateNumeProdus(textBox8.Text, textBox7.Text))
                {


                    SuccesUI succes3 = new SuccesUI();
                    succes3.label4.Text = "Update name";
                    succes3.Show();

                }
                else
                {
                    ErrorUI error = new ErrorUI();
                    error.Show();

                }
                NameChanged = false;
            }
            updateMenu();
        }
    





        private void button4_Click(object sender, EventArgs e)
        {
            menuService.deleteProduct(textBox8.Text);
            updateMenu();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            NameChanged = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void veziStatisticiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            StatisticsUI stat2 = new StatisticsUI();
            stat2.comenziService = stat.comenziService;

            stat2.Show();
        }
    }
}
