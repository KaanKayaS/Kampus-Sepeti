using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.FavoritePostCommands
{
    public class AddFavoritePostCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
