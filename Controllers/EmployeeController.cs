using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private SqlConnection Sqlconn;
        private SqlDataAdapter da;
        public SqlDataAdapter DatabaseConnection(string Query)
        {
            Sqlconn = new SqlConnection("data source=DESKTOP-B8QR1TK\\MSSQL;Initial Catalog=EmployeeDb;user id=sa;password=MSSQL");

            da = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(Query, Sqlconn)
            };

            return da;
        }


        // GET api/<controller>
        public IEnumerable<Department> Get()
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("DeptID");
            //dt.Columns.Add("DeptName");

            //dt.Rows.Add(1, "IT");
            //dt.Rows.Add(2, "CSE");

            SqlDataAdapter sadp = DatabaseConnection("select * from EmployeeTable");
            sadp.Fill(dt);

            List<Department> dept = new List<Department>(dt.Rows.Count);

            foreach(DataRow record in dt.Rows)
            {
                dept.Add(new ReadDepartment(record));
            }

            return dept;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Department Get(int id)
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("DeptID");
            //dt.Columns.Add("DeptName");

            //dt.Rows.Add(1, "IT");
            //dt.Rows.Add(2, "CSE");

            String query = "select * from EmployeeTable where EID = " + id;

            SqlDataAdapter sadp = DatabaseConnection(query);
            sadp.Fill(dt);

            Department dept = new ReadDepartment(dt.Rows[0]);

            return dept;
        }

        // POST api/<controller>
        public Boolean Post([FromBody]Department value)
        {
            Sqlconn = new SqlConnection("data source=DESKTOP-B8QR1TK\\MSSQL;Initial Catalog=EmployeeDb;user id=sa;password=MSSQL");
            var query = "insert into EmployeeTable(EID,NAME) values(@id,@name)";

            SqlCommand insertcommand = new SqlCommand(query, Sqlconn);

            insertcommand.Parameters.AddWithValue("@id", value.DeptId);
            insertcommand.Parameters.AddWithValue("@name", value.DeptName);

            Sqlconn.Open();

            int result = insertcommand.ExecuteNonQuery();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        // PUT api/<controller>/5
        public Boolean Put(int id, [FromBody]Department value)
        {
            Sqlconn = new SqlConnection("data source=DESKTOP-B8QR1TK\\MSSQL;Initial Catalog=EmployeeDb;user id=sa;password=MSSQL");

            var query = "UPDATE EmployeeTable set name = @name where EID ="+id;

            SqlCommand insertcommand = new SqlCommand(query, Sqlconn);

            insertcommand.Parameters.AddWithValue("@name", value.DeptName);

            Sqlconn.Open();

            int result = insertcommand.ExecuteNonQuery();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<controller>/5
        public Boolean Delete(int id)
        {
            Sqlconn = new SqlConnection("data source=DESKTOP-B8QR1TK\\MSSQL;Initial Catalog=EmployeeDb;user id=sa;password=MSSQL");

            var query = "Delete from EmployeeTable where EID =" + id;

            SqlCommand insertcommand = new SqlCommand(query, Sqlconn);

            Sqlconn.Open();

            int result = insertcommand.ExecuteNonQuery();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}