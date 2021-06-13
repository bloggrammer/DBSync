using System;

namespace DBSync.Usecases.Base
{
    public class UserDto
    {
        public UserDto(string name, int? age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Guid Id { get; set; }
    }
}
