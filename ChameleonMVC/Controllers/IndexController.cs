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
        LelangGAEntities1 objEntities = new LelangGAEntities1();
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

        public ActionResult CreateDisposal(string NoDisposal, string Status)
        {
            ViewBag.NoDisposal = NoDisposal;
            ViewBag.Status = Status;
            return View();
        }

        public ActionResult DisposalList()
        {
            return View(objEntities.vw_DisposalList);
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

        public ActionResult AddAssetItem(AssetAdd Model)
        {
            List<string> ModelData = new List<string>();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["LelangGA"];
            string conString = mySetting.ConnectionString;
            string query = "exec [dbo].[STP_CreateDisposal_Transc]";
            string Result;
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_CreateDisposal_Transc]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 2;

                command.Parameters.Add("@AssetNumberGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@AssetNumberGet"].Value = Model.AssetNumber;

                command.Parameters.Add("@NoDisposalGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoDisposalGet"].Value = Model.NoDisposal;

                command.Parameters.Add("@UsernameGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UsernameGet"].Value = "Admin";

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

        public ActionResult SetAssetDataIntoTable(AssetAdd Model)
        {
            List<string> ModelData = new List<string>();

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["LelangGA"];
            string conString = mySetting.ConnectionString;
            string query = "exec [dbo].[STP_CreateDisposal_Transc]";
            string Result;
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_CreateDisposal_Transc]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 3;

                command.Parameters.Add("@NoDisposalGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoDisposalGet"].Value = Model.NoDisposal;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<string> ModelDataDT = new List<string>();
            List<DataRow> DataRow = new List<DataRow>();
            int n = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ModelDataDT.Add(dr[0].ToString());
            }

            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
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

                command.Parameters.Add("@RuangGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@RuangGet"].Value = Model.Ruang;

                command.Parameters.Add("@UsernameGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@UsernameGet"].Value = "Admin";

                Result = (string)command.ExecuteScalar();

            }
            conn.Close();
            ModelData.Add(Result);
            return Json(ModelData);
        }

        public ActionResult DeleteAsset(AssetAdd Model)
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
                command.Parameters["@Option"].Value = 4;

                command.Parameters.Add("@NomorAktivaTetap", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NomorAktivaTetap"].Value = Model.NomorAktivaTetap;
                
                command.Parameters.Add("@NoDisposalGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoDisposalGet"].Value = Model.NoDisposal;

                Result = (string)command.ExecuteScalar();
            }
            conn.Close();

            DataTable dt = new DataTable();
            
            conn.Open();
            using (SqlCommand command = new SqlCommand("[dbo].[STP_CreateDisposal_Transc]", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Option", System.Data.SqlDbType.Int);
                command.Parameters["@Option"].Value = 3;

                command.Parameters.Add("@NoDisposalGet", System.Data.SqlDbType.NVarChar);
                command.Parameters["@NoDisposalGet"].Value = Model.NoDisposal;

                SqlDataAdapter dataAdapt = new SqlDataAdapter();
                dataAdapt.SelectCommand = command;

                dataAdapt.Fill(dt);
            }
            conn.Close();

            List<string> ModelDataDT = new List<string>();
            List<DataRow> DataRow = new List<DataRow>();
            int n = 0;

            foreach (DataRow dr in dt.Rows)
            {
                ModelDataDT.Add(dr[0].ToString());
            }

            List<DataRow> objAR = dt.AsEnumerable().ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return Json(rows);
        }
    }
}
