using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.AnnouncementQueries
{
    public class GetAllAnnouncementQuery: IRequest<IList<GetAllAnnouncementQueryResult>>
    {
    }
}
