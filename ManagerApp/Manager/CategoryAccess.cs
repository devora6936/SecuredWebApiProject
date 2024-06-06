using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class CategoryAccess
    {
        public int insertCategory(string connectionString)
        {
            String name;

            Console.WriteLine("insert category name");
            name = Console.ReadLine();

            String query = "INSERT INTO Categories(CATEGORY_NAME)" + "VALUES(@name)";
           
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query,cn)) 
            {
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 20).Value = name;


                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                return rowsAffected;
            }

        }

        internal void readCategory(String connectionString)
        {
            String qeryString = "select * from Categories";
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
            using (SqlCommand command = new SqlCommand(qeryString, connection))

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("\t{0}\t{1}",
                                reader[0], reader[1]);
                        }
                        reader.Close();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                Console.ReadLine();
                
            }
        }

        public void fillCategories(String connectionString)
        {
            String flag="y";

            while (flag == "y")
            {
                insertCategory(connectionString);
                Console.WriteLine("whould you like to continue? y/n");
                flag = Console.ReadLine();
            }
        }
    }
}
