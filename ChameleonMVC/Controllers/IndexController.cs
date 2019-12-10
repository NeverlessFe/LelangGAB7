using ChameleonMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChameleonMVC.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult CreateDisposal()
        {
            return View();
        }

        public ActionResult DisposalList()
        {
            return View();
        }

        public ActionResult DisposalDetail()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult DisposalApprovalList()
        {
            return View();
        }

        public ActionResult DisposalApprovalForm()
        {
            return View();
        }

        public ActionResult BidHistory()
        {
            return View();
        }

        public ActionResult GetLOBMasterData()
        {
            List<string> ModelData = new List<string>();

            DataTable dt = new DataTable();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["LelangGA"];
            string conString = mySetting.ConnectionString;
            string query = "exec [dbo].[STP_DDLMaster]";

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_DDLMaster]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 1;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<DataRow> DataRow = new List<DataRow>();
            int n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ModelData.Add(dr[0].ToString());
            }

            return Json(ModelData);
        }

        public ActionResult GenerateDisposalNumber(GenerateDisposal Model)
        {
            List<string> ModelData = new List<string>();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["LelangGA"];
            string conString = mySetting.ConnectionString;
            string query = "exec [dbo].[STP_CreateDisposal_Transc]";
            string Result;

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_CreateDisposal_Transc]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 1;

                command.Parameters.Add("@LOBGET", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LOBGET"].Value = Model.CostCenter;

                command.Parameters.Add("@PICGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@PICGet"].Value = Model.PIC;

                command.Parameters.Add("@LokasiGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@LokasiGet"].Value = Model.Lokasi;

                command.Parameters.Add("@AlasanLelangGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@AlasanLelangGet"].Value = Model.Alasan;

                command.Parameters.Add("@UsernameGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UsernameGet"].Value = "Admin";

                Result = (string)command.ExecuteScalar();

            }
            conn.Close();
            return Json(ModelData);
        }
    }
}
