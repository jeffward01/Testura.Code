namespace Testura.Code.Models;

using Generators.Common.Arguments.ArgumentTypes;

/// <summary>
///     Represent a constructor initializer.
/// </summary>
public class ConstructorInitializer
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConstructorInitializer" /> class.
    /// </summary>
    /// <param name="constructorInitializerType">Type of constructor initializer.</param>
    /// <param name="arguments">Constructor initializer arguments.</param>
    public ConstructorInitializer(
        ConstructorInitializerTypes constructorInitializerType, IEnumerable<Argument> arguments)
    {
        ConstructorInitializerType = constructorInitializerType;
        Arguments = arguments;
    }

    /// <summary>
    ///     Gets the constructor initializer type.
    /// </summary>
    public ConstructorInitializerTypes ConstructorInitializerType { get; }

    /// <summary>
    ///     Gets the constructor initializer arguments.
    /// </summary>
    public IEnumerable<Argument> Arguments { get; }
}
