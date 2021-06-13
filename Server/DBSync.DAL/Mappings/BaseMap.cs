using DBSync.Models;
using FluentNHibernate.Mapping;

namespace DBSync.DAL.Mappings
{
    public class BaseMap<T> : ClassMap<T> where T : BaseModel
    {
        public BaseMap() => Id(x => x.Id).GeneratedBy.GuidComb();

    }
}
