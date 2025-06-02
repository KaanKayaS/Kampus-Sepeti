using FluentValidation;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using KampüsSepeti.Application.Features.Queries.UserQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{  
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithName("Id");
        }
    }
    
}
