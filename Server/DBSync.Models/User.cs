using System;

namespace DBSync.Models
{
    public class User: BaseModel
    {      
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"A valid name is required.");
            Name = name;
        }
        protected User() { }
        public virtual string Name { get; set; }
        public virtual int? Age { get; set; }
    }
}
