﻿// Copyright © .NET Foundation and Contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using FileTime = System.Runtime.InteropServices.ComTypes.FILETIME;

    /// <content>
    /// Contains the <see cref="SP_DRVINFO_DATA" /> nested type.
    /// </content>
    public partial class SetupApi
    {
        /// <summary>
        /// An <see cref="SP_DRVINFO_DATA"/> structure contains information about a driver. This structure is a member of a driver information list
        /// that can be associated with a particular device instance or globally with a device information set.
        /// </summary>
        /// <seealso href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff553287(v=vs.85).aspx"/>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public unsafe struct SP_DRVINFO_DATA
        {
            /// <summary>
            /// The size, in bytes, of the <see cref="SP_DRVINFO_DATA"/> structure.
            /// </summary>
            public int cbSize;

            /// <summary>
            /// The type of driver represented by this structure.
            /// </summary>
            public DriverType DriverType;

            /// <summary>
            /// Reserved. For internal use only.
            /// </summary>
            public UIntPtr Reserved;

            /// <summary>
            /// A <see cref="string"/> that describes the device supported by this driver.
            /// </summary>
            public fixed char Description[LINE_LEN];

            /// <summary>
            /// A <see cref="string"/> that contains the name of the manufacturer of the device supported by this driver.
            /// </summary>
            public fixed char MfgName[LINE_LEN];

            /// <summary>
            /// A <see cref="string"/> giving the provider of this driver. This is typically the name of the organization that
            /// creates the driver or INF file. <see cref="ProviderName"/> can be an empty string.
            /// </summary>
            public fixed char ProviderName[LINE_LEN];

            /// <summary>
            /// Date of the driver. From the <c>DriverVer</c> entry in the INF file.
            /// </summary>
            public FileTime DriverDate;

            /// <summary>
            /// Version of the driver. From the <c>DriverVer</c> entry in the INF file.
            /// </summary>
            public ulong DriverVersion;

            /// <summary>
            /// The length of the strings that are part of this structure.
            /// </summary>
            private const int LINE_LEN = 256;

            /// <summary>
            /// Gets a <see cref="string"/> that describes the device supported by this driver.
            /// </summary>
            public string DescriptionString
            {
                get
                {
                    fixed (char* description = this.Description)
                    {
                        return new string(description);
                    }
                }
            }

            /// <summary>
            /// Gets a <see cref="string"/> that contains the name of the manufacturer of the device supported by this driver.
            /// </summary>
            public string MfgNameString
            {
                get
                {
                    fixed (char* description = this.MfgName)
                    {
                        return new string(description);
                    }
                }
            }

            /// <summary>
            /// Gets a <see cref="string"/> giving the provider of this driver. This is typically the name of the organization that
            /// creates the driver or INF file. <see cref="ProviderNameString"/> can be an empty string.
            /// </summary>
            public string ProviderNameString
            {
                get
                {
                    fixed (char* description = this.ProviderName)
                    {
                        return new string(description);
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="SP_DRVINFO_DATA" /> struct
            /// with <see cref="cbSize" /> set to the correct value.
            /// </summary>
            /// <returns>An instance of <see cref="SP_DRVINFO_DATA"/>.</returns>
            public static SP_DRVINFO_DATA Create()
            {
                return new SP_DRVINFO_DATA
                {
                    cbSize = Marshal.SizeOf(typeof(SP_DRVINFO_DATA)),
                };
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return this.DescriptionString;
            }
        }
    }
}
