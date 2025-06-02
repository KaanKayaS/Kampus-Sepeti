using AutoMapper;
using KampüsSepeti.Application.Features.Queries.CommentQueries;
using KampüsSepeti.Application.Features.Results.CommentResults;
using KampüsSepeti.Application.Features.Rules;
using KampüsSepeti.Application.Interfaces.UnitOfWorks;
using KampüsSepeti.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Handlers.CommentHandlers
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, GetCommentByIdQueryResult>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CommentRules commentRules;

        public GetCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, CommentRules commentRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.commentRules = commentRules;
        }
        public async Task<GetCommentByIdQueryResult> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            Comment comment = await unitOfWork.GetReadRepository<Comment>()
                .GetAsync(x => x.Id == request.Id, include: x => x.Include(u => u.User));

            await commentRules.CommentNoutFound(comment);

            return mapper.Map<GetCommentByIdQueryResult>(comment);
        }
    }
}
