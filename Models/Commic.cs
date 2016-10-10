using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ApiPayment;
using System.Web.Helpers;

namespace Commic.Models
{
    public class Commic
    {
        public MasterPage objMasterpage { get; set; }
        public List<MasterPage> objListMaster { get; set; }
        public SlideShowSection objSlide { get; set; }
        public List<SlideShowSection> objSlideListM { get; set; }
        public Payment objpayment { get; set; }
        public List<Payment> objPaymentListM { get; set; }
        public IdentificationDetails2 objIdenDetail { get; set; }
        public SubscriptionTimeDuration objSubTimeclass { get; set; }
        public List<SubscriptionTimeDuration> objSubsTimeListM { get; set; }
        public SubscriptionDeliveryTypes objSubDeliveryclass { get; set; }
        public List<SubscriptionDeliveryTypes> objSubsDeliveryListM { get; set; }
        public SubscriptionMaster objSubMasterclass { get; set; }
        //public List<SubscriptionMaster> objSubsMasterListM { get; set; }
        public MasterPageSec4BTN objMasterPageSec4BTN { get; set; }
        public List<MasterPageSec4BTN> objMasterPageSec4BTNListM { get; set; }
        public ComicMaster objComicMaster { get; set; }
        public List<ComicMaster> objComicMasterListM { get; set; }
        public CatMaster objCatMaster { get; set; }
        public List<CatMaster> objCatMasterListM { get; set; }
        public CatComboPriceMaster CatComboPriceMaster { get; set; }
        public List<CatComboPriceMaster> objCatComboPriceMasterListM { get; set; }
        public MiniPriceMaster MiniPriceMasterobj { get; set; }
        public List<MiniPriceMaster> objMiniPriceMasterListM { get; set; }
        public MiniNMaxiPrice MiniNMaxiPriceMasterobj { get; set; }
        public List<MiniNMaxiPrice> objMiniNMaxiPriceMasterListM { get; set; }
        public Registration objRegistrationMaster { get; set; }
        public List<Registration> objRegistrationMasterListM { get; set; }
        public Login objLoginMaster { get; set; }
        public CheckPaymentGateway objChkPayGate { get; set; }
        public ForgotPassword objforgotpwd { get; set; }
        public OrderStatus objOrderStatusM { get; set; }
        public List<OrderStatus> objOrderStatusListM { get; set; }
        public ActiveVouchar objActiveVouchar { get; set; }
        public orderno objorderno { get; set; }
    }
    public class MasterPage
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string logopath { get; set; }
        public string Sec2Heading1 { get; set; }
        public string Sec2Def { get; set; }
        public string Sec2Discription { get; set; }
        public string Sec2BtnUrl { get; set; }
        public string Sec2BtnCaption { get; set; }

        public string Sec3Head1 { get; set; }
        public string Sec3Desc1 { get; set; }
        public string Sec3Image1url { get; set; }
        public string Sec3Label1Url { get; set; }
        public string Sec3Head2 { get; set; }
        public string Sec3Desc2 { get; set; }
        public string Sec3Image2url { get; set; }
        public string Sec3Label2Url { get; set; }
        public string Sec3Head3 { get; set; }
        public string Sec3Desc3 { get; set; }
        public string Sec3Image3url { get; set; }
        public string Sec3Label3Url { get; set; }
        public string Sec3BottomImageUrl { get; set; }

        public string Sec4Head { get; set; }
        public string Sec4Desc { get; set; }
        public string Sec4Para { get; set; }

        public MasterPage GetMasterData()
        {
            cmd = new SqlCommand("spMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "Select");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            MasterPage ObjMaster = new MasterPage();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ObjMaster.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    ObjMaster.logopath = dt.Rows[i]["logoURL"].ToString();
                    ObjMaster.Sec2Heading1 = dt.Rows[i]["Sec2Heading1"].ToString();
                    ObjMaster.Sec2Def = dt.Rows[i]["Sec2Def"].ToString();
                    ObjMaster.Sec2Discription = dt.Rows[i]["Sec2Discription"].ToString();
                    ObjMaster.Sec2BtnUrl = dt.Rows[i]["Sec2BtnUrl"].ToString();
                    ObjMaster.Sec2BtnCaption = dt.Rows[i]["Sec2BtnCaption"].ToString();
                    ObjMaster.Sec3Head1 = dt.Rows[i]["Sec3Head1"].ToString();
                    ObjMaster.Sec3Desc1 = dt.Rows[i]["Sec3Desc1"].ToString();
                    ObjMaster.Sec3Image1url = dt.Rows[i]["Sec3Image1url"].ToString();
                    ObjMaster.Sec3Label1Url = dt.Rows[i]["Sec3Label1Url"].ToString();
                    ObjMaster.Sec3Head2 = dt.Rows[i]["Sec3Head2"].ToString();
                    ObjMaster.Sec3Desc2 = dt.Rows[i]["Sec3Desc2"].ToString();
                    ObjMaster.Sec3Image2url = dt.Rows[i]["Sec3Image2url"].ToString();
                    ObjMaster.Sec3Label2Url = dt.Rows[i]["Sec3Label2Url"].ToString();
                    ObjMaster.Sec3Head3 = dt.Rows[i]["Sec3Head3"].ToString();
                    ObjMaster.Sec3Desc3 = dt.Rows[i]["Sec3Desc3"].ToString();
                    ObjMaster.Sec3Image3url = dt.Rows[i]["Sec3Image3url"].ToString();
                    ObjMaster.Sec3Label3Url = dt.Rows[i]["Sec3Label3Url"].ToString();
                    ObjMaster.Sec3BottomImageUrl = dt.Rows[i]["Sec3BottomImageUrl"].ToString();
                    ObjMaster.Sec4Head = dt.Rows[i]["Sec4Head"].ToString();
                    ObjMaster.Sec4Desc = dt.Rows[i]["Sec4Desc"].ToString();
                    ObjMaster.Sec4Para = dt.Rows[i]["Sec4Para"].ToString();
                }
            }

            //HttpContext.Current.Session["h"] = 12; 
            return ObjMaster;
        }
    }
    public class MasterPageSec4BTN
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string SubsImageURL { get; set; }
        public string SubsIconURL { get; set; }
        public string SubsHeading { get; set; }
        public string SubsPrice { get; set; }
        public string SubsUnit { get; set; }
        public string SubssubHeading { get; set; }
        public string SubsDescription { get; set; }
        public string SubsBtnImageURL { get; set; }
        public string SubsbtnCaption { get; set; }
        public string SubsFooter { get; set; }

        public List<MasterPageSec4BTN> GetMasterSec4BTNData()
        {
            cmd = new SqlCommand("spMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectFromSec4BTN");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<MasterPageSec4BTN> objMasterPageSec4BTNList = new List<MasterPageSec4BTN>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MasterPageSec4BTN objMasterSec4btn = new MasterPageSec4BTN();
                    objMasterSec4btn.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    objMasterSec4btn.SubsImageURL = dt.Rows[i]["SubsImageURL"].ToString();
                    objMasterSec4btn.SubsIconURL = dt.Rows[i]["SubsIconURL"].ToString();
                    objMasterSec4btn.SubsHeading = dt.Rows[i]["SubsHeading"].ToString();
                    objMasterSec4btn.SubsPrice = dt.Rows[i]["SubsPrice"].ToString();
                    objMasterSec4btn.SubsUnit = dt.Rows[i]["SubsUnit"].ToString();
                    objMasterSec4btn.SubssubHeading = dt.Rows[i]["SubssubHeading"].ToString();
                    objMasterSec4btn.SubsDescription = dt.Rows[i]["SubsDescription"].ToString();
                    objMasterSec4btn.SubsBtnImageURL = dt.Rows[i]["SubsBtnImageURL"].ToString();
                    objMasterSec4btn.SubsbtnCaption = dt.Rows[i]["SubsbtnCaption"].ToString();
                    objMasterSec4btn.SubsFooter = dt.Rows[i]["SubsFooter"].ToString();
                    objMasterPageSec4BTNList.Add(objMasterSec4btn);
                }
            }
            return objMasterPageSec4BTNList;
        }

        public void DeleteSec4Subscription(int id)
        {
            try
            {
                cmd = new SqlCommand("update SubBTNMaster set IsActive=0,IsDeactive=1 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class SlideShowSection
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string MainImageUrl { get; set; }
        public string Heading1 { get; set; }
        public string DescLine1 { get; set; }
        public string DescLine2 { get; set; }
        public string DescLine3 { get; set; }
        public string DescLine4 { get; set; }
        public string Btn1Caption { get; set; }
        public string BTn2Caption { get; set; }



        public List<SlideShowSection> GetSlideSectionData()
        {
            cmd = new SqlCommand("spSlideShowSection", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "Select");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<SlideShowSection> objSlideShowList = new List<SlideShowSection>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SlideShowSection objSlideShow = new SlideShowSection();
                    objSlideShow.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    objSlideShow.MainImageUrl = dt.Rows[i]["MainImageUrl"].ToString();
                    objSlideShow.Heading1 = dt.Rows[i]["Headline1"].ToString();
                    objSlideShow.DescLine1 = dt.Rows[i]["DescLine1"].ToString();
                    objSlideShow.DescLine2 = dt.Rows[i]["DescLine2"].ToString();
                    objSlideShow.DescLine3 = dt.Rows[i]["DescLine3"].ToString();
                    objSlideShow.DescLine4 = dt.Rows[i]["DescLine4"].ToString();
                    objSlideShow.Btn1Caption = dt.Rows[i]["Btn1Caption"].ToString();
                    objSlideShow.BTn2Caption = dt.Rows[i]["BTn2Caption"].ToString();
                    objSlideShowList.Add(objSlideShow);
                }
            }
            return objSlideShowList;
        }

        public void DeleteSlideShow(int id)
        {
            try
            {
                cmd = new SqlCommand("update SlideshowTBL set IsActive=0 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class Payment
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpDate { get; set; }
        public string SecurityCode { get; set; }
        public string TotalSubscriber { get; set; }
        public List<Payment> GetPaymentData()
        {
            cmd = new SqlCommand("spPayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "Select");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<Payment> objPaymentDataList = new List<Payment>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Payment objPayment = new Payment();
                    objPayment.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    objPayment.CardNumber = dt.Rows[i]["CardNumber"].ToString();
                    objPayment.CardHolder = dt.Rows[i]["CardHolder_Name"].ToString();
                    objPayment.ExpDate = dt.Rows[i]["Exp_Date"].ToString();
                    objPayment.SecurityCode = dt.Rows[i]["Security_Code"].ToString();
                    objPayment.TotalSubscriber = dt.Rows[i]["Total_Subscriber"].ToString();
                    objPaymentDataList.Add(objPayment);
                }
            }
            return objPaymentDataList;
        }

    }
    public class IdentificationDetails2
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OptionalSociety { get; set; }
        public string Address { get; set; }
        public string OptionalSupplement { get; set; }
        public string PostalCode { get; set; }
        public string Village { get; set; }
        public string Country { get; set; }
        public string Telephone{ get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string msg { get; set; }

    }
    public class SubscriptionTimeDuration
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string SubsTimeDuration { get; set; }
        public string SubsTimePrice { get; set; }

        public List<SubscriptionTimeDuration> GetSubscriptionDurationData()
        {
            cmd = new SqlCommand("spSubscription", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "GetFromSubsDuree");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<SubscriptionTimeDuration> objSubsTimeDataList = new List<SubscriptionTimeDuration>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SubscriptionTimeDuration objSubscriptionTime = new SubscriptionTimeDuration();
                    objSubscriptionTime.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objSubscriptionTime.SubsTimeDuration = dt.Rows[i]["SubscriptionTime"].ToString();
                    objSubscriptionTime.SubsTimePrice = dt.Rows[i]["PriceAsTime"].ToString();
                    objSubsTimeDataList.Add(objSubscriptionTime);
                }
            }
            return objSubsTimeDataList;
        }

    }
    public class SubscriptionDeliveryTypes
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryPrice { get; set; }

        public List<SubscriptionDeliveryTypes> GetSubscriptionDeliveryData()
        {
            cmd = new SqlCommand("spSubscription", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "GetFromSubsDelivery");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<SubscriptionDeliveryTypes> objSubsDeliveryDataList = new List<SubscriptionDeliveryTypes>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SubscriptionDeliveryTypes objSubscriptionDelivery = new SubscriptionDeliveryTypes();
                    objSubscriptionDelivery.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objSubscriptionDelivery.DeliveryType = dt.Rows[i]["DeliveryType"].ToString();
                    objSubscriptionDelivery.DeliveryPrice = dt.Rows[i]["DeliveryPrice"].ToString();
                    objSubsDeliveryDataList.Add(objSubscriptionDelivery);
                }
            }
            return objSubsDeliveryDataList;
        }

    }
    public class SubscriptionMaster
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string FormuleHeading { get; set; }
        public string FormuleDesc { get; set; }
        public string DureeHeading { get; set; }
        public string DureeDesc { get; set; }
        public string DeliveryHeading { get; set; }
        public string DeliveryDesc { get; set; }
        public string FirstSubsName { get; set; }
        public string FirstSubsPrice { get; set; }
        public string FirstSubsUnit { get; set; }
        public string SecondSubsName { get; set; }
        public string SecondSubsPrice { get; set; }
        public string SecondSubsUnit { get; set; }
        public string ThirdSubsName { get; set; }
        public string ThirdSubsPrice { get; set; }
        public string ThirdSubsUnit { get; set; }
        public string FourthSubsName { get; set; }
        public string FourthSubsPrice { get; set; }
        public string FourthSubsUnit { get; set; }
        public string FooterDescription { get; set; }

        public SubscriptionMaster GetSubscriptionMasterData()
        {
            cmd = new SqlCommand("spSubscription", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "GetFromSubsMater");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            //List<SubscriptionMaster> objSubsMasterDataList = new List<SubscriptionMaster>();
            SubscriptionMaster objSubscriptionMaster = new SubscriptionMaster();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    objSubscriptionMaster.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objSubscriptionMaster.FormuleHeading = dt.Rows[i]["FormuleHeading"].ToString();
                    objSubscriptionMaster.FormuleDesc = dt.Rows[i]["FormuleDesc"].ToString();
                    objSubscriptionMaster.DureeHeading = dt.Rows[i]["DureeHeading"].ToString();
                    objSubscriptionMaster.DureeDesc = dt.Rows[i]["DureeDesc"].ToString();
                    objSubscriptionMaster.DeliveryHeading = dt.Rows[i]["DeliveryHeading"].ToString();
                    objSubscriptionMaster.DeliveryDesc = dt.Rows[i]["DeliveryDesc"].ToString();
                    objSubscriptionMaster.FirstSubsName = dt.Rows[i]["FirstSubsName"].ToString();
                    objSubscriptionMaster.FirstSubsPrice = dt.Rows[i]["FirstSubsPrice"].ToString();
                    objSubscriptionMaster.FirstSubsUnit = dt.Rows[i]["FirstSubsUnit"].ToString();
                    objSubscriptionMaster.SecondSubsName = dt.Rows[i]["SecondSubsName"].ToString();
                    objSubscriptionMaster.SecondSubsPrice = dt.Rows[i]["SecondSubsPrice"].ToString();
                    objSubscriptionMaster.SecondSubsUnit = dt.Rows[i]["SecondSubsUnit"].ToString();
                    objSubscriptionMaster.ThirdSubsName = dt.Rows[i]["ThirdSubsName"].ToString();
                    objSubscriptionMaster.ThirdSubsPrice = dt.Rows[i]["ThirdSubsPrice"].ToString();
                    objSubscriptionMaster.ThirdSubsUnit = dt.Rows[i]["ThirdSubsUnit"].ToString();
                    objSubscriptionMaster.FourthSubsName = dt.Rows[i]["FourthSubsName"].ToString();
                    objSubscriptionMaster.FourthSubsPrice = dt.Rows[i]["FourthSubsPrice"].ToString();
                    objSubscriptionMaster.FourthSubsUnit = dt.Rows[i]["FourthSubsUnit"].ToString();
                    objSubscriptionMaster.FooterDescription = dt.Rows[i]["FooterDesc"].ToString();
                }
            }
            return objSubscriptionMaster;
        }

    }
    public class ComicMaster
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int comicID { get; set; }
        public string comicName { get; set; }
        public string comicPrice { get; set; }
        public bool ForBSM { get; set; }
        public bool ForMB { get; set; }
        public bool ForMMB { get; set; }
        public List<ComicMaster> GetComicMasterData()
        {
            cmd = new SqlCommand("spComicAndCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectFromComic");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<ComicMaster> objcomicMaster = new List<ComicMaster>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ComicMaster objcomic = new ComicMaster();
                    objcomic.comicID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objcomic.comicName = dt.Rows[i]["ComicName"].ToString();
                    objcomic.comicPrice = dt.Rows[i]["ComicPrice"].ToString();
                    objcomic.ForBSM = Convert.ToBoolean(dt.Rows[i]["ForBSM"]);
                    objcomic.ForMB = Convert.ToBoolean(dt.Rows[i]["ForMB"]);
                    objcomic.ForMMB = Convert.ToBoolean(dt.Rows[i]["ForMMB"]);
                    objcomicMaster.Add(objcomic);
                }
            }
            return objcomicMaster;
        }
        public void DeleterComic(int id)
        {
            try
            {
                cmd = new SqlCommand("update ComicMaster set IsActive=0,IsDeactive=1 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class CatMaster
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public int CatID { get; set; }
        public string CatName { get; set; }
        public bool  ForBSM { get; set; }
        public bool ForMB { get; set; }
        public bool ForMMB { get; set; }
        public List<CatMaster> GetCatMasterData()
        {
            cmd = new SqlCommand("spComicAndCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectFromCat");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<CatMaster> objcatagoryMaster = new List<CatMaster>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CatMaster objcatmater = new CatMaster();
                    objcatmater.CatID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objcatmater.CatName = dt.Rows[i]["CatName"].ToString();
                    objcatmater.ForBSM = Convert.ToBoolean(dt.Rows[i]["ForBSM"]);
                    objcatmater.ForMB = Convert.ToBoolean(dt.Rows[i]["ForMB"]);
                    objcatmater.ForMMB = Convert.ToBoolean(dt.Rows[i]["ForMMB"]);
                    objcatagoryMaster.Add(objcatmater);
                }
            }
            return objcatagoryMaster;
        }
        public void DeleteCat(int id)
        {
            try
            {
                cmd = new SqlCommand("update CatagoryMaster set IsActive=0,IsDeactive=1 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class CatComboPriceMaster
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public int ID { get; set; }
        public string CatComboPrice { get; set; }

        public List<CatComboPriceMaster> GetCatComboData()
        {
            cmd = new SqlCommand("spComicAndCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectCatComboPrice");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<CatComboPriceMaster> objCatComboMaster = new List<CatComboPriceMaster>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CatComboPriceMaster objcatPriceCombo = new CatComboPriceMaster();
                    objcatPriceCombo.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objcatPriceCombo.CatComboPrice = dt.Rows[i]["CategoryPrice"].ToString();
                    objCatComboMaster.Add(objcatPriceCombo);
                }
            }
            return objCatComboMaster;
        }
        public void DeleteCatCombo(int id)
        {
            try
            {
                cmd = new SqlCommand("update Catagoryprice set IsActive=0 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class MiniPriceMaster
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public int ID { get; set; }
        public string Price { get; set; }

        public List<MiniPriceMaster> GetMiniPriceData()
        {
            cmd = new SqlCommand("spComicAndCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectMiniPrice");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<MiniPriceMaster> objMiniPrice = new List<MiniPriceMaster>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiniPriceMaster MiniPriceobj = new MiniPriceMaster();
                    MiniPriceobj.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    MiniPriceobj.Price = dt.Rows[i]["Price"].ToString();
                    objMiniPrice.Add(MiniPriceobj);
                }
            }
            return objMiniPrice;
        }
        public void DeleteCatCombo(int id)
        {
            try
            {
                cmd = new SqlCommand("update MiniPricingTBL set IsActive=0 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class MiniNMaxiPrice
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public int ID { get; set; }
        public string Price { get; set; }

        public List<MiniNMaxiPrice> GetMiniNMaxiPriceData()
        {
            cmd = new SqlCommand("spComicAndCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectMiniNMaxiPrice");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<MiniNMaxiPrice> objMiniNMaxiPrice = new List<MiniNMaxiPrice>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MiniNMaxiPrice MiniNMaxiPriceobj = new MiniNMaxiPrice();
                    MiniNMaxiPriceobj.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    MiniNMaxiPriceobj.Price = dt.Rows[i]["Price"].ToString();
                    objMiniNMaxiPrice.Add(MiniNMaxiPriceobj);
                }
            }
            return objMiniNMaxiPrice;
        }
        public void DeleteCatCombo(int id)
        {
            try
            {
                cmd = new SqlCommand("update MiniNMaxiPricingTBL set IsActive=0 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class Registration
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string CupanCode { get; set; }
        public string CupanDate { get; set; }
        public string CupanHolderID{ get; set; }

        public List<Registration> GetRegistrationData()
        {
            cmd = new SqlCommand("spRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "Select");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            List<Registration> objRegistrationDataList = new List<Registration>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Registration objRegistration = new Registration();
                    objRegistration.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objRegistration.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objRegistration.LastName = dt.Rows[i]["LastName"].ToString();
                    objRegistration.EmailID = dt.Rows[i]["EmailID"].ToString();
                    objRegistration.UserID = dt.Rows[i]["UserID"].ToString();
                    objRegistration.Password = dt.Rows[i]["Password"].ToString();
                    objRegistration.CupanCode = dt.Rows[i]["CupanCode"].ToString();
                    objRegistration.CupanDate = dt.Rows[i]["CupanDate"].ToString();
                    objRegistration.CupanHolderID = dt.Rows[i]["CupanHolderID"].ToString();
                    objRegistrationDataList.Add(objRegistration);
                }
            }
            return objRegistrationDataList;
        }

        public void DeleteRegistration(int id)
        {
            try
            {
                cmd = new SqlCommand("update RegistrationTBL set IsActive=0,Isdeactive=1 where id=" + id, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
    public class Login
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        public string UserID { get; set; }
        public string Password { get; set; }
        public string querystring { get; set; }
    }
    public class CheckPaymentGateway
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        // Required Properties

        public string UserId { get; set; }
        public string CartNumber { get; set; }
        public decimal TotalAmt { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string ville { get; set; }
        public string PostCode { get; set; }

    }
    public class ForgotPassword
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion
        // Required Properties
        public string UserID { get; set; }
        //public string Name { get; set; }
    }
    public class OrderStatus
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public int id { get; set; }
        public string UserID { get; set; }
        public string SubscriptionName { get; set; }
        public string SubscriptionPrice { get; set; }
        public string DurationType { get; set; }
        public string DelevaryType { get; set; }
        public string DelevaryPrice { get; set; }
        public string AdultComicCategory { get; set; }
        public string AdultComicNames { get; set; }
        public string KidComicCategory { get; set; }
        public string KidComicNames { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string ComicSelectionPrice { get; set; }
        public string TotalAmt { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Optional_Company { get; set; }
        public string Address { get; set; }
        public string Optional_Supplement { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingName { get; set; }
        public string BillingOptional_Company { get; set; }
        public string BillingAddress { get; set; }
        public string BillingOptional_Supplement { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCity { get; set; }
        public string BillingTelephone { get; set; }
        public string OfferFirstName { get; set; }
        public string OfferName { get; set; }
        public string OfferEmail { get; set; }
        public string OfferNameAndSername { get; set; }
        public string OfferMsg { get; set; }
        public string OfferSandBy { get; set; }
        public string SubscriptionType { get; set; }
        public string TimePeriod { get; set; }
        public string DueDate { get; set; }
        public string liv3add { get; set; }
        public string CreatedOn { get; set; }
        public string OrderStatuss { get; set; }
        public string OfferCodeStatus { get; set; }
        public bool IsActive { get; set; }

        public List<OrderStatus> GetOrderStatusDataByUserId()
        {
            cmd = new SqlCommand("spOrderManagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Qtype", "Select");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            List<OrderStatus> objorderstatusdata = new List<OrderStatus>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OrderStatus objorderstatus = new OrderStatus();
                    objorderstatus.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    //objorderstatus.UserID = dt.Rows[i]["UserID"].ToString();
                    objorderstatus.SubscriptionName = dt.Rows[i]["SubscriptionName"].ToString();
                    objorderstatus.SubscriptionPrice = dt.Rows[i]["SubscriptionPrice"].ToString();
                    objorderstatus.DurationType = dt.Rows[i]["DurationType"].ToString();
                    objorderstatus.DelevaryType = dt.Rows[i]["DelevaryType"].ToString();
                    objorderstatus.DelevaryPrice = dt.Rows[i]["DelevaryPrice"].ToString();
                    objorderstatus.AdultComicCategory = dt.Rows[i]["AdultComicCategory"].ToString();
                    objorderstatus.AdultComicNames = dt.Rows[i]["AdultComicNames"].ToString();
                    objorderstatus.KidComicCategory = dt.Rows[i]["KidComicCategory"].ToString();
                    objorderstatus.KidComicNames = dt.Rows[i]["KidComicNames"].ToString();
                    objorderstatus.Gender = dt.Rows[i]["Gender"].ToString();
                    objorderstatus.Age = dt.Rows[i]["Age"].ToString();

                    objorderstatus.ComicSelectionPrice = dt.Rows[i]["ComicSelectionPrice"].ToString();
                    objorderstatus.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                    objorderstatus.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objorderstatus.Name = dt.Rows[i]["Name"].ToString();
                    objorderstatus.Optional_Company = dt.Rows[i]["Optional_Company"].ToString();
                    objorderstatus.Address = dt.Rows[i]["Address"].ToString();
                    objorderstatus.Optional_Supplement = dt.Rows[i]["Optional_Supplement"].ToString();
                    objorderstatus.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    objorderstatus.City = dt.Rows[i]["City"].ToString();
                    objorderstatus.Telephone = dt.Rows[i]["Telephone"].ToString();

                    objorderstatus.BillingFirstName = dt.Rows[i]["BillingFirstName"].ToString();
                    objorderstatus.BillingName = dt.Rows[i]["BillingName"].ToString();
                    objorderstatus.BillingOptional_Company = dt.Rows[i]["BillingOptional_Company"].ToString();
                    objorderstatus.BillingAddress = dt.Rows[i]["BillingAddress"].ToString();
                    objorderstatus.BillingOptional_Supplement = dt.Rows[i]["BillingOptional_Supplement"].ToString();
                    objorderstatus.BillingPostalCode = dt.Rows[i]["BillingPostalCode"].ToString();
                    objorderstatus.BillingCity = dt.Rows[i]["BillingCity"].ToString();
                    objorderstatus.BillingTelephone = dt.Rows[i]["BillingTelephone"].ToString();

                    objorderstatus.OfferFirstName = dt.Rows[i]["OfferFirstName"].ToString();
                    objorderstatus.OfferName = dt.Rows[i]["OfferName"].ToString();
                    objorderstatus.OfferEmail = dt.Rows[i]["OfferEmail"].ToString();
                    objorderstatus.OfferNameAndSername = dt.Rows[i]["OfferNameAndSername"].ToString();
                    objorderstatus.OfferMsg = dt.Rows[i]["OfferMsg"].ToString();
                    objorderstatus.OfferSandBy = dt.Rows[i]["OfferSandBy"].ToString();
                    objorderstatus.SubscriptionType = dt.Rows[i]["SubscriptionType"].ToString();
                    objorderstatusdata.Add(objorderstatus);
                }
            }
            return objorderstatusdata;
        }
        public OrderStatus GetOrderStatusDataByOrderNumber(int id)
        {
            cmd = new SqlCommand("spOrderManagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", HttpContext.Current.Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Qtype", "SelectByOrderNumber");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            OrderStatus objorderstatus = new OrderStatus();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //objorderstatus.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    //objorderstatus.UserID = dt.Rows[i]["UserID"].ToString();
                    objorderstatus.SubscriptionName = dt.Rows[i]["SubscriptionName"].ToString();
                    objorderstatus.SubscriptionPrice = dt.Rows[i]["SubscriptionPrice"].ToString();
                    objorderstatus.DurationType = dt.Rows[i]["DurationType"].ToString();
                    objorderstatus.DelevaryType = dt.Rows[i]["DelevaryType"].ToString();
                    objorderstatus.DelevaryPrice = dt.Rows[i]["DelevaryPrice"].ToString();
                    objorderstatus.AdultComicCategory = dt.Rows[i]["AdultComicCategory"].ToString();
                    objorderstatus.AdultComicNames = dt.Rows[i]["AdultComicNames"].ToString();
                    objorderstatus.KidComicCategory = dt.Rows[i]["KidComicCategory"].ToString();
                    objorderstatus.KidComicNames = dt.Rows[i]["KidComicNames"].ToString();
                    objorderstatus.Gender = dt.Rows[i]["Gender"].ToString();
                    objorderstatus.Age = dt.Rows[i]["Age"].ToString();

                    objorderstatus.ComicSelectionPrice = dt.Rows[i]["ComicSelectionPrice"].ToString();
                    objorderstatus.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                    objorderstatus.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objorderstatus.Name = dt.Rows[i]["Name"].ToString();
                    objorderstatus.Optional_Company = dt.Rows[i]["Optional_Company"].ToString();
                    objorderstatus.Address = dt.Rows[i]["Address"].ToString();
                    objorderstatus.Optional_Supplement = dt.Rows[i]["Optional_Supplement"].ToString();
                    objorderstatus.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    objorderstatus.City = dt.Rows[i]["City"].ToString();
                    objorderstatus.Telephone = dt.Rows[i]["Telephone"].ToString();

                    objorderstatus.BillingFirstName = dt.Rows[i]["BillingFirstName"].ToString();
                    objorderstatus.BillingName = dt.Rows[i]["BillingName"].ToString();
                    objorderstatus.BillingOptional_Company = dt.Rows[i]["BillingOptional_Company"].ToString();
                    objorderstatus.BillingAddress = dt.Rows[i]["BillingAddress"].ToString();
                    objorderstatus.BillingOptional_Supplement = dt.Rows[i]["BillingOptional_Supplement"].ToString();
                    objorderstatus.BillingPostalCode = dt.Rows[i]["BillingPostalCode"].ToString();
                    objorderstatus.BillingCity = dt.Rows[i]["BillingCity"].ToString();
                    objorderstatus.BillingTelephone = dt.Rows[i]["BillingTelephone"].ToString();

                    objorderstatus.OfferFirstName = dt.Rows[i]["OfferFirstName"].ToString();
                    objorderstatus.OfferName = dt.Rows[i]["OfferName"].ToString();
                    objorderstatus.OfferEmail = dt.Rows[i]["OfferEmail"].ToString();
                    objorderstatus.OfferNameAndSername = dt.Rows[i]["OfferNameAndSername"].ToString();
                    objorderstatus.OfferMsg = dt.Rows[i]["OfferMsg"].ToString();
                    objorderstatus.OfferSandBy = dt.Rows[i]["OfferSandBy"].ToString();
                    objorderstatus.SubscriptionType = dt.Rows[i]["SubscriptionType"].ToString();
                }
            }
            return objorderstatus;
        }
        public OrderStatus GetOrderStatusDataByOdrNo(int id)
        {
            cmd = new SqlCommand("spOrderManagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Qtype", "SelectByOdrNo");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            OrderStatus objorderstatus = new OrderStatus();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //objorderstatus.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    //objorderstatus.UserID = dt.Rows[i]["UserID"].ToString();
                    objorderstatus.SubscriptionName = dt.Rows[i]["SubscriptionName"].ToString();
                    objorderstatus.SubscriptionPrice = dt.Rows[i]["SubscriptionPrice"].ToString();
                    objorderstatus.DurationType = dt.Rows[i]["DurationType"].ToString();
                    objorderstatus.DelevaryType = dt.Rows[i]["DelevaryType"].ToString();
                    objorderstatus.DelevaryPrice = dt.Rows[i]["DelevaryPrice"].ToString();
                    objorderstatus.AdultComicCategory = dt.Rows[i]["AdultComicCategory"].ToString();
                    objorderstatus.AdultComicNames = dt.Rows[i]["AdultComicNames"].ToString();
                    objorderstatus.KidComicCategory = dt.Rows[i]["KidComicCategory"].ToString();
                    objorderstatus.KidComicNames = dt.Rows[i]["KidComicNames"].ToString();
                    objorderstatus.Gender = dt.Rows[i]["Gender"].ToString();
                    objorderstatus.Age = dt.Rows[i]["Age"].ToString();

                    objorderstatus.ComicSelectionPrice = dt.Rows[i]["ComicSelectionPrice"].ToString();
                    objorderstatus.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                    objorderstatus.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objorderstatus.Name = dt.Rows[i]["Name"].ToString();
                    objorderstatus.Optional_Company = dt.Rows[i]["Optional_Company"].ToString();
                    objorderstatus.Address = dt.Rows[i]["Address"].ToString();
                    objorderstatus.Optional_Supplement = dt.Rows[i]["Optional_Supplement"].ToString();
                    objorderstatus.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    objorderstatus.City = dt.Rows[i]["City"].ToString();
                    objorderstatus.Telephone = dt.Rows[i]["Telephone"].ToString();

                    objorderstatus.BillingFirstName = dt.Rows[i]["BillingFirstName"].ToString();
                    objorderstatus.BillingName = dt.Rows[i]["BillingName"].ToString();
                    objorderstatus.BillingOptional_Company = dt.Rows[i]["BillingOptional_Company"].ToString();
                    objorderstatus.BillingAddress = dt.Rows[i]["BillingAddress"].ToString();
                    objorderstatus.BillingOptional_Supplement = dt.Rows[i]["BillingOptional_Supplement"].ToString();
                    objorderstatus.BillingPostalCode = dt.Rows[i]["BillingPostalCode"].ToString();
                    objorderstatus.BillingCity = dt.Rows[i]["BillingCity"].ToString();
                    objorderstatus.BillingTelephone = dt.Rows[i]["BillingTelephone"].ToString();

                    objorderstatus.OfferFirstName = dt.Rows[i]["OfferFirstName"].ToString();
                    objorderstatus.OfferName = dt.Rows[i]["OfferName"].ToString();
                    objorderstatus.OfferEmail = dt.Rows[i]["OfferEmail"].ToString();
                    objorderstatus.OfferNameAndSername = dt.Rows[i]["OfferNameAndSername"].ToString();
                    objorderstatus.OfferMsg = dt.Rows[i]["OfferMsg"].ToString();
                    objorderstatus.OfferSandBy = dt.Rows[i]["OfferSandBy"].ToString();
                    objorderstatus.liv3add = dt.Rows[i]["liv3Add"].ToString();
                    objorderstatus.SubscriptionType = dt.Rows[i]["SubscriptionType"].ToString();
                }
            }
            return objorderstatus;
        }
        public List<OrderStatus> GetALLOrders()
        {
            cmd = new SqlCommand("spOrderManagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qtype", "SelectAllOrder");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            List<OrderStatus> objorderstatusdata = new List<OrderStatus>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OrderStatus objorderstatus = new OrderStatus();
                    objorderstatus.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objorderstatus.CreatedOn = dt.Rows[i]["CreatedOn"].ToString();
                    //objorderstatus.UserID = dt.Rows[i]["UserID"].ToString();
                    objorderstatus.SubscriptionName = dt.Rows[i]["SubscriptionName"].ToString();
                    //objorderstatus.SubscriptionPrice = dt.Rows[i]["SubscriptionPrice"].ToString();
                    objorderstatus.DurationType = dt.Rows[i]["DurationType"].ToString();
                    objorderstatus.DelevaryType = dt.Rows[i]["DelevaryType"].ToString();
                    //objorderstatus.DelevaryPrice = dt.Rows[i]["DelevaryPrice"].ToString();
                    //objorderstatus.AdultComicCategory = dt.Rows[i]["AdultComicCategory"].ToString();
                    //objorderstatus.AdultComicNames = dt.Rows[i]["AdultComicNames"].ToString();
                    //objorderstatus.KidComicCategory = dt.Rows[i]["KidComicCategory"].ToString();
                    //objorderstatus.KidComicNames = dt.Rows[i]["KidComicNames"].ToString();
                    //objorderstatus.Gender = dt.Rows[i]["Gender"].ToString();
                    //objorderstatus.Age = dt.Rows[i]["Age"].ToString();

                    //objorderstatus.ComicSelectionPrice = dt.Rows[i]["ComicSelectionPrice"].ToString();
                    //objorderstatus.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                    objorderstatus.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objorderstatus.Name = dt.Rows[i]["Name"].ToString();
                    //objorderstatus.Optional_Company = dt.Rows[i]["Optional_Company"].ToString();
                    objorderstatus.Address = dt.Rows[i]["Address"].ToString();
                    //objorderstatus.Optional_Supplement = dt.Rows[i]["Optional_Supplement"].ToString();
                    objorderstatus.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    objorderstatus.City = dt.Rows[i]["City"].ToString();
                    //objorderstatus.Telephone = dt.Rows[i]["Telephone"].ToString();

                    //objorderstatus.BillingFirstName = dt.Rows[i]["BillingFirstName"].ToString();
                    //objorderstatus.BillingName = dt.Rows[i]["BillingName"].ToString();
                    //objorderstatus.BillingOptional_Company = dt.Rows[i]["BillingOptional_Company"].ToString();
                    objorderstatus.BillingAddress = dt.Rows[i]["BillingAddress"].ToString();
                    //objorderstatus.BillingOptional_Supplement = dt.Rows[i]["BillingOptional_Supplement"].ToString();
                    objorderstatus.BillingPostalCode = dt.Rows[i]["BillingPostalCode"].ToString();
                    objorderstatus.BillingCity = dt.Rows[i]["BillingCity"].ToString();
                    //objorderstatus.BillingTelephone = dt.Rows[i]["BillingTelephone"].ToString();

                    //objorderstatus.OfferFirstName = dt.Rows[i]["OfferFirstName"].ToString();
                    //objorderstatus.OfferName = dt.Rows[i]["OfferName"].ToString();
                    //objorderstatus.OfferEmail = dt.Rows[i]["OfferEmail"].ToString();
                    //objorderstatus.OfferNameAndSername = dt.Rows[i]["OfferNameAndSername"].ToString();
                    //objorderstatus.OfferMsg = dt.Rows[i]["OfferMsg"].ToString();
                    //objorderstatus.OfferSandBy = dt.Rows[i]["OfferSandBy"].ToString();
                    //objorderstatus.SubscriptionType = dt.Rows[i]["SubscriptionType"].ToString();
                    //objorderstatus.TimePeriod = dt.Rows[i]["TimePeriod"].ToString();
                    objorderstatus.DueDate = dt.Rows[i]["DueDate"].ToString();
                    objorderstatus.liv3add = dt.Rows[i]["liv3Add"].ToString();
                    objorderstatus.OrderStatuss = dt.Rows[i]["OrderStatuss"].ToString();
                    objorderstatus.OfferCodeStatus = dt.Rows[i]["OfferCodeStatus"].ToString();
                    objorderstatus.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"]);
                    objorderstatusdata.Add(objorderstatus);
                }
            }
            return objorderstatusdata;
        }
        public OrderStatus SelectOrderByorderNumber(int id)
        {
            cmd = new SqlCommand("spOrderManagement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@Qtype", "SelectOrderByorderNumber");
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            OrderStatus objorderstatus = new OrderStatus();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //objorderstatus.id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    //objorderstatus.UserID = dt.Rows[i]["UserID"].ToString();
                    objorderstatus.SubscriptionName = dt.Rows[i]["SubscriptionName"].ToString();
                    objorderstatus.SubscriptionPrice = dt.Rows[i]["SubscriptionPrice"].ToString();
                    objorderstatus.DurationType = dt.Rows[i]["DurationType"].ToString();
                    objorderstatus.DelevaryType = dt.Rows[i]["DelevaryType"].ToString();
                    objorderstatus.DelevaryPrice = dt.Rows[i]["DelevaryPrice"].ToString();
                    objorderstatus.AdultComicCategory = dt.Rows[i]["AdultComicCategory"].ToString();
                    objorderstatus.AdultComicNames = dt.Rows[i]["AdultComicNames"].ToString();
                    objorderstatus.KidComicCategory = dt.Rows[i]["KidComicCategory"].ToString();
                    objorderstatus.KidComicNames = dt.Rows[i]["KidComicNames"].ToString();
                    objorderstatus.Gender = dt.Rows[i]["Gender"].ToString();
                    objorderstatus.Age = dt.Rows[i]["Age"].ToString();

                    objorderstatus.ComicSelectionPrice = dt.Rows[i]["ComicSelectionPrice"].ToString();
                    objorderstatus.TotalAmt = dt.Rows[i]["TotalAmt"].ToString();
                    objorderstatus.FirstName = dt.Rows[i]["FirstName"].ToString();
                    objorderstatus.Name = dt.Rows[i]["Name"].ToString();
                    objorderstatus.Optional_Company = dt.Rows[i]["Optional_Company"].ToString();
                    objorderstatus.Address = dt.Rows[i]["Address"].ToString();
                    objorderstatus.Optional_Supplement = dt.Rows[i]["Optional_Supplement"].ToString();
                    objorderstatus.PostalCode = dt.Rows[i]["PostalCode"].ToString();
                    objorderstatus.City = dt.Rows[i]["City"].ToString();
                    objorderstatus.Telephone = dt.Rows[i]["Telephone"].ToString();

                    objorderstatus.BillingFirstName = dt.Rows[i]["BillingFirstName"].ToString();
                    objorderstatus.BillingName = dt.Rows[i]["BillingName"].ToString();
                    objorderstatus.BillingOptional_Company = dt.Rows[i]["BillingOptional_Company"].ToString();
                    objorderstatus.BillingAddress = dt.Rows[i]["BillingAddress"].ToString();
                    objorderstatus.BillingOptional_Supplement = dt.Rows[i]["BillingOptional_Supplement"].ToString();
                    objorderstatus.BillingPostalCode = dt.Rows[i]["BillingPostalCode"].ToString();
                    objorderstatus.BillingCity = dt.Rows[i]["BillingCity"].ToString();
                    objorderstatus.BillingTelephone = dt.Rows[i]["BillingTelephone"].ToString();

                    objorderstatus.OfferFirstName = dt.Rows[i]["OfferFirstName"].ToString();
                    objorderstatus.OfferName = dt.Rows[i]["OfferName"].ToString();
                    objorderstatus.OfferEmail = dt.Rows[i]["OfferEmail"].ToString();
                    objorderstatus.OfferNameAndSername = dt.Rows[i]["OfferNameAndSername"].ToString();
                    objorderstatus.OfferMsg = dt.Rows[i]["OfferMsg"].ToString();
                    objorderstatus.OfferSandBy = dt.Rows[i]["OfferSandBy"].ToString();
                    objorderstatus.SubscriptionType = dt.Rows[i]["SubscriptionType"].ToString();
                }
            }
            return objorderstatus;
        }
    }
    public class ActiveVouchar
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        //public string UserID { get; set; }
        public string VoucharCode { get; set; }

    }
    public class orderno
    {
        #region Variables Used

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;

        #endregion

        public string orderno1 { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
    }
}