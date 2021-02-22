using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supplier.Models;
namespace Supplier.Services.IServices
{
    public interface IService
    {
        void Add(object entity);
        IList<object> List();
        object Find(int id);


        void Update(object entity);
        void Delete(int id);
        List<object> Search(string term);
        void Delete(Category id);
    }
}
