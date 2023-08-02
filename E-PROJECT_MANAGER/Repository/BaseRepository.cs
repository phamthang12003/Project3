using E_PROJECT_MANAGER.Data;
using E_PROJECT_MANAGER.DataTransferObject;
using E_PROJECT_MANAGER.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace E_PROJECT_MANAGER.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public DataTableReposneDTO<T> GetAll();
        public DataTableReposneDTO<T> Filter(Expression<Func<T, bool>> filter, string columnName = "Id",
                                                            bool columnAsc = false,
                                                            int start = 1,
                                                            int length = 10,
                                                            int draw = 0,
                                                            Expression<Func<T, object>>[] includeProperties = null);

        public T GetById(int id);

        public ViewDTO<T> Insert(T entity);
        public ViewDTO<T> Update(T entity);
        public ViewDTO<T> Delelte(T entity);

        public ViewDTO<T> Save(int id, T entity);

    }
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {

        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public ViewDTO<T> Delelte(T entity)
        {
            var result = new ViewDTO<T>();
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
                result.StatusCode = 200;
                result.Message = "Delete success!";
            }
            return result;
        }

        public DataTableReposneDTO<T> Filter(Expression<Func<T, bool>> filter, string columnName = "Id", bool columnAsc = false, int start = 1, int length = 10, int draw = 0, Expression<Func<T, object>>[] includeProperties = null)
        {
            var result = new DataTableReposneDTO<T>();

            var dataRows = _dbSet.AsQueryable();

            //Tim kiem
            if (filter != null)
            {
                dataRows = dataRows.Where(filter);
            }
            //Include
            if(includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    dataRows = dataRows.Include(includeProperty);
                }
            }
            

            //Sap xep
            var propertyInfo = typeof(T).GetProperty(columnName);
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
            if (columnAsc == false)
            {
                dataRows = dataRows.OrderByDescending(lambda);
            }
            else
            {
                dataRows = dataRows.OrderBy(lambda);
            }

            //Phan trang
            var totalRows = dataRows.Count();
            dataRows = dataRows.Skip(start).Take(length);

            result.data = dataRows.ToList();
            result.recordsTotal = totalRows;
            result.recordsFiltered = totalRows;
            result.draw = draw;

            return result;
        }

        public DataTableReposneDTO<T> GetAll()
        {
            var result = new DataTableReposneDTO<T>();
            result.data = _dbSet.AsQueryable().ToList();
            return result;
        }

        public T GetById(int id)
        {
            if (id > 0)
            {
                var result = _dbSet.Find(id);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public ViewDTO<T> Insert(T entity)
        {
            var result = new ViewDTO<T>();
            if (entity != null)
            {
                if (entity.Id <= 0)
                {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
                    result.Message = "Bạn đã thêm mới thành công!";
                    result.StatusCode = 200;
                    result.Data.Add(entity);
                }
            }
            return result;
        }

        public ViewDTO<T> Save(int id, T entity)
        {
            ViewDTO<T> result = new ViewDTO<T>();
            if (id <= 0)
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                result.StatusCode = 200;
                result.Message = "Thêm Mới Thành Công!";
            }
            if (id > 0)
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
                result.StatusCode = 200;
                result.Message = "Cập nhật thành công!";

            }

            return result;
        }


        public ViewDTO<T> Update(T entity)
        {
            var result = new ViewDTO<T>();
            if (entity != null)
            {
                if (entity.Id > 0)
                {
                    _dbSet.Update(entity);
                    _context.SaveChanges();
                    result.Message = "Bạn đã chỉnh sửa thanh cong!";
                    result.StatusCode = 200;
                    result.Data.Add(entity);
                }
            }
            return result;
        }
    }
}
