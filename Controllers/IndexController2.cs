using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.IO;
using ApiPayment;
using ApiPayment.Web;
using ApiPayment.Common;

namespace Commic.Controllers
{
    public class IndexController : Controller
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        static int result;
        static int scopeid;
        static string chkDelevaryType;
        static string TotalAmount;
        static string TrasactionNumber;
        static string Vcode;
        static string res;
        static string chkmail;
        static string Name;
        static string address;
        static string Contact;
        static string mailID;
        static string pathToFiles;
        #endregion

        #region Sobnner Pages Controller

        [HttpGet]
        public ActionResult Index()
        {
            //if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            //{
            //    txtUserName.Text = Request.Cookies["UserName"].Value;
            //    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
            //}
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
            objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
            objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
            objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
            objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
            objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
            objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
            objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult Index(string id)
        {
            if (id == "b1")
            {
                return View("");
            }
            else if (id == "b2")
            {
                return View("FormuleBSM");
            }
            else if (id == "b3")
            {
                return View("FormuleMB");
            }
            else if (id == "b4")
            {
                return View("FormuleMiniAndMaxi");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult PertABuller()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult PertABuller(string sb, string sp, string duree, string deltype, string delprice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1)
        {
            try
            {

                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                //DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, UserName, Address, Contact, DelevaryType });

                dt.Rows.Add(totamt, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", Convert.ToDouble(delprice));

                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", Convert.ToInt32(postcode));
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Convert.ToInt64(Telephone));

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", Convert.ToInt32(postcode1));
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Convert.ToInt64(Telephone1));
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Subscription");
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "sabnner";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    ////objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    ////objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();

                    //////string strResult = CallPaymentGatwey("Request");
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    ////objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    ////objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();

                    //////string strResult = CallPaymentGatwey("Request");
                    //return View(objCommic);
                }


            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult FormuleBSMCommon()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult FormuleBSMCommon(string sb, string sp, string duree, string deltype, string delprice, string CatName, string ComicName, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                //DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, UserName, Address, Contact, DelevaryType });

                dt.Rows.Add(totamt, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                //cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                //cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                //cmd.Parameters.AddWithValue("@Gender", gender);
                //cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                //cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                //cmd.Parameters.AddWithValue("@OfferName", ofrname);
                //cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                //cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                //cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Subscription");
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "sabnner";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;

                   // return RedirectToAction("CallPaymentGatweyApi", "Index", new { RequestType = "Request" });
                    return Json("True");
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult FormuleMBCommon()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult FormuleMBCommon(string sb, string sp, string duree, string deltype, string delprice, string kidCatName, string kidComicName, string gender, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                //DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, UserName, Address, Contact, DelevaryType });

                dt.Rows.Add(totamt, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                //cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                //cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                //cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                //cmd.Parameters.AddWithValue("@OfferName", ofrname);
                //cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                //cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                //cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Subscription");
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "sabnner";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                    //objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                    //objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult FormuleMiniNMaxi()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult FormuleMiniNMaxi(string sb, string sp, string duree, string deltype, string delprice, string CatName, string kidCatName, string ComicName, string kidComicName, string gender, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                //DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, UserName, Address, Contact, DelevaryType });

                dt.Rows.Add(totamt, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                //cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                //cmd.Parameters.AddWithValue("@OfferName", ofrname);
                //cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                //cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                //cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Subscription");
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "sabnner";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region Offer Pages Controller

        [HttpGet]
        public ActionResult VoucherPageForPertaBuller()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult VoucherPageForPertaBuller(string sb, string sp, string duree, string deltype, string delprice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1, string ofrFname, string ofrname, string ofremail, string ofrmsg, string ofrsurname, string chkBymail)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn ChkByMail = new DataColumn("ChkByMail", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                DataColumn VoucharCode = new DataColumn("VoucharCode", typeof(System.String));
                DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, ChkByMail, UserName, Address, Contact, VoucharCode, EmailId, DelevaryType });

                dt.Rows.Add(totamt, chkBymail, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, tmp, ofremail, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                cmd.Parameters.AddWithValue("@OfferName", ofrname);
                cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "offer");
                cmd.Parameters.AddWithValue("@OfferCode", tmp);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "Offer";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    ////objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    ////objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    ////objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    ////objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult VoucherPageForBSM()
        {
            try
            {
                if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
                {
                    // RouteValueDictionary rvd = new RouteValueDictionary();
                    string strId = "0";
                    if (Request.QueryString["id"] != null)
                        strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                    // rvd.Add("id", strId );
                    return RedirectToAction("LoginPage", new { id = strId });
                    // return View("Login", new  {id=strId  });
                }
                else
                {
                    Commic.Models.Commic objCommic = new Models.Commic();
                    objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult VoucherPageForBSM(string sb, string sp, string duree, string deltype, string delprice, string CatName, string ComicName, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1, string ofrFname, string ofrname, string ofremail, string ofrmsg, string ofrsurname, string chkBymail)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn ChkByMail = new DataColumn("ChkByMail", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                DataColumn VoucharCode = new DataColumn("VoucharCode", typeof(System.String));
                DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, ChkByMail, UserName, Address, Contact, VoucharCode, EmailId, DelevaryType });

                dt.Rows.Add(totamt, chkBymail, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, tmp, ofremail, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                //cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                //cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                //cmd.Parameters.AddWithValue("@Gender", gender);
                //cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                cmd.Parameters.AddWithValue("@OfferName", ofrname);
                cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Offer");
                cmd.Parameters.AddWithValue("@OfferCode", tmp);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "Offer";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //return View(objCommic); 
                }
                else
                {
                    return Json("False");

                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult VoucherPageForMB()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult VoucherPageForMB(string sb, string sp, string duree, string deltype, string delprice, string kidCatName, string kidComicName, string gender, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1, string ofrFname, string ofrname, string ofremail, string ofrmsg, string ofrsurname, string chkBymail)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn ChkByMail = new DataColumn("ChkByMail", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                DataColumn VoucharCode = new DataColumn("VoucharCode", typeof(System.String));
                DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, ChkByMail, UserName, Address, Contact, VoucharCode, EmailId, DelevaryType });

                dt.Rows.Add(totamt, chkBymail, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, tmp, ofremail, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                //cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                //cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                cmd.Parameters.AddWithValue("@OfferName", ofrname);
                cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "offer");
                cmd.Parameters.AddWithValue("@OfferCode", tmp);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "Offer";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {

                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                    //objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                    //objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult VoucherPageForMMB()
        {
            if (HttpContext.Session["UserID"] == null || HttpContext.Session.Count == 0)
            {
                // RouteValueDictionary rvd = new RouteValueDictionary();
                string strId = "0";
                if (Request.QueryString["id"] != null)
                    strId = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "0";


                // rvd.Add("id", strId );
                return RedirectToAction("LoginPage", new { id = strId });
                // return View("Login", new  {id=strId  });
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                return View(objCommic);
            }
        }
        [HttpPost]
        public ActionResult VoucherPageForMMB(string sb, string sp, string duree, string deltype, string delprice, string CatName, string kidCatName, string ComicName, string kidComicName, string gender, string ComicSelectionPrice, string totamt, string fn, string ln, string sec, string add, string comfac, string postcode, string Ville, string Telephone, string fn1, string ln1, string sec1, string add1, string comfac1, string postcode1, string Ville1, string Telephone1, string ofrFname, string ofrname, string ofremail, string ofrmsg, string ofrsurname, string chkBymail)
        {
            try
            {
                int i = 0;
                Random random = new Random();
                int num = random.Next(1000, 9999);
                string tmp = (DateTime.Now.ToString() + i.ToString("x") + num.ToString()).GetHashCode().ToString("x");

                chkDelevaryType = deltype;
                totamt = totamt.Replace(",", "");
                totamt = totamt.Replace("€", "");
                DataTable dt = new DataTable();

                //Adding columns to table
                DataColumn TotalAmt = new DataColumn("TotalAmt", typeof(System.String));
                DataColumn ChkByMail = new DataColumn("ChkByMail", typeof(System.String));
                DataColumn UserName = new DataColumn("UserName", typeof(System.String));
                DataColumn Address = new DataColumn("Address", typeof(System.String));
                DataColumn Contact = new DataColumn("Contact", typeof(System.String));
                DataColumn VoucharCode = new DataColumn("VoucharCode", typeof(System.String));
                DataColumn EmailId = new DataColumn("EmailId", typeof(System.String));
                DataColumn DelevaryType = new DataColumn("DelevaryType", typeof(System.String));

                //Adding columns to datatable
                dt.Columns.AddRange(new DataColumn[] { TotalAmt, ChkByMail, UserName, Address, Contact, VoucharCode, EmailId, DelevaryType });

                dt.Rows.Add(totamt, chkBymail, fn + " " + ln, add + " City " + Ville + " Postal code : " + postcode, Telephone, tmp, ofremail, deltype);
                HttpContext.Session["data"] = dt;
                cmd = new SqlCommand("spOrderManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", HttpContext.Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@SubscriptionName", sb);
                cmd.Parameters.AddWithValue("@SubscriptionPrice", sp);
                cmd.Parameters.AddWithValue("@DurationType", duree);
                cmd.Parameters.AddWithValue("@DelevaryType", deltype);
                cmd.Parameters.AddWithValue("@DelevaryPrice", delprice);
                cmd.Parameters.AddWithValue("@AdultComicCategory", CatName);
                cmd.Parameters.AddWithValue("@AdultComicNames", ComicName);
                cmd.Parameters.AddWithValue("@KidComicCategory", kidCatName);
                cmd.Parameters.AddWithValue("@KidComicNames", kidComicName);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Age", "5");
                cmd.Parameters.AddWithValue("@ComicSelectionPrice", ComicSelectionPrice);
                cmd.Parameters.AddWithValue("@TotalAmt", totamt);
                cmd.Parameters.AddWithValue("@FirstName", fn);
                cmd.Parameters.AddWithValue("@Name", ln);
                cmd.Parameters.AddWithValue("@Optional_Company", sec);
                cmd.Parameters.AddWithValue("@Address", add);
                cmd.Parameters.AddWithValue("@Optional_Supplement", comfac);
                cmd.Parameters.AddWithValue("@PostalCode", postcode);
                cmd.Parameters.AddWithValue("@City", Ville);
                cmd.Parameters.AddWithValue("@Telephone", Telephone);

                cmd.Parameters.AddWithValue("@BillingFirstName", fn1);
                cmd.Parameters.AddWithValue("@BillingName", ln1);
                cmd.Parameters.AddWithValue("@BillingOptional_Company", sec1);
                cmd.Parameters.AddWithValue("@BillingAddress", add1);
                cmd.Parameters.AddWithValue("@BillingOptional_Supplement", comfac1);
                cmd.Parameters.AddWithValue("@BillingPostalCode", postcode1);
                cmd.Parameters.AddWithValue("@BillingCity", Ville1);
                cmd.Parameters.AddWithValue("@BillingTelephone", Telephone1);

                cmd.Parameters.AddWithValue("@OfferFirstName", ofrFname);
                cmd.Parameters.AddWithValue("@OfferName", ofrname);
                cmd.Parameters.AddWithValue("@OfferEmail", ofremail);
                cmd.Parameters.AddWithValue("@OfferNameAndSername", ofrsurname);
                cmd.Parameters.AddWithValue("@OfferMsg", ofrmsg);
                //cmd.Parameters.AddWithValue("@OfferSandBy", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@SubscriptionType", "Subscription");
                cmd.Parameters.AddWithValue("@OfferCode", tmp);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                scopeid = Convert.ToInt32(cmd.ExecuteScalar());
                HttpContext.Session["currentid"] = scopeid;
                HttpContext.Session["SubsType"] = "Offer";
                con.Close();
                con.Dispose();

                if (scopeid > 0)
                {
                    TrasactionNumber = GetTrasactionID(scopeid);
                    HttpContext.Session["TrasactionNumber"] = TrasactionNumber;
                    return Json("True");

                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
                else
                {
                    return Json("False");
                    //ViewBag.Message = "Votre abonnement est complété avec succès ... Non!";
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    ////objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                    //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                    //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                    //return View(objCommic);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPost]

        #endregion

        [HttpGet]
        public ActionResult RagistrationPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Reg_Page()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reg_Page(Commic.Models.Commic obj, string btnsave)
        {
            try
            {
                string str = Request["id"] != null ? Request["id"].ToString() : "0";

                cmd = new SqlCommand("spRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", obj.objRegistrationMaster.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.objRegistrationMaster.LastName);
                cmd.Parameters.AddWithValue("@EmailID", obj.objRegistrationMaster.EmailID);
                cmd.Parameters.AddWithValue("@UserID", obj.objRegistrationMaster.EmailID);
                cmd.Parameters.AddWithValue("@Password", obj.objRegistrationMaster.Password);
                cmd.Parameters.AddWithValue("@CupanCode", obj.objRegistrationMaster.CupanCode);
                cmd.Parameters.AddWithValue("@CupanDate", obj.objRegistrationMaster.CupanDate);
                cmd.Parameters.AddWithValue("@CupanHolderID", obj.objRegistrationMaster.CupanHolderID);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();

                if (result > 0)
                {
                    if (Request.QueryString["isActive"] == "1")
                    {
                        return View("Vouchar_Activation");
                    }
                    ViewBag.Message = "Vous êtes succès inscrits ...!";
                }
                else
                {
                    ViewBag.Message = "Ces modalités d'inscription sont déjà inscrits ...! Essayez avec un autre de détails";
                }

                //HttpContext.Session["ID"] = "1"; //dt.Rows[0]["ID"].ToString();
                //HttpContext.Session["FirstName"] = obj.objRegistrationMaster.FirstName;
                //HttpContext.Session["LastName"] = obj.objRegistrationMaster.LastName;
                //HttpContext.Session["UserID"] = obj.objRegistrationMaster.EmailID;
                //HttpContext.Session["Password"] = "";//dt.Rows[0]["Password"].ToString();
                //HttpContext.Session["IsActive"] ="1"; // dt.Rows[0]["IsActive"].ToString();


                //Commic.Models.Commic objCommic = new Models.Commic();
                //objCommic.objpayment = new Models.Payment();
                //objCommic.objPaymentListM = new Models.Payment().GetPaymentData();
                //return RedirectToAction("FormuleBSMCommon", new { id = str });
                return View();
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        #region Login And Registration Page Controller

        [HttpPost]
        public ActionResult RagistrationPage(Commic.Models.Commic obj, string btnsave)
        {
            try
            {
                string str = Request["id"] != null ? Request["id"].ToString() : "0";

                cmd = new SqlCommand("spRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", obj.objRegistrationMaster.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.objRegistrationMaster.LastName);
                cmd.Parameters.AddWithValue("@EmailID", obj.objRegistrationMaster.EmailID);
                cmd.Parameters.AddWithValue("@UserID", obj.objRegistrationMaster.EmailID);
                cmd.Parameters.AddWithValue("@Password", obj.objRegistrationMaster.Password);
                cmd.Parameters.AddWithValue("@CupanCode", obj.objRegistrationMaster.CupanCode);
                cmd.Parameters.AddWithValue("@CupanDate", obj.objRegistrationMaster.CupanDate);
                cmd.Parameters.AddWithValue("@CupanHolderID", obj.objRegistrationMaster.CupanHolderID);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();

                if (result > 0)
                {
                    if (Request.QueryString["isActive"] == "1")
                    {
                        return View("Vouchar_Activation");
                    }
                    ViewBag.Message = "Vous êtes succès inscrits ...!";
                }
                else
                {
                    ViewBag.Message = "Ces modalités d'inscription sont déjà inscrits ...! Essayez avec un autre de détails";
                }

                //HttpContext.Session["ID"] = "1"; //dt.Rows[0]["ID"].ToString();
                //HttpContext.Session["FirstName"] = obj.objRegistrationMaster.FirstName;
                //HttpContext.Session["LastName"] = obj.objRegistrationMaster.LastName;
                //HttpContext.Session["UserID"] = obj.objRegistrationMaster.EmailID;
                //HttpContext.Session["Password"] = "";//dt.Rows[0]["Password"].ToString();
                //HttpContext.Session["IsActive"] ="1"; // dt.Rows[0]["IsActive"].ToString();


                //Commic.Models.Commic objCommic = new Models.Commic();
                //objCommic.objpayment = new Models.Payment();
                //objCommic.objPaymentListM = new Models.Payment().GetPaymentData();
                //return RedirectToAction("FormuleBSMCommon", new { id = str });
                return View();
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult LoginPage()
        {
            try
            {
                if (Request.QueryString["isActive"] == "1")
                {
                    return View("Vouchar_Activation");
                }
                if (Request.Cookies["UserID"].Value != "")
                {
                    HttpContext.Session["UserID"] = Request.Cookies["UserID"].Value;
                    HttpContext.Session["FirstName"] = Request.Cookies["FirstName"].Value;
                    string str = Request["id"] != null ? Request["id"].ToString() : "0";
                    //Response.Cookies["UserName"].Value = obj.objLoginMaster.UserID.Trim();
                    // Response.Cookies["Password"].Value = obj.objLoginMaster.Password.Trim();
                    //Commic.Models.Commic objCommic = new Models.Commic();
                    //objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
                    //objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
                    //objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                    //objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                    //objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                    //objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                    //objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                    //objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
                    //objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
                    //return RedirectToAction("/Index", "Index", objCommic);

                    if (str == "1")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
                        objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
                        objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
                        //Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();

                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("PertABuller", new { id = str });

                        //return RedirectToAction("PertABuller", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (str == "2")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();

                        return RedirectToAction("FormuleBSMCommon", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (str == "3")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();

                        return RedirectToAction("FormuleMBCommon", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (str == "4")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();

                        return RedirectToAction("FormuleMiniNMaxi", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (str == "11")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                        return RedirectToAction("VoucherPageForPertaBuller", new { id = str });
                    }
                    else if (str == "22")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        return RedirectToAction("VoucherPageForBSM", new { id = str });
                    }
                    else if (str == "33")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                        objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                        return RedirectToAction("VoucherPageForMB", new { id = str });
                    }
                    else if (str == "44")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                        return RedirectToAction("VoucherPageForMMB", new { id = str });
                    }
                    else
                    {
                        Commic.Models.Commic obj1 = new Models.Commic();
                        obj1.objLoginMaster = new Models.Login();
                        return RedirectToAction("/Index", "Index", obj1);
                    }

                }


                Commic.Models.Commic obj = new Models.Commic();
                obj.objLoginMaster = new Models.Login();
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != null)
                {
                    obj.objLoginMaster.querystring = Request.QueryString["id"];
                    return RedirectToAction("/Index", "Index", obj);
                }

                return View(obj);
            }
            catch
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult LoginPage(Commic.Models.Commic obj, string returnUrl, string hdd, string chkrem)
        {
            try
            {

                string str = Request["id"] != null ? Request["id"].ToString() : "0";
                cmd = new SqlCommand("spRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", obj.objLoginMaster.UserID);
                cmd.Parameters.AddWithValue("@Password", obj.objLoginMaster.Password);
                cmd.Parameters.AddWithValue("@Qtype", "Select");
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session["ID"] = dt.Rows[0]["ID"].ToString();
                    HttpContext.Session["FirstName"] = dt.Rows[0]["FirstName"].ToString();
                    HttpContext.Session["LastName"] = dt.Rows[0]["LastName"].ToString();
                    HttpContext.Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                    HttpContext.Session["Password"] = dt.Rows[0]["Password"].ToString();
                    HttpContext.Session["IsActive"] = dt.Rows[0]["IsActive"].ToString();
                    if (chkrem == "1")
                    {
                        Response.Cookies["UserID"].Value = dt.Rows[0]["UserID"].ToString();
                        Response.Cookies["FirstName"].Value = dt.Rows[0]["FirstName"].ToString();
                        // Response.Cookies["Password"].Value = obj.objLoginMaster.Password.Trim();
                    }
                    if (Request.QueryString["isActive"] == "1")
                    {
                        return View("Vouchar_Activation");
                    }
                    if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "1")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
                        objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
                        objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
                        //Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();

                        FormsAuthentication.SetAuthCookie(obj.objLoginMaster.UserID, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            //return RedirectToAction("Index", "Home");
                            return RedirectToAction("PertABuller", new { id = str });
                        }

                        //return RedirectToAction("PertABuller", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "2")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();

                        return RedirectToAction("FormuleBSMCommon", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "3")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();

                        return RedirectToAction("FormuleMBCommon", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "4")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();

                        return RedirectToAction("FormuleMiniNMaxi", new { id = str });
                        //return View("FormuleBSMCommon", objCommic); // Json(new { Url = "http://chacunsabulle.com/" });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "11")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        //objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        //objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                        return RedirectToAction("VoucherPageForPertaBuller", new { id = str });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "22")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        return RedirectToAction("VoucherPageForBSM", new { id = str });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "33")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.MiniPriceMasterobj = new Models.MiniPriceMaster();
                        objCommic.objMiniPriceMasterListM = new Models.MiniPriceMaster().GetMiniPriceData();
                        return RedirectToAction("VoucherPageForMB", new { id = str });
                    }
                    else if (dt.Rows[0]["IsActive"].ToString() == "True" && str == "44")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();
                        objCommic.objCatComboPriceMasterListM = new Models.CatComboPriceMaster().GetCatComboData();
                        objCommic.objMiniNMaxiPriceMasterListM = new Models.MiniNMaxiPrice().GetMiniNMaxiPriceData();
                        return RedirectToAction("VoucherPageForMMB", new { id = str });
                    }
                    else
                    {
                        Commic.Models.Commic obj1 = new Models.Commic();
                        obj1.objLoginMaster = new Models.Login();
                        return RedirectToAction("/Index", "Index", obj1);
                    }
                }
                else
                {
                    Commic.Models.Commic obj2 = new Models.Commic();
                    obj2.objLoginMaster = new Models.Login();
                    obj2.objRegistrationMaster = new Models.Registration();
                    ViewBag.Message = "Rogntudju!  numéro non valide.. Stp recommence";
                    return View(obj2);
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult signout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Index");
        }
        [HttpPost]
        public ActionResult signout(Commic.Models.Commic obj)
        {
            Session.Abandon();
            return View("Index");
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Commic.Models.Commic obj)
        {
            try
            {
                string str = Request["id"] != null ? Request["id"].ToString() : "0";
                cmd = new SqlCommand("spRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", obj.objforgotpwd.UserID);
                //cmd.Parameters.AddWithValue("@FirstName", obj.objforgotpwd.Name);
                cmd.Parameters.AddWithValue("@Qtype", "ForgotPwd");
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Timeout = 100000;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("Bhupeekr.77777@gmail.com", "07320734799");
                    MailMessage Loginfo = new MailMessage();
                    Loginfo.To.Add(obj.objforgotpwd.UserID);
                    Loginfo.From = new MailAddress("Bhupeekr.77777@gmail.com");
                    Loginfo.Subject = "Forgot Password...";
                    Loginfo.Body = "Dear " + dt.Rows[0]["FirstName"].ToString() + " Your password is:- " + dt.Rows[0]["Password"].ToString();
                    Loginfo.IsBodyHtml = true;
                    smtp.Send(Loginfo);
                    Commic.Models.Commic obj2 = new Models.Commic();
                    obj2.objLoginMaster = new Models.Login();
                    obj2.objRegistrationMaster = new Models.Registration();
                    ViewBag.Message = "votre mot de passe est envoyé avec succès à votre mail id";
                    return View("LoginPage");
                }
                else
                {
                    //Commic.Models.Commic obj2 = new Models.Commic();
                    //obj2.objLoginMaster = new Models.Login();
                    //obj2.objRegistrationMaster = new Models.Registration();
                    ViewBag.Message = "Invalid User_ID or Name...!";
                    return View();
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region Order Related Controllers for Get Status of Order

        [HttpGet]
        public ActionResult OrderStatus(string id)
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objOrderStatusListM = new Models.OrderStatus().GetOrderStatusDataByUserId();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult OrderStatus(Commic.Models.Commic obj, string hddid, string id)
        {
            //derStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(hddid));
            return View();
        }
        [HttpGet]
        public ActionResult OrderStatusByOrderNo(string id, string Length)
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objOrderStatusM = new Models.OrderStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(id));
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult OrderStatusByOrderNo(Commic.Models.Commic obj, string id)
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            new Models.OrderStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(id));
            return View(objCommic);
        }
        [HttpPost]
        public JsonResult OrderStatusByOrderNo(string id)
        {
            //Commic.Models.Commic objCommic = new Models.Commic();
            //new Models.OrderStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(id));
            //return Json(objCommic);
            return Json("hello");
        }

        #endregion

        #region Some Other Pages Controllers

        [HttpGet]
        public ActionResult COMMENTCAMARCHE()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Vouchar_Activation()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objActiveVouchar = new Models.ActiveVouchar();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult Vouchar_Activation(Commic.Models.Commic obj)
        {
            try
            {
                cmd = new SqlCommand("spActiveVouchar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", obj.objActiveVouchar.UserID);
                cmd.Parameters.AddWithValue("@VoucharCode", obj.objActiveVouchar.VoucharCode);
                cmd.Parameters.AddWithValue("@Qtype", "Select");
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ViewBag.Message = "Your Vouchar is Activated";
                }
                else
                {
                    ViewBag.Message = "AIE ON NE VOUS A PAS RECONNU";
                }
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objActiveVouchar = new Models.ActiveVouchar();
                return View(objCommic);
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult CallPaymentGatweyApi()
        {
            if (HttpContext.Session.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = HttpContext.Session["data"] as DataTable;

                //Execution des methodes d'appel de l'api en fonction
                //du paramètre passe dans l'url 
                if (dt.Rows.Count > 0)
                {
                    switch (Request.Params.Get("RequestType"))
                    {
                        case "Request": RequestMethod(HttpContext.Session["currentid"].ToString(), dt.Rows[0]["TotalAmt"].ToString());
                            break;
                        case "Response": ResponseMethod();
                            break;
                        case "AutoResponse": AutoResponseMethod();
                            break;

                        default:
                            ;
                            break;

                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult CallPaymentGatweyApi(Commic.Models.Commic obj)
        {
            try
            {
                switch (Request.Params.Get("RequestType"))
                {
                    case "Request": RequestMethod(scopeid.ToString(), TotalAmount);
                        break;
                    case "Response": ResponseMethod();
                        break;
                    case "AutoResponse": AutoResponseMethod();
                        break;

                    default:
                        ;
                        break;
                }
                if (HttpContext.Session.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = HttpContext.Session["data"] as DataTable;
                    if (dt.Rows.Count > 0)
                    {
                        if (HttpContext.Session["SubsType"].ToString() == "Offer")
                        {
                            if (res == "17" || res == "34")
                            {
                                return View("Error");
                            }
                            else if (res == "05" && dt.Rows[0]["DelevaryType"].ToString() == "Livraison suivie")
                            {
                                if (dt.Rows[0]["ChkByMail"].ToString() == "Courier")
                                {
                                    sandcodeToOwner("Bhupeekr.77777@gmail.com", dt.Rows[0]["VoucharCode"].ToString(), dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["Address"].ToString(), dt.Rows[0]["Contact"].ToString());
                                }
                                if (dt.Rows[0]["ChkByMail"].ToString() == "mail")
                                {
                                    sandcode(dt.Rows[0]["EmailId"].ToString(), dt.Rows[0]["VoucharCode"].ToString());
                                }
                                Commic.Models.Commic objcom = new Models.Commic();
                                return View("Confirmation_Point_relay_page_34");
                            }
                            else if (res == "05" && dt.Rows[0]["DelevaryType"].ToString() != "Livraison suivie")
                            {
                                if (dt.Rows[0]["ChkByMail"].ToString() == "Courier")
                                {
                                    sandcodeToOwner("Bhupeekr.77777@gmail.com", dt.Rows[0]["VoucharCode"].ToString(), dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["Address"].ToString(), dt.Rows[0]["Contact"].ToString());
                                }
                                if (dt.Rows[0]["ChkByMail"].ToString() == "mail")
                                {
                                    sandcode(dt.Rows[0]["EmailId"].ToString(), dt.Rows[0]["VoucharCode"].ToString());
                                }
                                Commic.Models.Commic objcom = new Models.Commic();
                                return View("Confirmation_domicile_page_33");
                            }
                            else
                            {
                                return View("Error");
                            }
                        }
                        else if (HttpContext.Session["SubsType"].ToString() == "sabnner")
                        {
                            if (res == "17" || res == "34")
                            {
                                return View("Error");
                            }
                            else if (res == "05")
                            {
                                Commic.Models.Commic objcom = new Models.Commic();
                                return View("Confirmation_Adresse_page_35");
                            }
                            else
                            {
                                return View("Error");
                            }
                            
                        }

                    }
                }
                else
                {
                    return View("Error");
                }
                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        #endregion

        #region paymentGateway Functions

        //public string CallPaymentGatwey(string requestType)
        //{
        //    //Execution des methodes d'appel de l'api en fonction
        //    //du paramètre passe dans l'url 
        //    switch (requestType)
        //    {
        //        case "Request": RequestMethod(ordernumber, TotalAmount);
        //            break;
        //        case "Response": ResponseMethod();
        //            break;
        //        case "AutoResponse": AutoResponseMethod();
        //            break;

        //        default:
        //            ;
        //            break;

        //    }

        //    return "";
        //}



        /// <summary>
        ///  traitement de la requete de paiement
        /// Dans cet exemple, on affiche un formulaire de
        /// connexion au serveur de paiement a l'internaute.
        /// </summary>
        public void RequestMethod(string orderno, string totamt)
        {
            try
            {

                Response.ContentType = "text/html";

                TextWriter responseWriter = Response.Output;

                //responseWriter.WriteLine("<html><head><title>SHERLOCKS - Paiement Securise sur Internet</title></head>");
                //responseWriter.WriteLine("<body bgcolor=#ffffff>");
                
                responseWriter.WriteLine("<html><head><title>CHACUN SA BULLE</title></head>");
                //pathToFiles = Server.MapPath("/param/pathfile");
                //responseWriter.WriteLine(pathToFiles);
                responseWriter.WriteLine("<link type='text/css' href='../css/styles_3.css' rel='stylesheet'>");
                responseWriter.WriteLine("<link href='http://fonts.googleapis.com/css?family=Bangers' rel='stylesheet' type='text/css'>");
                responseWriter.WriteLine("<link href='http://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css'>");
                responseWriter.WriteLine("<link rel='stylesheet' href='../vendor/bootstrap/css/bootstrap.css' />");
                responseWriter.WriteLine("<link rel='stylesheet' href='../dist/css/formValidation.css' />");
                responseWriter.WriteLine("<link rel='stylesheet' href='~/CSSfiles/StyleClass.css' />");
                responseWriter.WriteLine("<link rel='stylesheet' href='http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css' />");
                responseWriter.WriteLine("<script type='text/javascript' src='../vendor/jquery/jquery.min.js'></script>");
                responseWriter.WriteLine("<script type='text/javascript' src='../js/CustomValidation.js'></script>");
                responseWriter.WriteLine("<body style='color:white;'>");
                responseWriter.WriteLine("<header id='header'>");
                responseWriter.WriteLine("<div class='header-wrap' style='width:1180px;'>");
                responseWriter.WriteLine("<span class='fleft logo'><a href='#'><img src='../Images/logo.png' alt='CHACUN SA BULLE' /></a></span>");
                responseWriter.WriteLine("<div class='main-menu'>");
                responseWriter.WriteLine("<nav>");
                responseWriter.WriteLine("<ul>");
                responseWriter.WriteLine("<li><a class='home_icon' href='http://www.chacunsabulle.com/'></a></li>");
                responseWriter.WriteLine("<li><a href='Sabboner'>S’abonner</a></li>");
                responseWriter.WriteLine("<li><a href='LessOfferSabboner'>Offrir</a></li>");
                responseWriter.WriteLine("<li><a href='COMMENTCAMARCHE'>Comment ça marche ?</a></li>");
                responseWriter.WriteLine("<li><a href='#'>Blog</a></li>");
                responseWriter.WriteLine("<li><a class='user_icon' href='LoginPage' @*onclick='$('#SectionLogin').show(); return false;'*@>Connexion</a></li>");
                responseWriter.WriteLine(" </ul>");
                responseWriter.WriteLine("<nav>");
                responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("<Font color='wheat'>");
                //responseWriter.WriteLine("<section id='content'>");
                //responseWriter.WriteLine("<div class='content-wrap forume4' id='identification_detail_3'>");
                //responseWriter.WriteLine("<div class='banner_block' style='margin: 0px 0 8px;'>");
                //responseWriter.WriteLine("<div id='voir' class='banner2'>");
                //responseWriter.WriteLine("<h3 class='txt_center'>FÉLICITATIONS !</h3>");
                responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("<div id='slideshow1' class='felicitations1'>");
                //responseWriter.WriteLine("<Font color=wheat>");
                responseWriter.WriteLine("<center><h1 style='color:white;'>Test de l'API C# SHERLOCKS</h1></center><br /><br />");



                /* Initialisation du chemin du fichier pathfile (a modifier)
                   ex :      SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");
                */
                //string uriPath = "C:\\Users\\Maa\\Documents\\Visual Studio 2012\\Projects\\Commic\\Commic\\param\\pathfile";
                //string localPath = new Uri(uriPath).LocalPath;
                //SIPSApiWeb api = new SIPSApiWeb(localPath);


                pathToFiles = Server.MapPath("/param/pathfile");
                SIPSApiWeb api = new SIPSApiWeb(pathToFiles);

                /* Initialisation de l'objet d'appel */
                SIPSDataObject call = (SIPSDataObject)new SIPSCallParm();

                /**************************** Parametres obligatoires*************************/
                // ex : merchant_id = 014295303911111
                call.setValue("merchant_id", "081228158200013");

                // ex : merchant_country = fr
                call.setValue("merchant_country", "fr");

                // Affectation du montant de la transaction dans la plus petite unite
                // monetaire du pays
                // ex : 123,00 Euros ==> 12300 (currency_code = 978)
                call.setValue("amount", totamt);

                // Affectation du code monetaire ISO 4217 pour la transaction
                // ex : Euro ==> 978
                call.setValue("currency_code", "978");

                // Identifiant de transaction
                // ex : transaction_id = 123456
                call.setValue("transaction_id", HttpContext.Session["TrasactionNumber"].ToString() != null?HttpContext.Session["TrasactionNumber"].ToString():"0000");
                /******************************************************************************/

                // Affectation d'un numero identifiant pour la transaction
                // Attention aux reserves sur l'affectation automatique
                // cf Guide du developpeur

                // Valorisation des autres donnees de la transaction
                // facultatives, a completer au besoin
                // Les valeurs proposees ne sont que des exemples
                // Les champs et leur utilisation sont expliques dans le Dictionnaire des donnees
                //
                //call.setValue("normal_return_url", "http://localhost:33285/index/CallPaymentGatweyApi?RequestType=Response");
                //call.setValue("cancel_return_url", "http://localhost:33285/index/CallPaymentGatweyApi?RequestType=Response");
                //call.setValue("automatic_response_url", "http://localhost:33285/index/CallPaymentGatweyApi?RequestType=AutoResponse");

                call.setValue("normal_return_url", "http://www.chacunsabulle.com/index/CallPaymentGatweyApi?RequestType=Response");
                call.setValue("cancel_return_url", "http://www.chacunsabulle.com/index/CallPaymentGatweyApi?RequestType=Response");
                call.setValue("automatic_response_url", "http://www.chacunsabulle.com/index/CallPaymentGatweyApi?RequestType=AutoResponse");

                // call.setValue("language","fr");
                // call.setValue("payment_means","CB,2,VISA,2,MASTERCARD,2");
                // call.setValue("header_flag","no");
                // call.setValue("capture_day","");
                // call.setValue("capture_mode","");
                // call.setValue("bgcolor","");
                // call.setValue("block_align","");
                // call.setValue("block_order","");
                // call.setValue("textcolor","");
                // call.setValue("receipt_complement","");
                // call.setValue("caddie","mon caddie");
                // call.setValue("customer_id","");
                // call.setValue("customer_email","");
                // call.setValue("data","");
                // call.setValue("return_context","");
                // call.setValue("target","");
                call.setValue("order_id", orderno);
                // call.setValue("customer_title","");
                // call.setValue("customer_name","");
                // call.setValue("customer_firstname","");
                // call.setValue("customer_birthdate","");
                // call.setValue("customer_phone","");
                // call.setValue("customer_mobile_phone","");
                // call.setValue("customer_ip_address","");
                // call.setValue("customer_nationality_country","");
                // call.setValue("customer_birth_zipcode","");
                // call.setValue("customer_birth_city","");
                // call.setValue("home_city","");
                // call.setValue("home_streetnumber","");
                // call.setValue("home_street","");
                // call.setValue("home_zipcode","");

                // ELV ou Paypal delivery, billing address
                //
                // call.setValue("delivery_title","");
                // call.setValue("delivery_firstname","");
                // call.setValue("delivery_name","Smith");
                // call.setValue("delivery_company","");
                // call.setValue("delivery_streetnumber","");
                // call.setValue("delivery_street","");
                // call.setValue("delivery_zipCode","");
                // call.setValue("delivery_city","");
                // call.setValue("delivery_state","");
                // call.setValue("delivery_country","");
                // call.setValue("delivery_additional1","");
                // call.setValue("delivery_additional2","");
                // call.setValue("delivery_additional3","");
                // call.setValue("delivery_email","");
                // call.setValue("delivery_phone","");
                //
                // call.setValue("billing_title","");
                // call.setValue("billing_firstname","");
                // call.setValue("billing_name","");
                // call.setValue("billing_company","");
                // call.setValue("billing_streetnumber","");
                // call.setValue("billing_street","");
                // call.setValue("billing_zipCode","");
                // call.setValue("billing_city","");
                // call.setValue("billing_state","");
                // call.setValue("billing_country","");
                // call.setValue("billing_additional1","");
                // call.setValue("billing_additional2","");
                // call.setValue("billing_additional3","");
                // call.setValue("billing_email","");
                // call.setValue("billing_phone","");

                // Les valeurs suivantes ne sont utilisables qu'en pre-production
                // Elles necessitent l'installation de vos logos et templates sur
                // le serveur de paiement
                //
                // call.setValue("normal_return_logo","");
                // call.setValue("cancel_return_logo","");
                // call.setValue("submit_logo","");
                // call.setValue("logo_id","");
                // call.setValue("logo_id2","");
                // call.setValue("advert","");
                // call.setValue("background_id","");
                // call.setValue("templatefile","mon_template");


                // Insertion de la commande dans votre base de donnees
                // avec le status "en cours"
                // ...

                // Appel de l'api SIPS payment
                //responseWriter.WriteLine("</div>");

                //responseWriter.WriteLine("<div id='votre' class='bottom_block' style='position:absolute;top:318px;left:-10px;'>");
                //responseWriter.WriteLine("<div class='votre_wrap txt_center'>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</section>");
                responseWriter.WriteLine(api.sipsPaymentCallFunc(call));
                responseWriter.WriteLine("<br /><br />");
                responseWriter.WriteLine("</body>");
                responseWriter.WriteLine("</html>");

                responseWriter.Close();

            }

            catch (Exception e)
            {

                TextWriter responseWriter;
                Response.ContentType = "text/html";
                responseWriter = Response.Output;
                responseWriter.WriteLine("<center>");
                responseWriter.WriteLine("<br />");
                responseWriter.WriteLine("Error = " + e.GetType().FullName + e.Message);
                responseWriter.WriteLine("</center>");
                responseWriter.WriteLine("</body>");
                responseWriter.WriteLine("</html>");

                responseWriter.Close();

            }
        }

        /// <summary>
        /// traitement de la reponse manuelle
        /// Dans cet exemple, les donnees de la transaction sont decryptees et affichees sur le navigateur
        /// de l'internaute.
        /// </summary>
        public void ResponseMethod()
        {
            /* 	Redirection de la sortie DEBUG vers le navigateur. 
                       Par defaut le mode DEBUG est redirige vers la sortie standard du serveur d'application.
                   */
            //DebugOutputStream.setOut(Response.Output);

            try
            {
                Response.ContentType = "text/html";
                TextWriter responseWriter = Response.Output;

                responseWriter.WriteLine("<html><head><title>CHACUN SA BULLE</title></head>");
                //responseWriter.WriteLine("<link type='text/css' href='../css/styles_3.css' rel='stylesheet'>");
                //responseWriter.WriteLine("<link href='http://fonts.googleapis.com/css?family=Bangers' rel='stylesheet' type='text/css'>");
                //responseWriter.WriteLine("<link href='http://fonts.googleapis.com/css?family=Lato' rel='stylesheet' type='text/css'>");
                
                //responseWriter.WriteLine("<body>");
                ////responseWriter.WriteLine("<Font color='wheat'>");
                //responseWriter.WriteLine("<section id='content'>");
                //responseWriter.WriteLine("<div class='content-wrap forume4' id='identification_detail_3'>");
                //responseWriter.WriteLine("<div class='banner_block' style='margin: 0px 0 8px;'>");
                //responseWriter.WriteLine("<div id='voir' class='banner2'>");
                //responseWriter.WriteLine("<h3 class='txt_center'>FÉLICITATIONS !</h3>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("<div id='slideshow' class='felicitations'>");
                
                //responseWriter.WriteLine("<style color:white;>");
                //responseWriter.WriteLine("<center><h1>Test de l'API C# SHERLOCKS</h1></center><br /><br />");

                /* Initialisation du chemin du fichier pathfile (a modifier)
                    ex :    SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");
                 */
                //SIPSApiWeb api = new SIPSApiWeb("C:\\Users\\Maa\\Documents\\Visual Studio 2012\\Projects\\Commic\\Commic\\param\\pathfile");

                pathToFiles = Server.MapPath("/param/pathfile");
                ViewBag.Title = pathToFiles;
                SIPSApiWeb api = new SIPSApiWeb(pathToFiles);

                // Initialisation de l'objet reponse
                SIPSDataObject resp = (SIPSDataObject)new SIPSResponseParm();

                // Recuperation de la variable cryptee postee
                String cypheredtxt = Request.Params.Get("DATA");

                // Decryptage de la reponse
                resp = api.sipsPaymentResponseFunc(cypheredtxt);

                // Affichage des donnees de la reponse du serveur

                //System.Text.StringBuilder sbOutputTBL = new System.Text.StringBuilder();
                //sbOutputTBL.Append("<center>\n");
                //sbOutputTBL.Append("<table width='70%' id='tblData' border='1' style='color: Black;position: absolute;top: 100px;left: 110px;'> <tr><td colspan='5' style='color: Black;font-size: 33px;text-align:center;'>R&eacute;ponse manuelle du serveur SIPS</td></tr>\n");
                //sbOutputTBL.Append("<tr><th>Merchant_ID</th><th>Merchant_Country</th><th>Amount</th><th>Trasaction No.</th><th>Trasaction Date</th>");
                //sbOutputTBL.Append("<tr><td><span>" + resp.getValue("merchant_id") + "</span></td>");
                //sbOutputTBL.Append("<td><span>" + resp.getValue("merchant_country") + "</span></td>");
                //sbOutputTBL.Append("<td><span>" + resp.getValue("amount") + "</span></td>");
                //sbOutputTBL.Append("<td><span>" + resp.getValue("transaction_id") + "</span></td>");
                //sbOutputTBL.Append("<td><span>" + resp.getValue("transmission_date") + "</span></td></tr>");
                //sbOutputTBL.Append("<center>\n");
                //responseWriter.WriteLine(sbOutputTBL.ToString());

                //responseWriter.WriteLine("</div>");

                //responseWriter.WriteLine("<div id='votre' class='bottom_block' style='position:absolute;top:318px;left:-10px;'>");
                //responseWriter.WriteLine("<div class='votre_wrap txt_center'>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</div>");
                //responseWriter.WriteLine("</section>");
                //responseWriter.WriteLine("<center>\n");
                //responseWriter.WriteLine("<h3>R&eacute;ponse manuelle du serveur SIPS</h3>\n");
                //responseWriter.WriteLine("</center>\n");
                //responseWriter.WriteLine("<b><h4>\n");
                //responseWriter.WriteLine("<br /><hr>\n");
                //responseWriter.WriteLine("merchant_id = " + resp.getValue("merchant_id") + "<br />");
                //responseWriter.WriteLine("merchant_country = " + resp.getValue("merchant_country") + "<br />");
                //responseWriter.WriteLine("amount = " + resp.getValue("amount") + "<br />");
                //responseWriter.WriteLine("transaction_id = " + resp.getValue("transaction_id") + "<br />");
                //responseWriter.WriteLine("transmission_date = " + resp.getValue("transmission_date") + "<br />");
                //responseWriter.WriteLine("payment_means = " + resp.getValue("payment_means") + "<br />");
                //responseWriter.WriteLine("payment_time = " + resp.getValue("payment_time") + "<br />");
                //responseWriter.WriteLine("payment_date = " + resp.getValue("payment_date") + "<br />");
               // responseWriter.WriteLine("response_code = " + resp.getValue("response_code") + "<br />");
                res = resp.getValue("response_code");
                //responseWriter.WriteLine("payment_certificate = " + resp.getValue("payment_certificate") + "<br />");
                //responseWriter.WriteLine("authorisation_id = " + resp.getValue("authorisation_id") + "<br />");
                //responseWriter.WriteLine("currency_code = " + resp.getValue("currency_code") + "<br />");
                //responseWriter.WriteLine("card_number = " + resp.getValue("card_number") + "<br />");
                //responseWriter.WriteLine("cvv_flag = " + resp.getValue("cvv_flag") + "<br />");
                //responseWriter.WriteLine("cvv_response_code = " + resp.getValue("cvv_response_code") + "<br />");
                //responseWriter.WriteLine("bank_response_code = " + resp.getValue("bank_response_code") + "<br />");
                //responseWriter.WriteLine("complementary_code = " + resp.getValue("complementary_code") + "<br />");
                //responseWriter.WriteLine("complementary_info = " + resp.getValue("complementary_info") + "<br />");
                //responseWriter.WriteLine("return_context = " + resp.getValue("return_context") + "<br />");
                //responseWriter.WriteLine("caddie = " + resp.getValue("caddie") + "<br />");
                //responseWriter.WriteLine("receipt_complement = " + resp.getValue("receipt_complement") + "<br />");
                //responseWriter.WriteLine("merchant_language = " + resp.getValue("merchant_language") + "<br />");
                //responseWriter.WriteLine("language = " + resp.getValue("language") + "<br />");
                //responseWriter.WriteLine("customer_id = " + resp.getValue("customer_id") + "<br />");
                //responseWriter.WriteLine("order_id = " + resp.getValue("order_id") + "<br />");
                Commic.Models.orderno obj = new Models.orderno();
                obj.orderno1 = resp.getValue("order_id");
                ViewBag.Message = resp.getValue("order_id");
                //responseWriter.WriteLine("customer_email = " + resp.getValue("customer_email") + "<br />");
                //responseWriter.WriteLine("customer_ip_address = " + resp.getValue("customer_ip_address") + "<br />");
                //responseWriter.WriteLine("capture_day = " + resp.getValue("capture_day") + "<br />");
                //responseWriter.WriteLine("capture_mode = " + resp.getValue("capture_mode") + "<br />");
                //responseWriter.WriteLine("data = " + resp.getValue("data") + "<br />");
                //responseWriter.WriteLine("order_validity = " + resp.getValue("order_validity") + "<br />");
                //responseWriter.WriteLine("transaction_condition = " + resp.getValue("transaction_condition") + "<br />");
                //responseWriter.WriteLine("statement_reference = " + resp.getValue("statement_reference") + "<br />");
                //responseWriter.WriteLine("card_validity = " + resp.getValue("card_validity") + "<br />");
                //responseWriter.WriteLine("score_color = " + resp.getValue("score_color") + "<br />");
                //responseWriter.WriteLine("score_info = " + resp.getValue("score_info") + "<br />");
                //responseWriter.WriteLine("score_value = " + resp.getValue("score_value") + "<br />");
                //responseWriter.WriteLine("score_threshold = " + resp.getValue("score_threshold") + "<br />");
                //responseWriter.WriteLine("score_profile = " + resp.getValue("score_profile") + "<br />");
                //responseWriter.WriteLine("threed_ls_code = " + resp.getValue("threed_ls_code") + "<br />");
                //responseWriter.WriteLine("threed_relegation_code = " + resp.getValue("threed_relegation_code") + "<br />");
                //responseWriter.WriteLine("<br /><br /><hr></b></h4>");
                responseWriter.WriteLine("</body>");
                responseWriter.WriteLine("</html>");

                responseWriter.Close();
            }
            catch (Exception e)
            {

                TextWriter responseWriter;
                Response.ContentType = "text/html";
                responseWriter = Response.Output;

                responseWriter.WriteLine("<center>");
                responseWriter.WriteLine("<br />");
                responseWriter.WriteLine("Error = " + e.GetType().FullName + e.Message);
                responseWriter.WriteLine("</center>");
                responseWriter.WriteLine("</body>");
                responseWriter.WriteLine("</html>");

                responseWriter.Close();

            }
        }

        /// <summary>
        ///  traitement de la reponse automatique
        /// Dans cet exemple, les donnees de la transaction
        /// sont decryptees et sauvegardees dans un fichier log.
        /// </summary>
        public void AutoResponseMethod()
        {
            try
            {

                /* Initialisation du chemin du fichier pathfile (a modifier)
                    ex :     SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");
                 */
                SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");

                // Initialisation de l'objet reponse
                SIPSDataObject resp = (SIPSDataObject)new SIPSResponseParm();

                // Recuperation de la variable cryptee postee
                String cypheredtxt = Request.Params.Get("DATA");

                // Decryptage de la reponse
                resp = api.sipsPaymentResponseFunc(cypheredtxt);

                // Sauvegarde des donnees dans un fichier
                // a modifier pour mettre a jour une base de donnees, etc...
                // ...
                /* Initialisation du chemin du fichier de log (a modifier)
                     ex : String log_file = new String("c:\\repertoire\\logfile");
                 */

                String log_file = "c:\\repertoire\\logApi.txt";

                StreamWriter responseWriter = new StreamWriter(log_file);

                responseWriter.WriteLine("merchant_id = " + resp.getValue("merchant_id"));
                responseWriter.WriteLine("merchant_country = " + resp.getValue("merchant_country"));
                responseWriter.WriteLine("amount = " + resp.getValue("amount"));
                responseWriter.WriteLine("transaction_id = " + resp.getValue("transaction_id"));
                responseWriter.WriteLine("transmission_date = " + resp.getValue("transmission_date"));
                responseWriter.WriteLine("payment_means = " + resp.getValue("payment_means"));
                responseWriter.WriteLine("payment_time = " + resp.getValue("payment_time"));
                responseWriter.WriteLine("payment_date = " + resp.getValue("payment_date"));
                responseWriter.WriteLine("response_code = " + resp.getValue("response_code"));
                responseWriter.WriteLine("payment_certificate = " + resp.getValue("payment_certificate"));
                responseWriter.WriteLine("authorisation_id = " + resp.getValue("authorisation_id"));
                responseWriter.WriteLine("currency_code = " + resp.getValue("currency_code"));
                responseWriter.WriteLine("card_number = " + resp.getValue("card_number"));
                responseWriter.WriteLine("cvv_flag = " + resp.getValue("cvv_flag"));
                responseWriter.WriteLine("cvv_response_code = " + resp.getValue("cvv_response_code"));
                responseWriter.WriteLine("bank_response_code = " + resp.getValue("bank_response_code"));
                responseWriter.WriteLine("complementary_code = " + resp.getValue("complementary_code"));
                responseWriter.WriteLine("complementary_info = " + resp.getValue("complementary_info"));
                responseWriter.WriteLine("return_context = " + resp.getValue("return_context"));
                responseWriter.WriteLine("caddie = " + resp.getValue("caddie"));
                responseWriter.WriteLine("receipt_complement = " + resp.getValue("receipt_complement"));
                responseWriter.WriteLine("merchant_language = " + resp.getValue("merchant_language"));
                responseWriter.WriteLine("language = " + resp.getValue("language"));
                responseWriter.WriteLine("customer_id = " + resp.getValue("customer_id"));
                responseWriter.WriteLine("order_id = " + resp.getValue("order_id"));
                responseWriter.WriteLine("customer_email = " + resp.getValue("customer_email"));
                responseWriter.WriteLine("customer_ip_address = " + resp.getValue("customer_ip_address"));
                responseWriter.WriteLine("capture_day = " + resp.getValue("capture_day"));
                responseWriter.WriteLine("capture_mode = " + resp.getValue("capture_mode"));
                responseWriter.WriteLine("data = " + resp.getValue("data"));
                responseWriter.WriteLine("order_validity = " + resp.getValue("order_validity"));
                responseWriter.WriteLine("transaction_condition = " + resp.getValue("transaction_condition"));
                responseWriter.WriteLine("statement_reference = " + resp.getValue("statement_reference"));
                responseWriter.WriteLine("card_validity = " + resp.getValue("card_validity"));
                responseWriter.WriteLine("score_color = " + resp.getValue("score_color"));
                responseWriter.WriteLine("score_info = " + resp.getValue("score_info"));
                responseWriter.WriteLine("score_value = " + resp.getValue("score_value"));
                responseWriter.WriteLine("score_threshold = " + resp.getValue("score_threshold"));
                responseWriter.WriteLine("score_profile = " + resp.getValue("score_profile"));
                responseWriter.WriteLine("threed_ls_code = " + resp.getValue("threed_ls_code"));
                responseWriter.WriteLine("threed_relegation_code = " + resp.getValue("threed_relegation_code"));
                responseWriter.Flush();
                responseWriter.Close();


            }
            catch (Exception e)
            {
                /* Initialisation du chemin du fichier de log (a modifier)
                    ex :     "c:\\repertoire\\logfile"
                */

                String log_file = "c:\\repertoire\\logApi.txt";
                StreamWriter responseWriter = new StreamWriter(log_file);
                responseWriter.WriteLine("Error = " + e);
                responseWriter.Flush();
                responseWriter.Close();


            }
        }


        #endregion

        #region Functions To get OrderNumber And Transaction ID

        public string GetOrderNumber(string Vouchercode)
        {
            #region Variables Used

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
            SqlDataAdapter da;
            SqlCommand cmd;
            DataTable dt;

            #endregion

            cmd = new SqlCommand("spActiveVouchar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VoucharCode", Vouchercode);
            cmd.Parameters.AddWithValue("@Qtype", "GetOrderNo");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //if (dt.Rows.Count > 0)
            //{
                return dt.Rows[0]["id"].ToString();
            //}
            
        }

        public string GetTrasactionID(int orderno)
        {
            #region Variables Used

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
            SqlDataAdapter da;
            SqlCommand cmd;
            DataTable dt;

            #endregion

            cmd = new SqlCommand("spActiveVouchar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Oderid", orderno);
            cmd.Parameters.AddWithValue("@Qtype", "GetTrasactionID");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            return dt.Rows[0]["TransactionID"].ToString();
            //}
            con.Close();
        }

        #endregion

        #region Mailing Function to Vouchar Holder And Courior Department

        public void sandcode(string id, string Vouchercode)
        {

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                string line;
                string pathToFiles = Server.MapPath("/Html_Files/OfferEmailBody.html");
                using (StreamReader sr = new StreamReader(pathToFiles))
                {

                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    line = sr.ReadToEnd();
                }

                string strHyperlink = "<a href='http://localhost:33285/Index/LoginPage?IsActive=1'>Click here To Activate This Vouchar</>";
                //string[] chkP = new string[line.Length];

                line = line.Replace("_P_", strHyperlink);

                MailMessage message = new MailMessage();
                MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
                MailAddress receiver = new MailAddress(id);
                SmtpClient smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["smtpServer"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPass"])

                };
                message.From = Sender;
                message.To.Add(receiver);
                message.Subject = "Voucher Code";
                message.Body = line + "And Your Vouchar Code is :- '" + Vouchercode + "'";
                message.IsBodyHtml = true;
                smtp.Send(message);

                //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;
                //smtp.Timeout = 100000;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("Bhupeekr.77777@gmail.com", "07320734799");
                //MailMessage Loginfo = new MailMessage();
                //Loginfo.To.Add(id);
                //Loginfo.From = new MailAddress("Bhupeekr.77777@gmail.com");
                //Loginfo.Subject = "Voucher Code";
                //Loginfo.Body = line + "And Your Vouchar Code is :- '" + Vouchercode + "'";
                //Loginfo.IsBodyHtml = true;
                //smtp.Send(Loginfo);

            }

            catch (Exception e)
            {

            }
        }
        public void sandcodeToOwner(string id, string Vouchercode, string name, string add, string Telephone)
        {

            try
            {
                MailMessage message = new MailMessage();
                MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);
                MailAddress receiver = new MailAddress(id);
                SmtpClient smtp = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["smtpServer"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                    EnableSsl = true,
                    Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPass"])

                };
                message.From = Sender;
                message.To.Add(receiver);
                message.Subject = "Offer Vouchar";
                message.Body = "Hi<br>You Have new Package to Courier<br>Name :- '" + name + "'<br>Order Number :- '" + scopeid + "' <br> Vouchar Code is :- '" + Vouchercode + "'<br> Address :- '" + add + "' <br> Contact No :- '" + Telephone + "'";
                message.IsBodyHtml = true;
                smtp.Send(message);
            }

            catch (Exception e)
            {

            }
        }

        #endregion

        #region Final Confirmation Pages Controllers

        [HttpGet]
        public ActionResult Confirmation_domicile_page_33()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Confirmation_Point_relay_page_34()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Confirmation_Adresse_page_35()
        {
            return View();
        }

        #endregion

        #region Extra Useless Controller

        [HttpGet]
        public ActionResult FormuleMB()
        {

            return View();
        }
        [HttpGet]
        public ActionResult FormuleBSM()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Home_JE_Mabonne_formunate()
        {

            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
            objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
            objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
            objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult Home_JE_Mabonne_formunate(string hdd)
        {
            //string id = Request.QueryString["id"];
            if (hdd == "1")
            {
                return View("");
            }
            else if (hdd == "2")
            {
                return View("FormuleBSM");
            }
            else if (hdd == "3")
            {
                return View("FormuleMB");
            }
            else if (hdd == "4")
            {
                return View("FormuleMiniAndMaxi");
            }
            else
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                return View(objCommic);
            }
        }
        [HttpGet]
        public ActionResult MailingAndBillingAddress_1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MailingAndBillingAddress_1(string abonne)
        {
            return View();
        }
        [HttpGet]
        public ActionResult lessoffersabboner()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
            objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult lessoffersabboner(string abonne)
        {
            if (abonne == "prêt à buller")
            {
                return View("");
            }
            if (abonne == "bulle sur mesure")
            {
                return View("FormuleBSM");
            }
            if (abonne == "mini")
            {
                return View("FormuleMB");
            }
            if (abonne == "mini&maxibulle")
            {
                return View("FormuleMiniAndMaxi");
            }
            return View();
        }
        [HttpGet]
        public ActionResult sabboner()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
            objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
            return View(objCommic);
        }
        [HttpPost]
        public ActionResult sabboner(string abonne)
        {
            if (abonne == "prêt à buller")
            {
                return View("");
            }
            if (abonne == "bulle sur mesure")
            {
                return View("FormuleBSM");
            }
            if (abonne == "mini")
            {
                return View("FormuleMB");
            }
            if (abonne == "mini&maxibulle")
            {
                return View("FormuleMiniAndMaxi");
            }
            return View();
        }
        [HttpGet]
        public ActionResult FormuleBM()
        {
            return View();
        }
        [HttpGet]
        public ActionResult indexValidation()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FormuleMiniAndMaxi()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(Commic.Models.Commic obj)
        {
            try
            {
                //cmd = new SqlCommand("spPayment", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CardNumber", obj.objpayment.CardNumber);
                //cmd.Parameters.AddWithValue("@CardHolder_Name", obj.objpayment.CardHolder);
                //cmd.Parameters.AddWithValue("@Exp_Date", obj.objpayment.ExpDate);
                //cmd.Parameters.AddWithValue("@Security_Code", obj.objpayment.SecurityCode);
                //cmd.Parameters.AddWithValue("@Total_Subscriber", obj.objpayment.TotalSubscriber);
                //cmd.Parameters.AddWithValue("@Qtype", "Payment");

                ApiPayment.SIPSCallParm.CUSTOMER_FIRST_NAME = obj.objpayment.CardHolder;
                ApiPayment.SIPSCallParm.TRANSMISSION_DATE = obj.objpayment.ExpDate;
                ApiPayment.SIPSCallParm.AMOUNT = obj.objpayment.TotalSubscriber;

                //con.Open();
                //result = cmd.ExecuteNonQuery();
                //con.Close();
                //con.Dispose();
                return View();
                //Commic.Models.Commic objCommic = new Models.Commic();
                //objCommic.objpayment = new Models.Payment();
                //objCommic.objPaymentListM = new Models.Payment ().GetPaymentData();

                //if (result > 0)
                //{
                //    ViewBag.Message = "Command Is Successfully Executed...!";
                //    return View(objCommic);
                //}
                //else
                //{
                //    return View(objCommic);
                //}
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public ActionResult MailingAndBillingAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MailingAndBillingAddress(Commic.Models.Commic obj)
        {
            try
            {
                cmd = new SqlCommand("spPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", obj.objIdenDetail.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.objIdenDetail.LastName);
                cmd.Parameters.AddWithValue("@OptionalSociety", obj.objIdenDetail.OptionalSociety);
                cmd.Parameters.AddWithValue("@Address", obj.objIdenDetail.Address);
                cmd.Parameters.AddWithValue("@OptionalSupplement", obj.objIdenDetail.OptionalSupplement);
                cmd.Parameters.AddWithValue("@PostalCode", obj.objIdenDetail.PostalCode);
                cmd.Parameters.AddWithValue("@Village", obj.objIdenDetail.Village);
                cmd.Parameters.AddWithValue("@Country", obj.objIdenDetail.Country);
                cmd.Parameters.AddWithValue("@TelephoneNumber", obj.objIdenDetail.Telephone);
                cmd.Parameters.AddWithValue("@Qtype", "Insert");

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();

                if (result > 0)
                {
                    ViewBag.Message = "Command Is Successfully Executed...!";
                    //return View(objCommic);
                    return View();
                }
                else
                {
                    //return View(objCommic);
                    return View();
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPost]
        public ActionResult Login(string uid, string pwd)
        {
            try
            {
                string str = Request["id"] != null ? Request["id"].ToString() : "0";
                cmd = new SqlCommand("spRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", uid);
                cmd.Parameters.AddWithValue("@Password", pwd);
                cmd.Parameters.AddWithValue("@Qtype", "Select");
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                con.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session["ID"] = dt.Rows[0]["ID"].ToString();
                    HttpContext.Session["FirstName"] = dt.Rows[0]["FirstName"].ToString();
                    HttpContext.Session["LastName"] = dt.Rows[0]["LastName"].ToString();
                    HttpContext.Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                    HttpContext.Session["Password"] = dt.Rows[0]["Password"].ToString();
                    HttpContext.Session["IsActive"] = dt.Rows[0]["IsActive"].ToString();
                    if (dt.Rows[0]["IsActive"].ToString() == "True")
                    {
                        Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
                        objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
                        objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
                        //Commic.Models.Commic objCommic = new Models.Commic();
                        objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                        objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                        objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                        objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                        objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
                        //return View(objCommic);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Votre compte est désactivé";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {

                    ViewBag.Message = "Rogntudju!  numéro non valide.. Stp recommence";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        public ActionResult PaymentGateway(Commic.Models.Commic obj)
        {
            try
            {
                Commic.Models.Commic objCommic = new Models.Commic();
                //objCommic.objChkPayGate objpay=new Models.CheckPaymentGateway().UserId="jkl";


                //return (objCommic);
                return View();
            }
            catch
            {
                return View();
            }
        }


        #endregion
    }
}
