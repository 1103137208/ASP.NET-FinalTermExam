using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CustomerService
    {
        Dao.CustomerDao CustomerDao = new Dao.CustomerDao();

        public List<Models.Customer> GetCustomer(Models.CustomerSearch arg)
        {
            DataTable dt = CustomerDao.GetCustomer(arg);
            return MapCustomer(dt); 
        }

        public List<Models.ContactTitle> GetContactTitle()
        {
            DataTable dt = CustomerDao.GetContactTitle();
            return MapComboBox(dt);
        }

        public int DeleteCustomerByID(string CustomerID)
        {
            int result = CustomerDao.DeleteCustomerByID(CustomerID);
            return result;
        }

        
            public int InsertCustomer(Models.Customer Customer)
        {
            int result = CustomerDao.InsertCustomer(Customer);
            return result;
        }

        public Models.Customer GetCustomerByID(int CustomerID)
        {
            DataTable dt = CustomerDao.GetCustomerByID(CustomerID);
            return MapCustomerForUpdate(dt);
        }

        public List<Models.Customer> MapCustomer(DataTable dt)
        {
            List<Models.Customer> result = new List<Models.Customer>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Models.Customer()
                {
                    CustomerID = (int)row["CustomerID"],
                    CompanyName = row["CompanyName"].ToString(),
                    ContactName = row["ContactName"].ToString(),
                    ContactTitle = row["ContactTitle"].ToString(),
                    CreationDate = row["CreationDate"].ToString(),
                    Address = row["Address"].ToString(),
                    City = row["City"].ToString(),
                    Region = row["Region"].ToString(),
                    PostalCode = row["PostalCode"].ToString(),
                    Country = row["Country"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Fax = row["Fax"].ToString(),
                });

            }
            return result;
        }

        public List<Models.ContactTitle> MapComboBox(DataTable dt)
        {
            List<Models.ContactTitle> result = new List<Models.ContactTitle>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Models.ContactTitle()
                {
                    CodeID = row["CodeID"].ToString(),
                    CodeVal = row["CodeVal"].ToString()
                });

            }
            return result;
        }

        public Models.Customer MapCustomerForUpdate(DataTable dt)
        {
            Models.Customer result = new Models.Customer();
            foreach(DataRow row in dt.Rows)
            {
                result.CustomerID = (int)row["CustomerID"];
                result.CompanyName = row["CompanyName"].ToString();
                result.ContactName = row["ContactName"].ToString();
                result.ContactTitle= row["ContactTitle"].ToString();
                result.CreationDate = row["CreationDate"].ToString();
                result.Address= row["Address"].ToString();
                result.City = row["City"].ToString();
                result.Region = row["Region"].ToString();
                result.PostalCode = row["PostalCode"].ToString();
                result.Country = row["Country"].ToString();
                result.Phone = row["Phone"].ToString();
                result.Fax = row["Fax"].ToString();
            }
            return result;
        }
    }
}