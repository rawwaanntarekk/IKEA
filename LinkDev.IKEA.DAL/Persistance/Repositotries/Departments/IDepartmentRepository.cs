using LinkDev.IKEA.DAL.Models.Departments;


namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Departments
{
    public interface IDepartmentRepository
    {
        public Department? Get(int id);
        public IEnumerable<Department> GetAll(bool withAsNoTracking = true);
        public IQueryable<Department> GetAllAsIQueryable();
        public void Add(Department entity);
        public void Update(Department entity);
        public void Delete(int id);

    }
}
