using System;
using Microsoft.AspNetCore.Identity;

namespace data.model
{
    public class User : IdentityUser<Guid>
    {
        public Guid UpdateBy { get; set; }

        public DateTime UpdateAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool RequirePasswordChange { get; set; }

        public DateTime LastPasswordChangeAt { get; set; }
    }
}
