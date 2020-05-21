using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Services;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.SessionState;
using Telerik.Web.UI;

public partial class Nonrenewal_customer : System.Web.UI.Page
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

        }
    }

    public void bind_grid()
    {
        try
        {
            lbl_error.Text = "";
            string strQry = "";

            //strQry = @"SELECT A.*, TO_CHAR(INACTIVE_DATE,'DD/MM/YYYY') INACTIVE_DATE_NEW FROM(
            //                SELECT DISTINCT CT.CUSTOMER_ID,PA.STATE,OE.BRANCH_NAME,OE.UNIT_NAME,PA.CITY,PA.STREET AREA,PA.ADDRLINE2 DAS_PHASE,FT.ATTRIBUTE9 ENTITY_TYPE,
            //                OE.ENTITY_ID,OE.ENTITY_CODE,OE.NETWORK_NAME,CT.CUSTOMER_NBR,PT.TITLE, PT.FIRST_NAME||' '||PT.MIDDLE_NAME||' '||PT.LAST_NAME CUSTOMER_NAME,
            //                PA.ADDRLINE1 ADDRESS,PA.ZIPCODE,PT.MOBILE_PHONE,IM.ITEM_DESCR STB_TYPE,IM.SD_HD,SD.SERIAL_NUMBER,
            //                CASE WHEN SD.ITEM_ID IN(10,14,15,19,20,25,32,34,37,39,46,49) THEN DT.PROV_ATTRIBUTE ELSE NVL(HW.SERIAL_NUMBER,SD.PROV_ATTRIBUTE) END VC_CARD,IM.SYSTEM_NAME,
            //                CASE WHEN SD.MISSING_STATUS = 'UNRETRIV' THEN 'UN-RETRIEVABLE' ELSE SD.MISSING_STATUS END MISSING_STATUS,SO.EFFECTIVE_DATE INACTIVE_DATE,
            //                CASE WHEN CN.STATUS = 'E' THEN 'SYSTEM DC' ELSE 'USER DC' END INACTIVE_BY,AU.USER_NAME,TRUNC(SYSDATE -1) RUN_DATE
            //                FROM CUSTOMER_TBL CT,PARTY PT,ITEM_SERIAL_DTL SD,ITEM_SERIAL_DTL DT,ITEM_SERIAL_DTL HW,SERVICE_ORDER SO,CONTRACT CN,VW_OPERATIONAL_ENTITY OE,PARTY_ADDRESS PA,
            //                STB_MASTER IM,APP_USER AU,FLEX_TABLE_ATTR_VALUE FT
            //                WHERE CT.STATUS = 'I' AND CT.CUSTOMER_ID = PT.PARTY_ID AND CT.CUSTOMER_ID = SD.PARTY_ID AND SD.MRN_ID != 0
            //                AND SD.ITEM_DETAIL_ID=DT.ITEM_DETAIL_ID(+) AND SD.PARTY_ID = HW.PARTY_ID(+)
            //                AND CT.CUSTOMER_ID = SO.CUSTOMER_ID AND SO.ORDER_ID = (SELECT MAX(ORDER_ID) FROM SERVICE_ORDER SO WHERE CT.CUSTOMER_ID = SO.CUSTOMER_ID)
            //                AND SO.CONTRACT_ID = CN.CONTRACT_ID AND CT.OPENTITY_ID = OE.ENTITY_ID AND (CT.CUSTOMER_ID = PA.PARTY_ID AND PA.ADDRESSTYPE_CODE='PRI') 
            //                AND SD.ITEM_ID = IM.ITEM_ID AND SD.ITEM_ID(+) NOT IN(12,35) AND DT.ITEM_ID(+) IN(10,14,15,19,20,25,32,34,37,39,46,49) AND HW.ITEM_ID(+) IN(12,35)
            //                AND CN.LAST_UPD_DATE = (SELECT MAX(C.LAST_UPD_DATE)FROM CONTRACT C WHERE SO.CONTRACT_ID = C.CONTRACT_ID) AND CN.LAST_UPD_BY = AU.USER_ID
            //                AND OE.ENTITY_ID = FT.KEY_ID AND FT.FLEX_TABLE_CODE = 'FRM117' 
            //                AND SD.ITEM_DETAIL_ID NOT IN(SELECT ITEM_DETAIL_ID FROM ITEM_SERIAL_DTL WHERE CURRENT_LOCATION IN('WDWH','DISUWH'))
            //                AND OE.ENTITY_CODE IN 
            //                (SELECT DISTINCT A.ENTITY_CODE  FROM OPERATIONAL_ENTITY_TBL A ,APP_USER AU,USER_ENTITIES UE WHERE AU.USER_ID=UE.USER_ID AND UE.ENTITY_ID=A.ENTITY_ID AND UE.IS_DELETED = 0 AND AU.USER_NAME='"+Convert.ToString(Session["user"]) +"'  AND A.STATUS='A') "+
            //               @"  AND CT.CUSTOMER_NBR NOT IN (SELECT CUSTOMER_NBR FROM GTPL_NONRENEWAL_CUST) 
            //                UNION ALL
            //                SELECT DISTINCT CT.CUSTOMER_ID,OE.STATE,OE.BRANCH_NAME,OE.UNIT_NAME,OE.CITY,OE.AREA,OE.DAS_PHASE,FT.ATTRIBUTE9 ENTITY_TYPE,OE.ENTITY_ID,OE.ENTITY_CODE,
            //                OE.NETWORK_NAME,CT.CUSTOMER_NBR,PT.TITLE, PT.FIRST_NAME||' '||PT.MIDDLE_NAME||' '||PT.LAST_NAME CUSTOMER_NAME,PA.ADDRLINE1 ADDRESS,PA.ZIPCODE,
            //                PT.MOBILE_PHONE,IM.ITEM_DESCR STB_TYPE,IM.SD_HD,NVL(SD.SERIAL_NUMBER ,0)STB_NUMBER,
            //                NVL(CASE WHEN SD.ITEM_ID IN(10,14,15,19,20,25,32,34,37,41,49,51,52,53) THEN DT.PROV_ATTRIBUTE ELSE HW.SERIAL_NUMBER END,SD.PROV_ATTRIBUTE) VC_CARD,
            //                IM.SYSTEM_NAME,SD.MISSING_STATUS,SD.LAST_TXN_DATE INACTIVE_DATE,NULL INACTIVE_BY,NULL USER_NAME,TRUNC(SYSDATE-1) RUN_DATE
            //                FROM CUSTOMER_TBL CT,PARTY PT,PARTY_ADDRESS PA,ITEM_SERIAL_DTL SD,ITEM_SERIAL_DTL DT,ITEM_SERIAL_DTL HW,VW_OPERATIONAL_ENTITY OE,STB_MASTER IM,FLEX_TABLE_ATTR_VALUE FT
            //                WHERE CT.CUSTOMER_ID = SD.PARTY_ID AND SD.MRN_ID != 0 AND SD.ITEM_DETAIL_ID=DT.ITEM_DETAIL_ID(+) AND SD.PARTY_ID = HW.PARTY_ID(+) AND CT.CUSTOMER_ID=PT.PARTY_ID 
            //                AND CT.CUSTOMER_ID=PA.PARTY_ID AND CT.OPENTITY_ID=OE.ENTITY_ID AND SD.ITEM_ID=IM.ITEM_ID AND SD.ITEM_ID(+) NOT IN(12,35) 
            //                AND OE.ENTITY_ID = FT.KEY_ID AND FT.FLEX_TABLE_CODE = 'FRM117' AND CT.STATUS = 'N' AND DT.ITEM_ID(+) IN(10,14,15,19,20,25,32,34,37,41,49,51,52,53)
            //                AND HW.ITEM_ID(+) IN(12,35) AND PA.ADDRESSTYPE_CODE='PRI'
            //                AND SD.ITEM_DETAIL_ID NOT IN(SELECT ITEM_DETAIL_ID FROM ITEM_SERIAL_DTL WHERE CURRENT_LOCATION IN('WDWH','DISUWH'))
            //                AND OE.ENTITY_CODE IN (SELECT DISTINCT A.ENTITY_CODE  FROM OPERATIONAL_ENTITY_TBL A,APP_USER AU,USER_ENTITIES UE WHERE AU.USER_ID=UE.USER_ID AND UE.ENTITY_ID=A.ENTITY_ID AND UE.IS_DELETED = 0 AND AU.USER_NAME= '"+Convert.ToString(Session["user"]) +"'  AND A.STATUS='A')  "+
            //              @" AND CT.CUSTOMER_NBR NOT IN (SELECT CUSTOMER_NBR FROM GTPL_NONRENEWAL_CUST) ) A
            //                WHERE TRUNC(INACTIVE_DATE) BETWEEN TRUNC(SYSDATE) - (TO_NUMBER(TO_CHAR(SYSDATE,'DD')) - 1) AND TRUNC(SYSDATE)-3";

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


    protected void rbt_Nonrenewal_cust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbt_Nonrenewal_cust.SelectedValue == "View")
        {
            pnl_upload.Visible = false;

            View_data.Visible = true;
            
           // grid_data1.Visible = false;
        }
        if (rbt_Nonrenewal_cust.SelectedValue == "Upload")
        {
            pnl_upload.Visible = true;

            View_data.Visible = false;

            grid_data.Visible = false;

            btn_Export.Visible = false;
        }
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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind_grid();
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        grid_data.ExportSettings.ExportOnlyData = true;
        grid_data.ExportSettings.IgnorePaging = true;
        grid_data.ExportSettings.OpenInNewWindow = true;
        grid_data.DataSource = Session["grid"];
        grid_data.Rebind();
        // grid_data.ExportSettings.FileName = report_Entity.SelectedItem.Text + "_" + grid_data.MasterTableView.Caption.ToString();
        grid_data.MasterTableView.ExportToExcel();
    }


    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            //if (file_upload.HasFile)
            //{
            if (file_upload.PostedFile.FileName == string.Empty)
            {
                return;
            }
            else
            {
                //save the file 
                //restrict user to upload other file extenstion

                string[] FileExt = file_upload.PostedFile.FileName.Split('.');

                string FileEx = FileExt[FileExt.Length - 1];

                if (FileEx.ToLower() == "csv")
                {
                    // ok
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only .csv File Allowed')", true);
                    return;
                }

            }

            //create object for CSVReader and pass the stream

            CSVReader reader = new CSVReader(file_upload.PostedFile.InputStream);

            //get the header

            string[] headers = reader.GetCSVLine();

            DataTable dt = new DataTable();

            //add headers

            foreach (string strHeader in headers)
                dt.Columns.Add(strHeader);

            string[] data;

            while ((data = reader.GetCSVLine()) != null)
                dt.Rows.Add(data);

            for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            {
                // QRY WITH USER RIGHTS
                // string StrEntity = "SELECT DISTINCT A.ENTITY_CODE ,A.ENTITY_ID FROM OPERATIONAL_ENTITY_TBL A , APP_USER AU,USER_ENTITIES UE WHERE AU.USER_ID = UE.USER_ID AND UE.ENTITY_ID = A.ENTITY_ID AND AU.USER_NAME='" + Convert.ToString(Session["user"]) + "'  AND A.ENTITY_CODE ='" + Convert.ToString(dt.Rows[i1]["ENTITY_CODE"]) + "'";

                // BYPASS QRY
                string StrEntity = "SELECT * FROM GTPL_NONRENEWAL_REASON WHERE REASON_NAME ='" + Convert.ToString(dt.Rows[i1]["REASON_NAME"]) + "'";

                DataSet dsEntityID = DataFunction.GetDATA(StrEntity);
                if (dsEntityID.Tables[0].Rows.Count > 0)
                {
                    Insert_Cust(dt.Rows[i1]);
                    this.Master.SuccessMessage("Data Uploaded Sucessfully...");
                }
                else
                {

                    lbl_error.Text = "Not valid Reason !!!";
                    return;
                }
                
            }

        
        }
        catch (Exception ex)
        {
            lblupload_error.Text = ex.Message.ToString();

        }
    }

    public void Insert_Cust(DataRow dr)
    {
        OracleTransaction trans = null;
        try
        {
            string struserid = "SELECT * FROM APP_USER WHERE STATUS = 'A'  AND USER_NAME = '" + Convert.ToString(Session["user"]) + "' ";
            DataSet dsuserid = DataFunction.GetDATA(struserid);

            string userid_id = Convert.ToString(dsuserid.Tables[0].Rows[0]["USER_ID"]);

            string str = "SELECT * FROM ENTITY_CUSTOMER_SEARCH WHERE ACC_NO = '"+Convert.ToString(dr["CUSTOMER_NBR"])+"' ";
            DataSet ds = DataFunction.GetDATA(str);

            string strreason = "SELECT * FROM GTPL_NONRENEWAL_REASON WHERE REASON_NAME = '" + Convert.ToString(dr["REASON_NAME"]) + "' ";
            DataSet dsReason = DataFunction.GetDATA(strreason);

            //string Entity_id = Convert.ToString(dsEntityid.Tables[0].Rows[0]["ENTITY_ID"]);

            con_orl.Open();
            trans = con_orl.BeginTransaction();

            string insqry = "insert into GTPL_NONRENEWAL_CUST (TBL_DATE,CUSTOMER_NBR,CUSTOMER_NAME,MOBILE,ADDRESS,ENTITY_ID,ENTITY_CODE,ENTITY_NAME,REASON_CODE,CREATED_BY)" +
                           " VALUES (:TBL_DATE,:CUSTOMER_NBR,:CUSTOMER_NAME,:MOBILE,:ADDRESS,:ENTITY_ID,:ENTITY_CODE,:ENTITY_NAME,:REASON_CODE,:CREATED_BY)";


            OracleCommand cmd = new OracleCommand(insqry, con_orl);
            cmd.Transaction = trans;

            cmd.Parameters.Add(new OracleParameter(":TBL_DATE", System.DateTime.Now));
            cmd.Parameters.Add(new OracleParameter(":CUSTOMER_NBR", Convert.ToString(dr["CUSTOMER_NBR"])));
            cmd.Parameters.Add(new OracleParameter(":CUSTOMER_NAME", Convert.ToString(ds.Tables[0].Rows[0]["NAME"])));
            cmd.Parameters.Add(new OracleParameter(":MOBILE", Convert.ToString(ds.Tables[0].Rows[0]["MOBILE_PHONE"])));
            cmd.Parameters.Add(new OracleParameter(":ADDRESS", Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS1"])));
            cmd.Parameters.Add(new OracleParameter(":ENTITY_ID", Convert.ToString(ds.Tables[0].Rows[0]["ENTITY_ID"])));
            cmd.Parameters.Add(new OracleParameter(":ENTITY_CODE", Convert.ToString(ds.Tables[0].Rows[0]["ENTITY_CODE"])));
            cmd.Parameters.Add(new OracleParameter(":ENTITY_NAME", Convert.ToString(ds.Tables[0].Rows[0]["NAME"])));
            cmd.Parameters.Add(new OracleParameter(":REASON_CODE", Convert.ToString(dsReason.Tables[0].Rows[0]["REASON_CODE"])));
            cmd.Parameters.Add(new OracleParameter(":CREATED_BY", userid_id));


            cmd.ExecuteNonQuery();

            trans.Commit();


            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insert Successfully !!')", true);
            this.Master.SuccessMessage("Insert Successfully !!");
        }
        catch (Exception ex)
        {
            trans.Rollback();
        }
        finally
        {
            con_orl.Close();
            trans.Dispose();
        }
    }

    protected void LNK_DOWNLOAD_Click(object sender, EventArgs e)
    {
        string fileName = "Nonrenewal_Customer.csv";
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.TransmitFile(Server.MapPath("~/App_Data/" + fileName));
        Response.End();
    }

    protected void LNK_REASON_Click(object sender, EventArgs e)
    {
        string fileName = "REASON_Master_.xlsx";
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.TransmitFile(Server.MapPath("~/App_Data/" + fileName));
        Response.End();
    }
}