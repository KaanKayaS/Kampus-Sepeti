using KampüsSepeti.Application.Features.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.CommentQueries
{
    public class GetCommentByIdQuery : IRequest<GetCommentByIdQueryResult>
    {
        public int Id { get; set; }
        public GetCommentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
