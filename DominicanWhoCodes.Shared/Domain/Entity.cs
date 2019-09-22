

using System;
using System.Linq;

namespace DominicanWhoCodes.Shared.Domain
{
    public abstract class Entity<T> where T: class
    {
        private int? _hashCode;
        private Guid _id;
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
        public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);
        public static bool operator !=(Entity<T> left, Entity<T> right) => !Equals(left, right);
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this.GetType() != other.GetType())
                return false;

            if (this.Id != ((Entity<T>)other).Id)
                return false;

            return true;
        }

        //Reference: (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
        public override int GetHashCode() => _hashCode ?? this.Id.GetHashCode() ^ 31;
    }
}
