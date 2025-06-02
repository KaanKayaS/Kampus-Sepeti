using KampüsSepeti.Application.Features.Results.PostResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.PostQueries
{
    public class GetPostByLocationIdQuery :IRequest<IList<GetPostByLocationIdQueryResult>>
    {
        public int Id { get; set; }

        public GetPostByLocationIdQuery(int id)
        {
            Id = id;
        }
    }
}
