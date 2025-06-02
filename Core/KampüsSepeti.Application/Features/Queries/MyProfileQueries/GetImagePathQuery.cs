using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Queries.MyProfileQueries
{
    public class GetImagePathQuery: IRequest<string>
    {
        public int Id { get; set; }

        public GetImagePathQuery(int id)
        {
            Id = id;
        }
    }
}
