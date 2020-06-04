using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleSearchAppDemo.Core.Domain
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public string ImageLink { get; set; }

        public virtual ICollection<PersonInterest> PersonInterests { get; set; } = new Collection<PersonInterest>();
    }
}
