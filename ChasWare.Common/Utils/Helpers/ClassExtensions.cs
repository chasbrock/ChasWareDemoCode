// --------------------------------------------------------------------------------------------------------------------
// <copyright file=ClassExtensions.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChasWare.Common.Utils.Helpers
{
    /// <summary>
    ///     The class extensions.
    /// </summary>
    public static class ClassExtensions
    {
        #region public methods

        /// <summary>
        ///     make a deep copy of source, including new collections.
        /// </summary>
        /// <typeparam name="T">
        ///     type to copy, must have default constructor
        /// </typeparam>
        /// <param name="source">
        ///     the object to copy
        /// </param>
        /// <param name="parent">
        ///     optional parent, will stop copy from trying to build a parent object
        /// </param>
        /// <returns>
        ///     new object
        /// </returns>
        public static T DeepCopy<T>(this T source, object parent = null)
        {
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
            {
                return default(T);
            }

            // create the new instance
            T newInstance = Activator.CreateInstance<T>();
            CopyValues(newInstance, null, source, parent);
            return newInstance;
        }

        /// <summary>
        ///     builds up an object with default values, and populate any child objects in hierarchy.
        ///     Notes:
        ///     1) if property is same type as parent it will not be create (due to terminal recursion).
        ///     2) if property is same type as parent and the name is type name, Owner or Parent
        ///     a reference will be set to parent
        ///     3) Nullable values will not be set
        ///     4) properties with non public setters will be ignored
        ///     5) if Collections are declared using interface List or Dictionary will be used
        /// </summary>
        /// <typeparam name="T">
        ///     type to create, must have default constructor
        /// </typeparam>
        /// <param name="parent">
        ///     optional parent
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        ///     typeof class to build
        /// </returns>
        public static T DeepCreate<T>(object parent = null)
        {
            // check that we have a default constuctor
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
            {
                return default(T);
            }

            // create the new instance
            T instance = Activator.CreateInstance<T>();

            // apply default values to all properties and
            // wire up any parent-to-child links
            ApplyDefaults(instance, parent);

            return instance;
        }

        /// <summary>
        ///     will set default values for all fields
        /// </summary>
        /// <typeparam name="T">
        ///     type of class to initialise
        /// </typeparam>
        /// <param name="instance">
        ///     the object to initialise
        /// </param>
        /// <param name="parent">
        ///     optional parent to ensure that back links are set up proprtly
        /// </param>
        public static void DeepInit<T>(this T instance, object parent = null)
        {
            ApplyDefaults(instance, parent);
        }

        /// <summary>
        ///     The memberwise match.
        /// </summary>
        /// <param name="lhs">
        ///     The left hand side.
        /// </param>
        /// <param name="rhs">
        ///     The right hand side
        /// </param>
        /// <typeparam name="T">
        ///     the type of the objects
        /// </typeparam>
        /// <returns>
        ///     returns true if values match <see cref="bool" />.
        /// </returns>
        public static bool MemberwiseMatch<T>(this T lhs, T rhs)
        {
            PropertyInfo[] sourceProperties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in sourceProperties)
            {
                object left = pi.GetValue(lhs, null);
                object right = pi.GetValue(rhs, null);

                if (left == right)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region other methods

        private static void ApplyDefaults(object instance, object parent)
        {
            const char genericDelim = '`';

            // now populate Properties
            foreach (PropertyInfo info in instance.GetType().GetProperties().Where(p => p.GetSetMethod() != null && !p.GetIndexParameters().Any()))
            {
                // strings
                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(instance, string.Empty);
                    continue;
                }

                // simple ref types (and datetimes etc)
                if (info.PropertyType.IsValueType)
                {
                    // if they are set as nullable respect it
                    if (Nullable.GetUnderlyingType(info.PropertyType) == null)
                    {
                        info.SetValue(instance, Activator.CreateInstance(info.PropertyType));
                    }

                    continue;
                }

                // see if we have a link to parent
                if (info.PropertyType == parent?.GetType())
                {
                    // link back to owner if it has a valid name, otherwise run a mile because
                    // this will recurse like a right bastard
                    if (info.Name.IsOneOf(parent.GetType().Name, "Parent", "Owner"))
                    {
                        info.SetValue(instance, parent);
                    }

                    continue;
                }

                // is the property a concrete class?
                if (!info.PropertyType.IsInterface)
                {
                    object value = Activator.CreateInstance(info.PropertyType);
                    ApplyDefaults(value, instance);
                    info.SetValue(instance, value);
                    continue;
                }

                // is this a generic class, we are only going to deal with lists / dictionaries
                // if yoy absolutely need to keep to a type do not use interace
                if (info.PropertyType.IsGenericType)
                {
                    // extract first part of name to see if we like it
                    string baseClassName = info.PropertyType.UnderlyingSystemType.Name.SubstringUpTo(genericDelim);
                    Type genericType;
                    Type embeddedType;
                    switch (baseClassName)
                    {
                        case "ICollection":
                        case "IEnumerable":
                        case "IList":
                        case "IReadonlyList":
                        case "IReadonlyCollection":
                            embeddedType = info.PropertyType.GenericTypeArguments[0];
                            genericType = typeof(List<>).MakeGenericType(embeddedType);
                            info.SetValue(instance, Activator.CreateInstance(genericType));
                            break;
                        case "ISet":
                            embeddedType = info.PropertyType.GenericTypeArguments[0];
                            genericType = typeof(HashSet<>).MakeGenericType(embeddedType);
                            info.SetValue(instance, Activator.CreateInstance(genericType));
                            break;
                        case "IDictionary":
                            Type embeddedKeyType = info.PropertyType.GenericTypeArguments[0];
                            Type embeddedValueType = info.PropertyType.GenericTypeArguments[1];
                            genericType = typeof(Dictionary<,>).MakeGenericType(embeddedKeyType, embeddedValueType);
                            info.SetValue(instance, Activator.CreateInstance(genericType));
                            break;
                    }
                }
            }
        }

        private static void CopyValues(object newInstance, object newParent, object oldInstance, object oldParent)
        {
            // now populate Properties
            foreach (PropertyInfo info in newInstance.GetType().GetProperties().Where(p => p.GetSetMethod() != null && !p.GetIndexParameters().Any()))
            {
                object oldValue = info.GetValue(oldInstance);
                if (oldValue == null)
                {
                    continue;
                }

                // simple ref types (and datetimes / strings/ etc)
                if (info.PropertyType.IsValueType || info.PropertyType == typeof(string))
                {
                    info.SetValue(newInstance, info.GetValue(oldInstance));
                    continue;
                }

                // see if we have a link to parent
                if (info.PropertyType == oldParent?.GetType())
                {
                    // link back to owner if it has a valid name, otherwise run a mile because
                    // this will recurse like a right bastard
                    if (info.Name.IsOneOf(oldParent.GetType().Name, "Parent", "Owner") && newParent != null)
                    {
                        info.SetValue(newInstance, newParent);
                    }

                    continue;
                }

                // can we treat this as a list
                if (oldValue is IList oldList)
                {
                    IList newList = (IList) Activator.CreateInstance(oldList.GetType());
                    foreach (object oldVal in oldList)
                    {
                        if (oldVal.GetType().IsValueType || oldVal is string)
                        {
                            newList.Add(oldVal);
                        }
                        else
                        {
                            object newVal = Activator.CreateInstance(oldVal.GetType());
                            CopyValues(newVal, newInstance, oldVal, oldInstance);
                            newList.Add(newVal);
                        }
                    }

                    info.SetValue(newInstance, newList);
                    continue;
                }

                // can we treat this as a list
                if (oldValue is IDictionary oldDick)
                {
                    IDictionary newList = (IDictionary) Activator.CreateInstance(oldDick.GetType());
                    foreach (DictionaryEntry oldVal in oldDick)
                    {
                        object key;
                        object value;

                        if (oldVal.Key.GetType().IsValueType || oldVal.Key is string)
                        {
                            key = oldVal.Key;
                        }
                        else
                        {
                            key = Activator.CreateInstance(oldVal.Key.GetType());
                            CopyValues(key, newInstance, oldVal.Key, oldInstance);
                        }

                        if (oldVal.Value.GetType().IsValueType || oldVal.Value is string)
                        {
                            value = oldVal.Value;
                        }
                        else
                        {
                            value = Activator.CreateInstance(oldVal.Value.GetType());
                            CopyValues(value, newInstance, oldVal.Value, oldInstance);
                        }

                        newList.Add(key, value);
                    }

                    info.SetValue(newInstance, newList);
                    continue;
                }

                // should be safe to just copy
                object newValue = Activator.CreateInstance(oldValue.GetType());
                CopyValues(newValue, newInstance, oldValue, oldInstance);
                info.SetValue(newInstance, newValue);
            }
        }

        #endregion
    }
}