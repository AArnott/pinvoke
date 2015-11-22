﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

    /// <summary>
    /// Exported functions from the Kernel32.dll Windows library.
    /// </summary>
    public static partial class Kernel32
    {
        /// <summary>
        /// The maximum length of file paths for most Win32 functions.
        /// </summary>
        public const int MAX_PATH = 260;

        /// <summary>
        /// Constant for invalid handle value
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        /// <summary>
        /// Searches a directory for a file or subdirectory with a name and attributes that match those specified.
        /// For the most basic version of this function, see FindFirstFile.
        /// To perform this operation as a transacted operation, use the FindFirstFileTransacted function.
        /// </summary>
        /// <param name="lpFileName">
        /// The directory or path, and the file name, which can include wildcard characters, for example, an asterisk (*) or a question mark (?).
        /// This parameter should not be NULL, an invalid string (for example, an empty string or a string that is missing the terminating null character), or end in a trailing backslash (\).
        /// If the string ends with a wildcard, period, or directory name, the user must have access to the root and all subdirectories on the path.
        /// In the ANSI version of this function, the name is limited to MAX_PATH characters. To extend this limit to approximately 32,000 wide characters, call the Unicode version of the function and prepend "\\?\" to the path. For more information, see Naming a File.
        /// </param>
        /// <param name="fInfoLevelId">
        /// The information level of the returned data.
        /// This parameter is one of the <see cref="FINDEX_INFO_LEVELS"/> enumeration values.
        /// </param>
        /// <param name="lpFindFileData">
        /// A pointer to the buffer that receives the file data.
        /// The pointer type is determined by the level of information that is specified in the <paramref name="fInfoLevelId"/> parameter.
        /// </param>
        /// <param name="fSearchOp">
        /// The type of filtering to perform that is different from wildcard matching.
        /// This parameter is one of the <see cref="FINDEX_SEARCH_OPS"/> enumeration values.
        /// </param>
        /// <param name="lpSearchFilter">
        /// A pointer to the search criteria if the specified <paramref name="fSearchOp"/> needs structured search information.
        /// At this time, none of the supported fSearchOp values require extended search information. Therefore, this pointer must be NULL.
        /// </param>
        /// <param name="dwAdditionalFlags">Specifies additional flags that control the search.</param>
        /// <returns>
        /// If the function succeeds, the return value is a search handle used in a subsequent call to FindNextFile or FindClose, and the lpFindFileData parameter contains information about the first file or directory found.
        /// If the function fails or fails to locate files from the search string in the lpFileName parameter, the return value is INVALID_HANDLE_VALUE and the contents of lpFindFileData are indeterminate.To get extended error information, call the <see cref="GetLastError"/> function.
        /// </returns>
        [DllImport(nameof(Kernel32))]
        public static extern SafeFindFilesHandle FindFirstFileEx(string lpFileName, FINDEX_INFO_LEVELS fInfoLevelId, out WIN32_FIND_DATA lpFindFileData, FINDEX_SEARCH_OPS fSearchOp, IntPtr lpSearchFilter, FindFirstFileExFlags dwAdditionalFlags);

        /// <summary>
        /// Formats a message string. The function requires a message definition as input. The message definition can come from a buffer passed into the function. It can come from a message table resource in an already-loaded module. Or the caller can ask the function to search the system's message table resource(s) for the message definition. The function finds the message definition in a message table resource based on a message identifier and a language identifier. The function copies the formatted message text to an output buffer, processing any embedded insert sequences if requested.
        /// </summary>
        /// <param name="dwFlags">
        /// The formatting options, and how to interpret the lpSource parameter. The low-order byte of dwFlags specifies how the function handles line breaks in the output buffer. The low-order byte can also specify the maximum width of a formatted output line.
        /// </param>
        /// <param name="lpSource">
        /// The location of the message definition. The type of this parameter depends upon the settings in the <paramref name="dwFlags"/> parameter.
        /// If <see cref="FormatMessageFlags.FromHModule"/>: A handle to the module that contains the message table to search.
        /// If <see cref="FormatMessageFlags.FromString"/>: Pointer to a string that consists of unformatted message text. It will be scanned for inserts and formatted accordingly.
        /// If neither of these flags is set in dwFlags, then lpSource is ignored.
        /// </param>
        /// <param name="dwMessageId">
        /// The message identifier for the requested message. This parameter is ignored if dwFlags includes <see cref="FormatMessageFlags.FromString" />.
        /// </param>
        /// <param name="dwLanguageId">
        /// The language identifier for the requested message. This parameter is ignored if dwFlags includes <see cref="FormatMessageFlags.FromString"/>.
        /// If you pass a specific LANGID in this parameter, FormatMessage will return a message for that LANGID only.If the function cannot find a message for that LANGID, it sets Last-Error to ERROR_RESOURCE_LANG_NOT_FOUND.If you pass in zero, FormatMessage looks for a message for LANGIDs in the following order:
        /// Language neutral
        /// Thread LANGID, based on the thread's locale value
        /// User default LANGID, based on the user's default locale value
        /// System default LANGID, based on the system default locale value
        /// US English
        /// If FormatMessage does not locate a message for any of the preceding LANGIDs, it returns any language message string that is present.If that fails, it returns ERROR_RESOURCE_LANG_NOT_FOUND.
        /// </param>
        /// <param name="lpBuffer">
        /// A pointer to a buffer that receives the null-terminated string that specifies the formatted message. If dwFlags includes <see cref="FormatMessageFlags.AllocateBuffer" />, the function allocates a buffer using the LocalAlloc function, and places the pointer to the buffer at the address specified in lpBuffer.
        /// This buffer cannot be larger than 64K bytes.
        /// </param>
        /// <param name="nSize">
        /// If the <see cref="FormatMessageFlags.AllocateBuffer"/> flag is not set, this parameter specifies the size of the output buffer, in TCHARs. If <see cref="FormatMessageFlags.AllocateBuffer"/> is set,
        /// this parameter specifies the minimum number of TCHARs to allocate for an output buffer.
        /// The output buffer cannot be larger than 64K bytes.
        /// </param>
        /// <param name="Arguments">
        /// An array of values that are used as insert values in the formatted message. A %1 in the format string indicates the first value in the Arguments array; a %2 indicates the second argument; and so on.
        /// The interpretation of each value depends on the formatting information associated with the insert in the message definition.The default is to treat each value as a pointer to a null-terminated string.
        /// By default, the Arguments parameter is of type va_list*, which is a language- and implementation-specific data type for describing a variable number of arguments.The state of the va_list argument is undefined upon return from the function.To use the va_list again, destroy the variable argument list pointer using va_end and reinitialize it with va_start.
        /// If you do not have a pointer of type va_list*, then specify the FORMAT_MESSAGE_ARGUMENT_ARRAY flag and pass a pointer to an array of DWORD_PTR values; those values are input to the message formatted as the insert values.Each insert must have a corresponding element in the array.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the number of TCHARs stored in the output buffer, excluding the terminating null character.
        /// If the function fails, the return value is zero.To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport(nameof(Kernel32), CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr[] Arguments);

        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        /// <returns>The thread identifier of the calling thread.</returns>
        [DllImport(nameof(Kernel32))]
        public static extern uint GetCurrentThreadId();

        /// <summary>Retrieves the process identifier of the calling process.</summary>
        /// <returns>The process identifier of the calling process.</returns>
        /// <remarks>Until the process terminates, the process identifier uniquely identifies the process throughout the system.</remarks>
        [DllImport(nameof(Kernel32))]
        public static extern uint GetCurrentProcessId();

        /// <summary>Retrieves a pseudo handle for the current process.</summary>
        /// <returns>The return value is a pseudo handle to the current process.</returns>
        /// <remarks>
        ///     A pseudo handle is a special constant, currently (HANDLE)-1, that is interpreted as the current process handle. For
        ///     compatibility with future operating systems, it is best to call GetCurrentProcess instead of hard-coding this
        ///     constant value. The calling process can use a pseudo handle to specify its own process whenever a process handle is
        ///     required. Pseudo handles are not inherited by child processes.
        ///     <para>This handle has the PROCESS_ALL_ACCESS access right to the process object.</para>
        ///     <para>
        ///         Windows Server 2003 and Windows XP:  This handle has the maximum access allowed by the security descriptor of
        ///         the process to the primary token of the process.
        ///     </para>
        ///     <para>
        ///         A process can create a "real" handle to itself that is valid in the context of other processes, or that can
        ///         be inherited by other processes, by specifying the pseudo handle as the source handle in a call to the
        ///         DuplicateHandle function. A process can also use the OpenProcess function to open a real handle to itself.
        ///     </para>
        ///     <para>
        ///         The pseudo handle need not be closed when it is no longer needed. Calling the <see cref="CloseHandle" />
        ///         function with a pseudo handle has no effect.If the pseudo handle is duplicated by DuplicateHandle, the
        ///         duplicate handle must be closed.
        ///     </para>
        /// </remarks>
        [DllImport(nameof(Kernel32))]
        public static extern SafeObjectHandle GetCurrentProcess();

        /// <summary>
        ///     Marks any outstanding I/O operations for the specified file handle. The function only cancels I/O operations
        ///     in the current process, regardless of which thread created the I/O operation.
        /// </summary>
        /// <param name="hFile">A handle to the file.</param>
        /// <param name="lpOverlapped">
        ///     A pointer to an <see cref="OVERLAPPED" /> data structure that contains the data used for asynchronous I/O.
        ///     <para>If this parameter is NULL, all I/O requests for the hFile parameter are canceled.</para>
        ///     <para>
        ///         If this parameter is not NULL, only those specific I/O requests that were issued for the file with the
        ///         specified
        ///         <paramref name="lpOverlapped" /> overlapped structure are marked as canceled, meaning that you can cancel one
        ///         or more requests, while the CancelIo function cancels all outstanding requests on a file handle.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. The cancel operation for all pending I/O operations issued
        ///     by the calling process for the specified file handle was successfully requested. The application must not free or
        ///     reuse the <see cref="OVERLAPPED" /> structure associated with the canceled I/O operations until they have
        ///     completed. The thread can use the GetOverlappedResult function to determine when the I/O operations themselves have
        ///     been completed.
        ///     <para>
        ///         If the function fails, the return value is 0 (zero). To get extended error information, call the
        ///         <see cref="GetLastError" /> function.
        ///     </para>
        ///     <para>
        ///         If this function cannot find a request to cancel, the return value is 0 (zero), and
        ///         <see cref="GetLastError" />
        ///         returns <see cref="Win32ErrorCode.ERROR_NOT_FOUND" />.
        ///     </para>
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern unsafe bool CancelIoEx(
            SafeObjectHandle hFile,
            OVERLAPPED* lpOverlapped);

        /// <summary>
        ///     Reads data from the specified file or input/output (I/O) device. Reads occur at the position specified by the file
        ///     pointer if supported by the device.
        ///     <para>
        ///         This function is designed for both synchronous and asynchronous operations. For a similar function designed
        ///         solely for asynchronous operation, see ReadFileEx.
        ///     </para>
        /// </summary>
        /// <param name="hFile">
        ///     A handle to the device (for example, a file, file stream, physical disk, volume, console buffer, tape drive,
        ///     socket, communications resource, mailslot, or pipe).
        ///     <para>The hFile parameter must have been created with read access.</para>
        ///     <para>
        ///         For asynchronous read operations, hFile can be any handle that is opened with the FILE_FLAG_OVERLAPPED flag
        ///         by the CreateFile function, or a socket handle returned by the socket or accept function.
        ///     </para>
        /// </param>
        /// <param name="lpBuffer">
        ///     A pointer to the buffer that receives the data read from a file or device.
        ///     <para>
        ///         This buffer must remain valid for the duration of the read operation. The caller must not use this buffer
        ///         until the read operation is completed.
        ///     </para>
        /// </param>
        /// <param name="nNumberOfBytesToRead">The maximum number of bytes to be read.</param>
        /// <param name="lpNumberOfBytesRead">
        ///     A pointer to the variable that receives the number of bytes read when using a synchronous hFile parameter. ReadFile
        ///     sets this value to zero before doing any work or error checking. Use <see langword="null" /> for this parameter if
        ///     this is an asynchronous operation to avoid potentially erroneous results.
        ///     <para>
        ///         This parameter can be <see langword="null" /> only when the <paramref name="lpOverlapped" /> parameter is not
        ///         <see langword="null" />.
        ///     </para>
        /// </param>
        /// <param name="lpOverlapped">
        ///     A pointer to an <see cref="OVERLAPPED" /> structure is required if the hFile parameter was opened with
        ///     FILE_FLAG_OVERLAPPED, otherwise it can be <see langword="null" />.
        ///     <para>
        ///         If hFile is opened with FILE_FLAG_OVERLAPPED, the <paramref name="lpOverlapped" /> parameter must point to a
        ///         valid and unique <see cref="OVERLAPPED" /> structure, otherwise the function can incorrectly report that the
        ///         read operation is complete.
        ///     </para>
        ///     <para>
        ///         For an hFile that supports byte offsets, if you use this parameter you must specify a byte offset at which to
        ///         start reading from the file or device. This offset is specified by setting the Offset and OffsetHigh members of
        ///         the <see cref="OVERLAPPED" /> structure. For an hFile that does not support byte offsets, Offset and OffsetHigh
        ///         are ignored.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is <see langword="true" />.
        ///     <para>
        ///         If the function fails, or is completing asynchronously, the return value is <see langword="false" />. To get
        ///         extended error information, call the GetLastError function.
        ///     </para>
        ///     <para>
        ///         Note: The <see cref="GetLastError" /> code <see cref="Win32ErrorCode.ERROR_IO_PENDING" /> is not a failure;
        ///         it designates the read operation is pending completion asynchronously.
        ///     </para>
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern unsafe bool ReadFile(
            SafeObjectHandle hFile,
            void* lpBuffer,
            uint nNumberOfBytesToRead,
            NullableUInt32 lpNumberOfBytesRead,
            OVERLAPPED* lpOverlapped);

        /// <summary>
        ///     Writes data to the specified file or input/output (I/O) device.
        ///     <para>
        ///         This function is designed for both synchronous and asynchronous operation. For a similar function designed
        ///         solely for asynchronous operation, see WriteFileEx.
        ///     </para>
        /// </summary>
        /// <param name="hFile">
        ///     A handle to the file or I/O device (for example, a file, file stream, physical disk, volume, console buffer, tape
        ///     drive, socket, communications resource, mailslot, or pipe).
        ///     <para>
        ///         The hFile parameter must have been created with the write access. For more information, see Generic Access
        ///         Rights and File Security and Access Rights.
        ///     </para>
        ///     <para>
        ///         For asynchronous write operations, hFile can be any handle opened with the CreateFile function using the
        ///         FILE_FLAG_OVERLAPPED flag or a socket handle returned by the socket or accept function.
        ///     </para>
        /// </param>
        /// <param name="lpBuffer">
        ///     A pointer to the buffer containing the data to be written to the file or device.
        ///     <para>
        ///         This buffer must remain valid for the duration of the write operation. The caller must not use this buffer
        ///         until the write operation is completed.
        ///     </para>
        /// </param>
        /// <param name="nNumberOfBytesToWrite">
        ///     The number of bytes to be written to the file or device.
        ///     <para>
        ///         A value of zero specifies a null write operation. The behavior of a null write operation depends on the
        ///         underlying file system or communications technology.
        ///     </para>
        /// </param>
        /// <param name="lpNumberOfBytesWritten">
        ///     A pointer to the variable that receives the number of bytes written when using a synchronous hFile parameter.
        ///     WriteFile sets this value to zero before doing any work or error checking. Use <see langword="null" />
        ///     for this parameter if this is an asynchronous operation to avoid potentially erroneous results.
        ///     <para>
        ///         This parameter can be NULL only when the <paramref name="lpOverlapped" /> parameter is not
        ///         <see langword="null" />.
        ///     </para>
        /// </param>
        /// <param name="lpOverlapped">
        ///     A pointer to an <see cref="OVERLAPPED" /> structure is required if the hFile parameter was opened with
        ///     FILE_FLAG_OVERLAPPED, otherwise this parameter can be NULL.
        ///     <para>
        ///         For an hFile that supports byte offsets, if you use this parameter you must specify a byte offset at which to
        ///         start writing to the file or device. This offset is specified by setting the Offset and OffsetHigh members of
        ///         the <see cref="OVERLAPPED" /> structure. For an hFile that does not support byte offsets, Offset and OffsetHigh
        ///         are ignored.
        ///     </para>
        ///     <para>
        ///         To write to the end of file, specify both the Offset and OffsetHigh members of the <see cref="OVERLAPPED" />
        ///         structure as 0xFFFFFFFF. This is functionally equivalent to previously calling the CreateFile function to open
        ///         hFile using FILE_APPEND_DATA access.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is <see langword="true" />.
        ///     <para>
        ///         If the function fails, or is completing asynchronously, the return value is <see langword="false" />. To get
        ///         extended error information, call the GetLastError function.
        ///     </para>
        ///     <para>
        ///         Note: The <see cref="GetLastError" /> code <see cref="Win32ErrorCode.ERROR_IO_PENDING" /> is not a failure;
        ///         it designates the write operation is pending completion asynchronously.
        ///     </para>
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern unsafe bool WriteFile(
            SafeObjectHandle hFile,
            void* lpBuffer,
            uint nNumberOfBytesToWrite,
            NullableUInt32 lpNumberOfBytesWritten,
            OVERLAPPED* lpOverlapped);

        /// <summary>
        /// Suspends the specified thread.
        /// A 64-bit application can suspend a WOW64 thread using the <see cref="Wow64SuspendThread"/> function.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread that is to be suspended.
        /// The handle must have the THREAD_SUSPEND_RESUME access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the thread's previous suspend count; otherwise, it is (DWORD) -1. To get extended error information, use the <see cref="GetLastError"/> function.
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern int SuspendThread(SafeObjectHandle hThread);

        /// <summary>
        /// Suspends the specified WOW64 thread.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread that is to be suspended.
        /// The handle must have the THREAD_SUSPEND_RESUME access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the thread's previous suspend count; otherwise, it is (DWORD) -1. To get extended error information, use the <see cref="GetLastError"/> function.
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern int Wow64SuspendThread(SafeObjectHandle hThread);

        /// <summary>
        /// Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread to be restarted.
        /// This handle must have the THREAD_SUSPEND_RESUME access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the thread's previous suspend count.
        /// If the function fails, the return value is (DWORD) -1. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern int ResumeThread(SafeObjectHandle hThread);

        /// <summary>
        /// Waits until the specified object is in the signaled state or the time-out interval elapses.
        /// To enter an alertable wait state, use the WaitForSingleObjectEx function. To wait for multiple objects, use WaitForMultipleObjects.
        /// </summary>
        /// <param name="hHandle">
        /// A handle to the object. For a list of the object types whose handles can be specified, see the following Remarks section.
        /// If this handle is closed while the wait is still pending, the function's behavior is undefined.
        /// The handle must have the SYNCHRONIZE access right. For more information, see Standard Access Rights.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The time-out interval, in milliseconds. If a nonzero value is specified, the function waits until the object is signaled or the interval elapses. If dwMilliseconds is zero, the function does not enter a wait state if the object is not signaled; it always returns immediately. If dwMilliseconds is INFINITE, the function will return only when the object is signaled.
        /// See MSDN docs for more information.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value indicates the event that caused the function to return. It can be one of the following values.
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern WaitForSingleObjectResult WaitForSingleObject(
            SafeHandle hHandle,
            uint dwMilliseconds);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Closes a file search handle opened by the FindFirstFile, FindFirstFileEx, FindFirstFileNameW, FindFirstFileNameTransactedW, FindFirstFileTransacted, FindFirstStreamTransactedW, or FindFirstStreamW functions.
        /// </summary>
        /// <param name="hFindFile">The file search handle.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.</para>
        /// </returns>
        [DllImport(nameof(Kernel32), SetLastError = true)]
        private static extern bool FindClose(IntPtr hFindFile);
    }
}
