using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CRUD_TEST
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ABC"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dispaly();
            }
        }
        public void Clear()
        {
            txtname.Text = "";
            txtage.Text = "";
            txtsalary.Text = "";
        }

        public void Dispaly()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from employee", con);
            SqlDataAdapter dd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dd.Fill(dt);
            con.Close();
            empgv.DataSource = dt;
            empgv.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into employee(name,age,salary)values('" + txtname.Text + "','" + txtage.Text + "','" + txtsalary.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Dispaly();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update employee set name='" + txtname.Text + "', age='" + txtage.Text + "', salary='" + txtsalary.Text + "' where id='"+ ViewState["IDD"] + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                btnsave.Text = "Save";
                Dispaly();
            }
            Clear();
        }

        protected void empgv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "A")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from employee where id='" + e.CommandArgument + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Dispaly();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from employee where id='"+e.CommandArgument+"'", con);
                SqlDataAdapter dd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dd.Fill(dt);
                con.Close();
                txtname.Text = dt.Rows[0]["name"].ToString();
                txtage.Text = dt.Rows[0]["age"].ToString();
                txtsalary.Text = dt.Rows[0]["salary"].ToString();
                ViewState["IDD"] = e.CommandArgument;
                btnsave.Text = "Update";
            }
        }
    }
}