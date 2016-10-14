namespace NotUseful.CSharp.Linq.EnumeratorImpl.Model
{
    using System.Collections.Generic;
    /// <summary>
    /// Sex of User
    /// </summary>
    public enum SexType { Male, Female }

    /// <summary>
    /// User Entity 
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SexType Sex { get; set; }
        public ICollection<User> Friends { get; set; }
    }
}
