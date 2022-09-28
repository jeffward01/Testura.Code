namespace Testura.Code.Enums.GetterSetters.Interfaces;

public interface IGetterSetterType
{
    /// <summary>  Gets the setter string without semi colon. </summary>
    /// <value>    The setter string without semi colon. </value>
    string SetterStringWithoutSemiColon { get; }

    /// <summary>   Gets the setter string with semi colon. </summary>
    /// <value> The setter string with semi colon. </value>
    string SetterStringWithSemiColon { get; }

    string Name { get; }

    int Value { get; }

    string GetFormattedGetterSetter(string getterString = "get");

    bool IsMatchSetterType(string setterType);
}
