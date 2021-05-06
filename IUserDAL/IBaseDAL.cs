using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IUserDAL
{
    public interface IBaseDAL<T> : IDisposable where T : class
    {
        new
        /// <summary>
        /// 释放上下文
        /// </summary>
        void Dispose();
        /// <summary>
        /// 根据主键查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        Task<T> FindAsync(string keyValue);

        /// <summary>
        /// 根据条件查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> condition);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        Task<IEnumerable<T>> IEnumerable(Expression<Func<T, bool>> condition);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        IQueryable<T> IQueryableByPage(int page, int size, out int totalcount);
        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        IQueryable<T> IQueryableByPage(Expression<Func<T, bool>> condition, int page, int size, out int totalcount);

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> AddEntityAsync(T entity, bool saved = true);
        /// <summary>
        /// 异步批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddEntityListAsync(List<T> entityList, bool saved = true);


        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> DeleteEntity(T entity, bool saved = true);
        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        Task<int> DeleteEntityListByCondition(Expression<Func<T, bool>> condition, bool saved = true);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<T> UpdateEnetity(T entity, bool saved = true);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        List<T> UpdateEnetityList(List<T> entity);

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTran();

        /// <summary>
        /// 事物提交
        /// </summary>
        void TranCommit();

        /// <summary>
        /// 事物回滚
        /// </summary>
        void TranRoolBack();

        /// <summary>
        /// 执行事物
        /// </summary>
        /// <param name="dbTransAction"></param>
        void Trans(Action<IDbContextTransaction> dbTransAction);

        /// <summary>
        /// 执行Sql语句、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        Task<int> ExecuteSql(string sql);

        /// <summary>
        /// 执行Sql语句（有参数）、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<int> ExecuteSql(string sql, params object[] parameters);
    }
}
