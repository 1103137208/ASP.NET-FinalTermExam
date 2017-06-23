using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Dao
{
    public class CustomerDao
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public DataTable GetCustomer(Models.CustomerSearch arg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [CustomerID]
      ,[CompanyName]
      ,[ContactName]
      ,[ContactTitle]+'-'+B.CodeVal AS ContactTitle
      ,[CreationDate]
      ,[Address]
      ,[City]
      ,[Region]
      ,[PostalCode]
      ,[Country]
      ,[Phone]
      ,[Fax]
  FROM [Sales].[Customers] AS A JOIN dbo.CodeTable AS B ON A.ContactTitle = B.CodeId AND B.CodeType='TITLE'
  WHERE(A.CompanyName Like @CompanyName Or @CompanyName='') And
                          (A.CustomerID=@CustomerID Or @CustomerID='') And
                          (A.ContactName Like @ContactName Or @ContactName='') And
                          (A.ContactTitle = @ContactTitle Or @ContactTitle='')";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", arg.CustomerID == null ? string.Empty : arg.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", arg.CompanyName == null ? string.Empty : '%' + arg.CompanyName + '%'));
                cmd.Parameters.Add(new SqlParameter("@ContactName", arg.ContactName == null ? string.Empty : '%' + arg.ContactName + '%'));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", arg.ContactTitle == null ? string.Empty : arg.ContactTitle));
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }


        public DataTable GetContactTitle()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [CodeId]
                              ,[CodeVal]
                          FROM [dbo].[CodeTable]
                          WHERE CodeType='TITLE'";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public int DeleteCustomerByID(string CustomerID)
        { 
            try
            {
                int result;
                string sql = @"DELETE FROM [Sales].[Customers]
                            WHERE CustomerID=@CustomerID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertCustomer(Models.Customer Customer)
        {
            string sql = @"INSERT INTO [Sales].[Customers]
                           ([CompanyName]
                           ,[ContactName]
                           ,[ContactTitle]
                           ,[CreationDate]
                           ,[Address]
                           ,[City]
                           ,[Region]
                           ,[PostalCode]
                           ,[Country]
                           ,[Phone]
                           ,[Fax])
                     VALUES
                           (@CompanyName,
                            @ContactName,
                            @ContactTitle,
                            @CreationDate,
                            @Address,
                            @City,
                            @Region,
                            @PostalCode,
                            @Country,
                            @Phone,
                            @Fax)
                            Select SCOPE_IDENTITY()";
            int CustomerID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CompanyName", Customer.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", Customer.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", Customer.ContactTitle));
                cmd.Parameters.Add(new SqlParameter("@CreationDate", Convert.ToDateTime(Customer.CreationDate)));
                cmd.Parameters.Add(new SqlParameter("@Address", Customer.Address));
                cmd.Parameters.Add(new SqlParameter("@City", Customer.City ));
                cmd.Parameters.Add(new SqlParameter("@Region", Customer.Region == null ? string.Empty : Customer.Region));
                cmd.Parameters.Add(new SqlParameter("@PostalCode", Customer.PostalCode == null ? string.Empty : Customer.PostalCode));
                cmd.Parameters.Add(new SqlParameter("@Country", Customer.Country));
                cmd.Parameters.Add(new SqlParameter("@Phone", Customer.Phone));
                cmd.Parameters.Add(new SqlParameter("@Fax", Customer.Fax == null ? string.Empty : Customer.Fax));

                CustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return CustomerID;
        }

        public DataTable GetCustomerByID(int CustomerID)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT [CustomerID]
                          ,[CompanyName]
                          ,[ContactName]
                          ,[ContactTitle]+'-'+B.CodeVal AS ContactTitle
                          ,[CreationDate]
                          ,[Address]
                          ,[City]
                          ,[Region]
                          ,[PostalCode]
                          ,[Country]
                          ,[Phone]
                          ,[Fax]
                      FROM [Sales].[Customers] AS A JOIN dbo.CodeTable AS B ON A.ContactTitle = B.CodeId AND B.CodeType='TITLE'
                      WHERE A.CustomerID=@CustomerID";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }


    }
}