﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace Project_03
{
    public partial class CustomerUpdate : System.Web.UI.Page
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


        // Inserts the selected data from GridViewRequest into fields
        protected void GridViewRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxPestID.Text = GridViewRequest.SelectedRow.Cells[5].Text;
            TextBoxDate.Text = GridViewRequest.SelectedRow.Cells[7].Text;
            LabelMessage.Text = "You chose request " + GridViewRequest.SelectedRow.Cells[1].Text;
        }


        // Update
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = LAPTOP-7ILGU10M; integrated security = true; database = PestExDB");
            SqlCommand cmd = null;
            string sqlsel = "update Help set PestID = @PestID, Date = @Date where CaseNo = @CaseNo";

            try
            {
                conn.Open();

                cmd = new SqlCommand(sqlsel, conn);

                cmd.Parameters.Add("@CaseNo", SqlDbType.Int);
                cmd.Parameters.Add("@PestID", SqlDbType.Int);
                cmd.Parameters.Add("@Date", SqlDbType.DateTime);

                cmd.Parameters["@CaseNo"].Value = Convert.ToInt32(GridViewRequest.SelectedRow.Cells[1].Text);
                cmd.Parameters["@PestID"].Value = TextBoxPestID.Text;
                cmd.Parameters["@Date"].Value = TextBoxDate.Text;

                cmd.ExecuteNonQuery();
                UpdateCustomerRequest();

                LabelMessage.Text = "Request updated";
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