using Microsoft.AspNetCore.Mvc;
using SimpleWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace SimpleWebMVC.Controllers
{


    public class DivisionController : Controller

    {
        SqlConnection conn;
        public string connectionString =
            "Data Source=WINDOWS-PC;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False;" +
            "Initial Catalog=KantorSederhana;" +
            "User ID=WINDOWS-PC\\Santana;" +
            "Password=mateyize49;";
        public IActionResult Index()
        {
            string query = "SELECT * FROM Division ORDER BY id ASC;";
            conn = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            List<Division> Divisions = new List<Division>();
            try
            {
                conn.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Division division = new Division();
                            division.Id = Convert.ToInt32(sqlDataReader[0]);
                            division.DivisionName = sqlDataReader[1].ToString();
                            Divisions.Add(division);
                            Console.WriteLine("{0} {1}", sqlDataReader[0], sqlDataReader[1]);
                        }
                    }
                    //else
                    //{
                    //    //Console.WriteLine("Wrong password or username !!");
                    //}
                    sqlDataReader.Close();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            ViewBag.LDivision = Divisions;
            return View();
        }
        public IActionResult AddDivision(string id, string _)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@nameDivision";
                sqlParameter.Value = id;
                sqlCommand.Parameters.Add(sqlParameter);
                try
                {
                    sqlCommand.CommandText = "INSERT INTO Division " +
                        "VALUES (@nameDivision)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine($"Success membuat divisi {id} !!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Input tidak valid");

                }
            }
            return Ok();
        }
        public IActionResult DeleteDivision(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = id;
                sqlCommand.Parameters.Add(sqlParameter);
                try
                {
                    sqlCommand.CommandText = "DELETE FROM Division " +
                        "WHERE id=@id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Success Menghapus divisi !!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Input tidak valid");

                }
            }
            return Ok();
        }
        public IActionResult UpdateDivision(int id, string nameDivision)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = id;
                sqlCommand.Parameters.Add(sqlParameter);
                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter2.ParameterName = "@nameDivision";
                sqlParameter2.Value = nameDivision;
                sqlCommand.Parameters.Add(sqlParameter2);
                try
                {
                    sqlCommand.CommandText = "UPDATE Division " +
                        "SET name = @nameDivision WHERE id = @id;";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine($"Success mengubah divisi {nameDivision} !!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Input tidak valid");

                }
            }
            return Ok();
        }
    }
}
