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
    public partial class CustomerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateCustomerRequest();
        }


        public void UpdateCustomerRequest()
        {
           SqlConnection conn = new SqlConnection(@"data source = LAPTOP-7ILGU10M; integrated security = true; database = PestExDB");
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string sqlsel = "SELECT Help.CaseNo, Customers.CustomerID, Customers.FirstName, Customers.LastName, Pests.PestID, Pests.Name, Help.Date " +
                            "FROM Help " +
                            "INNER JOIN Customers ON Help.CustomerID = Customers.CustomerID " +
                            "INNER JOIN Pests ON Help.PestID = Pests.PestID";


            /* "SELECT Request.RequestID, Customer.CustomerID, Customer.Firstname, Customer.Lastname, Pest.PestID, Pest.Name, Request.Date " +
                         "FROM Request " +
                         "INNER JOIN Customer ON Request.CustomerID = Customer.CustomerID " +
                         "INNER JOIN Pest ON Request.PestID = Pest.PestID";*/




            /*string sqlsel = "SELECT RequestID, Firstname, Lastname, Name, Date " +
                            "FROM Request " +
                            "INNER JOIN Customer ON Request.CustomerID = Customer.CustomerID " +
                            "INNER JOIN Pest ON Request.PestID = Pest.PestID";*/

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlsel, conn);

                rdr = cmd.ExecuteReader();

                GridViewRequest.DataSource = rdr;

                GridViewRequest.DataBind();

                rdr.Close();

            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}