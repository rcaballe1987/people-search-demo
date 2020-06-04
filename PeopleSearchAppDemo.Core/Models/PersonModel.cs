using System;
using System.Collections.Generic;

namespace PeopleSearchAppDemo.Core.Models
{
    public class PersonModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public string ImageLink { get; set; }

        public virtual ICollection<Guid> PersonInterestIds { get; set; }
    }
}
