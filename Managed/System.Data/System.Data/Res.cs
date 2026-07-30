using System;

// Token: 0x0200001E RID: 30
internal static class Res
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00005D82 File Offset: 0x00003F82
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005D85 File Offset: 0x00003F85
	internal static string GetString(string name, params object[] args)
	{
		return string.Format(name, args);
	}

	// Token: 0x040003DE RID: 990
	internal const string CodeGen_InvalidIdentifier = "Cannot generate identifier for name '{0}'";

	// Token: 0x040003DF RID: 991
	internal const string CodeGen_DuplicateTableName = "There is more than one table with the same name '{0}' (even if namespace is different)";

	// Token: 0x040003E0 RID: 992
	internal const string CodeGen_TypeCantBeNull = "Column '{0}': Type '{1}' cannot be null";

	// Token: 0x040003E1 RID: 993
	internal const string CodeGen_NoCtor0 = "Column '{0}': Type '{1}' does not have parameterless constructor";

	// Token: 0x040003E2 RID: 994
	internal const string CodeGen_NoCtor1 = "Column '{0}': Type '{1}' does not have constructor with string argument";

	// Token: 0x040003E3 RID: 995
	internal const string SQLUDT_MaxByteSizeValue = "range: 0-8000";

	// Token: 0x040003E4 RID: 996
	internal const string SqlUdt_InvalidUdtMessage = "'{0}' is an invalid user defined type, reason: {1}.";

	// Token: 0x040003E5 RID: 997
	internal const string Sql_NullCommandText = "Command parameter must have a non null and non empty command text.";

	// Token: 0x040003E6 RID: 998
	internal const string Sql_MismatchedMetaDataDirectionArrayLengths = "MetaData parameter array must have length equivalent to ParameterDirection array argument.";

	// Token: 0x040003E7 RID: 999
	public const string ADP_InvalidXMLBadVersion = "Invalid Xml; can only parse elements of version one.";

	// Token: 0x040003E8 RID: 1000
	public const string ADP_NotAPermissionElement = "Given security element is not a permission element.";

	// Token: 0x040003E9 RID: 1001
	public const string ADP_PermissionTypeMismatch = "Type mismatch.";

	// Token: 0x040003EA RID: 1002
	public const string ConfigProviderNotFound = "Unable to find the requested .Net Framework Data Provider.  It may not be installed.";

	// Token: 0x040003EB RID: 1003
	public const string ConfigProviderInvalid = "The requested .Net Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.";

	// Token: 0x040003EC RID: 1004
	public const string ConfigProviderNotInstalled = "Failed to find or load the registered .Net Framework Data Provider.";

	// Token: 0x040003ED RID: 1005
	public const string ConfigProviderMissing = "The missing .Net Framework Data Provider's assembly qualified name is required.";

	// Token: 0x040003EE RID: 1006
	public const string ConfigBaseElementsOnly = "Only elements allowed.";

	// Token: 0x040003EF RID: 1007
	public const string ConfigBaseNoChildNodes = "Child nodes not allowed.";

	// Token: 0x040003F0 RID: 1008
	public const string ConfigUnrecognizedAttributes = "Unrecognized attribute '{0}'.";

	// Token: 0x040003F1 RID: 1009
	public const string ConfigUnrecognizedElement = "Unrecognized element.";

	// Token: 0x040003F2 RID: 1010
	public const string ConfigSectionsUnique = "The '{0}' section can only appear once per config file.";

	// Token: 0x040003F3 RID: 1011
	public const string ConfigRequiredAttributeMissing = "Required attribute '{0}' not found.";

	// Token: 0x040003F4 RID: 1012
	public const string ConfigRequiredAttributeEmpty = "Required attribute '{0}' cannot be empty.";

	// Token: 0x040003F5 RID: 1013
	public const string ADP_QuotePrefixNotSet = "{0} requires open connection when the quote prefix has not been set.";
}
