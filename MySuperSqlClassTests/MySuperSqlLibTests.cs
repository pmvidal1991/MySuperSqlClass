using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySuperSqlClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySuperSqlClass.Tests
{
    [TestClass()]
    public class MySuperSqlLibTests
    {
        [TestMethod()]
        public void ExecuteSelectQueryTest()
        {
            string ConnectionString = "Server=LAPTOP-B06SI0T2\\SQLEXPRESS;Database=PhysioManager;Trusted_Connection=True;";
            MySuperSqlLib sqlLib = new MySuperSqlLib(ConnectionString);
            string query = "SELECT * FROM Users where username = @username AND password = @password";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("username", "Admin");
            parameters.Add("password", "Admin");
            try
            {
                List<LoginModel> user2 = sqlLib.ExecuteSelectQuery<LoginModel>(query, parameters);
                Assert.IsTrue(user2.Count > 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail();
            }
           
           

        }
        public partial class LoginModel {
            public int id { get; set; }
            public string Username { get; set; }
            public bool isSuccess { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public LoginModel()
            {

            }
        }
    }
}