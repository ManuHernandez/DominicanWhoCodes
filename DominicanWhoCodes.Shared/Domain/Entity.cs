

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominicanWhoCodes.Shared.Domain
{
    public abstract class Entity
    {
        private Guid _id;
        private List<INotification> _domainEvents;
        public virtual Guid Id
        {
            get
            {
                return _id;
            }

            protected set
            {
                _id = value;
            }
        }
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(INotification @event)
        {
            _domainEvents?.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public static bool operator ==(Entity left, Entity right) => Equals(left, right);
        public static bool operator !=(Entity left, Entity right) => !Equals(left, right);
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this.GetType() != other.GetType())
                return false;

            if (this.Id != ((Entity)other).Id)
                return false;

            return true;
        }

        //Reference: (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
        public override int GetHashCode() => this.Id.GetHashCode() ^ 31;
    }
}
