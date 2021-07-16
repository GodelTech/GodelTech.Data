using System;

namespace GodelTech.Data.Tests.Fakes.Entities
{
    public class FakeGuidEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public bool Equals(IEntity<Guid> x, IEntity<Guid> y)
        {
            // Check whether the compared objects reference the same data
            if (ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            // Check whether the objects' properties are equal.
            return x.Id == y.Id;
        }

        public int GetHashCode(IEntity<Guid> obj)
        {
            // Check whether the object is null
            if (ReferenceEquals(obj, null)) return 0;

            // Calculate the hash code for the object.
            return obj.Id.GetHashCode();
        }
    }
}