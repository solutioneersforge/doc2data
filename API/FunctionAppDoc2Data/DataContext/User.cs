using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class User
    {
        public User()
        {
            Receipts = new HashSet<Receipt>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
