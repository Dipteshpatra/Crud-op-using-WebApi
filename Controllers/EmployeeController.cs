using Microsoft.AspNetCore.Mvc;
using Demo_Core_Api.Models;
using System.Data.SqlClient;
using System.Net.WebSockets;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo_Core_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        SqlConnection con;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
             con= new SqlConnection(_configuration.GetConnectionString("Company"));

        }

        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Employee";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    employees.Add(new Employee
                    {
                        EmpId = Convert.ToInt32(sdr["EmpId"]),
                        EmpName = Convert.ToString(sdr["EmpName"]),
                        EmpAge = Convert.ToInt32(sdr["EmpAge"])
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return new JsonResult(employees);

        }

        [HttpPost]
        public Boolean AddEmployee(Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employee(EmpName, EmpAge) values('" + employee.EmpName + "'," + employee.EmpAge + ")";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPut]
        public Boolean UpdateEmployee(int EmpId, Employee employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Employee Set Name='" + employee.EmpName + "', EmpAge=" + employee.EmpAge + "Where(EmpId=" + EmpId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpDelete]
        public Boolean DeleteEmployee(int EmpId)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Employee Where(EmpID=" + EmpId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}