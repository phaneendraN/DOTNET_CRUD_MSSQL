using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Department
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
    }

    public class ReadDepartment : Department
    {
        public ReadDepartment(DataRow dr)
        {
            DeptId = Convert.ToInt32(dr["EID"]);
            DeptName = dr["name"].ToString();
        }
        
    }
}