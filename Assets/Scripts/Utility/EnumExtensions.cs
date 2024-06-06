using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class EnumExtensions
    {
        public static List<T> GetAllEnums<T>() where T : struct // With C# 7.3 where T : Enum works
        {
            // Unneeded if you add T : Enum
            if (typeof(T).BaseType != typeof(Enum))
                throw new ArgumentException("T must be an Enum type");

            // The return type of Enum.GetValues is Array but it is effectively int[] per docs
            // This bit converts to int[]
            var values = Enum.GetValues(typeof(T)).Cast<int>().ToArray();

            if (!typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Any()) {
                // We don't have flags so just return the result of GetValues
                return values.Cast<T>().ToList();
            }

            var valuesInverted = values.Select(v => ~v).ToArray();
            var max = values.Aggregate(0, (current, t) => current | t);

            var result = new List<T>();
            for (var i = 0; i <= max; i++) {
                var unaccountedBits = i;
                for (var j = 0; j < valuesInverted.Length; j++) {
                    // This step removes each flag that is set in one of the Enums thus ensuring that an Enum with missing bits won't be passed an int that has those bits set
                    unaccountedBits &= valuesInverted[j];
                    if (unaccountedBits == 0) {
                        result.Add((T)(object)i);
                        break;
                    }
                }
            }

            //Check for zero
            try {
                if (string.IsNullOrEmpty(Enum.GetName(typeof(T), (T)(object)0))) {
                    result.Remove((T)(object)0);
                }
            }
            catch {
                result.Remove((T)(object)0);
            }

            return result;
        }

        public static T GetNextValue<T>(this T value) where T : struct
        {
            var enumValues = (T[])Enum.GetValues(typeof(T));
            var nextIndex = Array.IndexOf(enumValues, value) + 1;
            return enumValues.Length == nextIndex ? enumValues[0] : enumValues[nextIndex];
        }
    }
}