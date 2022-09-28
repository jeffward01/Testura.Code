namespace Testura.Code.Enums.GetterSetters;

using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using Interfaces;

internal abstract class GetterSetterType : SmartEnum<GetterSetterType>, IGetterSetterType
{
    public static readonly GetterSetterType None = new NoSetterTypeDescribed();

    public static readonly GetterSetterType PublicSetter = new PublicSetterType();

    public static readonly GetterSetterType PrivateSetter = new PrivateSetterType();

    public static readonly GetterSetterType ProtectedSetter = new ProtectedSetterType();

    public static readonly GetterSetterType ProtectedInternalSetter = new ProtectedSetterType();

    public static readonly GetterSetterType InitSetter = new InitSetterType();

    public static readonly GetterSetterType ProtectedInitSetter = new ProtectedInitSetterType();

    public static readonly GetterSetterType InternalInitInitSetter = new InternalInitSetterType();

    public static readonly GetterSetterType ProtectedInternalInitSetter =
        new ProtectedInternalInitSetterType();

    public static readonly GetterSetterType Unknown = new UnknownSetterType();

    private GetterSetterType(string name, ushort value)
        : base(name, value)
    {
    }

    /// <summary>  Gets the setter string without semi colon. </summary>
    /// <value>    The setter string without semi colon. </value>
    public virtual string SetterStringWithoutSemiColon => Name;

    /// <summary>   Gets the setter string with semi colon. </summary>
    /// <value> The setter string with semi colon. </value>
    public virtual string SetterStringWithSemiColon => $"{Name};";

    public static IReadOnlyCollection<string> GetListOfNames()
    {
        var list = List;

        return list.Select(_ => _.Name)
            .ToList();
    }

    public virtual string GetFormattedGetterSetter(string getterString = "get")
    {
        if (getterString.EndsWith(";", StringComparison.Ordinal) == false)
        {
            getterString = $"{getterString};";
        }

        var setterSting =
            string.Equals(SetterStringWithoutSemiColon, "None", StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : SetterStringWithSemiColon;

        return ToSingleSpaceBetweenWords($"{{ {getterString} {setterSting} }}");
    }

    public virtual bool IsMatchSetterType(string setterType)
    {
        var cleaned = setterType.Replace(";", string.Empty, StringComparison.Ordinal)
            .Replace("get", string.Empty, StringComparison.Ordinal)
            .Replace("{", string.Empty, StringComparison.Ordinal)
            .Replace("}", string.Empty, StringComparison.Ordinal)
            .Replace(" ", string.Empty, StringComparison.Ordinal);

        return cleaned == SetterStringWithoutSemiColon;
    }

    private static string ToSingleSpaceBetweenWords(string target)
    {
        var regex = new Regex(@"[ ]{2,}", RegexOptions.None);

        return regex.Replace(target, @" "); // "words with multiple spaces"
    }

    private sealed class InitSetterType : GetterSetterType
    {
        public InitSetterType()
            : base("init", 5)
        {
        }
    }

    private sealed class InternalInitSetterType : GetterSetterType
    {
        public InternalInitSetterType()
            : base("internal init", 7)
        {
        }
    }

    private sealed class NoSetterTypeDescribed : GetterSetterType
    {
        public NoSetterTypeDescribed()
            : base("none", 1)
        {
        }
    }

    private sealed class PrivateSetterType : GetterSetterType
    {
        public PrivateSetterType()
            : base("private set", 3)
        {
        }
    }

    private sealed class ProtectedInitSetterType : GetterSetterType
    {
        public ProtectedInitSetterType()
            : base("protected init", 6)
        {
        }
    }

    private sealed class ProtectedInternalInitSetterType : GetterSetterType
    {
        public ProtectedInternalInitSetterType()
            : base("protected internal init", 8)
        {
        }
    }

    private sealed class ProtectedSetterType : GetterSetterType
    {
        public ProtectedSetterType()
            : base("protected set", 4)
        {
        }
    }

    private sealed class PublicSetterType : GetterSetterType
    {
        public PublicSetterType()
            : base("set", 2)
        {
        }
    }

    private sealed class UnknownSetterType : GetterSetterType
    {
        public UnknownSetterType()
            : base("unknown", 9)
        {
        }
    }
}
