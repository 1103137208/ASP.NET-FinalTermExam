using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertCustomer()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateCustomer(int CustomerID)
        {
            Services.CustomerService CustomerService = new Services.CustomerService();
            Models.Customer Customer = CustomerService.GetCustomerByID(CustomerID);
            ViewBag.Customer = Customer;
            return View();
        }

        public JsonResult DoInsertCustomer(Models.Customer Customer)
        {
            try
            {
                Services.CustomerService CustomerService = new Services.CustomerService();
                JsonResult result = this.Json(CustomerService.InsertCustomer(Customer), JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        public JsonResult Read(Models.CustomerSearch Arg)
        {
            try
            {
                Services.CustomerService CustomerService = new Services.CustomerService();
                JsonResult result = this.Json(CustomerService.GetCustomer(Arg), JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        public JsonResult GetContactTitle()
        {
            try
            {
                Services.CustomerService CustomerService = new Services.CustomerService();
                JsonResult result = this.Json(CustomerService.GetContactTitle(), JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        public JsonResult DeleteCustomer(string CustomerID)
        {
            try
            {
                Services.CustomerService CustomerService = new Services.CustomerService();
                int result = CustomerService.DeleteCustomerByID(CustomerID);
                if (result >= 1)
                {
                    return this.Json(true);
                }
                else
                {
                    return this.Json(false);
                }


            }
            catch (Exception)
            {

                return this.Json(false);
            }
        }

    }
}