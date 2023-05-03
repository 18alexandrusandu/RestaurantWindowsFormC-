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
    public partial class LoginUI : Form
    {
       public UserService userService;

       public AdminUI adminUI;
       public  AngajatUI angajatUI;


        public LoginUI()
        {
            InitializeComponent();
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

   

        private void button1_Click(object sender, EventArgs e)
        {
           UserEntity user=userService.login(textBox2.Text, textBox4.Text);
            if (user!=null)
            {
                if (user.role == "ADMIN")
                {
                    adminUI.updateMenu();
                    adminUI.updateListaUtilizatori();
                     adminUI.Show();

                }
                else
                 if (user.role == "ANGAJAT")
                {

                     angajatUI.updateListMenu();
                     angajatUI.label10.Text = user.name;
                     angajatUI.Show();
                }

            }else
            {
                ErrorUI error = new ErrorUI();
                error.Show();
            }



        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
