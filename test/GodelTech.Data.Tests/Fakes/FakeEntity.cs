namespace GodelTech.Data.Tests.Fakes
{
    public class FakeEntity<TType> : IEntity<TType>
    {
        public TType Id { get; set; }

        public bool Equals(IEntity<TType> x, IEntity<TType> y)
        {
            // Check whether the compared objects reference the same data
            if (ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            // Check whether the objects' properties are equal.
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(IEntity<TType> obj)
        {
            // Check whether the object is null
            if (ReferenceEquals(obj, null)) return 0;

            // Calculate the hash code for the object.
            return obj.Id.GetHashCode();
        }
    }
}