using System.Data.SqlClient;

namespace DatabaseConnection
{
    class Connection{
        private SqlConnection myConnection;
        private const username = "username";
        private const password = "password";
        private const serverurl = "localhost";
        private const database = "database";

        public void Connection(){
            myConnection = new SqlConnection("Server="  + serverurl + ";
                                              Database="+ database  + ";
                                              User Id=" + username  + ";
                                              Password="+ password  + ";");
            try{
                myConnection.Open();
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
            }
        }

        
    }
}