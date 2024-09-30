using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MyProtos;
using SE1701.Models;
using static MyProtos.GrpcCustomer;

namespace SE1701.Services
{
    public class CustomerService: GrpcCustomerBase 
    {
        public readonly AppDBContext _db;

        public CustomerService(AppDBContext db){
            this._db = db;
        }

        public override Task<CustomerList> GetAll(Empty request, ServerCallContext context)
        {
            //Customer list in MyProtos has a variable to store all Customer in db
            CustomerList response = new CustomerList();
            //cuslist => use linq to get data from database
            //after get data from db => map to MyProtos.Customer
            //return must be return Customer in protos
            //obj is the object of the model => map to customer in myprotos
            var cusList = from obj in this._db.Customers select new MyProtos.Customer(){
                Id = obj.Id,
                Name = obj.Name,
                Address = obj.Address
            };

            response.Customer.AddRange(cusList);
            return Task.FromResult(response);
        }

        public override Task<MyProtos.Customer> GetCustomer(IDRequest request, ServerCallContext context)
        {
            var obj = this._db.Customers.Find(request.Id);
            if (obj != null){
                MyProtos.Customer cus = new MyProtos.Customer(){
                Id = obj.Id,
                Name = obj.Name,
                Address = obj.Address
            };
            return Task.FromResult(cus);
            } else {
                throw new RpcException(new Status(StatusCode.NotFound,"Not found obj"));
            }
        }

        public override Task<IDResponse> AddCustomer(MyProtos.Customer request, ServerCallContext context)
        {
            
            return base.AddCustomer(request, context);
        }
    }
}