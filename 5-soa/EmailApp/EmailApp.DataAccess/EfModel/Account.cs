using System.Collections;
using System.Collections.Generic;

namespace EmailApp.DataAccess.EfModel
{

    // DTO, data transfer object
    // an object(/class) that isn't meant to have much behavior,
    // it's meant to be serialized or otherwise transferred to or from some different environment.

    // having these separate from domain objects means the domain classes can be designed without
    // worrying about things like serialization

    // this thinking applies to both the EF model and to the objects returned from ASP.NET action methods.

    // DTOs in .NET are often in a certain pattern called "POCO" (plain old CLR object)
    //    zero-argument constructor
    //    public auto-properties

    // (in C# 9, they added "record" types which handle making this kind of class easier/simpler)

    public class Account
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public ICollection<Message> SentMessages { get; set; }
    }
}