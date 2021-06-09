using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServer.BLL;
using TropicalServerApp.Models;

namespace TropicalServerApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product(string category)
        {
            ReportsBLL rpBLL = new ReportsBLL();
            DataSet ds = rpBLL.getAllCategories_BLL();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            List<string> categories = new List<string>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                categories.Add(dt.Rows[i]["ItemTypeDescription"].ToString());
            }
            ViewBag.Categories = categories;

            if (category == null)
            {
                category = categories[0];
            }
            ds = rpBLL.GetProductByProductCategory_BLL(category);
            if (ds.Tables[0].Rows.Count != 0)
            {
                dt = ds.Tables[0];
                List<Product> products = new List<Product>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Product product = new Product();
                    product.itemNumber = Int32.Parse(dt.Rows[i]["ItemNumber"].ToString());
                    product.itemDescription = dt.Rows[i]["ItemDescription"].ToString();
                    product.prePrice = dt.Rows[i]["PrePrice"].ToString();
                    string weight = dt.Rows[i]["itemWeights"].ToString();
                    if (weight != null && weight != "")
                    {
                        product.itemWeights = Convert.ToDouble(weight);
                    }
                    products.Add(product);
                }
                ViewBag.Products = products;
            }
            return View();
        }

        public ActionResult Orders(string parameters)
        {
            ViewBag.param = parameters;
            ReportsBLL rpBLL = new ReportsBLL();
            DataSet ds = rpBLL.getAllOrders_BLL();
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                List<Order> orders = new List<Order>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Order order = new Order();
                    order.orderTracking = dt.Rows[i]["OrderTrackingNumber"].ToString();
                    order.OrderDate = dt.Rows[i]["OrderDate"].ToString();
                    order.OrderCustomerNumber = Int32.Parse(dt.Rows[i]["OrderCustomerNumber"].ToString());
                    order.CustomerName = dt.Rows[i]["CustName"].ToString();
                    order.CustomerAddress = dt.Rows[i]["CustOfficeAddress1"].ToString();
                    order.CustomerRouteNumber = Int32.Parse(dt.Rows[i]["CustRouteNumber"].ToString());
                    orders.Add(order);
                }
                ViewBag.Orders = orders;
            }
            return View();
        }
    }
}