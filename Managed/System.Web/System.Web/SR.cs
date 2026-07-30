using System;
using System.Globalization;

// Token: 0x02000004 RID: 4
internal static class SR
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
	internal static string GetString(string name, params object[] args)
	{
		return global::SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002058 File Offset: 0x00000258
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000207C File Offset: 0x0000027C
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000207F File Offset: 0x0000027F
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002092 File Offset: 0x00000292
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000020A0 File Offset: 0x000002A0
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000020AF File Offset: 0x000002AF
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x0400002A RID: 42
	public const string Parameter_Invalid = "The parameter '{0}' is invalid.";

	// Token: 0x0400002B RID: 43
	public const string Parameter_NullOrEmpty = "The string parameter '{0}' cannot be null or empty.";

	// Token: 0x0400002C RID: 44
	public const string Property_NullOrEmpty = "The value assigned to property '{0}' cannot be null or empty.";

	// Token: 0x0400002D RID: 45
	public const string Property_Invalid = "The value assigned to property '{0}' is invalid.";

	// Token: 0x0400002E RID: 46
	public const string Unexpected_Error = "An unexpected error occurred in '{0}'.";

	// Token: 0x0400002F RID: 47
	public const string PropertyCannotBeNullOrEmptyString = "Value of the '{0}' property cannot be null or an empty string.";

	// Token: 0x04000030 RID: 48
	public const string PropertyCannotBeNull = "Value of the '{0}' property cannot be null.";

	// Token: 0x04000031 RID: 49
	public const string Invalid_string_from_browser_caps = "The HttpBrowserCapabilities string '{1}' evaluated to '{2}'.  {0}  Check the browserCaps section of machine.config or web.config to correct the error.";

	// Token: 0x04000032 RID: 50
	public const string Unrecognized_construct_in_pattern = "Server cannot recognize construct '{0}' in pattern '{1}'.";

	// Token: 0x04000033 RID: 51
	public const string Caps_cannot_be_inited_twice = "Capabilities object cannot be initialized twice.";

	// Token: 0x04000034 RID: 52
	public const string Duplicate_browser_id = "Id '{0}' has already been specified and must be unique.";

	// Token: 0x04000035 RID: 53
	public const string Invalid_browser_root = "Invalid root element of browser definition file.  Must be contained in a <browsers> element.";

	// Token: 0x04000036 RID: 54
	public const string Browser_mutually_exclusive_attributes = "Browser attributes '{0}' and '{1}' cannot be specified together.";

	// Token: 0x04000037 RID: 55
	public const string Browser_refid_prohibits_identification = "The identification element cannot be used when the browser element includes a refID.";

	// Token: 0x04000038 RID: 56
	public const string Browser_invalid_element = "The '{0}' element is not allowed in a browser or gateway definition.";

	// Token: 0x04000039 RID: 57
	public const string Browser_Circular_Reference = "The browser id '{0}' is specified in a circular reference.";

	// Token: 0x0400003A RID: 58
	public const string Browser_attributes_required = "The '{1}' or '{2}' attribute is required on the {0} element of the browser definition file.";

	// Token: 0x0400003B RID: 59
	public const string Browser_parentID_Not_Found = "The browser or gateway element with ID '{0}' cannot be found.";

	// Token: 0x0400003C RID: 60
	public const string Browser_parentID_applied_to_default = "parentID attribute is not allowed on a default browser element.";

	// Token: 0x0400003D RID: 61
	public const string Browser_InvalidID = "The {0} '{1}' is not a valid identifier.";

	// Token: 0x0400003E RID: 62
	public const string Browser_Not_Allowed_InAppLevel = "Invalid element '{0}' in the application browser file, this element can only be used in a machine level browser file.";

	// Token: 0x0400003F RID: 63
	public const string Browser_InvalidStrongNameKey = "Invalid strong name key was generated.";

	// Token: 0x04000040 RID: 64
	public const string Browser_compile_error = "BrowserCap assembly failed to be compiled.";

	// Token: 0x04000041 RID: 65
	public const string DefaultBrowser_parentID_Not_Found = "The defaultBrowser element specified by parentID '{0}' cannot be found.";

	// Token: 0x04000042 RID: 66
	public const string Browser_empty_identification = "A non-empty <identification> element is required for any browser definition.";

	// Token: 0x04000043 RID: 67
	public const string Browser_W3SVC_Failure_Helper_Text = "Cannot restart W3SVC Service, this operation might require other privileges.";

	// Token: 0x04000044 RID: 68
	public const string DefaultSiteName = "Default Web Site";

	// Token: 0x04000045 RID: 69
	public const string ControlAdapters_TypeNotFound = "Unable to create type '{0}'.";

	// Token: 0x04000046 RID: 70
	public const string Failed_gac_install = "Failed to install assembly to gac.";

	// Token: 0x04000047 RID: 71
	public const string Failed_gac_uninstall = "Failed to uninstall assembly to gac.";

	// Token: 0x04000048 RID: 72
	public const string WrongType_of_Protected_provider = "The type specified does not extend ProtectedConfigurationProvider class.";

	// Token: 0x04000049 RID: 73
	public const string Make_sure_remote_server_is_enabled_for_config_access = "Unable to access the configuration system on the remote server. Make sure that the remote server allows remote configuration.";

	// Token: 0x0400004A RID: 74
	public const string Config_unable_to_get_section = "Unable to retrieve configuration section '{0}'.";

	// Token: 0x0400004B RID: 75
	public const string Config_failed_to_resolve_site_id = "Failed to resolve the site ID for '{0}'.";

	// Token: 0x0400004C RID: 76
	public const string Config_GetSectionWithPathArgInvalid = "WebConfigurationManager::GetSection(sectionName, path) can only be called from within a web application.";

	// Token: 0x0400004D RID: 77
	public const string Unable_to_create_temp_file = "Unable to create a new temp file.";

	// Token: 0x0400004E RID: 78
	public const string Config_allow_definition_error_application = "It is an error to use a section registered as allowDefinition='MachineToApplication' beyond application level.  This error can be caused by a virtual directory not being configured as an application in IIS.";

	// Token: 0x0400004F RID: 79
	public const string Config_allow_definition_error_machine = "It is an error to use a section registered as allowDefinition='MachineOnly' beyond machine.config.";

	// Token: 0x04000050 RID: 80
	public const string Config_allow_definition_error_webroot = "It is an error to use a section registered as allowDefinition='MachineToWebRoot' beyond the root web.config file.";

	// Token: 0x04000051 RID: 81
	public const string Config_base_unrecognized_element = "Unrecognized element";

	// Token: 0x04000052 RID: 82
	public const string Config_base_required_attribute_missing = "The required attribute '{0}' was not found.";

	// Token: 0x04000053 RID: 83
	public const string Config_base_required_attribute_empty = "The required attribute '{0}' cannot be empty.";

	// Token: 0x04000054 RID: 84
	public const string Config_base_unrecognized_attribute = "Unrecognized attribute '{0}'. Note that attribute names are case-sensitive.";

	// Token: 0x04000055 RID: 85
	public const string Config_base_elements_only = "Only XML elements are allowed.";

	// Token: 0x04000056 RID: 86
	public const string Config_base_no_child_nodes = "Child nodes are not allowed.";

	// Token: 0x04000057 RID: 87
	public const string Config_base_file_load_exception_no_message = "Could not load the assembly. The property '{0}' must be a valid assembly.";

	// Token: 0x04000058 RID: 88
	public const string Config_base_bad_image_exception_no_message = "Could not load the assembly. The file image '{0}' is invalid.";

	// Token: 0x04000059 RID: 89
	public const string Config_base_report_exception_type = "Could not load the assembly.  The unexpected exception was '{0}'.";

	// Token: 0x0400005A RID: 90
	public const string Config_property_generic = "The configuration property is invalid.";

	// Token: 0x0400005B RID: 91
	public const string Config_section_not_supported = "The configuration section is not supported: {0}.";

	// Token: 0x0400005C RID: 92
	public const string Unable_To_Register_Assembly = "Unable to register assembly '{0}'.";

	// Token: 0x0400005D RID: 93
	public const string Unable_To_UnRegister_Assembly = "Unable to un-register assembly '{0}'.";

	// Token: 0x0400005E RID: 94
	public const string Could_not_create_type_instance = "Server could not create {0}.";

	// Token: 0x0400005F RID: 95
	public const string Config_Invalid_enum_value = "The enumeration value must be one of the following: {0}.";

	// Token: 0x04000060 RID: 96
	public const string Config_element_below_app_illegal = "The element '{0}' cannot be defined below the application level.";

	// Token: 0x04000061 RID: 97
	public const string Config_provider_must_exist = "The provider '{0}' specified for the defaultProvider does not exist in the providers collection.";

	// Token: 0x04000062 RID: 98
	public const string File_is_read_only = "The file '{0}' is marked as read-only.";

	// Token: 0x04000063 RID: 99
	public const string Can_not_access_files_other_than_config = "Only config files can be accessed by this feature.";

	// Token: 0x04000064 RID: 100
	public const string Error_loading_XML_file = "The XML file {0} could not be loaded.  {1}";

	// Token: 0x04000065 RID: 101
	public const string Unknown_tag_in_caps_config = "Server found the unknown tag <{0}> in capabilities configuration.";

	// Token: 0x04000066 RID: 102
	public const string Cannot_specify_test_without_match = "Cannot specify test without match.";

	// Token: 0x04000067 RID: 103
	public const string Result_must_be_at_the_top_browser_section = "<result> must be at the top of the <browsercaps> section";

	// Token: 0x04000068 RID: 104
	public const string Type_doesnt_inherit_from_type = "Type '{0}' does not inherit from '{1}'.";

	// Token: 0x04000069 RID: 105
	public const string Type_cannot_be_resolved = "The type '{0}' cannot be resolved. Please verify the spelling is correct or that the full type name is provided.";

	// Token: 0x0400006A RID: 106
	public const string Problem_reading_caps_config = "Server cannot read capabilities configuration {0}.";

	// Token: 0x0400006B RID: 107
	public const string Special_module_cannot_be_added_manually = "Special module of type '{0}' cannot be added or removed manually.";

	// Token: 0x0400006C RID: 108
	public const string Special_module_cannot_be_removed_manually = "Special module of type '{0}' cannot be removed manually.";

	// Token: 0x0400006D RID: 109
	public const string Module_not_in_app = "There is no '{0}' module in the application to remove.";

	// Token: 0x0400006E RID: 110
	public const string Invalid_credentials = "Invalid user credentials are specified in the config file.";

	// Token: 0x0400006F RID: 111
	public const string Auth_Invalid_Login_Url = "The login URL specified for authentication is not valid.";

	// Token: 0x04000070 RID: 112
	public const string Invalid_value_for_globalization_attr = "The <globalization> tag contains an invalid value for the '{0}' attribute.";

	// Token: 0x04000071 RID: 113
	public const string Invalid_credentials_2 = "Could not create Windows user token from the credentials specified in the config file. Error from the operating system '{0}'";

	// Token: 0x04000072 RID: 114
	public const string Invalid_registry_config = "Error reading configuration information from the registry.";

	// Token: 0x04000073 RID: 115
	public const string Invalid_Passport_Redirect_URL = "Redirect URL specified for Passport Authentication is invalid.";

	// Token: 0x04000074 RID: 116
	public const string Invalid_redirect_return_url = "The return URL specified for request redirection is invalid.";

	// Token: 0x04000075 RID: 117
	public const string Config_section_not_present = "The application's configuration files must contain '{0}' section.";

	// Token: 0x04000076 RID: 118
	public const string Local_free_threads_cannot_exceed_free_threads = "The value for 'minLocalRequestFreeThreads' cannot exceed the value for 'minFreeThreads'.";

	// Token: 0x04000077 RID: 119
	public const string Min_free_threads_must_be_under_thread_pool_limits = "The value for 'minFreeThreads' must be less than the thread pool limit of {0} threads.";

	// Token: 0x04000078 RID: 120
	public const string Thread_pool_limit_must_be_greater_than_minFreeThreads = "The value for 'maxWorkerThreads' and 'maxIoThreads' multiplied by the number of processors must be greater than the value of <httpRuntime minFreeThreads/>, which is currently set to {0}.";

	// Token: 0x04000079 RID: 121
	public const string Config_max_request_length_disk_threshold_exceeds_max_request_length = "The property 'RequestLengthDiskThreshold' must be less than or equal to the 'MaxRequestLength' property.";

	// Token: 0x0400007A RID: 122
	public const string Config_max_request_length_smaller_than_max_request_length_disk_threshold = "The property 'MaxRequestLength' must be greater than or equal to the 'RequestLengthDiskThreshold' property.";

	// Token: 0x0400007B RID: 123
	public const string Capability_file_root_element = "The root element of a capabilities file must be '{0}'.";

	// Token: 0x0400007C RID: 124
	public const string File_element_only_valid_in_config = "File elements are not allowed in external capability files.";

	// Token: 0x0400007D RID: 125
	public const string HttpRuntimeSection_TargetFramework_Invalid = "The targetFramework attribute must either be empty or contain a valid framework version string.";

	// Token: 0x0400007E RID: 126
	public const string Clear_not_valid = "Cannot specify <clear> on this node.";

	// Token: 0x0400007F RID: 127
	public const string Config_base_cannot_remove_inherited_items = "Inherited items may not be removed.";

	// Token: 0x04000080 RID: 128
	public const string Nested_group_not_valid = "Cannot nest the <group> element.";

	// Token: 0x04000081 RID: 129
	public const string Dup_protocol_id = "Duplicate protocol ID: '{0}'.";

	// Token: 0x04000082 RID: 130
	public const string WebPartsSection_NoVerbs = "The rule must contain at least one verb.  The valid verbs are 'enterSharedScope' and 'modifyState'.";

	// Token: 0x04000083 RID: 131
	public const string WebPartsSection_InvalidVerb = "The verb '{0}' is not valid.  The valid verbs are 'enterSharedScope' and 'modifyState'.";

	// Token: 0x04000084 RID: 132
	public const string Transformer_types_already_added = "A transformer with name '{0}' has already been added, and has the same provider and consumer types as the transformer with name '{1}'.";

	// Token: 0x04000085 RID: 133
	public const string Transformer_attribute_error = "Error reading WebPartTransformerAttribute: '{0}'";

	// Token: 0x04000086 RID: 134
	public const string File_changed_since_read = "The file '{0}' has changed since it was read.";

	// Token: 0x04000087 RID: 135
	public const string Config_invalid_element = "Invalid element is detected: '{0}'.";

	// Token: 0x04000088 RID: 136
	public const string Config_control_rendering_compatibility_version_is_less_than_minimum_version = "The control rendering compatibility version must not be less than 3.5.";

	// Token: 0x04000089 RID: 137
	public const string InvalidExpressionSyntax = "The expression '{0}' is invalid. Expressions use the syntax <%$ prefix:value %>.";

	// Token: 0x0400008A RID: 138
	public const string InvalidExpressionPrefix = "The expression prefix '{0}' was not recognized.  Please correct the prefix or register the prefix in the <expressionBuilders> section of configuration.";

	// Token: 0x0400008B RID: 139
	public const string ExpressionBuilder_InvalidType = "The type '{0}' is not an ExpressionBuilder.";

	// Token: 0x0400008C RID: 140
	public const string MissingExpressionPrefix = "The expression '{0}' does not have a prefix.";

	// Token: 0x0400008D RID: 141
	public const string MissingExpressionValue = "The expression '{0}' does not have a value.";

	// Token: 0x0400008E RID: 142
	public const string ExpressionBuilder_LiteralExpressionsNotAllowed = "Literal expressions like '{0}' are not allowed. Use <asp:Literal runat=\"server\" Text=\"<%{1}%>\" /> instead.";

	// Token: 0x0400008F RID: 143
	public const string ResourceExpresionBuilder_PageResourceNotFound = "The resource class for this page was not found.  Please check if the resource file exists and try again.";

	// Token: 0x04000090 RID: 144
	public const string Failed_to_start_monitoring = "Failed to start monitoring changes to '{0}'.";

	// Token: 0x04000091 RID: 145
	public const string Invalid_file_name_for_monitoring = "Invalid file name for file monitoring: '{0}'. Common reasons for failure include:\r\n- The filename is not a valid Win32 file name.\r\n- The filename is not an absolute path.\r\n- The filename contains wildcard characters.\r\n- The file specified is a directory.\r\n- Access denied.";

	// Token: 0x04000092 RID: 146
	public const string Access_denied_for_monitoring = "Failed to start monitoring changes to '{0}' because access is denied.";

	// Token: 0x04000093 RID: 147
	public const string Directory_does_not_exist_for_monitoring = "Directory '{0}' does not exist. Failed to start monitoring file changes.";

	// Token: 0x04000094 RID: 148
	public const string NetBios_command_limit_reached = "Failed to start monitoring changes to '{0}' because the network BIOS command limit has been reached. For more information on this error, please refer to Microsoft knowledge base article 810886. Hosting on a UNC share is not supported for the Windows XP Platform.";

	// Token: 0x04000095 RID: 149
	public const string Directory_rename_notification = "Directory rename change notification for '{0}'.";

	// Token: 0x04000096 RID: 150
	public const string Change_notification_critical_dir = "Change Notification for critical directories.";

	// Token: 0x04000097 RID: 151
	public const string Path_not_found = "Path '{0}' was not found.";

	// Token: 0x04000098 RID: 152
	public const string Path_forbidden = "Path '{0}' is forbidden.";

	// Token: 0x04000099 RID: 153
	public const string Method_for_path_not_implemented = "{0} {1} is not implemented.";

	// Token: 0x0400009A RID: 154
	public const string Method_not_allowed = "The HTTP verb {0} used to access path '{1}' is not allowed.";

	// Token: 0x0400009B RID: 155
	public const string Cannot_call_defaulthttphandler_sync = "DefaultHttpHandler must be called asynchronously using BeginProcessRequest.";

	// Token: 0x0400009C RID: 156
	public const string Handler_access_denied = "The handler must be granted Script or Execute permission in order to execute.  This is set via &lt;system.webServer&gt;\\&lt;handlers accessPolicy&gt; configuration.";

	// Token: 0x0400009D RID: 157
	public const string Debugging_forbidden = "{0} application debugging not enabled.";

	// Token: 0x0400009E RID: 158
	public const string Debug_Access_Denied = "Debug access denied to '{0}'.";

	// Token: 0x0400009F RID: 159
	public const string Invalid_Debug_Request = "DEBUG request is not valid.";

	// Token: 0x040000A0 RID: 160
	public const string Invalid_Debug_ID = "DebugSessionID corrupted or not provided.";

	// Token: 0x040000A1 RID: 161
	public const string Error_Attaching_with_MDM = "HRESULT={0};ErrorString=Unable to do an AutoAttach.";

	// Token: 0x040000A2 RID: 162
	public const string VaryByCustom_already_set = "VaryByCustom is already set.";

	// Token: 0x040000A3 RID: 163
	public const string CacheProfile_Not_Found = "The '{0}' cache profile is not defined.  Please define it in the configuration file.";

	// Token: 0x040000A4 RID: 164
	public const string Invalid_operation_cache_dependency = "The cache dependencies for the response have already been determined and cannot be modified.";

	// Token: 0x040000A5 RID: 165
	public const string Invalid_sqlDependency_argument = "The '{0}' SqlDependency attribute for OutputCache directive is invalid.\r\n\r\nFor SQL Server 7.0 and SQL Server 2000, the valid format is \"database:tablename\", and table name must conform to the format of regular identifiers in SQL. To specify multiple pairs of values, use the ';' separator between pairs. (To specify ':', '\\' or ';', prefix it with the '\\' escape character.)\r\n\r\nFor dependencies that use SQL Server 9.0 notifications, specify the value 'CommandNotification'.";

	// Token: 0x040000A6 RID: 166
	public const string Invalid_sqlDependency_argument2 = "The '{0}' SqlDependency attribute for OutputCache directive is invalid.\r\n\r\nDetailed error message: {1}";

	// Token: 0x040000A7 RID: 167
	public const string Etag_already_set = "Etag is already set.";

	// Token: 0x040000A8 RID: 168
	public const string Cant_both_set_and_generate_Etag = "Cannot set ETag and also generate it from files.";

	// Token: 0x040000A9 RID: 169
	public const string Cacheability_for_field_must_be_private_or_nocache = "Cacheability for a field must either be Private or NoCache.";

	// Token: 0x040000AA RID: 170
	public const string Cache_dependency_used_more_that_once = "An attempt was made to reference a CacheDependency object from more than one Cache entry.";

	// Token: 0x040000AB RID: 171
	public const string Invalid_expiration_combination = "absoluteExpiration must be DateTime.MaxValue or slidingExpiration must be timeSpan.Zero.";

	// Token: 0x040000AC RID: 172
	public const string Invalid_Dependency_Type = "dependency argument to CacheDependency constructor can only be of type System.Web.Caching.CacheDependency";

	// Token: 0x040000AD RID: 173
	public const string Invalid_Parameters_To_Insert = "One of the following parameters must be specified: dependencies, absoluteExpiration, slidingExpiration.";

	// Token: 0x040000AE RID: 174
	public const string Invalid_sql_cache_dep_polltime = "The property 'pollTime' must be an integer value greater than or equal to 500(ms).";

	// Token: 0x040000AF RID: 175
	public const string Database_not_found = "Cannot find the '{0}' database in the configuration.";

	// Token: 0x040000B0 RID: 176
	public const string Cant_connect_sql_cache_dep_database_polling = "Unable to connect to SQL database '{0}' for cache dependency polling.";

	// Token: 0x040000B1 RID: 177
	public const string Cant_connect_sql_cache_dep_database_admin = "Unable to connect to the SQL database for cache dependency registration.";

	// Token: 0x040000B2 RID: 178
	public const string Cant_connect_sql_cache_dep_database_admin_cmdtxt = "Failed during cache dependency registration.\r\n\r\nPlease make sure the database name and the table name are valid. Table names must conform to the format of regular identifiers in SQL.\r\n\r\nThe failing SQL command is:\r\n{0}\r\n";

	// Token: 0x040000B3 RID: 179
	public const string Database_not_enabled_for_notification = "The database '{0}' is not enabled for SQL cache notification.\r\n\r\nTo enable a database for SQL cache notification, please use the System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications method, or the command line tool aspnet_regsql. To use the tool, please run 'aspnet_regsql.exe -?' for more information.";

	// Token: 0x040000B4 RID: 180
	public const string Table_not_enabled_for_notification = "The '{0}' table in the database '{1}' is not enabled for SQL cache notification.\r\n\r\nPlease make sure the table exists, and the table name used for cache dependency matches exactly the table name used in cache notification registration.\r\n\r\nTo enable a table for SQL cache notification, please use SqlCacheDependencyAdmin.EnableTableForNotifications method, or the command line tool aspnet_regsql. To use the tool, please run 'aspnet_regsql.exe -?' for more information.\r\n\r\nTo get a list of enabled tables in the database, please use SqlCacheDependencyManager.GetTablesEnabledForNotifications method, or the command line tool aspnet_regsql.exe.";

	// Token: 0x040000B5 RID: 181
	public const string Polling_not_enabled_for_sql_cache = "SQL cache notification is not enabled. To enable SQL cache dependency, please set the 'enabled' attribute to \"true\" in the <sqlCacheDependency> section in the configuration file.";

	// Token: 0x040000B6 RID: 182
	public const string Polltime_zero_for_database_sql_cache = "SQL cache notification is not enabled for the database '{0}' because the pollTime attribute is set to zero in the configuration.";

	// Token: 0x040000B7 RID: 183
	public const string Permission_denied_database_polling = "SQL cache dependency polling failed for SQL database '{0}' because of permission problem.";

	// Token: 0x040000B8 RID: 184
	public const string Permission_denied_database_enable_notification = "You do not have sufficient permission on the database to enable SQL cache notification. You need to use a database owner account, such as 'sa' to add roles, to create the required tables and stored procedures, and to grant EXECUTE permission on those stored procedures.";

	// Token: 0x040000B9 RID: 185
	public const string Permission_denied_table_enable_notification = "You do not have sufficient permission on the database to enable SQL cache notification for the table '{0}'. You need the permission to create a trigger on the table '{0}', and to insert a row into the table 'AspNet_SqlCacheTablesForChangeNotification'.";

	// Token: 0x040000BA RID: 186
	public const string Permission_denied_database_disable_notification = "You do not have sufficient permission on the database to disable SQL cache notification. You need the permission to remove the table 'AspNet_SqlCacheTablesForChangeNotification'.";

	// Token: 0x040000BB RID: 187
	public const string Permission_denied_table_disable_notification = "You do not have sufficient permission on the database to disable SQL cache notification for the table '{0}'. You need the permission to remove a trigger from the table '{0}', and to remove a row from the table 'AspNet_SqlCacheTablesForChangeNotification'.";

	// Token: 0x040000BC RID: 188
	public const string Cant_get_enabled_tables_sql_cache_dep = "Cannot get the tables enabled for SQL cache notification.";

	// Token: 0x040000BD RID: 189
	public const string Cant_disable_table_sql_cache_dep = "Cannot disable a table for SQL cache notification because the database is not enabled for SQL cache notification.";

	// Token: 0x040000BE RID: 190
	public const string Cache_null_table = "The table name parameter cannot be null or have a length of zero.";

	// Token: 0x040000BF RID: 191
	public const string Cache_null_table_in_tables = "The tables parameter cannot contain a table which is null or has a length of zero.";

	// Token: 0x040000C0 RID: 192
	public const string Cache_dep_table_not_found = "The table '{0}' cannot be found in the database.";

	// Token: 0x040000C1 RID: 193
	public const string UC_not_cached = "The CachePolicy cannot be used, since the control is not being cached.";

	// Token: 0x040000C2 RID: 194
	public const string UCCachePolicy_unavailable = "The CachePolicy cannot be used after the PreRender phase.";

	// Token: 0x040000C3 RID: 195
	public const string SqlCacheDependency_permission_denied = "Failed to use SqlCacheDependency because permission is denied. In order to use SqlCacheDependency, the application needs to be granted unrestricted SqlClientPermission. Please check with your administrator if the application does not have this permission.";

	// Token: 0x040000C4 RID: 196
	public const string No_UniqueId_Cache_Dependency = "Unable to generate etag from dependencies. One of the dependencies couldn't generate a unique id.";

	// Token: 0x040000C5 RID: 197
	public const string SqlCacheDependency_OutputCache_Conflict = "The page already has the 'SqlDependency=\"CommandNotification\"' attribute specified in the OutputCache directive.  On such a page, it is an error to create a SqlCacheDependency object using a SqlCommand object.\r\n\r\nThere are two ways to solve this problem:\r\n1.Do not use the 'SqlDependency=\"CommandNotification\"' attribute in the OutputCache directive.\r\n2.On the SqlCommand object set SqlCommand.NotificationAutoEnlist to false.  The side effect is that that SqlCommand object will not affect the output caching of the page.";

	// Token: 0x040000C6 RID: 198
	public const string Cache_not_available = "Cache is not available";

	// Token: 0x040000C7 RID: 199
	public const string Http_handler_not_found_for_request_type = "No http handler was found for request type '{0}'";

	// Token: 0x040000C8 RID: 200
	public const string Request_not_available = "Request is not available in this context";

	// Token: 0x040000C9 RID: 201
	public const string Response_not_available = "Response is not available in this context.";

	// Token: 0x040000CA RID: 202
	public const string Session_not_available = "Session state is not available in this context.";

	// Token: 0x040000CB RID: 203
	public const string Server_not_available = "Server operation is not available in this context.";

	// Token: 0x040000CC RID: 204
	public const string User_not_available = "User is not available in this context.";

	// Token: 0x040000CD RID: 205
	public const string Sync_not_supported = "Synchronous request processing is not supported.";

	// Token: 0x040000CE RID: 206
	public const string Type_not_factory_or_handler = "{0} does not implement IHttpHandlerFactory or IHttpHandler.";

	// Token: 0x040000CF RID: 207
	public const string Type_from_untrusted_assembly = "Type '{0}' cannot be instantiated under a partially trusted security policy (AllowPartiallyTrustedCallersAttribute is not present on the target assembly).";

	// Token: 0x040000D0 RID: 208
	public const string Type_not_module = "{0} does not implement IHttpModule.";

	// Token: 0x040000D1 RID: 209
	public const string Request_timed_out = "Request timed out.";

	// Token: 0x040000D2 RID: 210
	public const string DynamicModuleRegistrationOff = "RegisterModule cannot be called when <httpRuntime allowDynamicModuleRegistration='false'>.";

	// Token: 0x040000D3 RID: 211
	public const string Invalid_ControlState = "The state information is invalid for this page and might be corrupted.";

	// Token: 0x040000D4 RID: 212
	public const string Too_late_for_ViewStateUserKey = "The ViewStateUserKey property needs to be set during Page_Init.";

	// Token: 0x040000D5 RID: 213
	public const string Too_late_for_RegisterRequiresViewStateEncryption = "The RegisterRequiresViewStateEncryption() method needs to be called before or during Page_PreRender.";

	// Token: 0x040000D6 RID: 214
	public const string MethodCannotBeCalledAfterAppStart = "This method cannot be called after Application_Start.";

	// Token: 0x040000D7 RID: 215
	public const string Invalid_urlencoded_form_data = "The URL-encoded form data is not valid.";

	// Token: 0x040000D8 RID: 216
	public const string Invalid_request_filter = "The request filter is not valid.";

	// Token: 0x040000D9 RID: 217
	public const string Cannot_map_path_without_context = "Server cannot map the path without the full request context.";

	// Token: 0x040000DA RID: 218
	public const string Cross_app_not_allowed = "The virtual path '{0}' maps to another application, which is not allowed.";

	// Token: 0x040000DB RID: 219
	public const string Max_request_length_exceeded = "Maximum request length exceeded.";

	// Token: 0x040000DC RID: 220
	public const string Dangerous_input_detected = "A potentially dangerous {0} value was detected from the client ({1}).";

	// Token: 0x040000DD RID: 221
	public const string Dangerous_input_detected_descr = "ASP.NET has detected data in the request that is potentially dangerous because it might include HTML markup or script. The data might represent an attempt to compromise the security of your application, such as a cross-site scripting attack. If this type of input is appropriate in your application, you can include code in a web page to explicitly allow it. For more information, see http://go.microsoft.com/fwlink/?LinkID=212874.";

	// Token: 0x040000DE RID: 222
	public const string CollectionCountExceeded_HttpValueCollection = "The maximum number of form, query string, or posted file items has already been read from the request. To change the maximum allowed request collection count from its current value of {0}, change the \"aspnet:MaxHttpCollectionKeys\" setting. See http://go.microsoft.com/fwlink/?LinkId=238386 for more information.";

	// Token: 0x040000DF RID: 223
	public const string CollectionCountExceeded_JavaScriptObjectDeserializer = "The maximum number of items has already been deserialized into a single dictionary by the JavaScriptSerializer. To change the maximum allowed JSON dictionary entry count from its current value of {0}, change the \"aspnet:MaxJsonDeserializerMembers\" setting. See http://go.microsoft.com/fwlink/?LinkId=238386 for more information.";

	// Token: 0x040000E0 RID: 224
	public const string Invalid_substitution_callback = "Invalid callback. Only static methods on controls, or methods on other objects, are allowed for substitution callbacks.";

	// Token: 0x040000E1 RID: 225
	public const string Url_too_long = "The length of the URL for this request exceeds the configured maxUrlLength value.";

	// Token: 0x040000E2 RID: 226
	public const string QueryString_too_long = "The length of the query string for this request exceeds the configured maxQueryStringLength value.";

	// Token: 0x040000E3 RID: 227
	public const string Using_BufferlessStream_API_Not_Supported = "This API is not supported when using the HttpBufferlessInputStream.";

	// Token: 0x040000E4 RID: 228
	public const string Using_InputStream_API_Not_Supported = "Request.BufferlessInputStream cannot be used because the Request's contents have already been read.";

	// Token: 0x040000E5 RID: 229
	public const string Cannot_get_snapshot_if_not_buffered = "Server cannot get response snapshot if responses are not buffered.";

	// Token: 0x040000E6 RID: 230
	public const string Cannot_use_snapshot_after_headers_sent = "Server cannot use snapshot after HTTP headers have been sent.";

	// Token: 0x040000E7 RID: 231
	public const string Cannot_use_snapshot_for_TextWriter = "Server cannot use snapshot for TextWriter responses.";

	// Token: 0x040000E8 RID: 232
	public const string Cannot_set_status_after_headers_sent = "Server cannot set status after HTTP headers have been sent.";

	// Token: 0x040000E9 RID: 233
	public const string Cannot_set_content_type_after_headers_sent = "Server cannot set content type after HTTP headers have been sent.";

	// Token: 0x040000EA RID: 234
	public const string Filtering_not_allowed = "Filtering is not allowed.";

	// Token: 0x040000EB RID: 235
	public const string Cannot_append_header_after_headers_sent = "Server cannot append header after HTTP headers have been sent.";

	// Token: 0x040000EC RID: 236
	public const string Cannot_append_cookie_after_headers_sent = "Server cannot append cookies after HTTP headers have been sent.";

	// Token: 0x040000ED RID: 237
	public const string Cannot_modify_cookies_after_headers_sent = "Server cannot modify cookies after HTTP headers have been sent.";

	// Token: 0x040000EE RID: 238
	public const string Cannot_clear_headers_after_headers_sent = "Server cannot clear headers after HTTP headers have been sent.";

	// Token: 0x040000EF RID: 239
	public const string Cannot_call_method_after_headers_sent_generic = "This functionality is unavailable after HTTP headers have been sent.";

	// Token: 0x040000F0 RID: 240
	public const string Cannot_flush_completed_response = "Server cannot flush a completed response.";

	// Token: 0x040000F1 RID: 241
	public const string No_Route_Found_For_Redirect = "No matching route found for RedirectToRoute.";

	// Token: 0x040000F2 RID: 242
	public const string Cannot_redirect_after_headers_sent = "Cannot redirect after HTTP headers have been sent.";

	// Token: 0x040000F3 RID: 243
	public const string Cannot_set_header_encoding_after_headers_sent = "Cannot change headers encoding after HTTP headers have been sent.";

	// Token: 0x040000F4 RID: 244
	public const string Invalid_header_encoding = "'{0}' cannot be used as Header Encoding.";

	// Token: 0x040000F5 RID: 245
	public const string Cannot_redirect_to_newline = "Redirect URI cannot contain newline characters.";

	// Token: 0x040000F6 RID: 246
	public const string Invalid_status_string = "HTTP status string is not valid.";

	// Token: 0x040000F7 RID: 247
	public const string Invalid_value_for_CacheControl = "Property value for CacheControl is not valid. Value={0}.";

	// Token: 0x040000F8 RID: 248
	public const string OutputStream_NotAvail = "OutputStream is not available when a custom TextWriter is used.";

	// Token: 0x040000F9 RID: 249
	public const string Information_Disclosure_Warning = "This error page might contain sensitive information because ASP.NET is configured to show verbose error messages using &lt;customErrors mode=\"Off\"/&gt;. Consider using &lt;customErrors mode=\"On\"/&gt; or &lt;customErrors mode=\"RemoteOnly\"/&gt; in production environments.";

	// Token: 0x040000FA RID: 250
	public const string InvalidOffsetOrCount = "The sum of {0} and {1} is greater than the length of the buffer.";

	// Token: 0x040000FB RID: 251
	public const string Access_denied_to_app_dir = "Server cannot access application directory '{0}'. The directory does not exist or is not accessible because of security settings.";

	// Token: 0x040000FC RID: 252
	public const string Access_denied_to_unicode_app_dir = "The server cannot access the application directory {0}.  This may be due to the presence of Unicode characters in the URL.  Unicode characters are supported only when running IIS 6 or newer in worker process isolation mode.";

	// Token: 0x040000FD RID: 253
	public const string Access_denied_to_path = "Access to path '{0}' was denied. The location does not exist or is not accessible because of security settings.";

	// Token: 0x040000FE RID: 254
	public const string Insufficient_trust_for_attribute = "The current trust level does not allow use of the '{0}' attribute";

	// Token: 0x040000FF RID: 255
	public const string XSP_init_error = "ASP.NET Initialization Error: {0}";

	// Token: 0x04000100 RID: 256
	public const string Unable_create_app_object = "Could not create application object.";

	// Token: 0x04000101 RID: 257
	public const string Could_not_create_type = "Could not create type '{0}'.";

	// Token: 0x04000102 RID: 258
	public const string StateManagedCollection_InvalidIndex = "Insertion index was out of range. Must be non-negative and less than or equal to size.";

	// Token: 0x04000103 RID: 259
	public const string StateManagedCollection_NoKnownTypes = "When implementing a derived StateManagedCollection, the implementation of CreateKnownType() must match the implementation of GetKnownTypes().";

	// Token: 0x04000104 RID: 260
	public const string VirtualPath_Length_Zero = "The file's virtual path must have a length greater than zero.";

	// Token: 0x04000105 RID: 261
	public const string Invalid_app_VirtualPath = "The virtual path '{0}' is not a valid absolute virtual path within the current application.";

	// Token: 0x04000106 RID: 262
	public const string Collection_CantAddNull = "Cannot add null to this collection.";

	// Token: 0x04000107 RID: 263
	public const string Collection_InvalidType = "Only objects of type {0} can be used with this collection.";

	// Token: 0x04000108 RID: 264
	public const string VirtualPath_AllowAppRelativePath = "The application relative virtual path '{0}' is not allowed here.";

	// Token: 0x04000109 RID: 265
	public const string VirtualPath_AllowRelativePath = "The relative virtual path '{0}' is not allowed here.";

	// Token: 0x0400010A RID: 266
	public const string VirtualPath_AllowAbsolutePath = "The absolute virtual path '{0}' is not allowed here.";

	// Token: 0x0400010B RID: 267
	public const string VirtualPath_CantMakeAppRelative = "The absolute virtual path '{0}' cannot be made application relative, because the path to the application is not known.";

	// Token: 0x0400010C RID: 268
	public const string VirtualPath_CantMakeAppAbsolute = "The application relative virtual path '{0}' cannot be made absolute, because the path to the application is not known.";

	// Token: 0x0400010D RID: 269
	public const string Bad_VirtualPath_in_VirtualFileBase = "The VirtualPathProvider returned a {0} object with VirtualPath set to '{1}' instead of the expected '{2}'.";

	// Token: 0x0400010E RID: 270
	public const string ControlRenderedOutsideServerForm = "Control '{0}' of type '{1}' must be placed inside a form tag with runat=server.";

	// Token: 0x0400010F RID: 271
	public const string RequiresNT = "This operation requires Windows NT, Windows 2000, or Windows XP.";

	// Token: 0x04000110 RID: 272
	public const string ListEnumVersionMismatch = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000111 RID: 273
	public const string ListEnumCurrentOutOfRange = "The enumerator's current position is out of bounds of the list.";

	// Token: 0x04000112 RID: 274
	public const string HTMLTextWriterUnbalancedPop = "A PopEndTag was called without a corresponding PushEndTag.";

	// Token: 0x04000113 RID: 275
	public const string Server_too_busy = "Server Too Busy";

	// Token: 0x04000114 RID: 276
	public const string InvalidArgumentValue = "Invalid value for '{0}' parameter.";

	// Token: 0x04000115 RID: 277
	public const string CompilationMutex_Create = "Mutex could not be created.";

	// Token: 0x04000116 RID: 278
	public const string CompilationMutex_Null = "Mutex state is invalid.";

	// Token: 0x04000117 RID: 279
	public const string CompilationMutex_Drained = "Application is restarting.";

	// Token: 0x04000118 RID: 280
	public const string CompilationMutex_Failed = "Failed to acquire mutex lock.";

	// Token: 0x04000119 RID: 281
	public const string Failed_to_create_temp_dir = "Failed to create temporary files directory '{0}'. Access denied.";

	// Token: 0x0400011A RID: 282
	public const string Failed_to_execute_child_request = "Failed to execute child request.";

	// Token: 0x0400011B RID: 283
	public const string Cannot_impersonate = "An error occurred while attempting to impersonate.  Execution of this request cannot continue.";

	// Token: 0x0400011C RID: 284
	public const string No_codegen_access = "The current identity ({0}) does not have write access to '{1}'.";

	// Token: 0x0400011D RID: 285
	public const string Transaction_not_supported_in_low_trust = "Transactions are not supported under current trust level settings.";

	// Token: 0x0400011E RID: 286
	public const string Debugging_not_supported_in_low_trust = "Debugging is not supported under current trust level settings.";

	// Token: 0x0400011F RID: 287
	public const string Session_state_need_higher_trust = "Access to session state is not supported under current trust level settings.";

	// Token: 0x04000120 RID: 288
	public const string ExecuteUrl_not_supported = "Execute URL is not supported on this platform.";

	// Token: 0x04000121 RID: 289
	public const string Cannot_execute_url_in_this_context = "Insufficient context to Execute URL.";

	// Token: 0x04000122 RID: 290
	public const string Failed_to_execute_url = "Failed to Execute URL.";

	// Token: 0x04000123 RID: 291
	public const string Aspnet_not_installed = "ASP.NET ({0}) is not installed on this machine.";

	// Token: 0x04000124 RID: 292
	public const string Failed_to_initialize_AppDomain = "Failed to initialize the AppDomain:";

	// Token: 0x04000125 RID: 293
	public const string Cannot_create_AppDomain = "Failed to create AppDomain.";

	// Token: 0x04000126 RID: 294
	public const string Cannot_create_HostEnv = "Failed to create HostingEnvironment.";

	// Token: 0x04000127 RID: 295
	public const string Unknown_protocol_id = "Unknown protocol ID '{0}'.";

	// Token: 0x04000128 RID: 296
	public const string Only_1_HostEnv = "Only one HostingEnvironment is allowed in an AppDomain.";

	// Token: 0x04000129 RID: 297
	public const string Not_IRegisteredObject = "Type '{0}' does not implement IRegisteredObject interface.";

	// Token: 0x0400012A RID: 298
	public const string Wellknown_object_already_exists = "Well known object of type '{0}' already exists in this App Domain.";

	// Token: 0x0400012B RID: 299
	public const string Invalid_IIS_app = "'{0}' is not a valid IIS application.";

	// Token: 0x0400012C RID: 300
	public const string App_Virtual_Path = "Application virtual path: '{0}'";

	// Token: 0x0400012D RID: 301
	public const string Hosting_Phys_Path_Changed = "Physical application path changed from {0} to {1}.";

	// Token: 0x0400012E RID: 302
	public const string App_Domain_Restart = "AppDomainRestart";

	// Token: 0x0400012F RID: 303
	public const string Hosting_Env_Restart = "HostingEnvironment caused shutdown";

	// Token: 0x04000130 RID: 304
	public const string Hosting_Env_IdleTimeout = "Idle timeout";

	// Token: 0x04000131 RID: 305
	public const string Unhandled_Exception = "An unhandled exception occurred and the process was terminated.";

	// Token: 0x04000132 RID: 306
	public const string Provider_must_implement_the_interface = "The provider class '{0}' must implement the class '{1}'.";

	// Token: 0x04000133 RID: 307
	public const string Permission_set_not_found = "Could not find permission set named '{0}'.";

	// Token: 0x04000134 RID: 308
	public const string Require_stable_string_hash_codes = "ASP.NET cannot operate when String hash code randomization is enabled for the current machine. Please verify that the registry key HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NETFramework\\UseRandomizedStringHashAlgorithm does not exist or is set to [DWORD] 0.";

	// Token: 0x04000135 RID: 309
	public const string Server_variable_cannot_be_modified = "This server variable cannot be modified during request execution.";

	// Token: 0x04000136 RID: 310
	public const string Cache_url_invalid = "The format of the CACHE_URL server variable is invalid.";

	// Token: 0x04000137 RID: 311
	public const string Invalid_range = "Specified range is not valid.";

	// Token: 0x04000138 RID: 312
	public const string Invalid_use_of_response_filter = "Invalid use of response filter";

	// Token: 0x04000139 RID: 313
	public const string Invalid_response_filter = "Response filter is not valid.";

	// Token: 0x0400013A RID: 314
	public const string Invalid_size = "The size parameter must be between zero and the maximum Int32 value.";

	// Token: 0x0400013B RID: 315
	public const string Process_information_not_available = "Process metrics are available only when the ASP.NET process model is enabled.  When running on versions of IIS 6 or newer in worker process isolation mode, this feature is not supported.";

	// Token: 0x0400013C RID: 316
	public const string Error_trying_to_enumerate_files = "Server encountered an error while enumerating files.";

	// Token: 0x0400013D RID: 317
	public const string File_enumerator_access_denied = "File enumerator access was denied.";

	// Token: 0x0400013E RID: 318
	public const string File_does_not_exist = "File does not exist.";

	// Token: 0x0400013F RID: 319
	public const string File_is_hidden = "File is hidden.";

	// Token: 0x04000140 RID: 320
	public const string Missing_star_mapping = "Missing */ handler mapping.  The server cannot handle the directory.";

	// Token: 0x04000141 RID: 321
	public const string Resource_access_forbidden = "Access to resource is forbidden.";

	// Token: 0x04000142 RID: 322
	public const string SMTP_TypeCreationError = "Could not create an object of type '{0}'.  Please verify that the current platform configuration supports SMTP mail.";

	// Token: 0x04000143 RID: 323
	public const string Could_not_create_object_of_type = "Could not create an object of type '{0}'.";

	// Token: 0x04000144 RID: 324
	public const string Could_not_create_object_from_clsid = "Could not create an object with CLASSID '{0}'.";

	// Token: 0x04000145 RID: 325
	public const string Error_executing_child_request_for_path = "Error executing child request for {0}.";

	// Token: 0x04000146 RID: 326
	public const string Error_executing_child_request_for_handler = "Error executing child request for handler '{0}'.";

	// Token: 0x04000147 RID: 327
	public const string Invalid_path_for_child_request = "Invalid path for child request '{0}'. A virtual path is expected.";

	// Token: 0x04000148 RID: 328
	public const string Transacted_page_calls_aspcompat = "All pages that invoke other pages with aspcompat enabled must also have a <%@ page aspcompat=true %> directive.";

	// Token: 0x04000149 RID: 329
	public const string Invalid_path_for_remove = "Invalid path for HttpResponse.RemoveOutputCacheItem '{0}'. An absolute virtual path is expected.";

	// Token: 0x0400014A RID: 330
	public const string Get_computer_name_failed = "Could not obtain computer name.";

	// Token: 0x0400014B RID: 331
	public const string Cannot_map_path = "Failed to map the path '{0}'.";

	// Token: 0x0400014C RID: 332
	public const string Cannot_access_mappath_title = "Failed to access IIS metabase.";

	// Token: 0x0400014D RID: 333
	public const string Cannot_access_mappath_details = "The process account used to run ASP.NET must have read access to the IIS metabase (e.g. IIS://servername/W3SVC).   For information on modifying metabase permissions, please see <a href=\"http://support.microsoft.com/?kbid=267904\">http://support.microsoft.com/?kbid=267904</a>.";

	// Token: 0x0400014E RID: 334
	public const string Cannot_retrieve_request_data = "Cannot retrieve the basic request data.";

	// Token: 0x0400014F RID: 335
	public const string Cannot_read_posted_data = "Cannot read the posted data.";

	// Token: 0x04000150 RID: 336
	public const string Cannot_get_query_string = "Cannot get the query string.";

	// Token: 0x04000151 RID: 337
	public const string Cannot_get_query_string_bytes = "Cannot get query string bytes.";

	// Token: 0x04000152 RID: 338
	public const string Not_supported = "The operation is not supported.";

	// Token: 0x04000153 RID: 339
	public const string GetGacLocaltion_failed = "Unable to get the Global Assembly Cache path.";

	// Token: 0x04000154 RID: 340
	public const string Server_Support_Function_Error = "An error occurred while communicating with the remote host. The error code is 0x{0}.";

	// Token: 0x04000155 RID: 341
	public const string Server_Support_Function_Error_Disconnect = "The remote host closed the connection. The error code is 0x{0}.";

	// Token: 0x04000156 RID: 342
	public const string MachineKeyDataProtectorFactory_FactoryCreationFailed = "Could not create an instance of the configured DataProtector type.";

	// Token: 0x04000157 RID: 343
	public const string MachineKey_InvalidPurpose = "If a list of purposes is specified, the list cannot contain null entries or entries that are comprised solely of whitespace characters.";

	// Token: 0x04000158 RID: 344
	public const string Provider_Schema_Version_Not_Match = "The '{0}' requires a database schema compatible with schema version '{1}'.  However, the current database schema is not compatible with this version.  You may need to either install a compatible schema with aspnet_regsql.exe (available in the framework installation directory), or upgrade the provider to a newer version.";

	// Token: 0x04000159 RID: 345
	public const string Could_not_create_passport_identity = "An error occurred while trying to create a passport identity.  Please ensure that Passport Manager is installed on the machine and correctly configured.";

	// Token: 0x0400015A RID: 346
	public const string Passport_method_failed = "A call to a Passport Manager method has failed.  Please ensure that Passport Manager is installed on the machine and correctly configured.";

	// Token: 0x0400015B RID: 347
	public const string Auth_rule_names_cant_contain_char = "Authorization rule names cannot contain the '{0}' character.";

	// Token: 0x0400015C RID: 348
	public const string Auth_rule_must_specify_users_andor_roles = "Authorization rule must specify a list of users and/or roles.";

	// Token: 0x0400015D RID: 349
	public const string PageIndex_bad = "The pageIndex must be greater than or equal to zero.";

	// Token: 0x0400015E RID: 350
	public const string PageSize_bad = "The pageSize must be greater than zero.";

	// Token: 0x0400015F RID: 351
	public const string PageIndex_PageSize_bad = "The combination of pageIndex and pageSize cannot exceed the maximum value of System.Int32.";

	// Token: 0x04000160 RID: 352
	public const string Bad_machine_key = "Machine decryption key is invalid. It should be either \"AutoGenerate\", or 16 (for DES) or 48 (for 3DES and AES) Hex chars long, and may be followed by \",IsolateApps\". Exception message from the underlying layer: {0}";

	// Token: 0x04000161 RID: 353
	public const string PassportAuthFailed = "<html><head><title>Passport Sign-in Required</title></head><body>\r\n<h1>Access Denied</h1><p><h3>You must sign in with valid or different Microsoft <sup>&reg;</sup> .NET Passport credentials to access this page.</h3><p> {0} </body></html>\r\n";

	// Token: 0x04000162 RID: 354
	public const string PassportAuthFailed_Title = "Passport Sign-in Required";

	// Token: 0x04000163 RID: 355
	public const string PassportAuthFailed_Description = "You must sign in with valid or different Microsoft .NET Passport credentials to access this page.";

	// Token: 0x04000164 RID: 356
	public const string Unable_to_encrypt_cookie_ticket = "Unable to encrypt the authentication ticket. Try changing the decryption key configured for this application.";

	// Token: 0x04000165 RID: 357
	public const string Unable_to_get_cookie_authentication_validation_key = "Machine validation key is invalid. It is '{0}' chars long. It should be either \"AutoGenerate\" or between 40 and 128 Hex chars long, and may be followed by \",IsolateApps\".";

	// Token: 0x04000166 RID: 358
	public const string Unable_to_validate_data = "Unable to validate data.";

	// Token: 0x04000167 RID: 359
	public const string Unable_to_get_policy_file = "Unable to read the security policy file for trust level '{0}'.";

	// Token: 0x04000168 RID: 360
	public const string Wrong_validation_enum = "The validation must be one of these values: SHA1, HMACSHA256, HMACSHA384, HMACSHA512, MD5, 3DES, AES, or alg:[HashAlgorithm].";

	// Token: 0x04000169 RID: 361
	public const string Wrong_validation_enum_FX45 = "When using <machineKey compatibilityMode=\"Framework45\" /> or the MachineKey.Protect and MachineKey.Unprotect APIs, the 'validation' attribute must be one of these values: SHA1, HMACSHA256, HMACSHA384, HMACSHA512, or alg:[KeyedHashAlgorithm].";

	// Token: 0x0400016A RID: 362
	public const string Wrong_decryption_enum = "The decryption must be one of these values: Auto, DES, 3DES, AES or alg:[SymmetricAlgorithm].";

	// Token: 0x0400016B RID: 363
	public const string Role_is_not_empty = "This role cannot be deleted because there are users present in it.";

	// Token: 0x0400016C RID: 364
	public const string Assess_Denied_Title = "Access is denied.";

	// Token: 0x0400016D RID: 365
	public const string Assess_Denied_Description2 = "An error occurred while accessing the resources required to serve this request. The server may not be configured for access to the requested URL.";

	// Token: 0x0400016E RID: 366
	public const string Assess_Denied_Section_Title2 = "Error message 401.2.";

	// Token: 0x0400016F RID: 367
	public const string Assess_Denied_Misc_Content2 = "Unauthorized: Logon failed due to server configuration.  Verify that you have permission to view this directory or page based on the credentials you supplied and the authentication methods enabled on the Web server.  Contact the Web server's administrator for additional assistance.";

	// Token: 0x04000170 RID: 368
	public const string Assess_Denied_Description1 = "An error occurred while accessing the resources required to serve this request. This may have been caused by an incorrect user name and/or password.";

	// Token: 0x04000171 RID: 369
	public const string Assess_Denied_MiscTitle1 = "Error message 401.1";

	// Token: 0x04000172 RID: 370
	public const string Assess_Denied_MiscContent1 = "Logon credentials were not recognized. Make sure you are providing the correct user name and password. Otherwise, contact the Web server's administrator for help.";

	// Token: 0x04000173 RID: 371
	public const string Assess_Denied_Description3 = "An error occurred while accessing the resources required to serve this request. You might not have permission to view the requested resources.";

	// Token: 0x04000174 RID: 372
	public const string Assess_Denied_Section_Title3 = "Error message 401.3";

	// Token: 0x04000175 RID: 373
	public const string Assess_Denied_Misc_Content3 = "You do not have permission to view this directory or page using the credentials you supplied (access denied due to Access Control Lists). Ask the Web server's administrator to give you access to '{0}'.";

	// Token: 0x04000176 RID: 374
	public const string Assess_Denied_Misc_Content3_2 = "You do not have permission to view this directory or page using the credentials you supplied (access denied due to Access Control Lists). Ask the Web server's administrator to give you access.";

	// Token: 0x04000177 RID: 375
	public const string Auth_bad_url = "The URL specified in the config file is invalid.";

	// Token: 0x04000178 RID: 376
	public const string Virtual_path_outside_application_not_supported = "Virtual path outside of the current application is not supported.";

	// Token: 0x04000179 RID: 377
	public const string Invalid_decryption_key = "Decryption key specified has invalid hex characters.";

	// Token: 0x0400017A RID: 378
	public const string Invalid_validation_key = "Validation key specified has invalid hex characters.";

	// Token: 0x0400017B RID: 379
	public const string Passport_not_installed = "The PassportManager object could not be initialized. Please ensure that Microsoft Passport is correctly installed on the server.";

	// Token: 0x0400017C RID: 380
	public const string DbFileName_can_not_contain_invalid_chars = "The database filename cannot contain the following 3 characters: [ (open square brace), ] (close square brace) and ' (single quote)";

	// Token: 0x0400017D RID: 381
	public const string Provider_can_not_create_file_in_this_trust_level = "The SSE Provider did not find the database file specified in the connection string. At the configured trust level (below High trust level), the SSE provider cannot automatically create the database file.";

	// Token: 0x0400017E RID: 382
	public const string LocalDB_cannot_have_userinstance_flag = "The user instance login flag is not allowed when connecting to a LocalDB instance of SQL Server. The connection will be closed.";

	// Token: 0x0400017F RID: 383
	public const string MembershipPasswordAttribute_InvalidPasswordLength = "The '{0}' field is an invalid password. Password must have {1} or more characters.";

	// Token: 0x04000180 RID: 384
	public const string MembershipPasswordAttribute_InvalidPasswordNonAlphanumericCharacters = "The '{0}' field is an invalid password. Password must have {1} or more non-alphanumeric characters.";

	// Token: 0x04000181 RID: 385
	public const string MembershipPasswordAttribute_InvalidPasswordStrength = "The '{0}' field does not meet the complexity requirements for a valid password.";

	// Token: 0x04000182 RID: 386
	public const string MembershipPasswordAttribute_InvalidRegularExpression = "The MembershipPasswordAttribute was initialized with an invalid regular expression for password strength.";

	// Token: 0x04000183 RID: 387
	public const string LocalizableString_LocalizationFailed = "Cannot retrieve property '{0}' because localization failed.  Type '{1}' is not public or does not contain a public static string property with the name '{2}'.";

	// Token: 0x04000184 RID: 388
	public const string Role_provider_name_invalid = "The role provider name specified is invalid.";

	// Token: 0x04000185 RID: 389
	public const string Def_provider_not_found = "Default provider not found";

	// Token: 0x04000186 RID: 390
	public const string Provider_no_type_name = "Type name must be specified for this provider.";

	// Token: 0x04000187 RID: 391
	public const string MembershipSqlProvider_description = "SQL membership provider.";

	// Token: 0x04000188 RID: 392
	public const string RoleSqlProvider_description = "SQL role provider.";

	// Token: 0x04000189 RID: 393
	public const string RoleAuthStoreProvider_description = "Authorization store role provider.";

	// Token: 0x0400018A RID: 394
	public const string RoleWindowsTokenProvider_description = "Windows token role provider.";

	// Token: 0x0400018B RID: 395
	public const string ProfileSqlProvider_description = "SQL profile provider.";

	// Token: 0x0400018C RID: 396
	public const string Role_Principal_not_fully_constructed = "This RolePrincipal object is not fully constructed.";

	// Token: 0x0400018D RID: 397
	public const string Only_one_connection_string_allowed = "SqlWebEventProvider: Specify either a connectionString or connectionStringName, not both.";

	// Token: 0x0400018E RID: 398
	public const string Must_specify_connection_string_or_name = "SqlWebEventProvider: Either a connectionString or connectionStringName must be specified.";

	// Token: 0x0400018F RID: 399
	public const string Cannot_use_integrated_security = "SqlWebEventProvider: connectionString can only contain connection strings that use Sql Server authentication.  Trusted Connection security is not supported.";

	// Token: 0x04000190 RID: 400
	public const string Provider_application_name_too_long = "The application name is too long.";

	// Token: 0x04000191 RID: 401
	public const string Provider_bad_password_format = "Password format specified is invalid.";

	// Token: 0x04000192 RID: 402
	public const string Provider_can_not_retrieve_hashed_password = "Configured settings are invalid: Hashed passwords cannot be retrieved. Either set the password format to different type, or set enablePasswordRetrieval to false.";

	// Token: 0x04000193 RID: 403
	public const string Provider_unrecognized_attribute = "Attribute not recognized '{0}'";

	// Token: 0x04000194 RID: 404
	public const string Provider_can_not_decode_hashed_password = "Hashed passwords cannot be decoded.";

	// Token: 0x04000195 RID: 405
	public const string Profile_group_not_found = "The profile group '{0}' has not been defined.";

	// Token: 0x04000196 RID: 406
	public const string Profile_not_enabled = "Profile has not been enabled.";

	// Token: 0x04000197 RID: 407
	public const string API_supported_for_current_user_only = "Method is only supported if the user name parameter matches the user name in the current Windows Identity.";

	// Token: 0x04000198 RID: 408
	public const string API_failed_due_to_error = "API failed due to error '{0}'";

	// Token: 0x04000199 RID: 409
	public const string Profile_property_already_added = "This profile property has already been defined.";

	// Token: 0x0400019A RID: 410
	public const string Profile_provider_not_found = "The profile provider was not found '{0}'";

	// Token: 0x0400019B RID: 411
	public const string Can_not_issue_cookie_or_redirect = "Redirect failed because authentication ticket could not be stored in a cookie or URL due to configuration restrictions. Set EnableCrossAppRedirect to true in the <forms> configuration section in order to enable the ticket to be transferred to external locations via the URL.";

	// Token: 0x0400019C RID: 412
	public const string Profile_default_provider_not_found = "The profile default provider was not found.";

	// Token: 0x0400019D RID: 413
	public const string Value_must_be_boolean = "The value must be boolean (true or false) for property '{0}'.";

	// Token: 0x0400019E RID: 414
	public const string Value_must_be_positive_integer = "The value must be a positive 32-bit integer for property '{0}'.";

	// Token: 0x0400019F RID: 415
	public const string Value_must_be_non_negative_integer = "The value must be a non-negative 32-bit integer for property '{0}'.";

	// Token: 0x040001A0 RID: 416
	public const string Value_too_big = "The value '{0}' cannot be greater than '{1}'.";

	// Token: 0x040001A1 RID: 417
	public const string Profile_name_can_not_be_empty = "Profile property and group names must not be empty.";

	// Token: 0x040001A2 RID: 418
	public const string Profile_name_can_not_contain_period = "Profile property and group names must not contain '.'.";

	// Token: 0x040001A3 RID: 419
	public const string Provider_user_not_found = "The user was not found in the database.";

	// Token: 0x040001A4 RID: 420
	public const string Provider_role_not_found = "The role '{0}' was not found.";

	// Token: 0x040001A5 RID: 421
	public const string Provider_unknown_failure = "Stored procedure call failed.";

	// Token: 0x040001A6 RID: 422
	public const string Provider_role_already_exists = "The role '{0}' already exists.";

	// Token: 0x040001A7 RID: 423
	public const string Profile_default_provider_not_specified = "The default profile provider not specified.";

	// Token: 0x040001A8 RID: 424
	public const string API_not_supported_at_this_level = "This API is not supported at this trust level.";

	// Token: 0x040001A9 RID: 425
	public const string Profile_bad_name = "The property name specified is invalid.";

	// Token: 0x040001AA RID: 426
	public const string Profile_bad_group = "The specified group name: {0} of this property is invalid.";

	// Token: 0x040001AB RID: 427
	public const string Def_membership_provider_not_specified = "Default Membership Provider must be specified.";

	// Token: 0x040001AC RID: 428
	public const string Def_membership_provider_not_found = "Default Membership Provider could not be found.";

	// Token: 0x040001AD RID: 429
	public const string Provider_Error = "The Provider encountered an unknown error.";

	// Token: 0x040001AE RID: 430
	public const string Roles_feature_not_enabled = "The Role Manager feature has not been enabled.";

	// Token: 0x040001AF RID: 431
	public const string Def_role_provider_not_specified = "Default Role Provider must be specified.";

	// Token: 0x040001B0 RID: 432
	public const string Def_role_provider_not_found = "Default Role Provider could not be found.";

	// Token: 0x040001B1 RID: 433
	public const string Membership_PasswordRetrieval_not_supported = "This Membership Provider has not been configured to support password retrieval.";

	// Token: 0x040001B2 RID: 434
	public const string Membership_UserNotFound = "The user was not found.";

	// Token: 0x040001B3 RID: 435
	public const string Membership_WrongPassword = "The password supplied is wrong.";

	// Token: 0x040001B4 RID: 436
	public const string Membership_WrongAnswer = "The password-answer supplied is wrong.";

	// Token: 0x040001B5 RID: 437
	public const string Membership_InvalidPassword = "The password supplied is invalid.  Passwords must conform to the password strength requirements configured for the default provider.";

	// Token: 0x040001B6 RID: 438
	public const string Membership_InvalidQuestion = "The password-question supplied is invalid.  Note that the current provider configuration requires a valid password question and answer.  As a result, a CreateUser overload that accepts question and answer parameters must also be used.";

	// Token: 0x040001B7 RID: 439
	public const string Membership_InvalidAnswer = "The password-answer supplied is invalid.";

	// Token: 0x040001B8 RID: 440
	public const string Membership_InvalidUserName = "The username supplied is invalid.";

	// Token: 0x040001B9 RID: 441
	public const string Membership_InvalidEmail = "The E-mail supplied is invalid.";

	// Token: 0x040001BA RID: 442
	public const string Membership_DuplicateUserName = "The username is already in use.";

	// Token: 0x040001BB RID: 443
	public const string Membership_DuplicateEmail = "The E-mail address is already in use.";

	// Token: 0x040001BC RID: 444
	public const string Membership_UserRejected = "The User was rejected.";

	// Token: 0x040001BD RID: 445
	public const string Membership_InvalidProviderUserKey = "The provider user key supplied is invalid.  It must be of type System.Guid.";

	// Token: 0x040001BE RID: 446
	public const string Membership_DuplicateProviderUserKey = "The provider user key is already in use.";

	// Token: 0x040001BF RID: 447
	public const string Membership_AccountLockOut = "The user account has been locked out.";

	// Token: 0x040001C0 RID: 448
	public const string Membership_Custom_Password_Validation_Failure = "The custom password validation failed.";

	// Token: 0x040001C1 RID: 449
	public const string MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength = "The minRequiredNonalphanumericCharacters cannot be greater than minRequiredPasswordLength.";

	// Token: 0x040001C2 RID: 450
	public const string ADMembership_Description = "Active Directory membership provider.";

	// Token: 0x040001C3 RID: 451
	public const string ADMembership_InvalidConnectionProtection = "The specified connection protection type, '{0}', is invalid.";

	// Token: 0x040001C4 RID: 452
	public const string ADMembership_Connection_username_must_not_be_empty = "Connection-username must not be empty.";

	// Token: 0x040001C5 RID: 453
	public const string ADMembership_Connection_password_must_not_be_empty = "Connection-password must not be empty.";

	// Token: 0x040001C6 RID: 454
	public const string ADMembership_Schema_mappings_must_not_be_empty = "Schema mapping for property '{0}' must not be empty.";

	// Token: 0x040001C7 RID: 455
	public const string ADMembership_Username_and_password_reqd = "If either of the properties connection-username or connection-password is specified, the other must also be specified.";

	// Token: 0x040001C8 RID: 456
	public const string ADMembership_PasswordReset_without_question_not_supported = "The Active Directory membership provider does not support password reset without password question and answer.";

	// Token: 0x040001C9 RID: 457
	public const string ADMembership_PasswordQuestionAnswerMapping_not_specified = "Attribute schema mappings for password-question and password-answer must be specified to enable password question and answer functionality.";

	// Token: 0x040001CA RID: 458
	public const string ADMembership_Provider_not_initialized = "The Active Directory Membership Provider has not been initialized.";

	// Token: 0x040001CB RID: 459
	public const string ADMembership_PasswordQ_not_supported = "Schema mapping for password question has not been specified.";

	// Token: 0x040001CC RID: 460
	public const string ADMembership_PasswordA_not_supported = "Schema mapping for password answer has not been specified.";

	// Token: 0x040001CD RID: 461
	public const string ADMembership_PasswordRetrieval_not_supported_AD = "The Active Directory membership provider does not support password retrieval.";

	// Token: 0x040001CE RID: 462
	public const string ADMembership_Username_mapping_invalid = "The property 'attributeMapUsername' must be mapped to 'sAMAccountName' or 'userPrincipalName'.";

	// Token: 0x040001CF RID: 463
	public const string ADMembership_Username_mapping_invalid_ADAM = "The property 'attributeMapUsername' must be mapped to 'userPrincipalName'.";

	// Token: 0x040001D0 RID: 464
	public const string ADMembership_Wrong_syntax = "'{0}' must be mapped to a schema attribute of type '{1}'.";

	// Token: 0x040001D1 RID: 465
	public const string ADMembership_MappedAttribute_does_not_exist = "The attribute '{0}' specified as a schema mapping for '{1}' does not exist.";

	// Token: 0x040001D2 RID: 466
	public const string ADMembership_MappedAttribute_does_not_exist_on_user = "The attribute '{0}' specified as a schema mapping for '{1}' is not an attribute of the user class.";

	// Token: 0x040001D3 RID: 467
	public const string ADMembership_OnlyLdap_supported = "Only LDAP connection strings are supported against Active Directory and ADAM.";

	// Token: 0x040001D4 RID: 468
	public const string ADMembership_ServerlessADsPath_not_supported = "Serverless LDAP connection strings are not supported by the Active Directory membership provider.";

	// Token: 0x040001D5 RID: 469
	public const string ADMembership_Secure_connection_not_established = "Unable to establish secure connection with the server";

	// Token: 0x040001D6 RID: 470
	public const string ADMembership_Ssl_connection_not_established = "Unable to establish secure connection with the server using SSL.";

	// Token: 0x040001D7 RID: 471
	public const string ADMembership_DefContainer_not_specified = "A default container (defaultNamingContext) has not been set for the specified server.";

	// Token: 0x040001D8 RID: 472
	public const string ADMembership_DefContainer_does_not_exist = "The default users container does not exist in the specified domain.";

	// Token: 0x040001D9 RID: 473
	public const string ADMembership_Container_must_be_specified = "For ADAM a container must be specified in the connection string.";

	// Token: 0x040001DA RID: 474
	public const string ADMembership_Valid_Targets = "This provider can target only Active Directory and ADAM directories.";

	// Token: 0x040001DB RID: 475
	public const string ADMembership_OnlineUsers_not_supported = "This Active Directory membership provider does not support the notion of online users.";

	// Token: 0x040001DC RID: 476
	public const string ADMembership_UserProperty_not_supported = "The property '{0}' is not supported by the Active Directory membership provider.";

	// Token: 0x040001DD RID: 477
	public const string ADMembership_Provider_SearchMethods_not_supported = "The Active Directory membership provider has not been configured to support search methods.";

	// Token: 0x040001DE RID: 478
	public const string ADMembership_No_ADAM_Partition = "Unable to find the ADAM application partition for the specified container.";

	// Token: 0x040001DF RID: 479
	public const string ADMembership_Setting_UserId_not_supported = "The Active Directory membership provider does not support setting of the providerUserKey attribute.";

	// Token: 0x040001E0 RID: 480
	public const string ADMembership_Default_Creds_not_supported = "Default credentials are not supported when the connection protection is set to None.";

	// Token: 0x040001E1 RID: 481
	public const string ADMembership_Container_not_superior = "User objects cannot be created in the specified container.";

	// Token: 0x040001E2 RID: 482
	public const string ADMembership_Container_does_not_exist = "The container specified in the connection string does not exist.";

	// Token: 0x040001E3 RID: 483
	public const string ADMembership_Property_not_found_on_object = "Property '{0}' on object '{1}' not found.";

	// Token: 0x040001E4 RID: 484
	public const string ADMembership_Property_not_found = "Property '{0}' not found.";

	// Token: 0x040001E5 RID: 485
	public const string ADMembership_BadPasswordAnswerMappings_not_specified = "Attribute schema mappings for bad password answer tracking must be specified to enable password reset functionality.";

	// Token: 0x040001E6 RID: 486
	public const string ADMembership_Unknown_Error = "Unknown error ({0})";

	// Token: 0x040001E7 RID: 487
	public const string ADMembership_GCPortsNotSupported = "LDAP connections on the GC port are not supported against Active Directory.";

	// Token: 0x040001E8 RID: 488
	public const string ADMembership_attribute_not_single_valued = "Property '{0}' must be mapped to a single valued schema attribute.";

	// Token: 0x040001E9 RID: 489
	public const string ADMembership_mapping_not_unique = "Property '{0}' cannot be mapped to schema attribute '{1}' as the attribute is already in use.";

	// Token: 0x040001EA RID: 490
	public const string ADMembership_InvalidProviderUserKey = "The provider user key supplied is invalid. It must be of type System.Security.Principal.SecurityIdentifier.";

	// Token: 0x040001EB RID: 491
	public const string ADMembership_unable_to_contact_domain = "The specified domain or server could not be contacted.";

	// Token: 0x040001EC RID: 492
	public const string ADMembership_unable_to_set_password_port = "Unable to set the password port and password method.";

	// Token: 0x040001ED RID: 493
	public const string ADMembership_invalid_path = "The specified connection string does not represent a valid LDAP adspath.";

	// Token: 0x040001EE RID: 494
	public const string ADMembership_Setting_ApplicationName_not_supported = "The Active Directory membership provider does not support setting of the ApplicationName property.";

	// Token: 0x040001EF RID: 495
	public const string ADMembership_Parameter_too_long = "The parameter '{0}' is too long.";

	// Token: 0x040001F0 RID: 496
	public const string ADMembership_No_secure_conn_for_password = "Unable to setup a secure connection for setting/changing the password.";

	// Token: 0x040001F1 RID: 497
	public const string ADMembership_Generated_password_not_complex = "Unable to generate a password that satisfies the required password complexity policy.";

	// Token: 0x040001F2 RID: 498
	public const string ADMembership_UPN_contains_backslash = "Usernames must not contain '\\' when mapped to 'userPrincipalName'.";

	// Token: 0x040001F3 RID: 499
	public const string Windows_Token_API_not_supported = "The configured Role Provider (WindowsTokenRoleProvider) relies upon Windows authentication to determine the groups that the user is allowed to be a member of. ASP.NET Role Manager cannot be used to manage Windows users and groups. Please use the SQLRoleProvider if you would like to support custom user/role assignment.";

	// Token: 0x040001F4 RID: 500
	public const string Parameter_can_not_contain_comma = "The parameter '{0}' must not contain commas.";

	// Token: 0x040001F5 RID: 501
	public const string Parameter_can_not_be_empty = "The parameter '{0}' must not be empty.";

	// Token: 0x040001F6 RID: 502
	public const string Parameter_too_long = "The parameter '{0}' is too long: it must not exceed {1} chars in length.";

	// Token: 0x040001F7 RID: 503
	public const string Parameter_array_empty = "The array parameter '{0}' should not be empty.";

	// Token: 0x040001F8 RID: 504
	public const string Parameter_collection_empty = "The collection parameter '{0}' should not be empty.";

	// Token: 0x040001F9 RID: 505
	public const string Parameter_duplicate_array_element = "The array '{0}' should not contain duplicate values.";

	// Token: 0x040001FA RID: 506
	public const string Membership_password_too_long = "The password is too long: it must not exceed 128 chars after encrypting.";

	// Token: 0x040001FB RID: 507
	public const string Provider_this_user_not_found = "The user '{0}' was not found.";

	// Token: 0x040001FC RID: 508
	public const string Provider_this_user_already_in_role = "The user '{0}' is already in role '{1}'.";

	// Token: 0x040001FD RID: 509
	public const string Provider_this_user_already_not_in_role = "The user '{0}' is already not in role '{1}'.";

	// Token: 0x040001FE RID: 510
	public const string SaveAs_requires_rooted_path = "The SaveAs method is configured to require a rooted path, and the path '{0}' is not rooted.";

	// Token: 0x040001FF RID: 511
	public const string Connection_name_not_specified = "The attribute 'connectionStringName' is missing or empty.";

	// Token: 0x04000200 RID: 512
	public const string Connection_string_not_found = "The connection name '{0}' was not found in the applications configuration or the connection string is empty.";

	// Token: 0x04000201 RID: 513
	public const string AppSetting_not_found = "The application setting '{0}' was not found in the applications configuration.";

	// Token: 0x04000202 RID: 514
	public const string AppSetting_not_convertible = "Could not convert the AppSetting '{0}' to the type '{1}' on property '{2}'.";

	// Token: 0x04000203 RID: 515
	public const string Provider_must_implement_type = "Provider must implement the class '{0}'.";

	// Token: 0x04000204 RID: 516
	public const string Feature_not_supported_at_this_level = "This feature is not supported at the configured trust level.";

	// Token: 0x04000205 RID: 517
	public const string Annoymous_id_module_not_enabled = "The Profile property '{0}' allows anonymous users to store data. This requires that the AnonymousIdentification feature be enabled.";

	// Token: 0x04000206 RID: 518
	public const string Anonymous_ClearAnonymousIdentifierNotSupported = "ClearAnonymousIdentifier is not supported when the feature is disabled or the user is anonymous.";

	// Token: 0x04000207 RID: 519
	public const string Anonymous_id_too_long = "The Anonymous Id specified is too long. It can be at most 128 chars long.";

	// Token: 0x04000208 RID: 520
	public const string Anonymous_id_too_long_2 = "The Anonymous Id specified is too long. It can be at most 512 chars long after encoding.";

	// Token: 0x04000209 RID: 521
	public const string Profile_could_not_create_type = "Attempting to load this property's type resulted in the following error: {0}";

	// Token: 0x0400020A RID: 522
	public const string DataAccessError_CanNotCreateDataDir_Title = "Access denied creating App_Data subdirectory";

	// Token: 0x0400020B RID: 523
	public const string DataAccessError_CanNotCreateDataDir_Description = "For security reasons, the identity '{0}' (under which this web application is running), does not have permissions to create the App_Data subdirectory within the application root directory.";

	// Token: 0x0400020C RID: 524
	public const string DataAccessError_CanNotCreateDataDir_Description_2 = "For security reasons, the identity under which this web application is running, does not have permissions to create the App_Data subdirectory within the application root directory.";

	// Token: 0x0400020D RID: 525
	public const string DataAccessError_MiscSectionTitle = "To grant the necessary permissions, follow these steps";

	// Token: 0x0400020E RID: 526
	public const string DataAccessError_MiscSection_1 = "In File Explorer, navigate to your application's directory.";

	// Token: 0x0400020F RID: 527
	public const string DataAccessError_MiscSection_2 = "Right-click on the \"App_Data\" subdirectory, within your application and select the \"Properties\" menu item.";

	// Token: 0x04000210 RID: 528
	public const string DataAccessError_MiscSection_2_CanNotCreateDataDir = "Create a folder named \"App_Data\": Right-click, choose \"New\" menu item, choose \"Folder\" sub-menu item, and then type \"App_Data\" (without quotes).";

	// Token: 0x04000211 RID: 529
	public const string DataAccessError_MiscSection_2_CanNotWriteToDBFile_a = "Navigate to the \"App_Data\" subdirectory.";

	// Token: 0x04000212 RID: 530
	public const string DataAccessError_MiscSection_2_CanNotWriteToDBFile_b = "Right-click on the Access Database file (by default, the file is named \"ASPNetDB.mdb\") and select the \"Properties\" menu item.";

	// Token: 0x04000213 RID: 531
	public const string DataAccessError_MiscSection_3 = "In the \"Properties\" dialog box that opens,  select the \"Security\" tab.";

	// Token: 0x04000214 RID: 532
	public const string DataAccessError_MiscSection_4 = "In the \"Enter the object names to select\" box, enter '{0}' (without quotes).";

	// Token: 0x04000215 RID: 533
	public const string DataAccessError_MiscSection_4_2 = "In the \"Enter the object names to select\" box, enter name of the identity used to run this web application.";

	// Token: 0x04000216 RID: 534
	public const string DataAccessError_MiscSection_ClickAdd = "Click Add";

	// Token: 0x04000217 RID: 535
	public const string DataAccessError_MiscSection_ClickOK = "Click OK";

	// Token: 0x04000218 RID: 536
	public const string DataAccessError_MiscSection_5 = "Make sure the account name is selected and then under Allow, check Write";

	// Token: 0x04000219 RID: 537
	public const string SqlExpressError_CanNotWriteToDataDir_Title = "Access denied creating Microsoft SQL Express Database file within App_Data subdirectory";

	// Token: 0x0400021A RID: 538
	public const string SqlExpressError_CanNotWriteToDbfFile_Title = "Access denied writing to Microsoft SQL Express Database file";

	// Token: 0x0400021B RID: 539
	public const string SqlExpressError_CanNotWriteToDataDir_Description = "For security reasons, the identity '{0}' (under which this web application is running), does not have permissions to create the SQL Express Database file within App_Data subdirectory.";

	// Token: 0x0400021C RID: 540
	public const string SqlExpressError_CanNotWriteToDbfFile_Description = "For security reasons, the identity '{0}' (under which this web application is running), does not have permissions to write to the SQL Express Database file configured for this application.";

	// Token: 0x0400021D RID: 541
	public const string SqlExpressError_CanNotWriteToDataDir_Description_2 = "For security reasons, the identity under which this web application is running, does not have permissions to create the SQL Express Database file within App_Data subdirectory.";

	// Token: 0x0400021E RID: 542
	public const string SqlExpressError_CanNotWriteToDbfFile_Description_2 = "For security reasons, the identity under which this web application is running, does not have permissions to write to the SQL Express Database file configured for this application.";

	// Token: 0x0400021F RID: 543
	public const string SqlExpressError_Description_1 = "ASP.NET stores the Microsoft SQL Express Database file used for services such as Membership and Profile in the App_Data subdirectory of your application.";

	// Token: 0x04000220 RID: 544
	public const string Membership_password_length_incorrect = "Password length specified must be between 1 and 128 characters.";

	// Token: 0x04000221 RID: 545
	public const string Membership_min_required_non_alphanumeric_characters_incorrect = "The value specified in parameter '{0}' should be in the range from zero to the value specified in the password length parameter.";

	// Token: 0x04000222 RID: 546
	public const string Membership_more_than_one_user_with_email = "More than one user has the specified e-mail address.";

	// Token: 0x04000223 RID: 547
	public const string Password_too_short = "The length of parameter '{0}' needs to be greater or equal to '{1}'.";

	// Token: 0x04000224 RID: 548
	public const string Password_need_more_non_alpha_numeric_chars = "Non alpha numeric characters in '{0}' needs to be greater than or equal to '{1}'.";

	// Token: 0x04000225 RID: 549
	public const string Password_does_not_match_regular_expression = "The parameter '{0}' does not match the regular expression specified in config file.";

	// Token: 0x04000226 RID: 550
	public const string Not_configured_to_support_password_resets = "This provider is not configured to allow password resets. To enable password reset, set enablePasswordReset to \"true\" in the configuration file.";

	// Token: 0x04000227 RID: 551
	public const string Property_not_serializable = "The type for the property '{0}' cannot be serialized using the binary serializer, since the type is not marked as serializable.";

	// Token: 0x04000228 RID: 552
	public const string Connection_not_secure_creating_secure_cookie = "The application is configured to issue secure cookies. These cookies require the browser to issue the request over SSL (https protocol). However, the current request is not over SSL.";

	// Token: 0x04000229 RID: 553
	public const string Profile_anonoymous_not_allowed_to_set_property = "This property cannot be set for anonymous users.";

	// Token: 0x0400022A RID: 554
	public const string AuthStore_Application_not_found = "The application was not found in the authorization store.";

	// Token: 0x0400022B RID: 555
	public const string AuthStore_Scope_not_found = "The scope was not found in the application.";

	// Token: 0x0400022C RID: 556
	public const string AuthStoreNotInstalled_Title = "The authorization store component is not installed";

	// Token: 0x0400022D RID: 557
	public const string AuthStoreNotInstalled_Description = "The AuthorizationStoreRoleProvider requires the authorization store components to be installed on the machine. The authorization store components are only installed and available by default on Windows Server 2003. Currently it appears that either the components have not been installed, or that the primary interop assembly has not been registered in the global assembly cache (GAC).  Both of these steps can be accomplished by downloading the Authorization Manager installation package from the web for your operating system, and installing the package on the machine.  Installations for other operating systems can be found by navigating to http://download.microsoft.com and searching with either the keyword \"AzMan\" or the keywords \"authorization manager\".";

	// Token: 0x0400022E RID: 558
	public const string AuthStore_policy_file_not_found = "The policy file '{0}' in the connection string could not be found.";

	// Token: 0x0400022F RID: 559
	public const string Wrong_profile_base_type = "The type specified in the configuration property \"inherits\" must inherit from the type System.Web.Profile.HttpProfileBase";

	// Token: 0x04000230 RID: 560
	public const string Command_not_recognized = "The command was not recognized.";

	// Token: 0x04000231 RID: 561
	public const string Configuration_for_path_not_found = "The configuration for virtual path '{0}' and site '{1}' cannot be opened.";

	// Token: 0x04000232 RID: 562
	public const string Configuration_for_physical_path_not_found = "The configuration for physical path '{0}' cannot be opened.";

	// Token: 0x04000233 RID: 563
	public const string Configuration_for_machine_config_not_found = "The configuration for machine.config cannot be opened.";

	// Token: 0x04000234 RID: 564
	public const string Configuration_Section_not_found = "The configuration section '{0}' was not found.";

	// Token: 0x04000235 RID: 565
	public const string RSA_Key_Container_not_found = "The RSA key container was not found.";

	// Token: 0x04000236 RID: 566
	public const string RSA_Key_Container_access_denied = "Could not access the RSA key container. Make sure that the ACLs on the container allow you to access it.";

	// Token: 0x04000237 RID: 567
	public const string RSA_Key_Container_already_exists = "The RSA key container already exists.";

	// Token: 0x04000238 RID: 568
	public const string SqlError_Connection_String = "An error occurred while attempting to initialize a System.Data.SqlClient.SqlConnection object. The value that was provided for the connection string may be wrong, or it may contain an invalid syntax.";

	// Token: 0x04000239 RID: 569
	public const string SqlExpress_MDF_File_Auto_Creation_MiscSectionTitle = "SQLExpress database file auto-creation error";

	// Token: 0x0400023A RID: 570
	public const string SqlExpress_MDF_File_Auto_Creation = "The connection string specifies a local Sql Server Express instance using a database location within the application's App_Data directory.  The provider attempted to automatically create the application services database because the provider determined that the database does not exist. The following configuration requirements are necessary to successfully check for existence of the application services database and automatically create the application services database:";

	// Token: 0x0400023B RID: 571
	public const string SqlExpress_MDF_File_Auto_Creation_1 = "If the application is running on either Windows 7 or Windows Server 2008R2, special configuration steps are necessary to enable automatic creation of the provider database.  Additional information is available at:  http://go.microsoft.com/fwlink/?LinkId=160102. If the application's App_Data directory does not already exist, the web server account must have read and write access to the application's directory.  This is necessary because the web server account will automatically create the App_Data directory if it does not already exist.";

	// Token: 0x0400023C RID: 572
	public const string SqlExpress_MDF_File_Auto_Creation_2 = "If the application's App_Data directory already exists, the web server account only requires read and write access to the application's App_Data directory.  This is necessary because the web server account will attempt to verify that the Sql Server Express database already exists within the application's App_Data directory.  Revoking read access on the App_Data directory from the web server account will prevent the provider from correctly determining if the Sql Server Express database already exists.  This will cause an error when the provider attempts to create a duplicate of an already existing database.  Write access is required because the web server account's credentials are used when creating the new database.";

	// Token: 0x0400023D RID: 573
	public const string SqlExpress_MDF_File_Auto_Creation_3 = "Sql Server Express must be installed on the machine.";

	// Token: 0x0400023E RID: 574
	public const string SqlExpress_MDF_File_Auto_Creation_4 = "The process identity for the web server account must have a local user profile.  See the readme document for details on how to create a local user profile for both machine and domain accounts.";

	// Token: 0x0400023F RID: 575
	public const string SqlExpress_file_not_found_in_connection_string = "SQL Express filename was not found in the connection string.";

	// Token: 0x04000240 RID: 576
	public const string SqlExpress_file_not_found = "The SQL Express filename specified was not found.";

	// Token: 0x04000241 RID: 577
	public const string Invalid_value_for_sessionstate_stateConnectionString = "The <sessionState> stateConnectionString is invalid. It must have the format tcpip=<server>:<port>, where <server> is a valid IPv4 address, a machine name using only ASCII characters, or a valid IPv6 address enclosed within square brackets, and <port> is an unsigned integer ranging from 0 to 65535 (e.g., tcpip=127.0.0.1:42424, tcpip=localhost:42424, or tcpip=[::1]:42424).";

	// Token: 0x04000242 RID: 578
	public const string No_database_allowed_in_sqlConnectionString = "The sqlConnectionString attribute or the connection string it refers to cannot contain the connection options 'Database', 'Initial Catalog' or 'AttachDbFileName'. In order to allow this, allowCustomSqlDatabase attribute must be set to true and the application needs to be granted unrestricted SqlClientPermission. Please check with your administrator if the application does not have this permission.";

	// Token: 0x04000243 RID: 579
	public const string No_database_allowed_in_sql_partition_resolver_string = "The SQL connection string (server='{1}', database='{2}') returned by an instance of the IPartitionResolver type '{0}' cannot contain the connection options 'Database', 'Initial Catalog' or 'AttachDbFileName'. In order to allow this, allowCustomSqlDatabase attribute must be set to true and the application needs to be granted unrestricted SqlClientPermission. Please check with your administrator if the application does not have this permission.";

	// Token: 0x04000244 RID: 580
	public const string Error_parsing_session_sqlConnectionString = "Error parsing <sessionState> sqlConnectionString attribute: {0}";

	// Token: 0x04000245 RID: 581
	public const string Error_parsing_sql_partition_resolver_string = "Error parsing the SQL connection string returned by an instance of the IPartitionResolver type '{0}': {1}";

	// Token: 0x04000246 RID: 582
	public const string Timeout_must_be_positive = "The argument to SetTimeout must be greater than 0.";

	// Token: 0x04000247 RID: 583
	public const string Cant_make_session_request = "Unable to make the session state request to the session state server. Please ensure that the ASP.NET State service is started and that the client and server ports are the same.  If the server is on a remote machine, please ensure that it accepts remote requests by checking the value of HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\aspnet_state\\Parameters\\AllowRemoteConnection.  If the server is on the local machine, and if the before mentioned registry value does not exist or is set to 0, then the state server connection string must use either 'localhost' or '127.0.0.1' as the server name.";

	// Token: 0x04000248 RID: 584
	public const string Cant_make_session_request_partition_resolver = "Unable to make the session state request to the session state server. The connection string (server='{1}', port='{2}') was returned by an instance of the IPartitionResolver type '{0}'. Please ensure that the ASP.NET State service is started and that the client and server ports are the same.  If the server is on a remote machine, please ensure that it accepts remote requests by checking the value of HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\aspnet_state\\Parameters\\AllowRemoteConnection.  If the server is on the local machine, and if the before mentioned registry value does not exist or is set to 0, then the state server connection string must use either 'localhost' or '127.0.0.1' as the server name.";

	// Token: 0x04000249 RID: 585
	public const string Need_v2_State_Server = "Unable to use session state server because this version of ASP.NET requires session state server version 2.0 or above.";

	// Token: 0x0400024A RID: 586
	public const string Need_v2_State_Server_partition_resolver = "Unable to use session state server because this version of ASP.NET requires session state server version 2.0 or above. The connection string (server='{1}', port='{2}') was returned by an instance of the IPartitionResolver type '{0}'.";

	// Token: 0x0400024B RID: 587
	public const string Cant_connect_sql_session_database = "Unable to connect to SQL Server session database.";

	// Token: 0x0400024C RID: 588
	public const string Cant_connect_sql_session_database_partition_resolver = "Unable to connect to SQL Server session database. The connection string (server='{1}', database='{2}') was returned by an instance of the IPartitionResolver type '{0}'.";

	// Token: 0x0400024D RID: 589
	public const string Login_failed_sql_session_database = "Failed to login to session state SQL server for user '{0}'.";

	// Token: 0x0400024E RID: 590
	public const string Bad_partition_resolver_connection_string = "Session state cannot connect to the server because a null connection string was returned by an instance of the IPartitionResolver type '{0}'.";

	// Token: 0x0400024F RID: 591
	public const string Bad_state_server_request = "Unable to make the session state request to the session state server. The session state server is running, but the request is not formatted correctly.";

	// Token: 0x04000250 RID: 592
	public const string Bad_state_server_request_partition_resolver = "Unable to make the session state request to the session state server. The session state server is running, but the request is not formatted correctly. The connection string (server='{1}', port='{2}') was returned by an instance of the IPartitionResolver type '{0}'.";

	// Token: 0x04000251 RID: 593
	public const string State_Server_detailed_error = "Unable to make the session state request to the session state server. Details: last phase='{0}', error code={1}, size of outgoing data={2}";

	// Token: 0x04000252 RID: 594
	public const string State_Server_detailed_error_phase0 = "Initialization";

	// Token: 0x04000253 RID: 595
	public const string State_Server_detailed_error_phase1 = "Connecting to the state server";

	// Token: 0x04000254 RID: 596
	public const string State_Server_detailed_error_phase2 = "Sending request to the state server";

	// Token: 0x04000255 RID: 597
	public const string State_Server_detailed_error_phase3 = "Reading response from the state server";

	// Token: 0x04000256 RID: 598
	public const string Error_parsing_state_server_partition_resolver_string = "Error parsing the state server connection string returned by an instance of the IPartitionResolver type '{0}'. The connection string must have the format tcpip=<server>:<port>, where <server> is either a valid IP address or a machine name using only ASCII characters, and <port> is an unsigned integer ranging from 0 to 65535.";

	// Token: 0x04000257 RID: 599
	public const string SessionIDManager_uninit = "SessionIDManager is not initialized. Call Initialize() method first.";

	// Token: 0x04000258 RID: 600
	public const string SessionIDManager_InitializeRequest_not_called = "ISessionIDManager.InitializeRequest has not been called for this request yet. In each request, please first call ISessionIDManager.InitializeRequest before calling other methods.";

	// Token: 0x04000259 RID: 601
	public const string Cant_save_session_id_because_response_was_flushed = "Session state has created a session id, but cannot save it because the response was already flushed by the application.";

	// Token: 0x0400025A RID: 602
	public const string Cant_save_session_id_because_id_is_invalid = "Cannot save the session id because it is invalid.  Session ID={0}";

	// Token: 0x0400025B RID: 603
	public const string Cant_serialize_session_state = "Unable to serialize the session state. In 'StateServer' and 'SQLServer' mode, ASP.NET will serialize the session state objects, and as a result non-serializable objects or MarshalByRef objects are not permitted. The same restriction applies if similar serialization is done by the custom session state store in 'Custom' mode.";

	// Token: 0x0400025C RID: 604
	public const string Null_value_for_SessionStateItemCollection = "The SessionStateStoreData returned by ISessionStateStore has a null value for Items.";

	// Token: 0x0400025D RID: 605
	public const string Session_id_too_long = "The session ID is longer than the maximum limit of {0} characters. Session ID={1}";

	// Token: 0x0400025E RID: 606
	public const string Need_v2_SQL_Server = "Unable to use SQL Server because either ASP.NET version 2.0 Session State is not installed on the SQL server, or ASP.NET does not have permission to run the dbo.TempGetVersion stored procedure. If the ASP.NET Session State schema has not been installed, please install ASP.NET Session State SQL Server version 2.0 or above. If the schema has been installed, please grant execute permission on the dbo.TempGetVersion stored procedure to either the ASP.NET application pool identity, or the Sql Server user specified in the sqlConnectionString attribute.";

	// Token: 0x0400025F RID: 607
	public const string Need_v2_SQL_Server_partition_resolver = "Unable to use SQL Server because either ASP.NET version 2.0 Session State is not installed on the SQL server, or ASP.NET does not have permission to run the dbo.TempGetVersion stored procedure. If the ASP.NET Session State schema has not been installed, please install ASP.NET Session State SQL Server version 2.0 or above. If the schema has been installed, please grant execute permission on the dbo.TempGetVersion stored procedure to either the ASP.NET application pool identity, or the Sql Server user specified in the sqlConnectionString attribute. The connection string (server='{1}', database='{2}') was returned by an instance of the IPartitionResolver type '{0}'.";

	// Token: 0x04000260 RID: 608
	public const string Cant_have_multiple_session_module = "Another component has already added an HttpSessionState to the context. Please make sure only one session state module should be registered.";

	// Token: 0x04000261 RID: 609
	public const string Missing_session_custom_provider = "The custom session state store provider '{0}' is not found.";

	// Token: 0x04000262 RID: 610
	public const string Invalid_session_custom_provider = "The custom session state store provider name '{0}' is invalid.";

	// Token: 0x04000263 RID: 611
	public const string Invalid_session_state = "The session state information is invalid and might be corrupted.";

	// Token: 0x04000264 RID: 612
	public const string Invalid_cache_based_session_timeout = "For InProc and StateServer modes, the session timeout value cannot be larger than one year.";

	// Token: 0x04000265 RID: 613
	public const string Cant_use_partition_resolve = "Cannot use 'partitionResolver' unless the mode is 'StateServer' or 'SQLServer'.";

	// Token: 0x04000266 RID: 614
	public const string Previous_Page_Not_Authorized = "The current user is not allowed to access the previous page.";

	// Token: 0x04000267 RID: 615
	public const string Empty_path_has_no_directory = "Empty path has no directory.";

	// Token: 0x04000268 RID: 616
	public const string Path_must_be_rooted = "The virtual path '{0}' is not rooted.";

	// Token: 0x04000269 RID: 617
	public const string Cannot_exit_up_top_directory = "Cannot use a leading .. to exit above the top directory.";

	// Token: 0x0400026A RID: 618
	public const string Physical_path_not_allowed = "'{0}' is a physical path, but a virtual path was expected.";

	// Token: 0x0400026B RID: 619
	public const string Invalid_vpath = "'{0}' is not a valid virtual path.";

	// Token: 0x0400026C RID: 620
	public const string Cannot_access_AspCompat = "Cannot access ASP compatibility mode.";

	// Token: 0x0400026D RID: 621
	public const string Apartment_component_not_allowed = "The component '{0}' cannot be created.  Apartment threaded components can only be created on pages with an <%@ Page aspcompat=true %> page directive.";

	// Token: 0x0400026E RID: 622
	public const string Error_onpagestart = "An error was encountered while calling OnStartPage in ASP compatibility mode.";

	// Token: 0x0400026F RID: 623
	public const string Cannot_execute_transacted_code = "Cannot execute transacted code.";

	// Token: 0x04000270 RID: 624
	public const string Cannot_post_workitem = "Cannot post work item to thread pool.";

	// Token: 0x04000271 RID: 625
	public const string Cannot_call_ISAPI_functions = "Hosting platform does not support ISAPI functions.";

	// Token: 0x04000272 RID: 626
	public const string Bad_attachment = "Invalid mail attachment '{0}'.";

	// Token: 0x04000273 RID: 627
	public const string Wrong_SimpleWorkerRequest = "Invalid use of SimpleWorkerRequest constructor. Application path cannot be overridden in this context. Please use SimpleWorkerRequest constructor that does not override the application path.";

	// Token: 0x04000274 RID: 628
	public const string Atio2BadString = "Unable to convert two characters in the string '{0}' to a number starting at offset {1}.";

	// Token: 0x04000275 RID: 629
	public const string MakeMonthBadString = "Unable to convert characters in the string '{0}' to a month starting at offset {1}.";

	// Token: 0x04000276 RID: 630
	public const string UtilParseDateTimeBad = "'{0}' was not of the correct format. Expected a string to be of the form 'Thursday, 10-Jun-93 01:29:59 GMT', 'Thu, 10 Jan 1993 01:29:59 GMT', or 'Wed Jun 09 01:29:59 1993 GMT'.";

	// Token: 0x04000277 RID: 631
	public const string SmtpMail_not_supported_on_Win7_and_higher = "System.Web.Mail.SmtpMail is not supported on this version of Windows.  The recommended alternative is System.Net.Mail.SmtpClient.";

	// Token: 0x04000278 RID: 632
	public const string Illegal_special_dir = "The file '{0}' is in the special directory '{1}', which is not allowed.";

	// Token: 0x04000279 RID: 633
	public const string Precomp_stub_file = "This is a marker file generated by the precompilation tool, and should not be deleted!";

	// Token: 0x0400027A RID: 634
	public const string Already_precomp = "This application is already precompiled.";

	// Token: 0x0400027B RID: 635
	public const string Cant_delete_dir = "The target directory could not be deleted. Please delete it manually, or choose a different target.";

	// Token: 0x0400027C RID: 636
	public const string Dir_not_empty = "The target directory is not empty. Please delete it manually, or choose a different target.";

	// Token: 0x0400027D RID: 637
	public const string Dir_not_empty_not_precomp = "The target directory is not empty, and does not appear to contain a previously compiled application. Please delete it manually, or choose a different target.";

	// Token: 0x0400027E RID: 638
	public const string Cant_update_precompiled_app = "The file '{0}' has not been pre-compiled, and cannot be requested.";

	// Token: 0x0400027F RID: 639
	public const string Too_early_for_webfile = "The file '{0}' cannot be processed because the code directory has not yet been built.";

	// Token: 0x04000280 RID: 640
	public const string Bar_dir_in_precompiled_app = "The directory '{0}' is not allowed because the application is precompiled.";

	// Token: 0x04000281 RID: 641
	public const string Assembly_already_loaded = "The assembly '{0}' is already loaded in another appdomain. Setting <deployment retail=\"true\" /> in machine.config can help solve this issue.";

	// Token: 0x04000282 RID: 642
	public const string Success_precompile = "The application was successfully precompiled.";

	// Token: 0x04000283 RID: 643
	public const string Profile_not_precomped = "This application was precompiled with personalization turned off, but it appears to have been turned on after the precompilation, which is not supported.";

	// Token: 0x04000284 RID: 644
	public const string Both_culture_and_language = "The file '{0}' cannot specify both a language and a culture.";

	// Token: 0x04000285 RID: 645
	public const string Inconsistent_language = "The files '{0}' and '{1}' use a different language, which is not allowed since they need to be compiled together.";

	// Token: 0x04000286 RID: 646
	public const string GetGeneratedSourceFile_Directory_Only = "The virtual path '{0}' does not point to a directory; only existing directories are allowed.";

	// Token: 0x04000287 RID: 647
	public const string Duplicate_appinitialize = "The AppInitialize method is defined both in '{0}' and in '{1}'.";

	// Token: 0x04000288 RID: 648
	public const string Virtual_codedir = "The path '{0}' maps to a directory outside this application, which is not supported.";

	// Token: 0x04000289 RID: 649
	public const string Unknown_buildprovider_extension = "There is no build provider registered for the extension '{0}'. You can register one in the <compilation><buildProviders> section in machine.config or web.config. Make sure is has a BuildProviderAppliesToAttribute attribute which includes the value '{1}' or 'All'.";

	// Token: 0x0400028A RID: 650
	public const string Directory_progress = "Building directory '{0}'.";

	// Token: 0x0400028B RID: 651
	public const string Bad_Base_Class_In_Code_File = "Make sure that the class defined in this code file matches the 'inherits' attribute, and that it extends the correct base class (e.g. Page or UserControl).";

	// Token: 0x0400028C RID: 652
	public const string Reference_assemblies_not_found = "Reference assemblies for target .NET Framework version not found; please ensure they are installed, or select a valid target version.";

	// Token: 0x0400028D RID: 653
	public const string Higher_dependencies = "The following assembly has dependencies on a version of the .NET Framework that is higher than the target and might not load correctly during runtime causing a failure: {0}. The dependencies are: {1}. You should either ensure that the dependent assembly is correct for the target framework, or ensure that the target framework you are addressing is that of the dependent assembly.";

	// Token: 0x0400028E RID: 654
	public const string Invalid_target_framework_version = "The value for the {0} attribute is invalid: '{1}'. Error: {2}.";

	// Token: 0x0400028F RID: 655
	public const string Invalid_lower_target_version = "The '{0}' attribute in the <compilation> element of the Web.config file is used only to target version 4.0 and later of the .NET Framework (for example, '<compilation {0}=\"4.0\">'). If you are compiling this Web application for a version of the .NET Framework earlier than 4.0, remove the '{0}' attribute from the <compilation> element of the Web.config file.";

	// Token: 0x04000290 RID: 656
	public const string Invalid_higher_target_version = "The '{0}' attribute in the <compilation> element of the Web.config file is used only to target version 4.0 and later of the .NET Framework (for example, '<compilation {0}=\"4.0\">'). The '{0}' attribute currently references a version that is later than the installed version of the .NET Framework. Specify a valid target version of the .NET Framework, or install the required version of the .NET Framework.";

	// Token: 0x04000291 RID: 657
	public const string Compiler_version_20_35_required = "In the <compilation> section of the Web.config file, the value for the 'compilerVersion' attribute in the provider options must be 'v2.0' if you are targeting version 2.0 or 3.0 of the .NET Framework, or must be 'v3.5' if you are targeting version 3.5 of the .NET Framework. If you are compiling this Web application for version 4.0 or later of the .NET Framework, the '{0}' attribute is required in the <compilation> element of the Web.config file instead (for example, '<compilation {0}=\"4.0\">').";

	// Token: 0x04000292 RID: 658
	public const string Compiler_version_40_required = "The value for the 'compilerVersion' attribute in the provider options must be 'v4.0' or later if you are compiling for version 4.0 or later of the .NET Framework. To compile this Web application for version 3.5 or earlier of the .NET Framework, remove the '{0}' attribute from the <compilation> element of the Web.config file.";

	// Token: 0x04000293 RID: 659
	public const string Assembly_not_found_in_profile = "The following assembly could not be resolved: '{0}'. The assembly is excluded from the target framework profile{1}. If this reference is required by your code, you may get compilation errors. Please either remove this reference, or ensure that the target framework profile you are addressing contains the assembly.";

	// Token: 0x04000294 RID: 660
	public const string Downlevel_requires_35 = "The <compilation> element of the Web.config file does not have a 'targetFramework' attribute. Therefore ASP.NET assumes that the Web application targets the .NET Framework version 3.5 or earlier, but ASP.NET could not find files that are installed with the .NET Framework version 3.5 that it needs to compile. If you are compiling this Web application for version 3.5 or earlier of the .NET Framework, make sure that version 3.5 of the .NET Framework is installed. If you are compiling this Web application for version 4 or later of the .NET Framework, add the 'targetFramework' attribute to the <compilation> element of the Web.config file (for example, '<compilation targetFramework=\"4.0\">').";

	// Token: 0x04000295 RID: 661
	public const string Invalid_PreApplicationStartMethodAttribute_value = "The method specified by the PreApplicationStartMethodAttribute on assembly '{0}' cannot be resolved. Type: '{1}', MethodName: '{2}'. Verify that the type is public and the method is public and static (Shared in Visual Basic).";

	// Token: 0x04000296 RID: 662
	public const string Method_can_only_be_called_during_pre_start_init = "This method can only be called during the application's pre-start initialization phase. Use PreApplicationStartMethodAttribute to declare a method that will be invoked in that phase.";

	// Token: 0x04000297 RID: 663
	public const string Method_cannot_be_called_during_pre_start_init = "This method cannot be called during the application's pre-start initialization phase.";

	// Token: 0x04000298 RID: 664
	public const string Pre_application_start_init_method_threw_exception = "The pre-application start initialization method {0} on type {1} threw an exception with the following error message: {2}.";

	// Token: 0x04000299 RID: 665
	public const string Cant_use_default_items_and_filtered_collection = "You cannot use {0}'s default collection '{1}' without the property tags when using a filtered version of the same collection.";

	// Token: 0x0400029A RID: 666
	public const string Children_not_supported_on_not_controls = "Child objects are not supported for objects that are not controls.";

	// Token: 0x0400029B RID: 667
	public const string Code_not_supported_on_not_controls = "Code blocks are not supported in this context.";

	// Token: 0x0400029C RID: 668
	public const string Code_not_allowed = "Code blocks are not allowed in this file.";

	// Token: 0x0400029D RID: 669
	public const string Compilmode_not_allowed = "The compilation mode cannot be set to 'Never', because an earlier construct in the page requires compilation.";

	// Token: 0x0400029E RID: 670
	public const string Include_not_allowed = "The include file '{0}' is not allowed in this page.";

	// Token: 0x0400029F RID: 671
	public const string Attrib_not_allowed = "The attribute '{0}' is not allowed in this page.";

	// Token: 0x040002A0 RID: 672
	public const string Directive_not_allowed = "The directive '{0}' is not allowed in this page.";

	// Token: 0x040002A1 RID: 673
	public const string Event_not_allowed = "The event handler '{0}' is not allowed in this page.";

	// Token: 0x040002A2 RID: 674
	public const string Literal_content_not_allowed = "Literal content ('{1}') is not allowed within a '{0}'.";

	// Token: 0x040002A3 RID: 675
	public const string Literal_content_not_match_property = "Content ('{1}') does not match any properties within a '{0}', make sure it is well-formed.";

	// Token: 0x040002A4 RID: 676
	public const string Too_many_controls = "This page allows a limit of {0} controls, and that limit has been exceeded.";

	// Token: 0x040002A5 RID: 677
	public const string Too_many_dependencies = "The page '{0}' allows a limit of {1} dependencies (direct or indirect), and that limit has been exceeded.";

	// Token: 0x040002A6 RID: 678
	public const string Too_many_direct_dependencies = "The page '{0}' allows a limit of {1} direct dependencies, and that limit has been exceeded.";

	// Token: 0x040002A7 RID: 679
	public const string Invalid_type = "Could not load type '{0}'.";

	// Token: 0x040002A8 RID: 680
	public const string Assembly_not_compiled = "Could not load the assembly '{0}'. Make sure that it is compiled before accessing the page.";

	// Token: 0x040002A9 RID: 681
	public const string Not_a_src_file = "'{0}' is not a valid source file.";

	// Token: 0x040002AA RID: 682
	public const string Ambiguous_type = "The type '{0}' is ambiguous: it could come from assembly '{1}' or from assembly '{2}'. Please specify the assembly explicitly in the type name.";

	// Token: 0x040002AB RID: 683
	public const string Unsupported_filename = "The file name '{0}' is not supported.";

	// Token: 0x040002AC RID: 684
	public const string Cannot_convert_from_to = "Cannot convert from '{0}' to '{1}'.";

	// Token: 0x040002AD RID: 685
	public const string Object_tag_must_have_id = "An object tag must have an ID.";

	// Token: 0x040002AE RID: 686
	public const string Invalid_scope = "'{0}' is not a valid object scope.";

	// Token: 0x040002AF RID: 687
	public const string Invalid_progid = "Cannot load type with ProgID '{0}'.";

	// Token: 0x040002B0 RID: 688
	public const string Invalid_clsid = "Cannot load type with CLASSID '{0}'.";

	// Token: 0x040002B1 RID: 689
	public const string Object_tag_must_have_class_classid_or_progid = "An object tag must contain a Class, ClassID or ProgID attribute.";

	// Token: 0x040002B2 RID: 690
	public const string Session_not_enabled = "Session state can only be used when enableSessionState is set to true, either in a configuration file or in the Page directive. Please also make sure that System.Web.SessionStateModule or a custom session state module is included in the <configuration>\\<system.web>\\<httpModules> section in the application configuration.";

	// Token: 0x040002B3 RID: 691
	public const string Page_ControlState_ControlCannotBeNull = "Cannot register a null control reference to require control state.";

	// Token: 0x040002B4 RID: 692
	public const string Page_theme_not_found = "Theme '{0}' cannot be found in the application or global theme directories.";

	// Token: 0x040002B5 RID: 693
	public const string Page_theme_invalid_name = "'{0}' is not a valid theme name.";

	// Token: 0x040002B6 RID: 694
	public const string Page_theme_default_theme_already_defined = "Type {0} already has a default theme defined.";

	// Token: 0x040002B7 RID: 695
	public const string Page_theme_skinID_already_defined = "SkinId '{0}' is already used by another control skin of the same type.";

	// Token: 0x040002B8 RID: 696
	public const string Page_theme_requires_page_header = "Using themed css files requires a header control on the page. (e.g. <head runat=\"server\" />).";

	// Token: 0x040002B9 RID: 697
	public const string Page_theme_only_controls_allowed = "This object cannot be themed. Only controls of type System.Web.UI.Control are allowed at the top-level of a skin file.";

	// Token: 0x040002BA RID: 698
	public const string Page_theme_skin_file = "skin file";

	// Token: 0x040002BB RID: 699
	public const string Page_Title_Requires_Head = "Using the Title property of Page requires a header control on the page. (e.g. <head runat=\"server\" />).";

	// Token: 0x040002BC RID: 700
	public const string Page_Description_Requires_Head = "Using the Description property of Page requires a header control on the page. (e.g. <head runat=\"server\" />).";

	// Token: 0x040002BD RID: 701
	public const string Page_Keywords_Requires_Head = "Using the Keywords property of Page requires a header control on the page. (e.g. <head runat=\"server\" />).";

	// Token: 0x040002BE RID: 702
	public const string DataBoundLiterals_cant_bind = "A call to Bind must be assigned to a property of a control inside a template.";

	// Token: 0x040002BF RID: 703
	public const string TwoWayBinding_requires_ID = "The {0} control with a two-way databinding to field {1} must have an ID.";

	// Token: 0x040002C0 RID: 704
	public const string NoCompileBinding_requires_ID = "The {0} control with a databinding to field {1} must have an ID when the page's CompilationMode is Never.";

	// Token: 0x040002C1 RID: 705
	public const string BadlyFormattedBind = "A call to Bind was not well formatted.  Please refer to documentation for the correct parameters to Bind.";

	// Token: 0x040002C2 RID: 706
	public const string BadlyFormattedBindItem = "Invalid code syntax for BindItem.";

	// Token: 0x040002C3 RID: 707
	public const string Property_readonly = "The '{0}' property is read-only and cannot be set.";

	// Token: 0x040002C4 RID: 708
	public const string Property_theme_disabled = "The '{0}' property of a control type {1} cannot be applied through a control skin.";

	// Token: 0x040002C5 RID: 709
	public const string Type_theme_disabled = "The control type '{0}' cannot be themed.";

	// Token: 0x040002C6 RID: 710
	public const string Collection_readonly_Codeblocks = "The Controls collection cannot be modified because the control contains code blocks (i.e. <% ... %>).";

	// Token: 0x040002C7 RID: 711
	public const string Parent_collections_readonly = "The control collection cannot be modified during DataBind, Init, Load, PreRender or Unload phases.";

	// Token: 0x040002C8 RID: 712
	public const string Property_Not_Persistable = "The '{0}' property cannot be set declaratively.";

	// Token: 0x040002C9 RID: 713
	public const string Property_Not_Supported = "The '{0}' property is not supported by '{1}' control.";

	// Token: 0x040002CA RID: 714
	public const string Property_Not_ClsCompliant = "The '{0}' property of '{1}' has type '{2}', which is not CLS-compliant.";

	// Token: 0x040002CB RID: 715
	public const string Property_Set_Not_Supported = "Setting the {0} property of {1} is not supported.";

	// Token: 0x040002CC RID: 716
	public const string ControlBuilder_InvalidLocalizeValue = "'{0}' is not a valid value for the meta:localize attribute. Only 'true' and 'false' are supported.";

	// Token: 0x040002CD RID: 717
	public const string meta_localize_error = "The meta:resourcekey attribute cannot be used on a tag with meta:localize='false'.";

	// Token: 0x040002CE RID: 718
	public const string meta_reskey_notallowed = "The '{0}' tag cannot have a meta:resourcekey attribute.";

	// Token: 0x040002CF RID: 719
	public const string meta_localize_notallowed = "The '{0}' tag cannot have a meta:localize attribute.";

	// Token: 0x040002D0 RID: 720
	public const string Invalid_enum_value = "'{0}' is not a valid value for attribute '{1}'. It must be of enum type '{2}'.";

	// Token: 0x040002D1 RID: 721
	public const string Type_not_creatable_from_string = "Cannot create an object of type '{0}' from its string representation '{1}' for the '{2}' property.";

	// Token: 0x040002D2 RID: 722
	public const string Invalid_collection_item_type = "{0} must have items of type '{1}'. '{2}' is of type '{3}'.";

	// Token: 0x040002D3 RID: 723
	public const string Invalid_template_container = "Template property '{0}' has a TemplateContainer attribute set to '{1}', but that type does not implement INamingContainer.";

	// Token: 0x040002D4 RID: 724
	public const string Event_handler_cant_be_empty = "The {0} event handler cannot be an empty string.";

	// Token: 0x040002D5 RID: 725
	public const string Events_cant_be_filtered = "The filter '{0}' cannot be applied to the attribute '{1}' because it is an event handler.";

	// Token: 0x040002D6 RID: 726
	public const string Type_doesnt_have_property = "Type '{0}' does not have a public property named '{1}'.";

	// Token: 0x040002D7 RID: 727
	public const string Property_doesnt_have_property = "Property '{0}' does not have a property named '{1}'.";

	// Token: 0x040002D8 RID: 728
	public const string MasterPage_Multiple_content = "Multiple contents applied to {0}.";

	// Token: 0x040002D9 RID: 729
	public const string MasterPage_doesnt_have_contentplaceholder = "Cannot find ContentPlaceHolder '{0}' in the master page '{1}', verify content control's ContentPlaceHolderID attribute in the content page.";

	// Token: 0x040002DA RID: 730
	public const string MasterPage_MasterPageFile = "Gets and sets the masterPageFile used by this page.";

	// Token: 0x040002DB RID: 731
	public const string MasterPage_MasterPage = "The MasterPage of this Page.";

	// Token: 0x040002DC RID: 732
	public const string MasterPage_Circular_Master_Not_Allowed = "Circular MasterPageFile references '{0}' are not allowed.";

	// Token: 0x040002DD RID: 733
	public const string MasterPage_Cannot_ApplyTo_ReadOnly_Collection = "MasterPage cannot be applied to this page because the control collection is read-only. If the page contains code blocks, make sure they are placed inside content controls (i.e. <asp:Content runat=server />)";

	// Token: 0x040002DE RID: 734
	public const string Only_Content_supported_on_content_page = "Only Content controls are allowed directly in a content page that contains Content controls.";

	// Token: 0x040002DF RID: 735
	public const string Content_allowed_in_top_level_only = "Content controls have to be top-level controls in a content page or a nested master page that references a master page.";

	// Token: 0x040002E0 RID: 736
	public const string Content_only_allowed_in_content_page = "Content controls are allowed only in content page that references a master page.";

	// Token: 0x040002E1 RID: 737
	public const string Content_only_one_contentPlaceHolderID_allowed = "Only one ContentPlaceHolderID attribute is allowed.";

	// Token: 0x040002E2 RID: 738
	public const string Invalid_master_base = "The file '{0}' is not a valid master page.";

	// Token: 0x040002E3 RID: 739
	public const string Invalid_typeless_reference = "The file '{0}' is not a valid here because it doesn't expose a type.";

	// Token: 0x040002E4 RID: 740
	public const string Bad_masterPage_ext = "Master page source files must have a .master file extension.";

	// Token: 0x040002E5 RID: 741
	public const string Illegal_Device = "The '{0}' attribute does not support the use of device filters.";

	// Token: 0x040002E6 RID: 742
	public const string Illegal_Resource_Builder = "The '{0}' attribute does not support the use of expression builders.";

	// Token: 0x040002E7 RID: 743
	public const string Too_many_filters = "The string '{0}' contains too many device filters. There can be only one.";

	// Token: 0x040002E8 RID: 744
	public const string Device_unsupported_in_directive = "The '{0}' directive does not support the use of device filters on its attributes.";

	// Token: 0x040002E9 RID: 745
	public const string Cannot_add_value_not_collection = "'{0}' could not be added to the collection. Details: {1}";

	// Token: 0x040002EA RID: 746
	public const string ControlBuilder_CollectionHasNoAddMethod = "The collection '{0}' does not have an Add() method.";

	// Token: 0x040002EB RID: 747
	public const string Cannot_set_property = "'{0}' could not be set on property '{1}'.";

	// Token: 0x040002EC RID: 748
	public const string Cannot_set_recursive_skin = "Control '{0}' cannot be declared in a template inside a control skin of the same type with identical skinID.";

	// Token: 0x040002ED RID: 749
	public const string Cannot_evaluate_expression = "Cannot have the expression '{0}' that does not support evaluate in a non-compiled page.";

	// Token: 0x040002EE RID: 750
	public const string Cannot_init = "'{0}' could not be initialized. Details: {1}";

	// Token: 0x040002EF RID: 751
	public const string Unexpected_Directory = "'{0}' is a directory, not a file.";

	// Token: 0x040002F0 RID: 752
	public const string Circular_include = "Circular file references are not allowed.";

	// Token: 0x040002F1 RID: 753
	public const string Unexpected_eof_looking_for_tag = "Unexpected end of file looking for </{0}> tag.";

	// Token: 0x040002F2 RID: 754
	public const string Invalid_app_file_content = "The content in the application file is not valid.";

	// Token: 0x040002F3 RID: 755
	public const string Invalid_use_of_config_uc = "The page '{0}' cannot use the user control '{1}', because it is registered in web.config and lives in the same directory as the page.";

	// Token: 0x040002F4 RID: 756
	public const string Page_scope_in_global_asax = "The Page scope is not valid in global.asax.";

	// Token: 0x040002F5 RID: 757
	public const string App_session_only_valid_in_global_asax = "The Application and Session scopes are valid only in the global.asax file.";

	// Token: 0x040002F6 RID: 758
	public const string Multiple_forms_not_allowed = "A page can have only one server-side Form tag.";

	// Token: 0x040002F7 RID: 759
	public const string Postback_ctrl_not_found = "An error has occurred because a control with id '{0}' could not be located or a different control is assigned to the same ID after postback. If the ID is not assigned, explicitly set the ID property of controls that raise postback events to avoid this error.";

	// Token: 0x040002F8 RID: 760
	public const string Ctrl_not_data_handler = "Page.RegisterRequiresPostBack can only be called on controls that implement IPostBackDataHandler.";

	// Token: 0x040002F9 RID: 761
	public const string Transfer_not_allowed_in_callback = "Server.Transfer cannot be called in a Page callback.";

	// Token: 0x040002FA RID: 762
	public const string Redirect_not_allowed_in_callback = "Response.Redirect cannot be called in a Page callback.";

	// Token: 0x040002FB RID: 763
	public const string Script_tag_without_src_must_have_content = "A script tag without a src attribute must have content.";

	// Token: 0x040002FC RID: 764
	public const string Unknown_server_tag = "Unknown server tag '{0}'.";

	// Token: 0x040002FD RID: 765
	public const string Ambiguous_server_tag = "The server tag '{0}' is ambiguous. Please modify the associated registration that is causing ambiguity and pick a new tag prefix.";

	// Token: 0x040002FE RID: 766
	public const string Invalid_type_for_input_tag = "'{0}' is not a valid type for an input tag.";

	// Token: 0x040002FF RID: 767
	public const string Control_type_not_allowed = "The control type '{0}' is not allowed on this page.";

	// Token: 0x04000300 RID: 768
	public const string Base_type_not_allowed = "The base type '{0}' is not allowed for this page.";

	// Token: 0x04000301 RID: 769
	public const string Reference_not_allowed = "The referenced file '{0}' is not allowed on this page.";

	// Token: 0x04000302 RID: 770
	public const string Id_already_used = "The ID '{0}' is already used by another control.";

	// Token: 0x04000303 RID: 771
	public const string Duplicate_id_used = "Multiple controls with the same ID '{0}' were found. {1} requires that controls have unique IDs.";

	// Token: 0x04000304 RID: 772
	public const string Only_one_directive_allowed = "There can be only one '{0}' directive.";

	// Token: 0x04000305 RID: 773
	public const string Invalid_res_expr = "'{0}' is not a valid expression string. It needs to use the following syntax: [className,] resourceKey.";

	// Token: 0x04000306 RID: 774
	public const string Res_not_found = "The resource object with key '{0}' was not found.";

	// Token: 0x04000307 RID: 775
	public const string Res_not_found_with_class_and_key = "The resource object with classname '{0}' and key '{1}' was not found.";

	// Token: 0x04000308 RID: 776
	public const string Invalid_cache_settings_location = "The CacheSettings Location value is invalid.";

	// Token: 0x04000309 RID: 777
	public const string Registered_async_tasks_remain = "There are registered asynchronous tasks that were never executed during the page processing.";

	// Token: 0x0400030A RID: 778
	public const string Async_tasks_wrong_thread = "Cannot execute asynchronous tasks in the context of the current thread.";

	// Token: 0x0400030B RID: 779
	public const string Async_task_timed_out = "An asynchronous operation exceeded the page timeout.";

	// Token: 0x0400030C RID: 780
	public const string ClientScriptManager_RegisterForEventValidation_Too_Early = "RegisterForEventValidation can only be called during Render();";

	// Token: 0x0400030D RID: 781
	public const string ClientScriptManager_InvalidPostBackArgument = "Invalid postback or callback argument.  Event validation is enabled using <pages enableEventValidation=\"true\"/> in configuration or <%@ Page EnableEventValidation=\"true\" %> in a page.  For security purposes, this feature verifies that arguments to postback or callback events originate from the server control that originally rendered them.  If the data is valid and expected, use the ClientScriptManager.RegisterForEventValidation method in order to register the postback or callback data for validation.";

	// Token: 0x0400030E RID: 782
	public const string ClientScriptManager_JqueryNotRegistered = "WebForms UnobtrusiveValidationMode requires a ScriptResourceMapping for 'jquery'. Please add a ScriptResourceMapping named jquery(case-sensitive).";

	// Token: 0x0400030F RID: 783
	public const string DesignTimeTemplateParser_ErrorParsingTheme = "There was an error parsing the theme:";

	// Token: 0x04000310 RID: 784
	public const string Duplicate_registered_tag = "The '{0}' tag has already been registered.";

	// Token: 0x04000311 RID: 785
	public const string Empty_attribute = "The '{0}' attribute cannot be an empty string.";

	// Token: 0x04000312 RID: 786
	public const string Space_attribute = "The '{0}' attribute cannot contain spaces.";

	// Token: 0x04000313 RID: 787
	public const string Empty_expression = "The expression in a <%= %> or <%# %> block cannot be an empty string.";

	// Token: 0x04000314 RID: 788
	public const string ControlBuilder_DatabindingRequiresEvent = "Databinding expressions are only supported on objects that have a DataBinding event. {0} does not have a DataBinding event.";

	// Token: 0x04000315 RID: 789
	public const string ControlBuilder_TwoWayBindingNonProperty = "Two-way binding is only supported for properties. '{0}' is not a valid property on '{1}'";

	// Token: 0x04000316 RID: 790
	public const string ControlBuilder_CannotHaveMultipleBoundEntries = "Cannot have more than one binding on property '{0}' on '{1}'. Ensure that this property is not bound through an implicit expression, for example, using meta:resourcekey.";

	// Token: 0x04000317 RID: 791
	public const string ControlBuilder_ExpressionsNotAllowedInThemes = "Expressions are not allowed in skin files.";

	// Token: 0x04000318 RID: 792
	public const string FilteredAttributeDictionary_ArgumentMustBeString = "The argument must be a string.";

	// Token: 0x04000319 RID: 793
	public const string HotSpotCollection_InvalidType = "Object is not a HotSpot.";

	// Token: 0x0400031A RID: 794
	public const string HotSpotCollection_InvalidTypeIndex = "Type index is out of bounds.";

	// Token: 0x0400031B RID: 795
	public const string Invalid_attribute_value = "'{0}' is not a valid value for attribute '{1}'.";

	// Token: 0x0400031C RID: 796
	public const string Invalid_boolean_attribute = "The '{0}' attribute must be set to 'true' or 'false'.";

	// Token: 0x0400031D RID: 797
	public const string Invalid_integer_attribute = "The '{0}' attribute must be an integer value.";

	// Token: 0x0400031E RID: 798
	public const string Invalid_nonnegative_integer_attribute = "The '{0}' attribute must be a non-negative integer.";

	// Token: 0x0400031F RID: 799
	public const string Invalid_positive_integer_attribute = "The '{0}' attribute must be set to a positive integer value.";

	// Token: 0x04000320 RID: 800
	public const string Invalid_non_zero_hexadecimal_attribute = "The '{0}' attribute must be set to a non-zero hexadecimal value.";

	// Token: 0x04000321 RID: 801
	public const string Invalid_hash_algorithm_type = "The '{0}' hash algorithm type could not be instantiated.";

	// Token: 0x04000322 RID: 802
	public const string Invalid_enum_attribute = "The '{0}' attribute must be one of the following values: {1}.";

	// Token: 0x04000323 RID: 803
	public const string Invalid_culture_attribute = "The 'Culture' attribute must be set to a non-neutral culture.  Try one of the following: {0}.";

	// Token: 0x04000324 RID: 804
	public const string Invalid_temp_directory = "The '{0}' attribute must be set to a valid absolute path.";

	// Token: 0x04000325 RID: 805
	public const string Invalid_reference_directive = "The Reference directive must have a VirtualPath attribute, and no other attributes.";

	// Token: 0x04000326 RID: 806
	public const string Invalid_reference_directive_attrib = "The file '{0}' is not of a type that can be used here.";

	// Token: 0x04000327 RID: 807
	public const string Invalid_typeNameOrVirtualPath_directive = "The '{0}' directive must have exactly one attribute: TypeName or VirtualPath";

	// Token: 0x04000328 RID: 808
	public const string Invalid_tagprefix_entry = "Invalid or missing attributes found in the tagPrefix entry. For user control, you must also specify 'tagName' and 'src'. For custom control, you must also specify 'namespace', and optionally 'assembly'.";

	// Token: 0x04000329 RID: 809
	public const string Mapped_type_must_inherit = "The specified type '{0}' used for mapping must inherit from the original type '{1}'.";

	// Token: 0x0400032A RID: 810
	public const string Missing_required_attribute = "The '{0}' attribute must be specified on the '{1}' tag.";

	// Token: 0x0400032B RID: 811
	public const string Missing_required_attributes = "The '{0}' or '{1}' attribute must be specified on the '{1}' tag.";

	// Token: 0x0400032C RID: 812
	public const string Attr_not_supported_in_directive = "The '{0}' attribute is not supported by the '{1}' directive.";

	// Token: 0x0400032D RID: 813
	public const string Attr_not_supported_in_ucdirective = "The '{0}' attribute is not supported by the '{1}' directive in a user control.";

	// Token: 0x0400032E RID: 814
	public const string Attr_not_supported_in_pagedirective = "The '{0}' attribute is not supported by the '{1}' directive in a page.";

	// Token: 0x0400032F RID: 815
	public const string Invalid_attr = "The '{0}' attribute is not supported on this directive when a '{1}' attribute is present.";

	// Token: 0x04000330 RID: 816
	public const string Attrib_parse_error = "Error parsing attribute '{0}': {1}";

	// Token: 0x04000331 RID: 817
	public const string Missing_attr = "The directive is missing a '{0}' attribute.";

	// Token: 0x04000332 RID: 818
	public const string Missing_output_cache_attr = "The directive or the configuration settings profile must specify the '{0}' attribute.";

	// Token: 0x04000333 RID: 819
	public const string Missing_varybyparam_attr = "The directive is missing a 'VaryByParam' attribute, which should be set to \"none\", \"*\", or a semicolon separated list of values.";

	// Token: 0x04000334 RID: 820
	public const string Missing_directive = "The page must have a <%@ {0} class=\"MyNamespace.MyClass\" ... %> directive.";

	// Token: 0x04000335 RID: 821
	public const string Unknown_directive = "The directive '{0}' is unknown.";

	// Token: 0x04000336 RID: 822
	public const string Malformed_server_tag = "The server tag is not well formed.";

	// Token: 0x04000337 RID: 823
	public const string Malformed_server_block = "The server block is not well formed.";

	// Token: 0x04000338 RID: 824
	public const string Server_tags_cant_contain_percent_constructs = "Server tags cannot contain <% ... %> constructs.";

	// Token: 0x04000339 RID: 825
	public const string Include_not_allowed_in_server_script_tag = "Server includes are not allowed in server script tags.";

	// Token: 0x0400033A RID: 826
	public const string Incompatible_with_get_bufferless_input_stream = "This method or property is not supported after HttpRequest.GetBufferlessInputStream has been invoked.";

	// Token: 0x0400033B RID: 827
	public const string Incompatible_with_get_buffered_input_stream = "This method or property is not supported after HttpRequest.GetBufferedInputStream has been invoked.";

	// Token: 0x0400033C RID: 828
	public const string Incompatible_with_input_stream = "This method or property is not supported after HttpRequest.Form, Files, InputStream, or BinaryRead has been invoked.";

	// Token: 0x0400033D RID: 829
	public const string Invalid_operation_with_get_buffered_input_stream = "Either BinaryRead, Form, Files, or InputStream was accessed before the internal storage was filled by the caller of HttpRequest.GetBufferedInputStream.";

	// Token: 0x0400033E RID: 830
	public const string Only_file_virtual_supported_on_server_include = "Only file and virtual are valid attributes in server-side include tags.";

	// Token: 0x0400033F RID: 831
	public const string Runat_can_only_be_server = "The Runat attribute must have the value Server.";

	// Token: 0x04000340 RID: 832
	public const string Invalid_identifier = "'{0}' is not a valid identifier.";

	// Token: 0x04000341 RID: 833
	public const string Invalid_resourcekey = "'{0}' is not a valid resource key.";

	// Token: 0x04000342 RID: 834
	public const string ControlBuilder_IDMustUseAttribute = "The ID property of a control can only be set using the ID attribute in the tag and a simple value. Example: <asp:Button runat=\"server\" id=\"Button1\" />";

	// Token: 0x04000343 RID: 835
	public const string ControlBuilder_CannotHaveComplexString = "The '{1}' property of '{0}' cannot be declared as an inner property, it must be declared as an attribute.";

	// Token: 0x04000344 RID: 836
	public const string ControlBuilder_ParseTimeDataNotAvailable = "The ParseTimeData property can only be used during parsing.";

	// Token: 0x04000345 RID: 837
	public const string Duplicate_attr_in_directive = "The directive contains duplicate '{0}' attributes.";

	// Token: 0x04000346 RID: 838
	public const string Duplicate_attr_in_tag = "The tag contains duplicate '{0}' attributes.";

	// Token: 0x04000347 RID: 839
	public const string Non_existent_base_type = "The base type '{0}' does not exist in the source file '{1}'.";

	// Token: 0x04000348 RID: 840
	public const string Invalid_type_to_inherit_from = "'{0}' is not allowed here because it does not extend class '{1}'.";

	// Token: 0x04000349 RID: 841
	public const string Invalid_type_to_implement = "'{0}' exists but is not an interface.";

	// Token: 0x0400034A RID: 842
	public const string Error_page_not_supported_when_buffering_off = "Error page is not supported when buffering is disabled.";

	// Token: 0x0400034B RID: 843
	public const string Enablesessionstate_must_be_true_false_or_readonly = "enableSessionState must be set to true, false or ReadOnly";

	// Token: 0x0400034C RID: 844
	public const string Attributes_mutually_exclusive = "The '{0}' and '{1}' attributes are mutually exclusive.";

	// Token: 0x0400034D RID: 845
	public const string Async_and_aspcompat = "Async attribute cannot be set to true when AspCompat mode is enabled.";

	// Token: 0x0400034E RID: 846
	public const string Async_and_transaction = "Async attribute cannot be set to true when Transaction mode is enabled.";

	// Token: 0x0400034F RID: 847
	public const string Async_required = "This operation requires the page to be asynchronous (the Async attribute must be set to true).";

	// Token: 0x04000350 RID: 848
	public const string Async_addhandler_too_late = "This operation can only be performed prior to PreRenderComplete page event.";

	// Token: 0x04000351 RID: 849
	public const string Async_operation_disabled = "Asynchronous operations are not allowed in this context. Page starting an asynchronous operation has to have the Async attribute set to true and an asynchronous operation can only be started on a page prior to PreRenderComplete event.";

	// Token: 0x04000352 RID: 850
	public const string Async_operation_pending = "There is a pending asynchronous operation, and only one asynchronous operation can be pending concurrently.";

	// Token: 0x04000353 RID: 851
	public const string Async_null_asyncresult = "IAsyncResult returned from Begin method is null.";

	// Token: 0x04000354 RID: 852
	public const string Async_operation_cannot_be_started = "An asynchronous operation cannot be started at this time. Asynchronous operations may only be started within an asynchronous handler or module or during certain events in the Page lifecycle. If this exception occurred while executing a Page, ensure that the Page is marked <%@ Page Async=\"true\" %>. This exception may also indicate an attempt to call an \"async void\" method, which is generally unsupported within ASP.NET request processing. Instead, the asynchronous method should return a Task, and the caller should await it.";

	// Token: 0x04000355 RID: 853
	public const string Async_operation_cannot_be_pending = "An asynchronous module or handler completed while an asynchronous operation was still pending.";

	// Token: 0x04000356 RID: 854
	public const string Server_execute_blocked_on_async_handler = "HttpServerUtility.Execute blocked while waiting for an asynchronous operation to complete.";

	// Token: 0x04000357 RID: 855
	public const string Mixed_lang_not_supported = "Cannot use '{0}' because another language has been specified earlier in this page (or was implied from a CodeFile attribute).";

	// Token: 0x04000358 RID: 856
	public const string Inconsistent_CodeFile_Language = "The language of the code file is inconsistent with the language of the page.";

	// Token: 0x04000359 RID: 857
	public const string Codefile_without_inherits = "The 'CodeFile' attribute cannot be used without an 'Inherits' attribute.";

	// Token: 0x0400035A RID: 858
	public const string CodeFileBaseClass_Without_Codefile = "The 'CodeFileBaseClass' attribute cannot be used without a 'CodeFile' attribute.";

	// Token: 0x0400035B RID: 859
	public const string Invalid_lang = "'{0}' is not a supported language.";

	// Token: 0x0400035C RID: 860
	public const string Invalid_lang_extension = "'{0}' is not a valid language extension.";

	// Token: 0x0400035D RID: 861
	public const string Cant_use_nocompile_uc = "The user control '{0}' is not compiled, and can only be used dynamically. To force it to be compiled, set compilationMode=\"Always\" in its @control directive.";

	// Token: 0x0400035E RID: 862
	public const string Invalid_CodeSubDirectory_Not_Exist = "The code subdirectory '{0}' does not exist.";

	// Token: 0x0400035F RID: 863
	public const string Invalid_CodeSubDirectory = "Invalid subdirectory '{0}'. Only subdirectories directly under the App_Code directory are allowed.";

	// Token: 0x04000360 RID: 864
	public const string Reserved_AssemblyName = "'{0}' is a reserved assembly name, and cannot be used for a code subdirectory.";

	// Token: 0x04000361 RID: 865
	public const string Empty_extension = "The file '{0}' must have an extension in order to be compiled.";

	// Token: 0x04000362 RID: 866
	public const string Base_class_field_with_type_different_from_type_of_control = "The base class includes the field '{0}', but its type ({1}) is not compatible with the type of control ({2}).";

	// Token: 0x04000363 RID: 867
	public const string ControlSkin_cannot_contain_controls = "Control skins cannot contain child controls.";

	// Token: 0x04000364 RID: 868
	public const string Inner_Content_not_literal = "Cannot get inner content of {0} because the contents are not literal.";

	// Token: 0x04000365 RID: 869
	public const string Invalid_client_target = "ClientTarget is set to an invalid alias '{0}'.  The <clientTarget> configuration section is used to define ClientTarget aliases.";

	// Token: 0x04000366 RID: 870
	public const string Empty_file_name = "The file name cannot be an empty string.";

	// Token: 0x04000367 RID: 871
	public const string SetStyleSheetThemeCannotBeSet = "The StyleSheetTheme property cannot be set, please override the property instead.";

	// Token: 0x04000368 RID: 872
	public const string PropertySetBeforePageEvent = "The '{0}' property can only be set in or before the '{1}' event.";

	// Token: 0x04000369 RID: 873
	public const string PropertySetBeforeStyleSheetApplied = "The '{0}' property cannot be changed dynamically if Page has a stylesheet theme. For dynamic controls, set the property before calling ApplyStyleSheetSkin().";

	// Token: 0x0400036A RID: 874
	public const string PropertySetBeforePreInitOrAddToControls = "The '{0}' property can only be set in or before the Page_PreInit event for static controls. For dynamic controls, set the property before adding it to the Controls collection.";

	// Token: 0x0400036B RID: 875
	public const string PropertySetAfterFrameworkInitialize = "The '{0}' property can only be set in the page directive or in the <pages> configuration section.";

	// Token: 0x0400036C RID: 876
	public const string StyleSheetAreadyAppliedOnControl = "StyleSheetTheme is already applied on the control, it cannot be applied more than once.";

	// Token: 0x0400036D RID: 877
	public const string Control_CannotOwnSelf = "A control cannot own itself.";

	// Token: 0x0400036E RID: 878
	public const string AdRotator_cant_open_file = "The AdRotator {0} was unable to open the AdvertisementFile '{1}'.";

	// Token: 0x0400036F RID: 879
	public const string AdRotator_cant_open_file_no_permission = "The AdRotator {0} could not find the AdvertisementFile or the file is invalid.";

	// Token: 0x04000370 RID: 880
	public const string AdRotator_parse_error = "The AdRotator {0} was unable to parse the XML in the AdvertisementFile. {1}";

	// Token: 0x04000371 RID: 881
	public const string AdRotator_no_advertisements = "The AdRotator {0} found no valid advertisements in the file '{1}'.";

	// Token: 0x04000372 RID: 882
	public const string AdRotator_only_one_datasource = "Only one of AdvertisementFile, DataSource, or DataSourceID properties can be set on AdRotator '{0}'.";

	// Token: 0x04000373 RID: 883
	public const string AdRotator_invalid_integer_format = "The value '{0}' of field '{1}' of an advertisement data has to be a valid string to be parsed by {2}.";

	// Token: 0x04000374 RID: 884
	public const string AdRotator_expect_records_with_advertisement_properties = "The AdRotator '{0}' is expecting the data type of the first item in the collection from the DataSource property to have advertisement properties such as ImageUrl, NavigateUrl, etc..  The current data type of the item is '{1}'.";

	// Token: 0x04000375 RID: 885
	public const string Validator_control_blank = "The ControlToValidate property of '{0}' cannot be blank.";

	// Token: 0x04000376 RID: 886
	public const string Validator_control_not_found = "Unable to find control id '{0}' referenced by the '{1}' property of '{2}'.";

	// Token: 0x04000377 RID: 887
	public const string Validator_bad_compare_control = "Control '{0}' cannot have the same value '{1}' for both ControlToValidate and ControlToCompare.";

	// Token: 0x04000378 RID: 888
	public const string Validator_bad_control_type = "Control '{0}' referenced by the {1} property of '{2}' cannot be validated.";

	// Token: 0x04000379 RID: 889
	public const string Validator_value_bad_type = "The value '{0}' of the {1} property of '{2}' cannot be converted to type '{3}'.";

	// Token: 0x0400037A RID: 890
	public const string Validator_range_overalap = "The MaximumValue {0} cannot be less than the MinimumValue {1} of {2}.";

	// Token: 0x0400037B RID: 891
	public const string Validator_bad_regex = "{0} is not a valid regular expression.";

	// Token: 0x0400037C RID: 892
	public const string ValSummary_error_message_1 = "Error message 1.";

	// Token: 0x0400037D RID: 893
	public const string ValSummary_error_message_2 = "Error message 2.";

	// Token: 0x0400037E RID: 894
	public const string ViewState_MissingViewStateField = "Invalid viewstate: Missing field: {0}.";

	// Token: 0x0400037F RID: 895
	public const string ViewState_InvalidViewState = "Invalid viewstate.";

	// Token: 0x04000380 RID: 896
	public const string ViewState_InvalidViewStatePlus = "Invalid viewstate. {0}";

	// Token: 0x04000381 RID: 897
	public const string ClientDisconnected = "The client disconnected.";

	// Token: 0x04000382 RID: 898
	public const string HttpBufferlessInputStream_ClientDisconnected = "The client is disconnected because the underlying request has been completed.  There is no longer an HttpContext available.";

	// Token: 0x04000383 RID: 899
	public const string ViewState_ClientDisconnected = "The client disconnected.";

	// Token: 0x04000384 RID: 900
	public const string ViewState_AuthenticationFailed = "Validation of viewstate MAC failed. If this application is hosted by a Web Farm or cluster, ensure that <machineKey> configuration specifies the same validationKey and validation algorithm. AutoGenerate cannot be used in a cluster.\r\n\r\nSee http://go.microsoft.com/fwlink/?LinkID=314055 for more information.";

	// Token: 0x04000385 RID: 901
	public const string Control_does_not_allow_children = "'{0}' does not allow child controls.";

	// Token: 0x04000386 RID: 902
	public const string DataBinder_Prop_Not_Found = "DataBinding: '{0}' does not contain a property with the name '{1}'.";

	// Token: 0x04000387 RID: 903
	public const string DataBinder_Invalid_Indexed_Expr = "DataBinding: '{0}' is not a valid indexed expression.";

	// Token: 0x04000388 RID: 904
	public const string DataBinder_No_Indexed_Accessor = "DataBinding: '{0}' does not allow indexed access.";

	// Token: 0x04000389 RID: 905
	public const string XPathBinder_MustBeIXPathNavigable = "XPath DataBinding: '{0}' must implement IXPathNavigable.";

	// Token: 0x0400038A RID: 906
	public const string XPathBinder_MustHaveXmlNodes = "XPathBinder.Select can only be used with data sources that contain XmlNodes.";

	// Token: 0x0400038B RID: 907
	public const string Field_Not_Found = "A field or property with the name '{0}' was not found on the selected data source.";

	// Token: 0x0400038C RID: 908
	public const string DataItem_Not_Found = "A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.";

	// Token: 0x0400038D RID: 909
	public const string DataGrid_Missing_VirtualItemCount = "AllowCustomPaging must be true and VirtualItemCount must be set for a DataGrid with ID '{0}' when AllowPaging is set to true and the selected data source does not implement ICollection.";

	// Token: 0x0400038E RID: 910
	public const string DataGrid_NoAutoGenColumns = "DataGrid with id '{0}' could not automatically generate any columns from the selected data source.";

	// Token: 0x0400038F RID: 911
	public const string GridView_Missing_VirtualItemCount = "GridView with id '{0}' must have a data source that either implements ICollection or can perform data source paging if AllowPaging is true.";

	// Token: 0x04000390 RID: 912
	public const string GridView_NoAutoGenFields = "The data source for GridView with id '{0}' did not have any properties or attributes from which to generate columns.  Ensure that your data source has content.";

	// Token: 0x04000391 RID: 913
	public const string GridView_DataSourceReturnedNullView = "The IDataSource that is the data source for GridView '{0}' returned a null view.  Check that the DataMember property value of GridView is valid.";

	// Token: 0x04000392 RID: 914
	public const string GridView_UnhandledEvent = "The GridView '{0}' fired event {1} which wasn't handled.";

	// Token: 0x04000393 RID: 915
	public const string GridView_MustBeParented = "A GridView with EnableSortingAndPagingCallbacks set to true must be parented to a naming container before Render is called.";

	// Token: 0x04000394 RID: 916
	public const string GridView_DataKeyNamesMustBeSpecified = "Data keys must be specified on GridView '{0}' before the selected data keys can be retrieved.  Use the DataKeyNames property to specify data keys.";

	// Token: 0x04000395 RID: 917
	public const string GridView_PersistedSelectionRequiresDataKeysNames = "DataKeyNames must be specified for persisted selection to work.";

	// Token: 0x04000396 RID: 918
	public const string DetailsView_NoAutoGenFields = "DetailsView with id '{0}' did not have any properties or attributes from which to generate fields.  Ensure that your data source has content.";

	// Token: 0x04000397 RID: 919
	public const string DetailsView_UnhandledEvent = "The DetailsView '{0}' fired event {1} which wasn't handled.";

	// Token: 0x04000398 RID: 920
	public const string DetailsView_DataSourceMustBeCollection = "DetailsView with id '{0}' must have a data source that implements ICollection if AllowPaging is true.";

	// Token: 0x04000399 RID: 921
	public const string DetailsView_MustBeParented = "A DetailsView with EnablePagingCallbacks set to true must be parented to a naming container before Render is called.";

	// Token: 0x0400039A RID: 922
	public const string FileUpload_AllowMultiple = "Whether to enable multi-file uploads.";

	// Token: 0x0400039B RID: 923
	public const string FileUpload_StreamNotSeekable = "The stream returned by FileContent does not support seeking, so FileBytes is not supported.";

	// Token: 0x0400039C RID: 924
	public const string FileUpload_StreamTooLong = "The stream returned by FileContent is longer than Int32.MaxValue.  FileBytes supports only streams less than or equal to Int32.MaxValue.";

	// Token: 0x0400039D RID: 925
	public const string FileUpload_StreamLengthNotReached = "The byte stream that represents the uploaded file appears to be incomplete.  Try the upload again.";

	// Token: 0x0400039E RID: 926
	public const string FormView_UnhandledEvent = "The FormView '{0}' fired event {1} which wasn't handled.";

	// Token: 0x0400039F RID: 927
	public const string FormView_DataSourceMustBeCollection = "DetailsView with id '{0}' must have a data source that implements ICollection if AllowPaging is true.";

	// Token: 0x040003A0 RID: 928
	public const string DetailsViewFormView_ControlMustBeInEditMode = "{0} '{1}' must be in edit mode to update a record.";

	// Token: 0x040003A1 RID: 929
	public const string DetailsViewFormView_ControlMustBeInInsertMode = "{0} '{1}' must be in insert mode to insert a new record.";

	// Token: 0x040003A2 RID: 930
	public const string DataBoundControl_InvalidDataPropertyChange = "Data properties on data control '{0}' such as DataSource, DataSourceID, and DataMember cannot be changed during the databinding phase of the control.";

	// Token: 0x040003A3 RID: 931
	public const string DataBoundControl_NullView = "The data source retrieved by '{0}' returned a null DataSourceView.";

	// Token: 0x040003A4 RID: 932
	public const string DataBoundControl_InvalidDataSourceType = "Data source is an invalid type.  It must be either an IListSource, IEnumerable, or IDataSource.";

	// Token: 0x040003A5 RID: 933
	public const string DataBoundControl_DataSourceDoesntSupportPaging = "The data source '{0}' does not support server-side paging and it returned non-ICollection data.  See your data source documentation to enable paging.";

	// Token: 0x040003A6 RID: 934
	public const string DataBoundControl_CallingDataMethods = "Occurs before model methods are invoked for data operations. Handle this event if the model methods are defined on a custom type other than the code behind file.";

	// Token: 0x040003A7 RID: 935
	public const string DataBoundControl_NeedICollectionOrTotalRowCount = "If a data source does not return ICollection and cannot return the total row count, it cannot be used by the {0} to implement server-side paging.";

	// Token: 0x040003A8 RID: 936
	public const string DataBoundControlHelper_NoNamingContainer = "The {0} control '{1}' does not have a naming container.  Ensure that the control is added to the page before calling DataBind.";

	// Token: 0x040003A9 RID: 937
	public const string HierarchicalDataBoundControl_InvalidDataSource = "HierarchicalDataBoundControl only accepts data sources that implement IHierarchicalDataSource or IHierarchicalEnumerable.";

	// Token: 0x040003AA RID: 938
	public const string DataBoundControl_OnCreatingModelDataSource = "Raised before the data bound control is data binding using data methods.";

	// Token: 0x040003AB RID: 939
	public const string HierarchicalDataControl_ViewNotFound = "The view that hierarchical data bound control '{0}' requested could not be found.";

	// Token: 0x040003AC RID: 940
	public const string HierarchicalDataControl_DataSourceIDMustBeHierarchicalDataControl = "The DataSourceID of '{0}' must be the ID of a control of type IHierarchicalDataSource.  '{1}' is not an IHierarchicalDataSource.";

	// Token: 0x040003AD RID: 941
	public const string HierarchicalDataControl_DataSourceDoesntExist = "The DataSourceID of '{0}' must be the ID of a control of type IHierarchicalDataSource.  A control with ID '{1}' could not be found.";

	// Token: 0x040003AE RID: 942
	public const string DataControl_ViewNotFound = "The view that data bound control '{0}' requested could not be found. Check that the DataMember property is valid.";

	// Token: 0x040003AF RID: 943
	public const string DataControl_DataSourceIDMustBeDataControl = "The DataSourceID of '{0}' must be the ID of a control of type IDataSource.  '{1}' is not an IDataSource.";

	// Token: 0x040003B0 RID: 944
	public const string DataControl_DataSourceDoesntExist = "The DataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.";

	// Token: 0x040003B1 RID: 945
	public const string DataControl_MultipleDataSources = "Both DataSource and DataSourceID are defined on '{0}'.  Remove one definition.";

	// Token: 0x040003B2 RID: 946
	public const string DataControl_ItemType_MultipleDataSources = "DataSource or DataSourceID cannot be defined on '{0}' when it uses model binding.";

	// Token: 0x040003B3 RID: 947
	public const string DataControlField_NoContainer = "A DataControlField must be within an INamingContainer.";

	// Token: 0x040003B4 RID: 948
	public const string DataControlField_CallbacksNotSupported = "Callbacks are not supported on this data control field.  Turn callbacks off on '{0}'.";

	// Token: 0x040003B5 RID: 949
	public const string DataControlFieldCollection_InvalidType = "Object is not a DataControlField.";

	// Token: 0x040003B6 RID: 950
	public const string DataControlFieldCollection_InvalidTypeIndex = "Type index is out of bounds.";

	// Token: 0x040003B7 RID: 951
	public const string BoundField_WrongControlType = "BoundField {0} contains a control that isn't a TextBox.  Override OnDataBindField to inherit from BoundField and add different controls.";

	// Token: 0x040003B8 RID: 952
	public const string CheckBoxField_WrongControlType = "CheckBoxField '{0}' contains a control that isn't a CheckBox.  Override OnDataBindField to inherit from CheckBoxField and add different controls.";

	// Token: 0x040003B9 RID: 953
	public const string CheckBoxField_CouldntParseAsBoolean = "The data in the CheckBoxField '{0}' could not be parsed as a boolean value.  Try using a BoundField instead.";

	// Token: 0x040003BA RID: 954
	public const string CheckBoxField_NotSupported = "The property {0} is not supported on CheckBoxField.";

	// Token: 0x040003BB RID: 955
	public const string CommandField_CallbacksNotSupported = "Callbacks are not supported on CommandField when the select button is enabled because other controls on your page that are dependent on the selected value of '{0}' for their rendering will not update in a callback.  Turn callbacks off on '{0}'.";

	// Token: 0x040003BC RID: 956
	public const string ImageField_WrongControlType = "ImageField {0} contains a control that isn't a TextBox in edit mode, or an Image and a Label in read-only mode.  Override OnDataBindField to inherit from ImageField and add different controls.";

	// Token: 0x040003BD RID: 957
	public const string TemplateField_CallbacksNotSupported = "Callbacks are not supported on TemplateField because some controls cannot update properly in a callback.  Turn callbacks off on '{0}'.";

	// Token: 0x040003BE RID: 958
	public const string PagedDataSource_Cannot_Get_Count = "Cannot compute Count for a data source that does not implement ICollection.";

	// Token: 0x040003BF RID: 959
	public const string Cannot_Have_Children_Of_Type = "'{0}' cannot have children of type '{1}'.";

	// Token: 0x040003C0 RID: 960
	public const string Control_Cannot_Databind = "'{0}' cannot contain a data binding expression.";

	// Token: 0x040003C1 RID: 961
	public const string InnerHtml_not_supported = "'{0}' does not support the InnerHtml property.";

	// Token: 0x040003C2 RID: 962
	public const string InnerText_not_supported = "'{0}' does not support the InnerText property.";

	// Token: 0x040003C3 RID: 963
	public const string ListControl_SelectionOutOfRange = "'{0}' has a {1} which is invalid because it does not exist in the list of items.";

	// Token: 0x040003C4 RID: 964
	public const string ListControl_RenderWhenDataEmptyNotSupportedWithTableLayout = "The RepeatLayout property on control '{0}' does not support Table layout when RenderWhenDataEmpty is enabled.";

	// Token: 0x040003C5 RID: 965
	public const string ListControl_RenderWhenDataEmpty = "Render the control's outer markup when not bound to data or data is empty.";

	// Token: 0x040003C6 RID: 966
	public const string BulletedList_SelectionNotSupported = "Setting the SelectedIndex or SelectedValue properties of BulletedList is not supported.";

	// Token: 0x040003C7 RID: 967
	public const string BulletedList_TextNotSupported = "Setting the Text property of BulletedList is not supported.";

	// Token: 0x040003C8 RID: 968
	public const string CannotUseParentPostBackWhenValidating = "A button that causes validation in {0} '{1}' is attempting to use the container {0} as the post back target.  The button should either turn off validation or use itself as the post back container.";

	// Token: 0x040003C9 RID: 969
	public const string CannotSetValidationOnDataControlButtons = "Setting CausesValidation on DataControlButtons is not supported.";

	// Token: 0x040003CA RID: 970
	public const string CannotSetValidationOnPagerButtons = "Setting CausesValidation on DataControlPagerLinkButtons is not supported.";

	// Token: 0x040003CB RID: 971
	public const string Invalid_DataSource_Type = "An invalid data source is being used for {0}. A valid data source must implement either IListSource or IEnumerable.";

	// Token: 0x040003CC RID: 972
	public const string Invalid_CurrentPageIndex = "Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount.";

	// Token: 0x040003CD RID: 973
	public const string ListSource_Without_DataMembers = "The IListSource does not contain any data sources.";

	// Token: 0x040003CE RID: 974
	public const string ListSource_Missing_DataMember = "The IListSource does not contain a data source named '{0}'.  Check your DataMember value.";

	// Token: 0x040003CF RID: 975
	public const string Enumerator_MoveNext_Not_Called = "You must call MoveNext on IEnumerator before accessing the Current property.";

	// Token: 0x040003D0 RID: 976
	public const string Sample_Databound_Text = "Databound";

	// Token: 0x040003D1 RID: 977
	public const string Resource_problem = "An error occurred while try to load the string resources ({0} failed with error {1}).";

	// Token: 0x040003D2 RID: 978
	public const string Duplicate_Resource_File = "The resource file '{0}' cannot be used, as it conflicts with another file with the same name.";

	// Token: 0x040003D3 RID: 979
	public const string Property_Had_Malformed_Url = "The '{0}' property had a malformed URL: {1}.";

	// Token: 0x040003D4 RID: 980
	public const string TypeResService_Needed = "The supplied IDesignerHost must provide an implementation of ITypeResolutionService.";

	// Token: 0x040003D5 RID: 981
	public const string DataList_TemplateTableNotFound = "A Table control was not found in the template for '{0}' for an item of type 'ListItemType.{1}'.";

	// Token: 0x040003D6 RID: 982
	public const string DataList_DataKeyFieldMustBeSpecified = "Data keys must be specified on DataList '{0}' before the selected data key can be retrieved.  Use the DataKeyField property to specify data keys.";

	// Token: 0x040003D7 RID: 983
	public const string DataList_LayoutNotSupported = "DataList does not support the '{0}' layout.";

	// Token: 0x040003D8 RID: 984
	public const string EnumAttributeInvalidString = "'{0}' is not a valid value for the '{2}' attribute. '{2}' must be a single text (not numeric) value from the '{3}' enumeration.";

	// Token: 0x040003D9 RID: 985
	public const string UnitParseNumericPart = "The numeric part ('{1}') of '{0}' cannot be parsed as a numeric part of a {2} unit.";

	// Token: 0x040003DA RID: 986
	public const string UnitParseNoDigits = "'{0}' cannot be parsed as a unit as there are no numeric values in it. Examples of valid unit strings are '1px' and '.5in'.";

	// Token: 0x040003DB RID: 987
	public const string IsValid_Cant_Be_Called = "Page.IsValid cannot be called before validation has taken place. It should be queried in the event handler for a control that has CausesValidation=True and initiated the postback, or after a call to Page.Validate.";

	// Token: 0x040003DC RID: 988
	public const string Invalid_HtmlTextWriter = "An instance of '{0}' could not be used as an HtmlTextWriter. Make sure the specified class can be instantiated, extends System.Web.UI.HtmlTextWriter, and implements a constructor with a single parameter of type System.IO.TextWriter.";

	// Token: 0x040003DD RID: 989
	public const string Form_Needs_Page = "HtmlForm cannot render without a reference to the Page instance.  Make sure your form has been added to the control tree.";

	// Token: 0x040003DE RID: 990
	public const string InvalidDefaultAutoFieldGenerator = "The field generator of type '{0}' can be used only with '{1}'.";

	// Token: 0x040003DF RID: 991
	public const string HtmlForm_OnlyIButtonControlCanBeDefaultButton = "The DefaultButton of '{0}' must be the ID of a control of type IButtonControl.";

	// Token: 0x040003E0 RID: 992
	public const string Head_Needs_Page = "HtmlHead cannot render without a reference to the Page instance.  Make sure your head has been added to the control tree.";

	// Token: 0x040003E1 RID: 993
	public const string HtmlHead_StyleAlreadyRegistered = "You cannot register a style twice.";

	// Token: 0x040003E2 RID: 994
	public const string HtmlHead_OnlyOneHeadAllowed = "You can only have one <head runat=\"server\"> control on a page.";

	// Token: 0x040003E3 RID: 995
	public const string HtmlHead_OnlyOneTitleAllowed = "You can only have one <title> element within the <head> element.";

	// Token: 0x040003E4 RID: 996
	public const string Style_RegisteredStylesAreReadOnly = "Registered Styles are read-only.";

	// Token: 0x040003E5 RID: 997
	public const string Style_InvalidBorderWidth = "BorderWidth must be a non-negative number and cannot be a percentage.";

	// Token: 0x040003E6 RID: 998
	public const string Style_InvalidWidth = "Width must be non negative.";

	// Token: 0x040003E7 RID: 999
	public const string Style_InvalidHeight = "Height must be non negative.";

	// Token: 0x040003E8 RID: 1000
	public const string Cant_Multiselect_In_Single_Mode = "Cannot have multiple items selected when the SelectionMode is Single.";

	// Token: 0x040003E9 RID: 1001
	public const string Cant_Multiselect = "Cannot have multiple items selected in a {0}.";

	// Token: 0x040003EA RID: 1002
	public const string HtmlSelect_Cant_Multiselect_In_Single_Mode = "An HtmlSelect cannot have multiple items selected when Multiple is false.";

	// Token: 0x040003EB RID: 1003
	public const string Controls_Cant_Change_Between_Posts = "Failed to load viewstate.  The control tree into which viewstate is being loaded must match the control tree that was used to save viewstate during the previous request.  For example, when adding controls dynamically, the controls added during a post-back must match the type and position of the controls added during the initial request.";

	// Token: 0x040003EC RID: 1004
	public const string Value_Set_Not_Supported = "The value property on {0} is not settable.";

	// Token: 0x040003ED RID: 1005
	public const string SiteMap_feature_disabled = "This feature is currently disabled, please enable section {0} in the configuration file.";

	// Token: 0x040003EE RID: 1006
	public const string SiteMapNode_readonly = "SiteMapNode is readonly, property {0} cannot be modified.";

	// Token: 0x040003EF RID: 1007
	public const string SiteMapNodeCollection_Invalid_Type = "Invalid value of type {0} passed in, value must be of type SiteMapNode.";

	// Token: 0x040003F0 RID: 1008
	public const string SiteMapProvider_Circular_Provider = "Circular Providers not allowed.";

	// Token: 0x040003F1 RID: 1009
	public const string SiteMapProvider_Invalid_RootNode = "Root node defined in provider {0} is null, root node cannot be null.";

	// Token: 0x040003F2 RID: 1010
	public const string SiteMapProvider_cannot_remove_root_node = "Root node cannot be removed from the providers, use RemoveProvider(string providerName) instead.";

	// Token: 0x040003F3 RID: 1011
	public const string XmlSiteMapProvider_cannot_add_node = "SiteMapNode {0} cannot be found in current provider, only nodes in the same provider can be added.";

	// Token: 0x040003F4 RID: 1012
	public const string XmlSiteMapProvider_invalid_resource_key = "Resource key {0} is not valid, it must contain a valid class name and key pair. For example, $resources:'className','key'";

	// Token: 0x040003F5 RID: 1013
	public const string XmlSiteMapProvider_resourceKey_cannot_be_empty = "Resource key cannot be empty.";

	// Token: 0x040003F6 RID: 1014
	public const string XmlSiteMapProvider_cannot_find_provider = "Provider {0} cannot be found inside XmlSiteMapProvider {1}.";

	// Token: 0x040003F7 RID: 1015
	public const string XmlSiteMapProvider_cannot_remove_node = "SiteMapNode {0} does not exist in provider {1}, it must be removed from provider {2}.";

	// Token: 0x040003F8 RID: 1016
	public const string XmlSiteMapProvider_missing_siteMapFile = "The {0} attribute must be specified on the XmlSiteMapProvider.";

	// Token: 0x040003F9 RID: 1017
	public const string XmlSiteMapProvider_Description = "SiteMap provider which reads in .sitemap XML files.";

	// Token: 0x040003FA RID: 1018
	public const string XmlSiteMapProvider_Not_Initialized = "XmlSiteMapProvider is not initialized. Call Initialize() method first.";

	// Token: 0x040003FB RID: 1019
	public const string XmlSiteMapProvider_Cannot_Be_Inited_Twice = "XmlSiteMapProvider cannot be initialized twice.";

	// Token: 0x040003FC RID: 1020
	public const string XmlSiteMapProvider_Top_Element_Must_Be_SiteMap = "Top element must be siteMap.";

	// Token: 0x040003FD RID: 1021
	public const string XmlSiteMapProvider_Only_One_SiteMapNode_Required_At_Top = "Exactly one <siteMapNode> element is required directly inside the <siteMap> element.";

	// Token: 0x040003FE RID: 1022
	public const string XmlSiteMapProvider_Only_SiteMapNode_Allowed = "Only <siteMapNode> elements are allowed at this location.";

	// Token: 0x040003FF RID: 1023
	public const string XmlSiteMapProvider_invalid_sitemapnode_returned = "Provider {0} must return a valid sitemap node.";

	// Token: 0x04000400 RID: 1024
	public const string XmlSiteMapProvider_invalid_GetRootNodeCore = "GetRootNode is returning null from Provider {0}, this method must return a non-empty sitemap node.";

	// Token: 0x04000401 RID: 1025
	public const string XmlSiteMapProvider_Error_loading_Config_file = "The XML sitemap config file {0} could not be loaded.  {1}";

	// Token: 0x04000402 RID: 1026
	public const string XmlSiteMapProvider_FileName_does_not_exist = "The file {0} required by XmlSiteMapProvider does not exist.";

	// Token: 0x04000403 RID: 1027
	public const string XmlSiteMapProvider_FileName_already_in_use = "The sitemap config file {0} is already used by other nodes or providers.";

	// Token: 0x04000404 RID: 1028
	public const string XmlSiteMapProvider_Invalid_Extension = "The file {0} has an invalid extension, only .sitemap files are allowed in XmlSiteMapProvider.";

	// Token: 0x04000405 RID: 1029
	public const string XmlSiteMapProvider_multiple_resource_definition = "Cannot have more than one resource binding on attribute '{0}'. Ensure that this attribute is not bound through an implicit expression, for example, {0}=\"$resources:key\".";

	// Token: 0x04000406 RID: 1030
	public const string UrlMappings_only_app_relative_url_allowed = "The URL '{0}' is not valid. Only application relative URLs (~/url) are allowed.";

	// Token: 0x04000407 RID: 1031
	public const string FileName_does_not_exist = "The file '{0}' does not exist.";

	// Token: 0x04000408 RID: 1032
	public const string SiteMapProvider_Multiple_Providers_With_Identical_Name = "Multiple providers with the same name '{0}' were found. SiteMap requires providers have unique names.";

	// Token: 0x04000409 RID: 1033
	public const string XmlSiteMapProvider_Multiple_Nodes_With_Identical_Url = "Multiple nodes with the same URL '{0}' were found. XmlSiteMapProvider requires that sitemap nodes have unique URLs.";

	// Token: 0x0400040A RID: 1034
	public const string XmlSiteMapProvider_Multiple_Nodes_With_Identical_Key = "Multiple nodes with the same key '{0}' were found. XmlSiteMapProvider requires that sitemap nodes have unique keys.";

	// Token: 0x0400040B RID: 1035
	public const string Provider_Not_Found = "Provider '{0}' was not found.";

	// Token: 0x0400040C RID: 1036
	public const string Provider_does_not_support_policy_for_responses = "When using a custom output cache provider like '{0}', only the following expiration policies and cache features are supported:  file dependencies, absolute expirations, static validation callbacks and static substitution callbacks.";

	// Token: 0x0400040D RID: 1037
	public const string Provider_does_not_support_policy_for_fragments = "When using a custom output cache provider like '{0}', only the following expiration policies and cache features are supported:  file dependencies and absolute expirations.";

	// Token: 0x0400040E RID: 1038
	public const string GetOutputCacheProviderName_Invalid = "HttpApplication.GetOutputCacheProviderName returned '{0}', but the provider was not found.";

	// Token: 0x0400040F RID: 1039
	public const string OutputCacheExtensibility_CantSerializeDeserializeType = "The provided parameter is not of a supported type for serialization and/or deserialization.";

	// Token: 0x04000410 RID: 1040
	public const string Collection_readonly = "Collection is read-only.";

	// Token: 0x04000411 RID: 1041
	public const string ParameterCollection_NotParameter = "Object is not a Parameter.";

	// Token: 0x04000412 RID: 1042
	public const string ControlParameter_CouldNotFindControl = "Could not find control '{0}' in ControlParameter '{1}'.";

	// Token: 0x04000413 RID: 1043
	public const string ControlParameter_ControlIDNotSpecified = "A ControlID must be specified in ControlParameter '{0}'.";

	// Token: 0x04000414 RID: 1044
	public const string ControlParameter_PropertyNameNotSpecified = "PropertyName must be set to a valid property name of the control named '{0}' in ControlParameter '{1}'.";

	// Token: 0x04000415 RID: 1045
	public const string DataSourceCache_InvalidExpiryPolicy = "Invalid DataSourceCacheExpiry.";

	// Token: 0x04000416 RID: 1046
	public const string DataSourceCache_InvalidDuration = "The duration must be non-negative.";

	// Token: 0x04000417 RID: 1047
	public const string DataSourceCache_CacheMustBeEnabled = "Cannot perform operation when cache is not enabled.";

	// Token: 0x04000418 RID: 1048
	public const string DataSourceView_NoPaging = "The data source does not support server-side data paging.";

	// Token: 0x04000419 RID: 1049
	public const string DataSourceView_NoSorting = "The data source does not support sorting.";

	// Token: 0x0400041A RID: 1050
	public const string DataSourceView_NoRowCount = "The data source does not support retrieving the number of rows of data.";

	// Token: 0x0400041B RID: 1051
	public const string AccessDataSource_Description = "Connect to an Access database created with Microsoft Office.";

	// Token: 0x0400041C RID: 1052
	public const string AccessDataSource_DisplayName = "Access Database";

	// Token: 0x0400041D RID: 1053
	public const string AccessDataSource_CannotSetConnectionString = "The AccessDataSource ConnectionString property cannot be set, it is automatically generated.";

	// Token: 0x0400041E RID: 1054
	public const string AccessDataSource_CannotSetProvider = "The provider name cannot be set on AccessDataSource '{0}'.";

	// Token: 0x0400041F RID: 1055
	public const string AccessDataSource_SqlCacheDependencyNotSupported = "SQL cache dependencies are not supported on AccessDataSource '{0}'.";

	// Token: 0x04000420 RID: 1056
	public const string AccessDataSource_DesignTimeRelativePathsNotSupported = "Cannot use AccessDataSource '{0}' at design time when the DataFile property is specified using a virtual path.";

	// Token: 0x04000421 RID: 1057
	public const string AccessDataSource_NoPathDiscoveryPermission = "Access to the file '{0}' in AccessDataSource '{1}' was denied because of security settings.";

	// Token: 0x04000422 RID: 1058
	public const string AccessDataSourceView_SelectRequiresDataFile = "To perform the 'Select' operation the DataFile property of the data source '{0}' must be set to a valid Microsoft Access database.";

	// Token: 0x04000423 RID: 1059
	public const string SqlDataSource_Description = "Connect to any SQL database supported by ADO.NET, such as Microsoft SQL Server, Oracle, or OLEDB.";

	// Token: 0x04000424 RID: 1060
	public const string SqlDataSource_DisplayName = "Database";

	// Token: 0x04000425 RID: 1061
	public const string SqlDataSource_InvalidMode = "The SqlDataSourceMode for the DataSourceMode property on data source '{0}' is invalid.";

	// Token: 0x04000426 RID: 1062
	public const string SqlDataSource_SqlCacheDependencyNotSupported = "SQL Cache Dependencies are not supported by the data source '{0}'. If this control is derived from SqlDataSource and has its own cache implementation it must also override the SaveDataToCache() method.";

	// Token: 0x04000427 RID: 1063
	public const string SqlDataSource_NoDbPermission = "Access to the ADO.net Managed Provider '{0}' was denied in the data source with ID '{1}' because of security settings.";

	// Token: 0x04000428 RID: 1064
	public const string SqlDataSourceView_SortNotSupported = "The data source '{0}' only supports sorting when the data source's DataSourceMode is set to DataSet.";

	// Token: 0x04000429 RID: 1065
	public const string SqlDataSourceView_FilterNotSupported = "The data source '{0}' only supports filtering when the data source's DataSourceMode is set to DataSet.";

	// Token: 0x0400042A RID: 1066
	public const string SqlDataSourceView_CacheNotSupported = "The data source '{0}' only supports caching when the data source's DataSourceMode is set to DataSet.";

	// Token: 0x0400042B RID: 1067
	public const string SqlDataSourceView_DeleteNotSupported = "Deleting is not supported by data source '{0}' unless DeleteCommand is specified.";

	// Token: 0x0400042C RID: 1068
	public const string SqlDataSourceView_InsertNotSupported = "Inserting is not supported by data source '{0}' unless InsertCommand is specified.";

	// Token: 0x0400042D RID: 1069
	public const string SqlDataSourceView_UpdateNotSupported = "Updating is not supported by data source '{0}' unless UpdateCommand is specified.";

	// Token: 0x0400042E RID: 1070
	public const string SqlDataSourceView_CouldNotCreateConnection = "The data source '{0}' could not create a database connection. Please check the data source's connection settings.";

	// Token: 0x0400042F RID: 1071
	public const string SqlDataSourceView_NoPaging = "The SqlDataSource '{0}' does not have paging enabled.  Set the DataSourceMode to DataSet to enable paging.";

	// Token: 0x04000430 RID: 1072
	public const string SqlDataSourceView_NoSorting = "The SqlDataSource '{0}' cannot sort.  Set DataSourceMode to DataSet to enable sorting. If you are using a stored procedure that supports sorting, set the SortParameterName property to the name of the stored procedure's parameter that accepts a sort expression.";

	// Token: 0x04000431 RID: 1073
	public const string SqlDataSourceView_NoRowCount = "The SqlDataSource'{0}' cannot retrieve the total row count of the data source.";

	// Token: 0x04000432 RID: 1074
	public const string SqlDataSourceView_CountNotValid = "The SelectCountCommand on SqlDataSource '{0}' did not return a count of the rows in the data source.  Please check the SelectCountCommand.";

	// Token: 0x04000433 RID: 1075
	public const string SqlDataSourceView_SortParameterRequiresStoredProcedure = "The SortParameterName property is only supported with stored procedure commands in SqlDataSource '{0}'.";

	// Token: 0x04000434 RID: 1076
	public const string SqlDataSourceView_CommandNotificationNotSupported = "SQL Server command notification for caching is only supported with the System.Data.SqlClient provider in SqlDataSource '{0}'.";

	// Token: 0x04000435 RID: 1077
	public const string SqlDataSourceView_Pessimistic = "You have specified that your {0} command compares all values on SqlDataSource '{1}', but the dictionary passed in for {2} is empty.  Pass in a valid dictionary for {0} or change your mode to OverwriteChanges.";

	// Token: 0x04000436 RID: 1078
	public const string SqlDataSourceView_MissingParameters = "Error executing '{0}Command' in SqlDataSource '{1}'. Ensure the command accepts the following parameters: {2}";

	// Token: 0x04000437 RID: 1079
	public const string SqlDataSourceView_NoParameters = "No Parameters";

	// Token: 0x04000438 RID: 1080
	public const string DataSourceView_delete = "delete";

	// Token: 0x04000439 RID: 1081
	public const string DataSourceView_update = "update";

	// Token: 0x0400043A RID: 1082
	public const string ModelDataSourceView_CannotCallOpenGenericMethods = "Cannot call the method '{0}' on page '{1}' because the method is a generic method.";

	// Token: 0x0400043B RID: 1083
	public const string ModelDataSourceView_CannotCallMethodsWithOutOrRefParameters = "Cannot call the method '{0}' on page '{1}' because the parameter '{2}' is passed by reference.";

	// Token: 0x0400043C RID: 1084
	public const string ModelDataSourceView_DataMethodNotFound = "A public method with the name '{0}' was either not found or there were multiple methods with the same name on the type '{1}'.";

	// Token: 0x0400043D RID: 1085
	public const string ModelDataSourceView_DeleteNotSupported = "Deleting is not supported unless the DeleteMethod is specified.";

	// Token: 0x0400043E RID: 1086
	public const string ModelDataSourceView_InvalidSelectReturnType = "The Select Method must return one of \"IQueryable<{0}>\" or \"IEnumerable<{0}>\" or \"{0}\" when ItemType is set to \"{0}\".";

	// Token: 0x0400043F RID: 1087
	public const string ModelDataSourceView_InvalidPagingParameters = "When the DataBoundControl has paging enabled, either the SelectMethod should return an IQueryable<ItemType> or should have all these mandatory parameters : int startRowIndex, int maximumRows, out int totalRowCount";

	// Token: 0x04000440 RID: 1088
	public const string ModelDataSourceView_InvalidSortingParameters = "When the DataBoundControl has sorting enabled, either the SelectMethod should return an IQueryable<ItemType> or should have all these mandatory parameters : string sortByExpression";

	// Token: 0x04000441 RID: 1089
	public const string ModelDataSourceView_InsertNotSupported = "Inserting is not supported unless the InsertMethod is specified.";

	// Token: 0x04000442 RID: 1090
	public const string ModelDataSourceView_MultipleModelMethodSources = "The DataMethodsType and DataMethodsObject properties cannot both be specified at the same time.";

	// Token: 0x04000443 RID: 1091
	public const string ModelDataSourceView_MultipleValueProvidersNotSupported = "Only one source for the parameter '{0}' can be specified.";

	// Token: 0x04000444 RID: 1092
	public const string ModelDataSourceView_UpdateNotSupported = "Updating is not supported unless the UpdateMethod is specified.";

	// Token: 0x04000445 RID: 1093
	public const string ModelDataSourceView_SelectNotSupported = "The Select operation is not supported unless the SelectMethod is specified.";

	// Token: 0x04000446 RID: 1094
	public const string ModelDataSourceView_SortNotSupportedOnIEnumerable = "The SelectMethod does not support sorting with IEnumerable data. Automatic sorting is only supported when the SelectMethod returns an IQueryable type.";

	// Token: 0x04000447 RID: 1095
	public const string ModelDataSourceView_ParameterCannotBeNull = "A null value for parameter '{0}' of non-nullable type '{1}' for method '{2}' in '{3}'. An optional parameter must be a reference type or a nullable type.";

	// Token: 0x04000448 RID: 1096
	public const string ModelDataSourceView_ParameterValueHasWrongType = "An invalid value for parameter '{0}' for method '{1}' in '{2}'. The value from model binding is of type '{3}', but the parameter requires a value of type '{4}'.";

	// Token: 0x04000449 RID: 1097
	public const string ObjectDataSource_Description = "Connect to a middle-tier business object or DataSet in the Bin or App_Code directory for the application.";

	// Token: 0x0400044A RID: 1098
	public const string ObjectDataSource_DisplayName = "Object";

	// Token: 0x0400044B RID: 1099
	public const string ObjectDataSourceView_DeleteNotSupported = "Deleting is not supported by ObjectDataSource '{0}' unless the DeleteMethod is specified.";

	// Token: 0x0400044C RID: 1100
	public const string ObjectDataSourceView_InsertNotSupported = "Inserting is not supported by ObjectDataSource '{0}' unless the InsertMethod is specified.";

	// Token: 0x0400044D RID: 1101
	public const string ObjectDataSourceView_UpdateNotSupported = "Updating is not supported by ObjectDataSource '{0}' unless the UpdateMethod is specified.";

	// Token: 0x0400044E RID: 1102
	public const string ObjectDataSourceView_SelectNotSupported = "The Select operation is not supported by ObjectDataSource '{0}' unless the SelectMethod is specified.";

	// Token: 0x0400044F RID: 1103
	public const string ObjectDataSourceView_InsertRequiresValues = "ObjectDataSource '{0}' has no values to insert. Check that the 'values' dictionary contains values.";

	// Token: 0x04000450 RID: 1104
	public const string ObjectDataSourceView_TypeNotSpecified = "A type must be specified in the TypeName property of ObjectDataSource '{0}'.";

	// Token: 0x04000451 RID: 1105
	public const string ObjectDataSourceView_TypeNotFound = "The type specified in the TypeName property of ObjectDataSource '{0}' could not be found.";

	// Token: 0x04000452 RID: 1106
	public const string ObjectDataSourceView_MethodNotFoundNoParams = "ObjectDataSource '{0}' could not find a non-generic method '{1}' that has no parameters.";

	// Token: 0x04000453 RID: 1107
	public const string ObjectDataSourceView_MethodNotFoundWithParams = "ObjectDataSource '{0}' could not find a non-generic method '{1}' that has parameters: {2}.";

	// Token: 0x04000454 RID: 1108
	public const string ObjectDataSourceView_MethodNotFoundForDataObject = "ObjectDataSource '{0}' could not find a non-generic method '{1}' that takes parameters of type '{2}'.";

	// Token: 0x04000455 RID: 1109
	public const string ObjectDataSourceView_DataObjectTypeNotFound = "The data object type specified in the DataObjectTypeName property of ObjectDataSource '{0}' could not be found.";

	// Token: 0x04000456 RID: 1110
	public const string ObjectDataSourceView_DataObjectPropertyNotFound = "Could not find a property named '{0}' on the type specified by the DataObjectTypeName property in ObjectDataSource '{1}'.";

	// Token: 0x04000457 RID: 1111
	public const string ObjectDataSourceView_DataObjectPropertyReadOnly = "The '{0}' property on the type specified by the DataObjectTypeName property in ObjectDataSource '{1}' is readonly and its value cannot be set.";

	// Token: 0x04000458 RID: 1112
	public const string ObjectDataSourceView_MultipleOverloads = "More than one method with the specified name and parameters was found for ObjectDataSource '{0}'. Adding the DataObjectMethodAttribute to one of these methods and/or making it the default method can help resolve overload conflicts.";

	// Token: 0x04000459 RID: 1113
	public const string ObjectDataSourceView_CacheNotSupportedOnSortedDataView = "The data source '{0}' only supports DataView caching when there is no sort expression.";

	// Token: 0x0400045A RID: 1114
	public const string ObjectDataSourceView_CacheNotSupportedOnIDataReader = "The data source '{0}' does not support caching objects that implement IDataReader.";

	// Token: 0x0400045B RID: 1115
	public const string ObjectDataSourceView_SortNotSupportedOnIEnumerable = "The data source '{0}' does not support sorting with IEnumerable data. Automatic sorting is only supported with DataView, DataTable, and DataSet.";

	// Token: 0x0400045C RID: 1116
	public const string ObjectDataSourceView_FilterNotSupported = "The data source '{0}' only supports filtering when the SelectMethod returns a DataSet or a DataTable.";

	// Token: 0x0400045D RID: 1117
	public const string ObjectDataSourceView_Pessimistic = "You have specified that your {0} method compares all values on ObjectDataSource '{1}', but the dictionary passed in for {2} is empty.  Pass in a valid dictionary for {0} or change your mode to OverwriteChanges.";

	// Token: 0x0400045E RID: 1118
	public const string ObjectDataSourceView_NoOldValuesParams = "The Update method on ObjectDataSource '{0}' must have a parameter that fits the OldValuesParameterFormatString.  Check your UpdateMethod or change your ConflictDetection to OverwriteValues.";

	// Token: 0x0400045F RID: 1119
	public const string ObjectDataSourceView_MissingPagingSettings = "In ObjectDataSource '{0}' when EnablePaging is set to true, StartRowIndexParameterName and MaximumRowsParameterName must also be set to valid parameter names of the SelectMethod.";

	// Token: 0x04000460 RID: 1120
	public const string ObjectDataSourceView_CannotConvertType = "Cannot convert value of parameter '{0}' from '{1}' to '{2}'";

	// Token: 0x04000461 RID: 1121
	public const string FilteredDataSetHelper_DataSetHasNoTables = "The DataSet in data source '{0}' does not contain any tables.";

	// Token: 0x04000462 RID: 1122
	public const string StringPropertyBuilder_CannotHaveChildObjects = "The '{0}' property of '{1}' does not allow child objects.";

	// Token: 0x04000463 RID: 1123
	public const string XmlHierarchyData_CouldNotFindNode = "Could not find node index. XmlNode does not exist in parent.";

	// Token: 0x04000464 RID: 1124
	public const string XmlDataSource_Description = "Connect to an XML file.";

	// Token: 0x04000465 RID: 1125
	public const string XmlDataSource_DesignTimeRelativePathsNotSupported = "Cannot use XmlDataSource '{0}' at design time when a file property is specified using a virtual path.";

	// Token: 0x04000466 RID: 1126
	public const string XmlDataSource_DisplayName = "XML File";

	// Token: 0x04000467 RID: 1127
	public const string XmlDataSource_SaveNotAllowed = "Save is not enabled in XmlDataSource '{0}' when either an XSL transform is specified, the content was specified using the Data property, the content was loaded from a URL, or a custom virtual path provider was used.";

	// Token: 0x04000468 RID: 1128
	public const string XmlDataSource_NoWebPermission = "Access to the URL '{0}' was denied in the XmlDataSource with ID '{1}' because of security settings.";

	// Token: 0x04000469 RID: 1129
	public const string XmlDataSource_CannotChangeWhileLoading = "The {0} property of XmlDataSource '{1}' cannot be changed while the document is being loaded.";

	// Token: 0x0400046A RID: 1130
	public const string XmlDataSource_NeedUniqueIDForCache = "When caching is enabled for the XmlDataSource that is not in the page's control tree it requires a UniqueID that is unique throughout the application.";

	// Token: 0x0400046B RID: 1131
	public const string XmlDataSource_CacheKeyContext = "Allows the user to specify a string that will be added to the cache key.";

	// Token: 0x0400046C RID: 1132
	public const string DataControlFieldCell_ShouldNotSetValidateRequestMode = "DataControlFieldCell gets the value of ValidateRequestMode from its ContainingField. The ValidateRequestMode property cannot be set directly on DataControlFieldCell.";

	// Token: 0x0400046D RID: 1133
	public const string NeedHeader = "Using {0} requires Page.Header to be non-null (e.g. <head runat=\"server\" />).";

	// Token: 0x0400046E RID: 1134
	public const string Form_Required_For_Focus = "A form tag with runat=server must exist on the Page to use SetFocus() or the Focus property.";

	// Token: 0x0400046F RID: 1135
	public const string Page_MustCallBeforeAndDuringPreRender = "{0} can only be called before and during PreRender.";

	// Token: 0x04000470 RID: 1136
	public const string RoleGroupCollection_InvalidType = "Argument must be a RoleGroup.";

	// Token: 0x04000471 RID: 1137
	public const string Page_CallBackError = "There was an error in the callback.";

	// Token: 0x04000472 RID: 1138
	public const string Page_CallBackInvalid = "The callback request is invalid.";

	// Token: 0x04000473 RID: 1139
	public const string Page_CallBackTargetInvalid = "The target '{0}' for the callback could not be found or did not implement ICallbackEventHandler.";

	// Token: 0x04000474 RID: 1140
	public const string NoThemingSupport = "Control of type '{0}' does not support theming.";

	// Token: 0x04000475 RID: 1141
	public const string ControlNonVisual = "Control of type '{0}' is a non-visual control and does not support setting the Visible property.";

	// Token: 0x04000476 RID: 1142
	public const string NoFocusSupport = "Control of type '{0}' does not support the Focus operation.";

	// Token: 0x04000477 RID: 1143
	public const string PageStatePersister_PageCannotBeNull = "A PageStatePersister must be constructed with a non-null Page reference.";

	// Token: 0x04000478 RID: 1144
	public const string SessionPageStatePersister_SessionMustBeEnabled = "SessionPageStatePersister can only be used when enableSessionState is set to true, either in a configuration file or in the Page directive.";

	// Token: 0x04000479 RID: 1145
	public const string Page_MissingDataBindingContext = "Databinding methods such as Eval(), XPath(), and Bind() can only be used in the context of a databound control.";

	// Token: 0x0400047A RID: 1146
	public const string TemplateControl_DataBindingRequiresPage = "Databinding methods such as Eval(), XPath(), and Bind() can only be used in controls contained in a page.";

	// Token: 0x0400047B RID: 1147
	public const string LabelForNotFound = "Unable to find control with id '{0}' that is associated with the Label '{1}'.";

	// Token: 0x0400047C RID: 1148
	public const string Attrib_Sql9_not_allowed = "The SqlDependency attribute does not support \"CommandNotification\" in a user control.";

	// Token: 0x0400047D RID: 1149
	public const string FactoryGenerator_TypeNotPublic = "Cannot instantiate type '{0}' because it is not public.";

	// Token: 0x0400047E RID: 1150
	public const string FactoryGenerator_TypeHasNoParameterlessConstructor = "Cannot instantiate type '{0}' because there is no public parameterless constructor.";

	// Token: 0x0400047F RID: 1151
	public const string FactoryInterface = "factoryInterface";

	// Token: 0x04000480 RID: 1152
	public const string InvalidSerializedData = "The serialized data is invalid.";

	// Token: 0x04000481 RID: 1153
	public const string NonSerializableType = "A value of type '{0}' cannot be serialized.";

	// Token: 0x04000482 RID: 1154
	public const string ErrorSerializingValue = "Error serializing value '{0}' of type '{1}.'";

	// Token: 0x04000483 RID: 1155
	public const string Control_ValidateRequestMode = "Determines whether the control validates client input or not. Defaults to inherit from parent.";

	// Token: 0x04000484 RID: 1156
	public const string Control_Controls = "The collection of child controls owned by the control.";

	// Token: 0x04000485 RID: 1157
	public const string Control_ID = "Programmatic name of the control.";

	// Token: 0x04000486 RID: 1158
	public const string Control_MaintainState = "Whether the control automatically saves its state for use in round-trips.";

	// Token: 0x04000487 RID: 1159
	public const string Control_ViewStateMode = "Determines whether the control has viewstate enabled or not, defaults to inherit from parent.";

	// Token: 0x04000488 RID: 1160
	public const string Control_Visible = "Indicates whether the control is visible and rendered.";

	// Token: 0x04000489 RID: 1161
	public const string Control_OnDisposed = "Fires when the control has been disposed.";

	// Token: 0x0400048A RID: 1162
	public const string Control_OnInit = "Fires when the page has been initialized.";

	// Token: 0x0400048B RID: 1163
	public const string Control_OnLoad = "Fires when the page has been loaded.";

	// Token: 0x0400048C RID: 1164
	public const string Control_OnUnload = "Fires when the page is unloaded.";

	// Token: 0x0400048D RID: 1165
	public const string Control_OnPreRender = "Fires before the page is rendered.";

	// Token: 0x0400048E RID: 1166
	public const string Control_OnDataBind = "Fires when the control's data binding expressions are to be evaluated.";

	// Token: 0x0400048F RID: 1167
	public const string Control_NamingContainer = "The containing control or page within which ID is unique.";

	// Token: 0x04000490 RID: 1168
	public const string Control_Page = "The page containing the control.";

	// Token: 0x04000491 RID: 1169
	public const string Control_Parent = "The control containing this control.";

	// Token: 0x04000492 RID: 1170
	public const string Control_TemplateSourceDirectory = "The virtual directory of the Page or UserControl that contains this control.";

	// Token: 0x04000493 RID: 1171
	public const string Control_TemplateControl = "The TemplateControl that hosts this control.";

	// Token: 0x04000494 RID: 1172
	public const string Control_Site = "Site of the control.";

	// Token: 0x04000495 RID: 1173
	public const string Control_State = "Current viewstate of the control.";

	// Token: 0x04000496 RID: 1174
	public const string Control_UniqueID = "The unique ID of the control within the page.";

	// Token: 0x04000497 RID: 1175
	public const string Control_ClientID = "The ID of the control that is rendered for the client.";

	// Token: 0x04000498 RID: 1176
	public const string Control_ClientIDMode = "Indicates how the ClientID should be generated for the control.";

	// Token: 0x04000499 RID: 1177
	public const string Control_SkinId = "The SkinId of the control skin that provides the skin of the control.";

	// Token: 0x0400049A RID: 1178
	public const string Control_EnableTheming = "Indicates whether the control can be themed.";

	// Token: 0x0400049B RID: 1179
	public const string Page_ClientTarget = "Allows you to override automatic detection of browser capabilities and force specific rendering.";

	// Token: 0x0400049C RID: 1180
	public const string Page_ErrorPage = "URL of the associated error page.";

	// Token: 0x0400049D RID: 1181
	public const string Page_ErrorDescription = "Occurs when an uncaught exception is thrown.";

	// Token: 0x0400049E RID: 1182
	public const string Page_OnCommitTransaction = "Occurs when a user initiates a transaction.";

	// Token: 0x0400049F RID: 1183
	public const string Page_OnAbortTransaction = "Occurs when a user aborts a transaction.";

	// Token: 0x040004A0 RID: 1184
	public const string Page_Illegal_MaxPageStateFieldLength = "MaxPageStateFieldLength can only be set to -1[off] or positive numbers.";

	// Token: 0x040004A1 RID: 1185
	public const string Page_Illegal_AsyncTimeout = "AsyncTimeout cannot be negative.";

	// Token: 0x040004A2 RID: 1186
	public const string Page_InvalidUpdateModelAttempt = "'{0}' must be passed a value provider or alternatively must be invoked from inside a data-operation method of a control that uses model binding for data binding.";

	// Token: 0x040004A3 RID: 1187
	public const string Page_UnobtrusiveValidationMode = "Specifies the client side validation mode of the validators on this page.";

	// Token: 0x040004A4 RID: 1188
	public const string Page_UpdateModel_UpdateUnsuccessful = "The model of type '{0}' could not be updated.";

	// Token: 0x040004A5 RID: 1189
	public const string ObjectDataSource_ConflictDetection = "Specifies how data conflicts are resolved.";

	// Token: 0x040004A6 RID: 1190
	public const string ObjectDataSource_ConvertNullToDBNull = "Specifies whether null parameter values passed into methods will be converted to System.DBNull.";

	// Token: 0x040004A7 RID: 1191
	public const string ObjectDataSource_DataObjectTypeName = "Specifies a type that can be constructed for Update, Insert, and Delete operations when the method takes this type rather than having one parameter for each property.";

	// Token: 0x040004A8 RID: 1192
	public const string ObjectDataSource_DeleteMethod = "The method to execute when Delete() is called.";

	// Token: 0x040004A9 RID: 1193
	public const string ObjectDataSource_DeleteParameters = "Collection of parameters used when calling the DeleteMethod. These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004AA RID: 1194
	public const string ObjectDataSource_EnablePaging = "Indicates whether the Select method supports paging.";

	// Token: 0x040004AB RID: 1195
	public const string ObjectDataSource_FilterExpression = "Filter expression used when Select() is called. Filtering is only available when the SelectMethod returns a DataSet or a DataTable.";

	// Token: 0x040004AC RID: 1196
	public const string ObjectDataSource_FilterParameters = "Collection of parameters used in the FilterExpression property. Filtering is only available when the SelectMethod returns a DataSet or a DataTable.";

	// Token: 0x040004AD RID: 1197
	public const string ObjectDataSource_InsertMethod = "The method to execute when Insert() is called.";

	// Token: 0x040004AE RID: 1198
	public const string ObjectDataSource_InsertParameters = "Collection of values used when calling the InsertMethod. These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004AF RID: 1199
	public const string ObjectDataSource_MaximumRowsParameterName = "When EnablePaging is true, this indicates the parameter of the Select method that accepts the value for the number of rows to retrieve.";

	// Token: 0x040004B0 RID: 1200
	public const string ObjectDataSource_SelectCountMethod = "The method to execute when the total row count is needed.";

	// Token: 0x040004B1 RID: 1201
	public const string ObjectDataSource_SelectMethod = "The method to execute when Select() is called.";

	// Token: 0x040004B2 RID: 1202
	public const string ObjectDataSource_SelectParameters = "Collection of parameters used when calling the SelectMethod.";

	// Token: 0x040004B3 RID: 1203
	public const string ObjectDataSource_SortParameterName = "The name of the parameter on the Select Method that accepts a sort expression, if any.";

	// Token: 0x040004B4 RID: 1204
	public const string ObjectDataSource_StartRowIndexParameterName = "When EnablePaging is set to true, this property indicates the parameter of the Select method that accepts the value for the index of first row to retrieve.";

	// Token: 0x040004B5 RID: 1205
	public const string ObjectDataSource_TypeName = "The type that contains the methods specified in this control.";

	// Token: 0x040004B6 RID: 1206
	public const string ObjectDataSource_UpdateMethod = "The method to execute when Update() is called.";

	// Token: 0x040004B7 RID: 1207
	public const string ObjectDataSource_UpdateParameters = "Collection of parameters and values used when calling the UpdateMethod. These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004B8 RID: 1208
	public const string ObjectDataSource_ObjectCreated = "This event is raised after the instance of the object has been created.";

	// Token: 0x040004B9 RID: 1209
	public const string ObjectDataSource_ObjectCreating = "This event is raised before an instance of the object is created to allow creation of a custom instance of the object.";

	// Token: 0x040004BA RID: 1210
	public const string ObjectDataSource_ObjectDisposing = "This event is raised before the instance of the object is disposed.";

	// Token: 0x040004BB RID: 1211
	public const string ObjectDataSource_Selected = "This event is raised one time each after the Select and SelectCount operation have completed.";

	// Token: 0x040004BC RID: 1212
	public const string ObjectDataSource_Selecting = "This event is raised one time each before the Select and SelectCount operations have been executed.";

	// Token: 0x040004BD RID: 1213
	public const string ObjectDataSource_ParsingCulture = "Indicates the Culture used by ObjectDataSource when converting string values to actual types of properties of data object.";

	// Token: 0x040004BE RID: 1214
	public const string DataSourceCache_Duration = "The duration, in seconds, of the expiration. The expiration policy is specified by the ExpirationPolicy property.";

	// Token: 0x040004BF RID: 1215
	public const string DataSourceCache_Enabled = "Whether caching is enabled for this data source.";

	// Token: 0x040004C0 RID: 1216
	public const string DataSourceCache_ExpirationPolicy = "The expiration policy of the cache. The duration for the expiration is specified by the Duration property.";

	// Token: 0x040004C1 RID: 1217
	public const string DataSourceCache_KeyDependency = "Indicates an arbitrary cache key to make this cache entry depend on.";

	// Token: 0x040004C2 RID: 1218
	public const string SqlDataSource_ConflictDetection = "Specifies how data conflicts are resolved.";

	// Token: 0x040004C3 RID: 1219
	public const string SqlDataSource_ConnectionString = "The connection string used to connect to the database. This property is not stored in ViewState.";

	// Token: 0x040004C4 RID: 1220
	public const string SqlDataSource_CancelSelectOnNullParameter = "Indicates whether the Select operation will be cancelled if the value of any of the SelectParameters is null.";

	// Token: 0x040004C5 RID: 1221
	public const string SqlDataSource_ProviderName = "The ADO.net managed provider name used to connect to the database. This property is not stored in ViewState.";

	// Token: 0x040004C6 RID: 1222
	public const string SqlDataSource_DataSourceMode = "Specifies the SqlDataSourceMode used for selecting rows.";

	// Token: 0x040004C7 RID: 1223
	public const string SqlDataSource_DeleteCommand = "The command to execute for deleting rows.";

	// Token: 0x040004C8 RID: 1224
	public const string SqlDataSource_DeleteCommandType = "The type of the delete command.";

	// Token: 0x040004C9 RID: 1225
	public const string SqlDataSource_DeleteParameters = "Collection of parameters used in Delete(). These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004CA RID: 1226
	public const string SqlDataSource_FilterExpression = "Filter expression used when Select() is called. Filtering is only available when the DataSourceMode is set to DataSet.";

	// Token: 0x040004CB RID: 1227
	public const string SqlDataSource_FilterParameters = "Collection of parameters used in the FilterExpression property. Filtering is only available when the DataSourceMode is set to DataSet.";

	// Token: 0x040004CC RID: 1228
	public const string SqlDataSource_InsertCommand = "The command to execute for inserting new rows.";

	// Token: 0x040004CD RID: 1229
	public const string SqlDataSource_InsertCommandType = "The type of the insert command.";

	// Token: 0x040004CE RID: 1230
	public const string SqlDataSource_InsertParameters = "Collection of values used in Insert(). These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004CF RID: 1231
	public const string SqlDataSource_SelectCommand = "The command to execute for selecting rows.";

	// Token: 0x040004D0 RID: 1232
	public const string SqlDataSource_SelectCommandType = "The type of the select command.";

	// Token: 0x040004D1 RID: 1233
	public const string SqlDataSource_SelectParameters = "Collection of parameters used for selecting rows.";

	// Token: 0x040004D2 RID: 1234
	public const string SqlDataSource_SortParameterName = "The name of the parameter on the Select Command that accepts a sort expression, if any. This is only supported when using a stored procedure.";

	// Token: 0x040004D3 RID: 1235
	public const string SqlDataSource_UpdateCommand = "The command to execute for updating rows.";

	// Token: 0x040004D4 RID: 1236
	public const string SqlDataSource_UpdateCommandType = "The type of the update command.";

	// Token: 0x040004D5 RID: 1237
	public const string SqlDataSource_UpdateParameters = "Collection of parameters and values used in Update(). These parameters are merged with the parameters provided by data-bound controls.";

	// Token: 0x040004D6 RID: 1238
	public const string SqlDataSource_Selected = "This event is raised after the Select operation has completed.";

	// Token: 0x040004D7 RID: 1239
	public const string SqlDataSource_Selecting = "This event is raised before the Select operation has been executed.";

	// Token: 0x040004D8 RID: 1240
	public const string SqlDataSourceCache_SqlCacheDependency = "A semi-colon delimited string indicating which databases to use for the dependency in the format \"database1:table1;database2:table2\".";

	// Token: 0x040004D9 RID: 1241
	public const string Parameter_DbType = "The database type of the parameter. If this property is set to DbType.Object, the Type property will be used instead.";

	// Token: 0x040004DA RID: 1242
	public const string Parameter_DefaultValue = "The default value to use if the value of the parameter is null.";

	// Token: 0x040004DB RID: 1243
	public const string Parameter_Direction = "The direction of the parameter.";

	// Token: 0x040004DC RID: 1244
	public const string Parameter_Name = "The name of the parameter.";

	// Token: 0x040004DD RID: 1245
	public const string Parameter_Size = "The maximum size of the parameter.";

	// Token: 0x040004DE RID: 1246
	public const string Parameter_ConvertEmptyStringToNull = "Whether an empty string should be treated as a null value. If this property is set to true and the value is an empty string, the default value will be used.";

	// Token: 0x040004DF RID: 1247
	public const string Parameter_Type = "The type of the parameter.";

	// Token: 0x040004E0 RID: 1248
	public const string Parameter_TypeNotSupported = "The Type property of parameter '{0}' cannot be set when the DbType property is set.";

	// Token: 0x040004E1 RID: 1249
	public const string Parameter_ValidateInput = "Determines whether the parameter's value is being validated or not.";

	// Token: 0x040004E2 RID: 1250
	public const string ControlParameter_ControlID = "The ID of the control to get the property value from.";

	// Token: 0x040004E3 RID: 1251
	public const string ControlParameter_PropertyName = "A property name indicating the property from which to get the value. If none is specified, the ControlValueProperty attribute of the control will be examined to determine the value.";

	// Token: 0x040004E4 RID: 1252
	public const string CookieParameter_CookieName = "The name of the cookie to get the value from.";

	// Token: 0x040004E5 RID: 1253
	public const string QueryStringParameter_QueryStringField = "The name of the query string field to get the value from.";

	// Token: 0x040004E6 RID: 1254
	public const string FormParameter_FormField = "The name of the form field to get the value from.";

	// Token: 0x040004E7 RID: 1255
	public const string SessionParameter_SessionField = "The name of the session field to get the value from.";

	// Token: 0x040004E8 RID: 1256
	public const string ProfileParameter_PropertyName = "The profile property to get the value from.";

	// Token: 0x040004E9 RID: 1257
	public const string HtmlInputHidden_OnServerChange = "Fires when the value of the control changes.";

	// Token: 0x040004EA RID: 1258
	public const string HtmlInputImage_OnServerClick = "Fires when the image is clicked.";

	// Token: 0x040004EB RID: 1259
	public const string HtmlInputText_ServerChange = "Fires when the text within the control changes.";

	// Token: 0x040004EC RID: 1260
	public const string HtmlSelect_DataTextField = "The field in the data source that provides the item text.";

	// Token: 0x040004ED RID: 1261
	public const string HtmlSelect_DataValueField = "The field in the data source that provides the item value.";

	// Token: 0x040004EE RID: 1262
	public const string HtmlSelect_OnServerChange = "Fires when the selection changes.";

	// Token: 0x040004EF RID: 1263
	public const string HtmlSelect_DataMember = "The data member of the select.";

	// Token: 0x040004F0 RID: 1264
	public const string HtmlTextArea_OnServerChange = "Fires when the text within the control changes.";

	// Token: 0x040004F1 RID: 1265
	public const string AccessDataSource_DataFile = "The name of a Microsoft Office Access database file.";

	// Token: 0x040004F2 RID: 1266
	public const string AdRotator_AdvertisementFile = "XML file containing advertisements.";

	// Token: 0x040004F3 RID: 1267
	public const string AdRotator_AlternateTextField = "The element name that specifies which alternate text to retrieve.";

	// Token: 0x040004F4 RID: 1268
	public const string AdRotator_ImageUrlField = "The element name that specifies which image URL to retrieve.";

	// Token: 0x040004F5 RID: 1269
	public const string AdRotator_KeywordFilter = "Keyword for limiting selection of advertisements.";

	// Token: 0x040004F6 RID: 1270
	public const string AdRotator_NavigateUrlField = "The element name that specifies which advertisement Web page URL to retrieve.";

	// Token: 0x040004F7 RID: 1271
	public const string AdRotator_Target = "The target frame for the NavigateUrl of the advertisement.";

	// Token: 0x040004F8 RID: 1272
	public const string AdRotator_OnAdCreated = "Fired after an advertisement is retrieved from the AdvertisementFile.";

	// Token: 0x040004F9 RID: 1273
	public const string AssemblyResourceLoader_HandlerNotRegistered = "The WebResource.axd handler must be registered in the configuration to process this request.\r\n\r\n<!-- Web.Config Configuration File -->\r\n\r\n<configuration>\r\n    <system.web>\r\n        <httpHandlers>\r\n            <add path=\"WebResource.axd\" verb=\"GET\" type=\"System.Web.Handlers.AssemblyResourceLoader\" validate=\"True\" />\r\n        </httpHandlers>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x040004FA RID: 1274
	public const string AssemblyResourceLoader_InvalidRequest = "This is an invalid webresource request.";

	// Token: 0x040004FB RID: 1275
	public const string AssemblyResourceLoader_AssemblyNotFound = "Assembly {0} not found.";

	// Token: 0x040004FC RID: 1276
	public const string AssemblyResourceLoader_ResourceNotFound = "Resource {0} not found in assembly.";

	// Token: 0x040004FD RID: 1277
	public const string AssemblyResourceLoader_NoCircularReferences = "The resource '{0}' cannot contain a reference to itself.";

	// Token: 0x040004FE RID: 1278
	public const string DataControls_ShowFooter = "Whether to the show the control's footer.";

	// Token: 0x040004FF RID: 1279
	public const string DataControls_ShowHeader = "Whether to the show the control's header.";

	// Token: 0x04000500 RID: 1280
	public const string DataControls_AutoGenerateColumns = "Whether the columns are generated automatically at runtime based on the associated data source.";

	// Token: 0x04000501 RID: 1281
	public const string Button_CausesValidation = "Whether the button causes validation to fire.";

	// Token: 0x04000502 RID: 1282
	public const string WebControl_RepeatLayout = "Whether items are repeated in a table or in-flow.";

	// Token: 0x04000503 RID: 1283
	public const string DataSource_Updating = "This event is raised before the Update operation has been executed.";

	// Token: 0x04000504 RID: 1284
	public const string DataSource_Inserting = "This event is raised before the Insert operation has been executed.";

	// Token: 0x04000505 RID: 1285
	public const string DataSource_Deleting = "This event is raised before the Delete operation has been executed.";

	// Token: 0x04000506 RID: 1286
	public const string DataSource_Updated = "This event is raised after the Update operation has completed.";

	// Token: 0x04000507 RID: 1287
	public const string DataSource_Inserted = "This event is raised after the Insert operation has completed.";

	// Token: 0x04000508 RID: 1288
	public const string DataSource_Deleted = "This event is raised after the Delete operation has completed.";

	// Token: 0x04000509 RID: 1289
	public const string TableItem_VerticalAlign = "The vertical alignment of the cell content.";

	// Token: 0x0400050A RID: 1290
	public const string Button_PostBackUrl = "The URL to post to when the button is clicked.";

	// Token: 0x0400050B RID: 1291
	public const string LoginControls_DefaultRequiredFieldValidatorText = "*";

	// Token: 0x0400050C RID: 1292
	public const string LoginControls_SuccessPageUrl = "The URL that the user is directed to after the action has succeeded.";

	// Token: 0x0400050D RID: 1293
	public const string LoginControls_EditProfileIconUrl = "The URL of the icon for the edit profile page";

	// Token: 0x0400050E RID: 1294
	public const string LoginControls_HelpPageIconUrl = "The URL of the icon for the help page.";

	// Token: 0x0400050F RID: 1295
	public const string LoginControls_HelpPageUrl = "The URL of the help page.";

	// Token: 0x04000510 RID: 1296
	public const string ChangePassword_ChangePasswordButtonImageUrl = "The URL of an image to be displayed for the change password button.";

	// Token: 0x04000511 RID: 1297
	public const string ChangePassword_ContinueButtonImageUrl = "The URL of an image to be displayed for the continue button.";

	// Token: 0x04000512 RID: 1298
	public const string PagerSettings_PreviousPageText = "The text to be used on the previous page button.";

	// Token: 0x04000513 RID: 1299
	public const string PagerSettings_NextPageText = "The text to be used on the next page button.";

	// Token: 0x04000514 RID: 1300
	public const string ChangePassword_UserNameRequiredErrorMessage = "The text to be shown in the validation summary when the user name is empty.";

	// Token: 0x04000515 RID: 1301
	public const string ChangePassword_ConfirmPasswordCompareErrorMessage = "The text to be shown in the validation summary when the password and confirm password do not match.";

	// Token: 0x04000516 RID: 1302
	public const string LoginControls_ConfirmPasswordRequiredErrorMessage = "The text to be shown in the validation summary when the confirm password is empty.";

	// Token: 0x04000517 RID: 1303
	public const string LoginControls_AnswerRequiredErrorMessage = "The text to be shown in the validation summary when the answer is empty.";

	// Token: 0x04000518 RID: 1304
	public const string LoginControls_TitleText = "The text to be shown for the title.";

	// Token: 0x04000519 RID: 1305
	public const string ChangePassword_PasswordRecoveryText = "The text to be shown for the password recovery link.";

	// Token: 0x0400051A RID: 1306
	public const string ChangePassword_ChangePasswordButtonText = "The text to be shown for the change password button.";

	// Token: 0x0400051B RID: 1307
	public const string ChangePassword_HelpPageText = "The text to be shown for the help link.";

	// Token: 0x0400051C RID: 1308
	public const string ChangePassword_CreateUserText = "The text to be shown for the create user link.";

	// Token: 0x0400051D RID: 1309
	public const string ChangePassword_SuccessText = "The text to be shown after the password has been changed.";

	// Token: 0x0400051E RID: 1310
	public const string LoginControls_UserNameLabelText = "The text that identifies the user name textbox.";

	// Token: 0x0400051F RID: 1311
	public const string WebControl_SkipLinkText = "The text that appears in the ALT attribute of the invisible image link that allows screen readers to skip repetitive content.";

	// Token: 0x04000520 RID: 1312
	public const string View_HeaderText = "The text shown in the header if no HeaderTemplate is defined.";

	// Token: 0x04000521 RID: 1313
	public const string View_FooterText = "The text shown in the footer if no FooterTemplate is defined.";

	// Token: 0x04000522 RID: 1314
	public const string View_EmptyDataText = "The text shown in the empty data row if no EmptyDataTemplate is defined.";

	// Token: 0x04000523 RID: 1315
	public const string BoundField_NullDisplayText = "The text displayed if the data bound to the field is null.";

	// Token: 0x04000524 RID: 1316
	public const string View_PagerTemplate = "The template used for the pager.";

	// Token: 0x04000525 RID: 1317
	public const string WebControl_HeaderTemplate = "The template used for the header.";

	// Token: 0x04000526 RID: 1318
	public const string View_EmptyDataTemplate = "The template used for the control when no items are returned from the data source.";

	// Token: 0x04000527 RID: 1319
	public const string LoginControls_TitleTextStyle = "The style of the title.";

	// Token: 0x04000528 RID: 1320
	public const string LoginControls_TextBoxStyle = "The style of the textboxes.";

	// Token: 0x04000529 RID: 1321
	public const string LoginControls_LabelStyle = "The style of the textbox labels.";

	// Token: 0x0400052A RID: 1322
	public const string WebControl_InstructionTextStyle = "The style of the instructions.";

	// Token: 0x0400052B RID: 1323
	public const string WebControl_HyperLinkStyle = "The style of the hyperlinks.";

	// Token: 0x0400052C RID: 1324
	public const string WebControl_FailureTextStyle = "The style of the failure text.";

	// Token: 0x0400052D RID: 1325
	public const string View_EmptyDataRowStyle = "The style applied to the row that contains the EmptyDataTemplate.";

	// Token: 0x0400052E RID: 1326
	public const string WebControl_HeaderStyle = "The style applied to the header.";

	// Token: 0x0400052F RID: 1327
	public const string View_RowStyle = "The style applied to rows.";

	// Token: 0x04000530 RID: 1328
	public const string View_InsertRowStyle = "The style applied to rows when the control is in insert mode.";

	// Token: 0x04000531 RID: 1329
	public const string View_EditRowStyle = "The style applied to rows when the control is in edit mode.";

	// Token: 0x04000532 RID: 1330
	public const string DataControls_Columns = "The set of columns to be shown in the control.";

	// Token: 0x04000533 RID: 1331
	public const string HotSpot_Target = "The name of the window/frame into which the navigation URL should be opened.";

	// Token: 0x04000534 RID: 1332
	public const string MembershipProvider_Name = "The name of the membership provider.";

	// Token: 0x04000535 RID: 1333
	public const string View_DefaultMode = "The mode that the control will begin in, and will revert to after a Cancel, Insert, or Update command.";

	// Token: 0x04000536 RID: 1334
	public const string LoginControls_TextLayout = "The layout of the labels in relation to the textboxes.";

	// Token: 0x04000537 RID: 1335
	public const string UserName_InitialValue = "The initial value in the user name textbox.";

	// Token: 0x04000538 RID: 1336
	public const string WebControl_SelectedIndex = "The index of the currently selected item.";

	// Token: 0x04000539 RID: 1337
	public const string View_DataSourceReturnedNullView = "The IDataSource that is the data source for DetailsView '{0}' returned a null view.  Check that the DataMember property value of DetailsView is valid.";

	// Token: 0x0400053A RID: 1338
	public const string WebControl_HorizontalAlign = "The horizontal alignment of the control.";

	// Token: 0x0400053B RID: 1339
	public const string TableItem_HorizontalAlign = "The horizontal alignment of the cell content.";

	// Token: 0x0400053C RID: 1340
	public const string DataSource_OldValuesParameterFormatString = "The format string applied to the parameter names of the old values passed in an update command when old data values are being compared for conflicts. For example, \"Original_{0}\". To remove the prefix, specify \"{0}\" for the value of this property.";

	// Token: 0x0400053D RID: 1341
	public const string Binding_DataMember = "The element or table name that contains the attributes or columns specified by TextField and ValueField.";

	// Token: 0x0400053E RID: 1342
	public const string Item_RepeatDirection = "The direction in which items are laid out.";

	// Token: 0x0400053F RID: 1343
	public const string DataControls_Caption = "The descriptive caption associated with the control.";

	// Token: 0x04000540 RID: 1344
	public const string DataSource_InvalidViewName = "The data source '{0}' only supports a single view named '{1}'. You may also leave the view name (also called a data member) empty for the default view to be chosen.";

	// Token: 0x04000541 RID: 1345
	public const string WebControl_CommandName = "The command associated with the button.";

	// Token: 0x04000542 RID: 1346
	public const string WebControl_CommandArgument = "The command argument associated with the button.";

	// Token: 0x04000543 RID: 1347
	public const string WebControl_BackImageUrl = "The background image within the control.";

	// Token: 0x04000544 RID: 1348
	public const string WebControl_TextAlign = "The alignment of the text label with respect to each item.";

	// Token: 0x04000545 RID: 1349
	public const string WebControl_CaptionAlign = "The alignment of the associated caption.";

	// Token: 0x04000546 RID: 1350
	public const string WebControl_InstructionText = "Text that is displayed to give instructions.";

	// Token: 0x04000547 RID: 1351
	public const string DataControls_HeaderStyle = "Style applied to the header.";

	// Token: 0x04000548 RID: 1352
	public const string DataControls_FooterStyle = "Style applied to the footer.";

	// Token: 0x04000549 RID: 1353
	public const string HotSpot_HotSpotMode = "Specifies whether the image map causes postback or navigation behavior.";

	// Token: 0x0400054A RID: 1354
	public const string DataControls_GridLines = "Settings for grid lines between cells.";

	// Token: 0x0400054B RID: 1355
	public const string Password_InvalidPasswordErrorMessage = "Please enter a different password.";

	// Token: 0x0400054C RID: 1356
	public const string Table_UseAccessibleHeader = "Indicates that the control should use accessible header cells in its containing table control.";

	// Token: 0x0400054D RID: 1357
	public const string HtmlControl_OnServerClick = "Fires when the control is clicked.";

	// Token: 0x0400054E RID: 1358
	public const string Button_OnCommand = "Fires when the button is clicked and an associated command is defined.";

	// Token: 0x0400054F RID: 1359
	public const string Control_OnServerCheckChanged = "Fires when the checked state of the control changes.";

	// Token: 0x04000550 RID: 1360
	public const string DataControls_OnItemUpdated = "Fires after an Update Command is executed on the data source.";

	// Token: 0x04000551 RID: 1361
	public const string DataControls_OnItemDeleting = "Fires before a Delete Command is executed on the data source.";

	// Token: 0x04000552 RID: 1362
	public const string DataControls_OnItemInserting = "Fires before an Insert Command is executed on the data source.";

	// Token: 0x04000553 RID: 1363
	public const string DataControls_OnItemUpdating = "Fires before an Update Command is executed on the data source.";

	// Token: 0x04000554 RID: 1364
	public const string DataControls_OnItemCreated = "Fires when an item is created.";

	// Token: 0x04000555 RID: 1365
	public const string DataControls_OnItemDataBound = "Fires after an item has been databound.";

	// Token: 0x04000556 RID: 1366
	public const string DataControls_OnItemDeleted = "Fires after a Delete Command is executed on the data source.";

	// Token: 0x04000557 RID: 1367
	public const string DataControls_OnItemInserted = "Fires after an Insert Command is executed on the data source.";

	// Token: 0x04000558 RID: 1368
	public const string DataControls_DataKeyNames = "A comma-separated list of key fields in the data source.";

	// Token: 0x04000559 RID: 1369
	public const string DataControls_DataSourceMustBeCollectionWhenNotDataBinding = "Data source must implement ICollection when calling CreateChildControls with dataBinding=false.";

	// Token: 0x0400055A RID: 1370
	public const string DataControls_OnRowDeleted = "Fires after a Delete Command is executed on the data source.";

	// Token: 0x0400055B RID: 1371
	public const string DataSource_Filtering = "This event is raised before the filter expression is applied to the data.";

	// Token: 0x0400055C RID: 1372
	public const string WebControl_PagerStyle = "Controls the paging UI style associated with the control.";

	// Token: 0x0400055D RID: 1373
	public const string WebControl_CantFindProvider = "Could not find the specified membership provider.";

	// Token: 0x0400055E RID: 1374
	public const string BaseDataList_CellPadding = "The padding within cells.";

	// Token: 0x0400055F RID: 1375
	public const string BaseDataList_CellSpacing = "The spacing between cells.";

	// Token: 0x04000560 RID: 1376
	public const string BaseDataList_DataKeyField = "The field in the data source used to populate the DataKeys collection.";

	// Token: 0x04000561 RID: 1377
	public const string BaseDataList_DataKeys = "The collection of data keys.";

	// Token: 0x04000562 RID: 1378
	public const string BaseDataList_DataMember = "The table or view used for binding when a DataSet is used as a data source.";

	// Token: 0x04000563 RID: 1379
	public const string BaseDataList_OnSelectedIndexChanged = "Fires when the current selection changes.";

	// Token: 0x04000564 RID: 1380
	public const string BaseValidator_ControlToValidate = "ID of the control to validate.";

	// Token: 0x04000565 RID: 1381
	public const string BaseValidator_ErrorMessage = "Message to display in a ValidationSummary when the validated control is invalid.";

	// Token: 0x04000566 RID: 1382
	public const string BaseValidator_IsValid = "Indicates whether the validated control is in error.";

	// Token: 0x04000567 RID: 1383
	public const string BaseValidator_Display = "How the validator is displayed.";

	// Token: 0x04000568 RID: 1384
	public const string BaseValidator_EnableClientScript = "Indicates whether to perform validation on the client in up-level browsers.";

	// Token: 0x04000569 RID: 1385
	public const string BaseValidator_SetFocusOnError = "Whether the validator sets focus on the control when invalid.";

	// Token: 0x0400056A RID: 1386
	public const string BaseValidator_Text = "Text to display for the validator when the validated control is invalid.";

	// Token: 0x0400056B RID: 1387
	public const string BaseValidator_ValidationGroup = "The group to which the validator belongs.";

	// Token: 0x0400056C RID: 1388
	public const string BaseCompareValidator_CultureInvariantValues = "Whether we should do culture invariant conversion against the string value properties on the validator.";

	// Token: 0x0400056D RID: 1389
	public const string BoundColumn_DataField = "The field to which this column is bound.";

	// Token: 0x0400056E RID: 1390
	public const string BoundColumn_DataFormatString = "The formatting that is applied to the bound value. For example, \"{0:d}\" or \"{0:c}\".";

	// Token: 0x0400056F RID: 1391
	public const string BoundColumn_ReadOnly = "Whether the column does not permit editing of its bound field.";

	// Token: 0x04000570 RID: 1392
	public const string BoundField_ApplyFormatInEditMode = "Whether the data should be shown with the DataFormatString formatting applied when in edit mode.  If set to true, the data may have to be unformatted before it is updated in the data source.";

	// Token: 0x04000571 RID: 1393
	public const string BoundField_DataField = "The field to which this field is bound.";

	// Token: 0x04000572 RID: 1394
	public const string BoundField_DataFormatString = "The formatting that is applied to the bound value. For example, \"{0:d}\" or \"{0:c}\".";

	// Token: 0x04000573 RID: 1395
	public const string BoundField_HtmlEncode = "Whether the field is HTML encoded when displayed to the user.";

	// Token: 0x04000574 RID: 1396
	public const string BoundField_ReadOnly = "Whether the field does not permit editing of its bound field.";

	// Token: 0x04000575 RID: 1397
	public const string BoundField_ConvertEmptyStringToNull = "Whether the field treats empty strings as null when the value is extracted from the field.";

	// Token: 0x04000576 RID: 1398
	public const string BulletedList_BulletedListDisplayMode = "The display mode of the bulleted list.";

	// Token: 0x04000577 RID: 1399
	public const string BulletedList_BulletImageUrl = "The URL of an image to use as bullets.";

	// Token: 0x04000578 RID: 1400
	public const string BulletedList_BulletStyle = "The Style used for the bullets.";

	// Token: 0x04000579 RID: 1401
	public const string BulletedList_FirstBulletNumber = "The value at which an ordered list begins numbering.";

	// Token: 0x0400057A RID: 1402
	public const string BulletedList_Target = "The target frame for the URL.";

	// Token: 0x0400057B RID: 1403
	public const string BulletedList_OnClick = "Fires when any linkbutton in list is clicked.";

	// Token: 0x0400057C RID: 1404
	public const string Button_OnClientClick = "The client-side script that is executed on a client-side OnClick.";

	// Token: 0x0400057D RID: 1405
	public const string ButtonColumn_ButtonType = "The type of button contained within the column.";

	// Token: 0x0400057E RID: 1406
	public const string ButtonColumn_CausesValidation = "Whether pressing the button will cause validation to occur.";

	// Token: 0x0400057F RID: 1407
	public const string ButtonColumn_DataTextField = "The field bound to the text property of the button.";

	// Token: 0x04000580 RID: 1408
	public const string ButtonColumn_DataTextFormatString = "The formatting applied to the value bound to the Text property. For example, \"select: {0}\".";

	// Token: 0x04000581 RID: 1409
	public const string ButtonColumn_Text = "The text used for the button.";

	// Token: 0x04000582 RID: 1410
	public const string ButtonColumn_ValidationGroup = "The name of the validation group for which this button should cause validation.";

	// Token: 0x04000583 RID: 1411
	public const string Button_Text = "The text to be shown on the button.";

	// Token: 0x04000584 RID: 1412
	public const string Button_OnClick = "Fires when the button is clicked.";

	// Token: 0x04000585 RID: 1413
	public const string Button_UseSubmitBehavior = "Indicates whether the button render as a submit button.";

	// Token: 0x04000586 RID: 1414
	public const string CheckBox_AutoPostBack = "Automatically posts back to the server when the control is clicked.";

	// Token: 0x04000587 RID: 1415
	public const string CheckBox_Checked = "The checked state of the control.";

	// Token: 0x04000588 RID: 1416
	public const string CheckBox_InputAttributes = "Attributes to be rendered on the HTML input element.";

	// Token: 0x04000589 RID: 1417
	public const string CheckBox_LabelAttributes = "Attributes to be rendered on the HTML span element associated with the checkbox text.";

	// Token: 0x0400058A RID: 1418
	public const string CheckBox_Text = "The text label shown with the check box.";

	// Token: 0x0400058B RID: 1419
	public const string CheckBoxField_Text = "The Text property of the CheckBox in this field.";

	// Token: 0x0400058C RID: 1420
	public const string CheckBoxList_CellPadding = "The padding between each item.";

	// Token: 0x0400058D RID: 1421
	public const string CheckBoxList_CellSpacing = "The spacing between each item.";

	// Token: 0x0400058E RID: 1422
	public const string CheckBoxList_RepeatColumns = "The number of columns used to lay out the items.";

	// Token: 0x0400058F RID: 1423
	public const string CircleHotSpot_X = "The x coordinate of the circle center.";

	// Token: 0x04000590 RID: 1424
	public const string CircleHotSpot_Y = "The y coordinate of the circle center.";

	// Token: 0x04000591 RID: 1425
	public const string CircleHotSpot_Radius = "The radius of the circular hot spot.";

	// Token: 0x04000592 RID: 1426
	public const string CommandField_DefaultCancelCaption = "Cancel";

	// Token: 0x04000593 RID: 1427
	public const string CommandField_DefaultDeleteCaption = "Delete";

	// Token: 0x04000594 RID: 1428
	public const string CommandField_DefaultEditCaption = "Edit";

	// Token: 0x04000595 RID: 1429
	public const string CommandField_DefaultInsertCaption = "Insert";

	// Token: 0x04000596 RID: 1430
	public const string CommandField_DefaultNewCaption = "New";

	// Token: 0x04000597 RID: 1431
	public const string CommandField_DefaultSelectCaption = "Select";

	// Token: 0x04000598 RID: 1432
	public const string CommandField_DefaultUpdateCaption = "Update";

	// Token: 0x04000599 RID: 1433
	public const string CommandField_CancelImageUrl = "The URL of the image to be displayed as the cancel button.";

	// Token: 0x0400059A RID: 1434
	public const string CommandField_DeleteImageUrl = "The URL of the image to be displayed as the delete button.";

	// Token: 0x0400059B RID: 1435
	public const string CommandField_EditImageUrl = "The URL of the image to be displayed as the edit button.";

	// Token: 0x0400059C RID: 1436
	public const string CommandField_InsertImageUrl = "The URL of the image to be displayed as the insert button.";

	// Token: 0x0400059D RID: 1437
	public const string CommandField_NewImageUrl = "The URL of the image to be displayed as the new button.";

	// Token: 0x0400059E RID: 1438
	public const string CommandField_SelectImageUrl = "The URL of the image to be displayed as the select button.";

	// Token: 0x0400059F RID: 1439
	public const string CommandField_UpdateImageUrl = "The URL of the image to be displayed as the update button.";

	// Token: 0x040005A0 RID: 1440
	public const string CommandField_ShowDeleteButton = "Whether the field should display a delete button to the user.";

	// Token: 0x040005A1 RID: 1441
	public const string CommandField_ShowCancelButton = "Whether the field should display a cancel button to the user.";

	// Token: 0x040005A2 RID: 1442
	public const string CommandField_ShowInsertButton = "Whether the field should display an insert button to the user.";

	// Token: 0x040005A3 RID: 1443
	public const string CommandField_ShowEditButton = "Whether the field should display an edit button to the user.";

	// Token: 0x040005A4 RID: 1444
	public const string CommandField_ShowSelectButton = "Whether the field should display a select button to the user.";

	// Token: 0x040005A5 RID: 1445
	public const string CommandField_CancelText = "The text to be displayed on the cancel button.";

	// Token: 0x040005A6 RID: 1446
	public const string CommandField_DeleteText = "The text to be displayed on the delete button.";

	// Token: 0x040005A7 RID: 1447
	public const string CommandField_EditText = "The text to be displayed on the edit button.";

	// Token: 0x040005A8 RID: 1448
	public const string CommandField_InsertText = "The text to be displayed on the insert button.";

	// Token: 0x040005A9 RID: 1449
	public const string CommandField_NewText = "The text to be displayed on the new button.";

	// Token: 0x040005AA RID: 1450
	public const string CommandField_SelectText = "The text to be displayed on the select button.";

	// Token: 0x040005AB RID: 1451
	public const string CommandField_UpdateText = "The text to be displayed on the update button.";

	// Token: 0x040005AC RID: 1452
	public const string ButtonFieldBase_ButtonType = "The type of the button to be rendered in the field.  The values are Link, Button, and Image.";

	// Token: 0x040005AD RID: 1453
	public const string ButtonFieldBase_CausesValidation = "Whether pressing the button will cause validation to occur.";

	// Token: 0x040005AE RID: 1454
	public const string ButtonFieldBase_ValidationGroup = "The name of the validation group for which this button should cause validation.";

	// Token: 0x040005AF RID: 1455
	public const string ButtonField_DataTextField = "The field bound to the text property of the button.";

	// Token: 0x040005B0 RID: 1456
	public const string ButtonField_DataTextFormatString = "The formatting applied to the value bound to the Text property. For example, \"select: {0}\".";

	// Token: 0x040005B1 RID: 1457
	public const string ButtonField_ImageUrl = "The URL of the image if the ButtonType is Image.";

	// Token: 0x040005B2 RID: 1458
	public const string ButtonField_Text = "The text used for the button.";

	// Token: 0x040005B3 RID: 1459
	public const string ChangePassword_CancelButtonType = "The type of the cancel button.";

	// Token: 0x040005B4 RID: 1460
	public const string ChangePassword_ContinueButtonType = "The type of the continue button.";

	// Token: 0x040005B5 RID: 1461
	public const string ChangePassword_ChangePasswordButtonType = "The type of the change password button.";

	// Token: 0x040005B6 RID: 1462
	public const string ChangePassword_CancelButtonImageUrl = "The URL of an image to be displayed for the cancel button.";

	// Token: 0x040005B7 RID: 1463
	public const string ChangePassword_CancelButtonText = "The text of the cancel button.";

	// Token: 0x040005B8 RID: 1464
	public const string ChangePassword_CancelButtonStyle = "The style of the cancel button.";

	// Token: 0x040005B9 RID: 1465
	public const string ChangePassword_CancelButtonClick = "Raised when the cancel button is clicked";

	// Token: 0x040005BA RID: 1466
	public const string ChangePassword_CancelDestinationPageUrl = "The URL to redirect to when the cancel button is clicked.";

	// Token: 0x040005BB RID: 1467
	public const string ChangePassword_ChangePasswordError = "Raised if the change password attempt fails.";

	// Token: 0x040005BC RID: 1468
	public const string ChangePassword_ChangedPassword = "Raised after the password has been changed.";

	// Token: 0x040005BD RID: 1469
	public const string ChangePassword_ChangingPassword = "Raised before the change password attempt.";

	// Token: 0x040005BE RID: 1470
	public const string ChangePassword_ChangePasswordFailureText = "The text to be shown when the change password attempt fails";

	// Token: 0x040005BF RID: 1471
	public const string ChangePassword_ContinueButtonClick = "Raised when the continue button is clicked";

	// Token: 0x040005C0 RID: 1472
	public const string LoginControls_ContinueDestinationPageUrl = "The URL to redirect to when the continue button is clicked.";

	// Token: 0x040005C1 RID: 1473
	public const string ChangePassword_ContinueButtonText = "The text of the continue button.";

	// Token: 0x040005C2 RID: 1474
	public const string ChangePassword_ContinueButtonStyle = "The style of the continue button.";

	// Token: 0x040005C3 RID: 1475
	public const string ChangePassword_CreateUserIconUrl = "The URL of the icon for the create user page.";

	// Token: 0x040005C4 RID: 1476
	public const string ChangePassword_CreateUserUrl = "The URL of the create user page.";

	// Token: 0x040005C5 RID: 1477
	public const string ChangePassword_DefaultChangePasswordTitleText = "Change Your Password";

	// Token: 0x040005C6 RID: 1478
	public const string ChangePassword_DefaultChangePasswordFailureText = "Password incorrect or New Password invalid. New Password length minimum: {0}. Non-alphanumeric characters required: {1}.";

	// Token: 0x040005C7 RID: 1479
	public const string ChangePassword_DefaultCancelButtonText = "Cancel";

	// Token: 0x040005C8 RID: 1480
	public const string ChangePassword_DefaultConfirmPasswordRequiredErrorMessage = "Confirm New Password is required.";

	// Token: 0x040005C9 RID: 1481
	public const string ChangePassword_DefaultConfirmNewPasswordLabelText = "Confirm New Password:";

	// Token: 0x040005CA RID: 1482
	public const string ChangePassword_DefaultContinueButtonText = "Continue";

	// Token: 0x040005CB RID: 1483
	public const string ChangePassword_DefaultNewPasswordLabelText = "New Password:";

	// Token: 0x040005CC RID: 1484
	public const string ChangePassword_DefaultNewPasswordRequiredErrorMessage = "New Password is required.";

	// Token: 0x040005CD RID: 1485
	public const string ChangePassword_DefaultConfirmPasswordCompareErrorMessage = "The Confirm New Password must match the New Password entry.";

	// Token: 0x040005CE RID: 1486
	public const string ChangePassword_DefaultPasswordRequiredErrorMessage = "Password is required.";

	// Token: 0x040005CF RID: 1487
	public const string ChangePassword_DefaultChangePasswordButtonText = "Change Password";

	// Token: 0x040005D0 RID: 1488
	public const string ChangePassword_DefaultSuccessTitleText = "Change Password Complete";

	// Token: 0x040005D1 RID: 1489
	public const string ChangePassword_DefaultSuccessText = "Your password has been changed!";

	// Token: 0x040005D2 RID: 1490
	public const string ChangePassword_DefaultUserNameLabelText = "User Name:";

	// Token: 0x040005D3 RID: 1491
	public const string ChangePassword_DefaultUserNameRequiredErrorMessage = "User Name is required.";

	// Token: 0x040005D4 RID: 1492
	public const string ChangePassword_EditProfileText = "The text to be shown for the edit link.";

	// Token: 0x040005D5 RID: 1493
	public const string ChangePassword_EditProfileUrl = "The URL of the edit profile page";

	// Token: 0x040005D6 RID: 1494
	public const string ChangePassword_DisplayUserName = "True if the user name text box should be shown";

	// Token: 0x040005D7 RID: 1495
	public const string ChangePassword_InvalidBorderPadding = "BorderPadding must be greater than or equal to -1.";

	// Token: 0x040005D8 RID: 1496
	public const string ChangePassword_PasswordHintText = "Text that is displayed to give password guidelines.";

	// Token: 0x040005D9 RID: 1497
	public const string ChangePassword_MailDefinition = "The content and format of the e-mail message that contains the successful change password notification.";

	// Token: 0x040005DA RID: 1498
	public const string ChangePassword_NewPasswordRegularExpressionErrorMessage = "The text to be shown when the new password regular expression fails.";

	// Token: 0x040005DB RID: 1499
	public const string ChangePassword_NewPasswordLabelText = "The text that identifies the new password textbox.";

	// Token: 0x040005DC RID: 1500
	public const string ChangePassword_NewPasswordRegularExpression = "Regular expression specification for valid new passwords.";

	// Token: 0x040005DD RID: 1501
	public const string ChangePassword_NewPasswordRequiredErrorMessage = "The text to be shown in the validation summary when the new password is empty.";

	// Token: 0x040005DE RID: 1502
	public const string ChangePassword_NoCurrentPasswordTextBox = "{0}: ChangePasswordTemplate does not contain an IEditableTextControl with ID {1} for the current password.";

	// Token: 0x040005DF RID: 1503
	public const string ChangePassword_NoNewPasswordTextBox = "{0}: ChangePasswordTemplate does not contain an IEditableTextControl with ID {1} for the new password.";

	// Token: 0x040005E0 RID: 1504
	public const string ChangePassword_NoUserNameTextBox = "{0}: ChangePasswordTemplate does not contain an IEditableTextControl with ID {1} for the username, this is required if DisplayUserName=true.";

	// Token: 0x040005E1 RID: 1505
	public const string ChangePassword_UserNameTextBoxNotAllowed = "{0}: ChangePasswordTemplate contains an IEditableTextControl with ID {1} for the username, this is not allowed if DisplayUserName=false.";

	// Token: 0x040005E2 RID: 1506
	public const string ChangePassword_PasswordHintStyle = "The style of the password hint";

	// Token: 0x040005E3 RID: 1507
	public const string ChangePassword_PasswordRecoveryIconUrl = "The URL of the icon for the password recovery link.";

	// Token: 0x040005E4 RID: 1508
	public const string ChangePassword_PasswordRecoveryUrl = "The URL of the password recovery page.";

	// Token: 0x040005E5 RID: 1509
	public const string ChangePassword_PasswordRequiredErrorMessage = "The text to be shown in the validation summary when the password is empty.";

	// Token: 0x040005E6 RID: 1510
	public const string ChangePassword_SendingMail = "Raised before the e-mail is sent.";

	// Token: 0x040005E7 RID: 1511
	public const string ChangePassword_SendMailError = "Raised when there is an error sending mail.";

	// Token: 0x040005E8 RID: 1512
	public const string ChangePassword_ChangePasswordButtonStyle = "The style to be shown for the change password button.";

	// Token: 0x040005E9 RID: 1513
	public const string ChangePassword_SuccessTitleText = "The text to be shown for the title on success";

	// Token: 0x040005EA RID: 1514
	public const string ChangePassword_SuccessTextStyle = "The style of the success text.";

	// Token: 0x040005EB RID: 1515
	public const string ChangePassword_ConfirmNewPasswordLabelText = "The text that identifies the confirm password textbox.";

	// Token: 0x040005EC RID: 1516
	public const string ChangePassword_ValidatorTextStyle = "The style of the validators' text.";

	// Token: 0x040005ED RID: 1517
	public const string CompareValidator_ControlToCompare = "ID of the control to compare with.";

	// Token: 0x040005EE RID: 1518
	public const string CompareValidator_Operator = "Comparison operation to apply to values.";

	// Token: 0x040005EF RID: 1519
	public const string CompareValidator_ValueToCompare = "Value to compare against.";

	// Token: 0x040005F0 RID: 1520
	public const string Content_ContentPlaceHolderID = "The ID of the corresponding ContentPlaceHolder in the master page.";

	// Token: 0x040005F1 RID: 1521
	public const string ContentPlaceHolder_only_in_master = "ContentPlaceHolder can only be used in .master files.";

	// Token: 0x040005F2 RID: 1522
	public const string ContentPlaceHolder_duplicate_contentPlaceHolderID = "Duplicate ContentPlaceHolder '{0}' were found. ContentPlaceHolders require unique IDs.";

	// Token: 0x040005F3 RID: 1523
	public const string CreateUserWizard_AutoGeneratePassword = "Determines if the control autogenerates a password for the user.";

	// Token: 0x040005F4 RID: 1524
	public const string CreateUserWizard_Answer = "The initial value in the answer textbox.";

	// Token: 0x040005F5 RID: 1525
	public const string CreateUserWizard_InvalidAnswerErrorMessage = "Text to be shown when the security answer is invalid.";

	// Token: 0x040005F6 RID: 1526
	public const string CreateUserWizard_AnswerLabelText = "The text that identifies the answer textbox.";

	// Token: 0x040005F7 RID: 1527
	public const string CreateUserWizard_CompleteSuccessText = "The text to be shown after the user has been created.";

	// Token: 0x040005F8 RID: 1528
	public const string CreateUserWizard_ContinueButtonType = "The type of the continue button.";

	// Token: 0x040005F9 RID: 1529
	public const string CreateUserWizard_CreatingUser = "Raised before the user is created.";

	// Token: 0x040005FA RID: 1530
	public const string CreateUserWizard_CreatedUser = "Raised after the user is created.";

	// Token: 0x040005FB RID: 1531
	public const string CreateUserWizard_ConfirmPasswordLabelText = "The text that identifies the confirm password textbox.";

	// Token: 0x040005FC RID: 1532
	public const string CreateUserWizard_ContinueButtonText = "The text of the continue button.";

	// Token: 0x040005FD RID: 1533
	public const string CreateUserWizard_ContinueButtonStyle = "The style of the continue button.";

	// Token: 0x040005FE RID: 1534
	public const string CreateUserWizard_ContinueButtonClick = "Raised when the continue button is clicked.";

	// Token: 0x040005FF RID: 1535
	public const string CreateUserWizard_CreateUserButtonImageUrl = "The URL for the image of the create user button.";

	// Token: 0x04000600 RID: 1536
	public const string CreateUserWizard_CreateUserButtonType = "The type of the create user button.";

	// Token: 0x04000601 RID: 1537
	public const string CreateUserWizard_CreateUserButtonText = "The text of the create user button.";

	// Token: 0x04000602 RID: 1538
	public const string CreateUserWizard_CreateUserButtonStyle = "The style of the create user button.";

	// Token: 0x04000603 RID: 1539
	public const string CreateUserWizard_CreateUserError = "Raised when there is an error creating the user.";

	// Token: 0x04000604 RID: 1540
	public const string CreateUserWizard_CreateUserStep = "The create user WizardStep.";

	// Token: 0x04000605 RID: 1541
	public const string CreateUserWizard_DefaultConfirmPasswordCompareErrorMessage = "The Password and Confirmation Password must match.";

	// Token: 0x04000606 RID: 1542
	public const string CreateUserWizard_DefaultConfirmPasswordRequiredErrorMessage = "Confirm Password is required.";

	// Token: 0x04000607 RID: 1543
	public const string CreateUserWizard_DefaultConfirmPasswordLabelText = "Confirm Password:";

	// Token: 0x04000608 RID: 1544
	public const string CreateUserWizard_DefaultContinueButtonText = "Continue";

	// Token: 0x04000609 RID: 1545
	public const string CreateUserWizard_DefaultCreateUserButtonText = "Create User";

	// Token: 0x0400060A RID: 1546
	public const string CreateUserWizard_DefaultDuplicateUserNameErrorMessage = "Please enter a different user name.";

	// Token: 0x0400060B RID: 1547
	public const string CreateUserWizard_DefaultDuplicateEmailErrorMessage = "The e-mail address that you entered is already in use. Please enter a different e-mail address.";

	// Token: 0x0400060C RID: 1548
	public const string CreateUserWizard_DefaultEmailLabelText = "E-mail:";

	// Token: 0x0400060D RID: 1549
	public const string CreateUserWizard_DefaultUnknownErrorMessage = "Your account was not created. Please try again.";

	// Token: 0x0400060E RID: 1550
	public const string CreateUserWizard_DefaultInvalidEmailErrorMessage = "Please enter a valid e-mail address.";

	// Token: 0x0400060F RID: 1551
	public const string CreateUserWizard_DefaultInvalidPasswordErrorMessage = "Password length minimum: {0}. Non-alphanumeric characters required: {1}.";

	// Token: 0x04000610 RID: 1552
	public const string CreateUserWizard_DefaultCompleteTitleText = "Complete";

	// Token: 0x04000611 RID: 1553
	public const string CreateUserWizard_DefaultPasswordRequiredErrorMessage = "Password is required.";

	// Token: 0x04000612 RID: 1554
	public const string CreateUserWizard_DefaultQuestionLabelText = "Security Question:";

	// Token: 0x04000613 RID: 1555
	public const string CreateUserWizard_DefaultInvalidQuestionErrorMessage = "Please enter a different security question.";

	// Token: 0x04000614 RID: 1556
	public const string CreateUserWizard_DefaultInvalidAnswerErrorMessage = "Please enter a different security answer.";

	// Token: 0x04000615 RID: 1557
	public const string CreateUserWizard_DefaultAnswerLabelText = "Security Answer:";

	// Token: 0x04000616 RID: 1558
	public const string CreateUserWizard_DefaultEmailRegularExpressionErrorMessage = "Please enter a different e-mail.";

	// Token: 0x04000617 RID: 1559
	public const string CreateUserWizard_DefaultCompleteSuccessText = "Your account has been successfully created.";

	// Token: 0x04000618 RID: 1560
	public const string CreateUserWizard_DefaultCreateUserTitleText = "Sign Up for Your New Account";

	// Token: 0x04000619 RID: 1561
	public const string CreateUserWizard_DefaultUserNameLabelText = "User Name:";

	// Token: 0x0400061A RID: 1562
	public const string CreateUserWizard_DefaultUserNameRequiredErrorMessage = "User Name is required.";

	// Token: 0x0400061B RID: 1563
	public const string CreateUserWizard_DefaultAnswerRequiredErrorMessage = "Security answer is required.";

	// Token: 0x0400061C RID: 1564
	public const string CreateUserWizard_DefaultEmailRequiredErrorMessage = "E-mail is required.";

	// Token: 0x0400061D RID: 1565
	public const string CreateUserWizard_DefaultQuestionRequiredErrorMessage = "Security question is required.";

	// Token: 0x0400061E RID: 1566
	public const string CreateUserWizard_DuplicateEmailErrorMessage = "Text to be shown when a duplicate e-mail error is returned from create user.";

	// Token: 0x0400061F RID: 1567
	public const string CreateUserWizard_DuplicateUserNameErrorMessage = "Text to be shown when a duplicate username error is returned from create user.";

	// Token: 0x04000620 RID: 1568
	public const string CreateUserWizard_EditProfileText = "The text to be shown for the edit link.";

	// Token: 0x04000621 RID: 1569
	public const string CreateUserWizard_EditProfileUrl = "The URL of the edit profile page.";

	// Token: 0x04000622 RID: 1570
	public const string CreateUserWizard_Email = "The text to be shown in the initial textbox for e-mail.";

	// Token: 0x04000623 RID: 1571
	public const string CreateUserWizard_EmailRegularExpression = "Regular expression specification for valid e-mail addresses.";

	// Token: 0x04000624 RID: 1572
	public const string CreateUserWizard_EmailRegularExpressionErrorMessage = "The text to be shown in the validation summary when the e-mail does not match the regular expression.";

	// Token: 0x04000625 RID: 1573
	public const string CreateUserWizard_InvalidEmailErrorMessage = "The text to be shown when the e-mail is invalid.";

	// Token: 0x04000626 RID: 1574
	public const string CreateUserWizard_EmailLabelText = "The text that identifies the e-mail textbox.";

	// Token: 0x04000627 RID: 1575
	public const string CreateUserWizard_UnknownErrorMessage = "The text that is displayed for unknown errors.";

	// Token: 0x04000628 RID: 1576
	public const string CreateUserWizard_CompleteStep = "The complete WizardStep.";

	// Token: 0x04000629 RID: 1577
	public const string CreateUserWizard_DisableCreatedUser = "Determines if the newly created user will be disabled.";

	// Token: 0x0400062A RID: 1578
	public const string CreateUserWizard_LoginCreatedUser = "Determines if the newly created user will be logged into the site.";

	// Token: 0x0400062B RID: 1579
	public const string CreateUserWizard_QuestionAndAnswerRequired = "Determines whether a security question and answer is required to create the user.";

	// Token: 0x0400062C RID: 1580
	public const string CreateUserWizard_RequireEmail = "Determines whether an e-mail address is required to create the user.";

	// Token: 0x0400062D RID: 1581
	public const string CreateUserWizard_ErrorMessageStyle = "The style of the error message.";

	// Token: 0x0400062E RID: 1582
	public const string CreateUserWizard_PasswordHintStyle = "The style of the password hint text.";

	// Token: 0x0400062F RID: 1583
	public const string CreateUserWizard_MailDefinition = "The content and format of the e-mail message that contains the create user notification.";

	// Token: 0x04000630 RID: 1584
	public const string CreateUserWizard_InvalidPasswordErrorMessage = "The text to be shown when the password is invalid.";

	// Token: 0x04000631 RID: 1585
	public const string CreateUserWizard_PasswordRegularExpression = "Regular expression specification for valid new passwords.";

	// Token: 0x04000632 RID: 1586
	public const string CreateUserWizard_PasswordRegularExpressionErrorMessage = "The text to be shown in the validation summary when the password does not match the regular expression.";

	// Token: 0x04000633 RID: 1587
	public const string CreateUserWizard_PasswordRequiredErrorMessage = "The text to be shown in the validation summary when the password is empty.";

	// Token: 0x04000634 RID: 1588
	public const string CreateUserWizard_NoPasswordTextBox = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the new password, this is required if AutoGeneratePassword = true.";

	// Token: 0x04000635 RID: 1589
	public const string CreateUserWizard_NoUserNameTextBox = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the username.";

	// Token: 0x04000636 RID: 1590
	public const string CreateUserWizard_NoEmailTextBox = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the e-mail, this is required if RequireEmail = true.";

	// Token: 0x04000637 RID: 1591
	public const string CreateUserWizard_NoQuestionTextBox = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security question, this is required if your membership provider requires a question and answer.";

	// Token: 0x04000638 RID: 1592
	public const string CreateUserWizard_NoAnswerTextBox = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security answer, this is required if your membership provider requires a question and answer.";

	// Token: 0x04000639 RID: 1593
	public const string CreateUserWizard_Question = "The initial value in the question textbox.";

	// Token: 0x0400063A RID: 1594
	public const string CreateUserWizard_InvalidQuestionErrorMessage = "Text to be shown when the security question is invalid.";

	// Token: 0x0400063B RID: 1595
	public const string CreateUserWizard_QuestionLabelText = "The text that identifies the question textbox.";

	// Token: 0x0400063C RID: 1596
	public const string CreateUserWizard_QuestionRequiredErrorMessage = "The text to be shown in the validation summary when the question is empty.";

	// Token: 0x0400063D RID: 1597
	public const string CreateUserWizard_EmailRequiredErrorMessage = "The text to be shown in the validation summary when the e-mail is empty.";

	// Token: 0x0400063E RID: 1598
	public const string CreateUserWizard_SendMailError = "Raised when there is an error sending mail.";

	// Token: 0x0400063F RID: 1599
	public const string CreateUserWizard_SideBar_Label_Not_Found = "{0} control must contain a Label with ID {1} in its ItemTemplate.";

	// Token: 0x04000640 RID: 1600
	public const string CreateUserWizard_CompleteSuccessTextStyle = "The style of the complete success text.";

	// Token: 0x04000641 RID: 1601
	public const string CreateUserWizard_DuplicateCreateUserWizardStep = "There can only be one CreateUserWizardStep in your WizardSteps.";

	// Token: 0x04000642 RID: 1602
	public const string CreateUserWizard_DuplicateCompleteWizardStep = "There can only be one CompleteWizardStep in your WizardSteps.";

	// Token: 0x04000643 RID: 1603
	public const string CreateUserWizard_ValidatorTextStyle = "The style of the validators' text.";

	// Token: 0x04000644 RID: 1604
	public const string TemplatedWizardStep_ContentTemplate = "The content template for the wizard step.";

	// Token: 0x04000645 RID: 1605
	public const string TemplatedWizardStep_CustomNavigationTemplate = "The custom navigation template for the wizard step.";

	// Token: 0x04000646 RID: 1606
	public const string CreateUserWizardStep_AllowReturnCannotBeSet = "AllowReturn cannot be set.";

	// Token: 0x04000647 RID: 1607
	public const string CreateUserWizardStep_StepTypeCannotBeSet = "StepType cannot be changed.";

	// Token: 0x04000648 RID: 1608
	public const string CreateUserWizardStep_OnlyAllowedInCreateUserWizard = "CreateUserWizardStep is only allowed in a CreateUserWizard control.";

	// Token: 0x04000649 RID: 1609
	public const string CompleteWizardStep_OnlyAllowedInCreateUserWizard = "CompleteWizardStep is only allowed in a CreateUserWizard control.";

	// Token: 0x0400064A RID: 1610
	public const string CustomValidator_ClientValidationFunction = "Client script validation function.";

	// Token: 0x0400064B RID: 1611
	public const string CustomValidator_ValidateEmptyText = "Whether the validator validates the control when the text of the control is empty.";

	// Token: 0x0400064C RID: 1612
	public const string CustomValidator_ServerValidate = "Called to perform validation on the server.";

	// Token: 0x0400064D RID: 1613
	public const string BaseDataBoundControl_DataSourceID = "The control ID of an IDataSource that will be used as the data source.";

	// Token: 0x0400064E RID: 1614
	public const string BaseDataBoundControl_DataSource = "The data source that is used to populate the items in the list.";

	// Token: 0x0400064F RID: 1615
	public const string BaseDataBoundControl_OnDataBound = "Fires after the control has been databound.";

	// Token: 0x04000650 RID: 1616
	public const string DataBoundControl_DataMember = "The table or view used for binding against.";

	// Token: 0x04000651 RID: 1617
	public const string DataBoundControl_EnableModelValidation = "Whether page validation will be performed after validation is done in the model.";

	// Token: 0x04000652 RID: 1618
	public const string DataBoundControl_ItemType = "The name of the model type used in the SelectMethod, InsertMethod, UpdateMethod, and DeleteMethod.";

	// Token: 0x04000653 RID: 1619
	public const string DataBoundControl_SelectMethod = "The name of the method on the page that is called when this control does a select operation.";

	// Token: 0x04000654 RID: 1620
	public const string DataBoundControl_UpdateMethod = "The name of the method on the page that is called when this control does an update operation.";

	// Token: 0x04000655 RID: 1621
	public const string DataBoundControl_InsertMethod = "The name of the method on the page that is called when this control does an insert operation.";

	// Token: 0x04000656 RID: 1622
	public const string DataBoundControl_DeleteMethod = "The name of the method on the page that is called when this control does a delete operation.";

	// Token: 0x04000657 RID: 1623
	public const string DataControlField_AccessibleHeaderText = "The text rendered by some controls as the abbreviated text within the header of this field.";

	// Token: 0x04000658 RID: 1624
	public const string DataControlField_ControlStyle = "The style applied to each control within this field.";

	// Token: 0x04000659 RID: 1625
	public const string DataControlField_FooterStyle = "The style applied to footer within this field.";

	// Token: 0x0400065A RID: 1626
	public const string DataControlField_FooterText = "The text within the footer of this field.";

	// Token: 0x0400065B RID: 1627
	public const string DataControlField_HeaderImageUrl = "The URL of the image to be displayed in the header of this field.";

	// Token: 0x0400065C RID: 1628
	public const string DataControlField_HeaderStyle = "The style applied to header within this field.";

	// Token: 0x0400065D RID: 1629
	public const string DataControlField_HeaderText = "The text within the header of this field.";

	// Token: 0x0400065E RID: 1630
	public const string DataControlField_InsertVisible = "Whether the field is present when in Insert mode.";

	// Token: 0x0400065F RID: 1631
	public const string DataControlField_ItemStyle = "The style applied to rows within this field.";

	// Token: 0x04000660 RID: 1632
	public const string DataControlField_ShowHeader = "Whether the field's HeaderText is visible.  This property is used to control layout by some controls.";

	// Token: 0x04000661 RID: 1633
	public const string DataControlField_SortExpression = "The sort expression associated with the field.";

	// Token: 0x04000662 RID: 1634
	public const string DataControlField_Visible = "Whether the field is visible or not.";

	// Token: 0x04000663 RID: 1635
	public const string DataGrid_AllowCustomPaging = "Whether to turn on support for custom paging.";

	// Token: 0x04000664 RID: 1636
	public const string DataGrid_AllowPaging = "Whether to turn on paging functionality in the DataGrid.";

	// Token: 0x04000665 RID: 1637
	public const string DataGrid_AllowSorting = "Whether the column headers can be used to sort the associated data source.";

	// Token: 0x04000666 RID: 1638
	public const string DataGrid_AlternatingItemStyle = "The style applied to alternating items.";

	// Token: 0x04000667 RID: 1639
	public const string DataGrid_CurrentPageIndex = "The index of the current page.";

	// Token: 0x04000668 RID: 1640
	public const string DataGrid_EditItemIndex = "The index of the item shown in edit mode.";

	// Token: 0x04000669 RID: 1641
	public const string DataGrid_EditItemStyle = "The style applied to items in edit mode.";

	// Token: 0x0400066A RID: 1642
	public const string DataGrid_ItemStyle = "The style applied to items.";

	// Token: 0x0400066B RID: 1643
	public const string DataGrid_Items = "The collection of items.";

	// Token: 0x0400066C RID: 1644
	public const string DataGrid_PageCount = "The current page count.";

	// Token: 0x0400066D RID: 1645
	public const string DataGrid_PagerStyle = "Controls the paging UI associated with the control.";

	// Token: 0x0400066E RID: 1646
	public const string DataGrid_PageSize = "The number of items from the data source to display per page.";

	// Token: 0x0400066F RID: 1647
	public const string DataGrid_SelectedItem = "The currently selected item.";

	// Token: 0x04000670 RID: 1648
	public const string DataGrid_SelectedItemStyle = "The style applied to selected items.";

	// Token: 0x04000671 RID: 1649
	public const string DataGrid_OnCancelCommand = "Fires when a Cancel CommandEvent is generated within the DataGrid.";

	// Token: 0x04000672 RID: 1650
	public const string DataGrid_OnDeleteCommand = "Fires when a Delete CommandEvent is generated within the DataGrid.";

	// Token: 0x04000673 RID: 1651
	public const string DataGrid_OnEditCommand = "Fires when an Edit CommandEvent is generated within the DataGrid.";

	// Token: 0x04000674 RID: 1652
	public const string DataGrid_OnItemCommand = "Fires when an event is generated within the DataGrid.";

	// Token: 0x04000675 RID: 1653
	public const string DataGrid_OnPageIndexChanged = "Fires when the current page index of the DataGrid has changed.";

	// Token: 0x04000676 RID: 1654
	public const string DataGrid_OnSortCommand = "Fires when a Sort CommandEvent is generated within the DataGrid.";

	// Token: 0x04000677 RID: 1655
	public const string DataGrid_OnUpdateCommand = "Fires when an Update CommandEvent is generated within the DataGrid.";

	// Token: 0x04000678 RID: 1656
	public const string DataGrid_VisibleItemCount = "The count of visible items.";

	// Token: 0x04000679 RID: 1657
	public const string DataGridColumn_FooterStyle = "The style applied to footer within this column.";

	// Token: 0x0400067A RID: 1658
	public const string DataGridColumn_FooterText = "The text within the footer of this column.";

	// Token: 0x0400067B RID: 1659
	public const string DataGridColumn_HeaderImageUrl = "The URL of the image to be displayed in the header of this column.";

	// Token: 0x0400067C RID: 1660
	public const string DataGridColumn_HeaderStyle = "The style applied to header within this column.";

	// Token: 0x0400067D RID: 1661
	public const string DataGridColumn_HeaderText = "The text within the header of this column.";

	// Token: 0x0400067E RID: 1662
	public const string DataGridColumn_ItemStyle = "The style applied to rows within this column.";

	// Token: 0x0400067F RID: 1663
	public const string DataGridColumn_SortExpression = "The sort expression associated with the column.";

	// Token: 0x04000680 RID: 1664
	public const string DataGridColumn_Visible = "Whether the column is visible or not.";

	// Token: 0x04000681 RID: 1665
	public const string DataGridPagerStyle_Mode = "The type of paging UI to use.";

	// Token: 0x04000682 RID: 1666
	public const string DataGridPagerStyle_PageButtonCount = "Number of pages to show in the paging UI.";

	// Token: 0x04000683 RID: 1667
	public const string DataGridPagerStyle_Position = "The position of the navigation bar.";

	// Token: 0x04000684 RID: 1668
	public const string DataGridPagerStyle_Visible = "Whether the paging UI is visible.";

	// Token: 0x04000685 RID: 1669
	public const string DataList_AlternatingItemStyle = "The style applied to alternating items.";

	// Token: 0x04000686 RID: 1670
	public const string DataList_AlternatingItemTemplate = "The template used for alternating items.";

	// Token: 0x04000687 RID: 1671
	public const string DataList_EditItemIndex = "The index of the item shown in edit mode.";

	// Token: 0x04000688 RID: 1672
	public const string DataList_EditItemStyle = "The style applied to items in edit mode.";

	// Token: 0x04000689 RID: 1673
	public const string DataList_EditItemTemplate = "The template used for items in edit mode.";

	// Token: 0x0400068A RID: 1674
	public const string DataList_ExtractTemplateRows = "Whether to extract the rows defined in the table within the template content.";

	// Token: 0x0400068B RID: 1675
	public const string DataList_FooterTemplate = "Template used for the footer.";

	// Token: 0x0400068C RID: 1676
	public const string DataList_HeaderTemplate = "Template used for the header.";

	// Token: 0x0400068D RID: 1677
	public const string DataList_ItemStyle = "The style applied to items.";

	// Token: 0x0400068E RID: 1678
	public const string DataList_Items = "The collection of items.";

	// Token: 0x0400068F RID: 1679
	public const string DataList_ItemTemplate = "The template used for items.";

	// Token: 0x04000690 RID: 1680
	public const string DataList_RepeatColumns = "The number of columns to be used for the layout.";

	// Token: 0x04000691 RID: 1681
	public const string DataList_SelectedItemStyle = "The style applied to selected items.";

	// Token: 0x04000692 RID: 1682
	public const string DataList_SelectedItem = "The currently selected item.";

	// Token: 0x04000693 RID: 1683
	public const string DataList_SelectedItemTemplate = "The template used for the currently selected item.";

	// Token: 0x04000694 RID: 1684
	public const string DataList_SeparatorStyle = "The style applied to separator items.";

	// Token: 0x04000695 RID: 1685
	public const string DataList_SeparatorTemplate = "The template used for separator items.";

	// Token: 0x04000696 RID: 1686
	public const string DataList_OnCancelCommand = "Fires when a Cancel CommandEvent is generated within the DataList.";

	// Token: 0x04000697 RID: 1687
	public const string DataList_OnDeleteCommand = "Fires when a Delete CommandEvent is generated within the DataList.";

	// Token: 0x04000698 RID: 1688
	public const string DataList_OnEditCommand = "Fires when an Edit CommandEvent is generated within the DataList.";

	// Token: 0x04000699 RID: 1689
	public const string DataList_OnItemCommand = "Fires when a CommandEvent is generated within the DataList.";

	// Token: 0x0400069A RID: 1690
	public const string DataList_OnUpdateCommand = "Fires when an Update CommandEvent is generated within the DataList.";

	// Token: 0x0400069B RID: 1691
	public const string DetailsView_AllowPaging = "Whether to turn on paging functionality in the DetailsView.";

	// Token: 0x0400069C RID: 1692
	public const string DetailsView_AlternatingRowStyle = "The style applied to alternating rows.";

	// Token: 0x0400069D RID: 1693
	public const string DetailsView_AutoGenerateDeleteButton = "Whether a delete button is generated automatically at runtime.";

	// Token: 0x0400069E RID: 1694
	public const string DetailsView_AutoGenerateEditButton = "Whether an edit button is generated automatically at runtime.";

	// Token: 0x0400069F RID: 1695
	public const string DetailsView_AutoGenerateInsertButton = "Whether an insert button is generated automatically at runtime.";

	// Token: 0x040006A0 RID: 1696
	public const string DetailsView_AutoGenerateRows = "Whether the rows are generated automatically at runtime based on the associated data source.";

	// Token: 0x040006A1 RID: 1697
	public const string DetailsView_CellPadding = "The padding within cells.";

	// Token: 0x040006A2 RID: 1698
	public const string DetailsView_CellSpacing = "The spacing between cells.";

	// Token: 0x040006A3 RID: 1699
	public const string DetailsView_CommandRowStyle = "The style applied to rows that contain command fields.";

	// Token: 0x040006A4 RID: 1700
	public const string DetailsView_DataKey = "The Data key of the currently displayed item.";

	// Token: 0x040006A5 RID: 1701
	public const string DetailsView_PageIndex = "The index of the current data item being displayed by the control.";

	// Token: 0x040006A6 RID: 1702
	public const string DetailsView_EnablePagingCallbacks = "Whether client script for paging should be rendered to browser clients that can support callbacks.";

	// Token: 0x040006A7 RID: 1703
	public const string DetailsView_FooterStyle = "The style applied to the footer.";

	// Token: 0x040006A8 RID: 1704
	public const string DetailsView_FooterTemplate = "The template used for the footer.";

	// Token: 0x040006A9 RID: 1705
	public const string DetailsView_FieldHeaderStyle = "The style applied to the header column.";

	// Token: 0x040006AA RID: 1706
	public const string DetailsView_OnPageIndexChanged = "Fires when the page index of the DetailsView has changed.";

	// Token: 0x040006AB RID: 1707
	public const string DetailsView_OnPageIndexChanging = "Fires when the page index of the DetailsView is changing.";

	// Token: 0x040006AC RID: 1708
	public const string DetailsView_OnItemCommand = "Fires when a CommandEvent is generated within the DetailsView.";

	// Token: 0x040006AD RID: 1709
	public const string DetailsView_OnItemCreated = "Fires when the item is created.";

	// Token: 0x040006AE RID: 1710
	public const string DetailsView_OnModeChanged = "Fires after the DetailsView's mode changes.";

	// Token: 0x040006AF RID: 1711
	public const string DetailsView_OnModeChanging = "Fires before the DetailsView's mode changes.";

	// Token: 0x040006B0 RID: 1712
	public const string DetailsView_PagerSettings = "Controls the paging UI settings associated with the control.";

	// Token: 0x040006B1 RID: 1713
	public const string DetailsView_Fields = "The set of fields to be shown in the control.";

	// Token: 0x040006B2 RID: 1714
	public const string DetailsView_Rows = "The collection of rows.";

	// Token: 0x040006B3 RID: 1715
	public const string FontInfo_Name = "The preferred font to be used to render the control.";

	// Token: 0x040006B4 RID: 1716
	public const string FontInfo_Names = "Sequence of fonts that can be used to render the control.";

	// Token: 0x040006B5 RID: 1717
	public const string FontInfo_Size = "The size of the font.";

	// Token: 0x040006B6 RID: 1718
	public const string FontInfo_Bold = "Whether the font is bold.";

	// Token: 0x040006B7 RID: 1719
	public const string FontInfo_Italic = "Whether the font is italic.";

	// Token: 0x040006B8 RID: 1720
	public const string FontInfo_Underline = "Whether the font is underlined.";

	// Token: 0x040006B9 RID: 1721
	public const string FontInfo_Strikeout = "Whether the font has a strike through it.";

	// Token: 0x040006BA RID: 1722
	public const string FontInfo_Overline = "Whether the font contains an overline.";

	// Token: 0x040006BB RID: 1723
	public const string FormView_AllowPaging = "Whether to turn on paging functionality in the FormView.";

	// Token: 0x040006BC RID: 1724
	public const string FormView_CellPadding = "The padding within cells.";

	// Token: 0x040006BD RID: 1725
	public const string FormView_CellSpacing = "The spacing between cells.";

	// Token: 0x040006BE RID: 1726
	public const string FormView_DataKey = "The Data key of the currently displayed item.";

	// Token: 0x040006BF RID: 1727
	public const string FormView_PageIndex = "The index of the current page being displayed by the control.";

	// Token: 0x040006C0 RID: 1728
	public const string FormView_EditItemTemplate = "The template used when the control is in edit mode.";

	// Token: 0x040006C1 RID: 1729
	public const string FormView_RenderOuterTable = "Whether to render a table around the templates in the FormView.";

	// Token: 0x040006C2 RID: 1730
	public const string FormView_FooterStyle = "The style applied to the footer.";

	// Token: 0x040006C3 RID: 1731
	public const string FormView_FooterTemplate = "The template used for the footer.";

	// Token: 0x040006C4 RID: 1732
	public const string FormView_InsertItemTemplate = "The template used when the control is in insert mode.";

	// Token: 0x040006C5 RID: 1733
	public const string FormView_OnPageIndexChanged = "Fires when the page index of the FormView has changed.";

	// Token: 0x040006C6 RID: 1734
	public const string FormView_OnPageIndexChanging = "Fires when the page index of the FormView is changing.";

	// Token: 0x040006C7 RID: 1735
	public const string FormView_OnItemCommand = "Fires when a CommandEvent is generated within the FormView.";

	// Token: 0x040006C8 RID: 1736
	public const string FormView_OnItemCreated = "Fires when the item is created.";

	// Token: 0x040006C9 RID: 1737
	public const string FormView_OnModeChanged = "Fires after the FormView's mode changes.";

	// Token: 0x040006CA RID: 1738
	public const string FormView_OnModeChanging = "Fires before the FormView's mode changes.";

	// Token: 0x040006CB RID: 1739
	public const string FormView_Rows = "The collection of rows.";

	// Token: 0x040006CC RID: 1740
	public const string HiddenField_OnValueChanged = "Fires when the value of the control changes.";

	// Token: 0x040006CD RID: 1741
	public const string HiddenField_Value = "The value stored in the hidden field.";

	// Token: 0x040006CE RID: 1742
	public const string HotSpot_AccessKey = "Keyboard shortcut used by the HotSpot.";

	// Token: 0x040006CF RID: 1743
	public const string HotSpot_AlternateText = "Tool tip to display over the hotspot.";

	// Token: 0x040006D0 RID: 1744
	public const string HotSpot_PostBackValue = "The value for postback event.";

	// Token: 0x040006D1 RID: 1745
	public const string HotSpot_NavigateUrl = "URL for navigation.";

	// Token: 0x040006D2 RID: 1746
	public const string HotSpot_TabIndex = "The tab order of the HotSpot.";

	// Token: 0x040006D3 RID: 1747
	public const string HyperLink_ImageUrl = "The URL of an image to be displayed.";

	// Token: 0x040006D4 RID: 1748
	public const string HyperLink_ImageHeight = "The height of an image to be displayed.";

	// Token: 0x040006D5 RID: 1749
	public const string HyperLink_ImageWidth = "The width of an image to be displayed.";

	// Token: 0x040006D6 RID: 1750
	public const string HyperLink_NavigateUrl = "The URL to navigate to.";

	// Token: 0x040006D7 RID: 1751
	public const string HyperLink_Target = "The target frame for the NavigateUrl.";

	// Token: 0x040006D8 RID: 1752
	public const string HyperLink_Text = "The text to be shown for the link.";

	// Token: 0x040006D9 RID: 1753
	public const string HyperLinkColumn_DataNavigateUrlField = "The field bound to the NavigateUrl property of the hyperlink.";

	// Token: 0x040006DA RID: 1754
	public const string HyperLinkColumn_DataTextField = "The field bound to the text property of the hyperlink.";

	// Token: 0x040006DB RID: 1755
	public const string HyperLinkColumn_NavigateUrl = "The URL navigated to by the hyperlink.";

	// Token: 0x040006DC RID: 1756
	public const string HyperLinkColumn_Text = "The text used for the hyperlink.";

	// Token: 0x040006DD RID: 1757
	public const string HyperLinkField_DataNavigateUrlFields = "The fields bound to the NavigateUrl property of the hyperlink.";

	// Token: 0x040006DE RID: 1758
	public const string HyperLinkField_DataNavigateUrlFormatString = "The formatting applied to the value bound to the NavigateUrl property of the hyperlink. For example, \"page.aspx?id={0}\".";

	// Token: 0x040006DF RID: 1759
	public const string HyperLinkField_DataTextField = "The field bound to the text property of the hyperlink.";

	// Token: 0x040006E0 RID: 1760
	public const string HyperLinkField_DataTextFormatString = "The formatting applied to the value bound to the Text property of the hyperlink. For example, \"go back to: {0}\".";

	// Token: 0x040006E1 RID: 1761
	public const string HyperLinkField_NavigateUrl = "The URL navigated to by the hyperlink.";

	// Token: 0x040006E2 RID: 1762
	public const string HyperLinkField_Text = "The text used for the hyperlink.";

	// Token: 0x040006E3 RID: 1763
	public const string Image_AlternateText = "The alternate text displayed when the image cannot be shown.";

	// Token: 0x040006E4 RID: 1764
	public const string Image_DescriptionUrl = "The URL containing a more detailed description of the image.";

	// Token: 0x040006E5 RID: 1765
	public const string Image_GenerateEmptyAlternateText = "Specifies whether an empty alternate text attribute is generated when the alternate text is not specified.";

	// Token: 0x040006E6 RID: 1766
	public const string Image_ImageAlign = "The alignment of the image.";

	// Token: 0x040006E7 RID: 1767
	public const string Image_ImageUrl = "The URL of the image to be shown.";

	// Token: 0x040006E8 RID: 1768
	public const string ImageButton_OnClick = "Fires when the image is clicked.";

	// Token: 0x040006E9 RID: 1769
	public const string ImageButton_OnCommand = "Fires when the image is clicked and an associated command is defined.";

	// Token: 0x040006EA RID: 1770
	public const string ImageField_AlternateText = "The value of the AlternateText property of the image.";

	// Token: 0x040006EB RID: 1771
	public const string ImageField_DataAlternateTextField = "The field bound to the AlternateText property of the image.";

	// Token: 0x040006EC RID: 1772
	public const string ImageField_DataAlternateTextFormatString = "The formatting applied to the value bound to the AlternateText property of the image when using the DataAlternateTextField. For example, \"image: {0}\".";

	// Token: 0x040006ED RID: 1773
	public const string ImageField_ConvertEmptyStringToNull = "Whether the field treats empty strings as null when the value is extracted from the field.";

	// Token: 0x040006EE RID: 1774
	public const string ImageField_ImageUrlField = "The field to which the image URL is bound.";

	// Token: 0x040006EF RID: 1775
	public const string ImageField_ImageUrlFormatString = "The formatting applied to the value bound to the ImageUrl property of the image.";

	// Token: 0x040006F0 RID: 1776
	public const string ImageField_NullImageUrl = "The URL of the image displayed if the data bound to the field is null.";

	// Token: 0x040006F1 RID: 1777
	public const string ImageField_ReadOnly = "Whether the field does not permit editing of its image field.";

	// Token: 0x040006F2 RID: 1778
	public const string ImageMap_Click = "Fires when a hotspot is clicked.";

	// Token: 0x040006F3 RID: 1779
	public const string ImageMap_HotSpots = "The hotspots collection.";

	// Token: 0x040006F4 RID: 1780
	public const string IRenderOuterTableControl_CannotSetStyleWhenDisableRenderOuterTable = "The style property '{0}' cannot be used while RenderOuterTable is disabled on the {1} control with ID '{2}'.";

	// Token: 0x040006F5 RID: 1781
	public const string Label_AssociatedControlID = "The ID of the control associated with the Label.";

	// Token: 0x040006F6 RID: 1782
	public const string Label_Text = "The text to be shown for the Label.";

	// Token: 0x040006F7 RID: 1783
	public const string Literal_Text = "The text to be shown for the literal.";

	// Token: 0x040006F8 RID: 1784
	public const string Literal_Mode = "Determines whether the text is transformed or encoded.";

	// Token: 0x040006F9 RID: 1785
	public const string LinkButton_Text = "The text to be shown for the link.";

	// Token: 0x040006FA RID: 1786
	public const string LinkButton_OnClick = "Fires when the button is clicked.";

	// Token: 0x040006FB RID: 1787
	public const string ListBox_Rows = "The number of visible rows to display.";

	// Token: 0x040006FC RID: 1788
	public const string ListBox_SelectionMode = "The selection mode for the list.";

	// Token: 0x040006FD RID: 1789
	public const string ListControl_AppendDataBoundItems = "Append data bound items to statically declared list items.";

	// Token: 0x040006FE RID: 1790
	public const string ListControl_AutoPostBack = "Automatically postback to the server after selection is changed.";

	// Token: 0x040006FF RID: 1791
	public const string ListControl_DataTextField = "The field in the data source which provides the item text.";

	// Token: 0x04000700 RID: 1792
	public const string ListControl_DataTextFormatString = "The formatting applied to the text field. For example, \"{0:d}\".";

	// Token: 0x04000701 RID: 1793
	public const string ListControl_DataValueField = "The field in the data source which provides the item value.";

	// Token: 0x04000702 RID: 1794
	public const string ListControl_Items = "The collection of items in the list.";

	// Token: 0x04000703 RID: 1795
	public const string ListControl_SelectedItem = "The currently selected item.";

	// Token: 0x04000704 RID: 1796
	public const string ListControl_SelectedValue = "The value of the currently selected item.";

	// Token: 0x04000705 RID: 1797
	public const string ListControl_OnSelectedIndexChanged = "Fires when the selected index has been changed.";

	// Token: 0x04000706 RID: 1798
	public const string ListControl_Text = "The text value.";

	// Token: 0x04000707 RID: 1799
	public const string ListControl_TextChanged = "Fires when the text property has been changed.";

	// Token: 0x04000708 RID: 1800
	public const string Login_LoggedIn = "Raised after the user is authenticated.";

	// Token: 0x04000709 RID: 1801
	public const string Login_Authenticate = "Raised to authenticate the user.";

	// Token: 0x0400070A RID: 1802
	public const string Login_LoggingIn = "Raised before the user is authenticated.";

	// Token: 0x0400070B RID: 1803
	public const string Login_CheckBoxStyle = "The style of the checkbox.";

	// Token: 0x0400070C RID: 1804
	public const string Login_CreateUserUrl = "The URL of the create user page.";

	// Token: 0x0400070D RID: 1805
	public const string Login_CreateUserIconUrl = "The URL of an icon for the create user link.";

	// Token: 0x0400070E RID: 1806
	public const string Login_DefaultFailureText = "Your login attempt was not successful. Please try again.";

	// Token: 0x0400070F RID: 1807
	public const string LoginControls_DefaultPasswordLabelText = "Password:";

	// Token: 0x04000710 RID: 1808
	public const string Login_DefaultPasswordRequiredErrorMessage = "Password is required.";

	// Token: 0x04000711 RID: 1809
	public const string Login_DefaultRememberMeText = "Remember me next time.";

	// Token: 0x04000712 RID: 1810
	public const string Login_DefaultLoginButtonText = "Log In";

	// Token: 0x04000713 RID: 1811
	public const string Login_DefaultTitleText = "Log In";

	// Token: 0x04000714 RID: 1812
	public const string Login_DefaultUserNameLabelText = "User Name:";

	// Token: 0x04000715 RID: 1813
	public const string Login_DefaultUserNameRequiredErrorMessage = "User Name is required.";

	// Token: 0x04000716 RID: 1814
	public const string Login_DestinationPageUrl = "The URL that the user is directed to upon successful login.";

	// Token: 0x04000717 RID: 1815
	public const string Login_DisplayRememberMe = "True if the remember me checkbox is displayed.";

	// Token: 0x04000718 RID: 1816
	public const string Login_HelpPageIconUrl = "The URL of an icon for the help page link.";

	// Token: 0x04000719 RID: 1817
	public const string Login_InvalidBorderPadding = "BorderPadding must be greater than or equal to -1.";

	// Token: 0x0400071A RID: 1818
	public const string Login_LoginError = "Raised if the authentication fails.";

	// Token: 0x0400071B RID: 1819
	public const string Login_FailureAction = "The action to take when a login attempt fails.";

	// Token: 0x0400071C RID: 1820
	public const string Login_FailureText = "The text to be shown when a login attempt fails.";

	// Token: 0x0400071D RID: 1821
	public const string Login_Orientation = "The general layout of the control.";

	// Token: 0x0400071E RID: 1822
	public const string Login_NoPasswordTextBox = "{0}: LayoutTemplate does not contain an IEditableTextControl with ID {1} for the password.";

	// Token: 0x0400071F RID: 1823
	public const string Login_NoUserNameTextBox = "{0}: LayoutTemplate does not contain an IEditableTextControl with ID {1} for the username.";

	// Token: 0x04000720 RID: 1824
	public const string LoginControls_PasswordLabelText = "The text that identifies the password textbox.";

	// Token: 0x04000721 RID: 1825
	public const string Login_PasswordRecoveryUrl = "The URL of the password recovery page.";

	// Token: 0x04000722 RID: 1826
	public const string Login_PasswordRecoveryIconUrl = "The URL of an icon for the password recovery link.";

	// Token: 0x04000723 RID: 1827
	public const string Login_PasswordRequiredErrorMessage = "The text to be shown in the validation summary when the password is empty.";

	// Token: 0x04000724 RID: 1828
	public const string Login_RememberMeSet = "Whether the remember me checkbox is initially checked.";

	// Token: 0x04000725 RID: 1829
	public const string Login_RememberMeText = "The text to be shown for the remember me checkbox.";

	// Token: 0x04000726 RID: 1830
	public const string LoginControls_RenderOuterTable = "Whether to render a table around the templates in this control.";

	// Token: 0x04000727 RID: 1831
	public const string Login_LoginButtonImageUrl = "The URL of an image to be displayed for the login button.";

	// Token: 0x04000728 RID: 1832
	public const string Login_LoginButtonStyle = "The style of the login button.";

	// Token: 0x04000729 RID: 1833
	public const string Login_LoginButtonType = "The type of the login button.";

	// Token: 0x0400072A RID: 1834
	public const string Login_LoginButtonText = "The text to be shown for the login button.";

	// Token: 0x0400072B RID: 1835
	public const string Login_BorderPadding = "The padding from the border of the control.";

	// Token: 0x0400072C RID: 1836
	public const string Login_ValidatorTextStyle = "The style of the validators' text.";

	// Token: 0x0400072D RID: 1837
	public const string Login_VisibleWhenLoggedIn = "True if the control remains visible when a user is logged in.";

	// Token: 0x0400072E RID: 1838
	public const string LoginName_InvalidFormatString = "FormatString is not a valid format string.";

	// Token: 0x0400072F RID: 1839
	public const string LoginName_FormatString = "The format specification.  {0} is replaced with the user name of the logged in user.";

	// Token: 0x04000730 RID: 1840
	public const string LoginName_DesignModeUserName = "[UserName]";

	// Token: 0x04000731 RID: 1841
	public const string LoginStatus_LoginImageUrl = "The URL of the image to be shown for the login button.";

	// Token: 0x04000732 RID: 1842
	public const string LoginStatus_LoginText = "The text to be shown for the login button.";

	// Token: 0x04000733 RID: 1843
	public const string LoginStatus_LogoutAction = "The action to perform after logging out.";

	// Token: 0x04000734 RID: 1844
	public const string LoginStatus_LogoutImageUrl = "The URL of the image to be shown for the logout button.";

	// Token: 0x04000735 RID: 1845
	public const string LoginStatus_LogoutPageUrl = "The URL redirected to after logging out.";

	// Token: 0x04000736 RID: 1846
	public const string LoginStatus_LogoutText = "The text to be shown for the logout button.";

	// Token: 0x04000737 RID: 1847
	public const string LoginStatus_LoggedOut = "Raised after the user is logged out.";

	// Token: 0x04000738 RID: 1848
	public const string LoginStatus_LoggingOut = "Raised before the user is logged out.";

	// Token: 0x04000739 RID: 1849
	public const string LoginStatus_DefaultLoginText = "Login";

	// Token: 0x0400073A RID: 1850
	public const string LoginStatus_DefaultLogoutText = "Logout";

	// Token: 0x0400073B RID: 1851
	public const string LoginView_RoleGroups = "Associates templates with roles.";

	// Token: 0x0400073C RID: 1852
	public const string LoginView_ViewChanged = "Raised after the view is changed.";

	// Token: 0x0400073D RID: 1853
	public const string LoginView_ViewChanging = "Raised before the view is changed.";

	// Token: 0x0400073E RID: 1854
	public const string EmbeddedMailObject_Name = "The name of the embedded mail object.";

	// Token: 0x0400073F RID: 1855
	public const string EmbeddedMailObject_Path = "The absolute path to the embedded mail object file.";

	// Token: 0x04000740 RID: 1856
	public const string MailDefinition_EmbeddedObjects = "Embedded objects within the e-mail message.";

	// Token: 0x04000741 RID: 1857
	public const string MailDefinition_BodyFileName = "The file that contains the body of the e-mail message.";

	// Token: 0x04000742 RID: 1858
	public const string MailDefinition_CC = "A comma-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message.";

	// Token: 0x04000743 RID: 1859
	public const string MailDefinition_From = "The sender's e-mail address.";

	// Token: 0x04000744 RID: 1860
	public const string MailDefinition_InvalidReplacements = "The replacements dictionary must contain only strings.";

	// Token: 0x04000745 RID: 1861
	public const string MailDefinition_IsBodyHtml = "True if the body is html;";

	// Token: 0x04000746 RID: 1862
	public const string MailDefinition_NoFromAddressSpecified = "A from e-mail address must be specified in the From property or the system.net/mailSettings/smtp config section.";

	// Token: 0x04000747 RID: 1863
	public const string MailDefinition_Priority = "The priority of the e-mail message.";

	// Token: 0x04000748 RID: 1864
	public const string MailDefinition_Subject = "The subject line of the e-mail message.";

	// Token: 0x04000749 RID: 1865
	public const string MenuItemStyle_HorizontalPadding = "The amount of horizontal padding around the item text.";

	// Token: 0x0400074A RID: 1866
	public const string MenuItemStyle_ItemSpacing = "The amount of vertical spacing between items.";

	// Token: 0x0400074B RID: 1867
	public const string MenuItemStyle_VerticalPadding = "The amount of vertical padding around the item text.";

	// Token: 0x0400074C RID: 1868
	public const string MenuItemStyleCollection_InvalidArgument = "MenuItemStyleCollection can only contain MenuItemStyles.";

	// Token: 0x0400074D RID: 1869
	public const string MenuItemBinding_Depth = "The menu depth associated with the menu level object.";

	// Token: 0x0400074E RID: 1870
	public const string MenuItemBinding_Enabled = "The default value for the Enabled property for all menu items in this level when databinding.";

	// Token: 0x0400074F RID: 1871
	public const string MenuItemBinding_EnabledField = "The table column or XML attribute name to use for the menu item's Enabled property when databinding.";

	// Token: 0x04000750 RID: 1872
	public const string MenuItemBinding_FormatString = "The format string to use when formatting the value retrieved from TextField, for example: \"Item {0}\".";

	// Token: 0x04000751 RID: 1873
	public const string MenuItemBinding_ImageUrl = "The default image URL for all menu items in this level when databinding.";

	// Token: 0x04000752 RID: 1874
	public const string MenuItemBinding_ImageUrlField = "The table column or XML attribute name to use for the menu item's ImageUrl property when databinding.";

	// Token: 0x04000753 RID: 1875
	public const string MenuItemBinding_NavigateUrl = "The default navigation URL for all menu items in this level when databinding.";

	// Token: 0x04000754 RID: 1876
	public const string MenuItemBinding_NavigateUrlField = "The table column or XML attribute name to use for the menu item's NavigateUrl property when databinding.";

	// Token: 0x04000755 RID: 1877
	public const string MenuItemBinding_PopOutImageUrl = "The default pop-out image URL for all menu items in this level when databinding.";

	// Token: 0x04000756 RID: 1878
	public const string MenuItemBinding_PopOutImageUrlField = "The table column or XML attribute name to use for the menu item's PopOutImageUrl property when databinding.";

	// Token: 0x04000757 RID: 1879
	public const string MenuItemBinding_Selectable = "The default value for the Selectable property for all menu items in this level when databinding.";

	// Token: 0x04000758 RID: 1880
	public const string MenuItemBinding_SelectableField = "The table column or XML attribute name to use for the menu item's Selectable property when databinding.";

	// Token: 0x04000759 RID: 1881
	public const string MenuItemBinding_SeparatorImageUrl = "The default separator image URL for all menu items in this level when databinding.";

	// Token: 0x0400075A RID: 1882
	public const string MenuItemBinding_SeparatorImageUrlField = "The table column or XML attribute name to use for the menu item's SeparatorImageUrl property when databinding.";

	// Token: 0x0400075B RID: 1883
	public const string MenuItemBinding_Target = "The default navigation target for all menu items in this level when databinding.";

	// Token: 0x0400075C RID: 1884
	public const string MenuItemBinding_TargetField = "The table column or XML attribute name to use for the menu item's Target property when databinding.";

	// Token: 0x0400075D RID: 1885
	public const string MenuItemBinding_Text = "The default text for all menu items in this level when databinding.";

	// Token: 0x0400075E RID: 1886
	public const string MenuItemBinding_TextField = "The table column or XML attribute name to use for a menu item's Text property when databinding.";

	// Token: 0x0400075F RID: 1887
	public const string MenuItemBinding_ToolTip = "The default tooltip for all menu items in this level when databinding.";

	// Token: 0x04000760 RID: 1888
	public const string MenuItemBinding_ToolTipField = "The table column or XML attribute name to use for the menu item's ToolTip property when databinding.";

	// Token: 0x04000761 RID: 1889
	public const string MenuItemBinding_Value = "The default value for all menu items in this level when databinding.";

	// Token: 0x04000762 RID: 1890
	public const string MenuItemBinding_ValueField = "The table column or XML attribute name to use for a menu item's Value property when databinding. When binding to a data source, this is usually a unique key in the current level view.";

	// Token: 0x04000763 RID: 1891
	public const string MenuItem_Enabled = "Enabled state of the menu item.";

	// Token: 0x04000764 RID: 1892
	public const string MenuItem_ImageUrl = "The URL for the image for the menu item.";

	// Token: 0x04000765 RID: 1893
	public const string MenuItem_NavigateUrl = "The URL to which the menu item navigates when selected.";

	// Token: 0x04000766 RID: 1894
	public const string MenuItem_PopOutImageUrl = "The URL for the image that shows if the menu item has children.";

	// Token: 0x04000767 RID: 1895
	public const string MenuItem_Selectable = "If false, the menu item can't be selected, but its child menu items are still accessible.";

	// Token: 0x04000768 RID: 1896
	public const string MenuItem_Selected = "The select state of the menu item.";

	// Token: 0x04000769 RID: 1897
	public const string MenuItem_SeparatorImageUrl = "The URL for the image that will be used as a separator.";

	// Token: 0x0400076A RID: 1898
	public const string MenuItem_Target = "The navigate target used when the menu item is selected.";

	// Token: 0x0400076B RID: 1899
	public const string MenuItem_Text = "The display text of the menu item.";

	// Token: 0x0400076C RID: 1900
	public const string MenuItem_ToolTip = "The tooltip of the menu item.";

	// Token: 0x0400076D RID: 1901
	public const string MenuItem_Value = "The value of the menu item.";

	// Token: 0x0400076E RID: 1902
	public const string MenuItemCollection_InvalidArrayType = "MenuItem[] expected.";

	// Token: 0x0400076F RID: 1903
	public const string Menu_Bindings = "The data bindings for menu items in the menu.";

	// Token: 0x04000770 RID: 1904
	public const string Menu_CannotChangeRenderingMode = "The RenderingMode property cannot be changed once rendering or pre-rendering begins.";

	// Token: 0x04000771 RID: 1905
	public const string Menu_DataSourceReturnedNullView = "The IHierarchicalDataSource that is the data source for Menu '{0}' returned a null view for the given view path.";

	// Token: 0x04000772 RID: 1906
	public const string Menu_DesignTimeDummyItemText = "Menu Item";

	// Token: 0x04000773 RID: 1907
	public const string Menu_DisappearAfter = "The time (in milliseconds) after which the popped-out menus will disappear.";

	// Token: 0x04000774 RID: 1908
	public const string Menu_DynamicBottomSeparatorImageUrl = "The URL of the image that will serve as a bottom separator in the dynamic part of the menu.";

	// Token: 0x04000775 RID: 1909
	public const string Menu_DynamicDisplayPopOutImage = "Determines if the default pop-out image is displayed in the dynamic part of the menu.";

	// Token: 0x04000776 RID: 1910
	public const string Menu_DynamicHorizontalOffset = "The horizontal offset in pixels between the right border of a menu item and the left border of its sub-menu.";

	// Token: 0x04000777 RID: 1911
	public const string Menu_DynamicHoverStyle = "The style that gets applied when the mouse hovers over an item in the dynamic part of the menu (only available when client script is enabled).";

	// Token: 0x04000778 RID: 1912
	public const string Menu_DynamicItemFormatString = "The format string that gets applied to the dynamic item texts, for example \"Item {0}\".";

	// Token: 0x04000779 RID: 1913
	public const string Menu_DynamicMenuItemStyle = "The style of menu items that are in the dynamic part of the menu.";

	// Token: 0x0400077A RID: 1914
	public const string Menu_DynamicMenuStyle = "The style of submenus in the dynamic part.";

	// Token: 0x0400077B RID: 1915
	public const string Menu_DynamicPopoutImageUrl = "The URL of the image that will show that an item has a sub-menu in the dynamic part of the menu.";

	// Token: 0x0400077C RID: 1916
	public const string Menu_DynamicPopoutImageText = "The alternate text for the pop-out image in the dynamic part of the menu.";

	// Token: 0x0400077D RID: 1917
	public const string Menu_DynamicSelectedStyle = "The style applied to the selected item if in the dynamic part of the menu.";

	// Token: 0x0400077E RID: 1918
	public const string Menu_DynamicTemplate = "The template for dynamic menu items.";

	// Token: 0x0400077F RID: 1919
	public const string Menu_DynamicTopSeparatorImageUrl = "The URL of the image that will serve as a top separator in the dynamic part of the menu.";

	// Token: 0x04000780 RID: 1920
	public const string Menu_DynamicVerticalOffset = "The vertical offset in pixels between the top of a menu item and the top of its sub-menu.";

	// Token: 0x04000781 RID: 1921
	public const string Menu_IncludeStyleBlock = "Determines whether or not to render the inline style block (only used in standards compliance mode)";

	// Token: 0x04000782 RID: 1922
	public const string Menu_InvalidDataBinding = "Could not bind to the '{0}' property (specified by {1}) while databinding Menu.  Please check the Bindings fields.";

	// Token: 0x04000783 RID: 1923
	public const string Menu_InvalidDepth = "The depth of the selected item is larger than StaticDisplayLevels + MaximumDynamicDisplayLevels. This could be caused by an invalid declaration, by a change since the last request or by a forged request.";

	// Token: 0x04000784 RID: 1924
	public const string Menu_InvalidNavigation = "Can't open a disabled menu item.";

	// Token: 0x04000785 RID: 1925
	public const string Menu_InvalidSelection = "Can't select a disabled or unselectable menu item.";

	// Token: 0x04000786 RID: 1926
	public const string Menu_Items = "The collection of items in the menu.";

	// Token: 0x04000787 RID: 1927
	public const string Menu_ItemWrap = "Whether the item text should be word wrapped.";

	// Token: 0x04000788 RID: 1928
	public const string Menu_LevelMenuItemStyles = "The menu item styles to be applied at each level of the menu.";

	// Token: 0x04000789 RID: 1929
	public const string Menu_LevelSelectedStyles = "The selected menu item styles to be applied at each level of the menu.";

	// Token: 0x0400078A RID: 1930
	public const string Menu_LevelSubMenuStyles = "The sub-menu styles to be applied at each level of the menu.";

	// Token: 0x0400078B RID: 1931
	public const string Menu_MaximumDynamicDisplayLevels = "The maximum number of pop-outs supported by the menu.";

	// Token: 0x0400078C RID: 1932
	public const string Menu_MaximumDynamicDisplayLevelsInvalid = "MaximumDynamicDisplayLevels must be a positive integer.";

	// Token: 0x0400078D RID: 1933
	public const string Menu_MenuItemClick = "Fired after an item has been clicked.";

	// Token: 0x0400078E RID: 1934
	public const string Menu_MenuItemDataBound = "Fired after a MenuItem is databound.";

	// Token: 0x0400078F RID: 1935
	public const string Menu_Orientation = "Whether the static part of the menu should be rendered horizontally or vertically.";

	// Token: 0x04000790 RID: 1936
	public const string Menu_PathSeparator = "The character used to separate parts of the path.";

	// Token: 0x04000791 RID: 1937
	public const string Menu_RenderingMode = "Whether to render the menu with tables or unordered lists.";

	// Token: 0x04000792 RID: 1938
	public const string Menu_ScrollDown = "Scroll down";

	// Token: 0x04000793 RID: 1939
	public const string Menu_ScrollDownImageUrl = "The URL of the image that will enable scrolling down in sub-menus.";

	// Token: 0x04000794 RID: 1940
	public const string Menu_ScrollDownText = "The tooltip text for the scroll-down button in sub-menus.";

	// Token: 0x04000795 RID: 1941
	public const string Menu_ScrollUpImageUrl = "The URL of the image that will enable scrolling up in sub-menus.";

	// Token: 0x04000796 RID: 1942
	public const string Menu_SkipLinkTextDefault = "Skip Navigation Links";

	// Token: 0x04000797 RID: 1943
	public const string Menu_ScrollUp = "Scroll up";

	// Token: 0x04000798 RID: 1944
	public const string Menu_ScrollUpText = "The tooltip text for the scroll-up button in sub-menus.";

	// Token: 0x04000799 RID: 1945
	public const string Menu_StaticBottomSeparatorImageUrl = "The URL of the image that will serve as a bottom separator in the static part of the menu.";

	// Token: 0x0400079A RID: 1946
	public const string Menu_StaticDisplayLevels = "The number of levels displayed in the static part of the menu.";

	// Token: 0x0400079B RID: 1947
	public const string Menu_StaticDisplayPopOutImage = "Determines if the default pop-out image is displayed in the static part of the menu.";

	// Token: 0x0400079C RID: 1948
	public const string Menu_StaticHoverStyle = "The style that gets applied when the mouse hovers over an item in the static part of the menu (only available when client script is enabled).";

	// Token: 0x0400079D RID: 1949
	public const string Menu_StaticItemFormatString = "The format string that gets applied to the static item texts, for example \"Item {0}\".";

	// Token: 0x0400079E RID: 1950
	public const string Menu_StaticMenuItemStyle = "The style of menu items that are in the static part of the menu.";

	// Token: 0x0400079F RID: 1951
	public const string Menu_StaticMenuStyle = "The style of the static part of the menu.";

	// Token: 0x040007A0 RID: 1952
	public const string Menu_StaticPopoutImageText = "The alternate text for the pop-out image in the static part of the menu.";

	// Token: 0x040007A1 RID: 1953
	public const string Menu_StaticPopoutImageUrl = "The URL of the image that will show that an item has a sub-menu in the static part of the menu.";

	// Token: 0x040007A2 RID: 1954
	public const string Menu_StaticSelectedStyle = "The style applied to the selected item if in the static part of the menu.";

	// Token: 0x040007A3 RID: 1955
	public const string Menu_StaticSubMenuIndent = "The indentation between a static menu item and its static sub-menu.";

	// Token: 0x040007A4 RID: 1956
	public const string Menu_StaticTemplate = "The template for static menu items.";

	// Token: 0x040007A5 RID: 1957
	public const string Menu_StaticTopSeparatorImageUrl = "The URL of the image that will serve as a top separator in the static part of the menu.";

	// Token: 0x040007A6 RID: 1958
	public const string ModelErrorMessage_AssociatedControlID = "The ID of the control associated with the model state error message.";

	// Token: 0x040007A7 RID: 1959
	public const string ModelErrorMessage_ModelStateKey = "The key of the model state value to display the first error for.";

	// Token: 0x040007A8 RID: 1960
	public const string ModelErrorMessage_SetFocusOnError = "Whether focus is set on the associated control when a model state error message is found.";

	// Token: 0x040007A9 RID: 1961
	public const string MultiView_ActiveView = "The active View control.";

	// Token: 0x040007AA RID: 1962
	public const string MultiView_ActiveViewChanged = "Fires when the active view control is changed.";

	// Token: 0x040007AB RID: 1963
	public const string MultiView_ActiveViewIndex_out_of_range = "The ActiveViewIndex must be less than Views.Count and at least -1.";

	// Token: 0x040007AC RID: 1964
	public const string MultiView_cannot_have_children_of_type = "MultiView cannot have children of type '{0}'.  It can only have children of type View.";

	// Token: 0x040007AD RID: 1965
	public const string Multiview_rendering_block_not_allowed = "A rendering block cannot be nested inside a MultiView control.";

	// Token: 0x040007AE RID: 1966
	public const string MultiView_Views = "The collection of Views.";

	// Token: 0x040007AF RID: 1967
	public const string MultiView_invalid_view_id = "MultiView with id '{0}' could not find a View '{1}' specified in CommandArgument with CommandName '{2}'.";

	// Token: 0x040007B0 RID: 1968
	public const string MultiView_invalid_view_index_format = "CommandArgument '{0}' with CommandName '{1}' is not a valid integer for ActiveViewIndex.";

	// Token: 0x040007B1 RID: 1969
	public const string MultiView_view_not_found = "The view {0} cannot be found inside {1}, the ActiveView must be a View control directly inside a MultiView.";

	// Token: 0x040007B2 RID: 1970
	public const string MultiView_ActiveViewIndex_less_than_minus_one = "ActiveViewIndex is being set to '{0}'.  It cannot be less than -1.";

	// Token: 0x040007B3 RID: 1971
	public const string MultiView_ActiveViewIndex_equal_or_greater_than_count = "ActiveViewIndex is being set to '{0}'.  It must be smaller than the current number of View controls '{1}'. For dynamically added views, make sure they are added before or in Page_PreInit event.";

	// Token: 0x040007B4 RID: 1972
	public const string View_CannotSetVisible = "The Visible property of a View control can only be set by setting the active View of a MultiView.";

	// Token: 0x040007B5 RID: 1973
	public const string SiteMapPath_CannotFindUrl = "Could not find the sitemap node with URL '{0}'.";

	// Token: 0x040007B6 RID: 1974
	public const string SiteMapPath_CurrentNodeStyle = "The style applied to current node.";

	// Token: 0x040007B7 RID: 1975
	public const string SiteMapPath_CurrentNodeTemplate = "The template used for the current node.";

	// Token: 0x040007B8 RID: 1976
	public const string SiteMapPath_OnItemDataBound = "Fires when an item is databound.";

	// Token: 0x040007B9 RID: 1977
	public const string SiteMapPath_NodeStyle = "The style applied to navigation nodes.";

	// Token: 0x040007BA RID: 1978
	public const string SiteMapPath_NodeTemplate = "The template used for the navigation nodes.";

	// Token: 0x040007BB RID: 1979
	public const string SiteMapPath_PathDirection = "The direction of path to render.";

	// Token: 0x040007BC RID: 1980
	public const string SiteMapPath_PathSeparator = "The separator string between each node.";

	// Token: 0x040007BD RID: 1981
	public const string SiteMapPath_PathSeparatorTemplate = "The template used for the path separators.";

	// Token: 0x040007BE RID: 1982
	public const string SiteMapPath_PathSeparatorStyle = "The style applied to the path separators.";

	// Token: 0x040007BF RID: 1983
	public const string SiteMapPath_Provider = "The SitemapProvider used by this control.";

	// Token: 0x040007C0 RID: 1984
	public const string SiteMapPath_RenderCurrentNodeAsLink = "Indicates whether the current node will be rendered as a link.";

	// Token: 0x040007C1 RID: 1985
	public const string SiteMapPath_RootNodeStyle = "The style applied to root node.";

	// Token: 0x040007C2 RID: 1986
	public const string SiteMapPath_RootNodeTemplate = "The template used for the root node.";

	// Token: 0x040007C3 RID: 1987
	public const string SiteMapPath_SiteMapProvider = "The name of the sitemap provider.";

	// Token: 0x040007C4 RID: 1988
	public const string SiteMapPath_SkipToContentText = "The text that appears in the ALT attribute of the invisible image link that allows screen readers to skip repetitive content.";

	// Token: 0x040007C5 RID: 1989
	public const string SiteMapPath_Default_SkipToContentText = "Skip Navigation Links";

	// Token: 0x040007C6 RID: 1990
	public const string SiteMapPath_ShowToolTips = "Indicates whether tooltip will be shown.";

	// Token: 0x040007C7 RID: 1991
	public const string SiteMapPath_ParentLevelsDisplayed = "The number of parent nodes to display.";

	// Token: 0x040007C8 RID: 1992
	public const string SubMenuStyle_HorizontalPadding = "The amount of horizontal padding around the items.";

	// Token: 0x040007C9 RID: 1993
	public const string SubMenuStyle_VerticalPadding = "The amount of vertical padding around the items.";

	// Token: 0x040007CA RID: 1994
	public const string SubMenuStyleCollection_InvalidArgument = "SubMenuStyleCollection can only contain SubMenuStyles.";

	// Token: 0x040007CB RID: 1995
	public const string Panel_BackImageUrl = "The background image of the panel.";

	// Token: 0x040007CC RID: 1996
	public const string Panel_DefaultButton = "The default button for the panel.";

	// Token: 0x040007CD RID: 1997
	public const string Panel_Direction = "The direction of text in the panel.";

	// Token: 0x040007CE RID: 1998
	public const string Panel_GroupingText = "The text of group box around this control's contents.";

	// Token: 0x040007CF RID: 1999
	public const string Panel_HorizontalAlign = "The horizontal alignment of the content.";

	// Token: 0x040007D0 RID: 2000
	public const string Panel_ScrollBars = "The appearance of scrollbars for the panel.";

	// Token: 0x040007D1 RID: 2001
	public const string Panel_Wrap = "Whether the content should wrap or not.";

	// Token: 0x040007D2 RID: 2002
	public const string PasswordRecovery_AnswerLabelText = "The text that identifies the answer textbox.";

	// Token: 0x040007D3 RID: 2003
	public const string PasswordRecovery_AnswerLookupError = "Raised when the answer provided is incorrect.";

	// Token: 0x040007D4 RID: 2004
	public const string PasswordRecovery_VerifyingAnswer = "Raised before the answer is validated.";

	// Token: 0x040007D5 RID: 2005
	public const string PasswordRecovery_SendingMail = "Raised before the e-mail is sent.";

	// Token: 0x040007D6 RID: 2006
	public const string PasswordRecovery_VerifyingUser = "Raised before the username is looked up.";

	// Token: 0x040007D7 RID: 2007
	public const string PasswordRecovery_DefaultAnswerLabelText = "Answer:";

	// Token: 0x040007D8 RID: 2008
	public const string PasswordRecovery_DefaultAnswerRequiredErrorMessage = "Answer is required.";

	// Token: 0x040007D9 RID: 2009
	public const string PasswordRecovery_DefaultBody = "Please return to the site and log in using the following information.\nUser Name: <%UserName%>\nPassword: <%Password%>\n\n";

	// Token: 0x040007DA RID: 2010
	public const string PasswordRecovery_DefaultGeneralFailureText = "Your attempt to retrieve your password was not successful. Please try again.";

	// Token: 0x040007DB RID: 2011
	public const string PasswordRecovery_DefaultUserNameFailureText = "We were unable to access your information. Please try again.";

	// Token: 0x040007DC RID: 2012
	public const string PasswordRecovery_DefaultQuestionInstructionText = "Answer the following question to receive your password.";

	// Token: 0x040007DD RID: 2013
	public const string PasswordRecovery_DefaultQuestionFailureText = "Your answer could not be verified. Please try again.";

	// Token: 0x040007DE RID: 2014
	public const string PasswordRecovery_DefaultQuestionLabelText = "Question:";

	// Token: 0x040007DF RID: 2015
	public const string PasswordRecovery_DefaultQuestionTitleText = "Identity Confirmation";

	// Token: 0x040007E0 RID: 2016
	public const string PasswordRecovery_DefaultSubject = "Password";

	// Token: 0x040007E1 RID: 2017
	public const string PasswordRecovery_DefaultSubmitButtonText = "Submit";

	// Token: 0x040007E2 RID: 2018
	public const string PasswordRecovery_DefaultSuccessText = "Your password has been sent to you.";

	// Token: 0x040007E3 RID: 2019
	public const string PasswordRecovery_DefaultUserNameInstructionText = "Enter your User Name to receive your password.";

	// Token: 0x040007E4 RID: 2020
	public const string PasswordRecovery_DefaultUserNameLabelText = "User Name:";

	// Token: 0x040007E5 RID: 2021
	public const string PasswordRecovery_DefaultUserNameRequiredErrorMessage = "User Name is required.";

	// Token: 0x040007E6 RID: 2022
	public const string PasswordRecovery_DefaultUserNameTitleText = "Forgot Your Password?";

	// Token: 0x040007E7 RID: 2023
	public const string PasswordRecovery_GeneralFailureText = "The text to be shown when there is an unexpected failure.";

	// Token: 0x040007E8 RID: 2024
	public const string PasswordRecovery_InvalidBorderPadding = "BorderPadding must be greater than or equal to -1.";

	// Token: 0x040007E9 RID: 2025
	public const string PasswordRecovery_MailDefinition = "The content and format of the e-mail message that contains the successful recovered password notification.";

	// Token: 0x040007EA RID: 2026
	public const string PasswordRecovery_NoUserNameTextBox = "{0}: UserNameTemplate does not contain an IEditableTextControl with ID {1} for the username.";

	// Token: 0x040007EB RID: 2027
	public const string PasswordRecovery_NoAnswerTextBox = "{0}: QuestionTemplate does not contain an IEditableTextControl with ID {1} for the answer.";

	// Token: 0x040007EC RID: 2028
	public const string PasswordRecovery_QuestionFailureText = "The text to be shown when the answer is incorrect.";

	// Token: 0x040007ED RID: 2029
	public const string PasswordRecovery_QuestionInstructionText = "Text that is displayed to give instructions for answering the question.";

	// Token: 0x040007EE RID: 2030
	public const string PasswordRecovery_QuestionLabelText = "The text that identifies the question textbox.";

	// Token: 0x040007EF RID: 2031
	public const string PasswordRecovery_QuestionTemplate = "Template for the question view.";

	// Token: 0x040007F0 RID: 2032
	public const string PasswordRecovery_QuestionTemplateContainer = "Container for the question view.";

	// Token: 0x040007F1 RID: 2033
	public const string PasswordRecovery_QuestionTitleText = "The text to be shown for the title when answering the question.";

	// Token: 0x040007F2 RID: 2034
	public const string PasswordRecovery_RecoveryNotSupported = "Membership provider does not support password retrieval or reset.";

	// Token: 0x040007F3 RID: 2035
	public const string PasswordRecovery_SubmitButtonStyle = "The style of the submit button.";

	// Token: 0x040007F4 RID: 2036
	public const string PasswordRecovery_SubmitButtonType = "The type of the submit button.";

	// Token: 0x040007F5 RID: 2037
	public const string PasswordRecovery_SuccessTemplate = "Template for the success view.";

	// Token: 0x040007F6 RID: 2038
	public const string PasswordRecovery_SuccessTemplateContainer = "Container for the success view.";

	// Token: 0x040007F7 RID: 2039
	public const string PasswordRecovery_SuccessText = "The text to be shown after the password e-mail has been sent.";

	// Token: 0x040007F8 RID: 2040
	public const string PasswordRecovery_SuccessTextStyle = "The style of the success text.";

	// Token: 0x040007F9 RID: 2041
	public const string PasswordRecovery_UserLookupError = "Raised when the user name is invalid.";

	// Token: 0x040007FA RID: 2042
	public const string PasswordRecovery_UserNameFailureText = "The text to be shown when the user name is invalid.";

	// Token: 0x040007FB RID: 2043
	public const string PasswordRecovery_UserNameInstructionText = "Text that is displayed to give instructions for entering the user name.";

	// Token: 0x040007FC RID: 2044
	public const string PasswordRecovery_UserNameLabelText = "The text that identifies the user name textbox.";

	// Token: 0x040007FD RID: 2045
	public const string PasswordRecovery_UserNameTemplate = "Template for the username view.";

	// Token: 0x040007FE RID: 2046
	public const string PasswordRecovery_UserNameTemplateContainer = "Container for the username view.";

	// Token: 0x040007FF RID: 2047
	public const string PasswordRecovery_UserNameTitleText = "The text to be shown for the title when entering the user name.";

	// Token: 0x04000800 RID: 2048
	public const string PolygonHotSpot_Coordinates = "The coordinates of the points representing the polygon.";

	// Token: 0x04000801 RID: 2049
	public const string RadioButton_GroupName = "Group that this radio button belongs to.";

	// Token: 0x04000802 RID: 2050
	public const string RadioButtonList_CellPadding = "The padding between each item.";

	// Token: 0x04000803 RID: 2051
	public const string RadioButtonList_CellSpacing = "The spacing between each item.";

	// Token: 0x04000804 RID: 2052
	public const string RadioButtonList_RepeatColumns = "The number of columns to use to lay out the items.";

	// Token: 0x04000805 RID: 2053
	public const string RangeValidator_MaximumValue = "Maximum value for the control being validated.";

	// Token: 0x04000806 RID: 2054
	public const string RangeValidator_MinmumValue = "Minimum value for the control being validated.";

	// Token: 0x04000807 RID: 2055
	public const string RangeValidator_Type = "Data type of values for comparison.";

	// Token: 0x04000808 RID: 2056
	public const string ReadOnlyHierarchicalDataSourceView_CantAccessPathInEnumerable = "Can't use a view path when the data source implements IHierarchicalEnumerable and not IHierarchicalDataSource.";

	// Token: 0x04000809 RID: 2057
	public const string RectangleHotSpot_Bottom = "The bottom coordinate of the rectangular hot spot.";

	// Token: 0x0400080A RID: 2058
	public const string RectangleHotSpot_Right = "The right coordinate of the rectangular hot spot.";

	// Token: 0x0400080B RID: 2059
	public const string RectangleHotSpot_Top = "The top coordinate of the rectangular hot spot.";

	// Token: 0x0400080C RID: 2060
	public const string RectangleHotSpot_Left = "The left coordinate of the rectangular hot spot.";

	// Token: 0x0400080D RID: 2061
	public const string RegularExpressionValidator_ValidationExpression = "Regular expression to determine validity.";

	// Token: 0x0400080E RID: 2062
	public const string Repeater_AlternatingItemTemplate = "The template used for alternating items.";

	// Token: 0x0400080F RID: 2063
	public const string Repeater_DataMember = "The table or view used for binding when a DataSet is used a data source.";

	// Token: 0x04000810 RID: 2064
	public const string Repeater_FooterTemplate = "The template used for the footer.";

	// Token: 0x04000811 RID: 2065
	public const string Repeater_Items = "The collection of items.";

	// Token: 0x04000812 RID: 2066
	public const string Repeater_ItemTemplate = "The template used for the items.";

	// Token: 0x04000813 RID: 2067
	public const string Repeater_OnItemCommand = "Fires when an event is generated within the DataList.";

	// Token: 0x04000814 RID: 2068
	public const string Repeater_SeparatorTemplate = "The template used for the separators.";

	// Token: 0x04000815 RID: 2069
	public const string RepeatInfo_ListLayoutDoesNotSupportHeaderFooterSeparator = "The UnorderedList and OrderedList layouts do not support headers, footers, or separators.";

	// Token: 0x04000816 RID: 2070
	public const string RepeatInfo_ListLayoutOnlySupportsVerticalLayout = "The UnorderedList and OrderedList layouts only support vertical layout.";

	// Token: 0x04000817 RID: 2071
	public const string RepeatInfo_ListLayoutDoesNotSupportMultipleColumn = "The UnorderedList and OrderedList layouts do not support multi-column layouts.";

	// Token: 0x04000818 RID: 2072
	public const string RepeatInfo_ListLayoutDoesNotSupportImpliedOuterTable = "The UnorderedList and OrderedList layouts do not support implied outer tables.";

	// Token: 0x04000819 RID: 2073
	public const string RequiredFieldValidator_InitialValue = "Initial value of the field to validate.";

	// Token: 0x0400081A RID: 2074
	public const string SiteMapDataSource_Description = "Connect to the site navigation tree for this application (requires a valid sitemap file at the application root).";

	// Token: 0x0400081B RID: 2075
	public const string SiteMapDataSource_DisplayName = "Site Map";

	// Token: 0x0400081C RID: 2076
	public const string SiteMapDataSource_Provider = "The SitemapProvider used by this control.";

	// Token: 0x0400081D RID: 2077
	public const string SiteMapDataSource_ContainsListCollection = "Indicates whether the data source control contains ListCollection.";

	// Token: 0x0400081E RID: 2078
	public const string SiteMapDataSource_StartingNodeOffset = "The depth at which the base node of the SiteMap starts.";

	// Token: 0x0400081F RID: 2079
	public const string SiteMapDataSource_StartingNodeUrl = "The URL of the starting node.";

	// Token: 0x04000820 RID: 2080
	public const string SiteMapDataSource_SiteMapProvider = "The name of the provider used to populate the data source control.";

	// Token: 0x04000821 RID: 2081
	public const string SiteMapDataSource_ProviderNotFound = "The SiteMapProvider '{0}' cannot be found.";

	// Token: 0x04000822 RID: 2082
	public const string SiteMapDataSource_DefaultProviderNotFound = "Default provider is not defined in your configuration files. You must specify the defaultProvider within <siteMap> section to enable sitemap feature.";

	// Token: 0x04000823 RID: 2083
	public const string SiteMapDataSource_ShowStartingNode = "Indicates whether the starting node should be returned.";

	// Token: 0x04000824 RID: 2084
	public const string SiteMapDataSource_StartFromCurrentNode = "Indicates whether to display from the current node.";

	// Token: 0x04000825 RID: 2085
	public const string SiteMapDataSource_StartingNodeUrlAndStartFromcurrentNode_Defined = "StartingNodeUrl may not be set when StartFromCurrentNode is true.";

	// Token: 0x04000826 RID: 2086
	public const string GridView_AllowCustomPaging = "Whether to turn on support for custom paging.";

	// Token: 0x04000827 RID: 2087
	public const string GridView_AllowPaging = "Whether to turn on paging functionality in the GridView.";

	// Token: 0x04000828 RID: 2088
	public const string GridView_AllowSorting = "Whether the field headers can be used to sort the associated data source.";

	// Token: 0x04000829 RID: 2089
	public const string GridView_AlternatingRowStyle = "The style applied to alternating rows.";

	// Token: 0x0400082A RID: 2090
	public const string GridView_AutoGenerateDeleteButton = "Whether the delete button is generated automatically at runtime.";

	// Token: 0x0400082B RID: 2091
	public const string GridView_AutoGenerateEditButton = "Whether the edit button is generated automatically at runtime.";

	// Token: 0x0400082C RID: 2092
	public const string GridView_AutoGenerateSelectButton = "Whether the select button is generated automatically at runtime.";

	// Token: 0x0400082D RID: 2093
	public const string GridView_CellPadding = "The padding within cells.";

	// Token: 0x0400082E RID: 2094
	public const string GridView_CellSpacing = "The spacing between cells.";

	// Token: 0x0400082F RID: 2095
	public const string GridView_DataKeys = "The collection of data key field values.";

	// Token: 0x04000830 RID: 2096
	public const string GridView_EditIndex = "The index of the row shown in edit mode.";

	// Token: 0x04000831 RID: 2097
	public const string GridView_EditRowStyle = "The style applied to rows in edit mode.";

	// Token: 0x04000832 RID: 2098
	public const string GridView_EnableSortingAndPagingCallbacks = "Whether client script for sorting and paging should be rendered to browser clients that can support callbacks.";

	// Token: 0x04000833 RID: 2099
	public const string GridView_EnablePersistedSelection = "Whether selection should be based on DataKeys or row index.";

	// Token: 0x04000834 RID: 2100
	public const string GridView_EmptyDataRowStyle = "The style applied to the row that contain the EmptyDataTemplate.";

	// Token: 0x04000835 RID: 2101
	public const string GridView_OnRowCancelingEdit = "Fires when a Cancel event is generated within the GridView.";

	// Token: 0x04000836 RID: 2102
	public const string GridView_OnRowEditing = "Fires when an Edit event is generated within the GridView.";

	// Token: 0x04000837 RID: 2103
	public const string GridView_OnPageIndexChanging = "Fires when the current page index of the GridView is changing.";

	// Token: 0x04000838 RID: 2104
	public const string GridView_OnPageIndexChanged = "Fires when the current page index of the GridView has changed.";

	// Token: 0x04000839 RID: 2105
	public const string GridView_OnSelectedIndexChanged = "Fires when a row is selected in the GridView, after the selection is complete.";

	// Token: 0x0400083A RID: 2106
	public const string GridView_OnSelectedIndexChanging = "Fires when a new row is selected in the GridView, before the new row is selected.";

	// Token: 0x0400083B RID: 2107
	public const string GridView_OnSorted = "Fires when a column is sorted in the GridView, after the sort is complete.";

	// Token: 0x0400083C RID: 2108
	public const string GridView_OnSorting = "Fires when a column is sorted in the GridView, before the sort occurs.";

	// Token: 0x0400083D RID: 2109
	public const string GridView_OnRowCommand = "Fires when an event is generated within the GridView.";

	// Token: 0x0400083E RID: 2110
	public const string GridView_OnRowCreated = "Fires when a row is created.";

	// Token: 0x0400083F RID: 2111
	public const string GridView_OnRowDataBound = "Fires after a row has been databound.";

	// Token: 0x04000840 RID: 2112
	public const string GridView_PageCount = "The current page count.";

	// Token: 0x04000841 RID: 2113
	public const string GridView_PageIndex = "The index of the current page.";

	// Token: 0x04000842 RID: 2114
	public const string GridView_PagerSettings = "Controls the paging UI settings associated with the control.";

	// Token: 0x04000843 RID: 2115
	public const string GridView_PageSize = "The number of rows from the data source to display per page.";

	// Token: 0x04000844 RID: 2116
	public const string GridView_RowHeaderColumn = "The data source field corresponding to the column that is the row header.";

	// Token: 0x04000845 RID: 2117
	public const string GridView_Rows = "The collection of rows.";

	// Token: 0x04000846 RID: 2118
	public const string GridView_ShowHeaderWhenEmpty = "Whether to the show the header when displaying the EmptyDataTemplate.";

	// Token: 0x04000847 RID: 2119
	public const string GridView_SelectedIndex = "The index of the currently selected row.";

	// Token: 0x04000848 RID: 2120
	public const string GridView_SelectedRow = "The currently selected row.";

	// Token: 0x04000849 RID: 2121
	public const string GridView_SelectedRowStyle = "The style applied to selected rows.";

	// Token: 0x0400084A RID: 2122
	public const string GridView_SortDirection = "The direction in which to sort the field.";

	// Token: 0x0400084B RID: 2123
	public const string GridView_SortExpression = "Sort expression used to sort the data source to which the GridView is binding.";

	// Token: 0x0400084C RID: 2124
	public const string GridView_SortedAscendingCellStyle = "The style applied to cells when sorting in ascending order.";

	// Token: 0x0400084D RID: 2125
	public const string GridView_SortedDescendingCellStyle = "The style applied to cells when sorting in descending order.";

	// Token: 0x0400084E RID: 2126
	public const string GridView_SortedAscendingHeaderStyle = "The style applied to header when sorting in ascending order.";

	// Token: 0x0400084F RID: 2127
	public const string GridView_SortedDescendingHeaderStyle = "The style applied to header when sorting in descending order.";

	// Token: 0x04000850 RID: 2128
	public const string GridView_VirtualItemCount = "The count of visible items.";

	// Token: 0x04000851 RID: 2129
	public const string PagerSettings_FirstPageImageUrl = "The URL to the image to be used for the first page button.";

	// Token: 0x04000852 RID: 2130
	public const string PagerSettings_FirstPageText = "The text to be used on the first page button.";

	// Token: 0x04000853 RID: 2131
	public const string PagerSettings_LastPageImageUrl = "The URL to the image to be used for the last page button.";

	// Token: 0x04000854 RID: 2132
	public const string PagerSettings_LastPageText = "The text to be used on the last page button.";

	// Token: 0x04000855 RID: 2133
	public const string PagerSettings_Mode = "The type of paging UI to use.";

	// Token: 0x04000856 RID: 2134
	public const string PagerSettings_NextPageImageUrl = "The URL to the image to be used for the next page button.";

	// Token: 0x04000857 RID: 2135
	public const string PagerSettings_PageButtonCount = "Number of pages to show in the paging UI.";

	// Token: 0x04000858 RID: 2136
	public const string PagerSettings_PreviousPageImageUrl = "The URL to the image to be used for the previous page button.";

	// Token: 0x04000859 RID: 2137
	public const string PagerStyle_Position = "The position of the navigation bar.";

	// Token: 0x0400085A RID: 2138
	public const string PagerStyle_Visible = "Whether the paging UI is visible.";

	// Token: 0x0400085B RID: 2139
	public const string Style_BackColor = "Background color of the controls applied with this style.";

	// Token: 0x0400085C RID: 2140
	public const string Style_BorderColor = "Border color of the controls applied with this style.";

	// Token: 0x0400085D RID: 2141
	public const string Style_BorderWidth = "Thickness of the border around the controls applied with this style.";

	// Token: 0x0400085E RID: 2142
	public const string Style_BorderStyle = "Style of the border of controls applied with this style.";

	// Token: 0x0400085F RID: 2143
	public const string Style_CSSClass = "CSS class name applied to the user of this style.";

	// Token: 0x04000860 RID: 2144
	public const string Style_Font = "The font used for text in controls applied with this style.";

	// Token: 0x04000861 RID: 2145
	public const string Style_ForeColor = "Foreground color of the controls applied with this style.";

	// Token: 0x04000862 RID: 2146
	public const string Style_Height = "The height of the controls applied with this style.";

	// Token: 0x04000863 RID: 2147
	public const string Style_Width = "The width of the controls applied with this style.";

	// Token: 0x04000864 RID: 2148
	public const string Substitution_MethodNameDescr = "The name of the substitution callback static method.";

	// Token: 0x04000865 RID: 2149
	public const string Substitution_CannotBeInCachedControl = "Substitution controls cannot be used in cached User Controls or cached Master Pages.";

	// Token: 0x04000866 RID: 2150
	public const string Substitution_BadMethodName = "Cannot find static method '{0}' matching HttpResponseSubstitutionCallback.";

	// Token: 0x04000867 RID: 2151
	public const string Substitution_NotAllowed = "Adding a Substitution control to a control collection is not permitted.";

	// Token: 0x04000868 RID: 2152
	public const string Substitution_SiteNotAllowed = "Setting the Site on a Substitution control is not permitted.";

	// Token: 0x04000869 RID: 2153
	public const string Table_SectionsMustBeInOrder = "The table {0} must contain row sections in order of header, body, then footer.";

	// Token: 0x0400086A RID: 2154
	public const string Table_BackImageUrl = "The background image of the table.";

	// Token: 0x0400086B RID: 2155
	public const string Table_Caption = "The caption associated with the table.";

	// Token: 0x0400086C RID: 2156
	public const string Table_CellSpacing = "The spacing between the table cells.";

	// Token: 0x0400086D RID: 2157
	public const string Table_CellPadding = "The padding within the table cells.";

	// Token: 0x0400086E RID: 2158
	public const string Table_GridLines = "Which grid lines to display between the table cells.";

	// Token: 0x0400086F RID: 2159
	public const string Table_HorizontalAlign = "The horizontal alignment of the table.";

	// Token: 0x04000870 RID: 2160
	public const string Table_Rows = "The collection of rows within the table.";

	// Token: 0x04000871 RID: 2161
	public const string TableCell_AssociatedHeaderCellNotFound = "The cell {0} listed as an associated header cell was not found.";

	// Token: 0x04000872 RID: 2162
	public const string TableCell_AssociatedHeaderCellID = "Lists the header cell IDs associated with the current table cell. This attribute is rendered with the HTML headers attribute.";

	// Token: 0x04000873 RID: 2163
	public const string TableCell_ColumnSpan = "The number of columns this cell spans.";

	// Token: 0x04000874 RID: 2164
	public const string TableCell_RowSpan = "The number of rows this cell spans.";

	// Token: 0x04000875 RID: 2165
	public const string TableCell_Text = "The text to be rendered within the cell.";

	// Token: 0x04000876 RID: 2166
	public const string TableCell_Wrap = "Whether the cell content should wrap or not.";

	// Token: 0x04000877 RID: 2167
	public const string TableHeaderCell_AbbreviatedText = "Sets the abbreviated text for a header cell. The abbreviated text is rendered with the HTML abbr attribute. The abbr attribute is important for screen readers since it allows them to read a shortened version of a header for each cell in the table.";

	// Token: 0x04000878 RID: 2168
	public const string TableHeaderCell_Scope = "Represents the cells that the header applies to. Renders the HTML SCOPE attribute. Possible values are from the TableHeaderScope enumeration: Column and Row.";

	// Token: 0x04000879 RID: 2169
	public const string TableHeaderCell_CategoryText = "Contains a list of categories associated with the table header (read by screen readers). The categories can be any string values. The categories are rendered as a comma delimited list using the HTML axis attribute.";

	// Token: 0x0400087A RID: 2170
	public const string TableItemStyle_Wrap = "Whether the cell content should wrap or not.";

	// Token: 0x0400087B RID: 2171
	public const string TableRow_Cells = "The collection of cells within the table row.";

	// Token: 0x0400087C RID: 2172
	public const string TableRow_TableSection = "The tablesection for the table row.";

	// Token: 0x0400087D RID: 2173
	public const string TableSectionStyle_Visible = "Determines whether the table section is visible.";

	// Token: 0x0400087E RID: 2174
	public const string TableStyle_BackImageUrl = "The background image within the table.";

	// Token: 0x0400087F RID: 2175
	public const string TableStyle_CellPadding = "The spacing within cells of the table.";

	// Token: 0x04000880 RID: 2176
	public const string TableStyle_CellSpacing = "The spacing between cells of the table.";

	// Token: 0x04000881 RID: 2177
	public const string TableStyle_GridLines = "The type of grid to be shown within the table.";

	// Token: 0x04000882 RID: 2178
	public const string TableStyle_InvalidCellSpacing = "CellSpacing must be greater than -1.";

	// Token: 0x04000883 RID: 2179
	public const string TableStyle_InvalidCellPadding = "CellPadding must be greater than -1.";

	// Token: 0x04000884 RID: 2180
	public const string TableStyle_HorizontalAlign = "The horizontal alignment of the table.";

	// Token: 0x04000885 RID: 2181
	public const string Control_Missing_Attribute = "The required attribute '{0}' is not found on '{1}' control.";

	// Token: 0x04000886 RID: 2182
	public const string TemplateColumn_EditItemTemplate = "The template to use for rows in edit mode in this column.";

	// Token: 0x04000887 RID: 2183
	public const string TemplateColumn_FooterTemplate = "The template to use for the footer in this column.";

	// Token: 0x04000888 RID: 2184
	public const string TemplateColumn_HeaderTemplate = "The template to use for the header in this column.";

	// Token: 0x04000889 RID: 2185
	public const string TemplateColumn_ItemTemplate = "The template to use for rows in this column.";

	// Token: 0x0400088A RID: 2186
	public const string TemplateField_AlternatingItemTemplate = "The template to use for alternating items in this field.";

	// Token: 0x0400088B RID: 2187
	public const string TemplateField_EditItemTemplate = "The template to use for items in edit mode in this field.";

	// Token: 0x0400088C RID: 2188
	public const string TemplateField_FooterTemplate = "The template to use for the footer in this field.";

	// Token: 0x0400088D RID: 2189
	public const string TemplateField_HeaderTemplate = "The template to use for the header in this field.";

	// Token: 0x0400088E RID: 2190
	public const string TemplateField_InsertItemTemplate = "The template to use for items in insert mode in this field.";

	// Token: 0x0400088F RID: 2191
	public const string TemplateField_ItemTemplate = "The template to use for items in this field.";

	// Token: 0x04000890 RID: 2192
	public const string TextBox_AutoCompleteType = "The type of input content used by client browsers for auto completion.";

	// Token: 0x04000891 RID: 2193
	public const string TextBox_AutoPostBack = "Automatically postback to the server after the text is modified.";

	// Token: 0x04000892 RID: 2194
	public const string TextBox_Columns = "The width of the textbox in characters.";

	// Token: 0x04000893 RID: 2195
	public const string TextBox_InvalidColumns = "Columns must be greater than -1.";

	// Token: 0x04000894 RID: 2196
	public const string TextBox_InvalidRows = "Rows must be greater than -1.";

	// Token: 0x04000895 RID: 2197
	public const string TextBox_MaxLength = "The maximum number of characters that can be entered.";

	// Token: 0x04000896 RID: 2198
	public const string TextBox_TextMode = "The behavior mode of the textbox.";

	// Token: 0x04000897 RID: 2199
	public const string TextBox_ReadOnly = "Whether the text in the control can be changed or not.";

	// Token: 0x04000898 RID: 2200
	public const string TextBox_Rows = "The number of lines to display for a multi-line textbox.";

	// Token: 0x04000899 RID: 2201
	public const string TextBox_Text = "The text value.";

	// Token: 0x0400089A RID: 2202
	public const string TextBox_Wrap = "Whether the text should wrap or not.";

	// Token: 0x0400089B RID: 2203
	public const string TextBox_OnTextChanged = "Fires when the text property has been changed.";

	// Token: 0x0400089C RID: 2204
	public const string TreeNodeStyle_ChildNodesPadding = "Gets and sets the vertical spacing between the node and its child nodes.";

	// Token: 0x0400089D RID: 2205
	public const string TreeNodeStyle_HorizontalPadding = "The amount of horizontal padding around the node text.";

	// Token: 0x0400089E RID: 2206
	public const string TreeNodeStyle_ImageUrl = "The URL of the image rendered on nodes.";

	// Token: 0x0400089F RID: 2207
	public const string TreeNodeStyle_NodeSpacing = "The amount of vertical spacing between nodes.";

	// Token: 0x040008A0 RID: 2208
	public const string TreeNodeStyle_VerticalPadding = "The amount of vertical padding around the node text.";

	// Token: 0x040008A1 RID: 2209
	public const string TreeNodeStyleCollection_InvalidArgument = "TreeNodeStyleCollection can only contain TreeNodeStyles.";

	// Token: 0x040008A2 RID: 2210
	public const string TreeNodeBinding_Depth = "The tree depth associated with the tree level object.";

	// Token: 0x040008A3 RID: 2211
	public const string TreeNodeBinding_EmptyBindingText = "(Empty)";

	// Token: 0x040008A4 RID: 2212
	public const string TreeNodeBinding_FormatString = "The format string to use when formatting the value retrieved from TextField, for example: \"Node {0}\".";

	// Token: 0x040008A5 RID: 2213
	public const string TreeNodeBinding_ImageToolTip = "The default image tooltip for all nodes in this level when data binding.";

	// Token: 0x040008A6 RID: 2214
	public const string TreeNodeBinding_ImageToolTipField = "The table column or XML attribute name to use for the node's ImageToolTip property when data binding.";

	// Token: 0x040008A7 RID: 2215
	public const string TreeNodeBinding_ImageUrl = "The default image URL for all nodes in this level when data binding.";

	// Token: 0x040008A8 RID: 2216
	public const string TreeNodeBinding_ImageUrlField = "The table column or XML attribute name to use for the node's ImageUrl property when data binding.";

	// Token: 0x040008A9 RID: 2217
	public const string TreeNodeBinding_NavigateUrl = "The default navigation URL for all nodes in this level when data binding.";

	// Token: 0x040008AA RID: 2218
	public const string TreeNodeBinding_NavigateUrlField = "The table column or XML attribute name to use for the node's NavigateUrl property when data binding.";

	// Token: 0x040008AB RID: 2219
	public const string TreeNodeBinding_PopulateOnDemand = "If set to true, the TreeView populates data at the hierarchy-level represented by the TreeNodeBinding object on-demand (when the node is expanded), either by calling a service directly from the client (if TreeView.PopulateNodesFromClient is set) or by causing a postback to the server.  If set to false, the TreeView populates node data all-at-once, when the page containing the TreeView is first requested.";

	// Token: 0x040008AC RID: 2220
	public const string TreeNodeBinding_SelectAction = "The default select action for all nodes in this level when data binding.";

	// Token: 0x040008AD RID: 2221
	public const string TreeNodeBinding_ShowCheckBox = "The default show checkbox state for all nodes in this level when data binding.";

	// Token: 0x040008AE RID: 2222
	public const string TreeNodeBinding_Target = "The default navigation target for all nodes in this level when data binding.";

	// Token: 0x040008AF RID: 2223
	public const string TreeNodeBinding_TargetField = "The table column or XML attribute name to use for the node's Target property when data binding.";

	// Token: 0x040008B0 RID: 2224
	public const string TreeNodeBinding_Text = "The default text for all nodes in this level when data binding.";

	// Token: 0x040008B1 RID: 2225
	public const string TreeNodeBinding_TextField = "The table column or XML attribute name to use for a node's Text property when data binding.";

	// Token: 0x040008B2 RID: 2226
	public const string TreeNodeBinding_ToolTip = "The default tooltip for all nodes in this level when data binding.";

	// Token: 0x040008B3 RID: 2227
	public const string TreeNodeBinding_ToolTipField = "The table column or XML attribute name to use for the node's ToolTip property when data binding.";

	// Token: 0x040008B4 RID: 2228
	public const string TreeNodeBinding_Value = "The default value for all nodes in this level when data binding.";

	// Token: 0x040008B5 RID: 2229
	public const string TreeNodeBinding_ValueField = "The table column or XML attribute name to use for an item's Value property when databinding. When binding to a data source, this is usually a unique key in the current level view.";

	// Token: 0x040008B6 RID: 2230
	public const string TreeNodeCollection_InvalidArrayType = "TreeNode[] expected.";

	// Token: 0x040008B7 RID: 2231
	public const string TreeNode_Checked = "The checked state of the tree node.";

	// Token: 0x040008B8 RID: 2232
	public const string TreeView_DataSourceReturnedNullView = "The IHierarchicalDataSource that is the data source for TreeView '{0}' returned a null view for the given view path.";

	// Token: 0x040008B9 RID: 2233
	public const string TreeNode_Expanded = "The expand state of the tree node.";

	// Token: 0x040008BA RID: 2234
	public const string TreeNode_ImageToolTip = "The tooltip of the image associated with the tree node.";

	// Token: 0x040008BB RID: 2235
	public const string TreeNode_ImageUrl = "The URL for the image for the tree node.";

	// Token: 0x040008BC RID: 2236
	public const string TreeView_InvalidDataBinding = "Could not bind to the '{0}' property (specified by {1}) while data binding TreeView.  Please check the Bindings fields.";

	// Token: 0x040008BD RID: 2237
	public const string TreeNode_NavigateUrl = "The URL to which the tree node navigates when selected.";

	// Token: 0x040008BE RID: 2238
	public const string TreeNode_PopulateOnDemand = "Whether the node should populate its child nodes on demand.";

	// Token: 0x040008BF RID: 2239
	public const string TreeView_PopulateOnlyForDataSourceControls = "PopulateOnDemand only supported when binding the TreeView to a data source control using the DataSourceID property or when the TreeNodePopulate event is handled.";

	// Token: 0x040008C0 RID: 2240
	public const string TreeView_PopulateOnlyEmptyNodes = "PopulateOnDemand can't be set to true on a node that already has children.";

	// Token: 0x040008C1 RID: 2241
	public const string TreeNode_Selected = "The select state of the tree node.";

	// Token: 0x040008C2 RID: 2242
	public const string TreeNode_SelectAction = "The action that the node takes when selected.";

	// Token: 0x040008C3 RID: 2243
	public const string TreeNode_ShowCheckBox = "Whether the node should show its checkbox.";

	// Token: 0x040008C4 RID: 2244
	public const string TreeNode_Target = "The navigate target used when the node is selected.";

	// Token: 0x040008C5 RID: 2245
	public const string TreeNode_Text = "The display text of the tree node.";

	// Token: 0x040008C6 RID: 2246
	public const string TreeNode_ToolTip = "The tooltip of the tree node.";

	// Token: 0x040008C7 RID: 2247
	public const string TreeNode_Value = "The value of the tree node.";

	// Token: 0x040008C8 RID: 2248
	public const string TreeView_AutoGenerateDataBindings = "Whether the tree will automatically bind to data.";

	// Token: 0x040008C9 RID: 2249
	public const string TreeView_DataBindings = "The data bindings for nodes in the tree.";

	// Token: 0x040008CA RID: 2250
	public const string TreeView_CollapseImageToolTip = "The tooltip format string of the image that collapses a node when clicked.";

	// Token: 0x040008CB RID: 2251
	public const string TreeView_CollapseImageToolTipDefaultValue = "Collapse {0}";

	// Token: 0x040008CC RID: 2252
	public const string TreeView_CollapseImageUrl = "The URL of the image that collapses a node when clicked.";

	// Token: 0x040008CD RID: 2253
	public const string TreeView_Default_SkipLinkText = "Skip Navigation Links.";

	// Token: 0x040008CE RID: 2254
	public const string TreeView_EnableClientScript = "Whether the TreeView should try to use client-script.";

	// Token: 0x040008CF RID: 2255
	public const string TreeView_ExpandImageToolTip = "The tooltip format string of the image that expands a node when clicked.";

	// Token: 0x040008D0 RID: 2256
	public const string TreeView_ExpandImageToolTipDefaultValue = "Expand {0}";

	// Token: 0x040008D1 RID: 2257
	public const string TreeView_ExpandImageUrl = "The URL to the image that expands a node when clicked.";

	// Token: 0x040008D2 RID: 2258
	public const string TreeView_HoverNodeStyle = "The style that gets applied when the mouse hovers over a node (only available when client script is enabled).";

	// Token: 0x040008D3 RID: 2259
	public const string TreeView_ExpandDepth = "When data binding, how many levels of the tree to expand by default.";

	// Token: 0x040008D4 RID: 2260
	public const string TreeView_ImageSet = "Whether to use the packaged images or those specified in the properties of TreeView.";

	// Token: 0x040008D5 RID: 2261
	public const string TreeView_LeafNodeStyle = "The style applied to leaf nodes.";

	// Token: 0x040008D6 RID: 2262
	public const string TreeView_LevelStyles = "The tree styles to be applied at each level of the tree.";

	// Token: 0x040008D7 RID: 2263
	public const string TreeView_LineImagesFolderUrl = "The relative folder that contains the line images for the TreeView.";

	// Token: 0x040008D8 RID: 2264
	public const string TreeView_MaxDataBindDepth = "The maximum depth to which the TreeView will databind.";

	// Token: 0x040008D9 RID: 2265
	public const string TreeView_NoExpandImageUrl = "The URL of the image that represents the absence of an expand/collapse icon.";

	// Token: 0x040008DA RID: 2266
	public const string TreeView_NodeIndent = "The number of pixels to indent each node.";

	// Token: 0x040008DB RID: 2267
	public const string TreeView_Nodes = "The initial collection of nodes for the tree.";

	// Token: 0x040008DC RID: 2268
	public const string TreeView_NodeStyle = "The default style applied to all nodes.";

	// Token: 0x040008DD RID: 2269
	public const string TreeView_NodeWrap = "Whether the node text should be word wrapped.";

	// Token: 0x040008DE RID: 2270
	public const string TreeView_ParentNodeStyle = "The style applied to parent nodes (excluding root nodes).";

	// Token: 0x040008DF RID: 2271
	public const string TreeView_PathSeparator = "The character used to separate parts of the path.";

	// Token: 0x040008E0 RID: 2272
	public const string TreeView_PopulateNodesFromClient = "Whether the TreeView should try to populate nodes from the client (without posting back).";

	// Token: 0x040008E1 RID: 2273
	public const string TreeView_RootNodeStyle = "The style applied to root nodes.";

	// Token: 0x040008E2 RID: 2274
	public const string TreeView_SelectedNodeStyle = "The style applied to the selected node.";

	// Token: 0x040008E3 RID: 2275
	public const string TreeView_ShowCheckBoxes = "The node types next to which checkboxes should be shown.";

	// Token: 0x040008E4 RID: 2276
	public const string TreeView_ShowExpandCollapse = "Whether to show the expand/collapse icon next to the nodes.";

	// Token: 0x040008E5 RID: 2277
	public const string TreeView_ShowLines = "Whether to show lines connecting the tree nodes.";

	// Token: 0x040008E6 RID: 2278
	public const string TreeView_SkipLinkText = "The text that appears in the ALT attribute of the invisible image link that allows screen readers to skip repetitive content.";

	// Token: 0x040008E7 RID: 2279
	public const string TreeView_CheckChanged = "Fired after the check state of a node changes.";

	// Token: 0x040008E8 RID: 2280
	public const string TreeView_SelectedNodeChanged = "Fired after the selected node changes.";

	// Token: 0x040008E9 RID: 2281
	public const string TreeView_TreeNodeCollapsed = "Fired after a TreeNode is collapsed.";

	// Token: 0x040008EA RID: 2282
	public const string TreeView_TreeNodeExpanded = "Fired after a TreeNode is expanded.";

	// Token: 0x040008EB RID: 2283
	public const string TreeView_TreeNodeDataBound = "Fired after a TreeNode is databound.";

	// Token: 0x040008EC RID: 2284
	public const string TreeView_TreeNodePopulate = "Fired when a TreeNode is being populated.";

	// Token: 0x040008ED RID: 2285
	public const string ValidationSummary_DisplayMode = "Format for error summary display.";

	// Token: 0x040008EE RID: 2286
	public const string ValidationSummary_HeaderText = "Header text to display in the summary.";

	// Token: 0x040008EF RID: 2287
	public const string ValidationSummary_ShowMessageBox = "Whether to display a message box on error in up-level browsers.";

	// Token: 0x040008F0 RID: 2288
	public const string ValidationSummary_ShowModelStateErrors = "Whether the model state errors from a data operation should be shown.";

	// Token: 0x040008F1 RID: 2289
	public const string ValidationSummary_ShowSummary = "Whether to display the summary text on the page.";

	// Token: 0x040008F2 RID: 2290
	public const string ValidationSummary_ShowValidationErrors = "Whether the validation summary from validators should be shown.";

	// Token: 0x040008F3 RID: 2291
	public const string ValidationSummary_EnableClientScript = "Whether to update the summary on the client in up-level browsers.";

	// Token: 0x040008F4 RID: 2292
	public const string ValidationSummary_ValidationGroup = "The group to which the validation summary belongs.";

	// Token: 0x040008F5 RID: 2293
	public const string PostBackControl_ValidationGroup = "The group that should be validated when the control causes a postback.";

	// Token: 0x040008F6 RID: 2294
	public const string AutoPostBackControl_CausesValidation = "Whether the control causes validation to fire.";

	// Token: 0x040008F7 RID: 2295
	public const string Calendar_Caption = "The caption associated with the calendar.";

	// Token: 0x040008F8 RID: 2296
	public const string Calendar_CellPadding = "The padding within cells.";

	// Token: 0x040008F9 RID: 2297
	public const string Calendar_CellSpacing = "The spacing between cells.";

	// Token: 0x040008FA RID: 2298
	public const string Calendar_DayHeaderStyle = "The style applied to the day header row.";

	// Token: 0x040008FB RID: 2299
	public const string Calendar_DayNameFormat = "Format for day header text.";

	// Token: 0x040008FC RID: 2300
	public const string Calendar_DayStyle = "The style applied to days.";

	// Token: 0x040008FD RID: 2301
	public const string Calendar_FirstDayOfWeek = "Which day of the week is displayed first.";

	// Token: 0x040008FE RID: 2302
	public const string Calendar_NextMonthText = "Text for the next month button.";

	// Token: 0x040008FF RID: 2303
	public const string Calendar_NextPrevFormat = "Format for month navigation buttons.";

	// Token: 0x04000900 RID: 2304
	public const string Calendar_NextPrevStyle = "The style applied to month navigation buttons.";

	// Token: 0x04000901 RID: 2305
	public const string Calendar_OtherMonthDayStyle = "The style applied to days from adjacent months";

	// Token: 0x04000902 RID: 2306
	public const string Calendar_PrevMonthText = "Text for the previous month button";

	// Token: 0x04000903 RID: 2307
	public const string Calendar_SelectedDate = "The currently selected date.";

	// Token: 0x04000904 RID: 2308
	public const string Calendar_SelectedDates = "The set of selected dates for use when range selection is enabled.";

	// Token: 0x04000905 RID: 2309
	public const string Calendar_SelectedDayStyle = "The style of currently selected days.";

	// Token: 0x04000906 RID: 2310
	public const string Calendar_SelectionMode = "Determines whether days, weeks and months are selectable.";

	// Token: 0x04000907 RID: 2311
	public const string Calendar_SelectMonthText = "Text for the select month button.";

	// Token: 0x04000908 RID: 2312
	public const string Calendar_SelectorStyle = "The style applied to the week and month selector column.";

	// Token: 0x04000909 RID: 2313
	public const string Calendar_SelectWeekText = "Text for select week button.";

	// Token: 0x0400090A RID: 2314
	public const string Calendar_ShowDayHeader = "True if showing days of week header.";

	// Token: 0x0400090B RID: 2315
	public const string Calendar_ShowGridLines = "True if showing grid lines.";

	// Token: 0x0400090C RID: 2316
	public const string Calendar_ShowNextPrevMonth = "True if showing the next/previous month buttons.";

	// Token: 0x0400090D RID: 2317
	public const string Calendar_ShowTitle = "True if showing the title.";

	// Token: 0x0400090E RID: 2318
	public const string Calendar_TitleFormat = "Format for month title in header.";

	// Token: 0x0400090F RID: 2319
	public const string Calendar_TitleStyle = "The style applied to the title.";

	// Token: 0x04000910 RID: 2320
	public const string Calendar_TodayDayStyle = "The style applied to today's date.";

	// Token: 0x04000911 RID: 2321
	public const string Calendar_TodaysDate = "The current date as displayed by the Calendar.";

	// Token: 0x04000912 RID: 2322
	public const string Calendar_VisibleDate = "The month to be displayed.";

	// Token: 0x04000913 RID: 2323
	public const string Calendar_WeekendDayStyle = "The style applied to weekend days.";

	// Token: 0x04000914 RID: 2324
	public const string Calendar_OnDayRender = "Fires as a day is being rendered.";

	// Token: 0x04000915 RID: 2325
	public const string Calendar_OnSelectionChanged = "Fires when selection is changed by the user.";

	// Token: 0x04000916 RID: 2326
	public const string Calendar_OnVisibleMonthChanged = "Fires when visible month is changed by the user.";

	// Token: 0x04000917 RID: 2327
	public const string Calendar_TitleText = "Calendar";

	// Token: 0x04000918 RID: 2328
	public const string Calendar_PreviousMonthTitle = "Go to the previous month";

	// Token: 0x04000919 RID: 2329
	public const string Calendar_NextMonthTitle = "Go to the next month";

	// Token: 0x0400091A RID: 2330
	public const string Calendar_SelectMonthTitle = "Select the whole month";

	// Token: 0x0400091B RID: 2331
	public const string Calendar_SelectWeekTitle = "Select week {0}";

	// Token: 0x0400091C RID: 2332
	public const string View_Activate = "Fires when the view control is activated.";

	// Token: 0x0400091D RID: 2333
	public const string View_Deactivate = "Fires when the view control is deactivated.";

	// Token: 0x0400091E RID: 2334
	public const string ViewCollection_must_contain_view = "Controls added to a ViewCollection must be of type View.";

	// Token: 0x0400091F RID: 2335
	public const string WebControl_AccessKey = "Keyboard shortcut used by the control.";

	// Token: 0x04000920 RID: 2336
	public const string WebControl_InvalidAccessKey = "AccessKey too long, cannot be more than one character.";

	// Token: 0x04000921 RID: 2337
	public const string WebControl_Attributes = "Tag attributes of the control.";

	// Token: 0x04000922 RID: 2338
	public const string WebControl_BackColor = "Color of the background of the control.";

	// Token: 0x04000923 RID: 2339
	public const string WebControl_BorderColor = "Color of the border around the control.";

	// Token: 0x04000924 RID: 2340
	public const string WebControl_BorderWidth = "Width of the border around the control.";

	// Token: 0x04000925 RID: 2341
	public const string WebControl_BorderStyle = "Style of the border around the control.";

	// Token: 0x04000926 RID: 2342
	public const string WebControl_CSSClassName = "CSS Class name applied to the control.";

	// Token: 0x04000927 RID: 2343
	public const string WebControl_ControlStyle = "The style associated with the control.";

	// Token: 0x04000928 RID: 2344
	public const string WebControl_ControlStyleCreated = "Whether the style associated with the control has been created.";

	// Token: 0x04000929 RID: 2345
	public const string WebControl_Enabled = "Enabled state of the control.";

	// Token: 0x0400092A RID: 2346
	public const string WebControl_Font = "The font used for text within the control.";

	// Token: 0x0400092B RID: 2347
	public const string WebControl_ForeColor = "Color of the text within the control.";

	// Token: 0x0400092C RID: 2348
	public const string WebControl_Height = "The height of the control.";

	// Token: 0x0400092D RID: 2349
	public const string WebControl_Style = "Low-level access to control styles.";

	// Token: 0x0400092E RID: 2350
	public const string WebControl_TabIndex = "The tab order of the control.";

	// Token: 0x0400092F RID: 2351
	public const string WebControl_Tooltip = "The tooltip displayed when the mouse is over the control.";

	// Token: 0x04000930 RID: 2352
	public const string WebControl_Width = "The width of the control.";

	// Token: 0x04000931 RID: 2353
	public const string Wizard_ActiveStep = "The active WizardStep control.";

	// Token: 0x04000932 RID: 2354
	public const string Wizard_ActiveStepIndex = "The index of the active WizardStep control.";

	// Token: 0x04000933 RID: 2355
	public const string Wizard_ActiveStepIndex_out_of_range = "The ActiveStepIndex must be less than WizardSteps.Count and at least -1.  For dynamically added steps, make sure they are added before or in Page_PreInit event.";

	// Token: 0x04000934 RID: 2356
	public const string Wizard_CancelButtonClick = "Fires when the cancel button is clicked.";

	// Token: 0x04000935 RID: 2357
	public const string Wizard_CancelButtonImageUrl = "The URL for the image to be used for the cancel button.";

	// Token: 0x04000936 RID: 2358
	public const string Wizard_CancelButtonText = "The text of the cancel button.";

	// Token: 0x04000937 RID: 2359
	public const string Wizard_CancelButtonType = "The button type of the cancel button.";

	// Token: 0x04000938 RID: 2360
	public const string Wizard_CancelButtonStyle = "The style of the cancel button.";

	// Token: 0x04000939 RID: 2361
	public const string Wizard_CancelDestinationPageUrl = "The URL to redirect to when the cancel button is clicked.";

	// Token: 0x0400093A RID: 2362
	public const string Wizard_CellPadding = "The padding within cells.";

	// Token: 0x0400093B RID: 2363
	public const string Wizard_CellSpacing = "The spacing between cells.";

	// Token: 0x0400093C RID: 2364
	public const string Wizard_Default_CancelButtonText = "Cancel";

	// Token: 0x0400093D RID: 2365
	public const string Wizard_DisplayCancelButton = "Indicates whether cancel button is displayed.";

	// Token: 0x0400093E RID: 2366
	public const string Wizard_FinishDestinationPageUrl = "The URL to redirect to when the finish button is clicked.";

	// Token: 0x0400093F RID: 2367
	public const string Wizard_FinishCompleteButtonStyle = "The style of the finish step button.";

	// Token: 0x04000940 RID: 2368
	public const string Wizard_FinishCompleteButtonText = "The text of the finish step button.";

	// Token: 0x04000941 RID: 2369
	public const string Wizard_FinishCompleteButtonType = "The button type of the finish step button.";

	// Token: 0x04000942 RID: 2370
	public const string Wizard_FinishCompleteButtonImageUrl = "The URL for the image to be used for the finish button.";

	// Token: 0x04000943 RID: 2371
	public const string Wizard_FinishPreviousButtonStyle = "The style of the finish step's previous button.";

	// Token: 0x04000944 RID: 2372
	public const string Wizard_FinishPreviousButtonText = "The text of the finish step's previous button.";

	// Token: 0x04000945 RID: 2373
	public const string Wizard_FinishPreviousButtonType = "The button type of the finish step's previous button.";

	// Token: 0x04000946 RID: 2374
	public const string Wizard_FinishPreviousButtonImageUrl = "The URL for the image to be used for the finish step's previous button";

	// Token: 0x04000947 RID: 2375
	public const string Wizard_FinishNavigationTemplate = "The template used for finish navigation layout.";

	// Token: 0x04000948 RID: 2376
	public const string Wizard_InvalidBubbleEvent = "The command '{0}' is not valid for the previous step, make sure the step type is not changed between postbacks.";

	// Token: 0x04000949 RID: 2377
	public const string Wizard_NavigationButtonStyle = "The style of the navigation buttons.";

	// Token: 0x0400094A RID: 2378
	public const string Wizard_NavigationStyle = "The style applied to the navigation layout.";

	// Token: 0x0400094B RID: 2379
	public const string Wizard_StepNextButtonStyle = "The style of the next step button.";

	// Token: 0x0400094C RID: 2380
	public const string Wizard_StepNextButtonText = "The text of the next step button.";

	// Token: 0x0400094D RID: 2381
	public const string Wizard_StepNextButtonType = "The button type of the next step button.";

	// Token: 0x0400094E RID: 2382
	public const string Wizard_StepNextButtonImageUrl = "The URL for the image to be used for the next button.";

	// Token: 0x0400094F RID: 2383
	public const string Wizard_StepPreviousButtonStyle = "The style of the previous step button.";

	// Token: 0x04000950 RID: 2384
	public const string Wizard_StepPreviousButtonText = "The text of the previous step button.";

	// Token: 0x04000951 RID: 2385
	public const string Wizard_StepPreviousButtonType = "The button type of the previous step button.";

	// Token: 0x04000952 RID: 2386
	public const string Wizard_StepPreviousButtonImageUrl = "The URL for the image to be used for the previous button.";

	// Token: 0x04000953 RID: 2387
	public const string Wizard_SideBarButtonStyle = "The style of the side bar buttons.";

	// Token: 0x04000954 RID: 2388
	public const string Wizard_DisplaySideBar = "Indicates whether sidebar is displayed.";

	// Token: 0x04000955 RID: 2389
	public const string Wizard_SideBarStyle = "The style applied to the side bar.";

	// Token: 0x04000956 RID: 2390
	public const string Wizard_SideBarTemplate = "The template used for the side bar layout.";

	// Token: 0x04000957 RID: 2391
	public const string Wizard_StartNavigationTemplate = "The template used for the start navigation layout.";

	// Token: 0x04000958 RID: 2392
	public const string Wizard_StartNextButtonStyle = "The style of the start step's next button.";

	// Token: 0x04000959 RID: 2393
	public const string Wizard_StartNextButtonText = "The text of the start step's next button.";

	// Token: 0x0400095A RID: 2394
	public const string Wizard_StartNextButtonType = "The type of the start step's next button.";

	// Token: 0x0400095B RID: 2395
	public const string Wizard_StartNextButtonImageUrl = "The URL for the image to be used for the start step's next button.";

	// Token: 0x0400095C RID: 2396
	public const string Wizard_Step_Not_In_Wizard = "The step cannot be found in Wizard's WizardSteps collection.";

	// Token: 0x0400095D RID: 2397
	public const string Wizard_StepNavigationTemplate = "The template used for the step navigation layout.";

	// Token: 0x0400095E RID: 2398
	public const string Wizard_StepStyle = "The style applied to the steps.";

	// Token: 0x0400095F RID: 2399
	public const string Wizard_WizardSteps = "The collection of the WizardStep controls inside the control.";

	// Token: 0x04000960 RID: 2400
	public const string Wizard_HeaderText = "The header text of wizard control.";

	// Token: 0x04000961 RID: 2401
	public const string Wizard_Default_SkipToContentText = "Skip Navigation Links.";

	// Token: 0x04000962 RID: 2402
	public const string Wizard_ActiveStepChanged = "Fires when the active step is changed.";

	// Token: 0x04000963 RID: 2403
	public const string Wizard_FinishButtonClick = "Fires when the finish button is clicked.";

	// Token: 0x04000964 RID: 2404
	public const string Wizard_NextButtonClick = "Fires when the next button is clicked.";

	// Token: 0x04000965 RID: 2405
	public const string Wizard_PreviousButtonClick = "Fires when the previous button is clicked.";

	// Token: 0x04000966 RID: 2406
	public const string Wizard_SideBarButtonClick = "Fires when the sidebar button is clicked.";

	// Token: 0x04000967 RID: 2407
	public const string Wizard_Default_StepPreviousButtonText = "Previous";

	// Token: 0x04000968 RID: 2408
	public const string Wizard_Default_StepNextButtonText = "Next";

	// Token: 0x04000969 RID: 2409
	public const string Wizard_Default_FinishButtonText = "Finish";

	// Token: 0x0400096A RID: 2410
	public const string Wizard_SideBar_Button_Not_Found = "{0} control must contain an IButtonControl with ID {1} in every item template, this maybe include ItemTemplate, EditItemTemplate, SelectedItemTemplate or AlternatingItemTemplate if they exist.";

	// Token: 0x0400096B RID: 2411
	public const string Wizard_DataList_Not_Found = "SideBarTemplate must contain a ListView control or a DataList control with ID {0} to enable the side bar navigation feature.";

	// Token: 0x0400096C RID: 2412
	public const string Wizard_Cannot_Modify_ControlCollection = "The Control collection cannot be modified.";

	// Token: 0x0400096D RID: 2413
	public const string Wizard_Header_Placeholder_Must_Be_Specified_For_HeaderTemplate = "A header placeholder must be specified on Wizard '{0}' when HeaderTemplate is set. Specify a placeholder by setting a control's ID property to \"{1}\". The placeholder control must also specify runat=\"server\".";

	// Token: 0x0400096E RID: 2414
	public const string Wizard_Header_Placeholder_Must_Be_Specified_For_HeaderText = "A header placeholder must be specified on Wizard '{0}' when HeaderText is set. Specify a placeholder by setting a control's ID property to \"{1}\". The placeholder control must also specify runat=\"server\".";

	// Token: 0x0400096F RID: 2415
	public const string Wizard_Navigation_Placeholder_Must_Be_Specified = "A navigation placeholder must be specified on Wizard '{0}'. Specify a placeholder by setting a control's ID property to \"{1}\". The placeholder control must also specify runat=\"server\".";

	// Token: 0x04000970 RID: 2416
	public const string Wizard_Sidebar_Placeholder_Must_Be_Specified = "A sidebar placeholder must be specified on Wizard '{0}' when DisplaySideBar is set to true. Specify a placeholder by setting a control's ID property to \"{1}\". The placeholder control must also specify runat=\"server\".";

	// Token: 0x04000971 RID: 2417
	public const string Wizard_Step_Placeholder_Must_Be_Specified = "A step placeholder must be specified on Wizard '{0}'. Specify a placeholder by setting a control's ID property to \"{1}\". The placeholder control must also specify runat=\"server\".";

	// Token: 0x04000972 RID: 2418
	public const string Wizard_LayoutTemplate = "The template used for a customized layout.";

	// Token: 0x04000973 RID: 2419
	public const string Wizard_WizardStepOnly = "Only WizardStep can be added to WizardControlCollection.";

	// Token: 0x04000974 RID: 2420
	public const string WizardStep_AllowReturn = "Determines whether the step can be visited more than once.";

	// Token: 0x04000975 RID: 2421
	public const string WizardStep_Name = "The name of wizard step.";

	// Token: 0x04000976 RID: 2422
	public const string WizardStep_Title = "The title of wizard step.";

	// Token: 0x04000977 RID: 2423
	public const string WizardStep_StepType = "The type of wizard step.";

	// Token: 0x04000978 RID: 2424
	public const string WizardStep_WrongContainment = "WizardStep can only be placed inside the <WizardSteps> tag of a Wizard control.";

	// Token: 0x04000979 RID: 2425
	public const string Xml_DocumentContent = "The XML string that the transform is applied to.";

	// Token: 0x0400097A RID: 2426
	public const string Xml_DocumentSource = "The XML file that the transform is applied to.";

	// Token: 0x0400097B RID: 2427
	public const string Xml_TransformSource = "The XSL file used to transform the XML data.";

	// Token: 0x0400097C RID: 2428
	public const string Xml_Document = "The XML document that the transform is applied to.";

	// Token: 0x0400097D RID: 2429
	public const string Xml_Transform = "The XSL transform used on the XML data.";

	// Token: 0x0400097E RID: 2430
	public const string Xml_TransformArgumentList = "The argument list used by the XSL stylesheet.";

	// Token: 0x0400097F RID: 2431
	public const string Xml_XPathNavigator = "The XPathNavigator that the transform is applied to.";

	// Token: 0x04000980 RID: 2432
	public const string XmlDataSource_Data = "Inline XML data. This is only used if no file is specified in the DataFile property.";

	// Token: 0x04000981 RID: 2433
	public const string XmlDataSource_DataFile = "The path to an XML data file.";

	// Token: 0x04000982 RID: 2434
	public const string XmlDataSource_Transform = "Inline XML transform. This is only used if no file is specified in the TransformFile property.";

	// Token: 0x04000983 RID: 2435
	public const string XmlDataSource_TransformFile = "The path to an XML transform file.";

	// Token: 0x04000984 RID: 2436
	public const string XmlDataSource_XPath = "Specifies an initial XPath that is applied to the XML data.";

	// Token: 0x04000985 RID: 2437
	public const string XmlDataSource_Transforming = "This event is raised before the transform is applied.";

	// Token: 0x04000986 RID: 2438
	public const string AppearanceEditorPart_Title = "Title";

	// Token: 0x04000987 RID: 2439
	public const string AppearanceEditorPart_Height = "Height";

	// Token: 0x04000988 RID: 2440
	public const string AppearanceEditorPart_Width = "Width";

	// Token: 0x04000989 RID: 2441
	public const string AppearanceEditorPart_ChromeType = "Chrome Type";

	// Token: 0x0400098A RID: 2442
	public const string AppearanceEditorPart_Hidden = "Hidden";

	// Token: 0x0400098B RID: 2443
	public const string AppearanceEditorPart_Direction = "Direction";

	// Token: 0x0400098C RID: 2444
	public const string AppearanceEditorPart_PartTitle = "Appearance";

	// Token: 0x0400098D RID: 2445
	public const string AppearanceEditorPart_Pixels = "pixels";

	// Token: 0x0400098E RID: 2446
	public const string AppearanceEditorPart_Points = "points";

	// Token: 0x0400098F RID: 2447
	public const string AppearanceEditorPart_Picas = "picas";

	// Token: 0x04000990 RID: 2448
	public const string AppearanceEditorPart_Inches = "inches";

	// Token: 0x04000991 RID: 2449
	public const string AppearanceEditorPart_Millimeters = "millimeters";

	// Token: 0x04000992 RID: 2450
	public const string AppearanceEditorPart_Centimeters = "centimeters";

	// Token: 0x04000993 RID: 2451
	public const string AppearanceEditorPart_Percent = "percent";

	// Token: 0x04000994 RID: 2452
	public const string AppearanceEditorPart_Em = "em";

	// Token: 0x04000995 RID: 2453
	public const string AppearanceEditorPart_Ex = "ex";

	// Token: 0x04000996 RID: 2454
	public const string BehaviorEditorPart_AllowClose = "Allow Close";

	// Token: 0x04000997 RID: 2455
	public const string BehaviorEditorPart_AllowConnect = "Allow Connect";

	// Token: 0x04000998 RID: 2456
	public const string BehaviorEditorPart_AllowHide = "Allow Hide";

	// Token: 0x04000999 RID: 2457
	public const string BehaviorEditorPart_AllowMinimize = "Allow Minimize";

	// Token: 0x0400099A RID: 2458
	public const string BehaviorEditorPart_AllowZoneChange = "Allow Zone Change";

	// Token: 0x0400099B RID: 2459
	public const string BehaviorEditorPart_ExportMode = "Export Mode";

	// Token: 0x0400099C RID: 2460
	public const string BehaviorEditorPart_ExportModeNone = "Do not allow";

	// Token: 0x0400099D RID: 2461
	public const string BehaviorEditorPart_ExportModeAll = "Export all data";

	// Token: 0x0400099E RID: 2462
	public const string BehaviorEditorPart_ExportModeNonSensitiveData = "Non-sensitive data only";

	// Token: 0x0400099F RID: 2463
	public const string BehaviorEditorPart_HelpMode = "Help Mode";

	// Token: 0x040009A0 RID: 2464
	public const string BehaviorEditorPart_HelpModeModal = "Modal";

	// Token: 0x040009A1 RID: 2465
	public const string BehaviorEditorPart_HelpModeModeless = "Modeless";

	// Token: 0x040009A2 RID: 2466
	public const string BehaviorEditorPart_HelpModeNavigate = "Navigate";

	// Token: 0x040009A3 RID: 2467
	public const string BehaviorEditorPart_Description = "Description";

	// Token: 0x040009A4 RID: 2468
	public const string BehaviorEditorPart_TitleLink = "Title Link";

	// Token: 0x040009A5 RID: 2469
	public const string BehaviorEditorPart_TitleIconImageLink = "Title Icon Image Link";

	// Token: 0x040009A6 RID: 2470
	public const string BehaviorEditorPart_CatalogIconImageLink = "Catalog Icon Image Link";

	// Token: 0x040009A7 RID: 2471
	public const string BehaviorEditorPart_HelpLink = "Help Link";

	// Token: 0x040009A8 RID: 2472
	public const string BehaviorEditorPart_ImportErrorMessage = "Import Error Message";

	// Token: 0x040009A9 RID: 2473
	public const string BehaviorEditorPart_AuthorizationFilter = "Authorization Filter";

	// Token: 0x040009AA RID: 2474
	public const string BehaviorEditorPart_AllowEdit = "Allow Edit";

	// Token: 0x040009AB RID: 2475
	public const string BehaviorEditorPart_PartTitle = "Behavior";

	// Token: 0x040009AC RID: 2476
	public const string BlobPersonalizationState_CantApply = "Cannot apply personalization data to '{0}', because personalization data was already applied to a control with this ID.";

	// Token: 0x040009AD RID: 2477
	public const string BlobPersonalizationState_CantExtract = "Cannot extract personalization data for '{0}', because personalization data was never applied to this control.  Ensure that the control's ID has not changed since personalization data was applied.";

	// Token: 0x040009AE RID: 2478
	public const string BlobPersonalizationState_DeserializeError = "Cannot deserialize the blob of personalization data associated with the current page.";

	// Token: 0x040009AF RID: 2479
	public const string BlobPersonalizationState_NotApplied = "Personalization data has not been applied.";

	// Token: 0x040009B0 RID: 2480
	public const string BlobPersonalizationState_NotLoaded = "Personalization data has not been loaded.";

	// Token: 0x040009B1 RID: 2481
	public const string CatalogPart_MustBeInZone = "CatalogPart '{0}' must be placed in a CatalogZone.";

	// Token: 0x040009B2 RID: 2482
	public const string CatalogPart_SampleWebPartTitle = "WebPart {0}";

	// Token: 0x040009B3 RID: 2483
	public const string CatalogPart_UnknownDescription = "Unknown description.";

	// Token: 0x040009B4 RID: 2484
	public const string CatalogZone_OnlyCatalogParts = "Should only have catalog parts in the ZoneTemplate of '{0}'.";

	// Token: 0x040009B5 RID: 2485
	public const string CatalogZoneBase_AddVerb = "Verb to add a Web Part to a Zone.";

	// Token: 0x040009B6 RID: 2486
	public const string CatalogZoneBase_CloseVerb = "Verb to close the CatalogZone.";

	// Token: 0x040009B7 RID: 2487
	public const string CatalogZoneBase_DefaultEmptyZoneText = "Catalog Zone contains no Catalog Parts.";

	// Token: 0x040009B8 RID: 2488
	public const string CatalogZoneBase_DefaultSelectTargetZoneText = "Add to:";

	// Token: 0x040009B9 RID: 2489
	public const string CatalogZoneBase_HeaderText = "Catalog Zone";

	// Token: 0x040009BA RID: 2490
	public const string CatalogZoneBase_InstructionText = "Select the catalog you would like to browse.";

	// Token: 0x040009BB RID: 2491
	public const string CatalogZoneBase_NoCatalogPartID = "CatalogPart does not have an ID.";

	// Token: 0x040009BC RID: 2492
	public const string CatalogZoneBase_PartLinkStyle = "The style applied to each link to select a CatalogPart.";

	// Token: 0x040009BD RID: 2493
	public const string CatalogZoneBase_SelectCatalogPart = "Selects '{0}'";

	// Token: 0x040009BE RID: 2494
	public const string CatalogZoneBase_SelectedCatalogPartID = "ID of the selected CatalogPart.";

	// Token: 0x040009BF RID: 2495
	public const string CatalogZoneBase_SelectedPartLinkStyle = "The style applied to the link to the currently selected CatalogPart.";

	// Token: 0x040009C0 RID: 2496
	public const string CatalogZoneBase_SelectTargetZoneText = "The text shown next to the dropdown for selecting the target Zone.";

	// Token: 0x040009C1 RID: 2497
	public const string CatalogZoneBase_ShowCatalogIcons = "Whether an icon should be displayed next to each item in a CatalogPart.";

	// Token: 0x040009C2 RID: 2498
	public const string ConnectionConsumerAttribute_InvalidConnectionPointType = "Type '{0}' is not a valid consumer connection point.  It must be public, a subclass of ConsumerConnectionPoint, and have a public constructor with the same parameters as the ConsumerConnectionPoint constructor.";

	// Token: 0x040009C3 RID: 2499
	public const string ConnectionProviderAttribute_InvalidConnectionPointType = "Type '{0}' is not a valid provider connection point.  It must be public, a subclass of ProviderConnectionPoint, and have a public constructor with the same parameters as the ProviderConnectionPoint constructor.";

	// Token: 0x040009C4 RID: 2500
	public const string ConnectionsZone_CancelVerb = "Verb to cancel the current action.";

	// Token: 0x040009C5 RID: 2501
	public const string ConnectionsZone_ConfigureConnectionTitle = "Configure Connection";

	// Token: 0x040009C6 RID: 2502
	public const string ConnectionsZone_ConfigureConnectionTitleDescription = "The title for the connection configuration mode.";

	// Token: 0x040009C7 RID: 2503
	public const string ConnectionsZone_ConfigureVerb = "Verb to configure a connection.";

	// Token: 0x040009C8 RID: 2504
	public const string ConnectionsZone_ConnectToConsumerInstructionText = "Create consumer connections for this Web Part.";

	// Token: 0x040009C9 RID: 2505
	public const string ConnectionsZone_ConnectToConsumerInstructionTextDescription = "The instruction text when creating a connection to a consumer.";

	// Token: 0x040009CA RID: 2506
	public const string ConnectionsZone_ConnectToConsumerText = "Create a connection to a Consumer";

	// Token: 0x040009CB RID: 2507
	public const string ConnectionsZone_ConnectToConsumerTextDescription = "The text of the link to the creation of a new connection to a consumer.";

	// Token: 0x040009CC RID: 2508
	public const string ConnectionsZone_ConnectToConsumerTitle = "Send Data to Web Part";

	// Token: 0x040009CD RID: 2509
	public const string ConnectionsZone_ConnectToConsumerTitleDescription = "The title when creating a new connection to a consumer.";

	// Token: 0x040009CE RID: 2510
	public const string ConnectionsZone_ConnectToProviderInstructionText = "Create provider connections for this Web Part.";

	// Token: 0x040009CF RID: 2511
	public const string ConnectionsZone_ConnectToProviderInstructionTextDescription = "The instruction text when creating a new connection to a consumer.";

	// Token: 0x040009D0 RID: 2512
	public const string ConnectionsZone_ConnectToProviderText = "Create a connection to a Provider";

	// Token: 0x040009D1 RID: 2513
	public const string ConnectionsZone_ConnectToProviderTextDescription = "The text of the link to the creation of a new connection to a provider.";

	// Token: 0x040009D2 RID: 2514
	public const string ConnectionsZone_ConnectToProviderTitle = "Get Data from Web Part";

	// Token: 0x040009D3 RID: 2515
	public const string ConnectionsZone_ConnectToProviderTitleDescription = "The title when creating a new connection to a provider.";

	// Token: 0x040009D4 RID: 2516
	public const string ConnectionsZone_ConnectVerb = "Verb to connect two Web Parts.";

	// Token: 0x040009D5 RID: 2517
	public const string ConnectionsZone_ConsumersInstructionText = "Web parts that the current Web part sends information to:";

	// Token: 0x040009D6 RID: 2518
	public const string ConnectionsZone_ConsumersInstructionTextDescription = "The text that describes the list of existing connections to consumers.";

	// Token: 0x040009D7 RID: 2519
	public const string ConnectionsZone_ConsumersTitle = "Consumers";

	// Token: 0x040009D8 RID: 2520
	public const string ConnectionsZone_ConsumersTitleDescription = "The legend for the set of existing connections to consumers.";

	// Token: 0x040009D9 RID: 2521
	public const string ConnectionsZone_CloseVerb = "Verb to close the ConnectionsZone.";

	// Token: 0x040009DA RID: 2522
	public const string ConnectionsZone_DisconnectVerb = "Verb to disconnect two Web Parts.";

	// Token: 0x040009DB RID: 2523
	public const string ConnectionsZone_DisconnectInvalid = "The provider or the consumer of the connection to disconnect must be the currently selected Web Part.";

	// Token: 0x040009DC RID: 2524
	public const string ConnectionsZone_ErrorCantContinueConnectionCreation = "Can't continue with the creation of this connection because at least one of the Web Parts or connection points has disappeared or has become incompatible with the currently selected Web Part or is already used by another connection and doesn't support multiple connections.";

	// Token: 0x040009DD RID: 2525
	public const string ConnectionsZone_ErrorMessage = "The message that's displayed by the connections zone when it can't create or continue creating a connection.";

	// Token: 0x040009DE RID: 2526
	public const string ConnectionsZone_Get = "Get:";

	// Token: 0x040009DF RID: 2527
	public const string ConnectionsZone_GetDescription = "The text that appears before consumer connection point names.";

	// Token: 0x040009E0 RID: 2528
	public const string ConnectionsZone_GetFromText = "From:";

	// Token: 0x040009E1 RID: 2529
	public const string ConnectionsZone_GetFromTextDescription = "The text that appears before provider names.";

	// Token: 0x040009E2 RID: 2530
	public const string ConnectionsZone_HeaderText = "Connections Zone";

	// Token: 0x040009E3 RID: 2531
	public const string ConnectionsZone_HeaderTextDescription = "The text that appears in the header of the connections zone.";

	// Token: 0x040009E4 RID: 2532
	public const string ConnectionsZone_InstructionText = "Manage the connections for the current Web part.";

	// Token: 0x040009E5 RID: 2533
	public const string ConnectionsZone_InstructionTextDescription = "The instruction text when the connections zone is displaying existing connections.";

	// Token: 0x040009E6 RID: 2534
	public const string ConnectionsZone_InstructionTitle = "Manage the connections for {0}";

	// Token: 0x040009E7 RID: 2535
	public const string ConnectionsZone_InstructionTitleDescription = "The title when the connections zone is displaying existing connections. The name of the Web part to connect is appended to this text at run-time.";

	// Token: 0x040009E8 RID: 2536
	public const string ConnectionsZone_MustImplementITransformerConfigurationControl = "The control returned from WebPartTransformer.CreateConfigurationControl() must implement ITransformerConfigurationControl.";

	// Token: 0x040009E9 RID: 2537
	public const string ConnectionsZone_NoConsumers = "No compatible consumers";

	// Token: 0x040009EA RID: 2538
	public const string ConnectionsZone_NoExistingConnectionTitle = "No active connections";

	// Token: 0x040009EB RID: 2539
	public const string ConnectionsZone_NoExistingConnectionTitleDescription = "The title when no connection exists on the selected Web part.";

	// Token: 0x040009EC RID: 2540
	public const string ConnectionsZone_NoExistingConnectionInstructionText = "There are no active connections available in your Web Part. You may create a new connection by selecting the links above if there are compatible Web Parts on the page.";

	// Token: 0x040009ED RID: 2541
	public const string ConnectionsZone_NoExistingConnectionInstructionTextDescription = "The instruction text when no connection exist on the selected Web part.";

	// Token: 0x040009EE RID: 2542
	public const string ConnectionsZone_NoProviders = "No compatible providers";

	// Token: 0x040009EF RID: 2543
	public const string ConnectionsZone_ProvidersInstructionText = "Web parts that the current Web part gets information from:";

	// Token: 0x040009F0 RID: 2544
	public const string ConnectionsZone_ProvidersInstructionTextDescription = "The text that describes the list of existing connections to providers.";

	// Token: 0x040009F1 RID: 2545
	public const string ConnectionsZone_ProvidersTitle = "Providers";

	// Token: 0x040009F2 RID: 2546
	public const string ConnectionsZone_ProvidersTitleDescription = "The legend for the set of existing connections to providers.";

	// Token: 0x040009F3 RID: 2547
	public const string ConnectionsZone_SendText = "Send:";

	// Token: 0x040009F4 RID: 2548
	public const string ConnectionsZone_SendTextDescription = "The text that appears before provider connection point names.";

	// Token: 0x040009F5 RID: 2549
	public const string ConnectionsZone_SendToText = "To:";

	// Token: 0x040009F6 RID: 2550
	public const string ConnectionsZone_SendToTextDescription = "The text that appears before consumer names.";

	// Token: 0x040009F7 RID: 2551
	public const string ConnectionsZone_WarningConnectionDisabled = "This connection is currently inactive due to the unavailability of one of its end points.";

	// Token: 0x040009F8 RID: 2552
	public const string ConnectionsZone_WarningMessage = "The message that is displayed when an existing connection is no longer valid.";

	// Token: 0x040009F9 RID: 2553
	public const string ConnectionPoint_InvalidControlType = "Type must be a subclass of Control.";

	// Token: 0x040009FA RID: 2554
	public const string ContentDirection_NotSet = "Not Set";

	// Token: 0x040009FB RID: 2555
	public const string ContentDirection_LeftToRight = "Left to Right";

	// Token: 0x040009FC RID: 2556
	public const string ContentDirection_RightToLeft = "Right to Left";

	// Token: 0x040009FD RID: 2557
	public const string DeclarativeCatalogPart_PartTitle = "Declarative Catalog";

	// Token: 0x040009FE RID: 2558
	public const string DeclarativeCatlaogPart_WebPartsListUserControlPath = "Path to a UserControl containing additional WebParts to display in the CatalogPart.";

	// Token: 0x040009FF RID: 2559
	public const string EditorPart_MustBeInZone = "EditorPart '{0}' must be placed in an EditorZone.";

	// Token: 0x04000A00 RID: 2560
	public const string EditorPart_ErrorBadUrl = "Url properties must be relative or use the http: or https: protocol.";

	// Token: 0x04000A01 RID: 2561
	public const string EditorPart_ErrorConvertingProperty = "Error converting property to the required type.";

	// Token: 0x04000A02 RID: 2562
	public const string EditorPart_ErrorConvertingPropertyWithType = "Property value must be of type {0}.";

	// Token: 0x04000A03 RID: 2563
	public const string EditorPart_ErrorSettingProperty = "Error setting property value.";

	// Token: 0x04000A04 RID: 2564
	public const string EditorPart_ErrorSettingPropertyWithExceptionMessage = "Error setting property value: {0}";

	// Token: 0x04000A05 RID: 2565
	public const string EditorPart_PropertyMaxValue = "Property value must be less than or equal to {0}.";

	// Token: 0x04000A06 RID: 2566
	public const string EditorPart_PropertyMinValue = "Property value must be greater than or equal to {0}.";

	// Token: 0x04000A07 RID: 2567
	public const string EditorPart_PropertyMustBeDecimal = "Property value must be a decimal number.";

	// Token: 0x04000A08 RID: 2568
	public const string EditorPart_PropertyMustBeInteger = "Property value must be an integer.";

	// Token: 0x04000A09 RID: 2569
	public const string EditorZone_OnlyEditorParts = "Should only have editor parts in the ZoneTemplate of '{0}'.";

	// Token: 0x04000A0A RID: 2570
	public const string EditorZoneBase_ApplyVerb = "Verb to apply the changes and leave the EditorZone open.";

	// Token: 0x04000A0B RID: 2571
	public const string EditorZoneBase_CancelVerb = "Verb to cancel the changes and close the EditorZone.";

	// Token: 0x04000A0C RID: 2572
	public const string EditorZoneBase_DefaultEmptyZoneText = "Editor Zone contains no Editor Parts.";

	// Token: 0x04000A0D RID: 2573
	public const string EditorZoneBase_DefaultErrorText = "There was an error applying one or more Editor Parts.";

	// Token: 0x04000A0E RID: 2574
	public const string EditorZoneBase_DefaultHeaderText = "Editor Zone";

	// Token: 0x04000A0F RID: 2575
	public const string EditorZoneBase_DefaultInstructionText = "Modify the properties of the Web Part, then click OK or Apply to apply your changes.";

	// Token: 0x04000A10 RID: 2576
	public const string EditorZoneBase_ErrorText = "The text shown when there is an error in an Editor Part.";

	// Token: 0x04000A11 RID: 2577
	public const string EditorZoneBase_NoEditorPartID = "EditorPart does not have an ID.";

	// Token: 0x04000A12 RID: 2578
	public const string EditorZoneBase_OKVerb = "Verb to apply the changes and close the EditorZone.";

	// Token: 0x04000A13 RID: 2579
	public const string ErrorWebPart_ErrorText = "Web Part Error: {0}";

	// Token: 0x04000A14 RID: 2580
	public const string GenericWebPart_CannotWrapWebPart = "Web Parts cannot be wrapped by a GenericWebPart.";

	// Token: 0x04000A15 RID: 2581
	public const string GenericWebPart_CannotWrapOutputCachedControl = "OutputCached controls cannot be wrapped by a GenericWebPart.";

	// Token: 0x04000A16 RID: 2582
	public const string GenericWebPart_NoID = "The specified control of type '{0}' does not have an ID.";

	// Token: 0x04000A17 RID: 2583
	public const string GenericWebPart_CannotModify = "Cannot modify the controls collection of a GenericWebPart.  To create a new GenericWebPart, use the WebPartManager.CreateWebPart() method.";

	// Token: 0x04000A18 RID: 2584
	public const string GenericWebPart_ChildControlIsNull = "The child control inside the GenericWebPart is null.";

	// Token: 0x04000A19 RID: 2585
	public const string ImportCatalogPart_PartTitle = "Imported Web Part Catalog";

	// Token: 0x04000A1A RID: 2586
	public const string ImportCatalogPart_Browse = "Type a file name (.WebPart) or click \"Browse\" to locate a Web Part.";

	// Token: 0x04000A1B RID: 2587
	public const string ImportCatalogPart_BrowseHelpText = "The help text that appears before the upload field.";

	// Token: 0x04000A1C RID: 2588
	public const string ImportCatalogPart_Upload = "Once you have selected a Web Part file to import, click the Upload button.";

	// Token: 0x04000A1D RID: 2589
	public const string ImportCatalogPart_UploadHelpText = "The text that appears above the upload button.";

	// Token: 0x04000A1E RID: 2590
	public const string ImportCatalogPart_UploadButton = "Upload";

	// Token: 0x04000A1F RID: 2591
	public const string ImportCatalogPart_UploadButtonText = "The text of the button that launches the Web Part upload.";

	// Token: 0x04000A20 RID: 2592
	public const string ImportCatalogPart_ImportedPartLabel = "Imported Web Part";

	// Token: 0x04000A21 RID: 2593
	public const string ImportCatalogPart_ImportedPartErrorLabel = "Error while importing Web Part";

	// Token: 0x04000A22 RID: 2594
	public const string ImportCatalogPart_PartImportErrorLabelText = "The text that appears in the catalog if an error occurs during import.";

	// Token: 0x04000A23 RID: 2595
	public const string ImportCatalogPart_ImportedPartLabelText = "The text that appears in the catalog above an imported part description.";

	// Token: 0x04000A24 RID: 2596
	public const string ImportCatalogPart_NoFileName = "You must type a Web Part file name (.WebPart).";

	// Token: 0x04000A25 RID: 2597
	public const string LayoutEditorPart_ChromeState = "Chrome State";

	// Token: 0x04000A26 RID: 2598
	public const string LayoutEditorPart_Zone = "Zone";

	// Token: 0x04000A27 RID: 2599
	public const string LayoutEditorPart_ZoneIndex = "Zone Index";

	// Token: 0x04000A28 RID: 2600
	public const string LayoutEditorPart_PartTitle = "Layout";

	// Token: 0x04000A29 RID: 2601
	public const string PageCatalogPart_PartTitle = "Page Catalog";

	// Token: 0x04000A2A RID: 2602
	public const string Part_Description = "The text description of the Part.";

	// Token: 0x04000A2B RID: 2603
	public const string Part_ChromeState = "Whether the Part is shown minimized or normal size.";

	// Token: 0x04000A2C RID: 2604
	public const string Part_ChromeType = "The type of chrome that will be rendered around the Part.";

	// Token: 0x04000A2D RID: 2605
	public const string Part_Title = "The title of the Part.";

	// Token: 0x04000A2E RID: 2606
	public const string Part_Unknown = "Unknown";

	// Token: 0x04000A2F RID: 2607
	public const string Part_Untitled = "Untitled";

	// Token: 0x04000A30 RID: 2608
	public const string PartChromeState_Normal = "Normal";

	// Token: 0x04000A31 RID: 2609
	public const string PartChromeState_Minimized = "Minimized";

	// Token: 0x04000A32 RID: 2610
	public const string PartChromeType_Default = "Default";

	// Token: 0x04000A33 RID: 2611
	public const string PartChromeType_TitleAndBorder = "Title and Border";

	// Token: 0x04000A34 RID: 2612
	public const string PartChromeType_TitleOnly = "Title Only";

	// Token: 0x04000A35 RID: 2613
	public const string PartChromeType_BorderOnly = "Border Only";

	// Token: 0x04000A36 RID: 2614
	public const string PartChromeType_None = "None";

	// Token: 0x04000A37 RID: 2615
	public const string PersonalizableTypeEntry_InvalidProperty = "The '{0}' property of '{1}' is not a valid Personalizable property.  It must have a public get and set accessor, and take no index parameters.";

	// Token: 0x04000A38 RID: 2616
	public const string PersonalizationDictionary_MustBeTypeString = "Value must be of type String.";

	// Token: 0x04000A39 RID: 2617
	public const string PersonalizationDictionary_MustBeTypePersonalizationEntry = "Value must be of type PersonalizationEntry.";

	// Token: 0x04000A3A RID: 2618
	public const string PersonalizationDictionary_MustBeTypeDictionaryEntryArray = "Value must be of type DictionaryEntry[].";

	// Token: 0x04000A3B RID: 2619
	public const string PersonalizationProvider_ApplicationNameExceedMaxLength = "The ApplicationName cannot exceed character length {0}.";

	// Token: 0x04000A3C RID: 2620
	public const string PersonalizationProvider_BadConnection = "The specified connectionStringName, '{0}', was not registered.";

	// Token: 0x04000A3D RID: 2621
	public const string PersonalizationProvider_CantAccess = "A connection could not be made by the {0} personalization provider using the specified registration.";

	// Token: 0x04000A3E RID: 2622
	public const string PersonalizationProvider_NoConnection = "The connectionStringName attribute must be specified when registering a personalization provider.";

	// Token: 0x04000A3F RID: 2623
	public const string PersonalizationProvider_UnknownProp = "Invalid attribute '{0}', specified in the '{1}' personalization provider registration.";

	// Token: 0x04000A40 RID: 2624
	public const string PersonalizationProvider_WrongType = "Argument must be of type BlobPersonalizationState.";

	// Token: 0x04000A41 RID: 2625
	public const string PropertyGridEditorPart_PartTitle = "Property Grid";

	// Token: 0x04000A42 RID: 2626
	public const string PropertyGridEditorPart_DesignModeWebPart_BoolProperty = "Sample boolean property";

	// Token: 0x04000A43 RID: 2627
	public const string PropertyGridEditorPart_DesignModeWebPart_EnumProperty = "Sample enum property";

	// Token: 0x04000A44 RID: 2628
	public const string PropertyGridEditorPart_DesignModeWebPart_StringProperty = "Sample string property";

	// Token: 0x04000A45 RID: 2629
	public const string ProxyWebPartConnectionCollection_ReadOnly = "The StaticConnections collection of ProxyWebPartManager is read-only after connections have been activated.";

	// Token: 0x04000A46 RID: 2630
	public const string RowToFieldTransformer_FieldName = "Field Name:";

	// Token: 0x04000A47 RID: 2631
	public const string RowToFieldTransformer_NoProviderSchema = "No Provider Schema";

	// Token: 0x04000A48 RID: 2632
	public const string RowToParametersTransformer_DifferentFieldNamesLength = "The number of ConsumerFieldNames and ProviderFieldNames must be the same.";

	// Token: 0x04000A49 RID: 2633
	public const string RowToParametersTransformer_ConsumerFieldName = "Consumer Field Name:";

	// Token: 0x04000A4A RID: 2634
	public const string RowToParametersTransformer_NoConsumerSchema = "No Consumer Schema";

	// Token: 0x04000A4B RID: 2635
	public const string RowToParametersTransformer_ProviderFieldName = "Provider Field Name:";

	// Token: 0x04000A4C RID: 2636
	public const string RowToParametersTransformer_NoProviderSchema = "No Provider Schema";

	// Token: 0x04000A4D RID: 2637
	public const string SqlPersonalizationProvider_Description = "Personalization provider that stores data in a SQL Server database.";

	// Token: 0x04000A4E RID: 2638
	public const string ToolZone_CantSetVisible = "Cannot set the Visible property of a Tool Zone.  Override the ToolZone.Display property instead.";

	// Token: 0x04000A4F RID: 2639
	public const string ToolZone_EditUIStyle = "The style applied to the UI elements used for editing.";

	// Token: 0x04000A50 RID: 2640
	public const string ToolZone_HeaderCloseVerb = "Verb displayed in the header to close the Zone.";

	// Token: 0x04000A51 RID: 2641
	public const string ToolZone_HeaderVerbStyle = "The style applied to the HeaderCloseVerb.";

	// Token: 0x04000A52 RID: 2642
	public const string ToolZone_InstructionText = "The instructional text shown in the Zone.";

	// Token: 0x04000A53 RID: 2643
	public const string ToolZone_InstructionTextStyle = "The style applied to the instructional text shown in the Zone.";

	// Token: 0x04000A54 RID: 2644
	public const string ToolZone_LabelStyle = "The style applied to the labels for the UI elements used for editing.";

	// Token: 0x04000A55 RID: 2645
	public const string ToolZone_DisplayModesReadOnly = "The collection of DisplayModes on a ToolZone is read-only.";

	// Token: 0x04000A56 RID: 2646
	public const string WebPartTransformerAttribute_Missing = "The WebPartTransformerAttribute is not defined on the type '{0}'.";

	// Token: 0x04000A57 RID: 2647
	public const string WebPartTransformerAttribute_NotTransformer = "The type '{0}' is not a subclass of WebPartTransformer.";

	// Token: 0x04000A58 RID: 2648
	public const string WebPartTransformerAttribute_SameTypes = "The consumer and provider types of a transformer may not be the same type.";

	// Token: 0x04000A59 RID: 2649
	public const string WebPartTransformerCollection_NotEmpty = "The WebPartTransformerCollection may contain at most one WebPartTransformer.";

	// Token: 0x04000A5A RID: 2650
	public const string WebPartTransformerCollection_ReadOnly = "The WebPartTransformerCollection of Connection is read-only after it has been activated.";

	// Token: 0x04000A5B RID: 2651
	public const string UnknownWebPart = "The specified Web Part does not belong the collection of Web Parts on this page.";

	// Token: 0x04000A5C RID: 2652
	public const string WebPart_AllowClose = "Whether the Web Part can be closed.";

	// Token: 0x04000A5D RID: 2653
	public const string WebPart_AllowConnect = "Whether the Web Part can be connected to other Web Parts.";

	// Token: 0x04000A5E RID: 2654
	public const string WebPart_AllowEdit = "Whether the Web Part's properties can be changed using the EditorZone.";

	// Token: 0x04000A5F RID: 2655
	public const string WebPart_AllowHide = "Whether the Web Part can be hidden.";

	// Token: 0x04000A60 RID: 2656
	public const string WebPart_AllowMinimize = "Whether the Web Part can be minimized.";

	// Token: 0x04000A61 RID: 2657
	public const string WebPart_AllowZoneChange = "Whether the Web Part can be moved to another Zone.";

	// Token: 0x04000A62 RID: 2658
	public const string WebPart_AuthorizationFilter = "String used by the WebPartManager to determine if the user is authorized to view this WebPart.";

	// Token: 0x04000A63 RID: 2659
	public const string WebPart_BadUrl = "'{0}' is not a valid Url.  It must be relative or use the http: or https: protocol.";

	// Token: 0x04000A64 RID: 2660
	public const string WebPart_CatalogIconImageUrl = "The URL of the image to be displayed in the catalog for the Web Part.";

	// Token: 0x04000A65 RID: 2661
	public const string WebPart_CantSetExportMode = "Cannot set ExportMode after Load if not in shared mode or if there is no WebPartManager or if the WebPart is outside of a Zone.";

	// Token: 0x04000A66 RID: 2662
	public const string WebPart_DefaultImportErrorMessage = "Cannot import this Web Part.";

	// Token: 0x04000A67 RID: 2663
	public const string WebPart_ErrorFormatString = "{0} (Error)";

	// Token: 0x04000A68 RID: 2664
	public const string WebPart_ExportMode = "Which properties can be exported from the Web Part.";

	// Token: 0x04000A69 RID: 2665
	public const string WebPart_HelpMode = "Determines how the help page should be shown.";

	// Token: 0x04000A6A RID: 2666
	public const string WebPart_HelpUrl = "The URL of the page that provides help for this Web Part.";

	// Token: 0x04000A6B RID: 2667
	public const string WebPart_Hidden = "Whether the Web Part should be hidden on the page. A hidden Web Part can still be edited and participate in connections.";

	// Token: 0x04000A6C RID: 2668
	public const string WebPart_HiddenFormatString = "(Hidden) {0}";

	// Token: 0x04000A6D RID: 2669
	public const string WebPart_ImportErrorInvalidVersion = "The version of the imported <webPart /> is not supported.";

	// Token: 0x04000A6E RID: 2670
	public const string WebPart_ImportErrorMessage = "The text shown when there is an error importing the Web Part.";

	// Token: 0x04000A6F RID: 2671
	public const string WebPart_ImportErrorNoVersion = "The imported <webPart /> must specify its version using the xmlns attribute.";

	// Token: 0x04000A70 RID: 2672
	public const string WebPart_NonWebPart = "The specified control is not being used as a Web Part.";

	// Token: 0x04000A71 RID: 2673
	public const string WebPart_NotStandalone = "The {0} property cannot be set on Web Part '{1}', since it is a standalone Web Part.";

	// Token: 0x04000A72 RID: 2674
	public const string WebPart_OnlyStandalone = "The {0} property cannot be set on Web Part '{1}'.  It can only be set on a standalone Web Part.";

	// Token: 0x04000A73 RID: 2675
	public const string WebPart_SetZoneTemplateTooLate = "The ZoneTemplate property can only be set in or before the Page_PreInit event for static controls. For dynamic controls, set the property before adding it to the Controls collection.";

	// Token: 0x04000A74 RID: 2676
	public const string WebPart_TitleIconImageUrl = "The URL of the image to be displayed in the title bar of the Web Part.";

	// Token: 0x04000A75 RID: 2677
	public const string WebPart_TitleUrl = "The URL of the page that contains additional information about this Web Part.  The title of the Web Part will be rendered as a link to this page.";

	// Token: 0x04000A76 RID: 2678
	public const string WebPart_Collection_DuplicateID = "A {0} has already been added with ID '{1}'.";

	// Token: 0x04000A77 RID: 2679
	public const string WebPartActionVerb_CantSetChecked = "Cannot set the Checked property of this Verb.";

	// Token: 0x04000A78 RID: 2680
	public const string WebPartCatalogAddVerb_Description = "Adds a Web Part to a Zone";

	// Token: 0x04000A79 RID: 2681
	public const string WebPartCatalogAddVerb_Text = "Add";

	// Token: 0x04000A7A RID: 2682
	public const string WebPartCatalogCloseVerb_Description = "Closes Catalog";

	// Token: 0x04000A7B RID: 2683
	public const string WebPartCatalogCloseVerb_Text = "Close";

	// Token: 0x04000A7C RID: 2684
	public const string WebPartChrome_ConfirmExportSensitive = "This Web Part Page has been personalized. As a result, one or more Web Part properties may contain confidential information. Make sure the properties contain information that is safe for others to read. After exporting this Web Part, view properties in the Web Part description file (.WebPart) by using a text editor such as Microsoft Notepad.";

	// Token: 0x04000A7D RID: 2685
	public const string WebPartCloseVerb_Description = "Closes '{0}'";

	// Token: 0x04000A7E RID: 2686
	public const string WebPartCloseVerb_Text = "Close";

	// Token: 0x04000A7F RID: 2687
	public const string WebPartConnectVerb_Description = "Edits the connections for '{0}'";

	// Token: 0x04000A80 RID: 2688
	public const string WebPartConnectVerb_Text = "Connect";

	// Token: 0x04000A81 RID: 2689
	public const string WebPartConnection_ConsumerIDNotSet = "The ConsumerID property is not set.";

	// Token: 0x04000A82 RID: 2690
	public const string WebPartConnection_ConsumerRequiresSecondaryInterfaces = "The consumer connection point '{0}' on '{1}' does not support a connection with no secondary interfaces, so it cannot be connected via a transformer.";

	// Token: 0x04000A83 RID: 2691
	public const string WebPartConnection_DisabledConnectionPoint = "The connection point '{0}' on '{1}' is disabled.";

	// Token: 0x04000A84 RID: 2692
	public const string WebPartConnection_Duplicate = "The connection point '{0}' on '{1}' does not allow multiple connections.";

	// Token: 0x04000A85 RID: 2693
	public const string WebPartConnection_IncompatibleConsumerTransformer = "The transformer and the consumer connection point '{0}' on '{1}' do not use the same connection interface.";

	// Token: 0x04000A86 RID: 2694
	public const string WebPartConnection_IncompatibleConsumerTransformerWithType = "The transformer of type '{0}' and the consumer connection point '{1}' on '{2}' do not use the same connection interface.";

	// Token: 0x04000A87 RID: 2695
	public const string WebPartConnection_IncompatibleProviderTransformer = "The provider connection point '{0}' on '{1}' and the transformer do not use the same connection interface.";

	// Token: 0x04000A88 RID: 2696
	public const string WebPartConnection_IncompatibleProviderTransformerWithType = "The provider connection point '{0}' on '{1}' and the transformer of type '{2}' do not use the same connection interface.";

	// Token: 0x04000A89 RID: 2697
	public const string WebPartConnection_IncompatibleSecondaryInterfaces = "The consumer connection point '{0}' on '{1}' does not support connecting on the secondary interfaces provided by connection point '{2}' on '{3}'.";

	// Token: 0x04000A8A RID: 2698
	public const string WebPartConnection_NoCommonInterface = "The provider connection point '{0}' on '{1}' and the consumer connection point '{2}' on '{3}' do not use the same connection interface.";

	// Token: 0x04000A8B RID: 2699
	public const string WebPartConnection_NoConsumer = "Could not find the connection consumer Web Part with ID '{0}'.";

	// Token: 0x04000A8C RID: 2700
	public const string WebPartConnection_NoConsumerConnectionPoint = "There is no consumer connection point '{0}' on '{1}'.";

	// Token: 0x04000A8D RID: 2701
	public const string WebPartConnection_NoID = "Connection does not have an ID.";

	// Token: 0x04000A8E RID: 2702
	public const string WebPartConnection_NoProvider = "Could not find the connection provider Web Part with ID '{0}'.";

	// Token: 0x04000A8F RID: 2703
	public const string WebPartConnection_NoProviderConnectionPoint = "There is no provider connection point '{0}' on '{1}'.";

	// Token: 0x04000A90 RID: 2704
	public const string WebPartConnection_ProviderIDNotSet = "The ProviderID property is not set.";

	// Token: 0x04000A91 RID: 2705
	public const string WebPartConnection_TransformerNotAvailable = "The required transformer type is not allowed to be used on this page.";

	// Token: 0x04000A92 RID: 2706
	public const string WebPartConnection_TransformerNotAvailableWithType = "The transformer type '{0}' is not allowed to be used on this page.";

	// Token: 0x04000A93 RID: 2707
	public const string WebPartConnectionsCancelVerb_Description = "Cancels the current action";

	// Token: 0x04000A94 RID: 2708
	public const string WebPartConnectionsCancelVerb_Text = "Cancel";

	// Token: 0x04000A95 RID: 2709
	public const string WebPartConnectionsCloseVerb_Description = "Closes Connections Editor";

	// Token: 0x04000A96 RID: 2710
	public const string WebPartConnectionsCloseVerb_Text = "Close";

	// Token: 0x04000A97 RID: 2711
	public const string WebPartConnectionsConfigureVerb_Description = "Modifies the configuration of the connection";

	// Token: 0x04000A98 RID: 2712
	public const string WebPartConnectionsConfigureVerb_Text = "Edit...";

	// Token: 0x04000A99 RID: 2713
	public const string WebPartConnectionsConnectVerb_Description = "Connects the Web Parts";

	// Token: 0x04000A9A RID: 2714
	public const string WebPartConnectionsConnectVerb_Text = "Connect";

	// Token: 0x04000A9B RID: 2715
	public const string WebPartConnectionsDisconnectVerb_Description = "Disconnects the Web Parts";

	// Token: 0x04000A9C RID: 2716
	public const string WebPartConnectionsDisconnectVerb_Text = "Disconnect";

	// Token: 0x04000A9D RID: 2717
	public const string WebPartDeleteVerb_Description = "Deletes '{0}'";

	// Token: 0x04000A9E RID: 2718
	public const string WebPartDeleteVerb_Text = "Delete";

	// Token: 0x04000A9F RID: 2719
	public const string WebPartDisplayModeCollection_CantRemove = "Collection does not allow removal of items.";

	// Token: 0x04000AA0 RID: 2720
	public const string WebPartDisplayModeCollection_CantSet = "Collection does not allow setting of items.";

	// Token: 0x04000AA1 RID: 2721
	public const string WebPartDisplayModeCollection_DuplicateName = "A display mode has already been added with name '{0}'.";

	// Token: 0x04000AA2 RID: 2722
	public const string WebPartEditorApplyVerb_Description = "Applies changes";

	// Token: 0x04000AA3 RID: 2723
	public const string WebPartEditorApplyVerb_Text = "Apply";

	// Token: 0x04000AA4 RID: 2724
	public const string WebPartEditorCancelVerb_Description = "Cancels changes";

	// Token: 0x04000AA5 RID: 2725
	public const string WebPartEditorCancelVerb_Text = "Cancel";

	// Token: 0x04000AA6 RID: 2726
	public const string WebPartEditorOKVerb_Description = "Applies changes and closes editor";

	// Token: 0x04000AA7 RID: 2727
	public const string WebPartEditorOKVerb_Text = "OK";

	// Token: 0x04000AA8 RID: 2728
	public const string WebPartEditVerb_Description = "Edits '{0}'";

	// Token: 0x04000AA9 RID: 2729
	public const string WebPartEditVerb_Text = "Edit";

	// Token: 0x04000AAA RID: 2730
	public const string WebPartExportHandler_InvalidArgument = "Invalid export parameters.";

	// Token: 0x04000AAB RID: 2731
	public const string WebPartExportHandler_DisabledExportHandler = "Web Part export is currently disabled. It can be enabled by setting enableExport=\"true\" in the WebParts section of the configuration file for this application.";

	// Token: 0x04000AAC RID: 2732
	public const string WebPartExportVerb_Description = "Exports the personalization data for '{0}' as an XML file";

	// Token: 0x04000AAD RID: 2733
	public const string WebPartExportVerb_Text = "Export";

	// Token: 0x04000AAE RID: 2734
	public const string WebPartHeaderCloseVerb_Description = "Closes Zone";

	// Token: 0x04000AAF RID: 2735
	public const string WebPartHeaderCloseVerb_Text = "Close";

	// Token: 0x04000AB0 RID: 2736
	public const string WebPartHelpVerb_Description = "Shows help for '{0}'";

	// Token: 0x04000AB1 RID: 2737
	public const string WebPartHelpVerb_Text = "Help";

	// Token: 0x04000AB2 RID: 2738
	public const string WebPartManager_Personalization = "The personalization settings associated with the WebPartManager and this page.";

	// Token: 0x04000AB3 RID: 2739
	public const string WebPartManager_MustRegister = "Zone must be registered with the WebPartManager.";

	// Token: 0x04000AB4 RID: 2740
	public const string WebPartManager_UnknownConnection = "Unknown connection.";

	// Token: 0x04000AB5 RID: 2741
	public const string WebPartManager_AlreadyInConnect = "Web part is already in connect mode.";

	// Token: 0x04000AB6 RID: 2742
	public const string WebPartManager_AlreadyInZone = "Web Part is already in a zone.";

	// Token: 0x04000AB7 RID: 2743
	public const string WebPartManager_MustBeInConnect = "Must be in connect mode.";

	// Token: 0x04000AB8 RID: 2744
	public const string WebPartManager_AlreadyInEdit = "Web part is already in edit mode.";

	// Token: 0x04000AB9 RID: 2745
	public const string WebPartManager_MustBeInEdit = "Must be in edit mode.";

	// Token: 0x04000ABA RID: 2746
	public const string WebPartManager_InvalidConnectionPoint = "ConnectionPoint must be from a Web Part's ConnectionPoints collection.";

	// Token: 0x04000ABB RID: 2747
	public const string WebPartManager_NoSelectedWebPartConnect = "No Web Part is having its connections changed.";

	// Token: 0x04000ABC RID: 2748
	public const string WebPartManager_NoSelectedWebPartEdit = "No Web Part is being edited.";

	// Token: 0x04000ABD RID: 2749
	public const string WebPartManager_MustBeInZone = "Web Part must be currently in a Zone.";

	// Token: 0x04000ABE RID: 2750
	public const string WebPartManager_OnlyOneInstance = "Can only have one instance of a WebPartManager and it must precede any instances of WebPartZone controls.";

	// Token: 0x04000ABF RID: 2751
	public const string WebPartManager_AlreadyRegistered = "Zone has already been registered.";

	// Token: 0x04000AC0 RID: 2752
	public const string WebPartManager_NoZoneID = "Zone does not have an ID.";

	// Token: 0x04000AC1 RID: 2753
	public const string WebPartManager_DuplicateZoneID = "A Zone has already been added with ID '{0}'.";

	// Token: 0x04000AC2 RID: 2754
	public const string WebPartManager_CannotModify = "Cannot directly modify the collection of Web Parts.  Instead, use the WebPartManager.AddWebPart() or WebPartManager.DeleteWebPart() method.";

	// Token: 0x04000AC3 RID: 2755
	public const string WebPartManager_NoWebPartID = "Web Part does not have an ID.";

	// Token: 0x04000AC4 RID: 2756
	public const string WebPartManager_NoChildControlID = "Child Control of Generic Web Part does not have an ID.";

	// Token: 0x04000AC5 RID: 2757
	public const string WebPartManager_DuplicateWebPartID = "A Web Part or Child Control of a Generic Web Part has already been added with ID '{0}'.";

	// Token: 0x04000AC6 RID: 2758
	public const string WebPartManager_StaticConnections = "The static connections between Web Parts on the page.";

	// Token: 0x04000AC7 RID: 2759
	public const string WebPartManager_InvalidConsumerSignature = "Method '{0}' on type '{1}' is not a valid connection consumer.  It must be public, return void, and take one parameter.";

	// Token: 0x04000AC8 RID: 2760
	public const string WebPartManager_InvalidProviderSignature = "Method '{0}' on type '{1}' is not a valid connection provider.  It must be public, return an object, and take no parameters.";

	// Token: 0x04000AC9 RID: 2761
	public const string WebPartManager_ConnectTooLate = "The ConnectWebParts method cannot be called after connections have already been activated (in WebPartManager.PreRender).";

	// Token: 0x04000ACA RID: 2762
	public const string WebPartManager_DisconnectTooLate = "The DisconnectWebParts method cannot be called after connections have already been activated (in WebPartManager.PreRender).";

	// Token: 0x04000ACB RID: 2763
	public const string WebPartManager_EnableClientScript = "Whether the client script features of the Web Part framework are enabled.";

	// Token: 0x04000ACC RID: 2764
	public const string WebPartManager_ForbiddenType = "You are not allowed to use this Web Part.";

	// Token: 0x04000ACD RID: 2765
	public const string WebPartManager_PartNotExportable = "This part is not exportable. To be exportable, a part must be personalizable and not have its ExportMode set to None.";

	// Token: 0x04000ACE RID: 2766
	public const string WebPartManager_ImportInvalidFormat = "The file format is not valid. Try importing a Web Part file (.WebPart).";

	// Token: 0x04000ACF RID: 2767
	public const string WebPartManager_ImportInvalidData = "Couldn't import property {0}.";

	// Token: 0x04000AD0 RID: 2768
	public const string WebPartManager_RegisterTooLate = "The RegisterZone method cannot be called after the Page has been initialized.";

	// Token: 0x04000AD1 RID: 2769
	public const string WebPartManager_ExportSensitiveDataWarning = "The warning message to be displayed when potentially exporting sensitive personalized data.";

	// Token: 0x04000AD2 RID: 2770
	public const string WebPartManager_AlreadyDisconnected = "Connection has already been disconnected.";

	// Token: 0x04000AD3 RID: 2771
	public const string WebPartManager_ConnectionsReadOnly = "The Connections collection of WebPartManager is read-only.";

	// Token: 0x04000AD4 RID: 2772
	public const string WebPartManager_DynamicConnectionsReadOnly = "The DynamicConnections collection of WebPartManager is read-only after connections have been activated.";

	// Token: 0x04000AD5 RID: 2773
	public const string WebPartManager_StaticConnectionsReadOnly = "The StaticConnections collection of WebPartManager is read-only after connections have been activated.";

	// Token: 0x04000AD6 RID: 2774
	public const string WebPartManager_DisplayModesReadOnly = "The collection of DisplayModes on the WebPartManager is read-only.";

	// Token: 0x04000AD7 RID: 2775
	public const string WebPartManager_InvalidDisplayMode = "The specified display mode is not supported on this page. Make sure personalization is enabled and the corresponding zones are present on the page. The display mode can be set during and after Page_Init.";

	// Token: 0x04000AD8 RID: 2776
	public const string WebPartManager_DisabledDisplayMode = "The specified display mode is currently disabled on this page. Make sure personalization is enabled for the current user.";

	// Token: 0x04000AD9 RID: 2777
	public const string WebPartManager_CloseProviderWarning = "The text shown to confirm closing a provider Web Part.";

	// Token: 0x04000ADA RID: 2778
	public const string WebPartManager_DefaultCloseProviderWarning = "You are about to close this Web Part.  It is currently providing data to other Web Parts, and these connections will be deleted if this Web Part is closed.  To close this Web Part, click OK.  To keep this Web Part, click Cancel.";

	// Token: 0x04000ADB RID: 2779
	public const string WebPartManager_DeleteWarning = "The text shown to confirm deleting a Web Part.";

	// Token: 0x04000ADC RID: 2780
	public const string WebPartManager_DefaultDeleteWarning = "You are about to permanently delete this Web Part.  Are you sure you want to do this?  To delete this Web Part, click OK.  To keep this Web Part, click Cancel.";

	// Token: 0x04000ADD RID: 2781
	public const string WebPartManager_CantConnectClosed = "Cannot create a new connection to closed Web Part '{0}'.";

	// Token: 0x04000ADE RID: 2782
	public const string WebPartManager_DuplicateConnectionID = "A Connection has already been added with ID '{0}'.";

	// Token: 0x04000ADF RID: 2783
	public const string WebPartManager_AuthorizeWebPart = "Raised to authorize a WebPart to be displayed in the page.";

	// Token: 0x04000AE0 RID: 2784
	public const string WebPartManager_ConnectionsActivated = "Raised after Connections have been activated.";

	// Token: 0x04000AE1 RID: 2785
	public const string WebPartManager_ConnectionsActivating = "Raised before Connections are activated.";

	// Token: 0x04000AE2 RID: 2786
	public const string WebPartManager_DisplayModeChanged = "Raised after the DisplayMode has been changed.";

	// Token: 0x04000AE3 RID: 2787
	public const string WebPartManager_DisplayModeChanging = "Raised before the DisplayMode is changed.";

	// Token: 0x04000AE4 RID: 2788
	public const string WebPartManager_SelectedWebPartChanged = "Raised after the SelectedWebPart has been changed.";

	// Token: 0x04000AE5 RID: 2789
	public const string WebPartManager_SelectedWebPartChanging = "Raised before the SelectedWebPart is changed.";

	// Token: 0x04000AE6 RID: 2790
	public const string WebPartManager_WebPartAdded = "Raised after a WebPart has been added.";

	// Token: 0x04000AE7 RID: 2791
	public const string WebPartManager_WebPartAdding = "Raised before a WebPart is added.";

	// Token: 0x04000AE8 RID: 2792
	public const string WebPartManager_WebPartClosed = "Raised after a WebPart has been closed.";

	// Token: 0x04000AE9 RID: 2793
	public const string WebPartManager_WebPartClosing = "Raised before a WebPart is closed.";

	// Token: 0x04000AEA RID: 2794
	public const string WebPartManager_WebPartDeleted = "Raised after a WebPart has been deleted.";

	// Token: 0x04000AEB RID: 2795
	public const string WebPartManager_WebPartDeleting = "Raised before a WebPart is deleted.";

	// Token: 0x04000AEC RID: 2796
	public const string WebPartManager_WebPartMoved = "Raised after a WebPart has been moved.";

	// Token: 0x04000AED RID: 2797
	public const string WebPartManager_WebPartMoving = "Raised before a WebPart is moved.";

	// Token: 0x04000AEE RID: 2798
	public const string WebPartManager_WebPartsConnected = "Raised after a new Connection has been established.";

	// Token: 0x04000AEF RID: 2799
	public const string WebPartManager_WebPartsConnecting = "Raised before a new Connection is established.";

	// Token: 0x04000AF0 RID: 2800
	public const string WebPartManager_WebPartsDisconnected = "Raised after a Connection has been disconnected.";

	// Token: 0x04000AF1 RID: 2801
	public const string WebPartManager_WebPartsDisconnecting = "Raised before a Connection is disconnected.";

	// Token: 0x04000AF2 RID: 2802
	public const string WebPartManager_CantDeleteStatic = "Cannot delete a static Web Part.";

	// Token: 0x04000AF3 RID: 2803
	public const string WebPartManager_CantDeleteSharedInUserScope = "Cannot delete a shared Web Part in User personalization scope.";

	// Token: 0x04000AF4 RID: 2804
	public const string WebPartManager_CantAddControlType = "Cannot add a control of Type {0}.  The Type must be loadable by BuildManager.GetType(string typeName).";

	// Token: 0x04000AF5 RID: 2805
	public const string WebPartManager_PathCannotBeEmpty = "The \"path\" argument cannot be empty if the \"type\" argument is UserControl.";

	// Token: 0x04000AF6 RID: 2806
	public const string WebPartManager_PathMustBeEmpty = "The \"path\" argument must be empty if the \"type\" argument is not UserControl.  The \"path\" argument cannot be '{0}'.";

	// Token: 0x04000AF7 RID: 2807
	public const string WebPartManager_CantCreateInstance = "Could not create instance of the required type.";

	// Token: 0x04000AF8 RID: 2808
	public const string WebPartManager_CantCreateInstanceWithType = "Could not create instance of type '{0}'.";

	// Token: 0x04000AF9 RID: 2809
	public const string WebPartManager_TypeMustDeriveFromControl = "The type is not a subclass of Control.";

	// Token: 0x04000AFA RID: 2810
	public const string WebPartManager_TypeMustDeriveFromControlWithType = "The type '{0}' is not a subclass of Control.";

	// Token: 0x04000AFB RID: 2811
	public const string WebPartManager_InvalidPath = "Could not load the required path.";

	// Token: 0x04000AFC RID: 2812
	public const string WebPartManager_InvalidPathWithPath = "Could not load path '{0}'.";

	// Token: 0x04000AFD RID: 2813
	public const string WebPartManager_CantCreateGeneric = "Could not create GenericWebPart.";

	// Token: 0x04000AFE RID: 2814
	public const string WebPartManager_CantBeginConnectingClosed = "Cannot begin connecting a closed WebPart.";

	// Token: 0x04000AFF RID: 2815
	public const string WebPartManager_CantBeginEditingClosed = "Cannot begin editing a closed WebPart.";

	// Token: 0x04000B00 RID: 2816
	public const string WebPartManager_AlreadyClosed = "Cannot close a closed WebPart.";

	// Token: 0x04000B01 RID: 2817
	public const string WebPartManager_CantSetEnableTheming = "Cannot set the EnableTheming property on WebPartManager.  EnableTheming must be true for the WebParts to be themeable.";

	// Token: 0x04000B02 RID: 2818
	public const string WebPartManager_CantConnectToSelf = "A WebPart cannot be connected to itself.";

	// Token: 0x04000B03 RID: 2819
	public const string WebPartManager_ErrorLoadingWebPartType = "Could not load the required type.";

	// Token: 0x04000B04 RID: 2820
	public const string WebPartManagerRequired = "You must enable Web Parts by adding a WebPartManager to your page.  The WebPartManager must be placed before any Web Part controls on the page.";

	// Token: 0x04000B05 RID: 2821
	public const string WebPartMenu_DefaultDropDownAlternateText = "Verbs";

	// Token: 0x04000B06 RID: 2822
	public const string WebPartMenuStyle_ShadowColor = "The color of the shadow below the popup menu.";

	// Token: 0x04000B07 RID: 2823
	public const string WebPartMinimizeVerb_Description = "Minimizes '{0}'";

	// Token: 0x04000B08 RID: 2824
	public const string WebPartMinimizeVerb_Text = "Minimize";

	// Token: 0x04000B09 RID: 2825
	public const string WebPartPersonalization_CannotLoadPersonalization = "Personalization state could not be loaded by the selected personalization provider.";

	// Token: 0x04000B0A RID: 2826
	public const string WebPartPersonalization_CannotEnterSharedScope = "Cannot toggle the page into shared personalization scope. The current user must be granted the right to enter shared personalization scope.";

	// Token: 0x04000B0B RID: 2827
	public const string WebPartPersonalization_CantCallMethodBeforeInit = "The '{0}' method of '{1}' cannot be called before initialization of the page is complete.";

	// Token: 0x04000B0C RID: 2828
	public const string WebPartPersonalization_CantUsePropertyBeforeInit = "The '{0}' property of '{1}' cannot be used before initialization of the page is complete.";

	// Token: 0x04000B0D RID: 2829
	public const string WebPartPersonalization_Enabled = "Whether personalization of Web Parts is enabled.";

	// Token: 0x04000B0E RID: 2830
	public const string WebPartPersonalization_InitialScope = "The initial PersonalizationScope to be used when the page is first requested.";

	// Token: 0x04000B0F RID: 2831
	public const string WebPartPersonalization_MustSetBeforeInit = "The '{0}' property of '{1}' must be set before initialization of the page is complete.";

	// Token: 0x04000B10 RID: 2832
	public const string WebPartPersonalization_PersonalizationNotEnabled = "Personalization is not enabled. The Enabled property must be set to true, a registered personalization provider must be selected, and initialization of the page must be complete.";

	// Token: 0x04000B11 RID: 2833
	public const string WebPartPersonalization_PersonalizationNotModifiable = "Personalization is not enabled and/or modifiable. The Enabled property must be set to true, and a registered personalization provider must be selected. The current user must be granted the right to modify personalization state.";

	// Token: 0x04000B12 RID: 2834
	public const string WebPartPersonalization_PersonalizationStateNotLoaded = "Personalization state has not been loaded.";

	// Token: 0x04000B13 RID: 2835
	public const string WebPartPersonalization_ProviderName = "The name of a registered PersonalizationProvider used to access personalization state.";

	// Token: 0x04000B14 RID: 2836
	public const string WebPartPersonalization_ProviderNotFound = "The specified personalization provider, '{0}', is not registered.";

	// Token: 0x04000B15 RID: 2837
	public const string WebPartPersonalization_SameType = "'{0}' and '{1}' must be the same type.";

	// Token: 0x04000B16 RID: 2838
	public const string WebPartRestoreVerb_Description = "Restores '{0}'";

	// Token: 0x04000B17 RID: 2839
	public const string WebPartRestoreVerb_Text = "Restore";

	// Token: 0x04000B18 RID: 2840
	public const string WebPartTracker_CircularConnection = "The ProviderConnectionPoint '{0}' is involved in a circular connection.";

	// Token: 0x04000B19 RID: 2841
	public const string WebPartVerb_Checked = "Whether the verb is checked.  In a menu a checkmark would appear next to the verb text.";

	// Token: 0x04000B1A RID: 2842
	public const string WebPartVerb_Description = "The description of the verb.  May be displayed in a tooltip.";

	// Token: 0x04000B1B RID: 2843
	public const string WebPartVerb_Enabled = "Whether the verb is enabled.  A disabled verb will be shown but cannot be invoked.";

	// Token: 0x04000B1C RID: 2844
	public const string WebPartVerb_ImageUrl = "The URL of the image to display for the verb.";

	// Token: 0x04000B1D RID: 2845
	public const string WebPartVerb_Text = "The text to be displayed for the verb.";

	// Token: 0x04000B1E RID: 2846
	public const string WebPartVerb_Visible = "Whether the verb is visible.";

	// Token: 0x04000B1F RID: 2847
	public const string WebPartZoneBase_AllowLayoutChange = "Whether Web Parts can be added to, removed from, or moved within the Zone.";

	// Token: 0x04000B20 RID: 2848
	public const string WebPartZoneBase_CloseVerb = "Verb to close a Web Part.";

	// Token: 0x04000B21 RID: 2849
	public const string WebPartZoneBase_ConnectVerb = "Verb to edit the connections of a Web Part.";

	// Token: 0x04000B22 RID: 2850
	public const string WebPartZoneBase_CreateVerbs = "Raised to add verbs to the Web Parts.";

	// Token: 0x04000B23 RID: 2851
	public const string WebPartZoneBase_DefaultEmptyZoneText = "Add a Web Part to this zone by dropping it here.";

	// Token: 0x04000B24 RID: 2852
	public const string WebPartZoneBase_DeleteVerb = "Verb to delete a Web Part.";

	// Token: 0x04000B25 RID: 2853
	public const string WebPartZoneBase_DisplayTitleFallback = "Zone {0}";

	// Token: 0x04000B26 RID: 2854
	public const string WebPartZoneBase_DragHighlightColor = "The color of the Zone's border when a Web Part is dragged over the Zone.";

	// Token: 0x04000B27 RID: 2855
	public const string WebPartZoneBase_EditVerb = "Verb to edit a Web Part.";

	// Token: 0x04000B28 RID: 2856
	public const string WebPartZoneBase_ExportVerb = "Verb to export a Web Part's personalization data.";

	// Token: 0x04000B29 RID: 2857
	public const string WebPartZoneBase_HelpVerb = "Verb to show the help for a Web Part.";

	// Token: 0x04000B2A RID: 2858
	public const string WebPartZoneBase_LayoutOrientation = "Specifies how the Web Parts are arranged within the Zone.";

	// Token: 0x04000B2B RID: 2859
	public const string WebPartZoneBase_MenuPopupStyle = "The style for the Verbs drop-down menu.";

	// Token: 0x04000B2C RID: 2860
	public const string WebPartZoneBase_MenuCheckImageStyle = "The style for the checkmarks in the verbs menu.";

	// Token: 0x04000B2D RID: 2861
	public const string WebPartZoneBase_MenuCheckImageUrl = "The image used to render the checkmarks in the verbs menu dropdown.";

	// Token: 0x04000B2E RID: 2862
	public const string WebPartZoneBase_MenuLabelHoverStyle = "The mouse hover style for the verbs menu label.";

	// Token: 0x04000B2F RID: 2863
	public const string WebPartZoneBase_MenuLabelStyle = "The style for the verbs menu label.";

	// Token: 0x04000B30 RID: 2864
	public const string WebPartZoneBase_MenuLabelText = "The text for the verbs menu label.";

	// Token: 0x04000B31 RID: 2865
	public const string WebPartZoneBase_MenuPopupImageUrl = "The image used to render the verbs menu popup.";

	// Token: 0x04000B32 RID: 2866
	public const string WebPartZoneBase_MenuVerbHoverStyle = "The mouse hover style applied to the verbs within the menu popup.";

	// Token: 0x04000B33 RID: 2867
	public const string WebPartZoneBase_MenuVerbStyle = "The style applied to the verbs within the menu popup.";

	// Token: 0x04000B34 RID: 2868
	public const string WebPartZoneBase_MinimizeVerb = "Verb to minimize a Web Part.";

	// Token: 0x04000B35 RID: 2869
	public const string WebPartZoneBase_RestoreVerb = "Verb to restore a Web Part.";

	// Token: 0x04000B36 RID: 2870
	public const string WebPartZoneBase_SelectedPartChromeStyle = "The style applied to the chrome of the selected Web Part.";

	// Token: 0x04000B37 RID: 2871
	public const string WebPartZoneBase_ShowTitleIcons = "Whether the icon of each Web Part should be displayed in its title bar.";

	// Token: 0x04000B38 RID: 2872
	public const string WebPartZoneBase_TitleBarVerbButtonType = "The type of the verb buttons for each Web Part when rendered in the verb bar.";

	// Token: 0x04000B39 RID: 2873
	public const string WebPartZoneBase_TitleBarVerbStyle = "The style applied to the verbs within the title bar.";

	// Token: 0x04000B3A RID: 2874
	public const string WebPartZoneBase_WebPartVerbRenderMode = "Specifies how the Web Part Verbs will be rendered.";

	// Token: 0x04000B3B RID: 2875
	public const string Zone_AddedTooLate = "A Zone can only be added to the Page in or before the Page_Init event.";

	// Token: 0x04000B3C RID: 2876
	public const string Zone_EmptyZoneText = "The text shown when the Zone is empty.";

	// Token: 0x04000B3D RID: 2877
	public const string Zone_EmptyZoneTextStyle = "The style applied to the EmptyZoneText.";

	// Token: 0x04000B3E RID: 2878
	public const string Zone_ErrorStyle = "The style applied to the error message shown in the Zone.";

	// Token: 0x04000B3F RID: 2879
	public const string Zone_FooterStyle = "Style for the footer of the zone.";

	// Token: 0x04000B40 RID: 2880
	public const string Zone_HeaderStyle = "Style for the header of the zone.";

	// Token: 0x04000B41 RID: 2881
	public const string Zone_HeaderText = "The text in the header of the zone.";

	// Token: 0x04000B42 RID: 2882
	public const string Zone_InvalidParent = "A Zone may not be placed inside a Part or another Zone.";

	// Token: 0x04000B43 RID: 2883
	public const string Zone_Padding = "The padding between Parts in the Zone.";

	// Token: 0x04000B44 RID: 2884
	public const string Zone_PartStyle = "Style for the contained parts.";

	// Token: 0x04000B45 RID: 2885
	public const string Zone_PartChromePadding = "Padding for the chrome of the contained parts.";

	// Token: 0x04000B46 RID: 2886
	public const string Zone_PartChromeStyle = "Style for the chrome of the contained parts.";

	// Token: 0x04000B47 RID: 2887
	public const string Zone_PartChromeType = "The type of chrome for the contained parts.";

	// Token: 0x04000B48 RID: 2888
	public const string Zone_PartTitleStyle = "Style for the title bars of the contained parts.";

	// Token: 0x04000B49 RID: 2889
	public const string Zone_VerbButtonType = "The type of the verb buttons.";

	// Token: 0x04000B4A RID: 2890
	public const string Zone_VerbStyle = "The style applied to the verbs.";

	// Token: 0x04000B4B RID: 2891
	public const string Zone_SampleHeaderText = "Zone Name";

	// Token: 0x04000B4C RID: 2892
	public const string PersonalizationAdmin_UnexpectedResetSharedStateReturnValue = "Unexpected integer value '{0}' is returned when calling provider's ResetState method for resetting shared state with one path. The expected value should be either 0 or 1.";

	// Token: 0x04000B4D RID: 2893
	public const string PersonalizationAdmin_UnexpectedResetUserStateReturnValue = "Unexpected integer value '{0}' is returned when calling provider's ResetState method for resetting user state with one path and one username. The expected value should be either 0 or 1.";

	// Token: 0x04000B4E RID: 2894
	public const string PersonalizationAdmin_UnexpectedPersonalizationProviderReturnValue = "The negative value '{0}' is returned when calling provider's '{1}' method.  The method should return non-negative integer.";

	// Token: 0x04000B4F RID: 2895
	public const string PersonalizationStateInfoCollection_CouldNotAddSharedStateInfo = "Error happened when adding a SharedPersonalizationStateInfo with Path '{0}' to the PersonalizationStateInfoCollection.";

	// Token: 0x04000B50 RID: 2896
	public const string PersonalizationStateInfoCollection_CouldNotAddUserStateInfo = "Error happened when adding a UserPersonalizationStateInfo with Path '{0}' and Username '{1}' to the PersonalizationStateInfoCollection.";

	// Token: 0x04000B51 RID: 2897
	public const string PersonalizationStateQuery_IncorrectValueType = "The query key '{0}' can only be set with value of type {1}.";

	// Token: 0x04000B52 RID: 2898
	public const string PersonalizationProviderHelper_CannotHaveCommaInString = "Input parameter '{0}' cannot have comma in string value '{1}'.";

	// Token: 0x04000B53 RID: 2899
	public const string PersonalizationProviderHelper_Empty_Collection = "Input parameter '{0}' cannot be an empty collection.";

	// Token: 0x04000B54 RID: 2900
	public const string PersonalizationProviderHelper_Invalid_Less_Than_Parameter = "Input parameter '{0}' must be greater than or equal to {1}.";

	// Token: 0x04000B55 RID: 2901
	public const string PersonalizationProviderHelper_More_Than_One_Path = "Input parameter '{0}' cannot contain more than one entry when '{1}' contains some entries.";

	// Token: 0x04000B56 RID: 2902
	public const string PersonalizationProviderHelper_Negative_Integer = "The input parameter cannot be negative.";

	// Token: 0x04000B57 RID: 2903
	public const string PersonalizationProviderHelper_No_Usernames_Set_In_Shared_Scope = "Input parameter '{0}' cannot be provided when '{1}' is set to '{2}'.";

	// Token: 0x04000B58 RID: 2904
	public const string PersonalizationProviderHelper_Null_Entries = "Input parameter '{0}' cannot contain null entries.";

	// Token: 0x04000B59 RID: 2905
	public const string PersonalizationProviderHelper_Null_Or_Empty_String_Entries = "Input parameter '{0}' cannot contain null or empty string entries.";

	// Token: 0x04000B5A RID: 2906
	public const string PersonalizationProviderHelper_TrimmedEmptyString = "Input parameter '{0}' cannot be an empty string.";

	// Token: 0x04000B5B RID: 2907
	public const string PersonalizationProviderHelper_Trimmed_Entry_Value_Exceed_Maximum_Length = "Trimmed entry value '{0}' of input parameter '{1}' cannot exceed character length {2}.";

	// Token: 0x04000B5C RID: 2908
	public const string StringUtil_Trimmed_String_Exceed_Maximum_Length = "Trimmed string value '{0}' of input parameter '{1}' cannot exceed character length {2}.";

	// Token: 0x04000B5D RID: 2909
	public const string Category_Accessibility = "Accessibility";

	// Token: 0x04000B5E RID: 2910
	public const string Category_Cache = "Cache";

	// Token: 0x04000B5F RID: 2911
	public const string Category_Control = "Control";

	// Token: 0x04000B60 RID: 2912
	public const string Category_Databindings = "Databindings";

	// Token: 0x04000B61 RID: 2913
	public const string Category_DefaultProperties = "Default Properties";

	// Token: 0x04000B62 RID: 2914
	public const string Category_Links = "Links";

	// Token: 0x04000B63 RID: 2915
	public const string Category_Navigation = "Navigation";

	// Token: 0x04000B64 RID: 2916
	public const string Category_Paging = "Paging";

	// Token: 0x04000B65 RID: 2917
	public const string Category_Parameter = "Parameter";

	// Token: 0x04000B66 RID: 2918
	public const string Category_Styles = "Styles";

	// Token: 0x04000B67 RID: 2919
	public const string Category_Validation = "Validation";

	// Token: 0x04000B68 RID: 2920
	public const string Category_Verbs = "Verbs";

	// Token: 0x04000B69 RID: 2921
	public const string Category_WebPart = "Web Part";

	// Token: 0x04000B6A RID: 2922
	public const string Category_WebPartAppearance = "Web Part Appearance";

	// Token: 0x04000B6B RID: 2923
	public const string Category_WebPartBehavior = "Web Part Behavior";

	// Token: 0x04000B6C RID: 2924
	public const string Error_Formatter_ASPNET_Error = "Server Error in '{0}' Application.";

	// Token: 0x04000B6D RID: 2925
	public const string Error_Formatter_Description = "Description:";

	// Token: 0x04000B6E RID: 2926
	public const string Error_Formatter_Source_File = "Source File:";

	// Token: 0x04000B6F RID: 2927
	public const string Error_Formatter_No_Source_File = "none";

	// Token: 0x04000B70 RID: 2928
	public const string Error_Formatter_Version = "Version Information:";

	// Token: 0x04000B71 RID: 2929
	public const string Error_Formatter_CLR_Build = "Microsoft .NET Framework Version:";

	// Token: 0x04000B72 RID: 2930
	public const string Error_Formatter_ASPNET_Build = "; ASP.NET Version:";

	// Token: 0x04000B73 RID: 2931
	public const string Error_Formatter_Line = "Line:";

	// Token: 0x04000B74 RID: 2932
	public const string Error_Formatter_FusionLog = "Assembly Load Trace";

	// Token: 0x04000B75 RID: 2933
	public const string Error_Formatter_FusionLogDesc = "The following information can be helpful to determine why the assembly '{0}' could not be loaded.";

	// Token: 0x04000B76 RID: 2934
	public const string Unhandled_Err_Error = "Unhandled Execution Error";

	// Token: 0x04000B77 RID: 2935
	public const string Unhandled_Err_Desc = "An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code.\r\n";

	// Token: 0x04000B78 RID: 2936
	public const string Unhandled_Err_Exception_Details = "Exception Details";

	// Token: 0x04000B79 RID: 2937
	public const string Unhandled_Err_Stack_Trace = "Stack Trace";

	// Token: 0x04000B7A RID: 2938
	public const string Unauthorized_Err_Desc1 = "ASP.NET is not authorized to access the requested resource. Consider granting access rights to the resource to the ASP.NET request identity. ASP.NET has a base process identity (typically {MACHINE}\\ASPNET on IIS 5 or Network Service on IIS 6 and IIS 7, and the configured application pool identity on IIS 7.5) that is used if the application is not impersonating.  If the application is impersonating via <identity impersonate=\"true\"/>, the identity will be the anonymous user (typically IUSR_MACHINENAME) or the authenticated request user.";

	// Token: 0x04000B7B RID: 2939
	public const string Unauthorized_Err_Desc2 = "To grant ASP.NET access to a file, right-click the file in File Explorer, choose \"Properties\" and select the Security tab. Click \"Add\" to add the appropriate user or group. Highlight the ASP.NET account, and check the boxes for the desired access.";

	// Token: 0x04000B7C RID: 2940
	public const string Security_Err_Error = "Security Exception";

	// Token: 0x04000B7D RID: 2941
	public const string Security_Err_Desc = "The application attempted to perform an operation not allowed by the security policy.  To grant this application the required permission please contact your system administrator or change the application's trust level in the configuration file.";

	// Token: 0x04000B7E RID: 2942
	public const string NotFound_Resource_Not_Found = "The resource cannot be found.";

	// Token: 0x04000B7F RID: 2943
	public const string NotFound_Http_404 = "HTTP 404. The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable.  Please review the following URL and make sure that it is spelled correctly.";

	// Token: 0x04000B80 RID: 2944
	public const string NotFound_Requested_Url = "Requested URL";

	// Token: 0x04000B81 RID: 2945
	public const string Forbidden_Type_Not_Served = "This type of page is not served.";

	// Token: 0x04000B82 RID: 2946
	public const string Forbidden_Extension_Incorrect = "The extension '{0}' may be incorrect.";

	// Token: 0x04000B83 RID: 2947
	public const string Forbidden_Extension_Desc = "The type of page you have requested is not served because it has been explicitly forbidden.  {0}   Please review the URL below and make sure that it is spelled correctly.";

	// Token: 0x04000B84 RID: 2948
	public const string Generic_Err_Title = "Runtime Error";

	// Token: 0x04000B85 RID: 2949
	public const string Generic_Err_Local_Desc = "An application error occurred on the server. The current custom error settings for this application prevent the details of the application error from being viewed.";

	// Token: 0x04000B86 RID: 2950
	public const string Generic_Err_Remote_Desc = "An application error occurred on the server. The current custom error settings for this application prevent the details of the application error from being viewed remotely (for security reasons). It could, however, be viewed by browsers running on the local server machine.";

	// Token: 0x04000B87 RID: 2951
	public const string Generic_Err_Details_Title = "Details";

	// Token: 0x04000B88 RID: 2952
	public const string Generic_Err_Local_Details_Desc = "To enable the details of this specific error message to be viewable on the local server machine, please create a <customErrors> tag within a \"web.config\" configuration file located in the root directory of the current web application. This <customErrors> tag should then have its \"mode\" attribute set to \"RemoteOnly\". To enable the details to be viewable on remote machines, please set \"mode\" to \"Off\".";

	// Token: 0x04000B89 RID: 2953
	public const string Generic_Err_Remote_Details_Desc = "To enable the details of this specific error message to be viewable on remote machines, please create a <customErrors> tag within a \"web.config\" configuration file located in the root directory of the current web application. This <customErrors> tag should then have its \"mode\" attribute set to \"Off\".";

	// Token: 0x04000B8A RID: 2954
	public const string Generic_Err_Local_Details_Sample = "<!-- Web.Config Configuration File -->\r\n\r\n<configuration>\r\n    <system.web>\r\n        <customErrors mode=\"RemoteOnly\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000B8B RID: 2955
	public const string Generic_Err_Remote_Details_Sample = "<!-- Web.Config Configuration File -->\r\n\r\n<configuration>\r\n    <system.web>\r\n        <customErrors mode=\"Off\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000B8C RID: 2956
	public const string Generic_Err_Notes_Title = "Notes";

	// Token: 0x04000B8D RID: 2957
	public const string Generic_Err_Notes_Desc = "The current error page you are seeing can be replaced by a custom error page by modifying the \"defaultRedirect\" attribute of the application's <customErrors> configuration tag to point to a custom error page URL.";

	// Token: 0x04000B8E RID: 2958
	public const string Generic_Err_Local_Notes_Sample = "<!-- Web.Config Configuration File -->\r\n\r\n<configuration>\r\n    <system.web>\r\n        <customErrors mode=\"On\" defaultRedirect=\"mycustompage.htm\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000B8F RID: 2959
	public const string Generic_Err_Remote_Notes_Sample = "<!-- Web.Config Configuration File -->\r\n\r\n<configuration>\r\n    <system.web>\r\n        <customErrors mode=\"RemoteOnly\" defaultRedirect=\"mycustompage.htm\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000B90 RID: 2960
	public const string CustomErrorFailed_Err_Desc = "An exception occurred while processing your request. Additionally, another exception occurred while executing the custom error page for the first exception. The request has been terminated.";

	// Token: 0x04000B91 RID: 2961
	public const string WithFile_No_Relevant_Line = "[No relevant source lines]";

	// Token: 0x04000B92 RID: 2962
	public const string Src_not_available = "The source code that generated this unhandled exception can only be shown when compiled in debug mode. To enable this, please follow one of the below steps, then request the URL:\r\n\r\n1. Add a \"Debug=true\" directive at the top of the file that generated the error. Example:\r\n\r\n{0}   <%@ Page Language=\"C#\" Debug=\"true\" %>{1}\r\n\r\nor:\r\n\r\n2) Add the following section to the configuration file of your application:\r\n\r\n{2}<configuration>\r\n    <system.web>\r\n        <compilation debug=\"true\"/>\r\n    </system.web>\r\n</configuration>{3}\r\n\r\n Note that this second technique will cause all files within a given application to be compiled in debug mode. The first technique will cause only that particular file to be compiled in debug mode.\r\n\r\nImportant: Running applications in debug mode does incur a memory/performance overhead. You should make sure that an application has debugging disabled before deploying into production scenario.";

	// Token: 0x04000B93 RID: 2963
	public const string Src_not_available_nodebug = "An unhandled exception was generated during the execution of the current web request. Information regarding the origin and location of the exception can be identified using the exception stack trace below.";

	// Token: 0x04000B94 RID: 2964
	public const string WithFile_Line_Num = "Line {0}:";

	// Token: 0x04000B95 RID: 2965
	public const string TmplCompilerErrorTitle = "Compilation Error";

	// Token: 0x04000B96 RID: 2966
	public const string TmplCompilerErrorDesc = "An error occurred during the compilation of a resource required to service this request. Please review the following specific error details and modify your source code appropriately.";

	// Token: 0x04000B97 RID: 2967
	public const string TmplCompilerCompleteOutput = "Show Detailed Compiler Output";

	// Token: 0x04000B98 RID: 2968
	public const string TmplCompilerGeneratedFile = "Show Complete Compilation Source";

	// Token: 0x04000B99 RID: 2969
	public const string TmplConfigurationAdditionalError = "Show Additional Configuration Errors";

	// Token: 0x04000B9A RID: 2970
	public const string TmplCompilerErrorSecTitle = "Compiler Error Message";

	// Token: 0x04000B9B RID: 2971
	public const string TmplCompilerFatalError = "The compiler failed with error code {0}.";

	// Token: 0x04000B9C RID: 2972
	public const string TmplCompilerWarningBanner = "Compiler Warning Messages";

	// Token: 0x04000B9D RID: 2973
	public const string TmplCompilerWarningSecTitle = "Warning";

	// Token: 0x04000B9E RID: 2974
	public const string TmplCompilerSourceSecTitle = "Source Error";

	// Token: 0x04000B9F RID: 2975
	public const string TmplCompilerSourceFileTitle = "Source File";

	// Token: 0x04000BA0 RID: 2976
	public const string TmplCompilerSourceFileLine = "Line";

	// Token: 0x04000BA1 RID: 2977
	public const string TmplCompilerLineHeader = "Line {0}:";

	// Token: 0x04000BA2 RID: 2978
	public const string Parser_Error = "Parser Error";

	// Token: 0x04000BA3 RID: 2979
	public const string Parser_Desc = "An error occurred during the parsing of a resource required to service this request.   Please review the following specific parse error details and modify your source file appropriately.";

	// Token: 0x04000BA4 RID: 2980
	public const string Parser_Error_Message = "Parser Error Message";

	// Token: 0x04000BA5 RID: 2981
	public const string Parser_Source_Error = "Source Error";

	// Token: 0x04000BA6 RID: 2982
	public const string Config_Error = "Configuration Error";

	// Token: 0x04000BA7 RID: 2983
	public const string Config_Desc = "An error occurred during the processing of a configuration file required to service this request. Please review the specific error details below and modify your configuration file appropriately.";

	// Token: 0x04000BA8 RID: 2984
	public const string File_Circular_Reference = "{0} has a circular reference!";

	// Token: 0x04000BA9 RID: 2985
	public const string CantGenPropertySet = "Unable to generate code for a value of type '{1}'. This error occurred while trying to generate the property value for {0}.";

	// Token: 0x04000BAA RID: 2986
	public const string Trace_Request = "Request";

	// Token: 0x04000BAB RID: 2987
	public const string Trace_Status_Code = "Status Code";

	// Token: 0x04000BAC RID: 2988
	public const string Trace_Trace_Information = "Trace Information";

	// Token: 0x04000BAD RID: 2989
	public const string Trace_Category = "Category";

	// Token: 0x04000BAE RID: 2990
	public const string Trace_From_First = "From First(s)";

	// Token: 0x04000BAF RID: 2991
	public const string Trace_Message = "Message";

	// Token: 0x04000BB0 RID: 2992
	public const string Trace_Warning = "Warning";

	// Token: 0x04000BB1 RID: 2993
	public const string Trace_From_Last = "From Last(s)";

	// Token: 0x04000BB2 RID: 2994
	public const string Trace_Control_Tree = "Control Tree";

	// Token: 0x04000BB3 RID: 2995
	public const string Trace_Control_Id = "Control UniqueID";

	// Token: 0x04000BB4 RID: 2996
	public const string Trace_Parent_Id = "Parent Id";

	// Token: 0x04000BB5 RID: 2997
	public const string Trace_Type = "Type";

	// Token: 0x04000BB6 RID: 2998
	public const string Trace_Viewstate_Size = "ViewState Size";

	// Token: 0x04000BB7 RID: 2999
	public const string Trace_Controlstate_Size = "ControlState Size";

	// Token: 0x04000BB8 RID: 3000
	public const string Trace_Render_Size = "Render Size";

	// Token: 0x04000BB9 RID: 3001
	public const string Trace_Session_State = "Session State";

	// Token: 0x04000BBA RID: 3002
	public const string Trace_Application_State = "Application State";

	// Token: 0x04000BBB RID: 3003
	public const string Trace_Request_Cookies_Collection = "Request Cookies Collection";

	// Token: 0x04000BBC RID: 3004
	public const string Trace_Response_Cookies_Collection = "Response Cookies Collection";

	// Token: 0x04000BBD RID: 3005
	public const string Trace_Headers_Collection = "Headers Collection";

	// Token: 0x04000BBE RID: 3006
	public const string Trace_Response_Headers_Collection = "Response Headers Collection";

	// Token: 0x04000BBF RID: 3007
	public const string Trace_Form_Collection = "Form Collection";

	// Token: 0x04000BC0 RID: 3008
	public const string Trace_Querystring_Collection = "Querystring Collection";

	// Token: 0x04000BC1 RID: 3009
	public const string Trace_Server_Variables = "Server Variables";

	// Token: 0x04000BC2 RID: 3010
	public const string Trace_Time_of_Request = "Time of Request";

	// Token: 0x04000BC3 RID: 3011
	public const string Trace_Url = "URL";

	// Token: 0x04000BC4 RID: 3012
	public const string Trace_Request_Type = "Request Type";

	// Token: 0x04000BC5 RID: 3013
	public const string Trace_Request_Encoding = "Request Encoding";

	// Token: 0x04000BC6 RID: 3014
	public const string Trace_Name = "Name";

	// Token: 0x04000BC7 RID: 3015
	public const string Trace_Value = "Value";

	// Token: 0x04000BC8 RID: 3016
	public const string Trace_Response_Encoding = "Response Encoding";

	// Token: 0x04000BC9 RID: 3017
	public const string Trace_Session_Id = "Session Id";

	// Token: 0x04000BCA RID: 3018
	public const string Trace_No = "No.";

	// Token: 0x04000BCB RID: 3019
	public const string Trace_Application_Key = "Application Key";

	// Token: 0x04000BCC RID: 3020
	public const string Trace_Session_Key = "Session Key";

	// Token: 0x04000BCD RID: 3021
	public const string Trace_Size = "Size";

	// Token: 0x04000BCE RID: 3022
	public const string Trace_Request_Details = "Request Details";

	// Token: 0x04000BCF RID: 3023
	public const string Trace_Application_Trace = "<h1>Application Trace</h1>";

	// Token: 0x04000BD0 RID: 3024
	public const string Trace_Clear_Current = "clear current trace";

	// Token: 0x04000BD1 RID: 3025
	public const string Trace_Physical_Directory = "Physical Directory:";

	// Token: 0x04000BD2 RID: 3026
	public const string Trace_Requests_This = "Requests to this Application";

	// Token: 0x04000BD3 RID: 3027
	public const string Trace_Remaining = "Remaining:";

	// Token: 0x04000BD4 RID: 3028
	public const string Trace_File = "File";

	// Token: 0x04000BD5 RID: 3029
	public const string Trace_Verb = "Verb";

	// Token: 0x04000BD6 RID: 3030
	public const string Trace_View_Details = "View Details";

	// Token: 0x04000BD7 RID: 3031
	public const string Trace_Render_Size_children = "Render Size Bytes (including children)";

	// Token: 0x04000BD8 RID: 3032
	public const string Trace_Viewstate_Size_Nochildren = "ViewState Size Bytes (excluding children)";

	// Token: 0x04000BD9 RID: 3033
	public const string Trace_Controlstate_Size_Nochildren = "ControlState Size Bytes (excluding children)";

	// Token: 0x04000BDA RID: 3034
	public const string Trace_Page = "Page";

	// Token: 0x04000BDB RID: 3035
	public const string Trace_Error_Title = "Trace Error";

	// Token: 0x04000BDC RID: 3036
	public const string Trace_Error_LocalOnly_Description = "The current trace settings prevent trace.axd from being viewed remotely (for security reasons).  It could, however, be viewed by browsers running on the local server machine.";

	// Token: 0x04000BDD RID: 3037
	public const string Trace_Error_LocalOnly_Details_Desc = "To enable trace.axd to be viewable on remote machines, please create a <trace> tag within the configuration file located in the root directory of the current web application. This <trace> tag should then have its \"localOnly\" attribute set to \"false\".";

	// Token: 0x04000BDE RID: 3038
	public const string Trace_Error_LocalOnly_Details_Sample = "<configuration>\r\n    <system.web>\r\n        <trace localOnly=\"false\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000BDF RID: 3039
	public const string Trace_Error_Enabled_Description = "Trace.axd is not enabled in the configuration file for this application.  Note: Trace is never enabled when <deployment retail=true />";

	// Token: 0x04000BE0 RID: 3040
	public const string Trace_Error_Enabled_Details_Desc = "To enable trace.axd, please create a <trace> tag within the configuration file located in the root directory of the current web application.  This <trace> tag should then have its \"enabled\" attribute set to \"true\".";

	// Token: 0x04000BE1 RID: 3041
	public const string Trace_Error_Enabled_Details_Sample = "<configuration>\r\n    <system.web>\r\n        <trace enabled=\"true\"/>\r\n    </system.web>\r\n</configuration>";

	// Token: 0x04000BE2 RID: 3042
	public const string WebPageTraceListener_Event = "Event";

	// Token: 0x04000BE3 RID: 3043
	public const string Adapter_GoLabel = "Go";

	// Token: 0x04000BE4 RID: 3044
	public const string Adapter_OKLabel = "OK";

	// Token: 0x04000BE5 RID: 3045
	public const string MenuAdapter_Up = "Up";

	// Token: 0x04000BE6 RID: 3046
	public const string MenuAdapter_UpOneLevel = "^Up One Level";

	// Token: 0x04000BE7 RID: 3047
	public const string MenuAdapter_Expand = "Expand {0}";

	// Token: 0x04000BE8 RID: 3048
	public const string PageAdapter_MustHaveFormRunatServer = "To adaptively render for this device, the page must have a form tag with runat=server.";

	// Token: 0x04000BE9 RID: 3049
	public const string PageAdapter_RenderDelegateMustBeInServerForm = "To adaptively render for this device, the delimiters <% %> or <%= %> must be within a form element with runat=server.";

	// Token: 0x04000BEA RID: 3050
	public const string SQL_Services_Database_Empty_Or_Space_Only_Arg = "The database name cannot be empty or contain only white space characters.";

	// Token: 0x04000BEB RID: 3051
	public const string SQL_Services_Cant_connect_sql_database = "Unable to connect to SQL Server database.";

	// Token: 0x04000BEC RID: 3052
	public const string SQL_Services_Invalid_Feature = "An invalid feature is requested.";

	// Token: 0x04000BED RID: 3053
	public const string SQL_Services_Error_Deleting_Session_Job = "The attempt to remove the Session State expired sessions job from msdb did not succeed.  This can occur either because the job no longer exists, or because the job was originally created with a different user account than the account that is currently performing the uninstall.  You will need to manually delete the Session State expired sessions job if it still exists.\"";

	// Token: 0x04000BEE RID: 3054
	public const string SQL_Services_Error_Executing_Command = "An error occurred during the execution of the SQL file '{0}'. The SQL error number is {1} and the SqlException message is: {2}";

	// Token: 0x04000BEF RID: 3055
	public const string SQL_Services_Error_Cant_Uninstall_Nonempty_Table = "Cannot uninstall the specified feature(s) because the SQL table '{0}' in the database '{1}' is not empty. You must first remove all rows from the table.";

	// Token: 0x04000BF0 RID: 3056
	public const string SQL_Services_Error_Cant_Uninstall_Nonexisting_Database = "Cannot uninstall the specified feature(s) because the SQL database '{0}' does not exist.";

	// Token: 0x04000BF1 RID: 3057
	public const string SQL_Services_Error_Cant_use_custom_database = "You cannot specify the database name because it is allowed only if the session state type is SessionStateType.Custom.";

	// Token: 0x04000BF2 RID: 3058
	public const string SQL_Services_Error_missing_custom_database = "The database name cannot be null or empty if the session state type is SessionStateType.Custom.";

	// Token: 0x04000BF3 RID: 3059
	public const string SQL_Services_Database_contains_invalid_chars = "The custom database name cannot contain the following three characters: single quotation mark ('), left bracket ([) or right bracket (]).";

	// Token: 0x04000BF4 RID: 3060
	public const string Provider_missing_attribute = "The attribute '{0}' is missing in the configuration of the '{1}' provider.";

	// Token: 0x04000BF5 RID: 3061
	public const string Invalid_provider_attribute = "The value '{2}' specified for the attribute '{0}' is invalid in the configuration of the '{1}' provider.";

	// Token: 0x04000BF6 RID: 3062
	public const string Invalid_mail_template_provider_attribute = "The value '{2}' specified for the attribute '{0}' is invalid in the configuration of the '{1}' provider. Only application relative URLs (~/url) are allowed.";

	// Token: 0x04000BF7 RID: 3063
	public const string Unexpected_provider_attribute = "The attribute '{0}' is unexpected in the configuration of the '{1}' provider.";

	// Token: 0x04000BF8 RID: 3064
	public const string Invalid_provider_positive_attributes = "The attribute '{0}' is invalid in the configuration of the '{1}' provider. The attribute must be set to a non-negative integer.";

	// Token: 0x04000BF9 RID: 3065
	public const string Invalid_provider_non_zero_positive_attributes = "The attribute '{0}' is invalid in the configuration of the '{1}' provider. The attribute must be greater than zero.";

	// Token: 0x04000BFA RID: 3066
	public const string Event_name_not_found = "The event name '{0}' is not found.";

	// Token: 0x04000BFB RID: 3067
	public const string Event_name_invalid_code_range = "The 'startEventCode' and 'endEventCode' attributes are invalid. 'startEventCode' must be equal or less than 'endEventCode'.";

	// Token: 0x04000BFC RID: 3068
	public const string Health_mon_profile_not_found = "The profile '{0}' is not found.";

	// Token: 0x04000BFD RID: 3069
	public const string Health_mon_provider_not_found = "The provider '{0}' is not found.";

	// Token: 0x04000BFE RID: 3070
	public const string Wmi_provider_cant_initialize = "Cannot initialize WMI event provider. Error code:{0}.";

	// Token: 0x04000BFF RID: 3071
	public const string Invalid_max_event_details_length = "The value '{1}' specified for the maxEventDetailsLength attribute of the '{0}' provider is invalid. It should be between 0 and 1073741823.";

	// Token: 0x04000C00 RID: 3072
	public const string Health_mon_buffer_mode_not_found = "The buffer mode '{0}' is not found.";

	// Token: 0x04000C01 RID: 3073
	public const string Invalid_attribute1_must_less_than_or_equal_attribute2 = "The value '{0}' specified for the {1} attribute must be less than or equal to the value '{2}' specified for the {3} attribute.";

	// Token: 0x04000C02 RID: 3074
	public const string Invalid_attribute1_must_less_than_attribute2 = "The value '{0}' specified for the {1} attribute must be less than the value '{2}' specified for the {3} attribute.";

	// Token: 0x04000C03 RID: 3075
	public const string MailWebEventProvider_discard_warning = "{1} events were discarded since last notification was made at {2} because the event buffer capacity was exceeded. (Warning ID: {0})";

	// Token: 0x04000C04 RID: 3076
	public const string MailWebEventProvider_events_drop_warning = "The {1} events remaining for this notification period will be discarded because the maximum number of messages allowed per notification was exceeded. (Warning ID: {0})";

	// Token: 0x04000C05 RID: 3077
	public const string MailWebEventProvider_summary_body = "This message contains events {0} to {1} from the total of {2} events scheduled for this notification.  There were {3} events left in the buffer at the beginning of this notification.";

	// Token: 0x04000C06 RID: 3078
	public const string WebEvent_event_email_subject = "Event Notification {0}, part {1}: {2}{3} event received in {4}";

	// Token: 0x04000C07 RID: 3079
	public const string WebEvent_event_group_email_subject = "Event Notification {0}, part {1}: {2}{3} events received in {4}";

	// Token: 0x04000C08 RID: 3080
	public const string WebEvent_event_email_subject_template_error = "Event Notification {0}, part {1}: {2}error in notification template";

	// Token: 0x04000C09 RID: 3081
	public const string MailWebEventProvider_Warnings = "** Warnings **";

	// Token: 0x04000C0A RID: 3082
	public const string MailWebEventProvider_Summary = "** Summary **";

	// Token: 0x04000C0B RID: 3083
	public const string MailWebEventProvider_Application_Info = "** Application Information **";

	// Token: 0x04000C0C RID: 3084
	public const string MailWebEventProvider_Events = "** Events **";

	// Token: 0x04000C0D RID: 3085
	public const string MailWebEventProvider_template_file_not_found_error = "The template file to be used for creating this event notification is not found.  The {0} events that were part of this message were discarded.";

	// Token: 0x04000C0E RID: 3086
	public const string MailWebEventProvider_template_runtime_error = "An unhandled exception occurred during the execution of the template page used to create this event notification.  The {0} events that were part of this message were discarded.";

	// Token: 0x04000C0F RID: 3087
	public const string MailWebEventProvider_template_compile_error = "An error occurred during the compilation of the template page used to create this event notification.  The {0} events that were part of this notification were discarded.";

	// Token: 0x04000C10 RID: 3088
	public const string MailWebEventProvider_template_error_no_details = "The current configuration prevents the exception details from being included in this message.  Add the \"detailedTemplateErrors=true\" attribute to the provider configuration to enable exception details to be reported.";

	// Token: 0x04000C11 RID: 3089
	public const string MailWebEventProvider_no_recipient_error = "No recipients have been specified for the {0} instance named {1}.  If you would like to disable this provider, please remove it from the providers collection.";

	// Token: 0x04000C12 RID: 3090
	public const string Sql_webevent_provider_events_dropped = "{0} events were discarded since last notification was made at {1} because the event buffer capacity was exceeded.";

	// Token: 0x04000C13 RID: 3091
	public const string MailWebEventProvider_cannot_send_mail = "Unable to send out an e-mail to the SMTP server. Please ensure that the server specified in the <smtpMail> section is valid.";

	// Token: 0x04000C14 RID: 3092
	public const string Invalid_eventCode_error = "The eventCode of a WebBaseEvent object must be a non-negative integer.";

	// Token: 0x04000C15 RID: 3093
	public const string Invalid_eventDetailCode_error = "The eventDetailCode of a WebBaseEvent object must be a non-negative integer.";

	// Token: 0x04000C16 RID: 3094
	public const string System_eventCode_not_allowed = "The event code {0} is invalid. Event codes less than {1} are reserved for ASP.NET.";

	// Token: 0x04000C17 RID: 3095
	public const string Event_log_provider_error = "The EventLogWebEventProvider provider failed to log an event with the error code {0}.";

	// Token: 0x04000C18 RID: 3096
	public const string Wmi_provider_error = "The WmiWebEventProvider provider failed to raise a WMI event with the error code {0}.";

	// Token: 0x04000C19 RID: 3097
	public const string Webevent_msg_ApplicationStart = "Application is starting.";

	// Token: 0x04000C1A RID: 3098
	public const string Webevent_msg_ApplicationShutdown = "Application is shutting down.";

	// Token: 0x04000C1B RID: 3099
	public const string Webevent_msg_ApplicationCompilationStart = "Application compilation is starting.";

	// Token: 0x04000C1C RID: 3100
	public const string Webevent_msg_ApplicationCompilationEnd = "Application compilation finished.";

	// Token: 0x04000C1D RID: 3101
	public const string Webevent_msg_ApplicationHeartbeat = "Application heartbeat.";

	// Token: 0x04000C1E RID: 3102
	public const string Webevent_msg_RequestTransactionComplete = "Request transaction is complete.";

	// Token: 0x04000C1F RID: 3103
	public const string Webevent_msg_RequestTransactionAbort = "Request transaction was aborted.";

	// Token: 0x04000C20 RID: 3104
	public const string Webevent_msg_RuntimeErrorRequestAbort = "The request has been aborted.";

	// Token: 0x04000C21 RID: 3105
	public const string Webevent_msg_RuntimeErrorViewStateFailure = "An error occurred while processing viewstate.";

	// Token: 0x04000C22 RID: 3106
	public const string Webevent_msg_RuntimeErrorValidationFailure = "A validation error has occurred.";

	// Token: 0x04000C23 RID: 3107
	public const string Webevent_msg_RuntimeErrorPostTooLarge = "Post size exceeded allowed limits.";

	// Token: 0x04000C24 RID: 3108
	public const string Webevent_msg_RuntimeErrorUnhandledException = "An unhandled exception has occurred.";

	// Token: 0x04000C25 RID: 3109
	public const string Webevent_msg_RuntimeErrorWebResourceFailure_DecryptionError = "An error occurred processing a web or script resource request. The resource identifier failed to decrypt.";

	// Token: 0x04000C26 RID: 3110
	public const string Webevent_msg_RuntimeErrorWebResourceFailure_ResourceMissing = "An error occurred processing a web or script resource request. The requested resource '{0}' does not exist or there was a problem loading it.";

	// Token: 0x04000C27 RID: 3111
	public const string Webevent_msg_WebErrorParserError = "A parser error has occurred.";

	// Token: 0x04000C28 RID: 3112
	public const string Webevent_msg_WebErrorCompilationError = "A compilation error has occurred.";

	// Token: 0x04000C29 RID: 3113
	public const string Webevent_msg_WebErrorConfigurationError = "A configuration error has occurred.";

	// Token: 0x04000C2A RID: 3114
	public const string Webevent_msg_AuditUnhandledSecurityException = "An unhandled security exception has occurred.";

	// Token: 0x04000C2B RID: 3115
	public const string Webevent_msg_AuditInvalidViewStateFailure = "Viewstate verification failed.";

	// Token: 0x04000C2C RID: 3116
	public const string Webevent_msg_AuditFormsAuthenticationSuccess = "Forms authentication succeeded for the request.";

	// Token: 0x04000C2D RID: 3117
	public const string Webevent_msg_AuditUrlAuthorizationSuccess = "URL authorization succeeded for the request.";

	// Token: 0x04000C2E RID: 3118
	public const string Webevent_msg_AuditFileAuthorizationFailure = "File authorization failed for the request.";

	// Token: 0x04000C2F RID: 3119
	public const string Webevent_msg_AuditFormsAuthenticationFailure = "Forms authentication failed for the request.";

	// Token: 0x04000C30 RID: 3120
	public const string Webevent_msg_AuditFileAuthorizationSuccess = "File authorization succeeded for the request.";

	// Token: 0x04000C31 RID: 3121
	public const string Webevent_msg_AuditMembershipAuthenticationSuccess = "Membership credential verification succeeded.";

	// Token: 0x04000C32 RID: 3122
	public const string Webevent_msg_AuditMembershipAuthenticationFailure = "Membership credential verification failed.";

	// Token: 0x04000C33 RID: 3123
	public const string Webevent_msg_AuditUrlAuthorizationFailure = "URL authorization failed for the request.";

	// Token: 0x04000C34 RID: 3124
	public const string Webevent_msg_AuditUnhandledAccessException = "An unhandled access exception has occurred.";

	// Token: 0x04000C35 RID: 3125
	public const string Webevent_msg_OSF_Deserialization_String = "A deserialization error occurred inside of ObjectStateFormatter.  Deserialization was attempted using a string TypeConverter.  The type of the property that failed to deserialize is '{0}'.";

	// Token: 0x04000C36 RID: 3126
	public const string Webevent_msg_OSF_Deserialization_Binary = "A deserialization error occurred inside of ObjectStateFormatter.  Deserialization was attempted using binary serialization.";

	// Token: 0x04000C37 RID: 3127
	public const string Webevent_msg_OSF_Deserialization_Type = "A deserialization error occurred inside of ObjectStateFormatter.  A property was typed as \"Type\" but the Type instance could not be created for '{0}'.";

	// Token: 0x04000C38 RID: 3128
	public const string Webevent_msg_Property_Deserialization = "Deserialization of property '{0}' failed.  The serialization setting for this property was '{1}'.  The type of this property is currently defined as '{2}'.";

	// Token: 0x04000C39 RID: 3129
	public const string Webevent_detail_ApplicationShutdownUnknown = "Reason: Unknown.";

	// Token: 0x04000C3A RID: 3130
	public const string Webevent_detail_ApplicationShutdownHostingEnvironment = "Reason: Hosting environment is shutting down.";

	// Token: 0x04000C3B RID: 3131
	public const string Webevent_detail_ApplicationShutdownChangeInGlobalAsax = "Reason: Global.asax changed.";

	// Token: 0x04000C3C RID: 3132
	public const string Webevent_detail_ApplicationShutdownConfigurationChange = "Reason: Configuration changed.";

	// Token: 0x04000C3D RID: 3133
	public const string Webevent_detail_ApplicationShutdownUnloadAppDomainCalled = "Reason: Appdomain was explicitly unloaded.";

	// Token: 0x04000C3E RID: 3134
	public const string Webevent_detail_ApplicationShutdownChangeInSecurityPolicyFile = "Reason: Security policy file changed.";

	// Token: 0x04000C3F RID: 3135
	public const string Webevent_detail_ApplicationShutdownBinDirChangeOrDirectoryRename = "Reason: A subdirectory in the Bin application directory was changed or renamed.";

	// Token: 0x04000C40 RID: 3136
	public const string Webevent_detail_ApplicationShutdownBrowsersDirChangeOrDirectoryRename = "Reason: A subdirectory in the Browsers application directory was changed or renamed.";

	// Token: 0x04000C41 RID: 3137
	public const string Webevent_detail_ApplicationShutdownCodeDirChangeOrDirectoryRename = "Reason: A subdirectory in the Code application directory was changed or renamed.";

	// Token: 0x04000C42 RID: 3138
	public const string Webevent_detail_ApplicationShutdownResourcesDirChangeOrDirectoryRename = "Reason: A subdirectory in the Resources application directory was changed or renamed.";

	// Token: 0x04000C43 RID: 3139
	public const string Webevent_detail_ApplicationShutdownIdleTimeout = "Reason: The idle timeout was exceeded.";

	// Token: 0x04000C44 RID: 3140
	public const string Webevent_detail_ApplicationShutdownPhysicalApplicationPathChanged = "Reason: The physical path of the application changed.";

	// Token: 0x04000C45 RID: 3141
	public const string Webevent_detail_ApplicationShutdownHttpRuntimeClose = "Reason: HttpRuntime was explicitly closed.";

	// Token: 0x04000C46 RID: 3142
	public const string Webevent_detail_ApplicationShutdownInitializationError = "Reason: Initialization error.";

	// Token: 0x04000C47 RID: 3143
	public const string Webevent_detail_ApplicationShutdownMaxRecompilationsReached = "Reason: Maximum number of recompilations was reached.";

	// Token: 0x04000C48 RID: 3144
	public const string Webevent_detail_ApplicationShutdownBuildManagerChange = "Reason: The BuildManager has made a change that requires the AppDomain to be shutdown.";

	// Token: 0x04000C49 RID: 3145
	public const string Webevent_detail_StateServerConnectionError = "Reason: An error occurred while communicating with the state server.";

	// Token: 0x04000C4A RID: 3146
	public const string Webevent_detail_InvalidTicketFailure = "Reason: The ticket supplied was invalid.";

	// Token: 0x04000C4B RID: 3147
	public const string Webevent_detail_ExpiredTicketFailure = "Reason: The ticket supplied has expired.";

	// Token: 0x04000C4C RID: 3148
	public const string Webevent_detail_InvalidViewStateMac = "Reason: The viewstate supplied failed integrity check.";

	// Token: 0x04000C4D RID: 3149
	public const string Webevent_detail_InvalidViewState = "Reason: Viewstate was invalid.";

	// Token: 0x04000C4E RID: 3150
	public const string Webevent_detail_SqlProviderEventsDropped = "Reason: Sql web event provider dropped events.";

	// Token: 0x04000C4F RID: 3151
	public const string Webevent_event_code = "Event code: {0}";

	// Token: 0x04000C50 RID: 3152
	public const string Webevent_event_message = "Event message: {0}";

	// Token: 0x04000C51 RID: 3153
	public const string Webevent_event_time = "Event time: {0}";

	// Token: 0x04000C52 RID: 3154
	public const string Webevent_event_time_Utc = "Event time (UTC): {0}";

	// Token: 0x04000C53 RID: 3155
	public const string Webevent_event_sequence = "Event sequence: {0}";

	// Token: 0x04000C54 RID: 3156
	public const string Webevent_event_occurrence = "Event occurrence: {0}";

	// Token: 0x04000C55 RID: 3157
	public const string Webevent_event_id = "Event ID: {0}";

	// Token: 0x04000C56 RID: 3158
	public const string Webevent_event_detail_code = "Event detail code: {0}";

	// Token: 0x04000C57 RID: 3159
	public const string Webevent_event_process_information = "Process information:";

	// Token: 0x04000C58 RID: 3160
	public const string Webevent_event_application_information = "Application information:";

	// Token: 0x04000C59 RID: 3161
	public const string Webevent_event_process_statistics = "Process statistics:";

	// Token: 0x04000C5A RID: 3162
	public const string Webevent_event_request_information = "Request information:";

	// Token: 0x04000C5B RID: 3163
	public const string Webevent_event_exception_information = "Exception information:";

	// Token: 0x04000C5C RID: 3164
	public const string Webevent_event_inner_exception_information = "Inner exception information (level {0}):";

	// Token: 0x04000C5D RID: 3165
	public const string Webevent_event_exception_type = "Exception type: {0}";

	// Token: 0x04000C5E RID: 3166
	public const string Webevent_event_exception_message = "Exception message: {0}";

	// Token: 0x04000C5F RID: 3167
	public const string Webevent_event_thread_information = "Thread information:";

	// Token: 0x04000C60 RID: 3168
	public const string Webevent_event_process_id = "Process ID: {0}";

	// Token: 0x04000C61 RID: 3169
	public const string Webevent_event_process_name = "Process name: {0}";

	// Token: 0x04000C62 RID: 3170
	public const string Webevent_event_account_name = "Account name: {0}";

	// Token: 0x04000C63 RID: 3171
	public const string Webevent_event_machine_name = "Machine name: {0}";

	// Token: 0x04000C64 RID: 3172
	public const string Webevent_event_application_domain = "Application domain: {0}";

	// Token: 0x04000C65 RID: 3173
	public const string Webevent_event_trust_level = "Trust level: {0}";

	// Token: 0x04000C66 RID: 3174
	public const string Webevent_event_application_virtual_path = "Application Virtual Path: {0}";

	// Token: 0x04000C67 RID: 3175
	public const string Webevent_event_application_path = "Application Path: {0}";

	// Token: 0x04000C68 RID: 3176
	public const string Webevent_event_request_url = "Request URL: {0}";

	// Token: 0x04000C69 RID: 3177
	public const string Webevent_event_request_path = "Request path: {0}";

	// Token: 0x04000C6A RID: 3178
	public const string Webevent_event_user = "User: {0}";

	// Token: 0x04000C6B RID: 3179
	public const string Webevent_event_is_authenticated = "Is authenticated: True";

	// Token: 0x04000C6C RID: 3180
	public const string Webevent_event_is_not_authenticated = "Is authenticated: False";

	// Token: 0x04000C6D RID: 3181
	public const string Webevent_event_authentication_type = "Authentication Type: {0}";

	// Token: 0x04000C6E RID: 3182
	public const string Webevent_event_process_start_time = "Process start time: {0}";

	// Token: 0x04000C6F RID: 3183
	public const string Webevent_event_thread_count = "Thread count: {0}";

	// Token: 0x04000C70 RID: 3184
	public const string Webevent_event_working_set = "Working set: {0} bytes";

	// Token: 0x04000C71 RID: 3185
	public const string Webevent_event_peak_working_set = "Peak working set: {0} bytes";

	// Token: 0x04000C72 RID: 3186
	public const string Webevent_event_managed_heap_size = "Managed heap size: {0} bytes";

	// Token: 0x04000C73 RID: 3187
	public const string Webevent_event_application_domain_count = "Application domain count: {0}";

	// Token: 0x04000C74 RID: 3188
	public const string Webevent_event_requests_executing = "Requests executing: {0}";

	// Token: 0x04000C75 RID: 3189
	public const string Webevent_event_request_queued = "Requests queued: {0}";

	// Token: 0x04000C76 RID: 3190
	public const string Webevent_event_request_rejected = "Requests rejected: {0}";

	// Token: 0x04000C77 RID: 3191
	public const string Webevent_event_thread_id = "Thread ID: {0}";

	// Token: 0x04000C78 RID: 3192
	public const string Webevent_event_thread_account_name = "Thread account name: {0}";

	// Token: 0x04000C79 RID: 3193
	public const string Webevent_event_is_impersonating = "Is impersonating: True";

	// Token: 0x04000C7A RID: 3194
	public const string Webevent_event_is_not_impersonating = "Is impersonating: False";

	// Token: 0x04000C7B RID: 3195
	public const string Webevent_event_stack_trace = "Stack trace: {0}";

	// Token: 0x04000C7C RID: 3196
	public const string Webevent_event_user_host_address = "User host address: {0}";

	// Token: 0x04000C7D RID: 3197
	public const string Webevent_event_name_to_authenticate = "Name to authenticate: {0}";

	// Token: 0x04000C7E RID: 3198
	public const string Webevent_event_custom_event_details = "Custom event details:";

	// Token: 0x04000C7F RID: 3199
	public const string Webevent_event_ViewStateException_information = "ViewStateException information:";

	// Token: 0x04000C80 RID: 3200
	public const string Etw_Batch_Compilation = "Batch compilation: {0} files.";

	// Token: 0x04000C81 RID: 3201
	public const string Etw_Success = "success";

	// Token: 0x04000C82 RID: 3202
	public const string Etw_Failure = "failure";

	// Token: 0x04000C83 RID: 3203
	public const string Config_collection_add_element_without_key = "The element cannot be added to the collection because it has an empty key.";

	// Token: 0x04000C84 RID: 3204
	public const string Failed_Pipeline_Subscription = "Event subscription failed for {0}";

	// Token: 0x04000C85 RID: 3205
	public const string Cant_Init_Native_Config = "Unable to initialize the native configuration support external to the web worker process (HRESULT=0x{0}).\r\nnativerd.dll must be in %windir%\\system32\\inetsrv.";

	// Token: 0x04000C86 RID: 3206
	public const string Cant_Enumerate_NativeDirs = "Unable to enumerate the application directories (HRESULT=0x{0}).";

	// Token: 0x04000C87 RID: 3207
	public const string Cant_Read_Native_Modules = "An error occurred reading the integrated module list from system.webServer/modules. The error is 0x{0}.";

	// Token: 0x04000C88 RID: 3208
	public const string Cant_Create_Process_Host = "An error occurred while initializing the default application domain.";

	// Token: 0x04000C89 RID: 3209
	public const string Invalid_AppDomain_Prot_Type = "An occurred while trying to read and instantiate the configured AppDomainHandlerType.";

	// Token: 0x04000C8A RID: 3210
	public const string Invalid_Process_Prot_Type = "An error occurred while trying to read and instantiate the configured ProcessHandlerType.";

	// Token: 0x04000C8B RID: 3211
	public const string Invalid_Application_Preload_Provider_Type = "Preload provider '{0}' does not implement IProcessHostPreloadClient interface.";

	// Token: 0x04000C8C RID: 3212
	public const string Invalid_Enabled_Preload_Parameter = "Application preload cannot be enabled when ApplicationPreloadUtil is not set.";

	// Token: 0x04000C8D RID: 3213
	public const string Failure_ApplicationPreloadUtil_Already_Set = "ApplicationPreloadUtil has already been set.";

	// Token: 0x04000C8E RID: 3214
	public const string Failure_Create_Application_Preload_Provider_Type = "An error occurred while trying to create preload provider '{0}'.";

	// Token: 0x04000C8F RID: 3215
	public const string Failure_Preload_Application_Initialization = "An initialization error occurred while trying to preload an application.";

	// Token: 0x04000C90 RID: 3216
	public const string Failure_Calling_Preload_Provider = "An error occurred while executing Preload method.";

	// Token: 0x04000C91 RID: 3217
	public const string Failure_Stop_Listener_Channel = "An error occurred while trying to stop the process protocol listener channel.";

	// Token: 0x04000C92 RID: 3218
	public const string Failure_Stop_Process_Prot = "An error occurred while trying to stop the process protocol handler.";

	// Token: 0x04000C93 RID: 3219
	public const string Failure_Start_AppDomain_Listener = "An error occurred while trying to start an app domain protocol listener channel.";

	// Token: 0x04000C94 RID: 3220
	public const string Failure_Stop_AppDomain_Listener = "An error occurred while trying to stop an app domain protocol listener channel.";

	// Token: 0x04000C95 RID: 3221
	public const string Failure_Stop_AppDomain_Protocol = "An error occurred while trying to stop an app domain protocol handler.";

	// Token: 0x04000C96 RID: 3222
	public const string Failure_Start_Integrated_App = "An error occurred while trying to start an integrated application instance.";

	// Token: 0x04000C97 RID: 3223
	public const string Failure_Stop_Integrated_App = "An error occurred while trying to stop an integrated application instance.";

	// Token: 0x04000C98 RID: 3224
	public const string Failure_Shutdown_ProcessHost = "An error occurred while trying to shutdown the process host.";

	// Token: 0x04000C99 RID: 3225
	public const string Failure_AppDomain_Enum = "An error occurred while enumerating application domains.";

	// Token: 0x04000C9A RID: 3226
	public const string Failure_PMH_Ping = "An error occurred during a process host ping.";

	// Token: 0x04000C9B RID: 3227
	public const string Failure_PMH_Idle = "An error occurred during a process host idle check.";

	// Token: 0x04000C9C RID: 3228
	public const string Failure_Create_Listener_Shim = "An error occurred while creating a dispatch shim in the target app domain.";

	// Token: 0x04000C9D RID: 3229
	public const string Event_Binding_Disallowed = "Event handlers can only be bound to HttpApplication events during IHttpModule initialization.";

	// Token: 0x04000C9E RID: 3230
	public const string Requires_Iis_Integrated_Mode = "This operation requires IIS integrated pipeline mode.";

	// Token: 0x04000C9F RID: 3231
	public const string Method_Not_Supported_By_Iis_Integrated_Mode = "The {0} method is not supported by IIS integrated pipeline mode.";

	// Token: 0x04000CA0 RID: 3232
	public const string Requires_Iis_7 = "This operation requires IIS version 7 or higher.";

	// Token: 0x04000CA1 RID: 3233
	public const string Requires_Iis_75_Integrated = "This operation requires IIS version 7.5 or higher running in integrated pipeline mode.";

	// Token: 0x04000CA2 RID: 3234
	public const string Invalid_before_authentication = "This method can only be called after the authentication event.";

	// Token: 0x04000CA3 RID: 3235
	public const string Application_instance_cannot_be_changed = "The application instance cannot be changed.";

	// Token: 0x04000CA4 RID: 3236
	public const string Invalid_http_data_chunk = "Output caching and response filtering are only compatible with memory and file based response buffers.  A native module in the pipeline has added an HTTP_DATA_CHUNK to the response that is not of type HttpDataChunkFromMemory or HttpDataChunkFromFileHandle.";

	// Token: 0x04000CA5 RID: 3237
	public const string Substitution_blocks_cannot_be_modified = "Post cache substitution is not compatible with modules in the IIS integrated pipeline that modify the response buffers.  Either a native module in the pipeline has modified an HTTP_DATA_CHUNK structure associated with a managed post cache substitution callback, or a managed filter has modified the response.";

	// Token: 0x04000CA6 RID: 3238
	public const string TransferRequest_cannot_be_invoked_more_than_once = "TransferRequest cannot be invoked more than once.";

	// Token: 0x04000CA7 RID: 3239
	public const string Invoke_before_pipeline_event = "'{0}' can only be invoked before '{1}' event is raised.";

	// Token: 0x04000CA8 RID: 3240
	public const string Invalid_queue_limit = "The value must be greater than zero.  A value of zero would disable the feature, but this can only be done via configuration.";

	// Token: 0x04000CA9 RID: 3241
	public const string Queue_limit_is_zero = "The value of '{0}' is currently zero, which means the feature is disabled.  To enable the feature, set the value to a positive integer in configuration.";

	// Token: 0x04000CAA RID: 3242
	public const string HttpMethodConstraint_ParameterValueMustBeString = "The constraint for route parameter '{0}' on the route with URL '{1}' must have a string value in order to use an HttpMethodConstraint.";

	// Token: 0x04000CAB RID: 3243
	public const string Route_CannotHaveCatchAllInMultiSegment = "A path segment that contains more than one section, such as a literal section or a parameter, cannot contain a catch-all parameter.";

	// Token: 0x04000CAC RID: 3244
	public const string Route_CannotHaveConsecutiveParameters = "A path segment cannot contain two consecutive parameters. They must be separated by a '/' or by a literal string.";

	// Token: 0x04000CAD RID: 3245
	public const string Route_CannotHaveConsecutiveSeparators = "The route URL separator character '/' cannot appear consecutively. It must be separated by either a parameter or a literal value.";

	// Token: 0x04000CAE RID: 3246
	public const string Route_CatchAllMustBeLast = "A catch-all parameter can only appear as the last segment of the route URL.";

	// Token: 0x04000CAF RID: 3247
	public const string Route_InvalidParameterName = "The route parameter name '{0}' is invalid. Route parameter names must be non-empty and cannot contain these characters: \"{{\", \"}}\", \"/\", \"?\"";

	// Token: 0x04000CB0 RID: 3248
	public const string Route_InvalidRouteUrl = "The route URL cannot start with a '/' or '~' character and it cannot contain a '?' character.";

	// Token: 0x04000CB1 RID: 3249
	public const string Route_MismatchedParameter = "There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character.";

	// Token: 0x04000CB2 RID: 3250
	public const string Route_RepeatedParameter = "The route parameter name '{0}' appears more than one time in the URL.";

	// Token: 0x04000CB3 RID: 3251
	public const string Route_ValidationMustBeStringOrCustomConstraint = "The constraint entry '{0}' on the route with URL '{1}' must have a string value or be of a type which implements IRouteConstraint.";

	// Token: 0x04000CB4 RID: 3252
	public const string RouteCollection_DuplicateEntry = "The route provided already exists in the route collection. The collection may not contain duplicate routes.";

	// Token: 0x04000CB5 RID: 3253
	public const string RouteCollection_DuplicateName = "A route named '{0}' is already in the route collection. Route names must be unique.";

	// Token: 0x04000CB6 RID: 3254
	public const string RouteCollection_NameNotFound = "A route named '{0}' could not be found in the route collection.";

	// Token: 0x04000CB7 RID: 3255
	public const string RouteCollection_RequiresContext = "HttpContext.Current must be non-null when a RequestContext is not provided.";

	// Token: 0x04000CB8 RID: 3256
	public const string RouteData_RequiredValue = "The RouteData must contain an item named '{0}' with a non-empty string value.";

	// Token: 0x04000CB9 RID: 3257
	public const string RouteTable_ContextMissingRequest = "The context does not contain any request data.";

	// Token: 0x04000CBA RID: 3258
	public const string UrlRoutingHandler_NoRouteMatches = "The incoming request does not match any route.";

	// Token: 0x04000CBB RID: 3259
	public const string UrlRoutingModule_NoHttpHandler = "The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method.";

	// Token: 0x04000CBC RID: 3260
	public const string UrlRoutingModule_NoRouteHandler = "A RouteHandler must be specified for the selected route.";

	// Token: 0x04000CBD RID: 3261
	public const string RouteUrlExpression_InvalidExpression = "Invalid expression, RouteUrlExpressionBuilder expects a string with format: RouteName=route,Key1=Value1,Key2=Value2.";

	// Token: 0x04000CBE RID: 3262
	public const string PageRouteHandler_InvalidVirtualPath = "VirtualPath must be a non-empty string starting with ~/.";

	// Token: 0x04000CBF RID: 3263
	public const string RouteParameter_RouteKey = "The key to use from the route's values.";

	// Token: 0x04000CC0 RID: 3264
	public const string Control_NotADescendentOfNamingContainer = "This control is not a descendent of the NamingContainer of '{0}'.";

	// Token: 0x04000CC1 RID: 3265
	public const string DynamicModuleRegistry_ModulesAlreadyInitialized = "Cannot register a module after the application has been initialized.";

	// Token: 0x04000CC2 RID: 3266
	public const string DynamicModuleRegistry_TypeIsNotIHttpModule = "The type '{0}' is not an IHttpModule.";

	// Token: 0x04000CC3 RID: 3267
	public const string StateApplication_FullTrustOnly = "This type can only be used in a fully trusted application.";

	// Token: 0x04000CC4 RID: 3268
	public const string HttpTaskAsyncHandler_CannotExecuteSynchronously = "The handler '{0}' cannot be executed synchronously.";

	// Token: 0x04000CC5 RID: 3269
	public const string SynchronizationContextUtil_AspCompatModeNotCompatible = "<%@ Page AspCompat=\"true\" %> and <httpRuntime apartmentThreading=\"true\" /> are unsupported in the current application configuration.";

	// Token: 0x04000CC6 RID: 3270
	public const string SynchronizationContextUtil_PageAsyncVoidMethodsNotCompatible = "\"async void\" Page events are unsupported in the current application configuration.";

	// Token: 0x04000CC7 RID: 3271
	public const string SynchronizationContextUtil_TaskReturningPageAsyncMethodsNotCompatible = "Task-returning Page methods are unsupported in the current application configuration.";

	// Token: 0x04000CC8 RID: 3272
	public const string SynchronizationContextUtil_PageAsyncTaskTimeoutHandlerParallelNotCompatible = "Providing a non-null 'timeoutHandler' or a true 'executeInParallel' parameter value to the PageAsyncTask constructor is unsupported in the current application configuration.";

	// Token: 0x04000CC9 RID: 3273
	public const string SynchronizationContextUtil_WebSocketsNotCompatible = "WebSockets is unsupported in the current application configuration.";

	// Token: 0x04000CCA RID: 3274
	public const string SynchronizationContextUtil_UpgradeToTargetFramework45Instructions = "To enable this, set the following configuration switch in Web.config:\r\n<system.web>\r\n  <httpRuntime targetFramework=\"4.5\" />\r\n</system.web>";

	// Token: 0x04000CCB RID: 3275
	public const string SynchronizationContextUtil_AddDowngradeAppSettingsSwitch = "To work around this, add the following configuration switch in Web.config:\r\n<appSettings>\r\n  <add key=\"aspnet:UseTaskFriendlySynchronizationContext\" value=\"false\" />\r\n</appSettings>";

	// Token: 0x04000CCC RID: 3276
	public const string SynchronizationContextUtil_RemoveAppSettingsSwitch = "To work around this, remove the following configuration switch in Web.config:\r\n<appSettings>\r\n  <add key=\"aspnet:UseTaskFriendlySynchronizationContext\" />\r\n</appSettings>";

	// Token: 0x04000CCD RID: 3277
	public const string SynchronizationContextUtil_ForMoreInformation = "For more information, see http://go.microsoft.com/fwlink/?LinkId=252465.";

	// Token: 0x04000CCE RID: 3278
	public const string PageAsyncManager_CannotEnqueue = "An asynchronous task cannot be queued at this time.";

	// Token: 0x04000CCF RID: 3279
	public const string TaskAsyncHelper_ParameterInvalid = "The provided IAsyncResult is invalid.";

	// Token: 0x04000CD0 RID: 3280
	public const string WebSockets_WebSocketModuleNotEnabled = "The IIS WebSocket module is not enabled. For more information on enabling this module, please see http://go.microsoft.com/fwlink/?LinkId=231398.";

	// Token: 0x04000CD1 RID: 3281
	public const string WebSockets_NotAWebSocketRequest = "The incoming request is not a WebSocket request.";

	// Token: 0x04000CD2 RID: 3282
	public const string WebSockets_OriginCheckFailed = "This resource can only be accessed when requested by a page originating from the same authority.";

	// Token: 0x04000CD3 RID: 3283
	public const string WebSockets_SubProtocolCannotBeNegotiated = "The sub-protocol '{0}' cannot be negotiated for this request. See the WebSocketRequestedProtocols property for the list of sub-protocols that the client has indicated it can understand.";

	// Token: 0x04000CD4 RID: 3284
	public const string WebSockets_AcceptWebSocketRequestCanOnlyBeCalledOnce = "This method can only be called once per request.";

	// Token: 0x04000CD5 RID: 3285
	public const string WebSockets_CannotBeCalledDuringBeginRequest = "This method cannot be called during or before BeginRequest.";

	// Token: 0x04000CD6 RID: 3286
	public const string WebSockets_CannotBeCalledAfterHandlerExecute = "This method cannot be called after HttpContext.CurrentNotification has passed the ExecuteRequestHandler step.";

	// Token: 0x04000CD7 RID: 3287
	public const string WebSockets_CannotBeCalledDuringChildExecute = "This method cannot be called during a child request or from within the target of a TransferRequest.";

	// Token: 0x04000CD8 RID: 3288
	public const string WebSockets_UnknownErrorWhileAccepting = "Cannot accept the WebSocket request. An unknown error occurred.";

	// Token: 0x04000CD9 RID: 3289
	public const string WebSockets_MethodNotAvailableDuringWebSocketProcessing = "This method cannot be called once the request has fully transitioned to a WebSocket request.";

	// Token: 0x04000CDA RID: 3290
	public const string AspNetWebSocket_SendInProgress = "A send operation is already in progress.";

	// Token: 0x04000CDB RID: 3291
	public const string AspNetWebSocket_SendMessageTypeInvalid = "The outgoing message type must be WebSocketMessageType.Text or WebSocketMessageType.Binary.";

	// Token: 0x04000CDC RID: 3292
	public const string AspNetWebSocket_CloseAlreadySent = "A close frame has already been sent to the remote endpoint.";

	// Token: 0x04000CDD RID: 3293
	public const string AspNetWebSocket_ReceiveInProgress = "A receive operation is already in progress.";

	// Token: 0x04000CDE RID: 3294
	public const string AspNetWebSocket_CloseAlreadyReceived = "A close frame has already been received from the remote endpoint.";

	// Token: 0x04000CDF RID: 3295
	public const string AspNetWebSocket_CloseStatusEmptyButCloseDescriptionNonNull = "If a close status of WebSocketCloseStatus.Empty is specified, the provided status description must be null or empty.";

	// Token: 0x04000CE0 RID: 3296
	public const string AspNetWebSocket_CloseDescriptionTooLong = "The close status description is too long. Its UTF8-encoded representation must be {0} bytes or fewer.";

	// Token: 0x04000CE1 RID: 3297
	public const string AspNetWebSocket_DisposeNotSupported = "WebSocket instances cannot be disposed because applications do not control the lifetime of these objects.";

	// Token: 0x04000CE2 RID: 3298
	public const string Common_NullOrEmpty = "Value cannot be null or empty.";

	// Token: 0x04000CE3 RID: 3299
	public const string Common_PropertyCannotBeNullOrEmpty = "The property '{0}' cannot be null or empty.";

	// Token: 0x04000CE4 RID: 3300
	public const string ValueProviderResult_ConversionThrew = "The parameter conversion from type '{0}' to type '{1}' failed. See the inner exception for more information.";

	// Token: 0x04000CE5 RID: 3301
	public const string ValueProviderResult_NoConverterExists = "The parameter conversion from type '{0}' to type '{1}' failed because no type converter can convert between these types.";

	// Token: 0x04000CE6 RID: 3302
	public const string Common_PropertyNotFound = "The property {0}.{1} could not be found.";

	// Token: 0x04000CE7 RID: 3303
	public const string DataAnnotationsModelMetadataProvider_UnknownProperty = "{0} has a DisplayColumn attribute for {1}, but property {1} does not exist.";

	// Token: 0x04000CE8 RID: 3304
	public const string DataAnnotationsModelMetadataProvider_UnreadableProperty = "{0} has a DisplayColumn attribute for {1}, but property {1} does not have a public getter.";

	// Token: 0x04000CE9 RID: 3305
	public const string Common_TypeMustDriveFromType = "The type {0} must derive from {1}";

	// Token: 0x04000CEA RID: 3306
	public const string DataAnnotationsModelValidatorProvider_ConstructorRequirements = "The type {0} must have a public constructor that accepts three parameters of types {1}, {2}, and {3}";

	// Token: 0x04000CEB RID: 3307
	public const string ClientDataTypeModelValidatorProvider_FieldMustBeNumeric = "The field {0} must be a number.";

	// Token: 0x04000CEC RID: 3308
	public const string DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements = "The type {0} must have a public constructor that accepts two parameters of types {1} and {2}";

	// Token: 0x04000CED RID: 3309
	public const string ValidatableObjectAdapter_IncompatibleType = "The model object inside the metadata claimed to be compatible with {0}, but was actually {1}.";

	// Token: 0x04000CEE RID: 3310
	public const string BindingBehavior_ValueNotFound = "A value for '{0}' is required but is not present in the request.";

	// Token: 0x04000CEF RID: 3311
	public const string Common_TypeMustImplementInterface = "The type '{0}' does not implement the interface '{1}'.";

	// Token: 0x04000CF0 RID: 3312
	public const string GenericModelBinderProvider_ParameterMustSpecifyOpenGenericType = "The type '{0}' is not an open generic type.";

	// Token: 0x04000CF1 RID: 3313
	public const string GenericModelBinderProvider_TypeArgumentCountMismatch = "The open model type '{0}' has {1} generic type argument(s), but the open binder type '{2}' has {3} generic type argument(s). The binder type must not be an open generic type or must have the same number of generic arguments as the open model type.";

	// Token: 0x04000CF2 RID: 3314
	public const string ModelBinderConfig_ValueInvalid = "The value '{0}' is not valid for {1}.";

	// Token: 0x04000CF3 RID: 3315
	public const string ModelBinderConfig_ValueRequired = "A value is required.";

	// Token: 0x04000CF4 RID: 3316
	public const string ModelBinderProviderCollection_BinderForTypeNotFound = "A binder for type {0} could not be located.";

	// Token: 0x04000CF5 RID: 3317
	public const string ModelBinderProviderCollection_InvalidBinderType = "The type '{0}' does not subclass {1} or implement the interface {2}.";

	// Token: 0x04000CF6 RID: 3318
	public const string ModelBinderUtil_ModelCannotBeNull = "The binding context has a null Model, but this binder requires a non-null model of type '{0}'.";

	// Token: 0x04000CF7 RID: 3319
	public const string ModelBinderUtil_ModelInstanceIsWrong = "The binding context has a Model of type '{0}', but this binder can only operate on models of type '{1}'.";

	// Token: 0x04000CF8 RID: 3320
	public const string ModelBinderUtil_ModelMetadataCannotBeNull = "The binding context cannot have a null ModelMetadata.";

	// Token: 0x04000CF9 RID: 3321
	public const string ModelBinderUtil_ModelTypeIsWrong = "The binding context has a ModelType of '{0}', but this binder can only operate on models of type '{1}'.";

	// Token: 0x04000CFA RID: 3322
	public const string ModelBindingContext_ModelMetadataMustBeSet = "The ModelMetadata property must be set before accessing this property.";

	// Token: 0x04000CFB RID: 3323
	public const string AppVerifier_Title = "ASP.NET Runtime Verification Assertion Failure";

	// Token: 0x04000CFC RID: 3324
	public const string AppVerifier_Subtitle = "ASP.NET detected an error while invoking an asynchronous method. Details of the error are provided below to help diagnose the problem.";

	// Token: 0x04000CFD RID: 3325
	public const string AppVerifier_BasicInfo_URL = "Current URL: {0}";

	// Token: 0x04000CFE RID: 3326
	public const string AppVerifier_BasicInfo_ErrorCode = "Error code: {0}";

	// Token: 0x04000CFF RID: 3327
	public const string AppVerifier_BasicInfo_Description = "Description: {0}";

	// Token: 0x04000D00 RID: 3328
	public const string AppVerifier_BasicInfo_ThreadInfo = "The assertion was triggered on thread {0} at {1} with the following stack trace:";

	// Token: 0x04000D01 RID: 3329
	public const string AppVerifier_BeginMethodInfo_EntryMethod = "Entry point which triggered failure: {0}";

	// Token: 0x04000D02 RID: 3330
	public const string AppVerifier_BeginMethodInfo_RequestNotification_Integrated = "Request notification at time of entry: {0} [IsPostNotification = {1}]";

	// Token: 0x04000D03 RID: 3331
	public const string AppVerifier_BeginMethodInfo_RequestNotification_NotIntegrated = "Request notification at time of entry: n/a (not running in integrated mode)";

	// Token: 0x04000D04 RID: 3332
	public const string AppVerifier_BeginMethodInfo_CurrentHandler = "Request handler at time of entry: {0}";

	// Token: 0x04000D05 RID: 3333
	public const string AppVerifier_BeginMethodInfo_ThreadInfo = "The entry point was invoked on thread {0} at {1} with the following stack trace:";

	// Token: 0x04000D06 RID: 3334
	public const string AppVerifier_AsyncCallbackInfo_InvocationCount = "AsyncCallback was invoked a total of {0} time(s).";

	// Token: 0x04000D07 RID: 3335
	public const string AppVerifier_AsyncCallbackInfo_FirstInvocation_ThreadInfo = "It was first invoked on thread {0} at {1} with the following stack trace:";

	// Token: 0x04000D08 RID: 3336
	public const string AppVerifier_Errors_HttpApplicationInstanceWasNull = "The provided HttpApplication instance was null. The HttpApplication instance must be non-null.";

	// Token: 0x04000D09 RID: 3337
	public const string AppVerifier_Errors_BeginHandlerDelegateWasNull = "The provided entry point (BeginHandler) was null. The entry point must be non-null.";

	// Token: 0x04000D0A RID: 3338
	public const string AppVerifier_Errors_AsyncCallbackInvokedMultipleTimes = "AsyncCallback has already been invoked. The callback must never be invoked multiple times.";

	// Token: 0x04000D0B RID: 3339
	public const string AppVerifier_Errors_AsyncCallbackInvokedWithNullParameter = "AsyncCallback was invoked with a null IAsyncResult argument. The callback must be given a non-null argument.";

	// Token: 0x04000D0C RID: 3340
	public const string AppVerifier_Errors_AsyncCallbackGivenAsyncResultWhichWasNotCompleted = "AsyncCallback was invoked with an IAsyncResult which was marked 'IsCompleted = false'. The callback must not be invoked until the asynchronous operation has completed.";

	// Token: 0x04000D0D RID: 3341
	public const string AppVerifier_Errors_AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously = "AsyncCallback was invoked asynchronously, but the provided IAsyncResult argument was marked 'CompletedSynchronously = true'. The argument's CompletedSynchronously property must match the manner in which the asynchronous operation completed.";

	// Token: 0x04000D0E RID: 3342
	public const string AppVerifier_Errors_AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously = "AsyncCallback was invoked synchronously, but the provided IAsyncResult argument was marked 'CompletedSynchronously = false'. The argument's CompletedSynchronously property must match the manner in which the asynchronous operation completed.";

	// Token: 0x04000D0F RID: 3343
	public const string AppVerifier_Errors_AsyncCallbackInvokedWithUnexpectedAsyncResultInstance = "The entry point returned an IAsyncResult instance other than the IAsyncResult argument provided to AsyncCallback. The entry point's return value must match the argument provided to AsyncCallback.";

	// Token: 0x04000D10 RID: 3344
	public const string AppVerifier_Errors_AsyncCallbackInvokedEvenThoughBeginHandlerThrew = "AsyncCallback was invoked even though the entry point threw an exception. AsyncCallback should not be invoked if the entry point throws an exception.";

	// Token: 0x04000D11 RID: 3345
	public const string AppVerifier_Errors_AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState = "The IAsyncResult argument passed to AsyncCallback had an invalid AsyncState property. The IAsyncResult's AsyncState property must match the state object parameter provided to the entry point.";

	// Token: 0x04000D12 RID: 3346
	public const string AppVerifier_Errors_AsyncCallbackCalledAfterHttpApplicationReassigned = "The underlying HTTP request had already completed by the time AsyncCallback was invoked asynchronously.";

	// Token: 0x04000D13 RID: 3347
	public const string AppVerifier_Errors_BeginHandlerReturnedNull = "The entry point returned a null value. The return value must be a non-null IAsyncResult instance.";

	// Token: 0x04000D14 RID: 3348
	public const string AppVerifier_Errors_BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted = "The entry point returned an IAsyncResult instance that was marked 'CompletedSynchronously = true' and 'IsCompleted = false'. If the operation is completed, it must be marked 'IsCompleted = true'.";

	// Token: 0x04000D15 RID: 3349
	public const string AppVerifier_Errors_BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled = "The entry point returned an IAsyncResult instance that was marked 'CompletedSynchronously = true', but AsyncCallback was never invoked synchronously. If an operation completes synchronously and AsyncCallback is non-null, the callback must be invoked synchronously before the entry point returns to its caller.";

	// Token: 0x04000D16 RID: 3350
	public const string AppVerifier_Errors_BeginHandlerReturnedUnexpectedAsyncResultAsyncState = "The entry point returned an IAsyncResult instance with an invalid AsyncState property. The IAsyncResult's AsyncState property must match the state object parameter provided to the entry point.";

	// Token: 0x04000D17 RID: 3351
	public const string AppVerifier_Errors_SyncContextSendOrPostCalledAfterRequestCompleted = "A thread attempted to call SynchronizationContext.Send or SynchronizationContext.Post after the request associated with the SynchronizationContext had already completed.";
}
