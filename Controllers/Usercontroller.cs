using DocumentManager.API.Models;
using DocumentManager.API.Helpers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace DocumentManager.API.Controllers
{
    public class Usercontroller : ApiController
    {
        [HttpPost]
        [Route("api/user/register")]
        public string Register(User user)
        {
            try
            {
                string conString =
                    ConfigurationManager.ConnectionStrings["DocumentDB"].ConnectionString;

                using (SqlConnection con =
                    new SqlConnection(conString))
                {
                    SqlCommand cmd =
                        new SqlCommand("usp_RegisterUser", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName",
                        user.FirstName);

                    cmd.Parameters.AddWithValue("@LastName",
                        user.LastName);

                    cmd.Parameters.AddWithValue("@Email",
                        user.Email);

                    cmd.Parameters.AddWithValue("@Password",
                        user.Password);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    // Send Email
                    try
                    {
                        EmailHelper.SendRegistrationEmail(
                            user.Email,
                            user.FirstName);
                    }
                    catch (Exception ex)
                    {
                        // In production, log the exception
                        // Registration should still succeed
                    }

                    con.Close();
                }

                return "User Registered Successfully";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }
    }
}