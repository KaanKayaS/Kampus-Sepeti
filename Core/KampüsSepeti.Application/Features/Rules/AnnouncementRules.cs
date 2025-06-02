using KampüsSepeti.Application.Bases;
using KampüsSepeti.Application.Features.Exceptions;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Rules
{
    public class AnnouncementRules : BaseRules
    {
        public Task AnnouncementTitleMustNotBeSame(IList<Announcement> announcements, string requestTitle)
        {
            if (announcements.Any(x => x.Title.Equals(requestTitle, StringComparison.OrdinalIgnoreCase)))
                throw new AnnouncementTitleMustNotBeSameException();

            return Task.CompletedTask;
        }

        public Task AnnouncementNotFound(Announcement announcement)
        {
            if (announcement == null)
                throw new AnnouncementNotFoundException();
            return Task.CompletedTask;

        }
    }
}
