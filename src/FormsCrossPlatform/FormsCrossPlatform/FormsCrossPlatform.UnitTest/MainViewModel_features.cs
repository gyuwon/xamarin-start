using System.Linq;
using FluentAssertions;
using FormsCrossPlatform.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FormsCrossPlatform.UnitTest
{
    [TestClass]
    public class MainViewModel_features
    {
        [TestMethod]
        public void given_name_is_empty_when_Add_executed_then_no_contact_added()
        {
            // Arrange
            var sut = new MainViewModel();
            sut.Name = string.Empty;

            // Act
            sut.Add.Execute(null);

            // Assert
            sut.Contacts.Should().BeEmpty();
        }

        [TestMethod]
        public void given_email_is_empty_when_Add_executed_then_no_contact_added()
        {
            // Arrange
            var sut = new MainViewModel();
            sut.Email = string.Empty;

            // Act
            sut.Add.Execute(null);

            // Assert
            sut.Contacts.Should().BeEmpty();
        }

        [TestMethod]
        public void when_Add_executed_then_new_contact_added_correctly()
        {
            // Arrange
            var sut = new MainViewModel();
            sut.Name = "Tony Stark";
            sut.Email = "ironman@avengers.com";

            // Act
            sut.Add.Execute(null);

            // Assert
            sut.Contacts.Should().HaveCount(1);
            sut.Contacts.Single().Name.Should().Be("Tony Stark");
            sut.Contacts.Single().Email.Should().Be("ironman@avengers.com");
        }

        [TestMethod]
        public void when_Add_exeuted_then_entry_fields_cleared()
        {
            // Arrange
            var sut = new MainViewModel();
            sut.Name = "Tony Stark";
            sut.Email = "ironman@avengers.com";

            // Act
            sut.Add.Execute(null);

            // Assert
            sut.Name.Should().BeEmpty();
            sut.Email.Should().BeEmpty();
        }
    }
}
