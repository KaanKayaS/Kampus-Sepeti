using KampüsSepeti.Application.Features.Results.FavoriteResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.FavoritePostQueries
{
    public class GetFavoritePostsByUserIdQuery: IRequest<IList<GetFavoritePostsByUserIdQueryResult>>
    {
        public int Id { get; set; }

        public GetFavoritePostsByUserIdQuery(int id)
        {
            Id = id;
        }
    }
}
