namespace Testura.Code.Enums.Modifiers;

using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using Interfaces;
using Util.Extensions;

/// <summary>   An enum modifier keyword. This class cannot be inherited. </summary>
/// <remarks>   Jeff Ward, 9/9/2021. </remarks>
/// <seealso cref="T:Ardalis.SmartEnum.SmartEnum{EnumModifierKeyword}" />
internal abstract class DotNetModifierKeyword : SmartEnum<DotNetModifierKeyword>,
    IDotNetModifierKeyword
{
    public static readonly DotNetModifierKeyword None = new NoneModifierKeywordType();

    public static readonly DotNetModifierKeyword Virtual = new VirtualModifierKeywordType();

    public static readonly DotNetModifierKeyword Event = new EventModifierKeywordType();

    public static readonly DotNetModifierKeyword Static = new StaticModifierKeywordType();

    public static readonly DotNetModifierKeyword Readonly = new ReadonlyModifierKeywordType();

    public static readonly DotNetModifierKeyword Async = new AsyncModifierKeywordType();

    public static readonly DotNetModifierKeyword Const = new ConstModifierKeywordType();

    public static readonly DotNetModifierKeyword New = new NewModifierKeywordType();

    public static readonly DotNetModifierKeyword Volatile = new VolatileModifierKeywordType();

    public static readonly DotNetModifierKeyword Unsafe = new UnsafeModifierKeywordType();

    public static readonly DotNetModifierKeyword In = new InModifierKeywordType();

    public static readonly DotNetModifierKeyword Override = new OverrideModifierKeywordType();

    public static readonly DotNetModifierKeyword Out = new OutModifierKeywordType();

    public static readonly DotNetModifierKeyword Extern = new ExternModifierKeywordType();

    public static readonly DotNetModifierKeyword Params = new ParamsModifierKeywordType();

    public static readonly DotNetModifierKeyword Ref = new RefModifierKeywordType();

    public static readonly DotNetModifierKeyword Unknown = new UnknownModifierKeywordType();

    public static readonly DotNetModifierKeyword Abstract = new AbstractModifierKeywordType();

    public static readonly DotNetModifierKeyword Sealed = new SealedModifierKeywordType();

    public static readonly DotNetModifierKeyword Partial = new PartialModifierKeywordType();

    /// <summary>
    ///     Initializes a new instance of the <see cref="DotNetModifierKeyword" /> class.
    /// </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    /// <param name="name">     The name. </param>
    /// <param name="value">    The value. </param>
    private DotNetModifierKeyword(string name, ushort value)
        : base(name, value)
    {
    }

    public abstract string MarkerString { get; }

    public static ICollection<DotNetModifierKeyword> GetAllMatches(string compare)
    {
        var cleaned = compare.ToSingleSpacedString();
        var listOfMatches = new List<DotNetModifierKeyword>();
        if (MatchCount(cleaned) == 0)
        {
            return listOfMatches.AsReadOnly();
        }

        var nameList = GetCompareValues();
        foreach (var n in nameList)
        {
            if (IsMarkerMatch(cleaned, n)
                .IsFalse())
            {
                continue;
            }

            var match = GetMatchingEnumByMarker(n, cleaned, List);

            listOfMatches.Add(match);
        }

        return listOfMatches;
    }

    public static ICollection<string> GetCompareValues()
    {
        var list = List.Except(GetInvalidEnumTypes());

        var listOfNames = list.Select(_ => _.MarkerString)
            .ToList();

        return listOfNames;
    }

    public static DotNetModifierKeyword GetSingleMatch(string compare)
    {
        var nameList = GetCompareValues();
        if (MatchCount(compare.ToSingleSpacedString()) == 0 ||
            ContainsAnyOf(compare, nameList)
                .IsFalse())
        {
            return None;
        }

        var markerStringMatch = nameList.First(compare.Contains);

        return GetMatchFromMarkerString(markerStringMatch);
    }

    public static bool HasMatch(string compare)
    {
        var list = GetCompareValues();

        return GetTotalCount(compare, list) > 0;
    }

    public static bool IsAnyOf(string source, ICollection<string> values)
    {
        return values.Any(x => string.Equals(source, x, StringComparison.Ordinal));
    }

    public static ICollection<string> ListOfNames()
    {
        var list = List.Except(GetInvalidEnumTypes());

        return list.Select(_ => _.Name)
            .ToList();
    }

    public static int MatchCount(string compare)
    {
        var list = GetCompareValues();

        return GetTotalCount(compare, list);
    }

    protected static DotNetModifierKeyword GetMatchingEnumByMarker(
        string marker, string compareString, IReadOnlyCollection<DotNetModifierKeyword> enumList)
    {
        return enumList.First(
            _ => IsMarkerMatch(compareString, _.MarkerString) && _.MarkerString == marker);
    }

    protected static bool IsMarkerMatch(string compareString, string marker)
    {
        return Regex.Matches(compareString, marker)
                   .Count >
               0;
    }

    protected static bool ContainsAnyOf(string target, IEnumerable<string> optionsList)
    {
        var collection = optionsList as ICollection<string>;

        if (string.IsNullOrWhiteSpace(target))
        {
            return false;
        }

        var targetListOfWords = target.ToSingleSpacedString()
            .Split(" ")
            .ToList();

        foreach (var targetListOfWord in targetListOfWords)
        {
            if (IsAnyOf(targetListOfWord, collection))
            {
                return true;
            }
        }

        return false;
    }

    private static ICollection<DotNetModifierKeyword> GetInvalidEnumTypes()
    {
        var list = new List<DotNetModifierKeyword>();
        list.Add(None);
        list.Add(Unknown);

        return list;
    }

    private static DotNetModifierKeyword GetMatchFromMarkerString(string markerString)
    {
        var list = List;
        foreach (var l in list)
        {
            if (string.Equals(markerString, l.MarkerString))
            {
                return l;
            }
        }

        throw new InvalidDataException(
            $"Something went wrong. We could not find a match for the marker string: >> {markerString} << ");
    }

    private static int GetTotalCount(string compareString, IEnumerable<string> markerList)
    {
        var totalCount = 0;

        var collection = markerList as ICollection<string>;

        foreach (var marker in collection)
        {
            totalCount += Regex.Matches(compareString, marker)
                .Count;
        }

        return totalCount;
    }

    private sealed class AbstractModifierKeywordType : DotNetModifierKeyword
    {
        public AbstractModifierKeywordType()
            : base("abstract", 18)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class AsyncModifierKeywordType : DotNetModifierKeyword
    {
        public AsyncModifierKeywordType()
            : base("async", 7)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class ConstModifierKeywordType : DotNetModifierKeyword
    {
        public ConstModifierKeywordType()
            : base("const", 6)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class EventModifierKeywordType : DotNetModifierKeyword
    {
        public EventModifierKeywordType()
            : base("event", 3)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class ExternModifierKeywordType : DotNetModifierKeyword
    {
        public ExternModifierKeywordType()
            : base("extern", 14)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class InModifierKeywordType : DotNetModifierKeyword
    {
        public InModifierKeywordType()
            : base("in", 11)
        {
        }

        public override string MarkerString => $"{Name} ";
    }

    private sealed class NewModifierKeywordType : DotNetModifierKeyword
    {
        public NewModifierKeywordType()
            : base("new", 8)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class NoneModifierKeywordType : DotNetModifierKeyword
    {
        public NoneModifierKeywordType()
            : base("none", 1)
        {
        }

        public override string MarkerString => string.Empty;
    }

    private sealed class OutModifierKeywordType : DotNetModifierKeyword
    {
        public OutModifierKeywordType()
            : base("out", 13)
        {
        }

        public override string MarkerString => $"{Name} ";
    }

    private sealed class OverrideModifierKeywordType : DotNetModifierKeyword
    {
        public OverrideModifierKeywordType()
            : base("override", 12)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class ParamsModifierKeywordType : DotNetModifierKeyword
    {
        public ParamsModifierKeywordType()
            : base("params", 15)
        {
        }

        public override string MarkerString => $"{Name} ";
    }

    private sealed class PartialModifierKeywordType : DotNetModifierKeyword
    {
        public PartialModifierKeywordType()
            : base("partial", 20)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class ReadonlyModifierKeywordType : DotNetModifierKeyword
    {
        public ReadonlyModifierKeywordType()
            : base("readonly", 5)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class RefModifierKeywordType : DotNetModifierKeyword
    {
        public RefModifierKeywordType()
            : base("ref", 16)
        {
        }

        public override string MarkerString => $"{Name} ";
    }

    private sealed class SealedModifierKeywordType : DotNetModifierKeyword
    {
        public SealedModifierKeywordType()
            : base("sealed", 19)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class StaticModifierKeywordType : DotNetModifierKeyword
    {
        public StaticModifierKeywordType()
            : base("static", 4)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class UnknownModifierKeywordType : DotNetModifierKeyword
    {
        public UnknownModifierKeywordType()
            : base("unknown", 17)
        {
        }

        public override string MarkerString => string.Empty;
    }

    private sealed class UnsafeModifierKeywordType : DotNetModifierKeyword
    {
        public UnsafeModifierKeywordType()
            : base("unsafe", 10)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class VirtualModifierKeywordType : DotNetModifierKeyword
    {
        public VirtualModifierKeywordType()
            : base("virtual", 2)
        {
        }

        public override string MarkerString => $" {Name} ";
    }

    private sealed class VolatileModifierKeywordType : DotNetModifierKeyword
    {
        public VolatileModifierKeywordType()
            : base("volatile", 9)
        {
        }

        public override string MarkerString => $" {Name} ";
    }
}
