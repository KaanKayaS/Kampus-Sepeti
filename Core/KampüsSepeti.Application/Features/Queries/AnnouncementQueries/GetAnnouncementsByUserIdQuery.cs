using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.AnnouncementQueries
{
    public class GetAnnouncementsByUserIdQuery : IRequest<IList<GetAnnouncementsByUserIdQueryResult>>
    {
        public int Id { get; set; }
        public GetAnnouncementsByUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
