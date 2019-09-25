
using System;

namespace DominicanWhoCodes.Shared.Domain
{
    public abstract class Id<TEntity> where TEntity: class
    {
        public Guid Value { get; }
        public Id(Guid value)
        {
            if (value == null || value == Guid.Empty)
                throw new ArgumentException(nameof(value), "The Id cannot be empty");

            Value = value;
        }

        public Id()
        {
            Value = Guid.NewGuid();
        }

        public override string ToString() => Value.ToString();
    }
}
