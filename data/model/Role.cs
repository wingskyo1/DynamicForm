using System;
using Microsoft.AspNetCore.Identity;

namespace data.model
{
    public class Role :IdentityRole<Guid>
    {
        public Guid UpdateBy { get; set; }

        public DateTime UpdateAt { get; set; }

        public bool IsDeleted { get; set; }
        public int AutoLogoutMinutes { get; set; }
        public int WaiveLimit { get; set; }
        public string Description { get; set; }

        public Role(string name) : base(name) {}
    }
}
