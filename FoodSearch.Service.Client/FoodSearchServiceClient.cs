using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Service.Contracts;

namespace FoodSearch.Service.Client
{
    public class FoodSearchServiceClient : RestServiceClientBase
    {
        public FoodSearchServiceClient()
        {
            apiPath += "StudentService/";
        }

        public Task<Student> GetStudent(int id)
        {
            return RestGet<Student>(id.ToString());
        }

        public Task<int> AddStudent(string firstName, string lastName, DateTime birthDate, string group)
        {

        }
    }
}
