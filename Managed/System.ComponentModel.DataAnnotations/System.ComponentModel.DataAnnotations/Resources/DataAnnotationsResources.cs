using System;

namespace System.ComponentModel.DataAnnotations.Resources
{
	// Token: 0x02000046 RID: 70
	internal class DataAnnotationsResources
	{
		// Token: 0x040000D7 RID: 215
		public const string AssociatedMetadataTypeTypeDescriptor_MetadataTypeContainsUnknownProperties = "The associated metadata type for type '{0}' contains the following unknown properties or fields: {1}. Please make sure that the names of these members match the names of the properties on the main type.";

		// Token: 0x040000D8 RID: 216
		public const string AttributeStore_Type_Must_Be_Public = "The type '{0}' must be public.";

		// Token: 0x040000D9 RID: 217
		public const string AttributeStore_Unknown_Method = "The type '{0}' does not contain a public method named '{1}'.";

		// Token: 0x040000DA RID: 218
		public const string AttributeStore_Unknown_Property = "The type '{0}' does not contain a public property named '{1}'.";

		// Token: 0x040000DB RID: 219
		public const string CustomValidationAttribute_Method_Must_Return_ValidationResult = "The CustomValidationAttribute method '{0}' in type '{1}' must return System.ComponentModel.DataAnnotations.ValidationResult.  Use System.ComponentModel.DataAnnotations.ValidationResult.Success to represent success.";

		// Token: 0x040000DC RID: 220
		public const string CustomValidationAttribute_Method_Not_Found = "The CustomValidationAttribute method '{0}' does not exist in type '{1}' or is not public and static.";

		// Token: 0x040000DD RID: 221
		public const string CustomValidationAttribute_Method_Required = "The CustomValidationAttribute.Method was not specified.";

		// Token: 0x040000DE RID: 222
		public const string CustomValidationAttribute_Method_Signature = "The CustomValidationAttribute method '{0}' in type '{1}' must match the expected signature: public static ValidationResult {0}(object value, ValidationContext context).  The value can be strongly typed.  The ValidationContext parameter is optional.";

		// Token: 0x040000DF RID: 223
		public const string CustomValidationAttribute_Type_Must_Be_Public = "The custom validation type '{0}' must be public.";

		// Token: 0x040000E0 RID: 224
		public const string CustomValidationAttribute_ValidationError = "{0} is not valid.";

		// Token: 0x040000E1 RID: 225
		public const string CustomValidationAttribute_ValidatorType_Required = "The CustomValidationAttribute.ValidatorType was not specified.";

		// Token: 0x040000E2 RID: 226
		public const string DataTypeAttribute_EmptyDataTypeString = "The custom DataType string cannot be null or empty.";

		// Token: 0x040000E3 RID: 227
		public const string LocalizableString_LocalizationFailed = "Cannot retrieve property '{0}' because localization failed.  Type '{1}' is not public or does not contain a public static string property with the name '{2}'.";

		// Token: 0x040000E4 RID: 228
		public const string Validator_Property_Value_Wrong_Type = "The value for property '{0}' must be of type '{1}'.";

		// Token: 0x040000E5 RID: 229
		public const string RangeAttribute_ArbitraryTypeNotIComparable = "The type {0} must implement {1}.";

		// Token: 0x040000E6 RID: 230
		public const string RangeAttribute_MinGreaterThanMax = "The maximum value '{0}' must be greater than or equal to the minimum value '{1}'.";

		// Token: 0x040000E7 RID: 231
		public const string RangeAttribute_Must_Set_Min_And_Max = "The minimum and maximum values must be set.";

		// Token: 0x040000E8 RID: 232
		public const string RangeAttribute_Must_Set_Operand_Type = "The OperandType must be set when strings are used for minimum and maximum values.";

		// Token: 0x040000E9 RID: 233
		public const string RangeAttribute_ValidationError = "The field {0} must be between {1} and {2}.";

		// Token: 0x040000EA RID: 234
		public const string RegexAttribute_ValidationError = "The field {0} must match the regular expression '{1}'.";

		// Token: 0x040000EB RID: 235
		public const string RegularExpressionAttribute_Empty_Pattern = "The pattern must be set to a valid regular expression.";

		// Token: 0x040000EC RID: 236
		public const string RequiredAttribute_ValidationError = "The {0} field is required.";

		// Token: 0x040000ED RID: 237
		public const string StringLengthAttribute_InvalidMaxLength = "The maximum length must be a nonnegative integer.";

		// Token: 0x040000EE RID: 238
		public const string StringLengthAttribute_ValidationError = "The field {0} must be a string with a maximum length of {1}.";

		// Token: 0x040000EF RID: 239
		public const string UIHintImplementation_ControlParameterKeyIsNotAString = "The key parameter at position {0} with value '{1}' is not a string. Every key control parameter must be a string.";

		// Token: 0x040000F0 RID: 240
		public const string UIHintImplementation_ControlParameterKeyIsNull = "The key parameter at position {0} is null. Every key control parameter must be a string.";

		// Token: 0x040000F1 RID: 241
		public const string UIHintImplementation_NeedEvenNumberOfControlParameters = "The number of control parameters must be even.";

		// Token: 0x040000F2 RID: 242
		public const string UIHintImplementation_ControlParameterKeyOccursMoreThanOnce = "The key parameter at position {0} with value '{1}' occurs more than once.";

		// Token: 0x040000F3 RID: 243
		public const string ValidationAttribute_Cannot_Set_ErrorMessage_And_Resource = "Either ErrorMessageString or ErrorMessageResourceName must be set, but not both.";

		// Token: 0x040000F4 RID: 244
		public const string ValidationAttribute_NeedBothResourceTypeAndResourceName = "Both ErrorMessageResourceType and ErrorMessageResourceName need to be set on this attribute.";

		// Token: 0x040000F5 RID: 245
		public const string ValidationAttribute_ResourcePropertyNotStringType = "The property '{0}' on resource type '{1}' is not a string type.";

		// Token: 0x040000F6 RID: 246
		public const string ValidationAttribute_ResourceTypeDoesNotHaveProperty = "The resource type '{0}' does not have an accessible static property named '{1}'.";

		// Token: 0x040000F7 RID: 247
		public const string ValidationAttribute_ValidationError = "The field {0} is invalid.";

		// Token: 0x040000F8 RID: 248
		public const string ValidationContext_Must_Be_Method = "The ValidationContext for the type '{0}', member name '{1}' must provide the MethodInfo.";

		// Token: 0x040000F9 RID: 249
		public const string EnumDataTypeAttribute_TypeNeedsToBeAnEnum = "The type '{0}' needs to represent an enumeration type.";

		// Token: 0x040000FA RID: 250
		public const string EnumDataTypeAttribute_TypeCannotBeNull = "The type provided for EnumDataTypeAttribute cannot be null.";

		// Token: 0x040000FB RID: 251
		public const string MetadataTypeAttribute_TypeCannotBeNull = "MetadataClassType cannot be null.";

		// Token: 0x040000FC RID: 252
		public const string DisplayAttribute_PropertyNotSet = "The {0} property has not been set.  Use the {1} method to get the value.";

		// Token: 0x040000FD RID: 253
		public const string ValidationContextServiceContainer_ItemAlreadyExists = "A service of type '{0}' already exists in the container.";

		// Token: 0x040000FE RID: 254
		public const string Validator_InstanceMustMatchValidationContextInstance = "The instance provided must match the ObjectInstance on the ValidationContext supplied.";

		// Token: 0x040000FF RID: 255
		public const string ValidationAttribute_IsValid_NotImplemented = "IsValid(object value) has not been implemented by this class.  The preferred entry point is GetValidationResult() and classes should override IsValid(object value, ValidationContext context).";

		// Token: 0x04000100 RID: 256
		public const string CustomValidationAttribute_Type_Conversion_Failed = "Could not convert the value of type '{0}' to '{1}' as expected by method {2}.{3}.";

		// Token: 0x04000101 RID: 257
		public const string StringLengthAttribute_ValidationErrorIncludingMinimum = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.";

		// Token: 0x04000102 RID: 258
		public const string CreditCardAttribute_Invalid = "The {0} field is not a valid credit card number.";

		// Token: 0x04000103 RID: 259
		public const string EmailAddressAttribute_Invalid = "The {0} field is not a valid e-mail address.";

		// Token: 0x04000104 RID: 260
		public const string FileExtensionsAttribute_Invalid = "The {0} field only accepts files with the following extensions: {1}";

		// Token: 0x04000105 RID: 261
		public const string UrlAttribute_Invalid = "The {0} field is not a valid fully-qualified http, https, or ftp URL.";

		// Token: 0x04000106 RID: 262
		public const string CompareAttribute_MustMatch = "'{0}' and '{1}' do not match.";

		// Token: 0x04000107 RID: 263
		public const string Common_NullOrEmpty = "Value cannot be null or empty.";

		// Token: 0x04000108 RID: 264
		public const string CompareAttribute_UnknownProperty = "Could not find a property named {0}.";

		// Token: 0x04000109 RID: 265
		public const string Common_PropertyNotFound = "The property {0}.{1} could not be found.";

		// Token: 0x0400010A RID: 266
		public const string PhoneAttribute_Invalid = "The {0} field is not a valid phone number.";

		// Token: 0x0400010B RID: 267
		public const string MaxLengthAttribute_InvalidMaxLength = "MaxLengthAttribute must have a Length value that is greater than zero. Use MaxLength() without parameters to indicate that the string or array can have the maximum allowable length.";

		// Token: 0x0400010C RID: 268
		public const string MaxLengthAttribute_ValidationError = "The field {0} must be a string or array type with a maximum length of '{1}'.";

		// Token: 0x0400010D RID: 269
		public const string MinLengthAttribute_InvalidMinLength = "MinLengthAttribute must have a Length value that is zero or greater.";

		// Token: 0x0400010E RID: 270
		public const string MinLengthAttribute_ValidationError = "The field {0} must be a string or array type with a minimum length of '{1}'.";

		// Token: 0x0400010F RID: 271
		public const string ArgumentIsNullOrWhitespace = "The argument '{0}' cannot be null, empty or contain only white space.";
	}
}
