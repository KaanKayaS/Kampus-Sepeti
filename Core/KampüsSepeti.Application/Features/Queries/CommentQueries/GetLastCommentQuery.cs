using KampüsSepeti.Application.Features.Results.CommentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.CommentQueries
{
    public class GetLastCommentQuery : IRequest<GetLastCommentQueryResult>
    {
        public int UserId { get; set; }
        public int AnnouncementId { get; set; }

        public GetLastCommentQuery(int userId, int announcementId)
        {
            UserId = userId;
            AnnouncementId = announcementId;
        }
    }
}
