using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProtos;
using static MyProtos.GrpcCustomer;

namespace SE1701Client.Controllers
{
    public class CustomerController : Controller
    {
        
        public IActionResult Index()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5294");
            var client = new GrpcCustomerClient(channel);
            MyProtos.CustomerList data = client.GetAll(new MyProtos.Empty());
            ViewBag.Data = data;
            return View();
        }

        public IActionResult GetCustomer(string id){
            using var channel = GrpcChannel.ForAddress("http://localhost:5294");
            var client = new GrpcCustomerClient(channel);
            MyProtos.Customer cus = client.GetCustomer(new MyProtos.IDRequest(){Id = id});
            ViewBag.Customer = cus;
            return View();
        }
    }
}