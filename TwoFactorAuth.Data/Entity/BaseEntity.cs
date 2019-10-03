using System;
using System.Collections.Generic;
using System.Text;

namespace TwoFactorAuth.Data.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
