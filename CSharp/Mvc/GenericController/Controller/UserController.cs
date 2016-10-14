namespace NotUseful.CSharp.Mvc.GenericController
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Model.Entity;

    
    [Route("[controller]/[generic]")]
    /// <summary>
    /// Generic Controll 
    /// constraint is TUser should inherit by User
    /// for this case ,all kind of users may have same logic in UserController
    /// <<typeparam name="TUser">any kind of User Type</typeparam> 
    /// <remarks>
    /// Route template is [[controller]/[generic]]
    /// so route path is User/TUser
    /// </remarks>
    /// </summary>
    public class UserController<TUser> : Controller where TUser : User
    {
        /// <summary>
        /// cache constructor parameter
        /// </summary>
        private IRepository<TUser> repo;
        /// <summary>
        /// constructor of UserController[T]
        /// then cache IRepository[T]
        /// </summary>
        /// <param name="repo">IRepository[T] will be inject from IServiceProvider when controller creating</param>
        public UserController(IRepository<TUser> repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        /// <summary>
        /// just return all what IRepository[T] get
        /// </summary>
        /// <returns>all data of TUser</returns>
        public IEnumerable<TUser> Get()
        {
            return repo.GetAll();
        }
    }
}
