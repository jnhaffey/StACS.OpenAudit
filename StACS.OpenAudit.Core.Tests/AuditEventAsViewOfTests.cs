using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StACS.OpenAudit.Core.Enums;
using StACS.OpenAudit.Core.Extensions;
using StACS.OpenAudit.Core.Models;

namespace StACS.OpenAudit.Core.Tests
{
    [TestClass]
    public class AuditEventAsViewOfTests
    {
        private TestCustomer _testCustomer;
        private string _testCustomerJson;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _testCustomer = new TestCustomer
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            _testCustomerJson = JsonConvert.SerializeObject(_testCustomer);
        }

        [TestMethod]
        public void Create_Instance_Non_Generic()
        {
            // Arrange

            // Act
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());

            // Assert
            auditEvent.OperationType.Should().Be(OperationType.View);
            auditEvent.TargetId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.TargetType.Should().Be(_testCustomer.GetType().Name);
            auditEvent.Timestamp.Should().NotBe(default(DateTime));
        }

        [TestMethod]
        public void Create_Instance_Generic()
        {
            // Arrange

            // Act
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());

            // Assert
            auditEvent.OperationType.Should().Be(OperationType.View);
            auditEvent.TargetId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.TargetType.Should().Be(_testCustomer.GetType().Name);
            auditEvent.Timestamp.Should().NotBe(default(DateTime));
        }
        
        [TestMethod]
        public void Generic_Instance_With_No_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());
            
            // Act
            auditEvent.WithNoData();

            // Assert
            auditEvent.DataId.Should().Be(null);
            auditEvent.DataType.Should().Be(null);
            auditEvent.Data.Should().Be(null);
        }
        
        [TestMethod]
        public void Non_Generic_Instance_With_No_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());
            
            // Act
            auditEvent.WithNoData();

            // Assert
            auditEvent.DataId.Should().Be(null);
            auditEvent.DataType.Should().Be(null);
            auditEvent.Data.Should().Be(null);
        }
        
        [TestMethod]
        public void Generic_Instance_With_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());
            
            // Act
            auditEvent.WithData(_testCustomer, tc => tc.Id.ToString());

            // Assert
            auditEvent.DataId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.DataType.Should().Be(_testCustomer.GetType().FullName);
            auditEvent.Data.Should().Be(_testCustomerJson);
            auditEvent.IsSensitiveData.Should().Be(false);
        }
        
        [TestMethod]
        public void Non_Generic_Instance_With_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());
            
            // Act
            auditEvent.WithData(_testCustomer, tc => tc.Id.ToString());

            // Assert
            auditEvent.DataId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.DataType.Should().Be(_testCustomer.GetType().FullName);
            auditEvent.Data.Should().Be(_testCustomerJson);
            auditEvent.IsSensitiveData.Should().Be(false);
        }
        
        [TestMethod]
        public void Generic_Instance_With_Sensitive_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());
            
            // Act
            auditEvent.WithSensitiveData(_testCustomer, tc => tc.Id.ToString());

            // Assert
            auditEvent.DataId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.DataType.Should().Be(_testCustomer.GetType().FullName);
            auditEvent.Data.Should().Be(_testCustomerJson);
            auditEvent.IsSensitiveData.Should().Be(true);
        }
        
        [TestMethod]
        public void Non_Generic_Instance_With_Sensitive_Data()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());
            
            // Act
            auditEvent.WithSensitiveData(_testCustomer, tc => tc.Id.ToString());

            // Assert
            auditEvent.DataId.Should().Be(_testCustomer.Id.ToString());
            auditEvent.DataType.Should().Be(_testCustomer.GetType().FullName);
            auditEvent.Data.Should().Be(_testCustomerJson);
            auditEvent.IsSensitiveData.Should().Be(true);
        }
        
        [TestMethod]
        public void Generic_Instance_As_Event()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());
            
            // Act
            auditEvent.AsEvent(TestHelpers.EventName);

            // Assert
            auditEvent.EventType.Should().Be(TestHelpers.EventName);
        }
        
        [TestMethod]
        public void Non_Generic_Instance_As_Event()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());
            
            // Act
            auditEvent.AsEvent(TestHelpers.EventName);

            // Assert
            auditEvent.EventType.Should().Be(TestHelpers.EventName);
        }
        
        [TestMethod]
        public void Generic_Instance_With_Description()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer, tc => tc.Id.ToString());
            
            // Act
            auditEvent.WithDescription(TestHelpers.Description);

            // Assert
            auditEvent.Description.Should().Be(TestHelpers.Description);
        }
        
        [TestMethod]
        public void Non_Generic_Instance_With_Description()
        {
            // Arrange
            var auditEvent = AuditEvent.AsViewOf(_testCustomer.GetType(), _testCustomer.Id.ToString());
            
            // Act
            auditEvent.WithDescription(TestHelpers.Description);

            // Assert
            auditEvent.Description.Should().Be(TestHelpers.Description);
        }
    }
}