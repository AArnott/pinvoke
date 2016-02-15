﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Collections.Generic;
    using CLRMetaHost;

    public static class IEnumUnknownExtensions
    {
        public static IEnumerator<object> GetEnumerator(this IEnumUnknown enumerator)
        {
            if (enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            uint count;
            do
            {
                object element;
                enumerator.RemoteNext(1, out element, out count);
                if (count == 1)
                {
                    yield return element;
                }
            }
            while (count > 0);
        }

        public static IEnumerable<T> Cast<T>(this IEnumUnknown enumerator)
        {
            var e = enumerator.GetEnumerator();
            while (e.MoveNext())
            {
                yield return (T)e.Current;
            }
        }
    }
}