﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace databaseproject
{
    public partial class StudentPart1d : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Proceed_Click(object sender, EventArgs e)
        {
            try
            {
                String sem = TextBox1.Text;
                Session["sem"] = sem;
                string connStr = WebConfigurationManager.ConnectionStrings["DBAdvising_System"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                int sid = (int)Session["user"];


                if (sem == "")
                {
                    Response.Write("You didn't enter semester code");
                }
                else
                {


                    SqlCommand procOptional = new SqlCommand("Exec Procedures_ViewOptionalCourse @StudentID,@current_semester_code", conn);
                    procOptional.Parameters.AddWithValue("@StudentID", sid);
                    procOptional.Parameters.AddWithValue("@current_semester_code", sem);

                    conn.Open();
                    SqlDataReader reader = procOptional.ExecuteReader();
                    while (reader.Read())
                    {
                        int course_id = reader.GetInt32(reader.GetOrdinal("course_id"));
                        String name = reader.GetString(reader.GetOrdinal("name"));

                        Label lll = new Label();
                        lll.Text = "course_id:  " + course_id + "     name:  " + name + "<br/>";
                        form1.Controls.Add(lll);
                    }
                    Response.Write("Action done successfully, if no data displayed then there is no data to be display");
                    conn.Close();





                }
            }
            catch (Exception ex)
            {
                Response.Write("No optional courses");
            }
        }

        protected void GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentHome.aspx");
        }
    }
}