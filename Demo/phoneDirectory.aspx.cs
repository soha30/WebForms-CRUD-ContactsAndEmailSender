using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using soha_f6269.App_Code;

namespace soha_f6269.Demo
{
    public partial class phoneDirectory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblOutput.Text = "Ahmed";
            populdateGvContact();
            populateDdlFname();
        }
        protected void populdateGvContact()
        {
            CRUD myCrud = new CRUD() ;
            string mySql = @"select* from v_contactDirectory";
            
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvContact.DataSource = dr;
            gvContact.DataBind();
           
        }
        protected void populateDdlFname()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select contactId , fName from Contact";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlfName.DataTextField = "fName";
            ddlfName.DataValueField = "contactId";
            ddlfName.DataSource = dr;
            ddlfName.DataBind();
        }
    }
}