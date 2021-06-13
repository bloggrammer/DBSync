using DBSync.Models;

namespace DBSync.DAL.Mappings
{
    public class UserMap : BaseMap<User>
    {
        public UserMap()
        {
            Table("People");
            Map(p => p.Age);
            Map(p => p.Name).Not.Nullable();
        }
    }
}
