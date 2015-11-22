﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Runtime.InteropServices;
	using static Gdi32;
	[System.Runtime.CompilerServices.CompilerGenerated]
	public class Gdi32Mockable : IGdi32Mockable {        [System.Runtime.CompilerServices.CompilerGenerated]
		public bool InvokeDeleteObject(IntPtr hObject)
			=> DeleteObject(hObject);
	}
}
