using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class EaseBuzz_Registration : System.Web.UI.Page
{
    OracleConnection con_orl = Connection.login2();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            


            string Android = Request.Form["IsAndroid"];
            string UserName = Request.Form["username"];
            string Password = Request.Form["password"];
            if (Android == "Y")
            {
                ViewState["ANDROID"] = "Y";
                Session["login"] = "login";
                Session["screen"] = "202";
                AndroidFun();
                Session["user"] = UserName;
                Session["pass"] = Password;
            }
            else
            {
                if (Session["login"] == null)
                {
                    string redirectUrl = FormsAuthentication.LoginUrl + "?ReturnUrl=index.aspx";
                    FormsAuthentication.SignOut();
                    Response.Redirect(redirectUrl);
                }
            }

            BindEntity();
            sqlentity1.SelectCommand = "SELECT * FROM BANK WHERE DEPOSITING_BANK = 'N' AND IS_DELETED='1' ORDER BY BANK_DESCR ASC";
            drp_bankname.DataBind();
        }
    }

    public void AndroidFun()
    {
        var LeftMenu = this.Master.FindControl("divLeftMenu");
        LeftMenu.Visible = false;
        var NevBar = this.Master.FindControl("divNavbar");
        NevBar.Visible = false;
        var Logo = this.Master.FindControl("divLogo");
        Logo.Visible = false;
    }
    public void BindEntity()
    {
        string strQurey = "";

        strQurey = "SELECT DISTINCT A.ENTITY_CODE ,A.ENTITY_ID FROM OPERATIONAL_ENTITY_TBL A , APP_USER AU,USER_ENTITIES UE WHERE AU.USER_ID=UE.USER_ID AND UE.ENTITY_ID=A.ENTITY_ID AND UE.IS_DELETED = 0 AND AU.USER_NAME='" + Convert.ToString(Session["user"]) + "'  AND A.STATUS='A' ORDER BY A.ENTITY_CODE ASC";
        con_orl.Open();
        OracleDataAdapter da = new OracleDataAdapter(strQurey, con_orl);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con_orl.Close();

        drop_entity_code.DataTextField = "ENTITY_CODE";
        drop_entity_code.DataValueField = "ENTITY_ID";
        drop_entity_code.DataSource = ds;
        drop_entity_code.DataBind();

        RadComboBoxItem L1 = new RadComboBoxItem("");
        drop_entity_code.Items.Insert(0, L1);
        strQurey = "";
    }

    protected void drop_entity_code_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {

        try
        {

            ViewState["ENTITY_ID"] = Convert.ToString(drop_entity_code.SelectedValue);

            string str = "SELECT * FROM VW_OPERATIONAL_ENTITY OE, PARTY P  WHERE OE.ENTITY_ID=P.PARTY_ID AND ENTITY_ID='" + drop_entity_code.SelectedValue + "'";
            con_orl.Open();

            OracleDataAdapter da = new OracleDataAdapter(str, con_orl);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_Ntework_name.Text = Convert.ToString(ds.Tables[0].Rows[0]["NETWORK_NAME"]);
                txt_Contact_person.Text = Convert.ToString(ds.Tables[0].Rows[0]["CONTACT_NAME"]);
                txt_Address.Text = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS"]) + "," + Convert.ToString(ds.Tables[0].Rows[0]["AREA"]) + "," + Convert.ToString(ds.Tables[0].Rows[0]["CITY"]) + "," + Convert.ToString(ds.Tables[0].Rows[0]["DISTRICT"]) + "," + Convert.ToString(ds.Tables[0].Rows[0]["STATE"]);
                txt_mobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_PHONE"]);
                txt_email.Text = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL"]);
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
        }
        finally
        {
            con_orl.Close();
        }

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        OracleTransaction trans = null;
        string bank_file = "";
        string PAN_file = "";
        string Certi_file = "";


        try
        {

            string[] FileExt = file_up_bankdetail.PostedFile.FileName.Split('.');

            string FileEx = FileExt[FileExt.Length - 1];

            if (FileEx.ToLower() == "pdf" || FileEx.ToLower() == "jpg" || FileEx.ToLower() == "png" || FileEx.ToLower() == "jpeg")
            {
                dynamic bankfileUploadControl = file_up_bankdetail;
                foreach (HttpPostedFile b in bankfileUploadControl.PostedFiles)
                {

                    b.SaveAs(Server.MapPath("~/BUZZ_DOC/") + drop_entity_code.SelectedItem.Text + "_Cheque." + FileEx.ToLower());
                    bank_file = drop_entity_code.SelectedItem.Text + "_Cheque." + FileEx.ToLower();
                }

            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only Image And .pdf File Allowed')", true);
                return;
            }



            string[] FileExt1 = FileUp_PAN.PostedFile.FileName.Split('.');

            string FileEx1 = FileExt1[FileExt1.Length - 1];

            if (FileEx1.ToLower() == "pdf" || FileEx1.ToLower() == "jpg" || FileEx1.ToLower() == "png" || FileEx1.ToLower() == "jpeg")
            {
                dynamic PANfileUploadControl = FileUp_PAN;
                foreach (HttpPostedFile P in PANfileUploadControl.PostedFiles)
                {

                    P.SaveAs(Server.MapPath("~/BUZZ_DOC/") + drop_entity_code.SelectedItem.Text + "_PAN." + FileEx1.ToLower());
                    PAN_file = drop_entity_code.SelectedItem.Text + "_PAN." + FileEx1.ToLower();
                }

            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only Image And .pdf File Allowed')", true);

                return;
            }

            string[] FileExt2 = FileUp_CERTI.PostedFile.FileName.Split('.');

            string FileEx2 = FileExt2[FileExt2.Length - 1];

            if (FileEx2.ToLower() == "pdf" || FileEx2.ToLower() == "jpg" || FileEx2.ToLower() == "png" || FileEx2.ToLower() == "jpeg")
            {
                dynamic CERTIfileUploadControl = FileUp_CERTI;
                foreach (HttpPostedFile C in CERTIfileUploadControl.PostedFiles)
                {
                    C.SaveAs(Server.MapPath("~/BUZZ_DOC/") + drop_entity_code.SelectedItem.Text + "_Certi." + FileEx2.ToLower());
                    Certi_file = drop_entity_code.SelectedItem.Text + "_Certi." + FileEx2.ToLower();
                }
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only Image And .pdf File Allowed')", true);
                return;
            }



            con_orl.Open();
            trans = con_orl.BeginTransaction();

            string insqry = "INSERT INTO GTPL_EASEBUZZ_DTL (TBL_DATE,ENTITY_ID,ENTITY_CODE,BANK_ACCOUNT,ACCOUNT_FILE_NAME,PAN_NUMBER,PAN_FILE_NAME,CERTI_NAME,CERTI_NUMBER,CERTI_FILE_NAME,CREATED_BY,STATUS,MOBILE_NO,EMAIL,CONTACT_PERSON,BANK_NAME,BRANCH_NAME,IFSC_CODE,PASSWORD,CONFIRM_PASSWORD,IP_ADDRESS) " +
                                                        " VALUES (:TBL_DATE,:ENTITY_ID,:ENTITY_CODE,:BANK_ACCOUNT,:ACCOUNT_FILE_NAME,:PAN_NUMBER,:PAN_FILE_NAME,:CERTI_NAME,:CERTI_NUMBER,:CERTI_FILE_NAME,:CREATED_BY,:STATUS,:MOBILE_NO,:EMAIL,:CONTACT_PERSON,:BANK_NAME,:BRANCH_NAME,:IFSC_CODE,:PASSWORD,:CONFIRM_PASSWORD,:IP_ADDRESS)";

            OracleCommand cmd = new OracleCommand(insqry, con_orl);
            cmd.Transaction = trans;

            cmd.Parameters.Add(new OracleParameter(":TBL_DATE", System.DateTime.Now));
            cmd.Parameters.Add(new OracleParameter(":ENTITY_ID", Convert.ToString(drop_entity_code.SelectedValue)));
            cmd.Parameters.Add(new OracleParameter(":ENTITY_CODE", Convert.ToString(drop_entity_code.SelectedItem.Text)));
            cmd.Parameters.Add(new OracleParameter(":BANK_ACCOUNT", Convert.ToString(txt_bank_detail.Text)));
            cmd.Parameters.Add(new OracleParameter(":ACCOUNT_FILE_NAME", bank_file));
            cmd.Parameters.Add(new OracleParameter(":PAN_NUMBER", Convert.ToString(txt_PAN_no.Text)));
            cmd.Parameters.Add(new OracleParameter(":PAN_FILE_NAME", PAN_file));
            cmd.Parameters.Add(new OracleParameter(":CERTI_NAME", Convert.ToString(drop_certi.SelectedItem.Text)));
            cmd.Parameters.Add(new OracleParameter(":CERTI_NUMBER", Convert.ToString(txt_certi.Text)));
            cmd.Parameters.Add(new OracleParameter(":CERTI_FILE_NAME", Certi_file));
            cmd.Parameters.Add(new OracleParameter(":STATUS", Convert.ToString("A")));
            cmd.Parameters.Add(new OracleParameter(":MOBILE_NO", Convert.ToString(txt_mobile.Text)));
            cmd.Parameters.Add(new OracleParameter(":EMAIL", Convert.ToString(txt_email.Text)));
            cmd.Parameters.Add(new OracleParameter(":CONTACT_PERSON", Convert.ToString(txt_Contact_person.Text)));
            cmd.Parameters.Add(new OracleParameter(":BANK_NAME", Convert.ToString(drp_bankname.SelectedItem.Text)));
            cmd.Parameters.Add(new OracleParameter(":BRANCH_NAME", Convert.ToString(drp_brnch_name.SelectedItem.Text)));
            cmd.Parameters.Add(new OracleParameter(":IFSC_CODE", Convert.ToString(txt_IFSC.Text)));
            cmd.Parameters.Add(new OracleParameter(":PASSWORD", Convert.ToString(drop_entity_code.SelectedItem.Text)));
            cmd.Parameters.Add(new OracleParameter(":CONFIRM_PASSWORD", Convert.ToString(drop_entity_code.SelectedItem.Text)));

            string IPAddres = GetIPAddress();

            cmd.Parameters.Add(new OracleParameter(":IP_ADDRESS", Convert.ToString(IPAddres)));

            string strUserID = "SELECT USER_ID FROM APP_USER WHERE USER_NAME='" + Convert.ToString(Session["user"]) + "'";
            DataSet dsUser = DataFunction.GetDATA(strUserID);
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                cmd.Parameters.Add(new OracleParameter(":CREATED_BY", Convert.ToString(dsUser.Tables[0].Rows[0]["USER_ID"])));
            }

            cmd.ExecuteNonQuery();


            trans.Commit();


            this.Master.SuccessMessage("Data Saved Sucessfully...");

            clear();
        }
        catch (Exception ex)
        {
            if (trans != null)
            {
                trans.Rollback();
            }
            lbl_error.Text = ex.Message.ToString();
        }
        finally
        {
            con_orl.Close();
            if (trans != null)
            {
                trans.Dispose();
            }

            //BindData();
        }
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    public void clear()
    {
        drop_entity_code.ClearSelection();
        drp_bankname.ClearSelection();
        drp_brnch_name.ClearSelection();

        txt_bank_detail.Text = "";
        txt_certi.Text = "";
        txt_PAN_no.Text = "";
        txt_mobile.Text = "";
        txt_email.Text = "";
        txt_Ntework_name.Text = "";
        txt_Contact_person.Text = "";
        txt_Address.Text = "";
        txt_IFSC.Text = "";

    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void drp_bankname_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        drp_brnch_name.Enabled = true;
        sqlentity2.SelectCommand = "SELECT * FROM GTPL_PAY_BANK_DETAILS WHERE BANK_NAME = UPPER('" + Convert.ToString(drp_bankname.SelectedItem.Text) + "') ORDER BY BANK_NAME ASC";
        drp_brnch_name.DataBind();

    }

    protected void drp_brnch_name_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            txt_IFSC.Enabled = true;
            string str = "SELECT * FROM GTPL_PAY_BANK_DETAILS WHERE BRANCH_NAME = UPPER('" + Convert.ToString(drp_brnch_name.SelectedItem.Text) + "') AND BANK_NAME = UPPER('" + Convert.ToString(drp_bankname.SelectedItem.Text) + "') ";
            con_orl.Open();

            OracleDataAdapter da = new OracleDataAdapter(str, con_orl);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_IFSC.Text = Convert.ToString(ds.Tables[0].Rows[0]["IFSC"]);
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
        }
        finally
        {
            con_orl.Close();
        }
    }
}