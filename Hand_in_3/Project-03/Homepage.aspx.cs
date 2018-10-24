using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace Project_03
{
    public partial class homepage : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"data source = LAPTOP-7ILGU10M; integrated security = true; database = PestExDB");
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string sqlsel = "SELECT * FROM Pests";

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowMyData();
        }
        public void ShowMyData()
        {
            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlsel, conn);

                rdr = cmd.ExecuteReader();
                RepeaterPestList.DataSource = rdr;
                RepeaterPestList.DataBind();
            }
            catch (Exception ex)
            {
                //LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

        }

        protected void ButtonSignup_Click(object sender, EventArgs e)
        {
            Server.Transfer("Signup.aspx");
        }
    }
}