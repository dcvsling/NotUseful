namespace NotUseful.CSharp.Mvc.GenericController.Model
{
    using System.Collections.Generic;
    using Entity;

    /// <summary>
    /// simple Repository interface
    /// </summary>
    public interface IRepository<out TUser> where TUser : User
    {
        /// <summary>
        /// get all data of user
        /// </summary>
        /// <returns>all users data we got</returns>
        IEnumerable<TUser> GetAll();
    }
}