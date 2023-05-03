using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.UI;
using WindowsFormsApp3.BLL;
using WindowsFormsApp3.DAL;

namespace WindowsFormsApp3
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //initis
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Dao definition: Dao sunt de tip singleton

            //Services definition 

            UserServiceImpl userServiceImpl = new UserServiceImpl();
            MenuServiceImpl menuServiceImpl = new MenuServiceImpl();
            ComenziServiceImpl comenziServiceImpl = new ComenziServiceImpl();
            userServiceImpl.userDao = UserDaoImpl.getInstance();
            menuServiceImpl.menuDao = MenuDaoImpl.getInstance();
            comenziServiceImpl.comandaDao = ComandaDaoImpl.getInstance();




          //UI definition 

            LoginUI loginUi = new LoginUI();
            AdminUI adminUi = new AdminUI(userServiceImpl,menuServiceImpl);
            AngajatUI angajatUi = new AngajatUI(comenziServiceImpl,menuServiceImpl);
            ReportUI reportUi = new ReportUI();
            StatisticsUI statisticsUi = new StatisticsUI();


            reportUi.comenziService = comenziServiceImpl;
            statisticsUi.comenziService = comenziServiceImpl;

            adminUi.report = reportUi;
            adminUi.stat = statisticsUi;

            angajatUi.comenziService = comenziServiceImpl;
            angajatUi.menuService = menuServiceImpl;

            loginUi.adminUI = adminUi;
            loginUi.angajatUI = angajatUi;
            loginUi.userService = userServiceImpl;

            //Start aplication


            Application.Run(loginUi);




        }
    }
}
