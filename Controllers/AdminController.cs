using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Commic.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Configuration;
using System.IO;
using System.Web.Security;


namespace Commic.Controllers
{
    public class AdminController : Controller
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        int result;
        string imgUrl;
        string SubsBTNImage;
        string SubsBTNicon;
        string BTNImage;
        string SubsBTNImageURL;
        string SubsBTNiconURL;
        string BTNImageURL;
        string NewImgName;
        string NewVideoName;

        #endregion

        public ActionResult Login()
        {
            return View();
        }
     
        [HttpPost]
        public ActionResult Login(string userId, string password)
        {

            string userIdConfig = ConfigurationManager.AppSettings["UserId"];
            string passwordConfig = ConfigurationManager.AppSettings["pwd"];

            if (userId == userIdConfig && password == passwordConfig)
            {
                FormsAuthentication.SetAuthCookie(userId, false);
                return RedirectToAction("OrderStatusManage");
            }
            else
                return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MasterPage2Section()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MasterPage2Section(Commic.Models.Commic obj)
        {
            try
            {
                if (Request.Files.Count > 0)
                {

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var upload1 = Request.Files[i];
                        if (i == 0)
                        {
                            if (upload1 != null && upload1.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(upload1.FileName);
                                NewImgName = Guid.NewGuid().ToString() + fileName;
                                imgUrl = Path.Combine(Server.MapPath("~/Uploads/"), NewImgName);
                                upload1.SaveAs(imgUrl);
                            }
                        }
                    }
                }
                if (NewImgName != null)
                {
                    cmd = new SqlCommand("spMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@logoURL", NewImgName);
                    cmd.Parameters.AddWithValue("@Sec2Heading1", obj.objMasterpage.Sec2Heading1);
                    cmd.Parameters.AddWithValue("@Sec2Def", obj.objMasterpage.Sec2Def);
                    cmd.Parameters.AddWithValue("@Sec2Discription", obj.objMasterpage.Sec2Discription);
                    cmd.Parameters.AddWithValue("@Sec2BtnCaption", obj.objMasterpage.Sec2BtnCaption);
                    cmd.Parameters.AddWithValue("@Qtype", "Update");
                    cmd.Parameters.AddWithValue("@ID", "1");
                }
                else
                {
                    cmd = new SqlCommand("spMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Sec2Heading1", obj.objMasterpage.Sec2Heading1);
                    cmd.Parameters.AddWithValue("@Sec2Def", obj.objMasterpage.Sec2Def);
                    cmd.Parameters.AddWithValue("@Sec2Discription", obj.objMasterpage.Sec2Discription);
                    cmd.Parameters.AddWithValue("@Sec2BtnCaption", obj.objMasterpage.Sec2BtnCaption);
                    cmd.Parameters.AddWithValue("@Qtype", "Update2");
                    cmd.Parameters.AddWithValue("@ID", "1");
                }
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                if (result > 0)
                {
                    ViewBag.Message = "Command Is Successfully Updated...!";
                }
                else
                {
                    ViewBag.Message = "Command Is Not Successfully Updated...!";
                }
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
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

        [Authorize]
        [HttpGet]
        public ActionResult MasterPage3Section()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MasterPage3Section(Commic.Models.Commic obj)
        {
            try
            {
                cmd = new SqlCommand("spMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sec3Head1", obj.objMasterpage.Sec3Head1);
                cmd.Parameters.AddWithValue("@Sec3Desc1", obj.objMasterpage.Sec3Desc1);
                cmd.Parameters.AddWithValue("@Sec3Head2", obj.objMasterpage.Sec3Head2);
                cmd.Parameters.AddWithValue("@Sec3Desc2", obj.objMasterpage.Sec3Desc2);
                cmd.Parameters.AddWithValue("@Sec3Head3", obj.objMasterpage.Sec3Head3);
                cmd.Parameters.AddWithValue("@Sec3Desc3", obj.objMasterpage.Sec3Desc3);
                cmd.Parameters.AddWithValue("@Sec4Head", obj.objMasterpage.Sec4Head);
                cmd.Parameters.AddWithValue("@Sec4Desc", obj.objMasterpage.Sec4Desc);
                cmd.Parameters.AddWithValue("@Sec4Para", obj.objMasterpage.Sec4Para);
                cmd.Parameters.AddWithValue("@Qtype", "Update3Sec");
                cmd.Parameters.AddWithValue("@ID", "1");
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                if (result > 0)
                {
                    ViewBag.Message = "Command Is Successfully Updated...!";
                }
                else
                {
                    ViewBag.Message = "Command Is Not Successfully Updated...!";

                }
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objMasterpage = new Models.MasterPage().GetMasterData();
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

        [Authorize]
        [HttpGet]
        public ActionResult SlideShowSectionn()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            //objCommic.objSlide = new Models.SlideShowSection();
            objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SlideShowSectionn(Commic.Models.Commic obj, string hdd)
        {
            try
            {
                if (hdd == "0")
                {
                    if (Request.Files.Count > 0)
                    {

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var upload1 = Request.Files[i];
                            if (i == 0)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    NewImgName = Guid.NewGuid().ToString() + fileName;
                                    imgUrl = Path.Combine(Server.MapPath("~/Uploads/"), NewImgName);
                                    upload1.SaveAs(imgUrl);
                                }
                            }
                        }
                    }
                    if (NewImgName != null)
                    {
                        cmd = new SqlCommand("spSlideShowSection", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MainImageUrl", NewImgName);
                        cmd.Parameters.AddWithValue("@Headline1", obj.objSlide.Heading1);
                        cmd.Parameters.AddWithValue("@DescLine1", obj.objSlide.DescLine1);
                        cmd.Parameters.AddWithValue("@DescLine2", obj.objSlide.DescLine2);
                        cmd.Parameters.AddWithValue("@DescLine3", obj.objSlide.DescLine3);
                        cmd.Parameters.AddWithValue("@DescLine4", obj.objSlide.DescLine4);
                        cmd.Parameters.AddWithValue("@btn1Caption", obj.objSlide.Btn1Caption);
                        cmd.Parameters.AddWithValue("@btn2Caption", obj.objSlide.BTn2Caption);
                        cmd.Parameters.AddWithValue("@Qtype", "insert");

                    }
                    else
                    {
                        cmd = new SqlCommand("spSlideShowSection", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Headline1", obj.objMasterpage.Sec2Heading1);
                        cmd.Parameters.AddWithValue("@DescLine1", obj.objMasterpage.Sec2Def);
                        cmd.Parameters.AddWithValue("@DescLine2", obj.objMasterpage.Sec2Discription);
                        cmd.Parameters.AddWithValue("@DescLine3", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@DescLine4", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@btn1Caption", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@btn2Caption", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@Qtype", "insert");
                    }
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var upload1 = Request.Files[i];
                            if (i == 0)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    NewImgName = Guid.NewGuid().ToString() + fileName;
                                    imgUrl = Path.Combine(Server.MapPath("~/Uploads/"), NewImgName);
                                    upload1.SaveAs(imgUrl);
                                }
                            }
                        }
                    }
                    if (NewImgName != null)
                    {
                        cmd = new SqlCommand("spSlideShowSection", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MainImageUrl", NewImgName);
                        cmd.Parameters.AddWithValue("@Headline1", obj.objSlide.Heading1);
                        cmd.Parameters.AddWithValue("@DescLine1", obj.objSlide.DescLine1);
                        cmd.Parameters.AddWithValue("@DescLine2", obj.objSlide.DescLine2);
                        cmd.Parameters.AddWithValue("@DescLine3", obj.objSlide.DescLine3);
                        cmd.Parameters.AddWithValue("@DescLine4", obj.objSlide.DescLine4);
                        cmd.Parameters.AddWithValue("@btn1Caption", obj.objSlide.Btn1Caption);
                        cmd.Parameters.AddWithValue("@btn2Caption", obj.objSlide.BTn2Caption);
                        cmd.Parameters.AddWithValue("@Id", hdd);
                        cmd.Parameters.AddWithValue("@Qtype", "Update");

                    }
                    else
                    {
                        cmd = new SqlCommand("spSlideShowSection", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Headline1", obj.objMasterpage.Sec2Heading1);
                        cmd.Parameters.AddWithValue("@DescLine1", obj.objMasterpage.Sec2Def);
                        cmd.Parameters.AddWithValue("@DescLine2", obj.objMasterpage.Sec2Discription);
                        cmd.Parameters.AddWithValue("@DescLine3", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@DescLine4", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@btn1Caption", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@btn2Caption", obj.objMasterpage.Sec2BtnCaption);
                        cmd.Parameters.AddWithValue("@Id", hdd);
                        cmd.Parameters.AddWithValue("@Qtype", "Update2");
                    }
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                }

                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSlide = new Models.SlideShowSection();
                objCommic.objSlideListM = new Models.SlideShowSection().GetSlideSectionData();

                if (result > 0)
                {
                    ViewBag.Message = "Command Is Successfully Executed...!";
                    return View(objCommic);
                }
                else
                {
                    return View(objCommic);
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

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DeleterSlideShow(string id)
        {

            new Models.SlideShowSection().DeleteSlideShow(Convert.ToInt32(id));

            System.Text.StringBuilder sbSlidShow = new System.Text.StringBuilder();
            List<Models.SlideShowSection> objSlidShowList = new List<Models.SlideShowSection>();
            objSlidShowList = new Models.SlideShowSection().GetSlideSectionData();
            sbSlidShow.Append("<table width='100%' id='tblData' border='1'><th>S.No.</th><th>Image</th><th>Heading</th><th>Description Line1</th><th>Description Line2</th><th>Description Line3</th><th>Description Line4</th><th>Caption of 1 Botton </th><th>Caption of 2 Botton</th><th>Action</th>");
            foreach (Models.SlideShowSection obj in objSlidShowList)
            {
                sbSlidShow.Append("<tr> <td> <input type='hidden' value='" + obj.id + "' data-heading='" + obj.Heading1 + "' data-descline1='" + obj.DescLine1 + "' data-descline2='" + obj.DescLine2 + "' data-descline3='" + obj.DescLine3 + "' data-descline4='" + obj.DescLine4 + "' data-btncap1='" + obj.Btn1Caption + "' data-btncap1='" + obj.BTn2Caption + "'/> <a href='#' onclick='FillEdit(" + obj.id + "); return false;'>" + obj.id + "</a> </td>");
                sbSlidShow.Append("<td><span><img src=" + Url.Content("~/Uploads/" + obj.MainImageUrl) + " width='" + 100 + "' heigth='" + 100 + "'/></span></td>");
                sbSlidShow.Append("<td><span>" + obj.Heading1 + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.DescLine1 + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.DescLine2 + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.DescLine3 + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.DescLine4 + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.Btn1Caption + "</span></td>");
                sbSlidShow.Append("<td><span>" + obj.BTn2Caption + "</span></td>");
                sbSlidShow.Append("<td><span><a href='#' onclick='Deleterecord(" + obj.id + " ); return false;'>Delete</a></span></td></tr>");

            }

            return Json(sbSlidShow.ToString());
        }

        [Authorize]
        [HttpGet]
        public ActionResult SubcriptionManagment()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            //objCommic.objSubsMasterListM = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubcriptionManagment(Commic.Models.Commic obj, string hdd)
        {
            try
            {
                cmd = new SqlCommand("spSubscription", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FormuleHeading", obj.objSubMasterclass.FormuleHeading);
                cmd.Parameters.AddWithValue("@FormuleDesc", obj.objSubMasterclass.FormuleDesc);
                cmd.Parameters.AddWithValue("@DureeHeading", obj.objSubMasterclass.DureeHeading);
                cmd.Parameters.AddWithValue("@DureeDesc", obj.objSubMasterclass.DureeDesc);
                cmd.Parameters.AddWithValue("@DeliveryHeading", obj.objSubMasterclass.DeliveryHeading);
                cmd.Parameters.AddWithValue("@DeliveryDesc", obj.objSubMasterclass.DeliveryDesc);
                cmd.Parameters.AddWithValue("@FirstSubsName", obj.objSubMasterclass.FirstSubsName);
                cmd.Parameters.AddWithValue("@FirstSubsPrice", obj.objSubMasterclass.FirstSubsPrice);
                cmd.Parameters.AddWithValue("@FirstSubsUnit", obj.objSubMasterclass.FirstSubsUnit);
                cmd.Parameters.AddWithValue("@SecondSubsName", obj.objSubMasterclass.SecondSubsName);
                cmd.Parameters.AddWithValue("@SecondSubsPrice", obj.objSubMasterclass.SecondSubsPrice);
                cmd.Parameters.AddWithValue("@SecondSubsUnit", obj.objSubMasterclass.SecondSubsUnit);
                cmd.Parameters.AddWithValue("@ThirdSubsName", obj.objSubMasterclass.ThirdSubsName);
                cmd.Parameters.AddWithValue("@ThirdSubsPrice", obj.objSubMasterclass.ThirdSubsPrice);
                cmd.Parameters.AddWithValue("@ThirdSubsUnit", obj.objSubMasterclass.ThirdSubsUnit);
                cmd.Parameters.AddWithValue("@FourthSubsName", obj.objSubMasterclass.FourthSubsName);
                cmd.Parameters.AddWithValue("@FourthSubsPrice", obj.objSubMasterclass.FourthSubsPrice);
                cmd.Parameters.AddWithValue("@FourthSubsUnit", obj.objSubMasterclass.FourthSubsUnit);
                cmd.Parameters.AddWithValue("@FooterDesc", obj.objSubMasterclass.FooterDescription);
                cmd.Parameters.AddWithValue("@Qtype", "UpdateSubsMaster");
                cmd.Parameters.AddWithValue("@ID", "1");
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                if (result > 0)
                {
                    ViewBag.Message = "Data Is Successfully Updated...!";

                }
                else
                {
                    ViewBag.Message = "Data Is Not Successfully Updated...!";

                }
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

        [Authorize]
        [HttpGet]
        public ActionResult DureeAndDeliveryManagement()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
            objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
            objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
            objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
            objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DureeAndDeliveryManagement(Commic.Models.Commic obj, string hdd)
        {
            try
            {
                if ((obj.objSubTimeclass.SubsTimeDuration == null || obj.objSubTimeclass.SubsTimeDuration == "" || obj.objSubTimeclass.SubsTimePrice == null || obj.objSubTimeclass.SubsTimePrice == "") && (obj.objSubDeliveryclass.DeliveryType != null && obj.objSubDeliveryclass.DeliveryPrice != null))
                {
                    cmd = new SqlCommand("spSubscription", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DeliveryType", obj.objSubDeliveryclass.DeliveryType);
                    cmd.Parameters.AddWithValue("@DeliveryPrice", obj.objSubDeliveryclass.DeliveryPrice);
                    cmd.Parameters.AddWithValue("@Qtype", "SaveinDureeAndDelivery1");
                }
                else if ((obj.objSubDeliveryclass.DeliveryType == null || obj.objSubDeliveryclass.DeliveryType == "" || obj.objSubDeliveryclass.DeliveryPrice == null || obj.objSubDeliveryclass.DeliveryPrice == "") && (obj.objSubTimeclass.SubsTimeDuration != null && obj.objSubTimeclass.SubsTimePrice != null))
                {
                    cmd = new SqlCommand("spSubscription", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubscriptionTime", obj.objSubTimeclass.SubsTimeDuration);
                    cmd.Parameters.AddWithValue("@SubsTimePrice", obj.objSubTimeclass.SubsTimePrice);
                    cmd.Parameters.AddWithValue("@Qtype", "SaveinDureeAndDelivery2");
                }
                else if (obj.objSubTimeclass.SubsTimeDuration != null && obj.objSubTimeclass.SubsTimePrice != null && obj.objSubDeliveryclass.DeliveryType != null && obj.objSubDeliveryclass.DeliveryPrice != null)
                {
                    cmd = new SqlCommand("spSubscription", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubscriptionTime", obj.objSubTimeclass.SubsTimeDuration);
                    cmd.Parameters.AddWithValue("@SubsTimePrice", obj.objSubTimeclass.SubsTimePrice);
                    cmd.Parameters.AddWithValue("@DeliveryType", obj.objSubDeliveryclass.DeliveryType);
                    cmd.Parameters.AddWithValue("@DeliveryPrice", obj.objSubDeliveryclass.DeliveryPrice);
                    cmd.Parameters.AddWithValue("@Qtype", "SaveinDureeAndDelivery3");
                }
                else
                {

                }
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objSubMasterclass = new Models.SubscriptionMaster().GetSubscriptionMasterData();
                objCommic.objSubTimeclass = new Models.SubscriptionTimeDuration();
                objCommic.objSubsTimeListM = new Models.SubscriptionTimeDuration().GetSubscriptionDurationData();
                objCommic.objSubDeliveryclass = new Models.SubscriptionDeliveryTypes();
                objCommic.objSubsDeliveryListM = new Models.SubscriptionDeliveryTypes().GetSubscriptionDeliveryData();

                if (result > 0)
                {
                    ViewBag.Message = "Data Is Successfully Saved...!";

                }
                else
                {
                    ViewBag.Message = "Data Is Not Successfully Saved...!";

                }
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

        [Authorize]
        [HttpGet]
        public ActionResult MaterPage4Section()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
            objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DeletersSec4Subcription(string id)
        {
            new Models.MasterPageSec4BTN().DeleteSec4Subscription(Convert.ToInt32(id));
            ViewBag.Message = "Data is Successfully Deleted...!";
            System.Text.StringBuilder sbDeleteSec4BTN = new System.Text.StringBuilder();
            List<Models.MasterPageSec4BTN> objSec4SubsBTNList = new List<Models.MasterPageSec4BTN>();
            objSec4SubsBTNList = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
            sbDeleteSec4BTN.Append("<table width='100%' id='tblData' border='1'><tr><th>S.No.</th><th>Heading</th><th>Sub Heading</th><th>Description</th><th>Price/Unit</th><th>Button Caption</th><th>Footer</th><th colspan=" + 2 + ">Action</th></tr>");
            foreach (Models.MasterPageSec4BTN obj in objSec4SubsBTNList)
            {
                sbDeleteSec4BTN.Append("<tr><td><input type='hidden' value='" + @obj.id + "' data-Heading='" + @obj.SubsHeading + "' data-SubHead='" + @obj.SubssubHeading + "' data-desc='" + @obj.SubsDescription + "' data-Footer='" + @obj.SubsFooter + "' data-Price='" + @obj.SubsPrice + "' data-unit='" + @obj.SubsUnit + "' data-btncaption='" + @obj.SubsbtnCaption + "' />" + obj.id + "</td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubsHeading + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubssubHeading + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubsDescription + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubsPrice + "" + obj.SubsUnit + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubsbtnCaption + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span>" + obj.SubsFooter + "</span></td>");
                sbDeleteSec4BTN.Append("<td><span><a href='#' onclick='Deleterecord(" + obj.id + " ); return false;'>Edit</a></span></td>");
                sbDeleteSec4BTN.Append("<td><span><a href='#' onclick='Deleterecord(" + obj.id + " ); return false;'>Delete</a></span></td></tr>");

            }

            return Json(sbDeleteSec4BTN.ToString());
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MaterPage4Section(Commic.Models.Commic obj, string hdd)
        {
            try
            {
                if (hdd == "0" || hdd == null)
                {
                    if (Request.Files.Count > 0)
                    {

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var upload1 = Request.Files[i];
                            if (i == 0)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    SubsBTNImageURL = Guid.NewGuid().ToString() + fileName;
                                    SubsBTNImage = Path.Combine(Server.MapPath("~/Uploads/"), SubsBTNImageURL);
                                    upload1.SaveAs(SubsBTNImage);
                                }
                            }
                            else if (i == 1)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    SubsBTNiconURL = Guid.NewGuid().ToString() + fileName;
                                    SubsBTNicon = Path.Combine(Server.MapPath("~/Uploads/"), SubsBTNiconURL);
                                    upload1.SaveAs(SubsBTNicon);
                                }
                            }
                            else if (i == 2)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    BTNImageURL = Guid.NewGuid().ToString() + fileName;
                                    BTNImage = Path.Combine(Server.MapPath("~/Uploads/"), BTNImageURL);
                                    upload1.SaveAs(BTNImage);
                                }
                            }
                        }
                    }
                    cmd = new SqlCommand("spMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubsImageURL", SubsBTNImageURL);
                    cmd.Parameters.AddWithValue("@SubsIconURL", SubsBTNiconURL);
                    cmd.Parameters.AddWithValue("@SubsHeading", obj.objMasterPageSec4BTN.SubsHeading);
                    cmd.Parameters.AddWithValue("@SubsPrice", obj.objMasterPageSec4BTN.SubsPrice);
                    cmd.Parameters.AddWithValue("@SubsUnit", obj.objMasterPageSec4BTN.SubsUnit);
                    cmd.Parameters.AddWithValue("@SubssubHeading", obj.objMasterPageSec4BTN.SubssubHeading);
                    cmd.Parameters.AddWithValue("@SubsDescription", obj.objMasterPageSec4BTN.SubsDescription);
                    cmd.Parameters.AddWithValue("@SubsBtnImageURL", BTNImageURL);
                    cmd.Parameters.AddWithValue("@SubsbtnCaption", obj.objMasterPageSec4BTN.SubsbtnCaption);
                    cmd.Parameters.AddWithValue("@SubsFooter", obj.objMasterPageSec4BTN.SubsFooter);
                    cmd.Parameters.AddWithValue("@Qtype", "InsertSec4BTN");
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "Data Is Successfully Saved...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Saved...!";
                    }
                }
                else
                {
                    if (Request.Files.Count > 0)
                    {

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var upload1 = Request.Files[i];
                            if (i == 0)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    SubsBTNImageURL = Guid.NewGuid().ToString() + fileName;
                                    SubsBTNImage = Path.Combine(Server.MapPath("~/Uploads/"), SubsBTNImageURL);
                                    upload1.SaveAs(SubsBTNImage);
                                }
                            }
                            else if (i == 1)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    SubsBTNiconURL = Guid.NewGuid().ToString() + fileName;
                                    SubsBTNicon = Path.Combine(Server.MapPath("~/Uploads/"), SubsBTNiconURL);
                                    upload1.SaveAs(SubsBTNicon);
                                }
                            }
                            else if (i == 2)
                            {
                                if (upload1 != null && upload1.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(upload1.FileName);
                                    BTNImageURL = Guid.NewGuid().ToString() + fileName;
                                    BTNImage = Path.Combine(Server.MapPath("~/Uploads/"), BTNImageURL);
                                    upload1.SaveAs(BTNImage);
                                }
                            }
                        }
                    }
                    if (SubsBTNImageURL != null && SubsBTNiconURL != null && BTNImageURL != null)
                    {
                        cmd = new SqlCommand("spMaster", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubsImageURL", SubsBTNImageURL);
                        cmd.Parameters.AddWithValue("@SubsIconURL", SubsBTNiconURL);
                        cmd.Parameters.AddWithValue("@SubsHeading", obj.objMasterPageSec4BTN.SubsHeading);
                        cmd.Parameters.AddWithValue("@SubsPrice", obj.objMasterPageSec4BTN.SubsPrice);
                        cmd.Parameters.AddWithValue("@SubsUnit", obj.objMasterPageSec4BTN.SubsUnit);
                        cmd.Parameters.AddWithValue("@SubssubHeading", obj.objMasterPageSec4BTN.SubssubHeading);
                        cmd.Parameters.AddWithValue("@SubsDescription", obj.objMasterPageSec4BTN.SubsDescription);
                        cmd.Parameters.AddWithValue("@SubsBtnImageURL", BTNImageURL);
                        cmd.Parameters.AddWithValue("@SubsbtnCaption", obj.objMasterPageSec4BTN.SubsbtnCaption);
                        cmd.Parameters.AddWithValue("@SubsFooter", obj.objMasterPageSec4BTN.SubsFooter);
                        cmd.Parameters.AddWithValue("@Qtype", "UpdateSec4BTN");

                    }
                    else
                    {
                        cmd = new SqlCommand("spMaster", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubsHeading", obj.objMasterPageSec4BTN.SubsHeading);
                        cmd.Parameters.AddWithValue("@SubsPrice", obj.objMasterPageSec4BTN.SubsPrice);
                        cmd.Parameters.AddWithValue("@SubsUnit", obj.objMasterPageSec4BTN.SubsUnit);
                        cmd.Parameters.AddWithValue("@SubssubHeading", obj.objMasterPageSec4BTN.SubssubHeading);
                        cmd.Parameters.AddWithValue("@SubsDescription", obj.objMasterPageSec4BTN.SubsDescription);
                        cmd.Parameters.AddWithValue("@SubsbtnCaption", obj.objMasterPageSec4BTN.SubsbtnCaption);
                        cmd.Parameters.AddWithValue("@SubsFooter", obj.objMasterPageSec4BTN.SubsFooter);
                        cmd.Parameters.AddWithValue("@Qtype", "UpdateSec4BTN2");
                    }
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "Data Is Successfully Updated...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Updated...!";
                    }
                }

                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objMasterPageSec4BTN = new Models.MasterPageSec4BTN();
                objCommic.objMasterPageSec4BTNListM = new Models.MasterPageSec4BTN().GetMasterSec4BTNData();
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

        [Authorize]
        [HttpGet]
        public ActionResult ComicAndCatagoryMaster()
        {
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
            objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();

            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DeleteComic(string id)
        {
            new Models.ComicMaster().DeleterComic(Convert.ToInt32(id));
            ViewBag.Message = "Data is Successfully Deleted...!";
            System.Text.StringBuilder sbDeleteComic = new System.Text.StringBuilder();
            List<Models.ComicMaster> objcomicdelList = new List<Models.ComicMaster>();
            objcomicdelList = new Models.ComicMaster().GetComicMasterData();
            sbDeleteComic.Append("<table width='100%' id='tblData' border='1'><tr><th>S.No.</th><th>Comic Name</th><th>Comic Price</th><th colspan="+2+">Action</th></tr>");
            foreach (Models.ComicMaster obj in objcomicdelList)
            {
                sbDeleteComic.Append("<tr><td><input type='hidden' value='" + @obj.comicID + "' data-comicname='" + @obj.comicName + "' data-price='" + @obj.comicPrice + "' data-BSM='" + @obj.ForBSM + "' data-MB='" + @obj.ForMB + "' data-MMB='" + @obj.ForMMB + "'/>" + obj.comicID + "</td>");
                sbDeleteComic.Append("<td><span>" + obj.comicName + "</span></td>");
                sbDeleteComic.Append("<td><span>" + obj.comicPrice + "&euro;</span></td>");
                sbDeleteComic.Append("<td><span><a href='#' onclick='FillEdit(" + obj.comicID + " ); return false;'>Edit</a></span></td>");
                sbDeleteComic.Append("<td><span><a href='#' onclick='Delete(" + obj.comicID + " ); return false;'>Delete</a></span></td></tr>");

           }
            return Json(sbDeleteComic.ToString());
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DeleteCategory(string id)
        {
            new Models.CatMaster().DeleteCat(Convert.ToInt32(id));
            ViewBag.Message = "Data is Successfully Deleted...!";
            System.Text.StringBuilder sbDeleteCat = new System.Text.StringBuilder();
            List<Models.CatMaster> objcatdelList = new List<Models.CatMaster>();
            objcatdelList = new Models.CatMaster().GetCatMasterData();
            sbDeleteCat.Append("<table width='100%' id='tblData' border='1'><tr><th>S.No.</th><th>Catagory Name</th><th colspan="+2+">Action</th></tr>");
            foreach (Models.CatMaster obj in objcatdelList)
            {
                sbDeleteCat.Append("<tr><td><input type='hidden' value='" + @obj.CatID + "' data-catname='" + @obj.CatName + "' data-BSM1='" + @obj.ForBSM + "' data-MB1='" + @obj.ForMB + "' data-MMB1='" + @obj.ForMMB + "'/>" + obj.CatID + "</td>");
                sbDeleteCat.Append("<td><span>" + obj.CatName + "</span></td>");
                sbDeleteCat.Append("<td><span><a href='#' onclick='FillEdit1(" + obj.CatID + " ); return false;'>Edit</a></span></td>");
                sbDeleteCat.Append("<td><span><a href='#' onclick='Delete1(" + obj.CatID + " ); return false;'>Delete</a></span></td></tr>");

            }
            return Json(sbDeleteCat.ToString());
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ComicAndCatagoryMaster(Commic.Models.Commic obj, string submit,string hdd)
        {
            try
            {
                if (submit == "Add New Comic" && submit != null && (hdd == null || hdd == "0"))
                {
                    cmd = new SqlCommand("spComicAndCat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ComicName", obj.objComicMaster.comicName);
                    cmd.Parameters.AddWithValue("@ComicPrice", obj.objComicMaster.comicPrice);
                    cmd.Parameters.AddWithValue("@ForBSM", obj.objComicMaster.ForBSM);
                    cmd.Parameters.AddWithValue("@ForMB", obj.objComicMaster.ForMB);
                    cmd.Parameters.AddWithValue("@ForMMB", obj.objComicMaster.ForMMB);
                    cmd.Parameters.AddWithValue("@Qtype", "Insert2");
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "A New Comic Is Successfully Added...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Saved...!";

                    }
                }
                else if (submit == "Add New Comic" && submit != null && (hdd != null || hdd != "0"))
                {
                    cmd = new SqlCommand("spComicAndCat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ComicName", obj.objComicMaster.comicName);
                    cmd.Parameters.AddWithValue("@ComicPrice", obj.objComicMaster.comicPrice);
                    cmd.Parameters.AddWithValue("@ForBSM", obj.objComicMaster.ForBSM);
                    cmd.Parameters.AddWithValue("@ForMB", obj.objComicMaster.ForMB);
                    cmd.Parameters.AddWithValue("@ForMMB", obj.objComicMaster.ForMMB);
                    cmd.Parameters.AddWithValue("@ID", hdd);
                    cmd.Parameters.AddWithValue("@Qtype", "Update2");
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "A New Comic Is Successfully Updated...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Updated...!";

                    }
                }
                if (submit == "Add New Category" && submit != null && (hdd == null || hdd == "0"))
                {
                    cmd = new SqlCommand("spComicAndCat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CatagoryName", obj.objCatMaster.CatName);
                    cmd.Parameters.AddWithValue("@ForBSM", obj.objCatMaster.ForBSM);
                    cmd.Parameters.AddWithValue("@ForMB", obj.objCatMaster.ForMB);
                    cmd.Parameters.AddWithValue("@ForMMB", obj.objCatMaster.ForMMB);
                    cmd.Parameters.AddWithValue("@Qtype", "Insert");
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "A New Catagory Is Successfully Added...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Saved...!";

                    }
                }
                else if (submit == "Add New Category" && submit != null && (hdd != null || hdd != "0"))
                {
                    cmd = new SqlCommand("spComicAndCat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CatagoryName", obj.objCatMaster.CatName);
                    cmd.Parameters.AddWithValue("@ForBSM", obj.objCatMaster.ForBSM);
                    cmd.Parameters.AddWithValue("@ForMB", obj.objCatMaster.ForMB);
                    cmd.Parameters.AddWithValue("@ForMMB", obj.objCatMaster.ForMMB);
                    cmd.Parameters.AddWithValue("@ID", hdd);
                    cmd.Parameters.AddWithValue("@Qtype", "Update");
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "A New Catagory Is Successfully Updated...!";

                    }
                    else
                    {
                        ViewBag.Message = "Data Is Not Successfully Updated...!";

                    }
                }
                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objComicMasterListM = new Models.ComicMaster().GetComicMasterData();
                objCommic.objCatMasterListM = new Models.CatMaster().GetCatMasterData();

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

        #region Order Related Controllers for Get Status of Order
        [Authorize]
        [HttpGet]
        public ActionResult OrderStatusManage(string id,string type)
        {
            if (id != "" && id !=null && type==null)
            {
                cmd = new SqlCommand("spOrderManagement",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Qtype", "Deactive");
                cmd.Parameters.AddWithValue("@ID",id);
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                if (result > 0)
                {
                    ViewBag.Message = "Subscription Is Successfully Deactivated...!";

                }
                else
                {
                    ViewBag.Message = "Subscription Is Not Successfully Deactivated...!";
                }
            }
            if (id != "" && id != null && type == "get")
            {
                //ObservableCollection<OrderStatus> OrderList2 = new ObservableCollection<OrderStatus>();
                //Commic.Models.Commic objCommic2 = new Models.Commic();
                //objCommic2.objOrderStatusListM=new Models.OrderStatus().GetALLOrders();
                //objCommic2.objOrderStatusM = new Models.OrderStatus().GetOrderStatusDataByOdrNo(Convert.ToInt32(id));
                //foreach (var item in objCommic2.objOrderStatusListM)
                //{
                //    OrderList2.Add(new OrderStatus { id = item.id, CreatedOn = item.CreatedOn, FirstName = item.FirstName, Name = item.Name, SubscriptionName = item.SubscriptionName, DelevaryType = item.DelevaryType, Address = item.Address + " , " + item.City + " , " + item.PostalCode, BillingAddress = item.BillingAddress + " , " + item.BillingCity + " , " + item.BillingPostalCode, DurationType = item.DurationType, DueDate = item.DueDate,liv3add =item.liv3add, OrderStatuss = item.OrderStatuss, OfferCodeStatus = item.OfferCodeStatus, IsActive = item.IsActive });
                //}
                //return View(OrderList2);
                return RedirectToAction("OrderStatusByOrderNoManage", new { id = id });
            }
            ObservableCollection<OrderStatus> OrderList = new ObservableCollection<OrderStatus>();
            //List<OrderStatus> OrderList = new List<OrderStatus>();
            //OrderList = new Models.OrderStatus().GetALLOrders();
            //return View(OrderList);

            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objOrderStatusListM = new Models.OrderStatus().GetALLOrders();
            foreach (var item in objCommic.objOrderStatusListM)
            {
                OrderList.Add(new OrderStatus { id = item.id, CreatedOn = item.CreatedOn, FirstName = item.FirstName, Name = item.Name, SubscriptionName = item.SubscriptionName, DelevaryType = item.DelevaryType, Address = item.Address + " , " + item.City + " , " + item.PostalCode, BillingAddress = item.BillingAddress + " , " + item.BillingCity + " , " + item.BillingPostalCode, DurationType = item.DurationType, DueDate = item.DueDate, liv3add = item.liv3add, OrderStatuss = item.OrderStatuss, OfferCodeStatus = item.OfferCodeStatus, IsActive = item.IsActive });
            }
            return View(OrderList);

        //    Commic.Models.Commic objCommic = new Models.Commic();
        //    objCommic.objOrderStatusListM = new Models.OrderStatus().GetALLOrders();
        //    return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OrderStatusManage(Commic.Models.Commic obj, string orderstatus, string hd, string orderno)
        {
            try
            {
                //if (orderno != "")
                //{ 
                //     cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@Qtype", "SelectOnOrderNo");
                //    cmd.Parameters.AddWithValue("@ID", orderno);
                //    con.Open();
                //    result = cmd.ExecuteNonQuery();
                //    con.Close();
                //    con.Dispose();
                //    if (result > 0)
                //    ObservableCollection<OrderStatus> OrderList1 = new ObservableCollection<OrderStatus>();
                //List<OrderStatus> OrderList = new List<OrderStatus>();
                //OrderList = new Models.OrderStatus().GetALLOrders();
                //return View(OrderList);

                //Commic.Models.Commic objCommic1 = new Models.Commic();
                //objCommic1.objOrderStatusListM = new Models.OrderStatus().GetALLOrders();
                //foreach (var item in objCommic1.objOrderStatusListM)
                //{
                //    OrderList1.Add(new OrderStatus {id=item.id, FirstName = item.FirstName,Name=item.Name, DelevaryType = item.DelevaryType, Address = item.Address + " , " + item.City + " , " + item.PostalCode, BillingAddress = item.BillingAddress + " , " + item.BillingCity + " , " + item.BillingPostalCode, DurationType = item.DurationType, TimePeriod = item.TimePeriod, OrderStatuss = item.OrderStatuss, OfferCodeStatus = item.OfferCodeStatus, IsActive = item.IsActive });
                //}
                //return View(OrderList1);
                //}
                if (hd != "" && hd!=null)
                {
                    cmd = new SqlCommand("spOrderManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Qtype", "UpdateStatus");
                    cmd.Parameters.AddWithValue("@OrderStatuss", orderstatus);
                    cmd.Parameters.AddWithValue("@ID", hd);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    if (result > 0)
                    {
                        ViewBag.Message = "Subscription Is Successfully Updated...!";

                    }
                    else
                    {
                        ViewBag.Message = "Subscription Is Not Successfully Updated...!";
                    }
                }
                ObservableCollection<OrderStatus> OrderList = new ObservableCollection<OrderStatus>();
                //List<OrderStatus> OrderList = new List<OrderStatus>();
                //OrderList = new Models.OrderStatus().GetALLOrders();
                //return View(OrderList);

                Commic.Models.Commic objCommic = new Models.Commic();
                objCommic.objOrderStatusListM = new Models.OrderStatus().GetALLOrders();
                foreach (var item in objCommic.objOrderStatusListM)
                {
                    OrderList.Add(new OrderStatus { id = item.id, CreatedOn = item.CreatedOn, FirstName = item.FirstName, Name = item.Name, SubscriptionName = item.SubscriptionName, DelevaryType = item.DelevaryType, Address = item.Address + " , " + item.City + " , " + item.PostalCode, BillingAddress = item.BillingAddress + " , " + item.BillingCity + " , " + item.BillingPostalCode, DurationType = item.DurationType, DueDate = item.DueDate, liv3add = item.liv3add, OrderStatuss = item.OrderStatuss, OfferCodeStatus = item.OfferCodeStatus, IsActive = item.IsActive });
                }
                return View(OrderList);
            }
            catch
            {
                return View();
            }

            //    Commic.Models.Commic objCommic = new Models.Commic();
            //    objCommic.objOrderStatusListM = new Models.OrderStatus().GetALLOrders();
            //    return View(objCommic);
        }


        //[HttpPost]
        //public ActionResult Edit(string hdd, string id)
        //{
        //    //derStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(hddid));
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult Edit(Commic.Models.Commic obj, string hddid, string id, string ID)
        //{

        //    //derStatus().GetOrderStatusDataByOrderNumber(Convert.ToInt32(hddid));
        //    return View("OrderStatusManage");
        //}

        [Authorize]
        [HttpGet]
        public ActionResult OrderStatusByOrderNoManage(string id, string Length)
        {
            string idd=Request.QueryString["id"].ToString();
            Commic.Models.Commic objCommic = new Models.Commic();
            objCommic.objOrderStatusM = new Models.OrderStatus().GetOrderStatusDataByOdrNo(Convert.ToInt32(idd));
            return View(objCommic);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OrderStatusByOrderNoManage(Commic.Models.Commic obj, string id)
        {
            return RedirectToAction("OrderStatusManage");
            //Commic.Models.Commic objCommic = new Models.Commic();
            //new Models.OrderStatus().SelectOrderByorderNumber(Convert.ToInt32(id));
            //return View(objCommic);
        }

        #endregion
    }
}
