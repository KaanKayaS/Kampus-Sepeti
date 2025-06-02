using KampüsSepeti.Application.Features.Results.CategoryResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.CategoriesQueries
{
    public class GetAllCategoryQuery : IRequest<IList<GetAllCategoryQueryResult>>
    {
    }
}
