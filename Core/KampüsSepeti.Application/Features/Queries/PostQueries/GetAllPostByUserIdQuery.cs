using KampüsSepeti.Application.Features.Results.PostResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.PostQueries
{
    public class GetAllPostByUserIdQuery : IRequest<IList<GetAllPostByUserIdQueryResult>>
    {
        public int Id { get; set; }

        public GetAllPostByUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
