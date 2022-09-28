namespace Testura.Code.Util.Extensions;

using System.Text.RegularExpressions;

internal static class InternalStringExtensions
{
    public static string ToSingleSpacedString(this string targetString)
    {
        var regex = new Regex("[ ]{2,}", RegexOptions.None);

        return regex.Replace(targetString, " ");
    }

    public static string ToTextBetweenLeftAndRightStrings(
        this string property, char leftTargetString, char rightTargetString)
    {
        var rightSideManipulationString = property;
        var leftSideManipulationString = property;

        var leftSide = leftSideManipulationString.ToLeftSubstring(leftTargetString);
        var rightSide = rightSideManipulationString.ToRightSubString(rightTargetString);

        return property.RemoveText(leftSide)
            .RemoveText(rightSide);
    }

    public static string ToTextBetweenLeftAndRightStrings(
        this string property, string leftTargetString, string rightTargetString)
    {
        var rightSideManipulationString = property;
        var leftSideManipulationString = property;

        var leftSide = leftSideManipulationString.ToLeftSubstring(leftTargetString);
        var rightSide = rightSideManipulationString.ToRightSubString(rightTargetString);

        return property.RemoveText(leftSide)
            .RemoveText(rightSide);
    }

    /// <summary>   Removes target text from the string. </summary>
    /// <remarks>   Jeff Ward, 1/7/2022. </remarks>
    /// <param name="target">           The target to act on. </param>
    /// <param name="removeThis">       The remove this. </param>
    /// <param name="comparisonType">   (Optional) Type of the comparison. </param>
    /// <returns>   A string. </returns>
    public static string RemoveText(
        this string target, string removeThis,
        StringComparison comparisonType = StringComparison.CurrentCulture)
    {
        return target.Replace(removeThis, string.Empty, comparisonType);
    }

    /// <summary>   A string extension method that gets right sub string. </summary>
    /// <remarks>   Jeff Ward, 1/14/2022. </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="val">                  val. </param>
    /// <param name="target">               Target for the. </param>
    /// <param name="stringComparisonType"> (Optional) Type of the string comparison. </param>
    /// <returns>   The given data converted to a string. </returns>
    public static string ToRightSubString(
        this string val, string target,
        StringComparison stringComparisonType = StringComparison.CurrentCulture)
    {
        var evalThis = val;
        var length = val.IndexOf(target, StringComparison.OrdinalIgnoreCase);
        var removedFromIndex = evalThis.Remove(0, length);
        var targetRemovedAlso = removedFromIndex.Remove(0, target.Length);
        var realRemovedIndex = val.IndexOf(targetRemovedAlso, stringComparisonType);

        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException(nameof(val));
        }

        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(target),
                "length cannot be higher than total string length or less than 0");
        }

        return val.Substring(realRemovedIndex, val.MaxIndex() - realRemovedIndex + 1);
    }

    /// <summary>   A string extension method that gets right sub string. </summary>
    /// <remarks>   Jeff Ward, 1/14/2022. </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="val">                  val. </param>
    /// <param name="target">               Target for the. </param>
    /// <param name="stringComparisonType"> (Optional) Type of the string comparison. </param>
    /// <returns>   The given data converted to a string. </returns>
    public static string ToRightSubString(
        this string val, char target,
        StringComparison stringComparisonType = StringComparison.CurrentCulture)
    {
        var evalThis = val;
        var length = val.IndexOf(target, StringComparison.OrdinalIgnoreCase);
        var removedFromIndex = evalThis.Remove(0, length);
        var targetRemovedAlso = removedFromIndex.Remove(0, 1);
        var realRemovedIndex = val.IndexOf(targetRemovedAlso, stringComparisonType);

        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException(nameof(val));
        }

        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(target),
                "length cannot be higher than total string length or less than 0");
        }

        return val.Substring(realRemovedIndex, val.MaxIndex() - realRemovedIndex + 1);
    }

    /// <summary>   A string extension method that gets left substring. </summary>
    /// <remarks>   Jeff Ward, 8/28/2021. </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="val">      val. </param>
    /// <param name="target">   Target for the. </param>
    /// <returns>   The left substring. </returns>
    public static string ToLeftSubstring(this string val, string target)
    {
        var length = val.IndexOf(target, StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException(nameof(val));
        }

        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(target),
                "length cannot be higher than total string length or less than 0");
        }

        return val.Substring(0, length);
    }

    /// <summary>   A string extension method that gets left substring. </summary>
    /// <remarks>   Jeff Ward, 8/28/2021. </remarks>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside the required range.
    /// </exception>
    /// <param name="val">      val. </param>
    /// <param name="target">   Target for the. </param>
    /// <returns>   The left substring. </returns>
    public static string ToLeftSubstring(this string val, char target)
    {
        var length = val.IndexOf(target, StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException(nameof(val));
        }

        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(target),
                "length cannot be higher than total string length or less than 0");
        }

        return val.Substring(0, length);
    }
}
