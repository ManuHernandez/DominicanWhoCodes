using System;
using System.Collections;
using System.Reflection;

namespace DominicanWhoCodes.Shared.Domain
{
    // Reference: Hands on Domain Driven Design with .Net Core Book
    public struct Member
    {
        public readonly string Name;
        public readonly Func<object, object> GetValue;
        public readonly bool IsNonStringEnumerable;
        public readonly Type Type;

        public Member(MemberInfo info)
        {
            switch (info)
            {
                case FieldInfo field:
                    Name = field.Name;
                    GetValue = obj => field.GetValue(obj);

                    IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(field.FieldType) &&
                                            field.FieldType != typeof(string);
                    Type = field.FieldType;
                    break;
                case PropertyInfo prop:
                    Name = prop.Name;
                    GetValue = obj => prop.GetValue(obj);

                    IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                                            prop.PropertyType != typeof(string);
                    Type = prop.PropertyType;
                    break;
                default:
                    throw new ArgumentException("Member is not a field or property.", info.Name);
            }
        }
    }
}
