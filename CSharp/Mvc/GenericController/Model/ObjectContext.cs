namespace NotUseful.CSharp.Mvc.GenericController.Model
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    /// <summary>
    /// data context
    /// </summary>
    public class ObjectContext
    {
        /// <summary>
        /// init map dictionary
        /// </summary>
        public ObjectContext()
        {
            map = new Dictionary<Type, List<object>>();
        }

        /// <summary>
        /// save data we add
        /// </summary>
        private Dictionary<Type, List<Object>> map;

        /// <summary>
        /// use generic method to get single type of data
        /// </summary>
        /// <typeparam name="T">type we want to get data</typeparam>
        /// <returns>all data of type T</returns>
        public IEnumerable<T> GetAllData<T>() 
        {
            var type = typeof(T);
            if(map.ContainsKey(type))
                return map[type].Cast<T>();
            else
                return Enumerable.Empty<T>();
        }

        /// <summary>
        /// use generic method to get single type of data
        /// </summary>
        /// <param name="type">type we want to get data</param>
        /// <returns>all data which parameter type</returns>
        public IEnumerable<object> GetAllData(Type type)
        {
            if (map.ContainsKey(type))
                return map[type];
            else 
                return Enumerable.Empty<object>();
        }

        /// <summary>
        /// add data for init data
        /// </summary>
        /// <typeparam name="T">add data type</typeparam>
        /// <param name="ts">array of data</param>
        /// <returns>return self for Fluent api</returns>
        public ObjectContext Add<T>(params T[] ts)
        {
            var type = typeof(T);
            if(!map.ContainsKey(type))
                map.Add(type, new List<object>());
            map[type].AddRange(ts.OfType<object>());
            return this;
        }
    }
}