using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleSearchAppDemo.Core.Domain
{
    public class PersonInterest
    {
        [Key]
        public Guid Id { get; set; }

        [Key, Column(Order = 1), ForeignKey("Person")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Key, Column(Order = 2), ForeignKey("Interest")]
        public Guid InterestId { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
