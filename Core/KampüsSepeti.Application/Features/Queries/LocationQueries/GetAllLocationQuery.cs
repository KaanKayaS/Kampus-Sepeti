using KampüsSepeti.Application.Features.Results.LocationResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.LocationQueries
{
    public class GetAllLocationQuery: IRequest<IList<GetAllLocationQueryResult>>
    {
    }
}
