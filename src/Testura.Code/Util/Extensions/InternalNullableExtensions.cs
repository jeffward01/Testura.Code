namespace Testura.Code.Util.Extensions;

internal static class InternalNullableExtensions
{
    public static bool IsNotNull(this object? input)
    {
        return input != null;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        if (source == null)
        {
            return true;
        }

        return source.Any() == false;
    }

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        if (source == null)
        {
            return false;
        }

        return source.Any();
    }

    public static bool IsNull(this object? input)
    {
        return input == null;
    }
}
