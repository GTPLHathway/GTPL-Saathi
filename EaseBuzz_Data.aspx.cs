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

public partial class EaseBuzz_Data : System.Web.UI.Page
{
    OracleConnection con_orl = Connection.login2();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] == null)
        {
            string redirectUrl = FormsAuthentication.LoginUrl + "?ReturnUrl=index.aspx";
            FormsAuthentication.SignOut();
            Response.Redirect(redirectUrl);
        }
        if (!Page.IsPostBack)
        {
            demo1.SelectedDate = System.DateTime.Now;
            demo2.SelectedDate = System.DateTime.Now;
            bind_grid();

            if (Convert.ToString(Session["user"]) == "EASEBUZZ")
            {
                AndroidFun();
            }

        }
    }
    public void AndroidFun()
    {
        var LeftMenu = this.Master.FindControl("divLeftMenu");
        LeftMenu.Visible = false;
        var NevBar = this.Master.FindControl("divNavbar");
        //  NevBar.Visible = false;
        var Logo = this.Master.FindControl("divLogo");
        //  Logo.Visible = false;
    }
    public void bind_grid()
    {
        try
        {
            lbl_error.Text = "";
            string strQry = "";

            if (Convert.ToString(Session["user"]) == "EASEBUZZ")
            {
                strQry = @"SELECT E.* FROM GTPL_EASEBUZZ_DTL E, VW_OPERATIONAL_ENTITY OE WHERE E.ENTITY_ID = OE.ENTITY_ID                        
                       AND TRUNC(TBL_DATE) BETWEEN TO_DATE('" + demo1.SelectedDate.Value.Date.ToString("dd/MM/yyyy") + "','DD/MM/YYYY') AND TO_DATE('" + demo2.SelectedDate.Value.Date.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            }
            else
            {
                strQry = @"SELECT E.* FROM GTPL_EASEBUZZ_DTL E, VW_OPERATIONAL_ENTITY OE WHERE E.ENTITY_ID = OE.ENTITY_ID 
                        AND OE.ENTITY_CODE IN (SELECT DISTINCT A.ENTITY_CODE  FROM OPERATIONAL_ENTITY_TBL A ,APP_USER AU,USER_ENTITIES UE 
                        WHERE AU.USER_ID=UE.USER_ID AND UE.ENTITY_ID=A.ENTITY_ID AND UE.IS_DELETED = 0 AND AU.USER_NAME='" + Convert.ToString(Session["user"]) + "'  AND A.STATUS='A') " +
                       " AND TRUNC(TBL_DATE) BETWEEN TO_DATE('" + demo1.SelectedDate.Value.Date.ToString("dd/MM/yyyy") + "','DD/MM/YYYY') AND TO_DATE('" + demo2.SelectedDate.Value.Date.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            }

            Session["Query"] = strQry;
            grid_data.Rebind();

            grid_data.Visible = true;
            btn_Export.Visible = true;


        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
        }
    }


    protected void btn_new_Click(object sender, EventArgs e)
    {
        bind_grid();
    }

    public DataTable GetDataTable(string query)
    {
        OracleDataAdapter da = new OracleDataAdapter();
        da.SelectCommand = new OracleCommand(query, con_orl);
        DataTable dt = new DataTable();
        con_orl.Open();
        try
        {
            Session["grid"] = null;
            da.Fill(dt);
            Session["grid"] = dt;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message.ToString());
        }
        finally
        {
            con_orl.Close();
        }

        return dt;
    }



    protected void grid_data_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        string strQurey = Convert.ToString(Session["Query"]);

        if (strQurey != null || strQurey != "")
        {
            Session["grid"] = null;
            DataSet ds = DataFunction.GetDATA(strQurey);
            grid_data.DataSource = ds;
            Session["grid"] = ds;
            lbl_error.Text = "";
        }
    }



    protected void btn_Export_Click(object sender, EventArgs e)
    {
        grid_data.ExportSettings.ExportOnlyData = true;
        grid_data.ExportSettings.IgnorePaging = true;
        grid_data.ExportSettings.OpenInNewWindow = true;
        grid_data.DataSource = Session["grid"];
        grid_data.Rebind();
        grid_data.MasterTableView.ExportToExcel();
    }
}