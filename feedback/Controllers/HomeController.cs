using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace feedback.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexEN()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult End()
        {
            return View();
        }

        #region Index 

        [HttpPost]
        public ActionResult submit(FormCollection form)
        {
            var company = form["company"];
            var name = form["name"];
            var contact = form["contact"];
            var machine = form["machine"];
            var Q1 = int.Parse(form["check1-1"]);
            var Q2 = int.Parse(form["check1-2"]);
            var Q3 = int.Parse(form["check1-3"]);
            var Q4 = int.Parse(form["check1-4"]);
            var Q5 = int.Parse(form["check2-1"]);
            var Q6 = int.Parse(form["check2-2"]);
            var Q7 = int.Parse(form["check2-3"]);
            var Q8 = int.Parse(form["check2-4"]);
            var Q9 = int.Parse(form["check3-1"]);
            var Q10 = int.Parse(form["check4-1"]);
            var suggestion = form["suggestion"];

            var id = Guid.NewGuid();
            var num = getCount() + 1;
            var state = "Y";
            var remark = "";
            var date = DateTime.Now;

            if(Q1 == 0 | Q2 == 0 | Q3 == 0 | Q4 == 0 | Q5 == 0 | Q6 == 0 | Q7 == 0 | Q8 == 0 | Q9 == 0 | Q10 == 0)
            {
                return  Json(new { result = "none" });
            }

            string sql = String.Format("insert into feedback values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", id, num, company, name, contact, machine, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, suggestion, state, remark, date);

            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();

            ////本地数据库
            //connStr.DataSource = "(local)";
            //connStr.InitialCatalog = "drcoffee";
            //connStr.UserID = "sa";
            //connStr.Password = "123456";

            //服务器数据库
            connStr.DataSource = "(local)";
            connStr.InitialCatalog = "Feedback";
            connStr.UserID = "feedback";
            connStr.Password = "Kbs1609";

            //连接数据库
            SqlConnection connOpen = new SqlConnection();
            connOpen.ConnectionString = connStr.ConnectionString;
            connOpen.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connOpen;
            cmd.CommandText = sql;
            int row = cmd.ExecuteNonQuery();

            connOpen.Close();
            connOpen.Dispose();

            if (row == 1)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "failed" });
            }

        }
        [HttpPost]
        public int getCount()
        {

            string sql = String.Format("select count(*) from feedback where state = 'Y' ");

            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();

            ////本地数据库
            //connStr.DataSource = "(local)";
            //connStr.InitialCatalog = "drcoffee";
            //connStr.UserID = "sa";
            //connStr.Password = "123456";

            //服务器数据库
            connStr.DataSource = "(local)";
            connStr.InitialCatalog = "Feedback";
            connStr.UserID = "feedback";
            connStr.Password = "Kbs1609";

            //连接数据库
            SqlConnection connOpen = new SqlConnection();
            connOpen.ConnectionString = connStr.ConnectionString;
            connOpen.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connOpen;
            cmd.CommandText = sql;
            int row = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            connOpen.Close();
            connOpen.Dispose();

            if (row <= 0)
            {
                row = 0;
            }
            return row;
        }

        #endregion


        #region Login
        [HttpPost]
        public ActionResult login(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];

            string sql = String.Format("select * from userInfo where username = '{0}' and password = '{1}' and state = 'Y' ",username,password);

            SqlConnectionStringBuilder connStr = new SqlConnectionStringBuilder();

            ////本地数据库
            //connStr.DataSource = "(local)";
            //connStr.InitialCatalog = "drcoffee";
            //connStr.UserID = "sa";
            //connStr.Password = "123456";

            //服务器数据库
            connStr.DataSource = "(local)";
            connStr.InitialCatalog = "Feedback";
            connStr.UserID = "feedback";
            connStr.Password = "Kbs1609";

            //连接数据库
            SqlConnection connOpen = new SqlConnection();
            connOpen.ConnectionString = connStr.ConnectionString;
            connOpen.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connOpen;
            cmd.CommandText = sql;

             var result = cmd.ExecuteReader().HasRows;

            connOpen.Close();
            connOpen.Dispose();

            if (result)
            {
                return Json(new { result = "success" });
            }
            else
            {
                return Json(new { result = "failed" });
            }


        }

        #endregion
    }
}