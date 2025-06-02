using KampüsSepeti.Application.Bases;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class PostRules : BaseRules
    {
        public Task PostNotFound(Post post)
        {
            if (post == null)
                throw new PostNotFoundException();
            return Task.CompletedTask;

        }
    }
}
