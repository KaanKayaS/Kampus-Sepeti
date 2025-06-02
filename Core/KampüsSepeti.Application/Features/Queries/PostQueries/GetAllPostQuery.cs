    using KampüsSepeti.Application.DTOs;
    using KampüsSepeti.Application.Features.Results.PostResults;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace KampüsSepeti.Application.Features.Queries.PostQueries
    {
        public class GetAllPostQuery : IRequest<IList<GetAllPostQueryResult>>
        {
        }
    }
    
