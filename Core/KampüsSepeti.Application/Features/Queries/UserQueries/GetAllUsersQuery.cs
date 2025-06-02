using KampüsSepeti.Application.Features.Results.UserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<IList<GetAllUsersQueryResult>>
    {
    }
}
