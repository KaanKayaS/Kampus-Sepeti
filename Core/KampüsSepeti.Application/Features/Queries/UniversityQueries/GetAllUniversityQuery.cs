using KampüsSepeti.Application.Features.Results.UniversityResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.UniversityQueries
{
    public class GetAllUniversityQuery: IRequest<IList<GetAllUniversityQueryResult>>
    {
    }
}
