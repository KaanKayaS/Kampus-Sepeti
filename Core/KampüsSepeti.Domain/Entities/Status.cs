using KampüsSepeti.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Domain.Entities
{
    public class Status : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
