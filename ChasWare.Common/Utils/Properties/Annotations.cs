﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Annotations.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming
namespace ChasWare.Common.Utils.Properties
{
    /// <summary>
    ///     Indicates that the value of the marked element could be <c>null</c> sometimes,
    ///     so the check for <c>null</c> is necessary before its usage.
    /// </summary>
    /// <example>
    ///     <code>
    /// [CanBeNull] object Test() => null;
    /// 
    /// void UseTest() {
    ///   var p = Test();
    ///   var s = p.ToString(); // Warning: Possible 'System.NullReferenceException'
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that the value of the marked element could never be <c>null</c>.
    /// </summary>
    /// <example>
    ///     <code>
    /// [NotNull] object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    /// <summary>
    ///     Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
    ///     and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    ///     or of the Lazy.Value property can never be null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class ItemNotNullAttribute : Attribute
    {
    }

    /// <summary>
    ///     Can be appplied to symbols of types derived from IEnumerable as well as to symbols of Task
    ///     and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    ///     or of the Lazy.Value property can be null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class ItemCanBeNullAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that the marked method builds string by format pattern and (optional) arguments.
    ///     Parameter, which contains format string, should be given in constructor. The format string
    ///     should be in <see cref="string.Format(IFormatProvider,string,object[])" />-like form.
    /// </summary>
    /// <example>
    ///     <code>
    /// [StringFormatMethod("message")]
    /// void ShowError(string message, params object[] args) { /* do something */ }
    /// 
    /// void Foo() {
    ///   ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringFormatMethodAttribute" /> class.
        ///     The string format method attribute.
        /// </summary>
        /// <param name="formatParameterName">
        ///     Specifies which parameter of an annotated method should be treated as format-string
        /// </param>
        public StringFormatMethodAttribute([NotNull] string formatParameterName)
        {
            FormatParameterName = formatParameterName;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format parameter name.
        /// </summary>
        [NotNull]
        public string FormatParameterName { get; }

        #endregion
    }

    /// <summary>
    ///     For a parameter that is expected to be one of the limited set of values.
    ///     Specify fields of which type should be used as values for this parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class ValueProviderAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueProviderAttribute" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public ValueProviderAttribute([NotNull] string name)
        {
            Name = name;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [NotNull]
        public string Name { get; }

        #endregion
    }

    /// <summary>
    ///     Indicates that the function argument should be string literal and match one
    ///     of the parameters of the caller function. For example, ReSharper annotates
    ///     the parameter of <see cref="System.ArgumentNullException" />.
    /// </summary>
    /// <example>
    ///     <code>
    /// void Foo(string param) {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InvokerParameterNameAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that the method is contained in a type that implements
    ///     <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
    ///     is used to notify that some property value changed.
    /// </summary>
    /// <remarks>
    ///     The method should be non-static and conform to one of the supported signatures:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged(string)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(params string[])</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T}(Expression{Func{T}})</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T,U}(Expression{Func{T,U}})</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty{T}(ref T, T, string)</c>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <example>
    ///     <code>
    ///  public class Foo : INotifyPropertyChanged {
    ///    public event PropertyChangedEventHandler PropertyChanged;
    ///  
    ///    [NotifyPropertyChangedInvocator]
    ///    protected virtual void NotifyChanged(string propertyName) { ... }
    /// 
    ///    string _name;
    ///  
    ///    public string Name {
    ///      get { return _name; }
    ///      set { _name = value; NotifyChanged("LastName"); /* Warning */ }
    ///    }
    ///  }
    ///  </code>
    ///     Examples of generated notifications:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged("Property")</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(() =&gt; Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged((VM x) =&gt; x.Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty(ref myField, value, "Property")</c>
    ///         </item>
    ///     </list>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyPropertyChangedInvocatorAttribute" /> class.
        /// </summary>
        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyPropertyChangedInvocatorAttribute" /> class.
        /// </summary>
        /// <param name="parameterName">
        ///     The parameter name.
        /// </param>
        public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
        {
            ParameterName = parameterName;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the parameter name.
        /// </summary>
        [CanBeNull]
        public string ParameterName { get; }

        #endregion
    }

    /// <summary>
    ///     Describes dependency between method input and output.
    /// </summary>
    /// <syntax>
    ///     <p>Function Definition Table syntax:</p>
    ///     <list>
    ///         <item>FDT      ::= FDTRow [;FDTRow]*</item>
    ///         <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
    ///         <item>Input    ::= ParameterName: Value [, Input]*</item>
    ///         <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
    ///         <item>Value    ::= true | false | null | notnull | canbenull</item>
    ///     </list>
    ///     If method has single input parameter, it's name could be omitted.<br />
    ///     Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same) for method output
    ///     means that the methos doesn't return normally (throws or terminates the process).<br />
    ///     Value <c>canbenull</c> is only applicable for output parameters.<br />
    ///     You can use multiple <c>[ContractAnnotation]</c> for each FDT row, or use single attribute
    ///     with rows separated by semicolon. There is no notion of order rows, all rows are checked
    ///     for applicability and applied per each program state tracked by R# analysis.<br />
    /// </syntax>
    /// <examples>
    ///     <list>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("=&gt; halt")]
    /// public void TerminationMethod()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("halt &lt;= condition: false")]
    /// public void Assert(bool condition, string text) // regular assertion method
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("s:null =&gt; true")]
    /// public bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// // A method that returns null if the parameter is null,
    /// // and not null if the parameter is not null
    /// [ContractAnnotation("null =&gt; null; notnull =&gt; notnull")]
    /// public object Transform(object data) 
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("=&gt; true, result: notnull; =&gt; false, result: null")]
    /// public bool TryParse(string s, out Person result)
    /// </code>
    ///         </item>
    ///     </list>
    /// </examples>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ContractAnnotationAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContractAnnotationAttribute" /> class.
        /// </summary>
        /// <param name="contract">
        ///     The contract.
        /// </param>
        public ContractAnnotationAttribute([NotNull] string contract)
            : this(contract, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContractAnnotationAttribute" /> class.
        /// </summary>
        /// <param name="contract">
        ///     The contract.
        /// </param>
        /// <param name="forceFullStates">
        ///     The force full states.
        /// </param>
        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the contract.
        /// </summary>
        [NotNull]
        public string Contract { get; }

        /// <summary>
        ///     Gets a value indicating whether force full states.
        /// </summary>
        public bool ForceFullStates { get; }

        #endregion
    }

    /// <summary>
    ///     Indicates that marked element should be localized or not.
    /// </summary>
    /// <example>
    ///     <code>
    /// [LocalizationRequiredAttribute(true)]
    /// class Foo {
    ///   string str = "my string"; // Warning: Localizable string
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizationRequiredAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LocalizationRequiredAttribute" /> class.
        /// </summary>
        public LocalizationRequiredAttribute()
            : this(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LocalizationRequiredAttribute" /> class.
        /// </summary>
        /// <param name="required">
        ///     The required.
        /// </param>
        public LocalizationRequiredAttribute(bool required)
        {
            Required = required;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets a value indicating whether required.
        /// </summary>
        public bool Required { get; }

        #endregion
    }

    /// <summary>
    ///     Indicates that the value of the marked type (or its derivatives)
    ///     cannot be compared using '==' or '!=' operators and <c>Equals()</c>
    ///     should be used instead. However, using '==' or '!=' for comparison
    ///     with <c>null</c> is always permitted.
    /// </summary>
    /// <example>
    ///     <code>
    /// [CannotApplyEqualityOperator]
    /// class NoEquality { }
    /// 
    /// class UsesNoEquality {
    ///   void Test() {
    ///     var ca1 = new NoEquality();
    ///     var ca2 = new NoEquality();
    ///     if (ca1 != null) { // OK
    ///       bool condition = ca1 == ca2; // Warning
    ///     }
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class CannotApplyEqualityOperatorAttribute : Attribute
    {
    }

    /// <summary>
    ///     When applied to a target attribute, specifies a requirement for any type marked
    ///     with the target attribute to implement or inherit specific type or types.
    /// </summary>
    /// <example>
    ///     <code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// class ComponentAttribute : Attribute { }
    /// 
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// class MyComponent : IComponent { }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true), BaseTypeRequired(typeof(Attribute))]
    public sealed class BaseTypeRequiredAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseTypeRequiredAttribute" /> class.
        /// </summary>
        /// <param name="baseType">
        ///     The base type.
        /// </param>
        public BaseTypeRequiredAttribute([NotNull] Type baseType)
        {
            BaseType = baseType;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the base type.
        /// </summary>
        [NotNull]
        public Type BaseType { get; }

        #endregion
    }

    /// <summary>
    ///     Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
    ///     so this symbol will not be marked as unused (as well as by other usage inspections).
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">
        ///     The use kind flags.
        /// </param>
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="targetFlags">
        ///     The target flags.
        /// </param>
        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsedImplicitlyAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">
        ///     The use kind flags.
        /// </param>
        /// <param name="targetFlags">
        ///     The target flags.
        /// </param>
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the target flags.
        /// </summary>
        public ImplicitUseTargetFlags TargetFlags { get; }

        /// <summary>
        ///     Gets the use kind flags.
        /// </summary>
        public ImplicitUseKindFlags UseKindFlags { get; }

        #endregion
    }

    /// <summary>
    ///     Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes
    ///     as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">
        ///     The use kind flags.
        /// </param>
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="targetFlags">
        ///     The target flags.
        /// </param>
        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MeansImplicitUseAttribute" /> class.
        /// </summary>
        /// <param name="useKindFlags">
        ///     The use kind flags.
        /// </param>
        /// <param name="targetFlags">
        ///     The target flags.
        /// </param>
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the target flags.
        /// </summary>
        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; }

        /// <summary>
        ///     Gets the use kind flags.
        /// </summary>
        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; }

        #endregion
    }

    /// <summary>
    ///     The implicit use kind flags.
    /// </summary>
    [Flags]
    public enum ImplicitUseKindFlags
    {
        /// <summary>
        ///     The default.
        /// </summary>
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,

        /// <summary>Only entity marked with attribute considered used.</summary>
        Access = 1,

        /// <summary>Indicates implicit assignment to a member.</summary>
        Assign = 2,

        /// <summary>
        ///     Indicates implicit instantiation of a type with fixed constructor signature.
        ///     That means any unused constructor parameters won't be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,

        /// <summary>Indicates implicit instantiation of a type.</summary>
        InstantiatedNoFixedConstructorSignature = 8
    }

    /// <summary>
    ///     Specify what is considered used implicitly when marked
    ///     with <see cref="MeansImplicitUseAttribute" /> or <see cref="UsedImplicitlyAttribute" />.
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        /// <summary>
        ///     The default.
        /// </summary>
        Default = Itself,

        /// <summary>
        ///     The itself.
        /// </summary>
        Itself = 1,

        /// <summary>Members of entity marked with attribute are considered used.</summary>
        Members = 2,

        /// <summary>Entity marked with attribute and all its members considered used.</summary>
        WithMembers = Itself | Members
    }

    /// <summary>
    ///     This attribute is intended to mark publicly available API
    ///     which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class PublicAPIAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicAPIAttribute" /> class.
        /// </summary>
        public PublicAPIAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicAPIAttribute" /> class.
        /// </summary>
        /// <param name="comment">
        ///     The comment.
        /// </param>
        public PublicAPIAttribute([NotNull] string comment)
        {
            Comment = comment;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the comment.
        /// </summary>
        [CanBeNull]
        public string Comment { get; }

        #endregion
    }

    /// <summary>
    ///     Tells code analysis engine if the parameter is completely handled when the invoked method is on stack.
    ///     If the parameter is a delegate, indicates that delegate is executed while the method is executed.
    ///     If the parameter is an enumerable, indicates that it is enumerated while the method is executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InstantHandleAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that a method does not make any observable state changes.
    ///     The same as <c>System.Diagnostics.Contracts.PureAttribute</c>.
    /// </summary>
    /// <example>
    ///     <code>
    /// [Pure] int Multiply(int x, int y) => x * y;
    /// 
    /// void M() {
    ///   Multiply(123, 42); // Waring: Return value of pure method is not used
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PureAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that the return value of method invocation must be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MustUseReturnValueAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MustUseReturnValueAttribute" /> class.
        /// </summary>
        public MustUseReturnValueAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MustUseReturnValueAttribute" /> class.
        /// </summary>
        /// <param name="justification">
        ///     The justification.
        /// </param>
        public MustUseReturnValueAttribute([NotNull] string justification)
        {
            Justification = justification;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the justification.
        /// </summary>
        [CanBeNull]
        public string Justification { get; }

        #endregion
    }

    /// <summary>
    ///     Indicates the type member or parameter of some type, that should be used instead of all other ways
    ///     to get the value that type. This annotation is useful when you have some "context" value evaluated
    ///     and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.
    /// </summary>
    /// <example>
    ///     <code>
    /// class Foo {
    ///   [ProvidesContext] IBarService _barService = ...;
    /// 
    ///   void ProcessNode(INode node) {
    ///     DoSomething(node, node.GetGlobalServices().Bar);
    ///     //              ^ Warning: use value of '_barService' field
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.GenericParameter)]
    public sealed class ProvidesContextAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that a parameter is a path to a file or a folder within a web project.
    ///     Path can be relative or absolute, starting from web root (~).
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class PathReferenceAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PathReferenceAttribute" /> class.
        /// </summary>
        public PathReferenceAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PathReferenceAttribute" /> class.
        /// </summary>
        /// <param name="basePath">
        ///     The base path.
        /// </param>
        public PathReferenceAttribute([NotNull, PathReference] string basePath)
        {
            BasePath = basePath;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the base path.
        /// </summary>
        [CanBeNull]
        public string BasePath { get; }

        #endregion
    }

    /// <summary>
    ///     An extension method marked with this attribute is processed by ReSharper code completion
    ///     as a 'Source Template'. When extension method is completed over some expression, it's source code
    ///     is automatically expanded like a template at call site.
    /// </summary>
    /// <remarks>
    ///     Template method body can contain valid source code and/or special comments starting with '$'.
    ///     Text inside these comments is added as source code when the template is applied. Template parameters
    ///     can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
    ///     Use the <see cref="MacroAttribute" /> attribute to specify macros for parameters.
    /// </remarks>
    /// <example>
    ///     In this example, the 'forEach' method is a source template available over all values
    ///     of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:
    ///     <code>
    /// [SourceTemplate]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
    ///   foreach (var x in xs) {
    ///      //$ $END$
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SourceTemplateAttribute : Attribute
    {
    }

    /// <summary>
    ///     Allows specifying a macro for a parameter of a <see cref="SourceTemplateAttribute">source template</see>.
    /// </summary>
    /// <remarks>
    ///     You can apply the attribute on the whole method or on any of its additional parameters. The macro expression
    ///     is defined in the <see cref="MacroAttribute.Expression" /> property. When applied on a method, the target
    ///     template parameter is defined in the <see cref="MacroAttribute.Target" /> property. To apply the macro silently
    ///     for the parameter, set the <see cref="MacroAttribute.Editable" /> property value = -1.
    /// </remarks>
    /// <example>
    ///     Applying the attribute on a source template method:
    ///     <code>
    /// [SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
    ///   foreach (var item in collection) {
    ///     //$ $END$
    ///   }
    /// }
    /// </code>
    ///     Applying the attribute on a template method parameter:
    ///     <code>
    /// [SourceTemplate]
    /// public static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
    ///   /*$ var $x$Id = "$newguid$" + x.ToString();
    ///   x.DoSomething($x$Id); */
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MacroAttribute : Attribute
    {
        #region public properties

        /// <summary>
        ///     Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.
        /// </summary>
        /// <remarks>
        ///     If the target parameter is used several times in the template, only one occurrence becomes editable;
        ///     other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
        ///     use values >= 0. To make the parameter non-editable when the template is expanded, use -1.
        /// </remarks>
        /// >
        public int Editable { get; set; }

        /// <summary>
        ///     Allows specifying a macro that will be executed for a <see cref="SourceTemplateAttribute">source template</see>
        ///     parameter when the template is expanded.
        /// </summary>
        [CanBeNull]
        public string Expression { get; set; }

        /// <summary>
        ///     Identifies the target parameter of a <see cref="SourceTemplateAttribute">source template</see> if the
        ///     <see cref="MacroAttribute" /> is applied on a template method.
        /// </summary>
        [CanBeNull]
        public string Target { get; set; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc area master location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcAreaMasterLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc area partial view location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcAreaPartialViewLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc area view location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcAreaViewLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc master location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcMasterLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcMasterLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc partial view location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcPartialViewLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     The asp mvc view location format attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcViewLocationFormatAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcViewLocationFormatAttribute" /> class.
        /// </summary>
        /// <param name="format">
        ///     The format.
        /// </param>
        public AspMvcViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the format.
        /// </summary>
        [NotNull]
        public string Format { get; }

        #endregion
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC action. If applied to a method, the MVC action name is calculated
    ///     implicitly from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcActionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcActionAttribute" /> class.
        /// </summary>
        public AspMvcActionAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcActionAttribute" /> class.
        /// </summary>
        /// <param name="anonymousProperty">
        ///     The anonymous property.
        /// </param>
        public AspMvcActionAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the anonymous property.
        /// </summary>
        [CanBeNull]
        public string AnonymousProperty { get; }

        #endregion
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcAreaAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcAreaAttribute" /> class.
        /// </summary>
        public AspMvcAreaAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcAreaAttribute" /> class.
        /// </summary>
        /// <param name="anonymousProperty">
        ///     The anonymous property.
        /// </param>
        public AspMvcAreaAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the anonymous property.
        /// </summary>
        [CanBeNull]
        public string AnonymousProperty { get; }

        #endregion
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
    ///     an MVC controller. If applied to a method, the MVC controller name is calculated
    ///     implicitly from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcControllerAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcControllerAttribute" /> class.
        /// </summary>
        public AspMvcControllerAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspMvcControllerAttribute" /> class.
        /// </summary>
        /// <param name="anonymousProperty">
        ///     The anonymous property.
        /// </param>
        public AspMvcControllerAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the anonymous property.
        /// </summary>
        [CanBeNull]
        public string AnonymousProperty { get; }

        #endregion
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute
    ///     for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcMasterAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute
    ///     for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcModelTypeAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
    ///     partial view. If applied to a method, the MVC partial view name is calculated implicitly
    ///     from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcPartialViewAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AspMvcSuppressViewErrorAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcDisplayTemplateAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcEditorTemplateAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcTemplateAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
    ///     from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Controller.View(Object)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AspMvcViewComponentAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component view. If applied to a method, the MVC view component view name is default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewComponentViewAttribute : Attribute
    {
    }

    /// <summary>
    ///     ASP.NET MVC attribute. When applied to a parameter of an attribute,
    ///     indicates that this parameter is an MVC action name.
    /// </summary>
    /// <example>
    ///     <code>
    /// [ActionName("Foo")]
    /// public ActionResult Login(string returnUrl) {
    ///   ViewBag.ReturnUrl = Url.Action("Foo"); // OK
    ///   return RedirectToAction("Bar"); // Error: Cannot resolve action
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public sealed class AspMvcActionSelectorAttribute : Attribute
    {
    }

    /// <summary>
    ///     The html element attributes attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HtmlElementAttributesAttribute" /> class.
        /// </summary>
        public HtmlElementAttributesAttribute()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HtmlElementAttributesAttribute" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            Name = name;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [CanBeNull]
        public string Name { get; }

        #endregion
    }

    /// <summary>
    ///     The html attribute value attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class HtmlAttributeValueAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HtmlAttributeValueAttribute" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            Name = name;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [NotNull]
        public string Name { get; }

        #endregion
    }

    /// <summary>
    ///     Razor attribute. Indicates that a parameter or a method is a Razor section.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class RazorSectionAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates how method, constructor invocation or property access
    ///     over collection type affects content of the collection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property)]
    public sealed class CollectionAccessAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CollectionAccessAttribute" /> class.
        /// </summary>
        /// <param name="collectionAccessType">
        ///     The collection access type.
        /// </param>
        public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
        {
            CollectionAccessType = collectionAccessType;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the collection access type.
        /// </summary>
        public CollectionAccessType CollectionAccessType { get; }

        #endregion
    }

    /// <summary>
    ///     The collection access type.
    /// </summary>
    [Flags]
    public enum CollectionAccessType
    {
        /// <summary>Method does not use or modify content of the collection.</summary>
        None = 0,

        /// <summary>Method only reads content of the collection but does not modify it.</summary>
        Read = 1,

        /// <summary>Method can change content of the collection but does not add new elements.</summary>
        ModifyExistingContent = 2,

        /// <summary>Method can add new elements to the collection.</summary>
        UpdatedContent = ModifyExistingContent | 4
    }

    /// <summary>
    ///     Indicates that the marked method is assertion method, i.e. it halts control flow if
    ///     one of the conditions is satisfied. To set the condition, mark one of the parameters with
    ///     <see cref="AssertionConditionAttribute" /> attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates the condition parameter of the assertion method. The method itself should be
    ///     marked by <see cref="AssertionMethodAttribute" /> attribute. The mandatory argument of
    ///     the attribute is the assertion type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssertionConditionAttribute" /> class.
        /// </summary>
        /// <param name="conditionType">
        ///     The condition type.
        /// </param>
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the condition type.
        /// </summary>
        public AssertionConditionType ConditionType { get; }

        #endregion
    }

    /// <summary>
    ///     Specifies assertion type. If the assertion method argument satisfies the condition,
    ///     then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,

        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,

        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,

        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3
    }

    /// <summary>
    ///     Indicates that the marked method unconditionally terminates control flow execution.
    ///     For example, it could unconditionally throw exception.
    /// </summary>
    [Obsolete("Use [ContractAnnotation('=> halt')] instead"), AttributeUsage(AttributeTargets.Method)]
    public sealed class TerminatesProgramAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that method is pure LINQ method, with postponed enumeration (like Enumerable.Select,
    ///     .Where). This annotation allows inference of [InstantHandle] annotation for parameters
    ///     of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LinqTunnelAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that IEnumerable, passed as parameter, is not enumerated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class NoEnumerationAttribute : Attribute
    {
    }

    /// <summary>
    ///     Indicates that parameter is regular expression pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RegexPatternAttribute : Attribute
    {
    }

    /// <summary>
    ///     Prevents the Member Reordering feature from tossing members of the marked class.
    /// </summary>
    /// <remarks>
    ///     The attribute must be mentioned in your member reordering patterns
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum)]
    public sealed class NoReorderAttribute : Attribute
    {
    }

    /// <summary>
    ///     XAML attribute. Indicates the type that has <c>ItemsSource</c> property and should be treated
    ///     as <c>ItemsControl</c>-derived type, to enable inner items <c>DataContext</c> type resolve.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class XamlItemsControlAttribute : Attribute
    {
    }

    /// <summary>
    ///     XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
    ///     is used to bind some item of <c>ItemsControl</c>-derived type. This annotation will
    ///     enable the <c>DataContext</c> type resolve for XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    ///     Property should have the tree ancestor of the <c>ItemsControl</c> type or
    ///     marked with the <see cref="XamlItemsControlAttribute" /> attribute.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class XamlItemBindingOfItemsControlAttribute : Attribute
    {
    }

    /// <summary>
    ///     The asp child control type attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspChildControlTypeAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspChildControlTypeAttribute" /> class.
        /// </summary>
        /// <param name="tagName">
        ///     The tag name.
        /// </param>
        /// <param name="controlType">
        ///     The control type.
        /// </param>
        public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
        {
            TagName = tagName;
            ControlType = controlType;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the control type.
        /// </summary>
        [NotNull]
        public Type ControlType { get; }

        /// <summary>
        ///     Gets the tag name.
        /// </summary>
        [NotNull]
        public string TagName { get; }

        #endregion
    }

    /// <summary>
    ///     The asp data field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class AspDataFieldAttribute : Attribute
    {
    }

    /// <summary>
    ///     The asp data fields attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class AspDataFieldsAttribute : Attribute
    {
    }

    /// <summary>
    ///     The asp method property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AspMethodPropertyAttribute : Attribute
    {
    }

    /// <summary>
    ///     The asp required attribute attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspRequiredAttributeAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspRequiredAttributeAttribute" /> class.
        /// </summary>
        /// <param name="attribute">
        ///     The attribute.
        /// </param>
        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the attribute.
        /// </summary>
        [NotNull]
        public string Attribute { get; }

        #endregion
    }

    /// <summary>
    ///     The asp type property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AspTypePropertyAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspTypePropertyAttribute" /> class.
        /// </summary>
        /// <param name="createConstructorReferences">
        ///     The create constructor references.
        /// </param>
        public AspTypePropertyAttribute(bool createConstructorReferences)
        {
            CreateConstructorReferences = createConstructorReferences;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets a value indicating whether create constructor references.
        /// </summary>
        public bool CreateConstructorReferences { get; }

        #endregion
    }

    /// <summary>
    ///     The razor import namespace attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorImportNamespaceAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RazorImportNamespaceAttribute" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public RazorImportNamespaceAttribute([NotNull] string name)
        {
            Name = name;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [NotNull]
        public string Name { get; }

        #endregion
    }

    /// <summary>
    ///     The razor injection attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorInjectionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RazorInjectionAttribute" /> class.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="fieldName">
        ///     The field name.
        /// </param>
        public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
        {
            Type = type;
            FieldName = fieldName;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the field name.
        /// </summary>
        [NotNull]
        public string FieldName { get; }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        [NotNull]
        public string Type { get; }

        #endregion
    }

    /// <summary>
    ///     The razor directive attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorDirectiveAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RazorDirectiveAttribute" /> class.
        /// </summary>
        /// <param name="directive">
        ///     The directive.
        /// </param>
        public RazorDirectiveAttribute([NotNull] string directive)
        {
            Directive = directive;
        }

        #endregion

        #region public properties

        /// <summary>
        ///     Gets the directive.
        /// </summary>
        [NotNull]
        public string Directive { get; }

        #endregion
    }

    /// <summary>
    ///     The razor helper common attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorHelperCommonAttribute : Attribute
    {
    }

    /// <summary>
    ///     The razor layout attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RazorLayoutAttribute : Attribute
    {
    }

    /// <summary>
    ///     The razor write literal method attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorWriteLiteralMethodAttribute : Attribute
    {
    }

    /// <summary>
    ///     The razor write method attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RazorWriteMethodAttribute : Attribute
    {
    }

    /// <summary>
    ///     The razor write method parameter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RazorWriteMethodParameterAttribute : Attribute
    {
    }
}