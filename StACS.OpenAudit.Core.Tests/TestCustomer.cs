using System;

namespace StACS.OpenAudit.Core.Tests
{
    public class TestCustomer
    {
        public TestCustomer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}