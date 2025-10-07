using CRUD_ADO_MVC_Demo.Models;
namespace CRUD_ADO_MVC_Demo.Services
{
    public interface IRepository
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee emp); 
        void Update(Employee emp);  
        void Delete(int id);    

    }
}
