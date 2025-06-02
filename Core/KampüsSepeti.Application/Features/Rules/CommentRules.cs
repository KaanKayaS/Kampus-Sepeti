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
    public class CommentRules : BaseRules
    {
        public Task CommentNoutFound(Comment comment)
        {
            if (comment == null)
                throw new CommentNoutFoundException();
            return Task.CompletedTask;
        }
    }
}
