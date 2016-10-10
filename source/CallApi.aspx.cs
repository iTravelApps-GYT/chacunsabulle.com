using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ApiPayment;
using ApiPayment.Web;
using ApiPayment.Common;
using System.Text;
using System.IO;

/*----------------------------------------------------------------------
  Topic		: Exemple code behind de traitement de la requete de paiement
  Version 	: P617
 *---------------------------------------------------------------------*/
public partial class CallApi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Execution des methodes d'appel de l'api en fonction
        //du paramètre passe dans l'url 
        switch (Request.Params.Get("RequestType"))
        {
            case "Request": RequestMethod();
                break;
            case "Response": ResponseMethod();
                break;
            case "AutoResponse": AutoResponseMethod();
                break;

              default :
                ;
                break;
                
        }
    }

    /// <summary>
    ///  traitement de la requete de paiement
    /// Dans cet exemple, on affiche un formulaire de
    /// connexion au serveur de paiement a l'internaute.
    /// </summary>
    public void RequestMethod()
    {
           try{

			Response.ContentType="text/html";

            TextWriter responseWriter = Response.Output;

			responseWriter.WriteLine("<html><head><title>SHERLOCKS - Paiement Securise sur Internet</title></head>");
			responseWriter.WriteLine("<body bgcolor=#ffffff>");
			responseWriter.WriteLine ("<Font color=#000000>");
			responseWriter.WriteLine ("<center><h1>Test de l'API C# SHERLOCKS</h1></center><br /><br />");

      /* Initialisation du chemin du fichier pathfile (a modifier)
         ex :      SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");
      */
            SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");


      /* Initialisation de l'objet d'appel */
      SIPSDataObject call = (SIPSDataObject) new SIPSCallParm();

      /**************************** Parametres obligatoires*************************/
      // ex : merchant_id = 014295303911111
      call.setValue("merchant_id","014295303911111");

      // ex : merchant_country = fr
      call.setValue("merchant_country","fr");

      // Affectation du montant de la transaction dans la plus petite unite
      // monetaire du pays
      // ex : 123,00 Euros ==> 12300 (currency_code = 978)
      call.setValue("amount","12300");

      // Affectation du code monetaire ISO 4217 pour la transaction
      // ex : Euro ==> 978
      call.setValue("currency_code","978");
      
      // Identifiant de transaction
      // ex : transaction_id = 123456
      call.setValue("transaction_id", "123456");
      /******************************************************************************/

      // Affectation d'un numero identifiant pour la transaction
      // Attention aux reserves sur l'affectation automatique
      // cf Guide du developpeur

      // Valorisation des autres donnees de la transaction
      // facultatives, a completer au besoin
      // Les valeurs proposees ne sont que des exemples
      // Les champs et leur utilisation sont expliques dans le Dictionnaire des donnees
	    //
      // call.setValue("normal_return_url","");
      // call.setValue("cancel_return_url","");
      // call.setValue("automatic_response_url","");
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
      // call.setValue("order_id","");
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
      responseWriter.WriteLine(api.sipsPaymentCallFunc(call));
	    responseWriter.WriteLine ("<br /><br />");
	    responseWriter.WriteLine ("</body>");
			responseWriter.WriteLine ("</html>");

			responseWriter.Close();

    }

    catch(Exception e){

        TextWriter responseWriter;
        Response.ContentType="text/html";
        responseWriter = Response.Output;

        responseWriter.WriteLine("<center>");
        responseWriter.WriteLine("<br />");
        responseWriter.WriteLine("Error = " + e.GetType().FullName+e.Message);
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

            responseWriter.WriteLine("<html><head><title>SHERLOCKS - Paiement Securise sur Internet</title></head>");
            responseWriter.WriteLine("<body bgcolor=#ffffff>");
            responseWriter.WriteLine("<Font color=#000000>");
            responseWriter.WriteLine("<center><h1>Test de l'API C# SHERLOCKS</h1></center><br /><br />");

            /* Initialisation du chemin du fichier pathfile (a modifier)
                ex :    SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");
             */
            SIPSApiWeb api = new SIPSApiWeb("c:\\repertoire\\pathfile");


            // Initialisation de l'objet reponse
            SIPSDataObject resp = (SIPSDataObject)new SIPSResponseParm();

            // Recuperation de la variable cryptee postee
            String cypheredtxt = Request.Params.Get("DATA");

            // Decryptage de la reponse
            resp = api.sipsPaymentResponseFunc(cypheredtxt);

            // Affichage des donnees de la reponse du serveur

            responseWriter.WriteLine("<center>\n");
            responseWriter.WriteLine("<h3>R&eacute;ponse manuelle du serveur SIPS</h3>\n");
            responseWriter.WriteLine("</center>\n");
            responseWriter.WriteLine("<b><h4>\n");
            responseWriter.WriteLine("<br /><hr>\n");
            responseWriter.WriteLine("merchant_id = " + resp.getValue("merchant_id") + "<br />");
            responseWriter.WriteLine("merchant_country = " + resp.getValue("merchant_country") + "<br />");
            responseWriter.WriteLine("amount = " + resp.getValue("amount") + "<br />");
            responseWriter.WriteLine("transaction_id = " + resp.getValue("transaction_id") + "<br />");
            responseWriter.WriteLine("transmission_date = " + resp.getValue("transmission_date") + "<br />");
            responseWriter.WriteLine("payment_means = " + resp.getValue("payment_means") + "<br />");
            responseWriter.WriteLine("payment_time = " + resp.getValue("payment_time") + "<br />");
            responseWriter.WriteLine("payment_date = " + resp.getValue("payment_date") + "<br />");
            responseWriter.WriteLine("response_code = " + resp.getValue("response_code") + "<br />");
            responseWriter.WriteLine("payment_certificate = " + resp.getValue("payment_certificate") + "<br />");
            responseWriter.WriteLine("authorisation_id = " + resp.getValue("authorisation_id") + "<br />");
            responseWriter.WriteLine("currency_code = " + resp.getValue("currency_code") + "<br />");
            responseWriter.WriteLine("card_number = " + resp.getValue("card_number") + "<br />");
            responseWriter.WriteLine("cvv_flag = " + resp.getValue("cvv_flag") + "<br />");
            responseWriter.WriteLine("cvv_response_code = " + resp.getValue("cvv_response_code") + "<br />");
            responseWriter.WriteLine("bank_response_code = " + resp.getValue("bank_response_code") + "<br />");
            responseWriter.WriteLine("complementary_code = " + resp.getValue("complementary_code") + "<br />");
            responseWriter.WriteLine("complementary_info = " + resp.getValue("complementary_info") + "<br />");
            responseWriter.WriteLine("return_context = " + resp.getValue("return_context") + "<br />");
            responseWriter.WriteLine("caddie = " + resp.getValue("caddie") + "<br />");
            responseWriter.WriteLine("receipt_complement = " + resp.getValue("receipt_complement") + "<br />");
            responseWriter.WriteLine("merchant_language = " + resp.getValue("merchant_language") + "<br />");
            responseWriter.WriteLine("language = " + resp.getValue("language") + "<br />");
            responseWriter.WriteLine("customer_id = " + resp.getValue("customer_id") + "<br />");
            responseWriter.WriteLine("order_id = " + resp.getValue("order_id") + "<br />");
            responseWriter.WriteLine("customer_email = " + resp.getValue("customer_email") + "<br />");
            responseWriter.WriteLine("customer_ip_address = " + resp.getValue("customer_ip_address") + "<br />");
            responseWriter.WriteLine("capture_day = " + resp.getValue("capture_day") + "<br />");
            responseWriter.WriteLine("capture_mode = " + resp.getValue("capture_mode") + "<br />");
            responseWriter.WriteLine("data = " + resp.getValue("data") + "<br />");
            responseWriter.WriteLine("order_validity = " + resp.getValue("order_validity") + "<br />");
            responseWriter.WriteLine("transaction_condition = " + resp.getValue("transaction_condition") + "<br />");
            responseWriter.WriteLine("statement_reference = " + resp.getValue("statement_reference") + "<br />");
            responseWriter.WriteLine("card_validity = " + resp.getValue("card_validity") + "<br />");
            responseWriter.WriteLine("score_color = " + resp.getValue("score_color") + "<br />");
            responseWriter.WriteLine("score_info = " + resp.getValue("score_info") + "<br />");
            responseWriter.WriteLine("score_value = " + resp.getValue("score_value") + "<br />");
            responseWriter.WriteLine("score_threshold = " + resp.getValue("score_threshold") + "<br />");
            responseWriter.WriteLine("score_profile = " + resp.getValue("score_profile") + "<br />");
            responseWriter.WriteLine("threed_ls_code = " + resp.getValue("threed_ls_code") + "<br />");
            responseWriter.WriteLine("threed_relegation_code = " + resp.getValue("threed_relegation_code") + "<br />");
            responseWriter.WriteLine("<br /><br /><hr></b></h4>");
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
            responseWriter.WriteLine("Error = " + e.GetType().FullName+e.Message);
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
}

