using System;
using System.Collections.Generic;

#nullable disable

namespace EfDbFirstDemo.DataAccess
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }

        // in the EF model, there's "simple" properties that map directly to columns in tables.
        // if you pull the object out of a DbSet, those properties will always be filled in.

        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }


        // the other properties either collections or references to other entities.
        // navigation properties.

        // EF will not fill these in by default... because if you take that idea to its conclusion,
        // that would mean downloading most or all of the database
        
        // "loading related data" in ef core.
        // #1 - eager loading. call the Include and ThenInclude methods with delegates that point to the right navigation properties
        //    that you want to be filled in.

        public virtual Customer Customer { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
