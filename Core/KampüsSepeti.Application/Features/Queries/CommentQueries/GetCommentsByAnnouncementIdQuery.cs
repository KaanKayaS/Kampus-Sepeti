using KampüsSepeti.Application.Features.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.CommentQueries
{
    public class GetCommentsByAnnouncementIdQuery : IRequest<IList<GetCommentsByAnnouncementIdQueryResult>>
    {
        public int Id { get; set; }

        public GetCommentsByAnnouncementIdQuery(int id)
        {
            Id = id;
        }
    }
}
