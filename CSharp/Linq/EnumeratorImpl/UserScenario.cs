namespace NotUseful.CSharp.Linq.EnumeratorImpl
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using Model;

    /// <summary>
    /// test User what we Implement
    /// </summary>
    public class UserScenario
    {
        /// <summary>
        /// 測試每個User都可以透過自己的IEnumerator改變Friend指向的User
        /// </summary>
        /// <param name="user"></param>
        [Theory,MemberData("AllUsers")]
        public void User_friends_tests(User user)
        {
            Assert.Equal((user.Id + 1) % 5, user.First().Friend.Id % 5);
            Assert.Equal((user.Id + 2) % 5,user.Skip(1).First().Friend.Id % 5);
        }

        /// <summary>
        /// 測試只要透過FindSelf就可以找到自己
        /// FindSelf是以遞迴的方式去尋找是否與Self相同Id的Friend
        /// 故名為四海之內皆兄弟(All Men Are Brothers translate by google)
        /// </summary>
        [Fact]
        public void All_Men_Are_Brothers()
        {
            var Users = AllUser().Cast<User>().ToList();
            Assert.True(FindSelf(Users.First()));
        }

        /// <summary>
        /// Find Self Entry Method
        /// </summary>
        /// <param name="self">User object we should find</param>
        /// <returns>return true if find self</returns>
        public bool FindSelf(User self) => self.Any(x => FindSelf(self, x.Friend));

        /// <summary>
        /// Recurrence FindSelf unless find same Id with self
        /// </summary>
        /// <param name="self">User we want to find</param>
        /// <param name="target">User we should find from</param>
        /// <returns>return true if find self</returns>
        public bool FindSelf(User self, User target) 
            => self.Id == target.Id 
                ? true 
                : target.Any(x => FindSelf(self, x.Friend));

        /// <summary>
        /// Init User Data and Relation
        /// Create 5 Users and have two friends in next two Id
        /// </summary>
        /// <returns>Users with friends</returns>
        public static IEnumerable<User> AllUser()
        {
            var Users = new List<User>() {
                new User() { Id = 0 , Name = "Kevin" , Sex = SexType.Male },
                new User() { Id = 1 , Name = "July" , Sex = SexType.Female },
                new User() { Id = 2 , Name = "Bill" , Sex = SexType.Male },
                new User() { Id = 3 , Name = "Wandy" , Sex = SexType.Female },
                new User() { Id = 4 , Name = "David" , Sex = SexType.Male },
            };

            var tUser = new List<User>();
            tUser.AddRange(Users);
            tUser.AddRange(Users);

            Users.ForEach(
                x => tUser.Skip(x.Id + 1 % 5)
                    .Take(2)
                    .ToList()
                    .ForEach(y => x.Friends.Add(y))
            );
            return Users;
        }     

        /// <summary>
        /// MemberData version AllUser 
        /// </summary>
        /// <returns>type of MemberData need</returns>
        public static IEnumerable<object[]> AllUsers()
        { 
            foreach(User u in AllUser())
            {
                yield return new object[] { u };
            }
        }
    }
}
