using System;
using System.Collections.Generic;

namespace HrSystem.Data.Models
{
    public partial class User
    {
        public User()
        {
            Candidates = new HashSet<Candidate>();
            Requisitions = new HashSet<Requisition>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserTokens = new HashSet<UserToken>();
            Vacancies = new HashSet<Vacancy>();
            Roles = new HashSet<Role>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Requisition> Requisitions { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
