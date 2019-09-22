
using System;

namespace DominicanWhoCodes.Shared.Domain
{
    public static class FieldChecker
    {
        public static string NotEmpty(string value, string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(propertyName));
            return value;
        }
    }
}
