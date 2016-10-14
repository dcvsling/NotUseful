namespace NotUseful.CSharp.Mvc.GenericController.Model
{
    using System.Collections.Generic;
    using Entity;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// implement IRepository[T] with only one method
    /// </summary>
    /// <typeparam name="TUser">all type of Users inherit User</typeparam>
    public class Repository<TUser> : IRepository<TUser> where TUser : User
    {
        /// <summary>
        /// caching users from constructor parameter context
        /// </summary>
        private IEnumerable<TUser> users;

        /// <summary>
        /// get data we need from context
        /// </summary>
        /// <param name="context">data context</param>
        public Repository(IOptions<ObjectContext> context)
        {
            this.users = context.Value.GetAllData<TUser>();
        }

        /// <summary>
        /// get all users data
        /// </summary>
        /// <returns>all users data we got</returns>
        public IEnumerable<TUser> GetAll()
        {
            return users;
        }
    }
}