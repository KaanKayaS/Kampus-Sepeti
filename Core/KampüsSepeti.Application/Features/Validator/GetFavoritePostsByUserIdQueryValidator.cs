using FluentValidation;
using KampüsSepeti.Application.Features.Queries.FavoritePostQueries;
using KampüsSepeti.Application.Features.Queries.PostQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Validator
{
    public class GetFavoritePostsByUserIdQueryValidator : AbstractValidator<GetFavoritePostsByUserIdQuery>
    {
        public GetFavoritePostsByUserIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithName("Id");
        }
    }
    
}
