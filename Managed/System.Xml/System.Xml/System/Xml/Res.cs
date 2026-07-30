using System;

namespace System.Xml
{
	// Token: 0x020002AC RID: 684
	internal static class Res
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x00002068 File Offset: 0x00000268
		public static string GetString(string name)
		{
			return name;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00090406 File Offset: 0x0008E606
		public static string GetString(string name, params object[] args)
		{
			if (args == null)
			{
				return name;
			}
			return SR.GetString(name, args);
		}

		// Token: 0x04001077 RID: 4215
		public const string Xml_UserException = "{0}";

		// Token: 0x04001078 RID: 4216
		public const string Xml_DefaultException = "An XML error has occurred.";

		// Token: 0x04001079 RID: 4217
		public const string Xml_InvalidOperation = "Operation is not valid due to the current state of the object.";

		// Token: 0x0400107A RID: 4218
		public const string Xml_ErrorFilePosition = "An error occurred at {0}, ({1}, {2}).";

		// Token: 0x0400107B RID: 4219
		public const string Xml_StackOverflow = "Stack overflow.";

		// Token: 0x0400107C RID: 4220
		public const string Xslt_NoStylesheetLoaded = "No stylesheet was loaded.";

		// Token: 0x0400107D RID: 4221
		public const string Xslt_NotCompiledStylesheet = "Type '{0}' is not a compiled stylesheet class.";

		// Token: 0x0400107E RID: 4222
		public const string Xslt_IncompatibleCompiledStylesheetVersion = "Executing a stylesheet that was compiled using a later version of the framework is not supported. Stylesheet Version: {0}. Current Framework Version: {1}.";

		// Token: 0x0400107F RID: 4223
		public const string Xml_AsyncIsRunningException = "An asynchronous operation is already in progress.";

		// Token: 0x04001080 RID: 4224
		public const string Xml_ReaderAsyncNotSetException = "Set XmlReaderSettings.Async to true if you want to use Async Methods.";

		// Token: 0x04001081 RID: 4225
		public const string Xml_UnclosedQuote = "There is an unclosed literal string.";

		// Token: 0x04001082 RID: 4226
		public const string Xml_UnexpectedEOF = "Unexpected end of file while parsing {0} has occurred.";

		// Token: 0x04001083 RID: 4227
		public const string Xml_UnexpectedEOF1 = "Unexpected end of file has occurred.";

		// Token: 0x04001084 RID: 4228
		public const string Xml_UnexpectedEOFInElementContent = "Unexpected end of file has occurred. The following elements are not closed: {0}";

		// Token: 0x04001085 RID: 4229
		public const string Xml_BadStartNameChar = "Name cannot begin with the '{0}' character, hexadecimal value {1}.";

		// Token: 0x04001086 RID: 4230
		public const string Xml_BadNameChar = "The '{0}' character, hexadecimal value {1}, cannot be included in a name.";

		// Token: 0x04001087 RID: 4231
		public const string Xml_BadDecimalEntity = "Invalid syntax for a decimal numeric entity reference.";

		// Token: 0x04001088 RID: 4232
		public const string Xml_BadHexEntity = "Invalid syntax for a hexadecimal numeric entity reference.";

		// Token: 0x04001089 RID: 4233
		public const string Xml_MissingByteOrderMark = "There is no Unicode byte order mark. Cannot switch to Unicode.";

		// Token: 0x0400108A RID: 4234
		public const string Xml_UnknownEncoding = "System does not support '{0}' encoding.";

		// Token: 0x0400108B RID: 4235
		public const string Xml_InternalError = "An internal error has occurred.";

		// Token: 0x0400108C RID: 4236
		public const string Xml_InvalidCharInThisEncoding = "Invalid character in the given encoding.";

		// Token: 0x0400108D RID: 4237
		public const string Xml_ErrorPosition = "Line {0}, position {1}.";

		// Token: 0x0400108E RID: 4238
		public const string Xml_MessageWithErrorPosition = "{0} Line {1}, position {2}.";

		// Token: 0x0400108F RID: 4239
		public const string Xml_UnexpectedTokenEx = "'{0}' is an unexpected token. The expected token is '{1}'.";

		// Token: 0x04001090 RID: 4240
		public const string Xml_UnexpectedTokens2 = "'{0}' is an unexpected token. The expected token is '{1}' or '{2}'.";

		// Token: 0x04001091 RID: 4241
		public const string Xml_ExpectingWhiteSpace = "'{0}' is an unexpected token. Expecting white space.";

		// Token: 0x04001092 RID: 4242
		public const string Xml_TagMismatch = "The '{0}' start tag on line {1} does not match the end tag of '{2}'.";

		// Token: 0x04001093 RID: 4243
		public const string Xml_TagMismatchEx = "The '{0}' start tag on line {1} position {2} does not match the end tag of '{3}'.";

		// Token: 0x04001094 RID: 4244
		public const string Xml_UnexpectedEndTag = "Unexpected end tag.";

		// Token: 0x04001095 RID: 4245
		public const string Xml_UnknownNs = "'{0}' is an undeclared prefix.";

		// Token: 0x04001096 RID: 4246
		public const string Xml_BadAttributeChar = "'{0}', hexadecimal value {1}, is an invalid attribute character.";

		// Token: 0x04001097 RID: 4247
		public const string Xml_ExpectExternalOrClose = "Expecting external ID, '[' or '>'.";

		// Token: 0x04001098 RID: 4248
		public const string Xml_MissingRoot = "Root element is missing.";

		// Token: 0x04001099 RID: 4249
		public const string Xml_MultipleRoots = "There are multiple root elements.";

		// Token: 0x0400109A RID: 4250
		public const string Xml_InvalidRootData = "Data at the root level is invalid.";

		// Token: 0x0400109B RID: 4251
		public const string Xml_XmlDeclNotFirst = "Unexpected XML declaration. The XML declaration must be the first node in the document, and no white space characters are allowed to appear before it.";

		// Token: 0x0400109C RID: 4252
		public const string Xml_InvalidXmlDecl = "Syntax for an XML declaration is invalid.";

		// Token: 0x0400109D RID: 4253
		public const string Xml_InvalidNodeType = "'{0}' is an invalid XmlNodeType.";

		// Token: 0x0400109E RID: 4254
		public const string Xml_InvalidPIName = "'{0}' is an invalid name for processing instructions.";

		// Token: 0x0400109F RID: 4255
		public const string Xml_InvalidXmlSpace = "'{0}' is an invalid xml:space value.";

		// Token: 0x040010A0 RID: 4256
		public const string Xml_InvalidVersionNumber = "Version number '{0}' is invalid.";

		// Token: 0x040010A1 RID: 4257
		public const string Xml_DupAttributeName = "'{0}' is a duplicate attribute name.";

		// Token: 0x040010A2 RID: 4258
		public const string Xml_BadDTDLocation = "Unexpected DTD declaration.";

		// Token: 0x040010A3 RID: 4259
		public const string Xml_ElementNotFound = "Element '{0}' was not found.";

		// Token: 0x040010A4 RID: 4260
		public const string Xml_ElementNotFoundNs = "Element '{0}' with namespace name '{1}' was not found.";

		// Token: 0x040010A5 RID: 4261
		public const string Xml_PartialContentNodeTypeNotSupportedEx = "XmlNodeType {0} is not supported for partial content parsing.";

		// Token: 0x040010A6 RID: 4262
		public const string Xml_MultipleDTDsProvided = "Cannot have multiple DTDs.";

		// Token: 0x040010A7 RID: 4263
		public const string Xml_CanNotBindToReservedNamespace = "Cannot bind to the reserved namespace.";

		// Token: 0x040010A8 RID: 4264
		public const string Xml_InvalidCharacter = "'{0}', hexadecimal value {1}, is an invalid character.";

		// Token: 0x040010A9 RID: 4265
		public const string Xml_InvalidBinHexValue = "'{0}' is not a valid BinHex text sequence.";

		// Token: 0x040010AA RID: 4266
		public const string Xml_InvalidBinHexValueOddCount = "'{0}' is not a valid BinHex text sequence. The sequence must contain an even number of characters.";

		// Token: 0x040010AB RID: 4267
		public const string Xml_InvalidTextDecl = "Invalid text declaration.";

		// Token: 0x040010AC RID: 4268
		public const string Xml_InvalidBase64Value = "'{0}' is not a valid Base64 text sequence.";

		// Token: 0x040010AD RID: 4269
		public const string Xml_UndeclaredEntity = "Reference to undeclared entity '{0}'.";

		// Token: 0x040010AE RID: 4270
		public const string Xml_RecursiveParEntity = "Parameter entity '{0}' references itself.";

		// Token: 0x040010AF RID: 4271
		public const string Xml_RecursiveGenEntity = "General entity '{0}' references itself.";

		// Token: 0x040010B0 RID: 4272
		public const string Xml_ExternalEntityInAttValue = "External entity '{0}' reference cannot appear in the attribute value.";

		// Token: 0x040010B1 RID: 4273
		public const string Xml_UnparsedEntityRef = "Reference to unparsed entity '{0}'.";

		// Token: 0x040010B2 RID: 4274
		public const string Xml_NotSameNametable = "Not the same name table.";

		// Token: 0x040010B3 RID: 4275
		public const string Xml_NametableMismatch = "XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.";

		// Token: 0x040010B4 RID: 4276
		public const string Xml_BadNamespaceDecl = "Invalid namespace declaration.";

		// Token: 0x040010B5 RID: 4277
		public const string Xml_ErrorParsingEntityName = "An error occurred while parsing EntityName.";

		// Token: 0x040010B6 RID: 4278
		public const string Xml_InvalidNmToken = "Invalid NmToken value '{0}'.";

		// Token: 0x040010B7 RID: 4279
		public const string Xml_EntityRefNesting = "Entity replacement text must nest properly within markup declarations.";

		// Token: 0x040010B8 RID: 4280
		public const string Xml_CannotResolveEntity = "Cannot resolve entity reference '{0}'.";

		// Token: 0x040010B9 RID: 4281
		public const string Xml_CannotResolveEntityDtdIgnored = "Cannot resolve entity reference '{0}' because the DTD has been ignored. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.";

		// Token: 0x040010BA RID: 4282
		public const string Xml_CannotResolveExternalSubset = "Cannot resolve external DTD subset - public ID = '{0}', system ID = '{1}'.";

		// Token: 0x040010BB RID: 4283
		public const string Xml_CannotResolveUrl = "Cannot resolve '{0}'.";

		// Token: 0x040010BC RID: 4284
		public const string Xml_CDATAEndInText = "']]>' is not allowed in character data.";

		// Token: 0x040010BD RID: 4285
		public const string Xml_ExternalEntityInStandAloneDocument = "Standalone document declaration must have a value of 'no' because an external entity '{0}' is referenced.";

		// Token: 0x040010BE RID: 4286
		public const string Xml_DtdAfterRootElement = "DTD must be defined before the document root element.";

		// Token: 0x040010BF RID: 4287
		public const string Xml_ReadOnlyProperty = "The '{0}' property is read only and cannot be set.";

		// Token: 0x040010C0 RID: 4288
		public const string Xml_DtdIsProhibited = "DTD is prohibited in this XML document.";

		// Token: 0x040010C1 RID: 4289
		public const string Xml_DtdIsProhibitedEx = "For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.";

		// Token: 0x040010C2 RID: 4290
		public const string Xml_ReadSubtreeNotOnElement = "ReadSubtree() can be called only if the reader is on an element node.";

		// Token: 0x040010C3 RID: 4291
		public const string Xml_DtdNotAllowedInFragment = "DTD is not allowed in XML fragments.";

		// Token: 0x040010C4 RID: 4292
		public const string Xml_CannotStartDocumentOnFragment = "WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment.";

		// Token: 0x040010C5 RID: 4293
		public const string Xml_ErrorOpeningExternalDtd = "An error has occurred while opening external DTD '{0}': {1}";

		// Token: 0x040010C6 RID: 4294
		public const string Xml_ErrorOpeningExternalEntity = "An error has occurred while opening external entity '{0}': {1}";

		// Token: 0x040010C7 RID: 4295
		public const string Xml_ReadBinaryContentNotSupported = "{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.";

		// Token: 0x040010C8 RID: 4296
		public const string Xml_ReadValueChunkNotSupported = "ReadValueChunk method is not supported on this XmlReader. Use CanReadValueChunk property to find out if an XmlReader implements it.";

		// Token: 0x040010C9 RID: 4297
		public const string Xml_InvalidReadContentAs = "The {0} method is not supported on node type {1}. If you want to read typed content of an element, use the ReadElementContentAs method.";

		// Token: 0x040010CA RID: 4298
		public const string Xml_InvalidReadElementContentAs = "The {0} method is not supported on node type {1}.";

		// Token: 0x040010CB RID: 4299
		public const string Xml_MixedReadElementContentAs = "ReadElementContentAs() methods cannot be called on an element that has child elements.";

		// Token: 0x040010CC RID: 4300
		public const string Xml_MixingReadValueChunkWithBinary = "ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex.";

		// Token: 0x040010CD RID: 4301
		public const string Xml_MixingBinaryContentMethods = "ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex.";

		// Token: 0x040010CE RID: 4302
		public const string Xml_MixingV1StreamingWithV2Binary = "ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadChars, ReadBase64, and ReadBinHex.";

		// Token: 0x040010CF RID: 4303
		public const string Xml_InvalidReadValueChunk = "The ReadValueAsChunk method is not supported on node type {0}.";

		// Token: 0x040010D0 RID: 4304
		public const string Xml_ReadContentAsFormatException = "Content cannot be converted to the type {0}.";

		// Token: 0x040010D1 RID: 4305
		public const string Xml_DoubleBaseUri = "BaseUri must be specified either as an argument of XmlReader.Create or on the XmlParserContext. If it is specified on both, it must be the same base URI.";

		// Token: 0x040010D2 RID: 4306
		public const string Xml_NotEnoughSpaceForSurrogatePair = "The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.";

		// Token: 0x040010D3 RID: 4307
		public const string Xml_EmptyUrl = "The URL cannot be empty.";

		// Token: 0x040010D4 RID: 4308
		public const string Xml_UnexpectedNodeInSimpleContent = "Unexpected node type {0}. {1} method can only be called on elements with simple or empty content.";

		// Token: 0x040010D5 RID: 4309
		public const string Xml_InvalidWhitespaceCharacter = "The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.";

		// Token: 0x040010D6 RID: 4310
		public const string Xml_IncompatibleConformanceLevel = "Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.";

		// Token: 0x040010D7 RID: 4311
		public const string Xml_LimitExceeded = "The input document has exceeded a limit set by {0}.";

		// Token: 0x040010D8 RID: 4312
		public const string Xml_ClosedOrErrorReader = "The XmlReader is closed or in error state.";

		// Token: 0x040010D9 RID: 4313
		public const string Xml_CharEntityOverflow = "Invalid value of a character entity reference.";

		// Token: 0x040010DA RID: 4314
		public const string Xml_BadNameCharWithPos = "The '{0}' character, hexadecimal value {1}, at position {2} within the name, cannot be included in a name.";

		// Token: 0x040010DB RID: 4315
		public const string Xml_XmlnsBelongsToReservedNs = "The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'.";

		// Token: 0x040010DC RID: 4316
		public const string Xml_UndeclaredParEntity = "Reference to undeclared parameter entity '{0}'.";

		// Token: 0x040010DD RID: 4317
		public const string Xml_InvalidXmlDocument = "Invalid XML document. {0}";

		// Token: 0x040010DE RID: 4318
		public const string Xml_NoDTDPresent = "No DTD found.";

		// Token: 0x040010DF RID: 4319
		public const string Xml_MultipleValidaitonTypes = "Unsupported combination of validation types.";

		// Token: 0x040010E0 RID: 4320
		public const string Xml_NoValidation = "No validation occurred.";

		// Token: 0x040010E1 RID: 4321
		public const string Xml_WhitespaceHandling = "Expected WhitespaceHandling.None, or WhitespaceHandling.All, or WhitespaceHandling.Significant.";

		// Token: 0x040010E2 RID: 4322
		public const string Xml_InvalidResetStateCall = "Cannot call ResetState when parsing an XML fragment.";

		// Token: 0x040010E3 RID: 4323
		public const string Xml_EntityHandling = "Expected EntityHandling.ExpandEntities or EntityHandling.ExpandCharEntities.";

		// Token: 0x040010E4 RID: 4324
		public const string Xml_AttlistDuplEnumValue = "'{0}' is a duplicate enumeration value.";

		// Token: 0x040010E5 RID: 4325
		public const string Xml_AttlistDuplNotationValue = "'{0}' is a duplicate notation value.";

		// Token: 0x040010E6 RID: 4326
		public const string Xml_EncodingSwitchAfterResetState = "'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.";

		// Token: 0x040010E7 RID: 4327
		public const string Xml_UnexpectedNodeType = "Unexpected XmlNodeType: '{0}'.";

		// Token: 0x040010E8 RID: 4328
		public const string Xml_InvalidConditionalSection = "A conditional section is not allowed in an internal subset.";

		// Token: 0x040010E9 RID: 4329
		public const string Xml_UnexpectedCDataEnd = "']]>' is not expected.";

		// Token: 0x040010EA RID: 4330
		public const string Xml_UnclosedConditionalSection = "There is an unclosed conditional section.";

		// Token: 0x040010EB RID: 4331
		public const string Xml_ExpectDtdMarkup = "Expected DTD markup was not found.";

		// Token: 0x040010EC RID: 4332
		public const string Xml_IncompleteDtdContent = "Incomplete DTD content.";

		// Token: 0x040010ED RID: 4333
		public const string Xml_EnumerationRequired = "Enumeration data type required.";

		// Token: 0x040010EE RID: 4334
		public const string Xml_InvalidContentModel = "Invalid content model.";

		// Token: 0x040010EF RID: 4335
		public const string Xml_FragmentId = "Fragment identifier '{0}' cannot be part of the system identifier '{1}'.";

		// Token: 0x040010F0 RID: 4336
		public const string Xml_ExpectPcData = "Expecting 'PCDATA'.";

		// Token: 0x040010F1 RID: 4337
		public const string Xml_ExpectNoWhitespace = "White space not allowed before '?', '*', or '+'.";

		// Token: 0x040010F2 RID: 4338
		public const string Xml_ExpectOp = "Expecting '?', '*', or '+'.";

		// Token: 0x040010F3 RID: 4339
		public const string Xml_InvalidAttributeType = "'{0}' is an invalid attribute type.";

		// Token: 0x040010F4 RID: 4340
		public const string Xml_InvalidAttributeType1 = "Invalid attribute type.";

		// Token: 0x040010F5 RID: 4341
		public const string Xml_ExpectAttType = "Expecting an attribute type.";

		// Token: 0x040010F6 RID: 4342
		public const string Xml_ColonInLocalName = "'{0}' is an unqualified name and cannot contain the character ':'.";

		// Token: 0x040010F7 RID: 4343
		public const string Xml_InvalidParEntityRef = "A parameter entity reference is not allowed in internal markup.";

		// Token: 0x040010F8 RID: 4344
		public const string Xml_ExpectSubOrClose = "Expecting an internal subset or the end of the DOCTYPE declaration.";

		// Token: 0x040010F9 RID: 4345
		public const string Xml_ExpectExternalOrPublicId = "Expecting a system identifier or a public identifier.";

		// Token: 0x040010FA RID: 4346
		public const string Xml_ExpectExternalIdOrEntityValue = "Expecting an external identifier or an entity value.";

		// Token: 0x040010FB RID: 4347
		public const string Xml_ExpectIgnoreOrInclude = "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.";

		// Token: 0x040010FC RID: 4348
		public const string Xml_UnsupportedClass = "Object type is not supported.";

		// Token: 0x040010FD RID: 4349
		public const string Xml_NullResolver = "Resolving of external URIs was prohibited.";

		// Token: 0x040010FE RID: 4350
		public const string Xml_RelativeUriNotSupported = "Relative URIs are not supported.";

		// Token: 0x040010FF RID: 4351
		public const string Xml_UntrustedCodeSettingResolver = "XmlResolver can be set only by fully trusted code.";

		// Token: 0x04001100 RID: 4352
		public const string Xml_WriterAsyncNotSetException = "Set XmlWriterSettings.Async to true if you want to use Async Methods.";

		// Token: 0x04001101 RID: 4353
		public const string Xml_PrefixForEmptyNs = "Cannot use a prefix with an empty namespace.";

		// Token: 0x04001102 RID: 4354
		public const string Xml_InvalidCommentChars = "An XML comment cannot contain '--', and '-' cannot be the last character.";

		// Token: 0x04001103 RID: 4355
		public const string Xml_UndefNamespace = "The '{0}' namespace is not defined.";

		// Token: 0x04001104 RID: 4356
		public const string Xml_EmptyName = "The empty string '' is not a valid name.";

		// Token: 0x04001105 RID: 4357
		public const string Xml_EmptyLocalName = "The empty string '' is not a valid local name.";

		// Token: 0x04001106 RID: 4358
		public const string Xml_InvalidNameCharsDetail = "Invalid name character in '{0}'. The '{1}' character, hexadecimal value {2}, cannot be included in a name.";

		// Token: 0x04001107 RID: 4359
		public const string Xml_NoStartTag = "There was no XML start tag open.";

		// Token: 0x04001108 RID: 4360
		public const string Xml_ClosedOrError = "The Writer is closed or in error state.";

		// Token: 0x04001109 RID: 4361
		public const string Xml_WrongToken = "Token {0} in state {1} would result in an invalid XML document.";

		// Token: 0x0400110A RID: 4362
		public const string Xml_XmlPrefix = "Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\".";

		// Token: 0x0400110B RID: 4363
		public const string Xml_XmlnsPrefix = "Prefix \"xmlns\" is reserved for use by XML.";

		// Token: 0x0400110C RID: 4364
		public const string Xml_NamespaceDeclXmlXmlns = "Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".";

		// Token: 0x0400110D RID: 4365
		public const string Xml_NonWhitespace = "Only white space characters should be used.";

		// Token: 0x0400110E RID: 4366
		public const string Xml_DupXmlDecl = "Cannot write XML declaration. WriteStartDocument method has already written it.";

		// Token: 0x0400110F RID: 4367
		public const string Xml_CannotWriteXmlDecl = "Cannot write XML declaration. XML declaration can be only at the beginning of the document.";

		// Token: 0x04001110 RID: 4368
		public const string Xml_NoRoot = "Document does not have a root element.";

		// Token: 0x04001111 RID: 4369
		public const string Xml_InvalidPosition = "The current position on the Reader is neither an element nor an attribute.";

		// Token: 0x04001112 RID: 4370
		public const string Xml_IncompleteEntity = "Incomplete entity contents.";

		// Token: 0x04001113 RID: 4371
		public const string Xml_InvalidSurrogateHighChar = "Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).";

		// Token: 0x04001114 RID: 4372
		public const string Xml_InvalidSurrogateMissingLowChar = "The surrogate pair is invalid. Missing a low surrogate character.";

		// Token: 0x04001115 RID: 4373
		public const string Xml_InvalidSurrogatePairWithArgs = "The surrogate pair (0x{0}, 0x{1}) is invalid. A high surrogate character (0xD800 - 0xDBFF) must always be paired with a low surrogate character (0xDC00 - 0xDFFF).";

		// Token: 0x04001116 RID: 4374
		public const string Xml_RedefinePrefix = "The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.";

		// Token: 0x04001117 RID: 4375
		public const string Xml_DtdAlreadyWritten = "The DTD has already been written out.";

		// Token: 0x04001118 RID: 4376
		public const string Xml_InvalidCharsInIndent = "XmlWriterSettings.{0} can contain only valid XML text content characters when XmlWriterSettings.CheckCharacters is true. {1}";

		// Token: 0x04001119 RID: 4377
		public const string Xml_IndentCharsNotWhitespace = "XmlWriterSettings.{0} can contain only valid XML white space characters when XmlWriterSettings.CheckCharacters and XmlWriterSettings.NewLineOnAttributes are true.";

		// Token: 0x0400111A RID: 4378
		public const string Xml_ConformanceLevelFragment = "Make sure that the ConformanceLevel setting is set to ConformanceLevel.Fragment or ConformanceLevel.Auto if you want to write an XML fragment.";

		// Token: 0x0400111B RID: 4379
		public const string Xml_InvalidQuote = "Invalid XML attribute quote character. Valid attribute quote characters are ' and \".";

		// Token: 0x0400111C RID: 4380
		public const string Xml_UndefPrefix = "An undefined prefix is in use.";

		// Token: 0x0400111D RID: 4381
		public const string Xml_NoNamespaces = "Cannot set the namespace if Namespaces is 'false'.";

		// Token: 0x0400111E RID: 4382
		public const string Xml_InvalidCDataChars = "Cannot have ']]>' inside an XML CDATA block.";

		// Token: 0x0400111F RID: 4383
		public const string Xml_NotTheFirst = "WriteStartDocument needs to be the first call.";

		// Token: 0x04001120 RID: 4384
		public const string Xml_InvalidPiChars = "Cannot have '?>' inside an XML processing instruction.";

		// Token: 0x04001121 RID: 4385
		public const string Xml_InvalidNameChars = "Invalid name character in '{0}'.";

		// Token: 0x04001122 RID: 4386
		public const string Xml_Closed = "The Writer is closed.";

		// Token: 0x04001123 RID: 4387
		public const string Xml_InvalidPrefix = "Prefixes beginning with \"xml\" (regardless of whether the characters are uppercase, lowercase, or some combination thereof) are reserved for use by XML.";

		// Token: 0x04001124 RID: 4388
		public const string Xml_InvalidIndentation = "Indentation value must be greater than 0.";

		// Token: 0x04001125 RID: 4389
		public const string Xml_NotInWriteState = "NotInWriteState.";

		// Token: 0x04001126 RID: 4390
		public const string Xml_SurrogatePairSplit = "The second character surrogate pair is not in the input buffer to be written.";

		// Token: 0x04001127 RID: 4391
		public const string Xml_NoMultipleRoots = "Document cannot have multiple document elements.";

		// Token: 0x04001128 RID: 4392
		public const string XmlBadName = "A node of type '{0}' cannot have the name '{1}'.";

		// Token: 0x04001129 RID: 4393
		public const string XmlNoNameAllowed = "A node of type '{0}' cannot have a name.";

		// Token: 0x0400112A RID: 4394
		public const string XmlConvert_BadUri = "The string was not recognized as a valid Uri.";

		// Token: 0x0400112B RID: 4395
		public const string XmlConvert_BadFormat = "The string '{0}' is not a valid {1} value.";

		// Token: 0x0400112C RID: 4396
		public const string XmlConvert_Overflow = "Value '{0}' was either too large or too small for {1}.";

		// Token: 0x0400112D RID: 4397
		public const string XmlConvert_TypeBadMapping = "Xml type '{0}' does not support Clr type '{1}'.";

		// Token: 0x0400112E RID: 4398
		public const string XmlConvert_TypeBadMapping2 = "Xml type '{0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.";

		// Token: 0x0400112F RID: 4399
		public const string XmlConvert_TypeListBadMapping = "Xml type 'List of {0}' does not support Clr type '{1}'.";

		// Token: 0x04001130 RID: 4400
		public const string XmlConvert_TypeListBadMapping2 = "Xml type 'List of {0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.";

		// Token: 0x04001131 RID: 4401
		public const string XmlConvert_TypeToString = "Xml type '{0}' cannot convert from Clr type '{1}' unless the destination type is String or XmlAtomicValue.";

		// Token: 0x04001132 RID: 4402
		public const string XmlConvert_TypeFromString = "Xml type '{0}' cannot convert to Clr type '{1}' unless the source value is a String or an XmlAtomicValue.";

		// Token: 0x04001133 RID: 4403
		public const string XmlConvert_TypeNoPrefix = "The QName '{0}' cannot be represented as a String.  A prefix for namespace '{1}' cannot be found.";

		// Token: 0x04001134 RID: 4404
		public const string XmlConvert_TypeNoNamespace = "The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.";

		// Token: 0x04001135 RID: 4405
		public const string XmlConvert_NotOneCharString = "String must be exactly one character long.";

		// Token: 0x04001136 RID: 4406
		public const string Sch_ParEntityRefNesting = "The parameter entity replacement text must nest properly within markup declarations.";

		// Token: 0x04001137 RID: 4407
		public const string Sch_NotTokenString = "line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.";

		// Token: 0x04001138 RID: 4408
		public const string Sch_XsdDateTimeCompare = "Cannot compare '{0}' and '{1}'.";

		// Token: 0x04001139 RID: 4409
		public const string Sch_InvalidNullCast = "Cannot return null as a value for type '{0}'.";

		// Token: 0x0400113A RID: 4410
		public const string Sch_InvalidDateTimeOption = "The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.";

		// Token: 0x0400113B RID: 4411
		public const string Sch_StandAloneNormalization = "StandAlone is 'yes' and the value of the attribute '{0}' contains a definition in an external document that changes on normalization.";

		// Token: 0x0400113C RID: 4412
		public const string Sch_UnSpecifiedDefaultAttributeInExternalStandalone = "Markup for unspecified default attribute '{0}' is external and standalone='yes'.";

		// Token: 0x0400113D RID: 4413
		public const string Sch_DefaultException = "A schema error occurred.";

		// Token: 0x0400113E RID: 4414
		public const string Sch_DupElementDecl = "The '{0}' element has already been declared.";

		// Token: 0x0400113F RID: 4415
		public const string Sch_IdAttrDeclared = "The attribute of type ID is already declared on the '{0}' element.";

		// Token: 0x04001140 RID: 4416
		public const string Sch_RootMatchDocType = "Root element name must match the DocType name.";

		// Token: 0x04001141 RID: 4417
		public const string Sch_DupId = "'{0}' is already used as an ID.";

		// Token: 0x04001142 RID: 4418
		public const string Sch_UndeclaredElement = "The '{0}' element is not declared.";

		// Token: 0x04001143 RID: 4419
		public const string Sch_UndeclaredAttribute = "The '{0}' attribute is not declared.";

		// Token: 0x04001144 RID: 4420
		public const string Sch_UndeclaredNotation = "The '{0}' notation is not declared.";

		// Token: 0x04001145 RID: 4421
		public const string Sch_UndeclaredId = "Reference to undeclared ID is '{0}'.";

		// Token: 0x04001146 RID: 4422
		public const string Sch_SchemaRootExpected = "Expected schema root. Make sure the root element is <schema> and the namespace is 'http://www.w3.org/2001/XMLSchema' for an XSD schema or 'urn:schemas-microsoft-com:xml-data' for an XDR schema.";

		// Token: 0x04001147 RID: 4423
		public const string Sch_XSDSchemaRootExpected = "The root element of a W3C XML Schema should be <schema> and its namespace should be 'http://www.w3.org/2001/XMLSchema'.";

		// Token: 0x04001148 RID: 4424
		public const string Sch_UnsupportedAttribute = "The '{0}' attribute is not supported in this context.";

		// Token: 0x04001149 RID: 4425
		public const string Sch_UnsupportedElement = "The '{0}' element is not supported in this context.";

		// Token: 0x0400114A RID: 4426
		public const string Sch_MissAttribute = "The '{0}' attribute is either invalid or missing.";

		// Token: 0x0400114B RID: 4427
		public const string Sch_AnnotationLocation = "The 'annotation' element cannot appear at this location.";

		// Token: 0x0400114C RID: 4428
		public const string Sch_DataTypeTextOnly = "Content must be \"textOnly\" when using DataType on an ElementType.";

		// Token: 0x0400114D RID: 4429
		public const string Sch_UnknownModel = "The model attribute must have a value of open or closed, not '{0}'.";

		// Token: 0x0400114E RID: 4430
		public const string Sch_UnknownOrder = "The order attribute must have a value of 'seq', 'one', or 'many', not '{0}'.";

		// Token: 0x0400114F RID: 4431
		public const string Sch_UnknownContent = "The content attribute must have a value of 'textOnly', 'eltOnly', 'mixed', or 'empty', not '{0}'.";

		// Token: 0x04001150 RID: 4432
		public const string Sch_UnknownRequired = "The required attribute must have a value of yes or no.";

		// Token: 0x04001151 RID: 4433
		public const string Sch_UnknownDtType = "Reference to an unknown data type, '{0}'.";

		// Token: 0x04001152 RID: 4434
		public const string Sch_MixedMany = "The order must be many when content is mixed.";

		// Token: 0x04001153 RID: 4435
		public const string Sch_GroupDisabled = "The group is not allowed when ElementType has empty or textOnly content.";

		// Token: 0x04001154 RID: 4436
		public const string Sch_MissDtvalue = "The DataType value cannot be empty.";

		// Token: 0x04001155 RID: 4437
		public const string Sch_MissDtvaluesAttribute = "The dt:values attribute is missing.";

		// Token: 0x04001156 RID: 4438
		public const string Sch_DupDtType = "Data type has already been declared.";

		// Token: 0x04001157 RID: 4439
		public const string Sch_DupAttribute = "The '{0}' attribute has already been declared for this ElementType.";

		// Token: 0x04001158 RID: 4440
		public const string Sch_RequireEnumeration = "Data type should be enumeration when the values attribute is present.";

		// Token: 0x04001159 RID: 4441
		public const string Sch_DefaultIdValue = "An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.";

		// Token: 0x0400115A RID: 4442
		public const string Sch_ElementNotAllowed = "Element is not allowed when the content is empty or textOnly.";

		// Token: 0x0400115B RID: 4443
		public const string Sch_ElementMissing = "There is a missing element.";

		// Token: 0x0400115C RID: 4444
		public const string Sch_ManyMaxOccurs = "When the order is many, the maxOccurs attribute must have a value of '*'.";

		// Token: 0x0400115D RID: 4445
		public const string Sch_MaxOccursInvalid = "The maxOccurs attribute must have a value of 1 or *.";

		// Token: 0x0400115E RID: 4446
		public const string Sch_MinOccursInvalid = "The minOccurs attribute must have a value of 0 or 1.";

		// Token: 0x0400115F RID: 4447
		public const string Sch_DtMaxLengthInvalid = "The value '{0}' is invalid for dt:maxLength.";

		// Token: 0x04001160 RID: 4448
		public const string Sch_DtMinLengthInvalid = "The value '{0}' is invalid for dt:minLength.";

		// Token: 0x04001161 RID: 4449
		public const string Sch_DupDtMaxLength = "The value of maxLength has already been declared.";

		// Token: 0x04001162 RID: 4450
		public const string Sch_DupDtMinLength = "The value of minLength has already been declared.";

		// Token: 0x04001163 RID: 4451
		public const string Sch_DtMinMaxLength = "The maxLength value must be equal to or greater than the minLength value.";

		// Token: 0x04001164 RID: 4452
		public const string Sch_DupElement = "The '{0}' element already exists in the content model.";

		// Token: 0x04001165 RID: 4453
		public const string Sch_DupGroupParticle = "The content model can only have one of the following; 'all', 'choice', or 'sequence'.";

		// Token: 0x04001166 RID: 4454
		public const string Sch_InvalidValue = "The value '{0}' is invalid according to its data type.";

		// Token: 0x04001167 RID: 4455
		public const string Sch_InvalidValueDetailed = "The value '{0}' is invalid according to its schema type '{1}' - {2}";

		// Token: 0x04001168 RID: 4456
		public const string Sch_InvalidValueDetailedAttribute = "The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}";

		// Token: 0x04001169 RID: 4457
		public const string Sch_MissRequiredAttribute = "The required attribute '{0}' is missing.";

		// Token: 0x0400116A RID: 4458
		public const string Sch_FixedAttributeValue = "The value of the '{0}' attribute does not equal its fixed value.";

		// Token: 0x0400116B RID: 4459
		public const string Sch_FixedElementValue = "The value of the '{0}' element does not equal its fixed value.";

		// Token: 0x0400116C RID: 4460
		public const string Sch_AttributeValueDataTypeDetailed = "The '{0}' attribute is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}";

		// Token: 0x0400116D RID: 4461
		public const string Sch_AttributeDefaultDataType = "The default value of '{0}' attribute is invalid according to its datatype.";

		// Token: 0x0400116E RID: 4462
		public const string Sch_IncludeLocation = "The 'include' element cannot appear at this location.";

		// Token: 0x0400116F RID: 4463
		public const string Sch_ImportLocation = "The 'import' element cannot appear at this location.";

		// Token: 0x04001170 RID: 4464
		public const string Sch_RedefineLocation = "The 'redefine' element cannot appear at this location.";

		// Token: 0x04001171 RID: 4465
		public const string Sch_InvalidBlockDefaultValue = "The values 'list' and 'union' are invalid for the blockDefault attribute.";

		// Token: 0x04001172 RID: 4466
		public const string Sch_InvalidFinalDefaultValue = "The value 'substitution' is invalid for the finalDefault attribute.";

		// Token: 0x04001173 RID: 4467
		public const string Sch_InvalidElementBlockValue = "The values 'list' and 'union' are invalid for the block attribute on element.";

		// Token: 0x04001174 RID: 4468
		public const string Sch_InvalidElementFinalValue = "The values 'substitution', 'list', and 'union' are invalid for the final attribute on element.";

		// Token: 0x04001175 RID: 4469
		public const string Sch_InvalidSimpleTypeFinalValue = "The values 'substitution' and 'extension' are invalid for the final attribute on simpleType.";

		// Token: 0x04001176 RID: 4470
		public const string Sch_InvalidComplexTypeBlockValue = "The values 'substitution', 'list', and 'union' are invalid for the block attribute on complexType.";

		// Token: 0x04001177 RID: 4471
		public const string Sch_InvalidComplexTypeFinalValue = "The values 'substitution', 'list', and 'union' are invalid for the final attribute on complexType.";

		// Token: 0x04001178 RID: 4472
		public const string Sch_DupIdentityConstraint = "The identity constraint '{0}' has already been declared.";

		// Token: 0x04001179 RID: 4473
		public const string Sch_DupGlobalElement = "The global element '{0}' has already been declared.";

		// Token: 0x0400117A RID: 4474
		public const string Sch_DupGlobalAttribute = "The global attribute '{0}' has already been declared.";

		// Token: 0x0400117B RID: 4475
		public const string Sch_DupSimpleType = "The simpleType '{0}' has already been declared.";

		// Token: 0x0400117C RID: 4476
		public const string Sch_DupComplexType = "The complexType '{0}' has already been declared.";

		// Token: 0x0400117D RID: 4477
		public const string Sch_DupGroup = "The group '{0}' has already been declared.";

		// Token: 0x0400117E RID: 4478
		public const string Sch_DupAttributeGroup = "The attributeGroup '{0}' has already been declared.";

		// Token: 0x0400117F RID: 4479
		public const string Sch_DupNotation = "The notation '{0}' has already been declared.";

		// Token: 0x04001180 RID: 4480
		public const string Sch_DefaultFixedAttributes = "The fixed and default attributes cannot both be present.";

		// Token: 0x04001181 RID: 4481
		public const string Sch_FixedInRef = "The fixed value constraint on the '{0}' attribute reference must match the fixed value constraint on the declaration.";

		// Token: 0x04001182 RID: 4482
		public const string Sch_FixedDefaultInRef = "The default value constraint cannot be present on the '{0}' attribute reference if the fixed value constraint is present on the declaration.";

		// Token: 0x04001183 RID: 4483
		public const string Sch_DupXsdElement = "'{0}' is a duplicate XSD element.";

		// Token: 0x04001184 RID: 4484
		public const string Sch_ForbiddenAttribute = "The '{0}' attribute cannot be present.";

		// Token: 0x04001185 RID: 4485
		public const string Sch_AttributeIgnored = "The '{0}' attribute is ignored, because the value of 'prohibited' for attribute use only prevents inheritance of an identically named attribute from the base type definition.";

		// Token: 0x04001186 RID: 4486
		public const string Sch_ElementRef = "When the ref attribute is present, the type attribute and complexType, simpleType, key, keyref, and unique elements cannot be present.";

		// Token: 0x04001187 RID: 4487
		public const string Sch_TypeMutualExclusive = "The type attribute cannot be present with either simpleType or complexType.";

		// Token: 0x04001188 RID: 4488
		public const string Sch_ElementNameRef = "For element declaration, either the name or the ref attribute must be present.";

		// Token: 0x04001189 RID: 4489
		public const string Sch_AttributeNameRef = "For attribute '{0}', either the name or the ref attribute must be present, but not both.";

		// Token: 0x0400118A RID: 4490
		public const string Sch_TextNotAllowed = "The following text is not allowed in this context: '{0}'.";

		// Token: 0x0400118B RID: 4491
		public const string Sch_UndeclaredType = "Type '{0}' is not declared.";

		// Token: 0x0400118C RID: 4492
		public const string Sch_UndeclaredSimpleType = "Type '{0}' is not declared, or is not a simple type.";

		// Token: 0x0400118D RID: 4493
		public const string Sch_UndeclaredEquivClass = "Substitution group refers to '{0}', an undeclared element.";

		// Token: 0x0400118E RID: 4494
		public const string Sch_AttListPresence = "An attribute of type ID must have a declared default of either #IMPLIED or #REQUIRED.";

		// Token: 0x0400118F RID: 4495
		public const string Sch_NotationValue = "'{0}' is not in the notation list.";

		// Token: 0x04001190 RID: 4496
		public const string Sch_EnumerationValue = "'{0}' is not in the enumeration list.";

		// Token: 0x04001191 RID: 4497
		public const string Sch_EmptyAttributeValue = "The attribute value cannot be empty.";

		// Token: 0x04001192 RID: 4498
		public const string Sch_InvalidLanguageId = "'{0}' is an invalid language identifier.";

		// Token: 0x04001193 RID: 4499
		public const string Sch_XmlSpace = "Invalid xml:space syntax.";

		// Token: 0x04001194 RID: 4500
		public const string Sch_InvalidXsdAttributeValue = "'{1}' is an invalid value for the '{0}' attribute.";

		// Token: 0x04001195 RID: 4501
		public const string Sch_InvalidXsdAttributeDatatypeValue = "The value for the '{0}' attribute is invalid - {1}";

		// Token: 0x04001196 RID: 4502
		public const string Sch_ElementValueDataTypeDetailed = "The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}";

		// Token: 0x04001197 RID: 4503
		public const string Sch_InvalidElementDefaultValue = "The default value '{0}' of element '{1}' is invalid according to the type specified by xsi:type.";

		// Token: 0x04001198 RID: 4504
		public const string Sch_NonDeterministic = "Multiple definition of element '{0}' causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

		// Token: 0x04001199 RID: 4505
		public const string Sch_NonDeterministicAnyEx = "Wildcard '{0}' allows element '{1}', and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

		// Token: 0x0400119A RID: 4506
		public const string Sch_NonDeterministicAnyAny = "Wildcards '{0}' and '{1}' have not empty intersection, and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

		// Token: 0x0400119B RID: 4507
		public const string Sch_StandAlone = "The standalone document declaration must have a value of 'no'.";

		// Token: 0x0400119C RID: 4508
		public const string Sch_XmlNsAttribute = "The value 'xmlns' cannot be used as the name of an attribute declaration.";

		// Token: 0x0400119D RID: 4509
		public const string Sch_AllElement = "Element '{0}' cannot appear more than once if content model type is \"all\".";

		// Token: 0x0400119E RID: 4510
		public const string Sch_MismatchTargetNamespaceInclude = "The targetNamespace '{0}' of included/redefined schema should be the same as the targetNamespace '{1}' of the including schema.";

		// Token: 0x0400119F RID: 4511
		public const string Sch_MismatchTargetNamespaceImport = "The namespace attribute '{0}' of an import should be the same value as the targetNamespace '{1}' of the imported schema.";

		// Token: 0x040011A0 RID: 4512
		public const string Sch_MismatchTargetNamespaceEx = "The targetNamespace parameter '{0}' should be the same value as the targetNamespace '{1}' of the schema.";

		// Token: 0x040011A1 RID: 4513
		public const string Sch_XsiTypeNotFound = "This is an invalid xsi:type '{0}'.";

		// Token: 0x040011A2 RID: 4514
		public const string Sch_XsiTypeAbstract = "The xsi:type '{0}' cannot be abstract.";

		// Token: 0x040011A3 RID: 4515
		public const string Sch_ListFromNonatomic = "A list data type must be derived from an atomic or union data type.";

		// Token: 0x040011A4 RID: 4516
		public const string Sch_UnionFromUnion = "It is an error if a union type has a member with variety union and this member cannot be substituted with its own members. This may be due to the fact that the union member is a restriction of a union with facets.";

		// Token: 0x040011A5 RID: 4517
		public const string Sch_DupLengthFacet = "This is a duplicate Length constraining facet.";

		// Token: 0x040011A6 RID: 4518
		public const string Sch_DupMinLengthFacet = "This is a duplicate MinLength constraining facet.";

		// Token: 0x040011A7 RID: 4519
		public const string Sch_DupMaxLengthFacet = "This is a duplicate MaxLength constraining facet.";

		// Token: 0x040011A8 RID: 4520
		public const string Sch_DupWhiteSpaceFacet = "This is a duplicate WhiteSpace constraining facet.";

		// Token: 0x040011A9 RID: 4521
		public const string Sch_DupMaxInclusiveFacet = "This is a duplicate MaxInclusive constraining facet.";

		// Token: 0x040011AA RID: 4522
		public const string Sch_DupMaxExclusiveFacet = "This is a duplicate MaxExclusive constraining facet.";

		// Token: 0x040011AB RID: 4523
		public const string Sch_DupMinInclusiveFacet = "This is a duplicate MinInclusive constraining facet.";

		// Token: 0x040011AC RID: 4524
		public const string Sch_DupMinExclusiveFacet = "This is a duplicate MinExclusive constraining facet.";

		// Token: 0x040011AD RID: 4525
		public const string Sch_DupTotalDigitsFacet = "This is a duplicate TotalDigits constraining facet.";

		// Token: 0x040011AE RID: 4526
		public const string Sch_DupFractionDigitsFacet = "This is a duplicate FractionDigits constraining facet.";

		// Token: 0x040011AF RID: 4527
		public const string Sch_LengthFacetProhibited = "The length constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B0 RID: 4528
		public const string Sch_MinLengthFacetProhibited = "The MinLength constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B1 RID: 4529
		public const string Sch_MaxLengthFacetProhibited = "The MaxLength constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B2 RID: 4530
		public const string Sch_PatternFacetProhibited = "The Pattern constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B3 RID: 4531
		public const string Sch_EnumerationFacetProhibited = "The Enumeration constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B4 RID: 4532
		public const string Sch_WhiteSpaceFacetProhibited = "The WhiteSpace constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B5 RID: 4533
		public const string Sch_MaxInclusiveFacetProhibited = "The MaxInclusive constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B6 RID: 4534
		public const string Sch_MaxExclusiveFacetProhibited = "The MaxExclusive constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B7 RID: 4535
		public const string Sch_MinInclusiveFacetProhibited = "The MinInclusive constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B8 RID: 4536
		public const string Sch_MinExclusiveFacetProhibited = "The MinExclusive constraining facet is prohibited for '{0}'.";

		// Token: 0x040011B9 RID: 4537
		public const string Sch_TotalDigitsFacetProhibited = "The TotalDigits constraining facet is prohibited for '{0}'.";

		// Token: 0x040011BA RID: 4538
		public const string Sch_FractionDigitsFacetProhibited = "The FractionDigits constraining facet is prohibited for '{0}'.";

		// Token: 0x040011BB RID: 4539
		public const string Sch_LengthFacetInvalid = "The Length constraining facet is invalid - {0}";

		// Token: 0x040011BC RID: 4540
		public const string Sch_MinLengthFacetInvalid = "The MinLength constraining facet is invalid - {0}";

		// Token: 0x040011BD RID: 4541
		public const string Sch_MaxLengthFacetInvalid = "The MaxLength constraining facet is invalid - {0}";

		// Token: 0x040011BE RID: 4542
		public const string Sch_MaxInclusiveFacetInvalid = "The MaxInclusive constraining facet is invalid - {0}";

		// Token: 0x040011BF RID: 4543
		public const string Sch_MaxExclusiveFacetInvalid = "The MaxExclusive constraining facet is invalid - {0}";

		// Token: 0x040011C0 RID: 4544
		public const string Sch_MinInclusiveFacetInvalid = "The MinInclusive constraining facet is invalid - {0}";

		// Token: 0x040011C1 RID: 4545
		public const string Sch_MinExclusiveFacetInvalid = "The MinExclusive constraining facet is invalid - {0}";

		// Token: 0x040011C2 RID: 4546
		public const string Sch_TotalDigitsFacetInvalid = "The TotalDigits constraining facet is invalid - {0}";

		// Token: 0x040011C3 RID: 4547
		public const string Sch_FractionDigitsFacetInvalid = "The FractionDigits constraining facet is invalid - {0}";

		// Token: 0x040011C4 RID: 4548
		public const string Sch_PatternFacetInvalid = "The Pattern constraining facet is invalid - {0}";

		// Token: 0x040011C5 RID: 4549
		public const string Sch_EnumerationFacetInvalid = "The Enumeration constraining facet is invalid - {0}";

		// Token: 0x040011C6 RID: 4550
		public const string Sch_InvalidWhiteSpace = "The white space character, '{0}', is invalid.";

		// Token: 0x040011C7 RID: 4551
		public const string Sch_UnknownFacet = "This is an unknown facet.";

		// Token: 0x040011C8 RID: 4552
		public const string Sch_LengthAndMinMax = "It is an error for both length and minLength or maxLength to be present.";

		// Token: 0x040011C9 RID: 4553
		public const string Sch_MinLengthGtMaxLength = "MinLength is greater than MaxLength.";

		// Token: 0x040011CA RID: 4554
		public const string Sch_FractionDigitsGtTotalDigits = "FractionDigits is greater than TotalDigits.";

		// Token: 0x040011CB RID: 4555
		public const string Sch_LengthConstraintFailed = "The actual length is not equal to the specified length.";

		// Token: 0x040011CC RID: 4556
		public const string Sch_MinLengthConstraintFailed = "The actual length is less than the MinLength value.";

		// Token: 0x040011CD RID: 4557
		public const string Sch_MaxLengthConstraintFailed = "The actual length is greater than the MaxLength value.";

		// Token: 0x040011CE RID: 4558
		public const string Sch_PatternConstraintFailed = "The Pattern constraint failed.";

		// Token: 0x040011CF RID: 4559
		public const string Sch_EnumerationConstraintFailed = "The Enumeration constraint failed.";

		// Token: 0x040011D0 RID: 4560
		public const string Sch_MaxInclusiveConstraintFailed = "The MaxInclusive constraint failed.";

		// Token: 0x040011D1 RID: 4561
		public const string Sch_MaxExclusiveConstraintFailed = "The MaxExclusive constraint failed.";

		// Token: 0x040011D2 RID: 4562
		public const string Sch_MinInclusiveConstraintFailed = "The MinInclusive constraint failed.";

		// Token: 0x040011D3 RID: 4563
		public const string Sch_MinExclusiveConstraintFailed = "The MinExclusive constraint failed.";

		// Token: 0x040011D4 RID: 4564
		public const string Sch_TotalDigitsConstraintFailed = "The TotalDigits constraint failed.";

		// Token: 0x040011D5 RID: 4565
		public const string Sch_FractionDigitsConstraintFailed = "The FractionDigits constraint failed.";

		// Token: 0x040011D6 RID: 4566
		public const string Sch_UnionFailedEx = "The value '{0}' is not valid according to any of the memberTypes of the union.";

		// Token: 0x040011D7 RID: 4567
		public const string Sch_NotationRequired = "NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.";

		// Token: 0x040011D8 RID: 4568
		public const string Sch_DupNotationAttribute = "No element type can have more than one NOTATION attribute specified.";

		// Token: 0x040011D9 RID: 4569
		public const string Sch_MissingPublicSystemAttribute = "NOTATION must have either the Public or System attribute present.";

		// Token: 0x040011DA RID: 4570
		public const string Sch_NotationAttributeOnEmptyElement = "An attribute of type NOTATION must not be declared on an element declared EMPTY.";

		// Token: 0x040011DB RID: 4571
		public const string Sch_RefNotInScope = "The Keyref '{0}' cannot find the referred key or unique in scope.";

		// Token: 0x040011DC RID: 4572
		public const string Sch_UndeclaredIdentityConstraint = "The '{0}' identity constraint is not declared.";

		// Token: 0x040011DD RID: 4573
		public const string Sch_RefInvalidIdentityConstraint = "Reference to an invalid identity constraint, '{0}'.";

		// Token: 0x040011DE RID: 4574
		public const string Sch_RefInvalidCardin = "Keyref '{0}' has different cardinality as the referred key or unique element.";

		// Token: 0x040011DF RID: 4575
		public const string Sch_ReftoKeyref = "The '{0}' Keyref can refer to key or unique only.";

		// Token: 0x040011E0 RID: 4576
		public const string Sch_EmptyXPath = "The XPath for selector or field cannot be empty.";

		// Token: 0x040011E1 RID: 4577
		public const string Sch_UnresolvedPrefix = "The prefix '{0}' in XPath cannot be resolved.";

		// Token: 0x040011E2 RID: 4578
		public const string Sch_UnresolvedKeyref = "The key sequence '{0}' in '{1}' Keyref fails to refer to some key.";

		// Token: 0x040011E3 RID: 4579
		public const string Sch_ICXpathError = "'{0}' is an invalid XPath for selector or field.";

		// Token: 0x040011E4 RID: 4580
		public const string Sch_SelectorAttr = "'{0}' is an invalid XPath for selector. Selector cannot have an XPath selection with an attribute node.";

		// Token: 0x040011E5 RID: 4581
		public const string Sch_FieldSimpleTypeExpected = "The field '{0}' is expecting an element or attribute with simple type or simple content.";

		// Token: 0x040011E6 RID: 4582
		public const string Sch_FieldSingleValueExpected = "The field '{0}' is expecting at the most one value.";

		// Token: 0x040011E7 RID: 4583
		public const string Sch_MissingKey = "The identity constraint '{0}' validation has failed. Either a key is missing or the existing key has an empty node.";

		// Token: 0x040011E8 RID: 4584
		public const string Sch_DuplicateKey = "There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.";

		// Token: 0x040011E9 RID: 4585
		public const string Sch_TargetNamespaceXsi = "The target namespace of an attribute declaration, whether local or global, must not match http://www.w3.org/2001/XMLSchema-instance.";

		// Token: 0x040011EA RID: 4586
		public const string Sch_UndeclaredEntity = "Reference to an undeclared entity, '{0}'.";

		// Token: 0x040011EB RID: 4587
		public const string Sch_UnparsedEntityRef = "Reference to an unparsed entity, '{0}'.";

		// Token: 0x040011EC RID: 4588
		public const string Sch_MaxOccursInvalidXsd = "The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.";

		// Token: 0x040011ED RID: 4589
		public const string Sch_MinOccursInvalidXsd = "The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.";

		// Token: 0x040011EE RID: 4590
		public const string Sch_MaxInclusiveExclusive = "'maxInclusive' and 'maxExclusive' cannot both be specified for the same data type.";

		// Token: 0x040011EF RID: 4591
		public const string Sch_MinInclusiveExclusive = "'minInclusive' and 'minExclusive' cannot both be specified for the same data type.";

		// Token: 0x040011F0 RID: 4592
		public const string Sch_MinInclusiveGtMaxInclusive = "The value specified for 'minInclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.";

		// Token: 0x040011F1 RID: 4593
		public const string Sch_MinExclusiveGtMaxExclusive = "The value specified for 'minExclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.";

		// Token: 0x040011F2 RID: 4594
		public const string Sch_MinInclusiveGtMaxExclusive = "The value specified for 'minInclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.";

		// Token: 0x040011F3 RID: 4595
		public const string Sch_MinExclusiveGtMaxInclusive = "The value specified for 'minExclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.";

		// Token: 0x040011F4 RID: 4596
		public const string Sch_SimpleTypeRestriction = "'simpleType' should be the first child of restriction.";

		// Token: 0x040011F5 RID: 4597
		public const string Sch_InvalidFacetPosition = "Facet should go before 'attribute', 'attributeGroup', or 'anyAttribute'.";

		// Token: 0x040011F6 RID: 4598
		public const string Sch_AttributeMutuallyExclusive = "'{0}' and content model are mutually exclusive.";

		// Token: 0x040011F7 RID: 4599
		public const string Sch_AnyAttributeLastChild = "'anyAttribute' must be the last child.";

		// Token: 0x040011F8 RID: 4600
		public const string Sch_ComplexTypeContentModel = "The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.";

		// Token: 0x040011F9 RID: 4601
		public const string Sch_ComplexContentContentModel = "Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.";

		// Token: 0x040011FA RID: 4602
		public const string Sch_NotNormalizedString = "Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.";

		// Token: 0x040011FB RID: 4603
		public const string Sch_FractionDigitsNotOnDecimal = "FractionDigits should be equal to 0 on types other then decimal.";

		// Token: 0x040011FC RID: 4604
		public const string Sch_ContentInNill = "Element '{0}' must have no character or element children.";

		// Token: 0x040011FD RID: 4605
		public const string Sch_NoElementSchemaFound = "Could not find schema information for the element '{0}'.";

		// Token: 0x040011FE RID: 4606
		public const string Sch_NoAttributeSchemaFound = "Could not find schema information for the attribute '{0}'.";

		// Token: 0x040011FF RID: 4607
		public const string Sch_InvalidNamespace = "The Namespace '{0}' is an invalid URI.";

		// Token: 0x04001200 RID: 4608
		public const string Sch_InvalidTargetNamespaceAttribute = "The targetNamespace attribute cannot have empty string as its value.";

		// Token: 0x04001201 RID: 4609
		public const string Sch_InvalidNamespaceAttribute = "The namespace attribute cannot have empty string as its value.";

		// Token: 0x04001202 RID: 4610
		public const string Sch_InvalidSchemaLocation = "The SchemaLocation '{0}' is an invalid URI.";

		// Token: 0x04001203 RID: 4611
		public const string Sch_ImportTargetNamespace = "Namespace attribute of an import must not match the real value of the enclosing targetNamespace of the <schema>.";

		// Token: 0x04001204 RID: 4612
		public const string Sch_ImportTargetNamespaceNull = "The enclosing <schema> must have a targetNamespace, if the Namespace attribute is absent on the import element.";

		// Token: 0x04001205 RID: 4613
		public const string Sch_GroupDoubleRedefine = "Double redefine for group.";

		// Token: 0x04001206 RID: 4614
		public const string Sch_ComponentRedefineNotFound = "Cannot find a {0} with name '{1}' to redefine.";

		// Token: 0x04001207 RID: 4615
		public const string Sch_GroupRedefineNotFound = "No group to redefine.";

		// Token: 0x04001208 RID: 4616
		public const string Sch_AttrGroupDoubleRedefine = "Double redefine for attribute group.";

		// Token: 0x04001209 RID: 4617
		public const string Sch_AttrGroupRedefineNotFound = "No attribute group to redefine.";

		// Token: 0x0400120A RID: 4618
		public const string Sch_ComplexTypeDoubleRedefine = "Double redefine for complex type.";

		// Token: 0x0400120B RID: 4619
		public const string Sch_ComplexTypeRedefineNotFound = "No complex type to redefine.";

		// Token: 0x0400120C RID: 4620
		public const string Sch_SimpleToComplexTypeRedefine = "Cannot redefine a simple type as complex type.";

		// Token: 0x0400120D RID: 4621
		public const string Sch_SimpleTypeDoubleRedefine = "Double redefine for simple type.";

		// Token: 0x0400120E RID: 4622
		public const string Sch_ComplexToSimpleTypeRedefine = "Cannot redefine a complex type as simple type.";

		// Token: 0x0400120F RID: 4623
		public const string Sch_SimpleTypeRedefineNotFound = "No simple type to redefine.";

		// Token: 0x04001210 RID: 4624
		public const string Sch_MinMaxGroupRedefine = "When group is redefined, the real value of both minOccurs and maxOccurs attribute must be 1 (or absent).";

		// Token: 0x04001211 RID: 4625
		public const string Sch_MultipleGroupSelfRef = "Multiple self-reference within a group is redefined.";

		// Token: 0x04001212 RID: 4626
		public const string Sch_MultipleAttrGroupSelfRef = "Multiple self-reference within an attribute group is redefined.";

		// Token: 0x04001213 RID: 4627
		public const string Sch_InvalidTypeRedefine = "If type is being redefined, the base type has to be self-referenced.";

		// Token: 0x04001214 RID: 4628
		public const string Sch_InvalidElementRef = "If ref is present, all of <complexType>, <simpleType>, <key>, <keyref>, <unique>, nillable, default, fixed, form, block, and type must be absent.";

		// Token: 0x04001215 RID: 4629
		public const string Sch_MinGtMax = "minOccurs value cannot be greater than maxOccurs value.";

		// Token: 0x04001216 RID: 4630
		public const string Sch_DupSelector = "Selector cannot appear twice in one identity constraint.";

		// Token: 0x04001217 RID: 4631
		public const string Sch_IdConstraintNoSelector = "Selector must be present.";

		// Token: 0x04001218 RID: 4632
		public const string Sch_IdConstraintNoFields = "At least one field must be present.";

		// Token: 0x04001219 RID: 4633
		public const string Sch_IdConstraintNoRefer = "The referring attribute must be present.";

		// Token: 0x0400121A RID: 4634
		public const string Sch_SelectorBeforeFields = "Cannot define fields before selector.";

		// Token: 0x0400121B RID: 4635
		public const string Sch_NoSimpleTypeContent = "SimpleType content is missing.";

		// Token: 0x0400121C RID: 4636
		public const string Sch_SimpleTypeRestRefBase = "SimpleType restriction should have either the base attribute or a simpleType child, but not both.";

		// Token: 0x0400121D RID: 4637
		public const string Sch_SimpleTypeRestRefBaseNone = "SimpleType restriction should have either the base attribute or a simpleType child to indicate the base type for the derivation.";

		// Token: 0x0400121E RID: 4638
		public const string Sch_SimpleTypeListRefBase = "SimpleType list should have either the itemType attribute or a simpleType child, but not both.";

		// Token: 0x0400121F RID: 4639
		public const string Sch_SimpleTypeListRefBaseNone = "SimpleType list should have either the itemType attribute or a simpleType child to indicate the itemType of the list.";

		// Token: 0x04001220 RID: 4640
		public const string Sch_SimpleTypeUnionNoBase = "Either the memberTypes attribute must be non-empty or there must be at least one simpleType child.";

		// Token: 0x04001221 RID: 4641
		public const string Sch_NoRestOrExtQName = "'restriction' or 'extension' child is required for complexType '{0}' in namespace '{1}', because it has a simpleContent or complexContent child.";

		// Token: 0x04001222 RID: 4642
		public const string Sch_NoRestOrExt = "'restriction' or 'extension' child is required for complexType with simpleContent or complexContent child.";

		// Token: 0x04001223 RID: 4643
		public const string Sch_NoGroupParticle = "'sequence', 'choice', or 'all' child is required.";

		// Token: 0x04001224 RID: 4644
		public const string Sch_InvalidAllMin = "'all' must have 'minOccurs' value of 0 or 1.";

		// Token: 0x04001225 RID: 4645
		public const string Sch_InvalidAllMax = "'all' must have {max occurs}=1.";

		// Token: 0x04001226 RID: 4646
		public const string Sch_InvalidFacet = "The 'value' attribute must be present in facet.";

		// Token: 0x04001227 RID: 4647
		public const string Sch_AbstractElement = "The element '{0}' is abstract or its type is abstract.";

		// Token: 0x04001228 RID: 4648
		public const string Sch_XsiTypeBlockedEx = "The xsi:type attribute value '{0}' is not valid for the element '{1}', either because it is not a type validly derived from the type in the schema, or because it has xsi:type derivation blocked.";

		// Token: 0x04001229 RID: 4649
		public const string Sch_InvalidXsiNill = "If the 'nillable' attribute is false in the schema, the 'xsi:nil' attribute must not be present in the instance.";

		// Token: 0x0400122A RID: 4650
		public const string Sch_SubstitutionNotAllowed = "Element '{0}' cannot substitute in place of head element '{1}' because it has block='substitution'.";

		// Token: 0x0400122B RID: 4651
		public const string Sch_SubstitutionBlocked = "Member element {0}'s type cannot be derived by restriction or extension from head element {1}'s type, because it has block='restriction' or 'extension'.";

		// Token: 0x0400122C RID: 4652
		public const string Sch_InvalidElementInEmptyEx = "The element '{0}' cannot contain child element '{1}' because the parent element's content model is empty.";

		// Token: 0x0400122D RID: 4653
		public const string Sch_InvalidElementInTextOnlyEx = "The element '{0}' cannot contain child element '{1}' because the parent element's content model is text only.";

		// Token: 0x0400122E RID: 4654
		public const string Sch_InvalidTextInElement = "The element {0} cannot contain text.";

		// Token: 0x0400122F RID: 4655
		public const string Sch_InvalidElementContent = "The element {0} has invalid child element {1}.";

		// Token: 0x04001230 RID: 4656
		public const string Sch_InvalidElementContentComplex = "The element {0} has invalid child element {1} - {2}";

		// Token: 0x04001231 RID: 4657
		public const string Sch_IncompleteContent = "The element {0} has incomplete content.";

		// Token: 0x04001232 RID: 4658
		public const string Sch_IncompleteContentComplex = "The element {0} has incomplete content - {2}";

		// Token: 0x04001233 RID: 4659
		public const string Sch_InvalidTextInElementExpecting = "The element {0} cannot contain text. List of possible elements expected: {1}.";

		// Token: 0x04001234 RID: 4660
		public const string Sch_InvalidElementContentExpecting = "The element {0} has invalid child element {1}. List of possible elements expected: {2}.";

		// Token: 0x04001235 RID: 4661
		public const string Sch_InvalidElementContentExpectingComplex = "The element {0} has invalid child element {1}. List of possible elements expected: {2}. {3}";

		// Token: 0x04001236 RID: 4662
		public const string Sch_IncompleteContentExpecting = "The element {0} has incomplete content. List of possible elements expected: {1}.";

		// Token: 0x04001237 RID: 4663
		public const string Sch_IncompleteContentExpectingComplex = "The element {0} has incomplete content. List of possible elements expected: {1}. {2}";

		// Token: 0x04001238 RID: 4664
		public const string Sch_InvalidElementSubstitution = "The element {0} cannot substitute for a local element {1} expected in that position.";

		// Token: 0x04001239 RID: 4665
		public const string Sch_ElementNameAndNamespace = "'{0}' in namespace '{1}'";

		// Token: 0x0400123A RID: 4666
		public const string Sch_ElementName = "'{0}'";

		// Token: 0x0400123B RID: 4667
		public const string Sch_ContinuationString = "{0}as well as";

		// Token: 0x0400123C RID: 4668
		public const string Sch_AnyElementNS = "any element in namespace '{0}'";

		// Token: 0x0400123D RID: 4669
		public const string Sch_AnyElement = "any element";

		// Token: 0x0400123E RID: 4670
		public const string Sch_InvalidTextInEmpty = "The element cannot contain text. Content model is empty.";

		// Token: 0x0400123F RID: 4671
		public const string Sch_InvalidWhitespaceInEmpty = "The element cannot contain white space. Content model is empty.";

		// Token: 0x04001240 RID: 4672
		public const string Sch_InvalidPIComment = "The element cannot contain comment or processing instruction. Content model is empty.";

		// Token: 0x04001241 RID: 4673
		public const string Sch_InvalidAttributeRef = "If ref is present, all of 'simpleType', 'form', 'type', and 'use' must be absent.";

		// Token: 0x04001242 RID: 4674
		public const string Sch_OptionalDefaultAttribute = "The 'use' attribute must be optional (or absent) if the default attribute is present.";

		// Token: 0x04001243 RID: 4675
		public const string Sch_AttributeCircularRef = "Circular attribute reference.";

		// Token: 0x04001244 RID: 4676
		public const string Sch_IdentityConstraintCircularRef = "Circular identity constraint reference.";

		// Token: 0x04001245 RID: 4677
		public const string Sch_SubstitutionCircularRef = "Circular substitution group affiliation.";

		// Token: 0x04001246 RID: 4678
		public const string Sch_InvalidAnyAttribute = "Invalid namespace in 'anyAttribute'.";

		// Token: 0x04001247 RID: 4679
		public const string Sch_DupIdAttribute = "Duplicate ID attribute.";

		// Token: 0x04001248 RID: 4680
		public const string Sch_InvalidAllElementMax = "The {max occurs} of all the particles in the {particles} of an all group must be 0 or 1.";

		// Token: 0x04001249 RID: 4681
		public const string Sch_InvalidAny = "Invalid namespace in 'any'.";

		// Token: 0x0400124A RID: 4682
		public const string Sch_InvalidAnyDetailed = "The value of the namespace attribute of the element or attribute wildcard is invalid - {0}";

		// Token: 0x0400124B RID: 4683
		public const string Sch_InvalidExamplar = "Cannot be nominated as the {substitution group affiliation} of any other declaration.";

		// Token: 0x0400124C RID: 4684
		public const string Sch_NoExamplar = "Reference to undeclared substitution group affiliation.";

		// Token: 0x0400124D RID: 4685
		public const string Sch_InvalidSubstitutionMember = "'{0}' cannot be a member of substitution group with head element '{1}'.";

		// Token: 0x0400124E RID: 4686
		public const string Sch_RedefineNoSchema = "'SchemaLocation' must successfully resolve if <redefine> contains any child other than <annotation>.";

		// Token: 0x0400124F RID: 4687
		public const string Sch_ProhibitedAttribute = "The '{0}' attribute is not allowed.";

		// Token: 0x04001250 RID: 4688
		public const string Sch_TypeCircularRef = "Circular type reference.";

		// Token: 0x04001251 RID: 4689
		public const string Sch_TwoIdAttrUses = "Two distinct members of the attribute uses must not have type definitions which are both xs:ID or are derived from xs:ID.";

		// Token: 0x04001252 RID: 4690
		public const string Sch_AttrUseAndWildId = "It is an error if there is a member of the attribute uses of a type definition with type xs:ID or derived from xs:ID and another attribute with type xs:ID matches an attribute wildcard.";

		// Token: 0x04001253 RID: 4691
		public const string Sch_MoreThanOneWildId = "It is an error if more than one attribute whose type is xs:ID or is derived from xs:ID, matches an attribute wildcard on an element.";

		// Token: 0x04001254 RID: 4692
		public const string Sch_BaseFinalExtension = "The base type is the final extension.";

		// Token: 0x04001255 RID: 4693
		public const string Sch_NotSimpleContent = "The content type of the base type must be a simple type definition or it must be mixed, and simpleType child must be present.";

		// Token: 0x04001256 RID: 4694
		public const string Sch_NotComplexContent = "The content type of the base type must not be a simple type definition.";

		// Token: 0x04001257 RID: 4695
		public const string Sch_BaseFinalRestriction = "The base type is final restriction.";

		// Token: 0x04001258 RID: 4696
		public const string Sch_BaseFinalList = "The base type is the final list.";

		// Token: 0x04001259 RID: 4697
		public const string Sch_BaseFinalUnion = "The base type is the final union.";

		// Token: 0x0400125A RID: 4698
		public const string Sch_UndefBaseRestriction = "Undefined complexType '{0}' is used as a base for complex type restriction.";

		// Token: 0x0400125B RID: 4699
		public const string Sch_UndefBaseExtension = "Undefined complexType '{0}' is used as a base for complex type extension.";

		// Token: 0x0400125C RID: 4700
		public const string Sch_DifContentType = "The derived type and the base type must have the same content type.";

		// Token: 0x0400125D RID: 4701
		public const string Sch_InvalidContentRestriction = "Invalid content type derivation by restriction.";

		// Token: 0x0400125E RID: 4702
		public const string Sch_InvalidContentRestrictionDetailed = "Invalid content type derivation by restriction. {0}";

		// Token: 0x0400125F RID: 4703
		public const string Sch_InvalidBaseToEmpty = "If the derived content type is Empty, then the base content type should also be Empty or Mixed with Emptiable particle according to rule 5.3 of Schema Component Constraint: Derivation Valid (Restriction, Complex).";

		// Token: 0x04001260 RID: 4704
		public const string Sch_InvalidBaseToMixed = "If the derived content type is Mixed, then the base content type should also be Mixed according to rule 5.4 of Schema Component Constraint: Derivation Valid (Restriction, Complex).";

		// Token: 0x04001261 RID: 4705
		public const string Sch_DupAttributeUse = "The attribute '{0}' already exists.";

		// Token: 0x04001262 RID: 4706
		public const string Sch_InvalidParticleRestriction = "Invalid particle derivation by restriction.";

		// Token: 0x04001263 RID: 4707
		public const string Sch_InvalidParticleRestrictionDetailed = "Invalid particle derivation by restriction - '{0}'.";

		// Token: 0x04001264 RID: 4708
		public const string Sch_ForbiddenDerivedParticleForAll = "'Choice' or 'any' is forbidden as derived particle when the base particle is 'all'.";

		// Token: 0x04001265 RID: 4709
		public const string Sch_ForbiddenDerivedParticleForElem = "Only 'element' is valid as derived particle when the base particle is 'element'.";

		// Token: 0x04001266 RID: 4710
		public const string Sch_ForbiddenDerivedParticleForChoice = "'All' or 'any' is forbidden as derived particle when the base particle is 'choice'.";

		// Token: 0x04001267 RID: 4711
		public const string Sch_ForbiddenDerivedParticleForSeq = "'All', 'any', and 'choice' are forbidden as derived particles when the base particle is 'sequence'.";

		// Token: 0x04001268 RID: 4712
		public const string Sch_ElementFromElement = "Derived element '{0}' is not a valid restriction of base element '{1}' according to Elt:Elt -- NameAndTypeOK.";

		// Token: 0x04001269 RID: 4713
		public const string Sch_ElementFromAnyRule1 = "The namespace of element '{0}'is not valid with respect to the wildcard's namespace constraint in the base, Elt:Any -- NSCompat Rule 1.";

		// Token: 0x0400126A RID: 4714
		public const string Sch_ElementFromAnyRule2 = "The occurrence range of element '{0}'is not a valid restriction of the wildcard's occurrence range in the base, Elt:Any -- NSCompat Rule2.";

		// Token: 0x0400126B RID: 4715
		public const string Sch_AnyFromAnyRule1 = "The derived wildcard's occurrence range is not a valid restriction of the base wildcard's occurrence range, Any:Any -- NSSubset Rule 1.";

		// Token: 0x0400126C RID: 4716
		public const string Sch_AnyFromAnyRule2 = "The derived wildcard's namespace constraint must be an intensional subset of the base wildcard's namespace constraint, Any:Any -- NSSubset Rule2.";

		// Token: 0x0400126D RID: 4717
		public const string Sch_AnyFromAnyRule3 = "The derived wildcard's 'processContents' must be identical to or stronger than the base wildcard's 'processContents', where 'strict' is stronger than 'lax' and 'lax' is stronger than 'skip', Any:Any -- NSSubset Rule 3.";

		// Token: 0x0400126E RID: 4718
		public const string Sch_GroupBaseFromAny1 = "Every member of the derived group particle must be a valid restriction of the base wildcard, NSRecurseCheckCardinality Rule 1.";

		// Token: 0x0400126F RID: 4719
		public const string Sch_GroupBaseFromAny2 = "The derived particle's occurrence range at ({0}, {1}) is not a valid restriction of the base wildcard's occurrence range at ({2}, {3}), NSRecurseCheckCardinality Rule 2.";

		// Token: 0x04001270 RID: 4720
		public const string Sch_ElementFromGroupBase1 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base sequence particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

		// Token: 0x04001271 RID: 4721
		public const string Sch_ElementFromGroupBase2 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base choice particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

		// Token: 0x04001272 RID: 4722
		public const string Sch_ElementFromGroupBase3 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base all particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

		// Token: 0x04001273 RID: 4723
		public const string Sch_GroupBaseRestRangeInvalid = "The derived particle's range is not a valid restriction of the base particle's range according to All:All,Sequence:Sequence -- Recurse Rule 1 or Choice:Choice -- RecurseLax.";

		// Token: 0x04001274 RID: 4724
		public const string Sch_GroupBaseRestNoMap = "The derived particle cannot have more members than the base particle - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.";

		// Token: 0x04001275 RID: 4725
		public const string Sch_GroupBaseRestNotEmptiable = "All particles in the {particles} of the base particle which are not mapped to by any particle in the {particles} of the derived particle should be emptiable - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.";

		// Token: 0x04001276 RID: 4726
		public const string Sch_SeqFromAll = "The derived sequence particle at ({0}, {1}) is not a valid restriction of the base all particle at ({2}, {3}) according to Sequence:All -- RecurseUnordered.";

		// Token: 0x04001277 RID: 4727
		public const string Sch_SeqFromChoice = "The derived sequence particle at ({0}, {1}) is not a valid restriction of the base choice particle at ({2}, {3}) according to Sequence:Choice -- MapAndSum.";

		// Token: 0x04001278 RID: 4728
		public const string Sch_UndefGroupRef = "Reference to undeclared model group '{0}'.";

		// Token: 0x04001279 RID: 4729
		public const string Sch_GroupCircularRef = "Circular group reference.";

		// Token: 0x0400127A RID: 4730
		public const string Sch_AllRefNotRoot = "The group ref to 'all' is not the root particle, or it is being used as an extension.";

		// Token: 0x0400127B RID: 4731
		public const string Sch_AllRefMinMax = "The group ref to 'all' must have {min occurs}= 0 or 1 and {max occurs}=1.";

		// Token: 0x0400127C RID: 4732
		public const string Sch_NotAllAlone = "'all' is not the only particle in a group, or is being used as an extension.";

		// Token: 0x0400127D RID: 4733
		public const string Sch_AttributeGroupCircularRef = "Circular attribute group reference.";

		// Token: 0x0400127E RID: 4734
		public const string Sch_UndefAttributeGroupRef = "Reference to undeclared attribute group '{0}'.";

		// Token: 0x0400127F RID: 4735
		public const string Sch_InvalidAttributeExtension = "Invalid attribute extension.";

		// Token: 0x04001280 RID: 4736
		public const string Sch_InvalidAnyAttributeRestriction = "The base any attribute must be a superset of the derived 'anyAttribute'.";

		// Token: 0x04001281 RID: 4737
		public const string Sch_AttributeRestrictionProhibited = "Invalid attribute restriction. Attribute restriction is prohibited in base type.";

		// Token: 0x04001282 RID: 4738
		public const string Sch_AttributeRestrictionInvalid = "Invalid attribute restriction. Derived attribute's type is not a valid restriction of the base attribute's type.";

		// Token: 0x04001283 RID: 4739
		public const string Sch_AttributeFixedInvalid = "Invalid attribute restriction. Derived attribute's fixed value must be the same as the base attribute's fixed value.";

		// Token: 0x04001284 RID: 4740
		public const string Sch_AttributeUseInvalid = "Derived attribute's use has to be required if base attribute's use is required.";

		// Token: 0x04001285 RID: 4741
		public const string Sch_AttributeRestrictionInvalidFromWildcard = "The {base type definition} must have an {attribute wildcard} and the {target namespace} of the R's {attribute declaration} must be valid with respect to that wildcard.";

		// Token: 0x04001286 RID: 4742
		public const string Sch_NoDerivedAttribute = "The base attribute '{0}' whose use = 'required' does not have a corresponding derived attribute while redefining attribute group '{1}'.";

		// Token: 0x04001287 RID: 4743
		public const string Sch_UnexpressibleAnyAttribute = "The 'anyAttribute' is not expressible.";

		// Token: 0x04001288 RID: 4744
		public const string Sch_RefInvalidAttribute = "Reference to invalid attribute '{0}'.";

		// Token: 0x04001289 RID: 4745
		public const string Sch_ElementCircularRef = "Circular element reference.";

		// Token: 0x0400128A RID: 4746
		public const string Sch_RefInvalidElement = "Reference to invalid element '{0}'.";

		// Token: 0x0400128B RID: 4747
		public const string Sch_ElementCannotHaveValue = "Element's type does not allow fixed or default value constraint.";

		// Token: 0x0400128C RID: 4748
		public const string Sch_ElementInMixedWithFixed = "Although the '{0}' element's content type is mixed, it cannot have element children, because it has a fixed value constraint in the schema.";

		// Token: 0x0400128D RID: 4749
		public const string Sch_ElementTypeCollision = "Elements with the same name and in the same scope must have the same type.";

		// Token: 0x0400128E RID: 4750
		public const string Sch_InvalidIncludeLocation = "Cannot resolve the 'schemaLocation' attribute.";

		// Token: 0x0400128F RID: 4751
		public const string Sch_CannotLoadSchema = "Cannot load the schema for the namespace '{0}' - {1}";

		// Token: 0x04001290 RID: 4752
		public const string Sch_CannotLoadSchemaLocation = "Cannot load the schema from the location '{0}' - {1}";

		// Token: 0x04001291 RID: 4753
		public const string Sch_LengthGtBaseLength = "It is an error if 'length' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'length'.";

		// Token: 0x04001292 RID: 4754
		public const string Sch_MinLengthGtBaseMinLength = "It is an error if 'minLength' is among the members of {facets} of {base type definition} and {value} is less than the {value} of the parent 'minLength'.";

		// Token: 0x04001293 RID: 4755
		public const string Sch_MaxLengthGtBaseMaxLength = "It is an error if 'maxLength' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'maxLength'.";

		// Token: 0x04001294 RID: 4756
		public const string Sch_MaxMinLengthBaseLength = "It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.";

		// Token: 0x04001295 RID: 4757
		public const string Sch_MaxInclusiveMismatch = "It is an error if the derived 'maxInclusive' facet value is greater than the parent 'maxInclusive' facet value.";

		// Token: 0x04001296 RID: 4758
		public const string Sch_MaxExclusiveMismatch = "It is an error if the derived 'maxExclusive' facet value is greater than the parent 'maxExclusive' facet value.";

		// Token: 0x04001297 RID: 4759
		public const string Sch_MinInclusiveMismatch = "It is an error if the derived 'minInclusive' facet value is less than the parent 'minInclusive' facet value.";

		// Token: 0x04001298 RID: 4760
		public const string Sch_MinExclusiveMismatch = "It is an error if the derived 'minExclusive' facet value is less than the parent 'minExclusive' facet value.";

		// Token: 0x04001299 RID: 4761
		public const string Sch_MinExlIncMismatch = "It is an error if the derived 'minExclusive' facet value is less than or equal to the parent 'minInclusive' facet value.";

		// Token: 0x0400129A RID: 4762
		public const string Sch_MinExlMaxExlMismatch = "It is an error if the derived 'minExclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

		// Token: 0x0400129B RID: 4763
		public const string Sch_MinIncMaxExlMismatch = "It is an error if the derived 'minInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

		// Token: 0x0400129C RID: 4764
		public const string Sch_MinIncExlMismatch = "It is an error if the derived 'minInclusive' facet value is less than or equal to the parent 'minExclusive' facet value.";

		// Token: 0x0400129D RID: 4765
		public const string Sch_MaxIncExlMismatch = "It is an error if the derived 'maxInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

		// Token: 0x0400129E RID: 4766
		public const string Sch_MaxExlIncMismatch = "It is an error if the derived 'maxExclusive' facet value is greater than or equal to the parent 'maxInclusive' facet value.";

		// Token: 0x0400129F RID: 4767
		public const string Sch_TotalDigitsMismatch = "It is an error if the derived 'totalDigits' facet value is greater than the parent 'totalDigits' facet value.";

		// Token: 0x040012A0 RID: 4768
		public const string Sch_FacetBaseFixed = "Values that are declared as {fixed} in a base type can not be changed in a derived type.";

		// Token: 0x040012A1 RID: 4769
		public const string Sch_WhiteSpaceRestriction1 = "It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'replace' or 'preserve', and the {value} of the parent 'whiteSpace' is 'collapse'.";

		// Token: 0x040012A2 RID: 4770
		public const string Sch_WhiteSpaceRestriction2 = "It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'preserve', and the {value} of the parent 'whiteSpace' is 'replace'.";

		// Token: 0x040012A3 RID: 4771
		public const string Sch_XsiNilAndFixed = "There must be no fixed value when an attribute is 'xsi:nil' and has a value of 'true'.";

		// Token: 0x040012A4 RID: 4772
		public const string Sch_MixSchemaTypes = "Different schema types cannot be mixed.";

		// Token: 0x040012A5 RID: 4773
		public const string Sch_XSDSchemaOnly = "'XmlSchemaSet' can load only W3C XML Schemas.";

		// Token: 0x040012A6 RID: 4774
		public const string Sch_InvalidPublicAttribute = "Public attribute '{0}' is an invalid URI.";

		// Token: 0x040012A7 RID: 4775
		public const string Sch_InvalidSystemAttribute = "System attribute '{0}' is an invalid URI.";

		// Token: 0x040012A8 RID: 4776
		public const string Sch_TypeAfterConstraints = "'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.";

		// Token: 0x040012A9 RID: 4777
		public const string Sch_XsiNilAndType = "There can be no type value when attribute is 'xsi:nil' and has value 'true'.";

		// Token: 0x040012AA RID: 4778
		public const string Sch_DupSimpleTypeChild = "'simpleType' should have only one child 'union', 'list', or 'restriction'.";

		// Token: 0x040012AB RID: 4779
		public const string Sch_InvalidIdAttribute = "Invalid 'id' attribute value: {0}";

		// Token: 0x040012AC RID: 4780
		public const string Sch_InvalidNameAttributeEx = "Invalid 'name' attribute value '{0}': '{1}'.";

		// Token: 0x040012AD RID: 4781
		public const string Sch_InvalidAttribute = "Invalid '{0}' attribute: '{1}'.";

		// Token: 0x040012AE RID: 4782
		public const string Sch_EmptyChoice = "Empty choice cannot be satisfied if 'minOccurs' is not equal to 0.";

		// Token: 0x040012AF RID: 4783
		public const string Sch_DerivedNotFromBase = "The data type of the simple content is not a valid restriction of the base complex type.";

		// Token: 0x040012B0 RID: 4784
		public const string Sch_NeedSimpleTypeChild = "Simple content restriction must have a simple type child if the content type of the base type is not a simple type definition.";

		// Token: 0x040012B1 RID: 4785
		public const string Sch_InvalidCollection = "The schema items collection cannot contain an object of type 'XmlSchemaInclude', 'XmlSchemaImport', or 'XmlSchemaRedefine'.";

		// Token: 0x040012B2 RID: 4786
		public const string Sch_UnrefNS = "Namespace '{0}' is not available to be referenced in this schema.";

		// Token: 0x040012B3 RID: 4787
		public const string Sch_InvalidSimpleTypeRestriction = "Restriction of 'anySimpleType' is not allowed.";

		// Token: 0x040012B4 RID: 4788
		public const string Sch_MultipleRedefine = "Multiple redefines of the same schema will be ignored.";

		// Token: 0x040012B5 RID: 4789
		public const string Sch_NullValue = "Value cannot be null.";

		// Token: 0x040012B6 RID: 4790
		public const string Sch_ComplexContentModel = "Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.";

		// Token: 0x040012B7 RID: 4791
		public const string Sch_SchemaNotPreprocessed = "All schemas in the set should be successfully preprocessed prior to compilation.";

		// Token: 0x040012B8 RID: 4792
		public const string Sch_SchemaNotRemoved = "The schema could not be removed because other schemas in the set have dependencies on this schema or its imports.";

		// Token: 0x040012B9 RID: 4793
		public const string Sch_ComponentAlreadySeenForNS = "An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.";

		// Token: 0x040012BA RID: 4794
		public const string Sch_DefaultAttributeNotApplied = "Default attribute '{0}' for element '{1}' could not be applied as the attribute namespace is not mapped to a prefix in the instance document.";

		// Token: 0x040012BB RID: 4795
		public const string Sch_NotXsiAttribute = "The attribute '{0}' does not match one of the four allowed attributes in the 'xsi' namespace.";

		// Token: 0x040012BC RID: 4796
		public const string Sch_SchemaDoesNotExist = "Schema does not exist in the set.";

		// Token: 0x040012BD RID: 4797
		public const string XmlDocument_ValidateInvalidNodeType = "Validate method can be called only on nodes of type Document, DocumentFragment, Element, or Attribute.";

		// Token: 0x040012BE RID: 4798
		public const string XmlDocument_NodeNotFromDocument = "Cannot validate '{0}' because its owner document is not the current document.";

		// Token: 0x040012BF RID: 4799
		public const string XmlDocument_NoNodeSchemaInfo = "Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.";

		// Token: 0x040012C0 RID: 4800
		public const string XmlDocument_NoSchemaInfo = "The XmlSchemaSet on the document is either null or has no schemas in it. Provide schema information before calling Validate.";

		// Token: 0x040012C1 RID: 4801
		public const string Sch_InvalidStartTransition = "It is invalid to call the '{0}' method in the current state of the validator. The '{1}' method must be called before proceeding with validation.";

		// Token: 0x040012C2 RID: 4802
		public const string Sch_InvalidStateTransition = "The transition from the '{0}' method to the '{1}' method is not allowed.";

		// Token: 0x040012C3 RID: 4803
		public const string Sch_InvalidEndValidation = "The 'EndValidation' method cannot not be called when all the elements have not been validated. 'ValidateEndElement' calls corresponding to 'ValidateElement' calls might be missing.";

		// Token: 0x040012C4 RID: 4804
		public const string Sch_InvalidEndElementCall = "It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' after 'ValidateText' or 'ValidateWhitespace' methods have been called.";

		// Token: 0x040012C5 RID: 4805
		public const string Sch_InvalidEndElementCallTyped = "It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' for elements with complex content.";

		// Token: 0x040012C6 RID: 4806
		public const string Sch_InvalidEndElementMultiple = "The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.";

		// Token: 0x040012C7 RID: 4807
		public const string Sch_DuplicateAttribute = "The '{0}' attribute has already been validated and is a duplicate attribute.";

		// Token: 0x040012C8 RID: 4808
		public const string Sch_InvalidPartialValidationType = "The partial validation type has to be 'XmlSchemaElement', 'XmlSchemaAttribute', or 'XmlSchemaType'.";

		// Token: 0x040012C9 RID: 4809
		public const string Sch_SchemaElementNameMismatch = "The element name '{0}' does not match the name '{1}' of the 'XmlSchemaElement' set as a partial validation type.";

		// Token: 0x040012CA RID: 4810
		public const string Sch_SchemaAttributeNameMismatch = "The attribute name '{0}' does not match the name '{1}' of the 'XmlSchemaAttribute' set as a partial validation type.";

		// Token: 0x040012CB RID: 4811
		public const string Sch_ValidateAttributeInvalidCall = "If the partial validation type is 'XmlSchemaElement' or 'XmlSchemaType', the 'ValidateAttribute' method cannot be called.";

		// Token: 0x040012CC RID: 4812
		public const string Sch_ValidateElementInvalidCall = "If the partial validation type is 'XmlSchemaAttribute', the 'ValidateElement' method cannot be called.";

		// Token: 0x040012CD RID: 4813
		public const string Sch_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

		// Token: 0x040012CE RID: 4814
		public const string Sch_EnumFinished = "Enumeration has already finished.";

		// Token: 0x040012CF RID: 4815
		public const string SchInf_schema = "The supplied xml instance is a schema or contains an inline schema. This class cannot infer a schema for a schema.";

		// Token: 0x040012D0 RID: 4816
		public const string SchInf_entity = "Inference cannot handle entity references. Pass in an 'XmlReader' that expands entities.";

		// Token: 0x040012D1 RID: 4817
		public const string SchInf_simplecontent = "Expected simple content. Schema was not created using this tool.";

		// Token: 0x040012D2 RID: 4818
		public const string SchInf_extension = "Expected 'Extension' within 'SimpleContent'. Schema was not created using this tool.";

		// Token: 0x040012D3 RID: 4819
		public const string SchInf_particle = "Particle cannot exist along with 'ContentModel'.";

		// Token: 0x040012D4 RID: 4820
		public const string SchInf_ct = "Complex type expected to exist with at least one 'Element' at this point.";

		// Token: 0x040012D5 RID: 4821
		public const string SchInf_seq = "sequence expected to contain elements only. Schema was not created using this tool.";

		// Token: 0x040012D6 RID: 4822
		public const string SchInf_noseq = "The supplied schema contains particles other than Sequence and Choice. Only schemas generated by this tool are supported.";

		// Token: 0x040012D7 RID: 4823
		public const string SchInf_noct = "Expected ComplexType. Schema was not generated using this tool.";

		// Token: 0x040012D8 RID: 4824
		public const string SchInf_UnknownParticle = "Expected Element. Schema was not generated using this tool.";

		// Token: 0x040012D9 RID: 4825
		public const string SchInf_schematype = "Inference can only handle simple built-in types for 'SchemaType'.";

		// Token: 0x040012DA RID: 4826
		public const string SchInf_NoElement = "There is no element to infer schema.";

		// Token: 0x040012DB RID: 4827
		public const string Xp_UnclosedString = "This is an unclosed string.";

		// Token: 0x040012DC RID: 4828
		public const string Xp_ExprExpected = "'{0}' is an invalid expression.";

		// Token: 0x040012DD RID: 4829
		public const string Xp_InvalidArgumentType = "The argument to function '{0}' in '{1}' cannot be converted to a node-set.";

		// Token: 0x040012DE RID: 4830
		public const string Xp_InvalidNumArgs = "Function '{0}' in '{1}' has an invalid number of arguments.";

		// Token: 0x040012DF RID: 4831
		public const string Xp_InvalidName = "'{0}' has an invalid qualified name.";

		// Token: 0x040012E0 RID: 4832
		public const string Xp_InvalidToken = "'{0}' has an invalid token.";

		// Token: 0x040012E1 RID: 4833
		public const string Xp_NodeSetExpected = "Expression must evaluate to a node-set.";

		// Token: 0x040012E2 RID: 4834
		public const string Xp_NotSupported = "The XPath query '{0}' is not supported.";

		// Token: 0x040012E3 RID: 4835
		public const string Xp_InvalidPattern = "'{0}' is an invalid XSLT pattern.";

		// Token: 0x040012E4 RID: 4836
		public const string Xp_InvalidKeyPattern = "'{0}' is an invalid key pattern. It either contains a variable reference or 'key()' function.";

		// Token: 0x040012E5 RID: 4837
		public const string Xp_BadQueryObject = "This is an invalid object. Only objects returned from Compile() can be passed as input.";

		// Token: 0x040012E6 RID: 4838
		public const string Xp_UndefinedXsltContext = "XsltContext is needed for this query because of an unknown function.";

		// Token: 0x040012E7 RID: 4839
		public const string Xp_NoContext = "Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.";

		// Token: 0x040012E8 RID: 4840
		public const string Xp_UndefVar = "The variable '{0}' is undefined.";

		// Token: 0x040012E9 RID: 4841
		public const string Xp_UndefFunc = "The function '{0}()' is undefined.";

		// Token: 0x040012EA RID: 4842
		public const string Xp_FunctionFailed = "Function '{0}()' has failed.";

		// Token: 0x040012EB RID: 4843
		public const string Xp_CurrentNotAllowed = "The 'current()' function cannot be used in a pattern.";

		// Token: 0x040012EC RID: 4844
		public const string Xp_QueryTooComplex = "The xpath query is too complex.";

		// Token: 0x040012ED RID: 4845
		public const string Xdom_DualDocumentTypeNode = "This document already has a 'DocumentType' node.";

		// Token: 0x040012EE RID: 4846
		public const string Xdom_DualDocumentElementNode = "This document already has a 'DocumentElement' node.";

		// Token: 0x040012EF RID: 4847
		public const string Xdom_DualDeclarationNode = "This document already has an 'XmlDeclaration' node.";

		// Token: 0x040012F0 RID: 4848
		public const string Xdom_Import = "Cannot import nodes of type '{0}'.";

		// Token: 0x040012F1 RID: 4849
		public const string Xdom_Import_NullNode = "Cannot import a null node.";

		// Token: 0x040012F2 RID: 4850
		public const string Xdom_NoRootEle = "The document does not have a root element.";

		// Token: 0x040012F3 RID: 4851
		public const string Xdom_Attr_Name = "The attribute local name cannot be empty.";

		// Token: 0x040012F4 RID: 4852
		public const string Xdom_AttrCol_Object = "An 'Attributes' collection can only contain 'Attribute' objects.";

		// Token: 0x040012F5 RID: 4853
		public const string Xdom_AttrCol_Insert = "The reference node must be a child of the current node.";

		// Token: 0x040012F6 RID: 4854
		public const string Xdom_NamedNode_Context = "The named node is from a different document context.";

		// Token: 0x040012F7 RID: 4855
		public const string Xdom_Version = "Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\".";

		// Token: 0x040012F8 RID: 4856
		public const string Xdom_standalone = "Wrong value for the XML declaration standalone attribute of '{0}'.";

		// Token: 0x040012F9 RID: 4857
		public const string Xdom_Ele_Prefix = "The prefix of an element name cannot start with 'xml'.";

		// Token: 0x040012FA RID: 4858
		public const string Xdom_Ent_Innertext = "The 'InnerText' of an 'Entity' node is read-only and cannot be set.";

		// Token: 0x040012FB RID: 4859
		public const string Xdom_EntRef_SetVal = "'EntityReference' nodes have no support for setting value.";

		// Token: 0x040012FC RID: 4860
		public const string Xdom_WS_Char = "The string for white space contains an invalid character.";

		// Token: 0x040012FD RID: 4861
		public const string Xdom_Node_SetVal = "Cannot set a value on node type '{0}'.";

		// Token: 0x040012FE RID: 4862
		public const string Xdom_Empty_LocalName = "The local name for elements or attributes cannot be null or an empty string.";

		// Token: 0x040012FF RID: 4863
		public const string Xdom_Set_InnerXml = "Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children.";

		// Token: 0x04001300 RID: 4864
		public const string Xdom_Attr_InUse = "The 'Attribute' node cannot be inserted because it is already an attribute of another element.";

		// Token: 0x04001301 RID: 4865
		public const string Xdom_Enum_ElementList = "The element list has changed. The enumeration operation failed to continue.";

		// Token: 0x04001302 RID: 4866
		public const string Xdom_Invalid_NT_String = "'{0}' does not represent any 'XmlNodeType'.";

		// Token: 0x04001303 RID: 4867
		public const string Xdom_InvalidCharacter_EntityReference = "Cannot create an 'EntityReference' node with a name starting with '#'.";

		// Token: 0x04001304 RID: 4868
		public const string Xdom_IndexOutOfRange = "The index being passed in is out of range.";

		// Token: 0x04001305 RID: 4869
		public const string Xdom_Document_Innertext = "The 'InnerText' of a 'Document' node is read-only and cannot be set.";

		// Token: 0x04001306 RID: 4870
		public const string Xpn_BadPosition = "Operation is not valid due to the current position of the navigator.";

		// Token: 0x04001307 RID: 4871
		public const string Xpn_MissingParent = "The current position of the navigator is missing a valid parent.";

		// Token: 0x04001308 RID: 4872
		public const string Xpn_NoContent = "No content generated as the result of the operation.";

		// Token: 0x04001309 RID: 4873
		public const string Xdom_Load_NoDocument = "The document to be loaded could not be found.";

		// Token: 0x0400130A RID: 4874
		public const string Xdom_Load_NoReader = "There is no reader from which to load the document.";

		// Token: 0x0400130B RID: 4875
		public const string Xdom_Node_Null_Doc = "Cannot create a node without an owner document.";

		// Token: 0x0400130C RID: 4876
		public const string Xdom_Node_Insert_Child = "Cannot insert a node or any ancestor of that node as a child of itself.";

		// Token: 0x0400130D RID: 4877
		public const string Xdom_Node_Insert_Contain = "The current node cannot contain other nodes.";

		// Token: 0x0400130E RID: 4878
		public const string Xdom_Node_Insert_Path = "The reference node is not a child of this node.";

		// Token: 0x0400130F RID: 4879
		public const string Xdom_Node_Insert_Context = "The node to be inserted is from a different document context.";

		// Token: 0x04001310 RID: 4880
		public const string Xdom_Node_Insert_Location = "Cannot insert the node in the specified location.";

		// Token: 0x04001311 RID: 4881
		public const string Xdom_Node_Insert_TypeConflict = "The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type.";

		// Token: 0x04001312 RID: 4882
		public const string Xdom_Node_Remove_Contain = "The current node cannot contain other nodes, so the node to be removed is not its child.";

		// Token: 0x04001313 RID: 4883
		public const string Xdom_Node_Remove_Child = "The node to be removed is not a child of this node.";

		// Token: 0x04001314 RID: 4884
		public const string Xdom_Node_Modify_ReadOnly = "This node is read-only. It cannot be modified.";

		// Token: 0x04001315 RID: 4885
		public const string Xdom_TextNode_SplitText = "The 'Text' node is not connected in the DOM live tree. No 'SplitText' operation could be performed.";

		// Token: 0x04001316 RID: 4886
		public const string Xdom_Attr_Reserved_XmlNS = "The namespace declaration attribute has an incorrect 'namespaceURI': '{0}'.";

		// Token: 0x04001317 RID: 4887
		public const string Xdom_Node_Cloning = "'Entity' and 'Notation' nodes cannot be cloned.";

		// Token: 0x04001318 RID: 4888
		public const string Xnr_ResolveEntity = "The node is not an expandable 'EntityReference' node.";

		// Token: 0x04001319 RID: 4889
		public const string XPathDocument_MissingSchemas = "An XmlSchemaSet must be provided to validate the document.";

		// Token: 0x0400131A RID: 4890
		public const string XPathDocument_NotEnoughSchemaInfo = "Element should have prior schema information to call this method.";

		// Token: 0x0400131B RID: 4891
		public const string XPathDocument_ValidateInvalidNodeType = "Validate and CheckValidity are only allowed on Root or Element nodes.";

		// Token: 0x0400131C RID: 4892
		public const string XPathDocument_SchemaSetNotAllowed = "An XmlSchemaSet is only allowed as a parameter on the Root node.";

		// Token: 0x0400131D RID: 4893
		public const string XmlBin_MissingEndCDATA = "CDATA end token is missing.";

		// Token: 0x0400131E RID: 4894
		public const string XmlBin_InvalidQNameID = "Invalid QName ID.";

		// Token: 0x0400131F RID: 4895
		public const string XmlBinary_UnexpectedToken = "Unexpected BinaryXml token.";

		// Token: 0x04001320 RID: 4896
		public const string XmlBinary_InvalidSqlDecimal = "Unable to parse data as SQL_DECIMAL.";

		// Token: 0x04001321 RID: 4897
		public const string XmlBinary_InvalidSignature = "Invalid BinaryXml signature.";

		// Token: 0x04001322 RID: 4898
		public const string XmlBinary_InvalidProtocolVersion = "Invalid BinaryXml protocol version.";

		// Token: 0x04001323 RID: 4899
		public const string XmlBinary_UnsupportedCodePage = "Unsupported BinaryXml codepage.";

		// Token: 0x04001324 RID: 4900
		public const string XmlBinary_InvalidStandalone = "Invalid BinaryXml standalone token.";

		// Token: 0x04001325 RID: 4901
		public const string XmlBinary_NoParserContext = "BinaryXml Parser does not support initialization with XmlParserContext.";

		// Token: 0x04001326 RID: 4902
		public const string XmlBinary_ListsOfValuesNotSupported = "Lists of BinaryXml value tokens not supported.";

		// Token: 0x04001327 RID: 4903
		public const string XmlBinary_CastNotSupported = "Token '{0}' does not support a conversion to Clr type '{1}'.";

		// Token: 0x04001328 RID: 4904
		public const string XmlBinary_NoRemapPrefix = "Prefix '{0}' is already assigned to namespace '{1}' and cannot be reassigned to '{2}' on this tag.";

		// Token: 0x04001329 RID: 4905
		public const string XmlBinary_AttrWithNsNoPrefix = "Attribute '{0}' has namespace '{1}' but no prefix.";

		// Token: 0x0400132A RID: 4906
		public const string XmlBinary_ValueTooBig = "The value is too big to fit into an Int32. The arithmetic operation resulted in an overflow.";

		// Token: 0x0400132B RID: 4907
		public const string SqlTypes_ArithOverflow = "Arithmetic Overflow.";

		// Token: 0x0400132C RID: 4908
		public const string SqlTypes_ArithTruncation = "Numeric arithmetic causes truncation.";

		// Token: 0x0400132D RID: 4909
		public const string SqlTypes_DivideByZero = "Divide by zero error encountered.";

		// Token: 0x0400132E RID: 4910
		public const string XmlMissingType = "Invalid serialization assembly: Required type {0} cannot be found in the generated assembly '{1}'.";

		// Token: 0x0400132F RID: 4911
		public const string XmlUnsupportedType = "{0} is an unsupported type.";

		// Token: 0x04001330 RID: 4912
		public const string XmlSerializerUnsupportedType = "{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.";

		// Token: 0x04001331 RID: 4913
		public const string XmlSerializerUnsupportedMember = "Cannot serialize member '{0}' of type '{1}', see inner exception for more details.";

		// Token: 0x04001332 RID: 4914
		public const string XmlUnsupportedTypeKind = "The type {0} may not be serialized.";

		// Token: 0x04001333 RID: 4915
		public const string XmlUnsupportedSoapTypeKind = "The type {0} may not be serialized with SOAP-encoded messages. Set the Use for your message to Literal.";

		// Token: 0x04001334 RID: 4916
		public const string XmlUnsupportedIDictionary = "The type {0} is not supported because it implements IDictionary.";

		// Token: 0x04001335 RID: 4917
		public const string XmlUnsupportedIDictionaryDetails = "Cannot serialize member {0} of type {1}, because it implements IDictionary.";

		// Token: 0x04001336 RID: 4918
		public const string XmlDuplicateTypeName = "A type with the name {0} has already been added in namespace {1}.";

		// Token: 0x04001337 RID: 4919
		public const string XmlSerializableNameMissing1 = "Schema Id is missing. The schema returned from {0}.GetSchema() must have an Id.";

		// Token: 0x04001338 RID: 4920
		public const string XmlConstructorInaccessible = "{0} cannot be serialized because it does not have a parameterless constructor.";

		// Token: 0x04001339 RID: 4921
		public const string XmlTypeInaccessible = "{0} is inaccessible due to its protection level. Only public types can be processed.";

		// Token: 0x0400133A RID: 4922
		public const string XmlTypeStatic = "{0} cannot be serialized. Static types cannot be used as parameters or return types.";

		// Token: 0x0400133B RID: 4923
		public const string XmlNoDefaultAccessors = "You must implement a default accessor on {0} because it inherits from ICollection.";

		// Token: 0x0400133C RID: 4924
		public const string XmlNoAddMethod = "To be XML serializable, types which inherit from {2} must have an implementation of Add({1}) at all levels of their inheritance hierarchy. {0} does not implement Add({1}).";

		// Token: 0x0400133D RID: 4925
		public const string XmlReadOnlyPropertyError = "Cannot deserialize type '{0}' because it contains property '{1}' which has no public setter.";

		// Token: 0x0400133E RID: 4926
		public const string XmlAttributeSetAgain = "'{0}.{1}' already has attributes.";

		// Token: 0x0400133F RID: 4927
		public const string XmlIllegalWildcard = "Cannot use wildcards at the top level of a schema.";

		// Token: 0x04001340 RID: 4928
		public const string XmlIllegalArrayElement = "An element declared at the top level of a schema cannot have maxOccurs > 1. Provide a wrapper element for '{0}' by using XmlArray or XmlArrayItem instead of XmlElementAttribute, or by using the Wrapped parameter style.";

		// Token: 0x04001341 RID: 4929
		public const string XmlIllegalForm = "There was an error exporting '{0}': elements declared at the top level of a schema cannot be unqualified.";

		// Token: 0x04001342 RID: 4930
		public const string XmlBareTextMember = "There was an error exporting '{0}': bare members cannot contain text content.";

		// Token: 0x04001343 RID: 4931
		public const string XmlBareAttributeMember = "There was an error exporting '{0}': bare members cannot be attributes.";

		// Token: 0x04001344 RID: 4932
		public const string XmlReflectionError = "There was an error reflecting '{0}'.";

		// Token: 0x04001345 RID: 4933
		public const string XmlTypeReflectionError = "There was an error reflecting type '{0}'.";

		// Token: 0x04001346 RID: 4934
		public const string XmlPropertyReflectionError = "There was an error reflecting property '{0}'.";

		// Token: 0x04001347 RID: 4935
		public const string XmlFieldReflectionError = "There was an error reflecting field '{0}'.";

		// Token: 0x04001348 RID: 4936
		public const string XmlInvalidDataTypeUsage = "'{0}' is an invalid value for the {1} property. The property may only be specified for primitive types.";

		// Token: 0x04001349 RID: 4937
		public const string XmlInvalidXsdDataType = "Value '{0}' cannot be used for the {1} property. The datatype '{2}' is missing.";

		// Token: 0x0400134A RID: 4938
		public const string XmlDataTypeMismatch = "'{0}' is an invalid value for the {1} property. {0} cannot be converted to {2}.";

		// Token: 0x0400134B RID: 4939
		public const string XmlIllegalTypeContext = "{0} cannot be used as: 'xml {1}'.";

		// Token: 0x0400134C RID: 4940
		public const string XmlUdeclaredXsdType = "The type, {0}, is undeclared.";

		// Token: 0x0400134D RID: 4941
		public const string XmlAnyElementNamespace = "The element {0} has been attributed with an XmlAnyElementAttribute and a namespace {1}, but no name. When a namespace is supplied, a name is also required. Supply a name or remove the namespace.";

		// Token: 0x0400134E RID: 4942
		public const string XmlInvalidConstantAttribute = "Only XmlEnum may be used on enumerated constants.";

		// Token: 0x0400134F RID: 4943
		public const string XmlIllegalDefault = "The default value for XmlAttribute or XmlElement may only be specified for primitive types.";

		// Token: 0x04001350 RID: 4944
		public const string XmlIllegalAttributesArrayAttribute = "XmlAttribute and XmlAnyAttribute cannot be used in conjunction with XmlElement, XmlText, XmlAnyElement, XmlArray, or XmlArrayItem.";

		// Token: 0x04001351 RID: 4945
		public const string XmlIllegalElementsArrayAttribute = "XmlElement, XmlText, and XmlAnyElement cannot be used in conjunction with XmlAttribute, XmlAnyAttribute, XmlArray, or XmlArrayItem.";

		// Token: 0x04001352 RID: 4946
		public const string XmlIllegalArrayArrayAttribute = "XmlArray and XmlArrayItem cannot be used in conjunction with XmlAttribute, XmlAnyAttribute, XmlElement, XmlText, or XmlAnyElement.";

		// Token: 0x04001353 RID: 4947
		public const string XmlIllegalAttribute = "For non-array types, you may use the following attributes: XmlAttribute, XmlText, XmlElement, or XmlAnyElement.";

		// Token: 0x04001354 RID: 4948
		public const string XmlIllegalType = "The type for {0} may not be specified for primitive types.";

		// Token: 0x04001355 RID: 4949
		public const string XmlIllegalAttrOrText = "Cannot serialize member '{0}' of type {1}. XmlAttribute/XmlText cannot be used to encode complex types.";

		// Token: 0x04001356 RID: 4950
		public const string XmlIllegalSoapAttribute = "Cannot serialize member '{0}' of type {1}. SoapAttribute cannot be used to encode complex types.";

		// Token: 0x04001357 RID: 4951
		public const string XmlIllegalAttrOrTextInterface = "Cannot serialize member '{0}' of type {1}. XmlAttribute/XmlText cannot be used to encode types implementing {2}.";

		// Token: 0x04001358 RID: 4952
		public const string XmlIllegalAttributeFlagsArray = "XmlAttribute cannot be used to encode array of {1}, because it is marked with FlagsAttribute.";

		// Token: 0x04001359 RID: 4953
		public const string XmlIllegalAnyElement = "Cannot serialize member of type {0}: XmlAnyElement can only be used with classes of type XmlNode or a type deriving from XmlNode.";

		// Token: 0x0400135A RID: 4954
		public const string XmlInvalidIsNullable = "IsNullable may not be 'true' for value type {0}.  Please consider using Nullable<{0}> instead.";

		// Token: 0x0400135B RID: 4955
		public const string XmlInvalidNotNullable = "IsNullable may not be set to 'false' for a Nullable<{0}> type. Consider using '{0}' type or removing the IsNullable property from the {1} attribute.";

		// Token: 0x0400135C RID: 4956
		public const string XmlInvalidFormUnqualified = "The Form property may not be 'Unqualified' when an explicit Namespace property is present.";

		// Token: 0x0400135D RID: 4957
		public const string XmlDuplicateNamespace = "The namespace, {0}, is a duplicate.";

		// Token: 0x0400135E RID: 4958
		public const string XmlElementHasNoName = "This element has no name. Please review schema type '{0}' from namespace '{1}'.";

		// Token: 0x0400135F RID: 4959
		public const string XmlAttributeHasNoName = "This attribute has no name.";

		// Token: 0x04001360 RID: 4960
		public const string XmlElementImportedTwice = "The element, {0}, from namespace, {1}, was imported in two different contexts: ({2}, {3}).";

		// Token: 0x04001361 RID: 4961
		public const string XmlHiddenMember = "Member {0}.{1} of type {2} hides base class member {3}.{4} of type {5}. Use XmlElementAttribute or XmlAttributeAttribute to specify a new name.";

		// Token: 0x04001362 RID: 4962
		public const string XmlInvalidXmlOverride = "Member '{0}.{1}' hides inherited member '{2}.{3}', but has different custom attributes.";

		// Token: 0x04001363 RID: 4963
		public const string XmlMembersDeriveError = "These members may not be derived.";

		// Token: 0x04001364 RID: 4964
		public const string XmlTypeUsedTwice = "The type '{0}' from namespace '{1}' was used in two different ways.";

		// Token: 0x04001365 RID: 4965
		public const string XmlMissingGroup = "Group {0} is missing.";

		// Token: 0x04001366 RID: 4966
		public const string XmlMissingAttributeGroup = "The attribute group {0} is missing.";

		// Token: 0x04001367 RID: 4967
		public const string XmlMissingDataType = "The datatype '{0}' is missing.";

		// Token: 0x04001368 RID: 4968
		public const string XmlInvalidEncoding = "Referenced type '{0}' is only valid for encoded SOAP.";

		// Token: 0x04001369 RID: 4969
		public const string XmlMissingElement = "The element '{0}' is missing.";

		// Token: 0x0400136A RID: 4970
		public const string XmlMissingAttribute = "The attribute {0} is missing.";

		// Token: 0x0400136B RID: 4971
		public const string XmlMissingMethodEnum = "The method for enum {0} is missing.";

		// Token: 0x0400136C RID: 4972
		public const string XmlNoAttributeHere = "Cannot write a node of type XmlAttribute as an element value. Use XmlAnyAttributeAttribute with an array of XmlNode or XmlAttribute to write the node as an attribute.";

		// Token: 0x0400136D RID: 4973
		public const string XmlNeedAttributeHere = "The node must be either type XmlAttribute or a derived type.";

		// Token: 0x0400136E RID: 4974
		public const string XmlElementNameMismatch = "This element was named '{0}' from namespace '{1}' but should have been named '{2}' from namespace '{3}'.";

		// Token: 0x0400136F RID: 4975
		public const string XmlUnsupportedDefaultType = "The default value type, {0}, is unsupported.";

		// Token: 0x04001370 RID: 4976
		public const string XmlUnsupportedDefaultValue = "The formatter {0} cannot be used for default values.";

		// Token: 0x04001371 RID: 4977
		public const string XmlInvalidDefaultValue = "Value '{0}' cannot be converted to {1}.";

		// Token: 0x04001372 RID: 4978
		public const string XmlInvalidDefaultEnumValue = "Enum {0} cannot be converted to {1}.";

		// Token: 0x04001373 RID: 4979
		public const string XmlUnknownNode = "{0} was not expected.";

		// Token: 0x04001374 RID: 4980
		public const string XmlUnknownConstant = "Instance validation error: '{0}' is not a valid value for {1}.";

		// Token: 0x04001375 RID: 4981
		public const string XmlSerializeError = "There is an error in the XML document.";

		// Token: 0x04001376 RID: 4982
		public const string XmlSerializeErrorDetails = "There is an error in XML document ({0}, {1}).";

		// Token: 0x04001377 RID: 4983
		public const string XmlCompilerError = "Unable to generate a temporary class (result={0}).";

		// Token: 0x04001378 RID: 4984
		public const string XmlSchemaDuplicateNamespace = "There are more then one schema with targetNamespace='{0}'.";

		// Token: 0x04001379 RID: 4985
		public const string XmlSchemaCompiled = "Cannot add schema to compiled schemas collection.";

		// Token: 0x0400137A RID: 4986
		public const string XmlInvalidSchemaExtension = "'{0}' is not a valid SchemaExtensionType.";

		// Token: 0x0400137B RID: 4987
		public const string XmlInvalidArrayDimentions = "SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.";

		// Token: 0x0400137C RID: 4988
		public const string XmlInvalidArrayTypeName = "The SOAP-ENC:arrayType references type is named '{0}'; a type named '{1}' was expected at {2}.";

		// Token: 0x0400137D RID: 4989
		public const string XmlInvalidArrayTypeNamespace = "The SOAP-ENC:arrayType references type is from namespace '{0}'; the namespace '{1}' was expected at {2}.";

		// Token: 0x0400137E RID: 4990
		public const string XmlMissingArrayType = "SOAP-ENC:arrayType was missing at {0}.";

		// Token: 0x0400137F RID: 4991
		public const string XmlEmptyArrayType = "SOAP-ENC:arrayType was empty at {0}.";

		// Token: 0x04001380 RID: 4992
		public const string XmlInvalidArraySyntax = "SOAP-ENC:arrayType must end with a ']' character.";

		// Token: 0x04001381 RID: 4993
		public const string XmlInvalidArrayTypeSyntax = "Invalid wsd:arrayType syntax: '{0}'.";

		// Token: 0x04001382 RID: 4994
		public const string XmlMismatchedArrayBrackets = "SOAP-ENC:arrayType has mismatched brackets.";

		// Token: 0x04001383 RID: 4995
		public const string XmlInvalidArrayLength = "SOAP-ENC:arrayType could not handle '{1}' as the length of the array.";

		// Token: 0x04001384 RID: 4996
		public const string XmlMissingHref = "The referenced element with ID '{0}' is located outside the current document and cannot be retrieved.";

		// Token: 0x04001385 RID: 4997
		public const string XmlInvalidHref = "The referenced element with ID '{0}' was not found in the document.";

		// Token: 0x04001386 RID: 4998
		public const string XmlUnknownType = "The specified type was not recognized: name='{0}', namespace='{1}', at {2}.";

		// Token: 0x04001387 RID: 4999
		public const string XmlAbstractType = "The specified type is abstract: name='{0}', namespace='{1}', at {2}.";

		// Token: 0x04001388 RID: 5000
		public const string XmlMappingsScopeMismatch = "Exported mappings must come from the same importer.";

		// Token: 0x04001389 RID: 5001
		public const string XmlMethodTypeNameConflict = "The XML element '{0}' from namespace '{1}' references a method and a type. Change the method's message name using WebMethodAttribute or change the type's root element using the XmlRootAttribute.";

		// Token: 0x0400138A RID: 5002
		public const string XmlCannotReconcileAccessor = "The top XML element '{0}' from namespace '{1}' references distinct types {2} and {3}. Use XML attributes to specify another XML name or namespace for the element or types.";

		// Token: 0x0400138B RID: 5003
		public const string XmlCannotReconcileAttributeAccessor = "The global XML attribute '{0}' from namespace '{1}' references distinct types {2} and {3}. Use XML attributes to specify another XML name or namespace for the attribute or types.";

		// Token: 0x0400138C RID: 5004
		public const string XmlCannotReconcileAccessorDefault = "The global XML item '{0}' from namespace '{1}' has mismatch default value attributes: '{2}' and '{3}' and cannot be mapped to the same schema item. Use XML attributes to specify another XML name or namespace for one of the items, or make sure that the default values match.";

		// Token: 0x0400138D RID: 5005
		public const string XmlInvalidTypeAttributes = "XmlRoot and XmlType attributes may not be specified for the type {0}.";

		// Token: 0x0400138E RID: 5006
		public const string XmlInvalidAttributeUse = "XML attributes may not be specified for the type {0}.";

		// Token: 0x0400138F RID: 5007
		public const string XmlTypesDuplicate = "Types '{0}' and '{1}' both use the XML type name, '{2}', from namespace '{3}'. Use XML attributes to specify a unique XML name and/or namespace for the type.";

		// Token: 0x04001390 RID: 5008
		public const string XmlInvalidSoapArray = "An array of type {0} may not be used with XmlArrayType.Soap.";

		// Token: 0x04001391 RID: 5009
		public const string XmlCannotIncludeInSchema = "The type {0} may not be exported to a schema because the IncludeInSchema property of the XmlType attribute is 'false'.";

		// Token: 0x04001392 RID: 5010
		public const string XmlSoapCannotIncludeInSchema = "The type {0} may not be exported to a schema because the IncludeInSchema property of the SoapType attribute is 'false'.";

		// Token: 0x04001393 RID: 5011
		public const string XmlInvalidSerializable = "The type {0} may not be used in this context. To use {0} as a parameter, return type, or member of a class or struct, the parameter, return type, or member must be declared as type {0} (it cannot be object). Objects of type {0} may not be used in un-typed collections, such as ArrayLists.";

		// Token: 0x04001394 RID: 5012
		public const string XmlInvalidUseOfType = "The type {0} may not be used in this context.";

		// Token: 0x04001395 RID: 5013
		public const string XmlUnxpectedType = "The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.";

		// Token: 0x04001396 RID: 5014
		public const string XmlUnknownAnyElement = "The XML element '{0}' from namespace '{1}' was not expected. The XML element name and namespace must match those provided via XmlAnyElementAttribute(s).";

		// Token: 0x04001397 RID: 5015
		public const string XmlMultipleAttributeOverrides = "{0}. {1} already has attributes.";

		// Token: 0x04001398 RID: 5016
		public const string XmlInvalidEnumAttribute = "Only SoapEnum may be used on enum constants.";

		// Token: 0x04001399 RID: 5017
		public const string XmlInvalidReturnPosition = "The return value must be the first member.";

		// Token: 0x0400139A RID: 5018
		public const string XmlInvalidElementAttribute = "Only SoapElementAttribute or SoapAttributeAttribute may be used on members.";

		// Token: 0x0400139B RID: 5019
		public const string XmlInvalidVoid = "The type Void is not valid in this context.";

		// Token: 0x0400139C RID: 5020
		public const string XmlInvalidContent = "Invalid content {0}.";

		// Token: 0x0400139D RID: 5021
		public const string XmlInvalidSchemaElementType = "Types must be declared at the top level in the schema. Please review schema type '{0}' from namespace '{1}': element '{2}' is using anonymous type declaration, anonymous types are not supported with encoded SOAP.";

		// Token: 0x0400139E RID: 5022
		public const string XmlInvalidSubstitutionGroupUse = "Substitution group may not be used with encoded SOAP. Please review type declaration '{0}' from namespace '{1}'.";

		// Token: 0x0400139F RID: 5023
		public const string XmlElementMissingType = "Please review type declaration '{0}' from namespace '{1}': element '{2}' does not specify a type.";

		// Token: 0x040013A0 RID: 5024
		public const string XmlInvalidAnyAttributeUse = "Any may not be specified. Attributes are not supported with encoded SOAP. Please review schema type '{0}' from namespace '{1}'.";

		// Token: 0x040013A1 RID: 5025
		public const string XmlSoapInvalidAttributeUse = "Attributes are not supported with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}': use elements (not attributes) for fields/parameters.";

		// Token: 0x040013A2 RID: 5026
		public const string XmlSoapInvalidChoice = "Choice is not supported with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}': use all or sequence (not choice) for fields/parameters.";

		// Token: 0x040013A3 RID: 5027
		public const string XmlSoapUnsupportedGroupRef = "The ref syntax for groups is not supported with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}': replace the group reference with local group declaration.";

		// Token: 0x040013A4 RID: 5028
		public const string XmlSoapUnsupportedGroupRepeat = "Group may not repeat.  Unbounded groups are not supported with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}'.";

		// Token: 0x040013A5 RID: 5029
		public const string XmlSoapUnsupportedGroupNested = "Nested groups may not be used with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}'.";

		// Token: 0x040013A6 RID: 5030
		public const string XmlSoapUnsupportedGroupAny = "Any may not be used with encoded SOAP. Please change definition of schema type '{0}' from namespace '{1}'.";

		// Token: 0x040013A7 RID: 5031
		public const string XmlInvalidEnumContent = "Invalid content '{0}' for enumerated data type {1}.";

		// Token: 0x040013A8 RID: 5032
		public const string XmlInvalidAttributeType = "{0} may not be used on parameters or return values when they are not wrapped.";

		// Token: 0x040013A9 RID: 5033
		public const string XmlInvalidBaseType = "Type {0} cannot derive from {1} because it already has base type {2}.";

		// Token: 0x040013AA RID: 5034
		public const string XmlPrimitiveBaseType = "Type '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.";

		// Token: 0x040013AB RID: 5035
		public const string XmlInvalidIdentifier = "Identifier '{0}' is not CLS-compliant.";

		// Token: 0x040013AC RID: 5036
		public const string XmlGenError = "There was an error generating the XML document.";

		// Token: 0x040013AD RID: 5037
		public const string XmlInvalidXmlns = "Invalid namespace attribute: xmlns:{0}=\"\".";

		// Token: 0x040013AE RID: 5038
		public const string XmlCircularReference = "A circular reference was detected while serializing an object of type {0}.";

		// Token: 0x040013AF RID: 5039
		public const string XmlCircularReference2 = "A circular type reference was detected in anonymous type '{0}'.  Please change '{0}' to be a named type by setting {1}={2} in the type definition.";

		// Token: 0x040013B0 RID: 5040
		public const string XmlAnonymousBaseType = "Illegal type derivation: Type '{0}' derives from anonymous type '{1}'. Please change '{1}' to be a named type by setting {2}={3} in the type definition.";

		// Token: 0x040013B1 RID: 5041
		public const string XmlMissingSchema = "Missing schema targetNamespace=\"{0}\".";

		// Token: 0x040013B2 RID: 5042
		public const string XmlNoSerializableMembers = "Cannot serialize object of type '{0}'. The object does not have serializable members.";

		// Token: 0x040013B3 RID: 5043
		public const string XmlIllegalOverride = "Error: Type '{0}' could not be imported because it redefines inherited member '{1}' with a different type. '{1}' is declared as type '{3}' on '{0}', but as type '{2}' on base class '{4}'.";

		// Token: 0x040013B4 RID: 5044
		public const string XmlReadOnlyCollection = "Could not deserialize {0}. Parameterless constructor is required for collections and enumerators.";

		// Token: 0x040013B5 RID: 5045
		public const string XmlRpcNestedValueType = "Cannot serialize {0}. Nested structs are not supported with encoded SOAP.";

		// Token: 0x040013B6 RID: 5046
		public const string XmlRpcRefsInValueType = "Cannot serialize {0}. References in structs are not supported with encoded SOAP.";

		// Token: 0x040013B7 RID: 5047
		public const string XmlRpcArrayOfValueTypes = "Cannot serialize {0}. Arrays of structs are not supported with encoded SOAP.";

		// Token: 0x040013B8 RID: 5048
		public const string XmlDuplicateElementName = "The XML element '{0}' from namespace '{1}' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the element.";

		// Token: 0x040013B9 RID: 5049
		public const string XmlDuplicateAttributeName = "The XML attribute '{0}' from namespace '{1}' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the attribute.";

		// Token: 0x040013BA RID: 5050
		public const string XmlBadBaseElement = "Element '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.";

		// Token: 0x040013BB RID: 5051
		public const string XmlBadBaseType = "Type '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.";

		// Token: 0x040013BC RID: 5052
		public const string XmlUndefinedAlias = "Namespace prefix '{0}' is not defined.";

		// Token: 0x040013BD RID: 5053
		public const string XmlChoiceIdentifierType = "Type of choice identifier '{0}' is inconsistent with type of '{1}'. Please use {2}.";

		// Token: 0x040013BE RID: 5054
		public const string XmlChoiceIdentifierArrayType = "Type of choice identifier '{0}' is inconsistent with type of '{1}'. Please use array of {2}.";

		// Token: 0x040013BF RID: 5055
		public const string XmlChoiceIdentifierTypeEnum = "Choice identifier '{0}' must be an enum.";

		// Token: 0x040013C0 RID: 5056
		public const string XmlChoiceIdentiferMemberMissing = "Missing '{0}' member needed for serialization of choice '{1}'.";

		// Token: 0x040013C1 RID: 5057
		public const string XmlChoiceIdentiferAmbiguous = "Ambiguous choice identifier. There are several members named '{0}'.";

		// Token: 0x040013C2 RID: 5058
		public const string XmlChoiceIdentiferMissing = "You need to add {0} to the '{1}' member.";

		// Token: 0x040013C3 RID: 5059
		public const string XmlChoiceMissingValue = "Type {0} is missing enumeration value '{1}' for element '{2}' from namespace '{3}'.";

		// Token: 0x040013C4 RID: 5060
		public const string XmlChoiceMissingAnyValue = "Type {0} is missing enumeration value '##any:' corresponding to XmlAnyElementAttribute.";

		// Token: 0x040013C5 RID: 5061
		public const string XmlChoiceMismatchChoiceException = "Value of {0} mismatches the type of {1}; you need to set it to {2}.";

		// Token: 0x040013C6 RID: 5062
		public const string XmlArrayItemAmbiguousTypes = "Ambiguous types specified for member '{0}'.  Items '{1}' and '{2}' have the same type.  Please consider using {3} with {4} instead.";

		// Token: 0x040013C7 RID: 5063
		public const string XmlUnsupportedInterface = "Cannot serialize interface {0}.";

		// Token: 0x040013C8 RID: 5064
		public const string XmlUnsupportedInterfaceDetails = "Cannot serialize member {0} of type {1} because it is an interface.";

		// Token: 0x040013C9 RID: 5065
		public const string XmlUnsupportedRank = "Cannot serialize object of type {0}. Multidimensional arrays are not supported.";

		// Token: 0x040013CA RID: 5066
		public const string XmlUnsupportedInheritance = "Using {0} as a base type for a class is not supported by XmlSerializer.";

		// Token: 0x040013CB RID: 5067
		public const string XmlIllegalMultipleText = "Cannot serialize object of type '{0}' because it has multiple XmlText attributes. Consider using an array of strings with XmlTextAttribute for serialization of a mixed complex type.";

		// Token: 0x040013CC RID: 5068
		public const string XmlIllegalMultipleTextMembers = "XmlText may not be used on multiple parameters or return values.";

		// Token: 0x040013CD RID: 5069
		public const string XmlIllegalArrayTextAttribute = "Member '{0}' cannot be encoded using the XmlText attribute. You may use the XmlText attribute to encode primitives, enumerations, arrays of strings, or arrays of XmlNode.";

		// Token: 0x040013CE RID: 5070
		public const string XmlIllegalTypedTextAttribute = "Cannot serialize object of type '{0}'. Consider changing type of XmlText member '{0}.{1}' from {2} to string or string array.";

		// Token: 0x040013CF RID: 5071
		public const string XmlIllegalSimpleContentExtension = "Cannot serialize object of type '{0}'. Base type '{1}' has simpleContent and can only be extended by adding XmlAttribute elements. Please consider changing XmlText member of the base class to string array.";

		// Token: 0x040013D0 RID: 5072
		public const string XmlInvalidCast = "Cannot assign object of type {0} to an object of type {1}.";

		// Token: 0x040013D1 RID: 5073
		public const string XmlInvalidCastWithId = "Cannot assign object of type {0} to an object of type {1}. The error occurred while reading node with id='{2}'.";

		// Token: 0x040013D2 RID: 5074
		public const string XmlInvalidArrayRef = "Invalid reference id='{0}'. Object of type {1} cannot be stored in an array of this type. Details: array index={2}.";

		// Token: 0x040013D3 RID: 5075
		public const string XmlInvalidNullCast = "Cannot assign null value to an object of type {1}.";

		// Token: 0x040013D4 RID: 5076
		public const string XmlMultipleXmlns = "Cannot serialize object of type '{0}' because it has multiple XmlNamespaceDeclarations attributes.";

		// Token: 0x040013D5 RID: 5077
		public const string XmlMultipleXmlnsMembers = "XmlNamespaceDeclarations may not be used on multiple parameters or return values.";

		// Token: 0x040013D6 RID: 5078
		public const string XmlXmlnsInvalidType = "Cannot use XmlNamespaceDeclarations attribute on member '{0}' of type {1}.  This attribute is only valid on members of type {2}.";

		// Token: 0x040013D7 RID: 5079
		public const string XmlSoleXmlnsAttribute = "XmlNamespaceDeclarations attribute cannot be used in conjunction with any other custom attributes.";

		// Token: 0x040013D8 RID: 5080
		public const string XmlConstructorHasSecurityAttributes = "The type '{0}' cannot be serialized because its parameterless constructor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the constructor.";

		// Token: 0x040013D9 RID: 5081
		public const string XmlPropertyHasSecurityAttributes = "The property '{0}' on type '{1}' cannot be serialized because it is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the property accessors.";

		// Token: 0x040013DA RID: 5082
		public const string XmlMethodHasSecurityAttributes = "The type '{0}' cannot be serialized because the {1}({2}) method is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the method.";

		// Token: 0x040013DB RID: 5083
		public const string XmlDefaultAccessorHasSecurityAttributes = "The type '{0}' cannot be serialized because its default accessor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the accessor.";

		// Token: 0x040013DC RID: 5084
		public const string XmlInvalidChoiceIdentifierValue = "Invalid or missing value of the choice identifier '{1}' of type '{0}[]'.";

		// Token: 0x040013DD RID: 5085
		public const string XmlAnyElementDuplicate = "The element '{0}' has been attributed with duplicate XmlAnyElementAttribute(Name=\"{1}\", Namespace=\"{2}\").";

		// Token: 0x040013DE RID: 5086
		public const string XmlChoiceIdDuplicate = "Enum values in the XmlChoiceIdentifier '{0}' have to be unique.  Value '{1}' already present.";

		// Token: 0x040013DF RID: 5087
		public const string XmlChoiceIdentifierMismatch = "Value '{0}' of the choice identifier '{1}' does not match element '{2}' from namespace '{3}'.";

		// Token: 0x040013E0 RID: 5088
		public const string XmlUnsupportedRedefine = "Cannot import schema for type '{0}' from namespace '{1}'. Redefine not supported.";

		// Token: 0x040013E1 RID: 5089
		public const string XmlDuplicateElementInScope = "The XML element named '{0}' from namespace '{1}' is already present in the current scope.";

		// Token: 0x040013E2 RID: 5090
		public const string XmlDuplicateElementInScope1 = "The XML element named '{0}' from namespace '{1}' is already present in the current scope. Elements with the same name in the same scope must have the same type.";

		// Token: 0x040013E3 RID: 5091
		public const string XmlNoPartialTrust = "One or more assemblies referenced by the XmlSerializer cannot be called from partially trusted code.";

		// Token: 0x040013E4 RID: 5092
		public const string XmlInvalidEncodingNotEncoded1 = "The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.";

		// Token: 0x040013E5 RID: 5093
		public const string XmlInvalidEncoding3 = "The encoding style '{0}' is not valid for this call. Valid values are '{1}' for SOAP 1.1 encoding or '{2}' for SOAP 1.2 encoding.";

		// Token: 0x040013E6 RID: 5094
		public const string XmlInvalidSpecifiedType = "Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.";

		// Token: 0x040013E7 RID: 5095
		public const string XmlUnsupportedOpenGenericType = "Type {0} is not supported because it has unbound generic parameters.  Only instantiated generic types can be serialized.";

		// Token: 0x040013E8 RID: 5096
		public const string XmlMismatchSchemaObjects = "Warning: Cannot share {0} named '{1}' from '{2}' namespace. Several mismatched schema declarations were found.";

		// Token: 0x040013E9 RID: 5097
		public const string XmlCircularTypeReference = "Type '{0}' from targetNamespace='{1}' has invalid definition: Circular type reference.";

		// Token: 0x040013EA RID: 5098
		public const string XmlCircularGroupReference = "Group '{0}' from targetNamespace='{1}' has invalid definition: Circular group reference.";

		// Token: 0x040013EB RID: 5099
		public const string XmlRpcLitElementNamespace = "{0}='{1}' is not supported with rpc\\literal SOAP. The wrapper element has to be unqualified.";

		// Token: 0x040013EC RID: 5100
		public const string XmlRpcLitElementNullable = "{0}='{1}' is not supported with rpc\\literal SOAP. The wrapper element cannot be nullable.";

		// Token: 0x040013ED RID: 5101
		public const string XmlRpcLitElements = "Multiple accessors are not supported with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

		// Token: 0x040013EE RID: 5102
		public const string XmlRpcLitArrayElement = "Input or output values of an rpc\\literal method cannot have maxOccurs > 1. Provide a wrapper element for '{0}' by using XmlArray or XmlArrayItem instead of XmlElement attribute.";

		// Token: 0x040013EF RID: 5103
		public const string XmlRpcLitAttributeAttributes = "XmlAttribute and XmlAnyAttribute cannot be used with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

		// Token: 0x040013F0 RID: 5104
		public const string XmlRpcLitAttributes = "XmlText, XmlAnyElement, or XmlChoiceIdentifier cannot be used with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

		// Token: 0x040013F1 RID: 5105
		public const string XmlSequenceMembers = "Explicit sequencing may not be used on parameters or return values.  Please remove {0} property from custom attributes.";

		// Token: 0x040013F2 RID: 5106
		public const string XmlRpcLitXmlns = "Input or output values of an rpc\\literal method cannot have an XmlNamespaceDeclarations attribute (member '{0}').";

		// Token: 0x040013F3 RID: 5107
		public const string XmlDuplicateNs = "Illegal namespace declaration xmlns:{0}='{1}'. Namespace alias '{0}' already defined in the current scope.";

		// Token: 0x040013F4 RID: 5108
		public const string XmlAnonymousInclude = "Cannot include anonymous type '{0}'.";

		// Token: 0x040013F5 RID: 5109
		public const string RefSyntaxNotSupportedForElements0 = "Element reference syntax not supported with encoded SOAP. Replace element reference '{0}' from namespace '{1}' with a local element declaration.";

		// Token: 0x040013F6 RID: 5110
		public const string XmlSchemaIncludeLocation = "Schema attribute schemaLocation='{1}' is not supported on objects of type {0}.  Please set {0}.Schema property.";

		// Token: 0x040013F7 RID: 5111
		public const string XmlSerializableSchemaError = "Schema type information provided by {0} is invalid: {1}";

		// Token: 0x040013F8 RID: 5112
		public const string XmlGetSchemaMethodName = "'{0}' is an invalid language identifier.";

		// Token: 0x040013F9 RID: 5113
		public const string XmlGetSchemaMethodMissing = "You must implement public static {0}({1}) method on {2}.";

		// Token: 0x040013FA RID: 5114
		public const string XmlGetSchemaMethodReturnType = "Method {0}.{1}() specified by {2} has invalid signature: return type must be compatible with {3}.";

		// Token: 0x040013FB RID: 5115
		public const string XmlGetSchemaEmptyTypeName = "{0}.{1}() must return a valid type name.";

		// Token: 0x040013FC RID: 5116
		public const string XmlGetSchemaTypeMissing = "{0}.{1}() must return a valid type name. Type '{2}' cannot be found in the targetNamespace='{3}'.";

		// Token: 0x040013FD RID: 5117
		public const string XmlGetSchemaInclude = "Multiple schemas with targetNamespace='{0}' returned by {1}.{2}().  Please use only the main (parent) schema, and add the others to the schema Includes.";

		// Token: 0x040013FE RID: 5118
		public const string XmlSerializableAttributes = "Only XmlRoot attribute may be specified for the type {0}. Please use {1} to specify schema type.";

		// Token: 0x040013FF RID: 5119
		public const string XmlSerializableMergeItem = "Cannot merge schemas with targetNamespace='{0}'. Several mismatched declarations were found: {1}";

		// Token: 0x04001400 RID: 5120
		public const string XmlSerializableBadDerivation = "Type '{0}' from namespace '{1}' declared as derivation of type '{2}' from namespace '{3}, but corresponding CLR types are not compatible.  Cannot convert type '{4}' to '{5}'.";

		// Token: 0x04001401 RID: 5121
		public const string XmlSerializableMissingClrType = "Type '{0}' from namespace '{1}' does not have corresponding IXmlSerializable type. Please consider adding {2} to '{3}'.";

		// Token: 0x04001402 RID: 5122
		public const string XmlCircularDerivation = "Circular reference in derivation of IXmlSerializable type '{0}'.";

		// Token: 0x04001403 RID: 5123
		public const string XmlSerializerAccessDenied = "Access to the temp directory is denied.  The process under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn\ufffdt have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location.";

		// Token: 0x04001404 RID: 5124
		public const string XmlIdentityAccessDenied = "Access to the temp directory is denied.  Identity '{0}' under which XmlSerializer is running does not have sufficient permission to access the temp directory.  CodeDom will use the user account the process is using to do the compilation, so if the user doesn\ufffdt have access to system temp directory, you will not be able to compile.  Use Path.GetTempPath() API to find out the temp directory location.";

		// Token: 0x04001405 RID: 5125
		public const string XmlMelformMapping = "This mapping was not crated by reflection importer and cannot be used in this context.";

		// Token: 0x04001406 RID: 5126
		public const string XmlSchemaSyntaxErrorDetails = "Schema with targetNamespace='{0}' has invalid syntax. {1} Line {2}, position {3}.";

		// Token: 0x04001407 RID: 5127
		public const string XmlSchemaElementReference = "Element reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

		// Token: 0x04001408 RID: 5128
		public const string XmlSchemaAttributeReference = "Attribute reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

		// Token: 0x04001409 RID: 5129
		public const string XmlSchemaItem = "Schema item '{1}' from namespace '{0}'. {2}";

		// Token: 0x0400140A RID: 5130
		public const string XmlSchemaNamedItem = "Schema item '{1}' named '{2}' from namespace '{0}'. {3}";

		// Token: 0x0400140B RID: 5131
		public const string XmlSchemaContentDef = "Check content definition of schema type '{0}' from namespace '{1}'. {2}";

		// Token: 0x0400140C RID: 5132
		public const string XmlSchema = "Schema with targetNamespace='{0}' has invalid syntax. {1}";

		// Token: 0x0400140D RID: 5133
		public const string XmlSerializerCompileFailed = "Cannot load dynamically generated serialization assembly. In some hosting environments assembly load functionality is restricted, consider using pre-generated serializer. Please see inner exception for more information.";

		// Token: 0x0400140E RID: 5134
		public const string XmlSerializableRootDupName = "Cannot reconcile schema for '{0}'. Please use [XmlRoot] attribute to change default name or namespace of the top-level element to avoid duplicate element declarations: element name='{1}' namespace='{2}'.";

		// Token: 0x0400140F RID: 5135
		public const string XmlDropDefaultAttribute = "DefaultValue attribute on members of type {0} is not supported in this version of the .Net Framework.";

		// Token: 0x04001410 RID: 5136
		public const string XmlDropAttributeValue = "'{0}' attribute on items of type '{1}' is not supported in this version of the .Net Framework.  Ignoring {0}='{2}' attribute.";

		// Token: 0x04001411 RID: 5137
		public const string XmlDropArrayAttributeValue = "'{0}' attribute on array-like elements is not supported in this version of the .Net Framework.  Ignoring {0}='{1}' attribute on element name='{2}'.";

		// Token: 0x04001412 RID: 5138
		public const string XmlDropNonPrimitiveAttributeValue = "'{0}' attribute supported only for primitive types.  Ignoring {0}='{1}' attribute.";

		// Token: 0x04001413 RID: 5139
		public const string XmlNotKnownDefaultValue = "Schema importer extension {0} failed to parse '{1}'='{2}' attribute of type {3} from namespace='{4}'.";

		// Token: 0x04001414 RID: 5140
		public const string XmlRemarks = "<remarks/>";

		// Token: 0x04001415 RID: 5141
		public const string XmlCodegenWarningDetails = "CODEGEN Warning: {0}";

		// Token: 0x04001416 RID: 5142
		public const string XmlExtensionComment = "This type definition was generated by {0} schema importer extension.";

		// Token: 0x04001417 RID: 5143
		public const string XmlExtensionDuplicateDefinition = "Schema importer extension {0} generated duplicate type definitions: {1}.";

		// Token: 0x04001418 RID: 5144
		public const string XmlImporterExtensionBadLocalTypeName = "Schema importer extension {0} returned invalid type information: '{1}' is not a valid type name.";

		// Token: 0x04001419 RID: 5145
		public const string XmlImporterExtensionBadTypeName = "Schema importer extension {0} returned invalid type information for xsd type {1} from namespace='{2}': '{3}' is not a valid type name.";

		// Token: 0x0400141A RID: 5146
		public const string XmlConfigurationDuplicateExtension = "Duplicate extension name.  schemaImporterExtension with name '{0}' already been added.";

		// Token: 0x0400141B RID: 5147
		public const string XmlPregenMissingDirectory = "Could not find directory to save XmlSerializer generated assembly: {0}.";

		// Token: 0x0400141C RID: 5148
		public const string XmlPregenMissingTempDirectory = "Could not find TEMP directory to save XmlSerializer generated assemblies.";

		// Token: 0x0400141D RID: 5149
		public const string XmlPregenTypeDynamic = "Cannot pre-generate serialization assembly for type '{0}'. Pre-generation of serialization assemblies is not supported for dynamic types. Save the assembly and load it from disk to use it with XmlSerialization.";

		// Token: 0x0400141E RID: 5150
		public const string XmlSerializerExpiredDetails = "Pre-generated serializer '{0}' has expired. You need to re-generate serializer for '{1}'.";

		// Token: 0x0400141F RID: 5151
		public const string XmlSerializerExpired = "Pre-generated assembly '{0}' CodeBase='{1}' has expired.";

		// Token: 0x04001420 RID: 5152
		public const string XmlPregenAssemblyDynamic = "Cannot pre-generate serialization assembly. Pre-generation of serialization assemblies is not supported for dynamic assemblies. Save the assembly and load it from disk to use it with XmlSerialization.";

		// Token: 0x04001421 RID: 5153
		public const string XmlNotSerializable = "Type '{0}' is not serializable.";

		// Token: 0x04001422 RID: 5154
		public const string XmlPregenOrphanType = "Cannot pre-generate serializer for multiple assemblies. Type '{0}' does not belong to assembly {1}.";

		// Token: 0x04001423 RID: 5155
		public const string XmlPregenCannotLoad = "Could not load file or assembly '{0}' or one of its dependencies. The system cannot find the file specified.";

		// Token: 0x04001424 RID: 5156
		public const string XmlPregenInvalidXmlSerializerAssemblyAttribute = "Invalid XmlSerializerAssemblyAttribute usage. Please use {0} property or {1} property.";

		// Token: 0x04001425 RID: 5157
		public const string XmlSequenceInconsistent = "Inconsistent sequencing: if used on one of the class's members, the '{0}' property is required on all particle-like members, please explicitly set '{0}' using XmlElement, XmlAnyElement or XmlArray custom attribute on class member '{1}'.";

		// Token: 0x04001426 RID: 5158
		public const string XmlSequenceUnique = "'{1}' values must be unique within the same scope. Value '{0}' is in use. Please change '{1}' property on '{2}'.";

		// Token: 0x04001427 RID: 5159
		public const string XmlSequenceHierarchy = "There was an error processing type '{0}'. Type member '{1}' declared in '{2}' is missing required '{3}' property. If one class in the class hierarchy uses explicit sequencing feature ({3}), then its base class and all derived classes have to do the same.";

		// Token: 0x04001428 RID: 5160
		public const string XmlSequenceMatch = "If multiple custom attributes specified on a single member only one of them have to have explicit '{0}' property, however if more that one attribute has the explicit '{0}', all values have to match.";

		// Token: 0x04001429 RID: 5161
		public const string XmlDisallowNegativeValues = "Negative values are prohibited.";

		// Token: 0x0400142A RID: 5162
		public const string Xml_BadComment = "This is an invalid comment syntax.  Expected '-->'.";

		// Token: 0x0400142B RID: 5163
		public const string Xml_NumEntityOverflow = "The numeric entity value is too large.";

		// Token: 0x0400142C RID: 5164
		public const string Xml_UnexpectedCharacter = "'{0}', hexadecimal value {1}, is an unexpected character.";

		// Token: 0x0400142D RID: 5165
		public const string Xml_UnexpectedToken1 = "This is an unexpected token. The expected token is '|' or ')'.";

		// Token: 0x0400142E RID: 5166
		public const string Xml_TagMismatchFileName = "The '{0}' start tag on line '{1}' doesn't match the end tag of '{2}' in file '{3}'.";

		// Token: 0x0400142F RID: 5167
		public const string Xml_ReservedNs = "This is a reserved namespace.";

		// Token: 0x04001430 RID: 5168
		public const string Xml_BadElementData = "The element data is invalid.";

		// Token: 0x04001431 RID: 5169
		public const string Xml_UnexpectedElement = "The <{0}> tag from namespace {1} is not expected.";

		// Token: 0x04001432 RID: 5170
		public const string Xml_TagNotInTheSameEntity = "<{0}> and </{0}> are not defined in the same entity.";

		// Token: 0x04001433 RID: 5171
		public const string Xml_InvalidPartialContentData = "There is invalid partial content data.";

		// Token: 0x04001434 RID: 5172
		public const string Xml_CanNotStartWithXmlInNamespace = "Namespace qualifiers beginning with 'xml' are reserved, and cannot be used in user-specified namespaces.";

		// Token: 0x04001435 RID: 5173
		public const string Xml_UnparsedEntity = "The '{0}' entity is not an unparsed entity.";

		// Token: 0x04001436 RID: 5174
		public const string Xml_InvalidContentForThisNode = "Invalid content for {0} NodeType.";

		// Token: 0x04001437 RID: 5175
		public const string Xml_MissingEncodingDecl = "Encoding declaration is required in an XmlDeclaration in an external entity.";

		// Token: 0x04001438 RID: 5176
		public const string Xml_InvalidSurrogatePair = "The surrogate pair is invalid.";

		// Token: 0x04001439 RID: 5177
		public const string Sch_ErrorPosition = "An error occurred at {0}, ({1}, {2}).";

		// Token: 0x0400143A RID: 5178
		public const string Sch_ReservedNsDecl = "The '{0}' prefix is reserved.";

		// Token: 0x0400143B RID: 5179
		public const string Sch_NotInSchemaCollection = "The '{0}' schema does not exist in the XmlSchemaCollection.";

		// Token: 0x0400143C RID: 5180
		public const string Sch_NotationNotAttr = "This NOTATION should be used only on attributes.";

		// Token: 0x0400143D RID: 5181
		public const string Sch_InvalidContent = "The element '{0}' has invalid content.";

		// Token: 0x0400143E RID: 5182
		public const string Sch_InvalidContentExpecting = "The element '{0}' has invalid content. Expected '{1}'.";

		// Token: 0x0400143F RID: 5183
		public const string Sch_InvalidTextWhiteSpace = "The element cannot contain text or white space. Content model is empty.";

		// Token: 0x04001440 RID: 5184
		public const string Sch_XSCHEMA = "x-schema can load only XDR schemas.";

		// Token: 0x04001441 RID: 5185
		public const string Sch_DubSchema = "Schema for targetNamespace '{0}' already present in collection and being used for validation.";

		// Token: 0x04001442 RID: 5186
		public const string Xp_TokenExpected = "A token was expected.";

		// Token: 0x04001443 RID: 5187
		public const string Xp_NodeTestExpected = "A NodeTest was expected at {0}.";

		// Token: 0x04001444 RID: 5188
		public const string Xp_NumberExpected = "A number was expected.";

		// Token: 0x04001445 RID: 5189
		public const string Xp_QueryExpected = "A query was expected.";

		// Token: 0x04001446 RID: 5190
		public const string Xp_InvalidArgument = "'{0}' function in '{1}' has an invalid argument. Possibly ')' is missing.";

		// Token: 0x04001447 RID: 5191
		public const string Xp_FunctionExpected = "A function was expected.";

		// Token: 0x04001448 RID: 5192
		public const string Xp_InvalidPatternString = "{0} is an invalid XSLT pattern.";

		// Token: 0x04001449 RID: 5193
		public const string Xp_BadQueryString = "The XPath expression passed into Compile() is null or empty.";

		// Token: 0x0400144A RID: 5194
		public const string XdomXpNav_NullParam = "The parameter (other) being passed in is null.";

		// Token: 0x0400144B RID: 5195
		public const string Xdom_Load_NodeType = "XmlLoader.Load(): Unexpected NodeType: {0}.";

		// Token: 0x0400144C RID: 5196
		public const string XmlMissingMethod = "{0} was not found in {1}.";

		// Token: 0x0400144D RID: 5197
		public const string XmlIncludeSerializableError = "Type {0} is derived from {1} and therefore cannot be used with attribute XmlInclude.";

		// Token: 0x0400144E RID: 5198
		public const string XmlCompilerDynModule = "Unable to generate a serializer for type {0} from assembly {1} because the assembly may be dynamic. Save the assembly and load it from disk to use it with XmlSerialization.";

		// Token: 0x0400144F RID: 5199
		public const string XmlInvalidSchemaType = "Types must be declared at the top level in the schema.";

		// Token: 0x04001450 RID: 5200
		public const string XmlInvalidAnyUse = "Any may not be specified.";

		// Token: 0x04001451 RID: 5201
		public const string XmlSchemaSyntaxError = "Schema with targetNamespace='{0}' has invalid syntax.";

		// Token: 0x04001452 RID: 5202
		public const string XmlDuplicateChoiceElement = "The XML element named '{0}' from namespace '{1}' is already present in the current scope. Elements with the same name in the same scope must have the same type.";

		// Token: 0x04001453 RID: 5203
		public const string XmlConvert_BadTimeSpan = "The string was not recognized as a valid TimeSpan value.";

		// Token: 0x04001454 RID: 5204
		public const string XmlConvert_BadBoolean = "The string was not recognized as a valid Boolean value.";

		// Token: 0x04001455 RID: 5205
		public const string Xml_UnexpectedToken = "This is an unexpected token. The expected token is '{0}'.";

		// Token: 0x04001456 RID: 5206
		public const string Xml_PartialContentNodeTypeNotSupported = "This NodeType is not supported for partial content parsing.";

		// Token: 0x04001457 RID: 5207
		public const string Sch_AttributeValueDataType = "The '{0}' attribute has an invalid value according to its data type.";

		// Token: 0x04001458 RID: 5208
		public const string Sch_ElementValueDataType = "The '{0}' element has an invalid value according to its data type.";

		// Token: 0x04001459 RID: 5209
		public const string Sch_NonDeterministicAny = "The content model must be deterministic. Wildcard declaration along with a local element declaration causes the content model to become ambiguous.";

		// Token: 0x0400145A RID: 5210
		public const string Sch_MismatchTargetNamespace = "The attribute targetNamespace does not match the designated namespace URI.";

		// Token: 0x0400145B RID: 5211
		public const string Sch_UnionFailed = "Union does not support this value.";

		// Token: 0x0400145C RID: 5212
		public const string Sch_XsiTypeBlocked = "The element '{0}' has xsi:type derivation blocked.";

		// Token: 0x0400145D RID: 5213
		public const string Sch_InvalidElementInEmpty = "The element cannot contain child element. Content model is empty.";

		// Token: 0x0400145E RID: 5214
		public const string Sch_InvalidElementInTextOnly = "The element cannot contain a child element. Content model is text only.";

		// Token: 0x0400145F RID: 5215
		public const string Sch_InvalidNameAttribute = "Invalid 'name' attribute value: {0}.";

		// Token: 0x04001460 RID: 5216
		public const string XmlInternalError = "Internal error.";

		// Token: 0x04001461 RID: 5217
		public const string XmlInternalErrorDetails = "Internal error: {0}.";

		// Token: 0x04001462 RID: 5218
		public const string XmlInternalErrorMethod = "Internal error: missing generated method for {0}.";

		// Token: 0x04001463 RID: 5219
		public const string XmlInternalErrorReaderAdvance = "Internal error: deserialization failed to advance over underlying stream.";

		// Token: 0x04001464 RID: 5220
		public const string Enc_InvalidByteInEncoding = "Invalid byte was found at index {0}.";

		// Token: 0x04001465 RID: 5221
		public const string Arg_ExpectingXmlTextReader = "The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader.";

		// Token: 0x04001466 RID: 5222
		public const string Arg_CannotCreateNode = "Cannot create node of type {0}.";

		// Token: 0x04001467 RID: 5223
		public const string Arg_IncompatibleParamType = "Type is incompatible.";

		// Token: 0x04001468 RID: 5224
		public const string XmlNonCLSCompliantException = "Non-CLS Compliant Exception.";

		// Token: 0x04001469 RID: 5225
		public const string Xml_CannotFindFileInXapPackage = "Cannot find file '{0}' in the application xap package.";

		// Token: 0x0400146A RID: 5226
		public const string Xml_XapResolverCannotOpenUri = "Cannot open '{0}'. The Uri parameter must be a relative path pointing to content inside the Silverlight application's XAP package. If you need to load content from an arbitrary Uri, please see the documentation on Loading XML content using WebClient/HttpWebRequest.";
	}
}
