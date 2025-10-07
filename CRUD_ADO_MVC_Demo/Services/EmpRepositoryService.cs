using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient; // Use this for .NET Core/.NET 5+/8 instead of System.Data.SqlClient
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using CRUD_ADO_MVC_Demo.Models;
using CRUD_ADO_MVC_Demo.Services;

public class EmpRepositoryService:IRepository

{
   // private readonly string _connectionString;

    private readonly string _connectionString= "Server=ENCDAPHYDLT0171;Database=myencora;Integrated Security=True;TrustServerCertificate=True;";
    //public EmpRepositoryService(IConfiguration configuration)
    //{
    //    _connectionString = configuration.GetConnectionString("DefaultConnection");
    //}

    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Department = reader["Department"].ToString(),
                    Salary = (double)Convert.ToDecimal(reader["salary"].ToString())
                });
            }
        }
        return employees;
    }

    public void Add(Employee employee)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Employee (Name,Department,Salary) VALUES (@Name,@Department, @Salary)", conn);
        //    cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public Employee GetById(int id)
    {
        Employee employee = null;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                employee = new Employee
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Department = reader["Department"].ToString(),
                    Salary = (double)Convert.ToDecimal(reader["Salary"])
                };
            }
        }
        return employee;
    }

    public void Update(Employee employee)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UPDATE Employee SET Name = @Name, Department = @Department, Salary = @Salary WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
