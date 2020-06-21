﻿using System.Collections.Generic;

namespace DIMS_Core.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<string> enumerable, string separator) => string.Join(separator, enumerable);
    }
}