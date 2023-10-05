using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FantasyCore
{
    /// <summary>
    /// Generic wrapper for common database operations
    /// </summary>
	public class DbRepository
	{
        /// <summary>
        /// The underlying database context for direct manipulation
        /// </summary>
        public FantasyContext Context
        {
            get
            {
                return new FantasyContext();
            }
        }

        /// <summary>
        /// Gets an entity based on the non-null fields in the provided rawEntity.
        /// If an entity doesn't exist with those field values it creates a new one and returns that new entity
        /// The rawEntity must have sufficient non-null fields to be created.
        /// </summary>
        /// <typeparam name="T">The type of database entity to find</typeparam>
        /// <param name="rawEntity">A raw entity with non-null fields for querying</param>
        /// <returns>A persisted entity</returns>
        public async Task<T> GetOrCreateByAsync<T>(T rawEntity) where T : class, new()
        {
            using(FantasyContext context = this.Context)
            {
                var query = context.Set<T>().AsQueryable();

                var properties = typeof(T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.GetValue(rawEntity) != null)
                    .ToList();

                foreach (var property in properties)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var propertyExpression = Expression.Property(parameter, property.Name);
                    var constant = Expression.Constant(property.GetValue(rawEntity));
                    var equal = Expression.Equal(propertyExpression, constant);
                    var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

                    query = query.Where(lambda);
                }

                var entity = await query.FirstOrDefaultAsync();

                if (entity == null)
                {
                    entity = new T();

                    foreach (var property in properties)
                    {
                        property.SetValue(entity, property.GetValue(rawEntity));
                    }

                    context.Set<T>().Add(entity);
                    await context.SaveChangesAsync();
                }

                return entity;
            }
            
        }
                
    }
}

