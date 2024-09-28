﻿using LinkDev.IKEA.DAL.Models.Departments;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositotries._Generic;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.IKEA.DAL.Persistance.Repositotries.Departments
{
    // Primary Constructor
    // Asking CLR for object of ApplicationDbContext Implicitly
    // Will be avaailble upon the object
    public class DepartmentRepository : GenericRepositories<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }

        public Department? Get(int id)
        {
           var department = _dbContext.Departments.Find(id);
           return department;
        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return _dbContext.Departments;
        }

        public void Add(Department entity)
        {
           _dbContext.Departments.Add(entity);
        }
        public void Update(Department entity)
        {
           _dbContext.Departments.Update(entity);
        }

        public void Delete(int id)
        {
            var department = _dbContext.Departments.Find(id);
            if (department is { })
                _dbContext.Departments.Remove(department);

        }



    }
   
}
