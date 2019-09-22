
namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users
{
    using DominicanWhoCodes.Shared.Domain;
    using System;
    public class User : Entity<User>, IAggregateRoot
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private UserStatus _userStatus;
        private DateTime _creationDate;
        public User(UserId userId, string firstName, string lastName, string email)
        {
            Id = userId.Value;
            _firstName = FieldChecker.NotEmpty(firstName, nameof(firstName));
            _lastName = FieldChecker.NotEmpty(lastName, nameof(lastName));
            _email = FieldChecker.NotEmpty(email, nameof(email));
            _userStatus = UserStatus.Pending;
            _creationDate = DateTime.UtcNow;
        }

        public DateTime CreationDate => _creationDate;

        public UserStatus CurrentStatus => _userStatus;
    }
}
