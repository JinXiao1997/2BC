using IUserDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserDAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        private DBContex DB { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        private IDbContextTransaction DBTransaction { get; set; }
        public BaseDAL(DBContex DB)
        {
            this.DB = DB;
        }
        /// <summary>
        /// 异步新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> AddEntityAsync(T entity, bool saved = true) 
        {
            await DB.Set<T>().AddAsync(entity);
            if (saved)
            {
                return await DB.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 异步批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddEntityListAsync(List<T> entityList, bool saved = true) 
        {

           await DB.Set<T>().AddRangeAsync(entityList);
            if (saved) {
                return await DB.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        /// 
        public async Task<int> DeleteEntity(T entity, bool saved = true)
        {
            DB.Set<T>().Attach(entity);
            DB.Set<T>().Remove(entity);
            if (saved)
            {
                return await DB.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public async Task<int> DeleteEntityListByCondition(Expression<Func<T, bool>> condition, bool saved = true) 
        {
            var entityList = DB.Set<T>().Where(condition).ToList();
            DB.Set<T>().RemoveRange(entityList);
            if (saved)
            {
                return await DB.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
    
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<T> UpdateEnetity(T entity, bool saved = true)
        {
            var EFEntity = DB.Update(entity).Entity;
            if (saved) {
               await  DB.SaveChangesAsync();
                return EFEntity;
            }
            else{
                return entity;
            }
 
        }
        /// <summary>
        /// 根据主键查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        public Task<T> FindAsync(string keyValue)
        {
            return   DB.Set<T>().FindAsync(keyValue);
        }
        /// <summary>
        /// 根据条件查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> condition)
        {
              return await DB.Set<T>().Where(condition).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> IEnumerable(Expression<Func<T, bool>> condition)
        {
            return await DB.Set<T>().Where(condition).ToArrayAsync();
        }
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await DB.Set<T>().ToArrayAsync();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        public IQueryable<T> IQueryableByPage(int page, int size, out int totalcount) 
        {
            var PageData = DB.Set<T>();
            totalcount = PageData.Count();
            return PageData.Skip((page - 1) * size).Take(size);
        }
        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        public IQueryable<T> IQueryableByPage(Expression<Func<T, bool>> condition, int page, int size, out int totalcount) 
        {
            var PageData = DB.Set<T>();
            totalcount = PageData.Count();
            return PageData.Where(condition).Skip((page - 1) * size).Take(size);
        }
        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTran()
        {
            if (DBTransaction == null)
                DBTransaction = DB.Database.BeginTransaction();
            return DBTransaction;
        }
        public void Dispose()
        {
            DB.Dispose();
        }
        /// <summary>
        /// 执行Sql语句（有参数）、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteSql(string sql, params object[] parameters)
        {
          return await DB.Database.ExecuteSqlCommandAsync(sql, parameters); 
        }
        /// <summary>
        /// 事物提交
        /// </summary>
        public void TranCommit()
        {
            if (DBTransaction == null)
                return;
            DBTransaction.Commit();
        }
        /// <summary>
        /// 事物回滚
        /// </summary>
        public void TranRoolBack()
        {
            if (DBTransaction == null)
                return;
            DBTransaction.Rollback();
        }
        /// <summary>
        /// 执行事物
        /// </summary>
        /// <param name="dbTransAction"></param>
        public void Trans(Action<IDbContextTransaction> dbTransAction)
        {
            if (DBTransaction == null)
                DBTransaction = DB.Database.BeginTransaction();
            dbTransAction.Invoke(DBTransaction);
            DBTransaction.Commit();
        }
        /// <summary>
        /// 执行Sql语句、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public async Task<int> ExecuteSql(string sql)
        {
           return await  DB.Database.ExecuteSqlCommandAsync(sql);
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public List<T> UpdateEnetityList(List<T> entity)
        {
            return DB.Update(entity).Entity;
        }

    
    }
}
