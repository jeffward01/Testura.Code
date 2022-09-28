namespace Testura.Code.Enums.Collections;

using Ardalis.SmartEnum;
using GetterSetters;
using Interfaces;
using Modifiers;
using Util.Extensions;

/// <summary>   An enum collection csharp. </summary>
/// <remarks>   Jeff Ward, 9/9/2021. </remarks>
/// <seealso cref="T:Ardalis.SmartEnum.SmartEnum{EnumCollectionCSharp}" />
public abstract class DotNetCollectionType : SmartEnum<DotNetCollectionType>, IDotNetCollectionType
{
    /// <summary>   (Immutable) the array. </summary>
    public static readonly DotNetCollectionType Array = new ArrayEnum();

    /// <summary>   (Immutable) list of arrays. </summary>
    public static readonly DotNetCollectionType ArrayList = new ArrayListEnum();

    /// <summary>   (Immutable) the array pool. </summary>
    public static readonly DotNetCollectionType ArrayPool = new ArrayPoolEnum();

    /// <summary>   (Immutable) the collection. </summary>
    public static readonly DotNetCollectionType Collection = new CollectionEnum();

    /// <summary>   (Immutable) the collection base. </summary>
    public static readonly DotNetCollectionType CollectionBase = new CollectionBaseEnum();

    /// <summary>   (Immutable) zero-based index of the collection. </summary>
    public static readonly DotNetCollectionType ICollection = new CollectionInterfaceEnum();

    /// <summary>   (Immutable) queue of concurrents. </summary>
    public static readonly DotNetCollectionType ConcurrentQueue = new ConcurrentQueueEnum();

    /// <summary>   (Immutable) the dictionary. </summary>
    public static readonly DotNetCollectionType Dictionary = new DictionaryCollectionEnum();

    /// <summary>   (Immutable) zero-based index of the dictionary. </summary>
    public static readonly DotNetCollectionType IDictionary =
        new DictionaryInterfaceCollectionEnum();

    /// <summary>   (Immutable) array of doubles. </summary>
    public static readonly DotNetCollectionType DoubleArray = new DoubleArrayEnum();

    /// <summary>   (Immutable) collection of extensions. </summary>
    public static readonly DotNetCollectionType ExtensionCollection = new ExtensionCollectionEnum();

    /// <summary>   (Immutable) collection of extensions. </summary>
    public static readonly DotNetCollectionType IExtensionCollection =
        new ExtensionInterfaceCollectionEnum();

    /// <summary>   (Immutable) list of linked. </summary>
    public static readonly DotNetCollectionType LinkedList = new LinkedListEnum();

    /// <summary>   (Immutable) collection of lists. </summary>
    public static readonly DotNetCollectionType ListCollection = new ListCollectionEnum();

    /// <summary>   (Immutable) collection of lists. </summary>
    public static readonly DotNetCollectionType IListCollection = new ListInterfaceCollectionEnum();

    /// <summary>   (Immutable) array of quads. </summary>
    public static readonly DotNetCollectionType QuadArray = new QuadArrayEnum();

    // 17

    /// <summary>   (Immutable) the queue. </summary>
    public static readonly DotNetCollectionType Queue = new QueueEnum();

    // 18

    /// <summary>   (Immutable) the read only collection base. </summary>
    public static readonly DotNetCollectionType ReadOnlyCollectionBase =
        new ReadOnlyCollectionBaseEnum();

    /// <summary>   (Immutable) collection of read onlies. </summary>
    public static readonly DotNetCollectionType IReadOnlyCollection =
        new ReadOnlyCollectionInterfaceEnum();

    /// <summary>   (Immutable) array of singles. </summary>
    public static readonly DotNetCollectionType SingleArray = new SingleArrayEnum();

    /// <summary>   (Immutable) the stack. </summary>
    public static readonly DotNetCollectionType Stack = new StackEnum();

    /// <summary>   (Immutable) array of triples. </summary>
    public static readonly DotNetCollectionType TripleArray = new TripleArrayEnum();

    /// <summary>   (Immutable) the none. </summary>
    public static readonly DotNetCollectionType None = new NoneEnum();

    public static readonly DotNetCollectionType DbSet = new DbSetCollectionEnum();

    public static readonly DotNetCollectionType CustomType = new CustomUnknownType();

    public static readonly DotNetCollectionType ReadOnlyList = new ReadOnlyListEnum();

    public static readonly DotNetCollectionType IReadOnlyList = new IReadOnlyListEnum();

    public static readonly DotNetCollectionType Undefined = new UndefinedListType();

    /// <summary>
    ///     Initializes a new instance of the <see cref="DotNetCollectionType" /> class.
    /// </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    /// <param name="name">     The name. </param>
    /// <param name="value">    The value. </param>
    private DotNetCollectionType(string name, int value)
        : base(name, value)
    {
    }

    /// <summary>   Gets a value indicating whether this object is interface. </summary>
    /// <value> True if this object is interface, false if not. </value>
    public abstract bool IsInterface { get; }

    public virtual DotNetCollectionType GetCollectionType(string collectionTypeEval)
    {
        var evalThis = RemoveAccessorTypes(collectionTypeEval);

        var removedAccessor = RemoveGetterSetterString(evalThis);
        var removedModifierTypes = RemoveModifierTypes(removedAccessor);
        var cleaned = CleanPropertyTypeIfAny(removedModifierTypes);
        if (TryFromName(cleaned, false, out var result))
        {
            return result;
        }

        return None;
    }

    private static string CleanPropertyTypeIfAny(string propertyTypeSide)
    {
        if (propertyTypeSide.Contains('<') && propertyTypeSide.Contains('>'))
        {
            var evalThis = propertyTypeSide;
            var result = evalThis.ToTextBetweenLeftAndRightStrings("<", ">");
            var removedValues = evalThis.Replace(result, string.Empty)
                .Replace("<", string.Empty)
                .Replace(">", string.Empty);

            return result;
        }

        return propertyTypeSide;
    }

    private static string RemoveWordsFromString(
        string targetString, ICollection<string> wordsToRemove)
    {
        var evalThis = targetString;
        foreach (var removeWord in wordsToRemove)
        {
            var splits = evalThis.Split(" ");
            var removeThese = new List<string>();
            foreach (var split in splits)
            {
                if (removeWord.Length == split.Length)
                {
                    if (string.Equals(split, removeWord, StringComparison.Ordinal)
                        .IsTrue())
                    {
                        removeThese.Add(split);
                    }
                }
            }

            var joined = string.Join(" ", splits);

            if (removeThese.IsNotNullOrEmpty())
            {
                foreach (var stringItem in removeThese)
                {
                    joined = joined.Replace(stringItem, string.Empty, StringComparison.Ordinal);
                }
            }

            evalThis = joined;
        }

        var singleSpaced = evalThis.ToSingleSpacedString()
            .Trim();

        return singleSpaced;
    }

    private static string RemoveAccessorTypes(string scannedLine)
    {
        var evalString = scannedLine;
        var accessorList = new[]
        {
            "private", "public", "internal",
            "private protected", "private internal", "protected"
        };

        var accessListMassage = string.Join(" ", accessorList);
        var accessorListMicroSplit = accessListMassage.Split(" ")
            .ToList();

        return RemoveWordsFromString(evalString, accessorListMicroSplit);
    }

    private static string RemoveGetterSetterString(string scannedLine)
    {
        var evalString = scannedLine;
        var cleaned = evalString.Replace("{", string.Empty)
            .Replace("}", string.Empty)
            .Replace("get;", string.Empty);

        var types = GetterSetterType.List.Select(_ => _.SetterStringWithSemiColon)
            .ToList();

        foreach (var type in types)
        {
            cleaned = cleaned.Replace(type, string.Empty);
        }

        return cleaned.Trim();
    }

    private static string RemoveModifierTypes(string scannedLine)
    {
        var enumModifierKeywords = DotNetModifierKeyword.List.Select(_ => _.Name)
            .ToList();

        return RemoveWordsFromString(scannedLine, enumModifierKeywords);
    }

    /// <summary>   An array enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ArrayEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ArrayEnum" /> class.
        ///     Initializes a new instance of the ArrayEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ArrayEnum()
            : base("Array", 1)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   An array list enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ArrayListEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ArrayListEnum" /> class.
        ///     Initializes a new instance of the
        ///     ArrayListEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ArrayListEnum()
            : base("ArrayList", 2)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   An array pool enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ArrayPoolEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ArrayPoolEnum" /> class.
        ///     Initializes a new instance of the
        ///     ArrayPoolEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ArrayPoolEnum()
            : base("ArrayPool", 3)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A collection base enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class CollectionBaseEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.CollectionBaseEnum" /> class.
        ///     Initializes a new instance of the
        ///     CollectionBaseEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public CollectionBaseEnum()
            : base("CollectionBase", 5)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A collection enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class CollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.CollectionEnum" /> class.
        ///     Initializes a new instance of the
        ///     CollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public CollectionEnum()
            : base("Collection", 4)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A collection interface enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class CollectionInterfaceEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.CollectionInterfaceEnum" />
        ///     class.
        ///     Initializes a new instance of the
        ///     CollectionInterfaceEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public CollectionInterfaceEnum()
            : base("ICollection", 6)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A concurrent queue enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ConcurrentQueueEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ConcurrentQueueEnum" />
        ///     class.
        ///     Initializes a new instance of the
        ///     ConcurrentQueueEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ConcurrentQueueEnum()
            : base("ConcurrentQueue", 7)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    private sealed class CustomUnknownType : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.CustomUnknownType" /> class.
        ///     Initializes a new instance of the
        ///     TripleArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public CustomUnknownType()
            : base("custom", 24)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    private sealed class DbSetCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.DbSetCollectionEnum" />
        ///     class.
        ///     Initializes a new instance of the
        ///     TripleArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public DbSetCollectionEnum()
            : base("DbSet", 23)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A dictionary collection enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class DictionaryCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.DictionaryCollectionEnum" />
        ///     class.
        ///     Initializes a new instance of the
        ///     DictionaryCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public DictionaryCollectionEnum()
            : base("Dictionary", 8)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>
    ///     A dictionary interface collection enum. This class cannot be inherited.
    /// </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class DictionaryInterfaceCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DotNetCollectionType.DictionaryInterfaceCollectionEnum" /> class.
        ///     Initializes a new instance of the
        ///     DictionaryInterfaceCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public DictionaryInterfaceCollectionEnum()
            : base("IDictionary", 9)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A double array enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class DoubleArrayEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.DoubleArrayEnum" /> class.
        ///     Initializes a new instance of the
        ///     DoubleArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public DoubleArrayEnum()
            : base("[][]", 10)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   An extension collection enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ExtensionCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ExtensionCollectionEnum" />
        ///     class.
        ///     Initializes a new instance of the
        ///     ExtensionCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ExtensionCollectionEnum()
            : base("ExtensionCollection", 11)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>
    ///     An extension interface collection enum. This class cannot be inherited.
    /// </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ExtensionInterfaceCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ExtensionInterfaceCollectionEnum" /> class.
        ///     Initializes a new instance of the
        ///     ExtensionInterfaceCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ExtensionInterfaceCollectionEnum()
            : base("IExtensionCollection", 12)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A stack enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class IReadOnlyListEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.IReadOnlyListEnum" /> class.
        ///     Initializes a new instance of the StackEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public IReadOnlyListEnum()
            : base("IReadOnlyList", 27)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A linked list enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class LinkedListEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.LinkedListEnum" /> class.
        ///     Initializes a new instance of the
        ///     LinkedListEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public LinkedListEnum()
            : base("LinkedList", 13)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A list collection enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ListCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ListCollectionEnum" /> class.
        ///     Initializes a new instance of the
        ///     ListCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ListCollectionEnum()
            : base("List", 14)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A list interface collection enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ListInterfaceCollectionEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ListInterfaceCollectionEnum" /> class.
        ///     Initializes a new instance of the
        ///     ListInterfaceCollectionEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ListInterfaceCollectionEnum()
            : base("IList", 15)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A none enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class NoneEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.NoneEnum" /> class.
        ///     Initializes a new instance of the NoneEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public NoneEnum()
            : base("none", 23)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A quad array enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class QuadArrayEnum : DotNetCollectionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetCollectionType.QuadArrayEnum"/> class.
        ///     Initializes a new instance of the
        ///     QuadArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public QuadArrayEnum()
            : base("[][][][]", 16)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A queue enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class QueueEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.QueueEnum" /> class.
        ///     Initializes a new instance of the QueueEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public QueueEnum()
            : base("Queue", 17)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A read only collection base enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ReadOnlyCollectionBaseEnum : DotNetCollectionType
    {
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ReadOnlyCollectionBaseEnum()
            : base("ReadOnlyCollectionBase", 18)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A read only collection interface enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ReadOnlyCollectionInterfaceEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DotNetCollectionType.ReadOnlyCollectionInterfaceEnum" /> class.
        ///     Initializes a new instance of the
        ///     ReadOnlyCollectionInterfaceEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ReadOnlyCollectionInterfaceEnum()
            : base("IReadOnlyCollection", 19)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => true;
    }

    /// <summary>   A stack enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class ReadOnlyListEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.ReadOnlyListEnum" /> class.
        ///     Initializes a new instance of the StackEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public ReadOnlyListEnum()
            : base("ReadOnlyList", 26)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A single array enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class SingleArrayEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.SingleArrayEnum" /> class.
        ///     Initializes a new instance of the
        ///     SingleArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public SingleArrayEnum()
            : base("[]", 20)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A stack enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class StackEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.StackEnum" /> class.
        ///     Initializes a new instance of the StackEnum
        ///     class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public StackEnum()
            : base("Stack", 21)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    /// <summary>   A triple array enum. This class cannot be inherited. </summary>
    /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
    private sealed class TripleArrayEnum : DotNetCollectionType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCollectionType.TripleArrayEnum" /> class.
        ///     Initializes a new instance of the
        ///     TripleArrayEnum class.
        /// </summary>
        /// <remarks>   Jeff Ward, 9/9/2021. </remarks>
        public TripleArrayEnum()
            : base("[][][]", 22)
        {
        }

        /// <summary>   Gets a value indicating whether this object is interface. </summary>
        /// <value> True if this object is interface, false if not. </value>
        public override bool IsInterface => false;
    }

    private sealed class UndefinedListType : DotNetCollectionType
    {
        public UndefinedListType()
            : base("undefined", 28)
        {
        }

        public override bool IsInterface => false;
    }
}
