﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;
    using static Kernel32;

    /// <content>
    /// Exported functions from the AdvApi32.dll Windows library
    /// that are available to Desktop apps only.
    /// </content>
    [OfferFriendlyOverloads]
    public static partial class AdvApi32
    {
        /// <summary>
        /// Used to prefix group names so that they can be distinguished from a service name,
        /// because services and service groups share the same name space.
        /// </summary>
        public const string SC_GROUP_IDENTIFIER = "+";
        public const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;

#pragma warning disable SA1303 // Const field names must begin with upper-case letter
#if APISets
        private const string api_ms_win_service_core_l1_1_1 = ApiSets.api_ms_win_service_core_l1_1_1;
        private const string api_ms_win_service_management_l1_1_0 = ApiSets.api_ms_win_service_management_l1_1_0;
        private const string api_ms_win_service_management_l2_1_0 = ApiSets.api_ms_win_service_management_l2_1_0;
        private const string api_ms_win_core_processthreads_l1_1_1 = ApiSets.api_ms_win_core_processthreads_l1_1_1;
        private const string api_ms_win_security_base_l1_2_0 = ApiSets.api_ms_win_security_base_l1_2_0;
        private const string api_ms_win_service_winsvc_l1_2_0 = ApiSets.api_ms_win_service_winsvc_l1_2_0;
#else
        private const string api_ms_win_service_core_l1_1_1 = nameof(AdvApi32);
        private const string api_ms_win_service_management_l1_1_0 = nameof(AdvApi32);
        private const string api_ms_win_service_management_l2_1_0 = nameof(AdvApi32);
        private const string api_ms_win_core_processthreads_l1_1_1 = nameof(AdvApi32);
        private const string api_ms_win_security_base_l1_2_0 = nameof(AdvApi32);
        private const string api_ms_win_service_winsvc_l1_2_0 = nameof(AdvApi32);
#endif
#pragma warning restore SA1303 // Const field names must begin with upper-case letter

        /// <summary>
        /// Changes the configuration parameters of a service.
        /// To change the optional configuration parameters, use the <see cref="ChangeServiceConfig2(SafeServiceHandle, ServiceInfoLevel, void*)"/> function.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service.
        /// This handle is returned by the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function and
        /// must have the <see cref="ServiceAccess.SERVICE_CHANGE_CONFIG"/> access right.
        /// </param>
        /// <param name="dwServiceType">
        /// The type of service. Specify <see cref="SERVICE_NO_CHANGE"/> if you are not changing the existing service type;
        /// otherwise, specify one of the following service types.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceType.SERVICE_FILE_SYSTEM_DRIVER"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_KERNEL_DRIVER"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_WIN32_OWN_PROCESS"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_WIN32_SHARE_PROCESS"/>
        /// </item>
        /// </list>
        /// </param>
        /// If you specify either <see cref="ServiceType.SERVICE_WIN32_OWN_PROCESS"/> or <see cref="ServiceType.SERVICE_WIN32_SHARE_PROCESS"/>,
        /// and the service is running in the context of the LocalSystem account, you can also specify the following type.
        /// <see cref="ServiceType.SERVICE_INTERACTIVE_PROCESS"/>
        /// <param name="dwStartType">
        /// The type of service. Specify <see cref="SERVICE_NO_CHANGE"/> if you are not changing the existing service type;
        /// otherwise, specify one of the following service types.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceStartType.SERVICE_AUTO_START"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStartType.SERVICE_BOOT_START"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStartType.SERVICE_DEMAND_START"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStartType.SERVICE_DISABLED"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStartType.SERVICE_SYSTEM_START"/>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="dwErrorControl">
        /// The severity of the error, and action taken, if this service fails to start. Specify SERVICE_NO_CHANGE if you are not changing the existing error control;
        /// otherwise, specify one of the following values.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceErrorControl.SERVICE_ERROR_CRITICAL"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceErrorControl.SERVICE_ERROR_IGNORE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceErrorControl.SERVICE_ERROR_NORMAL"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceErrorControl.SERVICE_ERROR_SEVERE"/>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="lpBinaryPathName">
        /// The fully qualified path to the service binary file. Specify NULL if you are not changing the existing path.
        /// If the path contains a space, it must be quoted so that it is correctly interpreted.
        /// For example, "d:\\my share\\myservice.exe" should be specified as "\"d:\\my share\\myservice.exe\"".
        /// The path can also include arguments for an auto-start service.
        /// For example, "d:\\myshare\\myservice.exe arg1 arg2". These arguments are passed to the service entry point (typically the main function).
        /// If you specify a path on another computer, the share must be accessible by the computer account of the local computer because this is the security context used in the remote call.
        /// However, this requirement allows any potential vulnerabilities in the remote computer to affect the local computer. Therefore, it is best to use a local file.
        /// </param>
        /// <param name="lpLoadOrderGroup">
        /// The name of the load ordering group of which this service is a member. Specify NULL if you are not changing the existing group. Specify an empty string if the service does not belong to a group.
        /// The startup program uses load ordering groups to load groups of services in a specified order with respect to the other groups. The list of load ordering groups is contained in the ServiceGroupOrder value of the following registry key:
        /// HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control
        /// </param>
        /// <param name="lpdwTagId">
        /// A pointer to a variable that receives a tag value that is unique in the group specified in the lpLoadOrderGroup parameter. Specify NULL if you are not changing the existing tag.
        /// You can use a tag for ordering service startup within a load ordering group by specifying a tag order vector in the following registry value:
        /// HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\GroupOrderList
        /// Tags are only evaluated for driver services that have <see cref="ServiceStartType.SERVICE_BOOT_START"/> or <see cref="ServiceStartType.SERVICE_SYSTEM_START"/> start types.
        /// </param>
        /// <param name="lpDependencies">
        /// A pointer to a double null-terminated array of null-separated names of services or load ordering groups that the system must start before this service. Specify NULL or an empty string if the service has no dependencies.
        /// Dependency on a group means that this service can run if at least one member of the group is running after an attempt to start all members of the group.
        /// You must prefix group names with SC_GROUP_IDENTIFIER so that they can be distinguished from a service name, because services and service groups share the same name space.
        /// </param>
        /// <param name="lpServiceStartName">
        /// The name of the account under which the service should run.
        /// If the service type is <see cref="ServiceType.SERVICE_WIN32_OWN_PROCESS"/>, use an account name in the form DomainName\UserName.
        /// The service process will be logged on as this user.
        /// If the account belongs to the built-in domain, you can specify .\UserName.
        /// </param>
        /// <param name="lpPassword">
        /// The password to the account name specified by the lpServiceStartName parameter.
        /// Specify an empty string if the account has no password or if the service runs in the LocalService, NetworkService, or LocalSystem account.
        /// If the account name specified by the lpServiceStartName parameter is the name of a managed service account or virtual account name, the lpPassword parameter must be NULL.
        /// Passwords are ignored for driver services.
        /// </param>
        /// <param name="lpDisplayName">
        /// The display name to be used by applications to identify the service for its users. Specify NULL if you are not changing the existing display name; otherwise, this string has a maximum length of 256 characters. The name is case-preserved in the service control manager. Display name comparisons are always case-insensitive.
        /// This parameter can specify a localized string using the following format:
        /// @[path\]dllname,-strID
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// The following error codes may be set by the service control manager.
        /// Other error codes may be set by the registry functions that are called by the service control manager.
        /// <list type="bullet">
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_ACCESS_DENIED"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_CIRCULAR_DEPENDENCY"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_DUPLICATE_SERVICE_NAME"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_INVALID_HANDLE"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_INVALID_PARAMETER"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_INVALID_SERVICE_ACCOUNT"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_SERVICE_MARKED_FOR_DELETE"/>
        /// </item>
        /// </list>
        /// </returns>
        [DllImport(api_ms_win_service_management_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ChangeServiceConfig(
            SafeServiceHandle hService,
            ServiceType dwServiceType,
            ServiceStartType dwStartType,
            ServiceErrorControl dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            int lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword,
            string lpDisplayName);

        /// <summary>
        /// Changes the optional configuration parameters of a service.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service.
        /// This handle is returned by the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function and
        /// must have the <see cref="ServiceAccess.SERVICE_CHANGE_CONFIG"/> access right.
        /// </param>
        /// <param name="dwInfoLevel">
        /// The configuration information to be changed.
        /// This parameter can be one value from <see cref="ServiceStartType"/>.
        /// </param>
        /// <param name="lpInfo">
        /// A pointer to the new value to be set for the configuration information.
        /// The format of this data depends on the value of the <paramref name="dwInfoLevel"/> parameter.
        /// If this value is NULL, the information remains unchanged.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero
        /// </returns>
        [DllImport(api_ms_win_service_management_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static unsafe extern bool ChangeServiceConfig2(SafeServiceHandle hService, ServiceInfoLevel dwInfoLevel, void* lpInfo);

        /// <summary>
        /// Sends a control code to a service.
        /// To specify additional information when stopping a service, use the ControlServiceEx function.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service. This handle is returned by the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function.
        /// The access rights required for this handle depend on the <paramref name="dwControl"/> code requested.
        /// </param>
        /// <param name="dwControl">
        /// This parameter can be one of the following control codes.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_CONTINUE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_INTERROGATE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_NETBINDADD"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_NETBINDDISABLE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_NETBINDENABLE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_NETBINDREMOVE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_PARAMCHANGE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_PAUSE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceControl.SERVICE_CONTROL_STOP"/>
        /// </item>
        /// </list>
        /// </param>
        /// This value can also be a user-defined control code (Range 128 to 255).
        /// The service defines the action associated with the control code. The hService handle must have the <see cref="ServiceAccess.SERVICE_USER_DEFINED_CONTROL"/> access right.
        /// <param name="lpServiceStatus">
        /// A pointer to a <see cref="SERVICE_STATUS"/> structure that receives the latest service status information.
        /// The information returned reflects the most recent status that the service reported to the service control manager.
        /// The service control manager fills in the structure only when ControlService returns one of the following error codes:
        /// NO_ERROR, <see cref="Win32ErrorCode.ERROR_INVALID_SERVICE_CONTROL"/>, ERROR_SERVICE_CANNOT_ACCEPT_CTRL, or <see cref="Win32ErrorCode.ERROR_SERVICE_NOT_ACTIVE"/>.
        /// Otherwise, the structure is not filled in.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ControlService(SafeServiceHandle hService, ServiceControl dwControl, ref SERVICE_STATUS lpServiceStatus);

        /// <summary>
        /// Creates a service object and adds it to the specified service control manager database.
        /// </summary>
        /// <param name="hSCManager">
        /// A handle to the service control manager database.
        /// This handle is returned by the <see cref="OpenSCManager"/> function and must have the <see cref="ServiceManagerAccess.SC_MANAGER_CREATE_SERVICE"/> access right.
        /// </param>
        /// <param name="lpServiceName">
        /// The name of the service to install.
        /// The maximum string length is 256 characters.
        /// The service control manager database preserves the case of the characters,
        /// but service name comparisons are always case insensitive.
        /// Forward-slash (/) and backslash (\) are not valid service name characters.
        /// </param>
        /// <param name="lpDisplayName">
        /// The display name to be used by user interface programs to identify the service.
        /// This string has a maximum length of 256 characters.
        /// The name is case-preserved in the service control manager.
        /// Display name comparisons are always case-insensitive.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// Before granting the requested access, the system checks the access token of the calling process.
        /// Common specific rights are defined in <seealso cref="ServiceAccess"/>.
        /// </param>
        /// <param name="dwServiceType">
        /// The service type (<see cref="ServiceType"/>).
        /// </param>
        /// <param name="dwStartType">
        /// The service start options (<see cref="ServiceStartType"/>).
        /// </param>
        /// <param name="dwErrorControl">
        /// The severity of the error (<see cref="ServiceErrorControl"/>), and action taken, if this service fails to start.
        /// </param>
        /// <param name="lpBinaryPathName">
        /// The fully qualified path to the service binary file. If the path contains a space, it must be quoted so that it is correctly interpreted. For example, "d:\\my share\\myservice.exe" should be specified as "\"d:\\my share\\myservice.exe\"".
        /// The path can also include arguments for an auto-start service.For example, "d:\\myshare\\myservice.exe arg1 arg2". These arguments are passed to the service entry point (typically the main function).
        /// If you specify a path on another computer, the share must be accessible by the computer account of the local computer because this is the security context used in the remote call.However, this requirement allows any potential vulnerabilities in the remote computer to affect the local computer. Therefore, it is best to use a local file.
        /// </param>
        /// <param name="lpLoadOrderGroup">
        /// The names of the load ordering group of which this service is a member. Specify NULL or an empty string if the service does not belong to a group.
        /// The startup program uses load ordering groups to load groups of services in a specified order with respect to the other groups.
        /// The list of load ordering groups is contained in the following registry value:
        /// HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\ServiceGroupOrder
        /// </param>
        /// <param name="lpdwTagId">
        /// A pointer to a variable that receives a tag value that is unique in the group specified in the lpLoadOrderGroup parameter. Specify NULL if you are not changing the existing tag.
        /// You can use a tag for ordering service startup within a load ordering group by specifying a tag order vector in the following registry value:
        /// HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\GroupOrderList
        /// Tags are only evaluated for driver services that have <see cref="ServiceStartType.SERVICE_BOOT_START"/> or <see cref="ServiceStartType.SERVICE_SYSTEM_START"/> start types.
        /// </param>
        /// <param name="lpDependencies">
        /// A pointer to a double null-terminated array of null-separated names of services or load ordering groups that the system must start before this service. Specify NULL or an empty string if the service has no dependencies.
        /// Dependency on a group means that this service can run if at least one member of the group is running after an attempt to start all members of the group.
        /// You must prefix group names with SC_GROUP_IDENTIFIER so that they can be distinguished from a service name, because services and service groups share the same name space.
        /// </param>
        /// <param name="lpServiceStartName">
        /// The name of the account under which the service should run.
        /// If the service type is <see cref="ServiceType.SERVICE_WIN32_OWN_PROCESS"/>, use an account name in the form DomainName\UserName.
        /// The service process will be logged on as this user.
        /// If the account belongs to the built-in domain, you can specify .\UserName.
        /// </param>
        /// <param name="lpPassword">
        /// The password to the account name specified by the lpServiceStartName parameter.
        /// Specify an empty string if the account has no password or if the service runs in the LocalService, NetworkService, or LocalSystem account.
        /// If the account name specified by the lpServiceStartName parameter is the name of a managed service account or virtual account name, the lpPassword parameter must be NULL.
        /// Passwords are ignored for driver services.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the service.
        /// If the function fails, the return value is NULL
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeServiceHandle CreateService(SafeServiceHandle hSCManager, string lpServiceName, string lpDisplayName, ACCESS_MASK dwDesiredAccess, ServiceType dwServiceType, ServiceStartType dwStartType, ServiceErrorControl dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, int lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

        /// <summary>
        /// Marks the specified service for deletion from the service control manager database.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service. This handle is returned by the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function, and it must have the <see cref="ACCESS_MASK.StandardRight.DELETE"/> access right
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteService(SafeServiceHandle hService);

        /// <summary>
        /// Establishes a connection to the service control manager on the specified computer and opens the specified service control manager database.
        /// </summary>
        /// <param name="lpMachineName">
        /// The name of the target computer.
        /// If the pointer is NULL or points to an empty string,
        /// the function connects to the service control manager on the local computer.
        /// </param>
        /// <param name="lpDatabaseName">
        /// The name of the service control manager database.
        /// This parameter should be set to SERVICES_ACTIVE_DATABASE.
        /// If it is NULL, the SERVICES_ACTIVE_DATABASE database is opened by default.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The access to the service control manager. For a list of access rights, see Service Security and Access Rights.
        /// Before granting the requested access rights, the system checks the access token of the calling process against the discretionary access-control list of the security descriptor associated with the service control manager.
        /// The <see cref="ServiceManagerAccess.SC_MANAGER_CONNECT"/> access right is implicitly specified by calling this function.
        /// Common specific rights are defined in <seealso cref="ServiceManagerAccess"/>.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the specified service control manager database.
        /// If the function fails, the return value is NULL.To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeServiceHandle OpenSCManager(string lpMachineName, string lpDatabaseName, ACCESS_MASK dwDesiredAccess);

        /// <summary>
        /// Opens an existing service.
        /// </summary>
        /// <param name="hSCManager">
        /// A handle to the service control manager database. The <see cref="OpenSCManager"/> function returns this handle.
        /// </param>
        /// <param name="lpServiceName">
        /// The name of the service to be opened. This is the name specified by the lpServiceName parameter of the CreateService function when the service object was created, not the service display name that is shown by user interface applications to identify the service.
        /// The maximum string length is 256 characters.The service control manager database preserves the case of the characters, but service name comparisons are always case insensitive.Forward-slash(/) and backslash(\) are invalid service name characters.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// Before granting the requested access, the system checks the access token of the calling process against the discretionary access-control list of the security descriptor associated with the service object.
        /// Common specific rights are defined in <seealso cref="ServiceAccess"/>.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the service.
        /// If the function fails, the return value is NULL.
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeServiceHandle OpenService(SafeServiceHandle hSCManager, string lpServiceName, ACCESS_MASK dwDesiredAccess);

        /// <summary>
        /// Starts a service.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service. This handle is returned by the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function, and it must have the <see cref="ServiceAccess.SERVICE_START"/> access right.
        /// </param>
        /// <param name="dwNumServiceArgs">
        /// The number of strings in the lpServiceArgVectors array. If lpServiceArgVectors is NULL, this parameter can be zero.
        /// </param>
        /// <param name="lpServiceArgVectors">
        /// The null-terminated strings to be passed to the ServiceMain function for the service as arguments. If there are no arguments, this parameter can be NULL. Otherwise, the first argument (lpServiceArgVectors[0]) is the name of the service, followed by any additional arguments (lpServiceArgVectors[1] through lpServiceArgVectors[dwNumServiceArgs-1]).
        /// Driver services do not receive these arguments.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StartService(SafeServiceHandle hService, int dwNumServiceArgs, string lpServiceArgVectors);

        /// <summary>Opens the access token associated with a process.</summary>
        /// <param name="processHandle">
        ///     A handle to the process whose access token is opened. The process must have the
        ///     PROCESS_QUERY_INFORMATION access permission.
        /// </param>
        /// <param name="desiredAccess">
        ///     Specifies an access mask that specifies the requested types of access to the access token.
        ///     These requested access types are compared with the discretionary access control list (DACL) of the token to
        ///     determine which accesses are granted or denied.
        ///     Common specific rights are defined in <seealso cref="TokenAccessRights"/>.
        /// </param>
        /// <param name="tokenHandle">A handle that identifies the newly opened access token when the function returns.</param>
        /// <returns>
        ///     If the function succeeds, the return value is a nonzero value.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(api_ms_win_core_processthreads_l1_1_1, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(
            IntPtr processHandle,
            ACCESS_MASK desiredAccess,
            out SafeObjectHandle tokenHandle);

        /// <summary>
        ///     The GetTokenInformation function retrieves a specified type of information about an access token. The calling
        ///     process must have appropriate access rights to obtain the information.
        ///     <para>
        ///         To determine if a user is a member of a specific group, use the CheckTokenMembership function. To determine
        ///         group membership for app container tokens, use the CheckTokenMembershipEx function.
        ///     </para>
        /// </summary>
        /// <param name="TokenHandle">
        ///     A handle to an access token from which information is retrieved. If TokenInformationClass
        ///     specifies TokenSource, the handle must have TOKEN_QUERY_SOURCE access. For all other TokenInformationClass values,
        ///     the handle must have TOKEN_QUERY access.
        /// </param>
        /// <param name="TokenInformationClass">
        ///     Specifies a value from the TOKEN_INFORMATION_CLASS enumerated type to identify the
        ///     type of information the function retrieves. Any callers who check the TokenIsAppContainer and have it return 0
        ///     should also verify that the caller token is not an identify level impersonation token. If the current token is not
        ///     an app container but is an identity level token, you should return AccessDenied.
        /// </param>
        /// <param name="TokenInformation">
        ///     A pointer to a buffer the function fills with the requested information. The structure
        ///     put into this buffer depends upon the type of information specified by the
        ///     <paramref name="TokenInformationClass" /> parameter.
        /// </param>
        /// <param name="TokenInformationLength">
        ///     Specifies the size, in bytes, of the buffer pointed to by the TokenInformation
        ///     parameter. If <paramref name="TokenInformation" /> is NULL, this parameter must be zero.
        /// </param>
        /// <param name="ReturnLength">
        ///     A pointer to a variable that receives the number of bytes needed for the buffer pointed to by the TokenInformation
        ///     parameter. If this value is larger than the value specified in the TokenInformationLength parameter, the function
        ///     fails and stores no data in the buffer.
        ///     <para>
        ///         If the value of the <paramref name="TokenInformationClass" /> parameter is
        ///         <see cref="TOKEN_INFORMATION_CLASS.TokenDefaultDacl" /> and the token has no default DACL, the function sets
        ///         the variable pointed to by ReturnLength to sizeof(TOKEN_DEFAULT_DACL) and sets the DefaultDacl member of the
        ///         TOKEN_DEFAULT_DACL structure to NULL.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a nonzero value.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(api_ms_win_security_base_l1_2_0, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern unsafe bool GetTokenInformation(
            SafeObjectHandle TokenHandle,
            TOKEN_INFORMATION_CLASS TokenInformationClass,
            void* TokenInformation,
            int TokenInformationLength,
            out int ReturnLength);

        /// <summary>
        ///     The QueryServiceObjectSecurity function retrieves a copy of the security descriptor associated with a service
        ///     object. You can also use the GetNamedSecurityInfo function to retrieve a security descriptor.
        /// </summary>
        /// <param name="hService">
        ///     A handle to the service control manager or the service. Handles to the service control manager
        ///     are returned by the <see cref="OpenSCManager" /> function, and handles to a service are returned by either the
        ///     <see cref="OpenService" /> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)" /> function. The handle must have the READ_CONTROL access
        ///     right.
        /// </param>
        /// <param name="dwSecurityInformation">
        ///     A set of bit flags that indicate the type of security information to retrieve. This
        ///     parameter can be a combination of the <see cref="SECURITY_INFORMATION" /> flags, with the exception that this
        ///     function does not support the <see cref="SECURITY_INFORMATION.LABEL_SECURITY_INFORMATION" /> value.
        /// </param>
        /// <param name="lpSecurityDescriptor">
        ///     A pointer to a buffer that receives a copy of the security descriptor of the
        ///     specified service object. The calling process must have the appropriate access to view the specified aspects of the
        ///     security descriptor of the object. The SECURITY_DESCRIPTOR structure is returned in self-relative format.
        /// </param>
        /// <param name="cbBufSize">
        ///     The size of the buffer pointed to by the <paramref name="lpSecurityDescriptor" /> parameter, in
        ///     bytes. The largest size allowed is 8 kilobytes.
        /// </param>
        /// <param name="pcbBytesNeeded">
        ///     A pointer to a variable that receives the number of bytes needed to return the requested
        ///     security descriptor information, if the function fails.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a nonzero value.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(api_ms_win_service_management_l2_1_0, SetLastError = true)]
        public static extern bool QueryServiceObjectSecurity(
            SafeServiceHandle hService,
            SECURITY_INFORMATION dwSecurityInformation,
            byte[] lpSecurityDescriptor,
            int cbBufSize,
            out int pcbBytesNeeded);

        /// <summary>
        /// Retrieves the current status of the specified service.
        /// This function has been superseded by the <see cref="QueryServiceStatusEx(SafeServiceHandle, SC_STATUS_TYPE, void*, int, out int)"/> function. <see cref="QueryServiceStatusEx(SafeServiceHandle, SC_STATUS_TYPE, void*, int, out int)"/> returns the same information <see cref="QueryServiceStatus"/> returns, with the addition of the process identifier and additional information for the service.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service. This handle is returned by the <see cref="OpenService"/> or the <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function, and it must have the <see cref="ServiceAccess.SERVICE_QUERY_STATUS"/> access right.
        /// </param>
        /// <param name="dwServiceStatus">
        /// A pointer to a <see cref="SERVICE_STATUS"/> structure that receives the status information.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(api_ms_win_service_winsvc_l1_2_0, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryServiceStatus(SafeServiceHandle hService, ref SERVICE_STATUS dwServiceStatus);

        /// <summary>The SetServiceObjectSecurity function sets the security descriptor of a service object.</summary>
        /// <param name="hService">
        ///     A handle to the service. This handle is returned by the <see cref="OpenService" /> or
        ///     <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)" /> function. The access required for this handle depends on the security information
        ///     specified in the <paramref name="dwSecurityInformation" /> parameter.
        /// </param>
        /// <param name="dwSecurityInformation">
        ///     Specifies the components of the security descriptor to set. This parameter can be a
        ///     combination of the following values : <see cref="SECURITY_INFORMATION.DACL_SECURITY_INFORMATION" />,
        ///     <see cref="SECURITY_INFORMATION.GROUP_SECURITY_INFORMATION" />,
        ///     <see cref="SECURITY_INFORMATION.OWNER_SECURITY_INFORMATION" />,
        ///     <see cref="SECURITY_INFORMATION.SACL_SECURITY_INFORMATION" />. Note that flags not handled by
        ///     SetServiceObjectSecurity will be silently ignored.
        /// </param>
        /// <param name="lpSecurityDescriptor">
        ///     A pointer to a SECURITY_DESCRIPTOR structure that contains the new security
        ///     information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a nonzero value.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(api_ms_win_service_management_l2_1_0, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetServiceObjectSecurity(
            SafeServiceHandle hService,
            SECURITY_INFORMATION dwSecurityInformation,
            byte[] lpSecurityDescriptor);

        /// <summary>
        /// Updates the service control manager's status information for the calling service.
        /// </summary>
        /// <param name="hServiceStatus">
        /// A handle to the status information structure for the current service.
        /// This handle is returned by the RegisterServiceCtrlHandlerEx function.
        /// The service status handle does not have to be closed.
        /// </param>
        /// <param name="lpServiceStatus">
        /// A pointer to the <see cref="SERVICE_STATUS"/> structure the contains the latest status information for the calling service.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// The following error codes can be set by the service control manager.
        /// Other error codes can be set by the registry functions that are called by the service control manager.
        /// <list type="bullet">
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_INVALID_DATA"/>
        /// </item>
        /// <item>
        /// <see cref="Win32ErrorCode.ERROR_INVALID_HANDLE"/>
        /// </item>
        /// </list>
        /// </returns>
        [DllImport(api_ms_win_service_core_l1_1_1, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetServiceStatus(IntPtr hServiceStatus, ref SERVICE_STATUS lpServiceStatus);

        /// <summary>
        /// Enumerates services in the specified service control manager database.
        /// The name and status of each service are provided.
        /// This function has been superseded by the EnumServicesStatusEx function.
        /// It returns the same information <see cref="EnumServicesStatus(SafeServiceHandle,ServiceType,ServiceStateQuery,byte*,int,ref int,ref int,ref int)"/> returns, plus the process identifier and additional information for the service.
        /// In addition, EnumServicesStatusEx enables you to enumerate services that belong to a specified group.
        /// </summary>
        /// <param name="hSCManager">
        /// A handle to the service control manager database.
        /// This handle is returned by the <see cref="OpenSCManager"/> function, and must have the <see cref="ServiceManagerAccess.SC_MANAGER_ENUMERATE_SERVICE"/> access right.
        /// </param>
        /// <param name="dwServiceType">
        /// The type of services to be enumerated.
        /// This parameter can be one or more of the following values.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceType.SERVICE_DRIVER"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_FILE_SYSTEM_DRIVER"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_KERNEL_DRIVER"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_WIN32"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_WIN32_OWN_PROCESS"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceType.SERVICE_WIN32_SHARE_PROCESS"/>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="dwServiceState">
        /// The state of the services to be enumerated.
        /// This parameter can be one of the following values.
        /// <list type="bullet">
        /// <item>
        /// <see cref="ServiceStateQuery.SERVICE_ACTIVE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStateQuery.SERVICE_INACTIVE"/>
        /// </item>
        /// <item>
        /// <see cref="ServiceStateQuery.SERVICE_STATE_ALL"/>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="lpServices">
        /// A pointer to a buffer that contains an array of <see cref="ENUM_SERVICE_STATUS"/> structures that receive the name and service status information for each service in the database.
        /// The buffer must be large enough to hold the structures, plus the strings to which their members point.
        /// The maximum size of this array is 256K bytes.
        /// To determine the required size, specify NULL for this parameter and 0 for the <paramref name="cbBufSize" /> parameter.
        /// The function will fail and <see cref="GetLastError"/> will return <see cref="Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER"/>.
        /// The <paramref name="pcbBytesNeeded"/> parameter will receive the required size.
        /// Windows Server 2003 and Windows XP:  The maximum size of this array is 64K bytes.
        /// This limit was increased as of Windows Server 2003 with SP1 and Windows XP with SP2.
        /// </param>
        /// <param name="cbBufSize">
        /// The size of the buffer pointed to by the <paramref name="lpServices"/> parameter, in bytes.
        /// </param>
        /// <param name="pcbBytesNeeded">
        /// A pointer to a variable that receives the number of bytes needed to return the remaining service entries, if the buffer is too small.
        /// </param>
        /// <param name="lpServicesReturned">
        /// A pointer to a variable that receives the number of service entries returned.
        /// </param>
        /// <param name="lpResumeHandle">
        /// A pointer to a variable that, on input, specifies the starting point of enumeration.
        /// You must set this value to zero the first time this function is called. On output, this value is zero if the function succeeds.
        /// However, if the function returns zero and the <see cref="GetLastError"/> function returns <see cref="Win32ErrorCode.ERROR_MORE_DATA"/>,
        /// this value is used to indicate the next service entry to be read when the function is called to retrieve the additional data.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport(nameof(AdvApi32), SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static unsafe extern bool EnumServicesStatus(
            SafeServiceHandle hSCManager,
            ServiceType dwServiceType,
            ServiceStateQuery dwServiceState,
            byte* lpServices,
            int cbBufSize,
            ref int pcbBytesNeeded,
            ref int lpServicesReturned,
            ref int lpResumeHandle);

        /// <summary>
        /// Converts an NTSTATUS code returned by an LSA function to a Windows error code.
        /// </summary>
        /// <param name="Status">An NTSTATUS code returned by an LSA function call. This value will be converted to a System error code.</param>
        /// <returns>
        /// The return value is the Windows error code that corresponds to the Status parameter. If there is no corresponding Windows error code, the return value is <see cref="Win32ErrorCode.ERROR_MR_MID_NOT_FOUND"/>.
        /// </returns>
        [DllImport(nameof(AdvApi32))]
        public static extern Win32ErrorCode LsaNtStatusToWinError(NTSTATUS Status);

        /// <summary>
        ///     Opens the specified registry key. Note that key names are not case sensitive.
        ///     <para>To perform transacted registry operations on a key, call the RegOpenKeyTransacted function.</para>
        /// </summary>
        /// <param name="hKey">
        ///     A handle to an open registry key.
        ///     <para>
        ///         This handle is returned by the RegCreateKeyEx, RegCreateKeyTransacted, <see cref="RegOpenKeyEx" />, or
        ///         RegOpenKeyTransacted function. It can also be one of the following predefined keys:
        ///         <see cref="HKEY_CLASSES_ROOT" />,
        ///         <see cref="HKEY_CURRENT_CONFIG" />,
        ///         <see cref="HKEY_CURRENT_USER" />,
        ///         <see cref="HKEY_LOCAL_MACHINE" />,
        ///         <see cref="HKEY_PERFORMANCE_DATA" /> and
        ///         <see cref="HKEY_USERS" />
        ///     </para>
        /// </param>
        /// <param name="lpSubKey">
        ///     The name of the registry subkey to be opened.
        ///     <para>Key names are not case sensitive.</para>
        ///     <para>
        ///         The lpSubKey parameter can be an empty string. If lpSubKey is a pointer to an empty string and
        ///         <paramref name="hKey" /> is <see cref="HKEY_CLASSES_ROOT" />, <paramref name="phkResult" /> receives the same
        ///         <paramref name="hKey" /> handle passed into the function. Otherwise, <paramref name="phkResult" /> receives a
        ///         new handle to the key specified by <paramref name="hKey" />.
        ///     </para>
        ///     <para>
        ///         The lpSubKey parameter can be <c>null</c> only if hKey is one of the predefined keys. If lpSubKey is
        ///         <c>null</c> and hKey is <see cref="HKEY_CLASSES_ROOT" />, <paramref name="phkResult" /> receives a new handle
        ///         to the key specified by <paramref name="hKey" />. Otherwise, <paramref name="phkResult" /> receives the same
        ///         <paramref name="hKey" /> handle passed in to the function.
        ///     </para>
        /// </param>
        /// <param name="ulOptions">Specifies the option to apply when opening the key.</param>
        /// <param name="samDesired">
        ///     A mask that specifies the desired access rights to the key to be opened. The function fails if
        ///     the security descriptor of the key does not permit the requested access for the calling process.
        /// </param>
        /// <param name="phkResult">
        ///     A variable that receives a handle to the opened key. If the key is not one of the predefined
        ///     registry keys, it should be disposed after you have finished using the handle.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is <see cref="Win32ErrorCode.ERROR_SUCCESS" />. If the function
        ///     fails the error code is returned.
        /// </returns>
        [DllImport(nameof(AdvApi32), CharSet = CharSet.Unicode)]
        public static extern Win32ErrorCode RegOpenKeyEx(
            SafeRegistryHandle hKey,
            string lpSubKey,
            RegOpenKeyOptions ulOptions,
            ACCESS_MASK samDesired,
            out SafeRegistryHandle phkResult);

        /// <summary>Writes all the attributes of the specified open registry key into the registry.</summary>
        /// <param name="hKey">
        ///     A handle to an open registry key. The key must have been opened with the <see cref="KEY_QUERY_VALUE" /> access
        ///     right.
        ///     <para>
        ///         This handle is returned by the RegCreateKeyEx, RegCreateKeyTransacted, <see cref="RegOpenKeyEx" />, or
        ///         RegOpenKeyTransacted function. It can also be one of the following predefined keys:
        ///         <see cref="HKEY_CLASSES_ROOT" />,
        ///         <see cref="HKEY_CURRENT_CONFIG" />,
        ///         <see cref="HKEY_CURRENT_USER" />,
        ///         <see cref="HKEY_LOCAL_MACHINE" />,
        ///         <see cref="HKEY_PERFORMANCE_DATA" /> and
        ///         <see cref="HKEY_USERS" />
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is <see cref="Win32ErrorCode.ERROR_SUCCESS" />. If the function
        ///     fails the error code is returned.
        /// </returns>
        [DllImport(nameof(AdvApi32))]
        public static extern Win32ErrorCode RegFlushKey(SafeRegistryHandle hKey);

        /// <summary>Notifies the caller about changes to the attributes or contents of a specified registry key.</summary>
        /// <param name="hKey">
        ///     A handle to an open registry key.
        ///     <para>
        ///         This handle is returned by the RegCreateKeyEx, RegCreateKeyTransacted, <see cref="RegOpenKeyEx" />, or
        ///         RegOpenKeyTransacted function. It can also be one of the following predefined keys:
        ///         <see cref="HKEY_CLASSES_ROOT" />,
        ///         <see cref="HKEY_CURRENT_CONFIG" />,
        ///         <see cref="HKEY_CURRENT_USER" />,
        ///         <see cref="HKEY_LOCAL_MACHINE" />,
        ///         <see cref="HKEY_PERFORMANCE_DATA" /> and
        ///         <see cref="HKEY_USERS" />
        ///     </para>
        ///     <para>
        ///         This parameter must be a local handle. If <see cref="RegNotifyChangeKeyValue" /> is called with a remote
        ///         handle, it returns <see cref="Win32ErrorCode.ERROR_INVALID_HANDLE" />.
        ///     </para>
        ///     <para>The key must have been opened with the <see cref="KEY_NOTIFY" /> access right.</para>
        /// </param>
        /// <param name="bWatchSubtree">
        ///     If this parameter is <c>true</c>, the function reports changes in the specified key and its
        ///     subkeys. If the parameter is <c>false</c>, the function reports changes only in the specified key.
        /// </param>
        /// <param name="dwNotifyFilter">A value that indicates the changes that should be reported.</param>
        /// <param name="hEvent">
        ///     A handle to an event. If the <paramref name="fAsynchronous" /> parameter is <c>true</c>, the
        ///     function returns immediately and changes are reported by signaling this event. If <paramref name="fAsynchronous" />
        ///     is <c>false</c>, hEvent is ignored.
        /// </param>
        /// <param name="fAsynchronous">
        ///     If this parameter is <c>true</c>, the function returns immediately and reports changes by
        ///     signaling the specified event. If this parameter is <c>false</c>, the function does not return until a change has
        ///     occurred.
        ///     <para>
        ///         If <paramref name="hEvent" /> does not specify a valid event, the fAsynchronous parameter cannot be
        ///         <c>true</c>.
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is <see cref="Win32ErrorCode.ERROR_SUCCESS" />. If the function
        ///     fails the error code is returned.
        /// </returns>
        [DllImport(nameof(AdvApi32))]
        public static extern Win32ErrorCode RegNotifyChangeKeyValue(
            SafeRegistryHandle hKey,
            bool bWatchSubtree,
            RegNotifyFilter dwNotifyFilter,
            SafeWaitHandle hEvent,
            bool fAsynchronous);

        /// <summary>
        /// Retrieves the current status of the specified service based on the specified information level.
        /// </summary>
        /// <param name="hService">
        /// A handle to the service. This handle is returned by the <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> or <see cref="OpenService"/> function, and it must have the <see cref="ServiceAccess.SERVICE_QUERY_STATUS"/> access right. For more information, see Service Security and Access Rights.
        /// </param>
        /// <param name="InfoLevel">
        /// The service attributes to be returned. Use <see cref="SC_STATUS_TYPE.SC_STATUS_PROCESS_INFO"/> to retrieve the service status information.
        /// The <paramref name="lpBuffer"/> parameter is a pointer to a <see cref="SERVICE_STATUS_PROCESS"/> structure.</param>
        /// <param name="lpBuffer">
        /// A pointer to the buffer that receives the status information. The format of this data depends on the value of the <paramref name="InfoLevel"/> parameter.
        /// The maximum size of this array is 8K bytes. To determine the required size, specify NULL for this parameter and 0 for the <paramref name="cbBufSize" /> parameter.The function will fail and <see cref="GetLastError "/> will return ERROR_INSUFFICIENT_BUFFER.The <paramref name="pcbBytesNeeded" /> parameter will receive the required size.
        /// </param>
        /// <param name="cbBufSize">
        /// The size of the buffer pointed to by the <paramref name="lpBuffer"/> parameter, in bytes.
        /// </param>
        /// <param name="pcbBytesNeeded">
        /// A pointer to a variable that receives the number of bytes needed to store all status information, if the function fails with <see cref="Win32ErrorCode.ERROR_INSUFFICIENT_BUFFER"/>.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport(nameof(AdvApi32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static unsafe extern bool QueryServiceStatusEx(
            SafeServiceHandle hService,
            SC_STATUS_TYPE InfoLevel,
            void* lpBuffer,
            int cbBufSize,
            out int pcbBytesNeeded);

        /// <summary>
        /// Closes a handle to a service control manager or service object.
        /// </summary>
        /// <param name="hSCObject">
        /// A handle to the service control manager object or the service object to close.
        /// Handles to service control manager objects are returned by the <see cref="OpenSCManager"/> function,
        /// and handles to service objects are returned by either the <see cref="OpenService"/> or <see cref="CreateService(SafeServiceHandle,string,string,ACCESS_MASK,ServiceType,ServiceStartType,ServiceErrorControl,string,string,int, string,string,string)"/> function.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport(api_ms_win_service_management_l1_1_0, SetLastError = true)]
        private static extern bool CloseServiceHandle(IntPtr hSCObject);
    }
}
