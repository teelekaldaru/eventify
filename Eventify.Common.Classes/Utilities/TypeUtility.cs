﻿using System.Collections.Generic;

namespace Eventify.Common.Classes.Utilities
{
    public static class TypeUtility
    {
        public static bool IsNullOrDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default!);
        }
    }
}
