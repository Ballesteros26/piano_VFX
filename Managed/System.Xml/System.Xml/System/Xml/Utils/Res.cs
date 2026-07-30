using System;

namespace System.Xml.Utils
{
	// Token: 0x020002AD RID: 685
	internal static class Res
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x00002068 File Offset: 0x00000268
		public static string GetString(string name)
		{
			return name;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00090414 File Offset: 0x0008E614
		public static string GetString(string name, params object[] args)
		{
			return SR.GetString(name, args);
		}

		// Token: 0x0400146B RID: 5227
		public const string Xml_UserException = "{0}";

		// Token: 0x0400146C RID: 5228
		public const string Xml_ErrorFilePosition = "An error occurred at {0}({1},{2}).";

		// Token: 0x0400146D RID: 5229
		public const string Xml_InvalidOperation = "Operation is not valid due to the current state of the object.";

		// Token: 0x0400146E RID: 5230
		public const string Xml_EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";

		// Token: 0x0400146F RID: 5231
		public const string XPath_UnclosedString = "String literal was not closed.";

		// Token: 0x04001470 RID: 5232
		public const string XPath_ScientificNotation = "Scientific notation is not allowed.";

		// Token: 0x04001471 RID: 5233
		public const string XPath_UnexpectedToken = "Unexpected token '{0}' in the expression.";

		// Token: 0x04001472 RID: 5234
		public const string XPath_NodeTestExpected = "Expected a node test, found '{0}'.";

		// Token: 0x04001473 RID: 5235
		public const string XPath_EofExpected = "Expected end of the expression, found '{0}'.";

		// Token: 0x04001474 RID: 5236
		public const string XPath_TokenExpected = "Expected token '{0}', found '{1}'.";

		// Token: 0x04001475 RID: 5237
		public const string XPath_InvalidAxisInPattern = "Only 'child' and 'attribute' axes are allowed in a pattern outside predicates.";

		// Token: 0x04001476 RID: 5238
		public const string XPath_PredicateAfterDot = "Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead.";

		// Token: 0x04001477 RID: 5239
		public const string XPath_PredicateAfterDotDot = "Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead.";

		// Token: 0x04001478 RID: 5240
		public const string XPath_NArgsExpected = "Function '{0}()' must have {1} argument(s).";

		// Token: 0x04001479 RID: 5241
		public const string XPath_NOrMArgsExpected = "Function '{0}()' must have {1} or {2} argument(s).";

		// Token: 0x0400147A RID: 5242
		public const string XPath_AtLeastNArgsExpected = "Function '{0}()' must have at least {1} argument(s).";

		// Token: 0x0400147B RID: 5243
		public const string XPath_AtMostMArgsExpected = "Function '{0}()' must have no more than {2} arguments.";

		// Token: 0x0400147C RID: 5244
		public const string XPath_NodeSetArgumentExpected = "Argument {1} of function '{0}()' cannot be converted to a node-set.";

		// Token: 0x0400147D RID: 5245
		public const string XPath_NodeSetExpected = "Expression must evaluate to a node-set.";

		// Token: 0x0400147E RID: 5246
		public const string XPath_RtfInPathExpr = "To use a result tree fragment in a path expression, first convert it to a node-set using the msxsl:node-set() function.";

		// Token: 0x0400147F RID: 5247
		public const string Xslt_WarningAsError = "Warning as Error: {0}";

		// Token: 0x04001480 RID: 5248
		public const string Xslt_InputTooComplex = "The stylesheet is too complex.";

		// Token: 0x04001481 RID: 5249
		public const string Xslt_CannotLoadStylesheet = "Cannot load the stylesheet object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. One of Stream, XmlReader, and IXPathNavigable types was expected.";

		// Token: 0x04001482 RID: 5250
		public const string Xslt_WrongStylesheetElement = "Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.";

		// Token: 0x04001483 RID: 5251
		public const string Xslt_WdXslNamespace = "The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported.";

		// Token: 0x04001484 RID: 5252
		public const string Xslt_NotAtTop = "'{0}' element children must precede all other children of the '{1}' element.";

		// Token: 0x04001485 RID: 5253
		public const string Xslt_UnexpectedElement = "'{0}' cannot be a child of the '{1}' element.";

		// Token: 0x04001486 RID: 5254
		public const string Xslt_NullNsAtTopLevel = "Top-level element '{0}' may not have a null namespace URI.";

		// Token: 0x04001487 RID: 5255
		public const string Xslt_TextNodesNotAllowed = "'{0}' element cannot have text node children.";

		// Token: 0x04001488 RID: 5256
		public const string Xslt_NotEmptyContents = "The contents of '{0}' must be empty.";

		// Token: 0x04001489 RID: 5257
		public const string Xslt_InvalidAttribute = "'{0}' is an invalid attribute for the '{1}' element.";

		// Token: 0x0400148A RID: 5258
		public const string Xslt_MissingAttribute = "Missing mandatory attribute '{0}'.";

		// Token: 0x0400148B RID: 5259
		public const string Xslt_InvalidAttrValue = "'{1}' is an invalid value for the '{0}' attribute.";

		// Token: 0x0400148C RID: 5260
		public const string Xslt_BistateAttribute = "The value of the '{0}' attribute must be '{1}' or '{2}'.";

		// Token: 0x0400148D RID: 5261
		public const string Xslt_CharAttribute = "The value of the '{0}' attribute must be a single character.";

		// Token: 0x0400148E RID: 5262
		public const string Xslt_CircularInclude = "Stylesheet '{0}' cannot directly or indirectly include or import itself.";

		// Token: 0x0400148F RID: 5263
		public const string Xslt_SingleRightBraceInAvt = "The right curly brace in an attribute value template '{0}' outside an expression must be doubled.";

		// Token: 0x04001490 RID: 5264
		public const string Xslt_VariableCntSel2 = "The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.";

		// Token: 0x04001491 RID: 5265
		public const string Xslt_KeyCntUse = "'xsl:key' has a 'use' attribute and has non-empty content, or it has empty content and no 'use' attribute.";

		// Token: 0x04001492 RID: 5266
		public const string Xslt_DupTemplateName = "'{0}' is a duplicate template name.";

		// Token: 0x04001493 RID: 5267
		public const string Xslt_BothMatchNameAbsent = "'xsl:template' must have either a 'match' attribute or a 'name' attribute, or both.";

		// Token: 0x04001494 RID: 5268
		public const string Xslt_InvalidVariable = "The variable or parameter '{0}' is either not defined or it is out of scope.";

		// Token: 0x04001495 RID: 5269
		public const string Xslt_DupGlobalVariable = "The variable or parameter '{0}' was duplicated with the same import precedence.";

		// Token: 0x04001496 RID: 5270
		public const string Xslt_DupLocalVariable = "The variable or parameter '{0}' was duplicated within the same scope.";

		// Token: 0x04001497 RID: 5271
		public const string Xslt_DupNsAlias = "Namespace URI '{0}' is declared to be an alias for multiple different namespace URIs with the same import precedence.";

		// Token: 0x04001498 RID: 5272
		public const string Xslt_EmptyAttrValue = "The value of the '{0}' attribute cannot be empty.";

		// Token: 0x04001499 RID: 5273
		public const string Xslt_EmptyNsAlias = "The value of the '{0}' attribute cannot be empty. Use '#default' to specify the default namespace.";

		// Token: 0x0400149A RID: 5274
		public const string Xslt_UnknownXsltFunction = "'{0}()' is an unknown XSLT function.";

		// Token: 0x0400149B RID: 5275
		public const string Xslt_UnsupportedXsltFunction = "'{0}()' is an unsupported XSLT function.";

		// Token: 0x0400149C RID: 5276
		public const string Xslt_NoAttributeSet = "A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.";

		// Token: 0x0400149D RID: 5277
		public const string Xslt_UndefinedKey = "A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.";

		// Token: 0x0400149E RID: 5278
		public const string Xslt_CircularAttributeSet = "Circular reference in the definition of attribute set '{0}'.";

		// Token: 0x0400149F RID: 5279
		public const string Xslt_InvalidCallTemplate = "The named template '{0}' does not exist.";

		// Token: 0x040014A0 RID: 5280
		public const string Xslt_InvalidPrefix = "Prefix '{0}' is not defined.";

		// Token: 0x040014A1 RID: 5281
		public const string Xslt_ScriptXsltNamespace = "Script block cannot implement the XSLT namespace.";

		// Token: 0x040014A2 RID: 5282
		public const string Xslt_ScriptInvalidLanguage = "Scripting language '{0}' is not supported.";

		// Token: 0x040014A3 RID: 5283
		public const string Xslt_ScriptMixedLanguages = "All script blocks implementing the namespace '{0}' must use the same language.";

		// Token: 0x040014A4 RID: 5284
		public const string Xslt_ScriptCompileException = "Error occurred while compiling the script: {0}";

		// Token: 0x040014A5 RID: 5285
		public const string Xslt_ScriptNotAtTop = "Element '{0}' must precede script code.";

		// Token: 0x040014A6 RID: 5286
		public const string Xslt_AssemblyNameHref = "'msxsl:assembly' must have either a 'name' attribute or an 'href' attribute, but not both.";

		// Token: 0x040014A7 RID: 5287
		public const string Xslt_ScriptAndExtensionClash = "Cannot have both an extension object and a script implementing the same namespace '{0}'.";

		// Token: 0x040014A8 RID: 5288
		public const string Xslt_NoDecimalFormat = "Decimal format '{0}' is not defined.";

		// Token: 0x040014A9 RID: 5289
		public const string Xslt_DecimalFormatSignsNotDistinct = "The '{0}' and '{1}' attributes of 'xsl:decimal-format' must have distinct values.";

		// Token: 0x040014AA RID: 5290
		public const string Xslt_DecimalFormatRedefined = "The '{0}' attribute of 'xsl:decimal-format' cannot be redefined with a value of '{1}'.";

		// Token: 0x040014AB RID: 5291
		public const string Xslt_UnknownExtensionElement = "'{0}' is not a recognized extension element.";

		// Token: 0x040014AC RID: 5292
		public const string Xslt_ModeWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

		// Token: 0x040014AD RID: 5293
		public const string Xslt_ModeListEmpty = "List of modes in 'xsl:template' element can't be empty.";

		// Token: 0x040014AE RID: 5294
		public const string Xslt_ModeListDup = "List of modes in 'xsl:template' element can't contain duplicates ('{0}').";

		// Token: 0x040014AF RID: 5295
		public const string Xslt_ModeListAll = "List of modes in 'xsl:template' element can't contain token '#all' together with any other value.";

		// Token: 0x040014B0 RID: 5296
		public const string Xslt_PriorityWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'priority' attribute.";

		// Token: 0x040014B1 RID: 5297
		public const string Xslt_InvalidApplyImports = "An 'xsl:apply-imports' element can only occur within an 'xsl:template' element with a 'match' attribute, and cannot occur within an 'xsl:for-each' element.";

		// Token: 0x040014B2 RID: 5298
		public const string Xslt_DuplicateWithParam = "Value of parameter '{0}' cannot be specified more than once within a single 'xsl:call-template' or 'xsl:apply-templates' element.";

		// Token: 0x040014B3 RID: 5299
		public const string Xslt_ReservedNS = "Elements and attributes cannot belong to the reserved namespace '{0}'.";

		// Token: 0x040014B4 RID: 5300
		public const string Xslt_XmlnsAttr = "An attribute with a local name 'xmlns' and a null namespace URI cannot be created.";

		// Token: 0x040014B5 RID: 5301
		public const string Xslt_NoWhen = "An 'xsl:choose' element must have at least one 'xsl:when' child.";

		// Token: 0x040014B6 RID: 5302
		public const string Xslt_WhenAfterOtherwise = "'xsl:when' must precede the 'xsl:otherwise' element.";

		// Token: 0x040014B7 RID: 5303
		public const string Xslt_DupOtherwise = "An 'xsl:choose' element can have only one 'xsl:otherwise' child.";

		// Token: 0x040014B8 RID: 5304
		public const string Xslt_AttributeRedefinition = "Attribute '{0}' of 'xsl:output' cannot be defined more than once with the same import precedence.";

		// Token: 0x040014B9 RID: 5305
		public const string Xslt_InvalidMethod = "'{0}' is not a supported output method. Supported methods are 'xml', 'html', and 'text'.";

		// Token: 0x040014BA RID: 5306
		public const string Xslt_InvalidEncoding = "'{0}' is not a supported encoding name.";

		// Token: 0x040014BB RID: 5307
		public const string Xslt_InvalidLanguage = "'{0}' is not a supported language identifier.";

		// Token: 0x040014BC RID: 5308
		public const string Xslt_InvalidCompareOption = "String comparison option(s) '{0}' are either invalid or cannot be used together.";

		// Token: 0x040014BD RID: 5309
		public const string Xslt_KeyNotAllowed = "The 'key()' function cannot be used in 'use' and 'match' attributes of 'xsl:key' element.";

		// Token: 0x040014BE RID: 5310
		public const string Xslt_VariablesNotAllowed = "Variables cannot be used within this expression.";

		// Token: 0x040014BF RID: 5311
		public const string Xslt_CurrentNotAllowed = "The 'current()' function cannot be used in a pattern.";

		// Token: 0x040014C0 RID: 5312
		public const string Xslt_DocumentFuncProhibited = "Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.";

		// Token: 0x040014C1 RID: 5313
		public const string Xslt_ScriptsProhibited = "Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.";

		// Token: 0x040014C2 RID: 5314
		public const string Xslt_ItemNull = "Extension functions cannot return null values.";

		// Token: 0x040014C3 RID: 5315
		public const string Xslt_NodeSetNotNode = "Cannot convert a node-set which contains zero nodes or more than one node to a single node.";

		// Token: 0x040014C4 RID: 5316
		public const string Xslt_UnsupportedClrType = "Extension function parameters or return values which have Clr type '{0}' are not supported.";

		// Token: 0x040014C5 RID: 5317
		public const string Xslt_NotYetImplemented = "'{0}' is not yet implemented.";

		// Token: 0x040014C6 RID: 5318
		public const string Xslt_SchemaDeclaration = "'{0}' declaration is not permitted in non-schema aware processor.";

		// Token: 0x040014C7 RID: 5319
		public const string Xslt_SchemaAttribute = "Attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

		// Token: 0x040014C8 RID: 5320
		public const string Xslt_SchemaAttributeValue = "Value '{1}' of attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

		// Token: 0x040014C9 RID: 5321
		public const string Xslt_ElementCntSel = "The element '{0}' cannot have both a 'select' attribute and non-empty content.";

		// Token: 0x040014CA RID: 5322
		public const string Xslt_PerformSortCntSel = "The element 'xsl:perform-sort' cannot have 'select' attribute any content other than 'xsl:sort' and 'xsl:fallback' instructions.";

		// Token: 0x040014CB RID: 5323
		public const string Xslt_RequiredAndSelect = "Mandatory parameter '{0}' must be empty and must not have a 'select' attribute.";

		// Token: 0x040014CC RID: 5324
		public const string Xslt_NoSelectNoContent = "Element '{0}' must have either 'select' attribute or non-empty content.";

		// Token: 0x040014CD RID: 5325
		public const string Xslt_NonTemplateTunnel = "Stylesheet or function parameter '{0}' cannot have attribute 'tunnel'.";

		// Token: 0x040014CE RID: 5326
		public const string Xslt_RequiredOnFunction = "The 'required' attribute must not be specified for parameter '{0}'. Function parameters are always mandatory.";

		// Token: 0x040014CF RID: 5327
		public const string Xslt_ExcludeDefault = "Value '#default' is used within the 'exclude-result-prefixes' attribute and the parent element of this attribute has no default namespace.";

		// Token: 0x040014D0 RID: 5328
		public const string Xslt_CollationSyntax = "The value of an 'default-collation' attribute contains no recognized collation URI.";

		// Token: 0x040014D1 RID: 5329
		public const string Xslt_AnalyzeStringDupChild = "'xsl:analyze-string' cannot have second child with name '{0}'.";

		// Token: 0x040014D2 RID: 5330
		public const string Xslt_AnalyzeStringChildOrder = "When both 'xsl:matching-string' and 'xsl:non-matching-string' elements are present, 'xsl:matching-string' element must come first.";

		// Token: 0x040014D3 RID: 5331
		public const string Xslt_AnalyzeStringEmpty = "'xsl:analyze-string' must contain either 'xsl:matching-string' or 'xsl:non-matching-string' elements or both.";

		// Token: 0x040014D4 RID: 5332
		public const string Xslt_SortStable = "Only the first 'xsl:sort' element may have 'stable' attribute.";

		// Token: 0x040014D5 RID: 5333
		public const string Xslt_InputTypeAnnotations = "It is an error if there is a stylesheet module in the stylesheet that specifies 'input-type-annotations'=\"strip\" and another stylesheet module that specifies 'input-type-annotations'=\"preserve\".";

		// Token: 0x040014D6 RID: 5334
		public const string Coll_BadOptFormat = "Collation option '{0}' is invalid. Options must have the following format: <option-name>=<option-value>.";

		// Token: 0x040014D7 RID: 5335
		public const string Coll_Unsupported = "The collation '{0}' is not supported.";

		// Token: 0x040014D8 RID: 5336
		public const string Coll_UnsupportedLanguage = "Collation language '{0}' is not supported.";

		// Token: 0x040014D9 RID: 5337
		public const string Coll_UnsupportedOpt = "Unsupported option '{0}' in collation.";

		// Token: 0x040014DA RID: 5338
		public const string Coll_UnsupportedOptVal = "Collation option '{0}' cannot have the value '{1}'.";

		// Token: 0x040014DB RID: 5339
		public const string Coll_UnsupportedSortOpt = "Unsupported sort option '{0}' in collation.";

		// Token: 0x040014DC RID: 5340
		public const string Qil_Validation = "QIL Validation Error! '{0}'.";

		// Token: 0x040014DD RID: 5341
		public const string XmlIl_TooManyParameters = "Functions may not have more than 65535 parameters.";

		// Token: 0x040014DE RID: 5342
		public const string XmlIl_BadXmlState = "An item of type '{0}' cannot be constructed within a node of type '{1}'.";

		// Token: 0x040014DF RID: 5343
		public const string XmlIl_BadXmlStateAttr = "Attribute and namespace nodes cannot be added to the parent element after a text, comment, pi, or sub-element node has already been added.";

		// Token: 0x040014E0 RID: 5344
		public const string XmlIl_NmspAfterAttr = "Namespace nodes cannot be added to the parent element after an attribute node has already been added.";

		// Token: 0x040014E1 RID: 5345
		public const string XmlIl_NmspConflict = "Cannot construct namespace declaration xmlns{0}{1}='{2}'. Prefix '{1}' is already mapped to namespace '{3}'.";

		// Token: 0x040014E2 RID: 5346
		public const string XmlIl_CantResolveEntity = "Cannot query the data source object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. Only Stream, XmlReader, and IXPathNavigable data source objects are currently supported.";

		// Token: 0x040014E3 RID: 5347
		public const string XmlIl_NoDefaultDocument = "Query requires a default data source, but no default was supplied to the query engine.";

		// Token: 0x040014E4 RID: 5348
		public const string XmlIl_UnknownDocument = "Data source '{0}' cannot be located.";

		// Token: 0x040014E5 RID: 5349
		public const string XmlIl_UnknownParam = "Supplied XsltArgumentList does not contain a parameter with local name '{0}' and namespace '{1}'.";

		// Token: 0x040014E6 RID: 5350
		public const string XmlIl_UnknownExtObj = "Cannot find a script or an extension object associated with namespace '{0}'.";

		// Token: 0x040014E7 RID: 5351
		public const string XmlIl_CantStripNav = "White space cannot be stripped from input documents that have already been loaded. Provide the input document as an XmlReader instead.";

		// Token: 0x040014E8 RID: 5352
		public const string XmlIl_ExtensionError = "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.";

		// Token: 0x040014E9 RID: 5353
		public const string XmlIl_TopLevelAttrNmsp = "XmlWriter cannot process the sequence returned by the query, because it contains an attribute or namespace node.";

		// Token: 0x040014EA RID: 5354
		public const string XmlIl_NoExtensionMethod = "Extension object '{0}' does not contain a matching '{1}' method that has {2} parameter(s).";

		// Token: 0x040014EB RID: 5355
		public const string XmlIl_AmbiguousExtensionMethod = "Ambiguous method call. Extension object '{0}' contains multiple '{1}' methods that have {2} parameter(s).";

		// Token: 0x040014EC RID: 5356
		public const string XmlIl_NonPublicExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is not public.";

		// Token: 0x040014ED RID: 5357
		public const string XmlIl_GenericExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is generic.";

		// Token: 0x040014EE RID: 5358
		public const string XmlIl_ByRefType = "Method '{1}' of extension object '{0}' cannot be called because it has one or more ByRef parameters.";

		// Token: 0x040014EF RID: 5359
		public const string XmlIl_DocumentLoadError = "An error occurred while loading document '{0}'. See InnerException for a complete description of the error.";

		// Token: 0x040014F0 RID: 5360
		public const string Xslt_CompileError = "XSLT compile error at {0}({1},{2}). See InnerException for details.";

		// Token: 0x040014F1 RID: 5361
		public const string Xslt_CompileError2 = "XSLT compile error.";

		// Token: 0x040014F2 RID: 5362
		public const string Xslt_UnsuppFunction = "'{0}()' is an unsupported XSLT function.";

		// Token: 0x040014F3 RID: 5363
		public const string Xslt_NotFirstImport = "'xsl:import' instructions must precede all other element children of an 'xsl:stylesheet' element.";

		// Token: 0x040014F4 RID: 5364
		public const string Xslt_UnexpectedKeyword = "'{0}' cannot be a child of the '{1}' element.";

		// Token: 0x040014F5 RID: 5365
		public const string Xslt_InvalidContents = "The contents of '{0}' are invalid.";

		// Token: 0x040014F6 RID: 5366
		public const string Xslt_CantResolve = "Cannot resolve the referenced document '{0}'.";

		// Token: 0x040014F7 RID: 5367
		public const string Xslt_SingleRightAvt = "Right curly brace in the attribute value template '{0}' must be doubled.";

		// Token: 0x040014F8 RID: 5368
		public const string Xslt_OpenBracesAvt = "The braces are not closed in AVT expression '{0}'.";

		// Token: 0x040014F9 RID: 5369
		public const string Xslt_OpenLiteralAvt = "The literal in AVT expression is not correctly closed '{0}'.";

		// Token: 0x040014FA RID: 5370
		public const string Xslt_NestedAvt = "AVT cannot be nested in AVT '{0}'.";

		// Token: 0x040014FB RID: 5371
		public const string Xslt_EmptyAvtExpr = "XPath Expression in AVT cannot be empty: '{0}'.";

		// Token: 0x040014FC RID: 5372
		public const string Xslt_InvalidXPath = "'{0}' is an invalid XPath expression.";

		// Token: 0x040014FD RID: 5373
		public const string Xslt_InvalidQName = "'{0}' is an invalid QName.";

		// Token: 0x040014FE RID: 5374
		public const string Xslt_NoStylesheetLoaded = "No stylesheet was loaded.";

		// Token: 0x040014FF RID: 5375
		public const string Xslt_TemplateNoAttrib = "The 'xsl:template' instruction must have the 'match' and/or 'name' attribute present.";

		// Token: 0x04001500 RID: 5376
		public const string Xslt_DupVarName = "Variable or parameter '{0}' was duplicated within the same scope.";

		// Token: 0x04001501 RID: 5377
		public const string Xslt_WrongNumberArgs = "XSLT function '{0}()' has the wrong number of arguments.";

		// Token: 0x04001502 RID: 5378
		public const string Xslt_NoNodeSetConversion = "Cannot convert the operand to a node-set.";

		// Token: 0x04001503 RID: 5379
		public const string Xslt_NoNavigatorConversion = "Cannot convert the operand to 'Result tree fragment'.";

		// Token: 0x04001504 RID: 5380
		public const string Xslt_FunctionFailed = "Function '{0}()' has failed.";

		// Token: 0x04001505 RID: 5381
		public const string Xslt_InvalidFormat = "Format cannot be empty.";

		// Token: 0x04001506 RID: 5382
		public const string Xslt_InvalidFormat1 = "Format '{0}' cannot have digit symbol after zero digit symbol before a decimal point.";

		// Token: 0x04001507 RID: 5383
		public const string Xslt_InvalidFormat2 = "Format '{0}' cannot have zero digit symbol after digit symbol after decimal point.";

		// Token: 0x04001508 RID: 5384
		public const string Xslt_InvalidFormat3 = "Format '{0}' has two pattern separators.";

		// Token: 0x04001509 RID: 5385
		public const string Xslt_InvalidFormat4 = "Format '{0}' cannot end with a pattern separator.";

		// Token: 0x0400150A RID: 5386
		public const string Xslt_InvalidFormat5 = "Format '{0}' cannot have two decimal separators.";

		// Token: 0x0400150B RID: 5387
		public const string Xslt_InvalidFormat8 = "Format string should have at least one digit or zero digit.";

		// Token: 0x0400150C RID: 5388
		public const string Xslt_ScriptCompileErrors = "Script compile errors:\n{0}";

		// Token: 0x0400150D RID: 5389
		public const string Xslt_ScriptInvalidPrefix = "Cannot find the script or external object that implements prefix '{0}'.";

		// Token: 0x0400150E RID: 5390
		public const string Xslt_ScriptDub = "Namespace '{0}' has a duplicate implementation.";

		// Token: 0x0400150F RID: 5391
		public const string Xslt_ScriptEmpty = "The 'msxsl:script' element cannot be empty.";

		// Token: 0x04001510 RID: 5392
		public const string Xslt_DupDecimalFormat = "Decimal format '{0}' has a duplicate declaration.";

		// Token: 0x04001511 RID: 5393
		public const string Xslt_CircularReference = "Circular reference in the definition of variable '{0}'.";

		// Token: 0x04001512 RID: 5394
		public const string Xslt_InvalidExtensionNamespace = "Extension namespace cannot be 'null' or an XSLT namespace URI.";

		// Token: 0x04001513 RID: 5395
		public const string Xslt_InvalidModeAttribute = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

		// Token: 0x04001514 RID: 5396
		public const string Xslt_MultipleRoots = "There are multiple root elements in the output XML.";

		// Token: 0x04001515 RID: 5397
		public const string Xslt_ApplyImports = "The 'xsl:apply-imports' instruction cannot be included within the content of an 'xsl:for-each' instruction or within an 'xsl:template' instruction without the 'match' attribute.";

		// Token: 0x04001516 RID: 5398
		public const string Xslt_Terminate = "Transform terminated: '{0}'.";

		// Token: 0x04001517 RID: 5399
		public const string Xslt_InvalidPattern = "'{0}' is an invalid XSLT pattern.";

		// Token: 0x04001518 RID: 5400
		public const string Xslt_EmptyTagRequired = "The tag '{0}' must be empty.";

		// Token: 0x04001519 RID: 5401
		public const string Xslt_WrongNamespace = "The wrong namespace was used for XSL. Use 'http://www.w3.org/1999/XSL/Transform'.";

		// Token: 0x0400151A RID: 5402
		public const string Xslt_InvalidFormat6 = "Format '{0}' has both  '*' and '_' which is invalid.";

		// Token: 0x0400151B RID: 5403
		public const string Xslt_InvalidFormat7 = "Format '{0}' has '{1}' which is invalid.";

		// Token: 0x0400151C RID: 5404
		public const string Xslt_ScriptMixLang = "Multiple scripting languages for the same namespace is not supported.";

		// Token: 0x0400151D RID: 5405
		public const string Xslt_ScriptInvalidLang = "The scripting language '{0}' is not supported.";

		// Token: 0x0400151E RID: 5406
		public const string Xslt_InvalidExtensionPermitions = "Extension object should not have wider permissions than the caller of the AddExtensionObject(). If wider permissions are needed, wrap the extension object.";

		// Token: 0x0400151F RID: 5407
		public const string Xslt_InvalidParamNamespace = "Parameter cannot belong to XSLT namespace.";

		// Token: 0x04001520 RID: 5408
		public const string Xslt_DuplicateParametr = "Duplicate parameter: '{0}'.";

		// Token: 0x04001521 RID: 5409
		public const string Xslt_VariableCntSel = "The '{0}' variable has both a select attribute of '{1}' and non-empty contents.";
	}
}
