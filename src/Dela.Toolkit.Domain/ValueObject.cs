namespace Dela.Toolkit.Domain;

/// <summary>
/// This is pattern called the Curiously Recurring Template Pattern (CRTP).
/// means that the type parameter T must inherit from ValueObject<T> itself.
/// This pattern enables static polymorphism - the base class can call methods on the derived class at compile time without virtual method overhead.
/// </summary>
public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    protected abstract IEnumerable<object?> GetAttributesToIncludeInEqualityCheck();

    public override bool Equals(object? other)
    {
        return Equals(other as T);
    }

    public virtual bool Equals(T? other)
    {
        if (other == null)
            return false;

        return GetAttributesToIncludeInEqualityCheck()
            .SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
    }

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        var hash = 17;
        foreach (var obj in GetAttributesToIncludeInEqualityCheck())
            hash = hash * 31 + (obj == null ? 0 : obj.GetHashCode());
        return hash;
    }
}