using KampüsSepeti.Application.Features.Results.StatusResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.StatusQueries
{
    public class GetAllStatusQuery : IRequest<IList<GetAllStatusQueryResult>>
    {
    }
}
