using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using soha_f6269.App_Code;
using System.Data.SqlClient;

namespace soha_f6269.Demo
{
    public partial class myServiceControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populateddlCountry();
               
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string myName = txtfName.Text;
            //lblOutput.Text = myName;
            string strfName = txtfName.Text;
            string strlName = txtlNmae.Text;
            string strCell = txtCell.Text;
            string strEmail = txtEmail.Text;
            string ddlCoutryId = ddlCountry.SelectedItem.Value;
            CRUD myCrud = new CRUD();
            string mySql = @"insert contact(fName,lName,cell,email,countryId)
                              values(@fName,@lName,@cell,@email,@countryId)";
             Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@fName", strfName);
            myPara.Add("@lName", strlName);
            myPara.Add("@cell", strCell);
            myPara.Add("@email",strEmail);
            myPara.Add("@countryId",ddlCoutryId);
           int rtn= myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn>=1)
            {
                lblOutput.Text = "operation seuccessfull*_*";
            }
            else
            {
                lblOutput.Text = "operation failed *-*";
            }
            populateGvContact();
        }

        protected void populateddlCountry()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select countryId, country from country";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "countryId";
            ddlCountry.DataSource = dr;
            ddlCountry.DataBind();
        }
        
        protected void populateGvContact()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select contactId, fName, lName, cell, email, country
                     from contact c inner join country co On c.countryId = co.countryId";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvContact.DataSource = dr;
            gvContact.DataBind();
        }

        protected void btnShowContactInfo_Click(object sender, EventArgs e)
        {
            populateGvContact();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"delete from contact where contactId = @contactId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            string strContactId = txtContactId.Text;
            int intContactId = int.Parse(strContactId);
            myPara.Add("@contactId", intContactId);
            txtContactId.Text = "";
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "operation seuccessfull*_*";
            }
            else
            {
                lblOutput.Text = "operation failed *-*";
            }
            populateGvContact();
        }

        protected void btnUbdate_Click(object sender, EventArgs e)
        {
            string strfName = txtfName.Text;
            string strlName = txtlNmae.Text;
            string strCell = txtCell.Text;
            string strEmail = txtEmail.Text;
            string ddlCoutryId = ddlCountry.SelectedItem.Value;
            CRUD myCrud = new CRUD();
            string mySql = @"update
             set fName = @fName, lName = @lName, cell = @cell, email = @email, countryId = @countryId
             where contactId = @contactId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@fName", strfName);
            myPara.Add("@lName", strlName);
            myPara.Add("@cell", strCell);
            myPara.Add("@email", strEmail);
            myPara.Add("@countryId", ddlCoutryId);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "operation seuccessfull*_*";
            }
            else
            {
                lblOutput.Text = "operation failed *-*";
            }
            populateGvContact();
        }
    }
}