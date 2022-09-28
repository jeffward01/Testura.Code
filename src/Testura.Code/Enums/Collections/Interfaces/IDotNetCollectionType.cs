namespace Testura.Code.Enums.Collections.Interfaces;

public interface IDotNetCollectionType
{
    /// <summary>   Gets a value indicating whether this object is interface. </summary>
    /// <value> True if this object is interface, false if not. </value>
    bool IsInterface { get; }

    string Name { get; }

    int Value { get; }

    DotNetCollectionType GetCollectionType(string collectionTypeEval);
}
