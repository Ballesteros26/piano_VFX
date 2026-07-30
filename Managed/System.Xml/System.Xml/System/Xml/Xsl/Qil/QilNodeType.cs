using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x0200063A RID: 1594
	internal enum QilNodeType
	{
		// Token: 0x04002852 RID: 10322
		QilExpression,
		// Token: 0x04002853 RID: 10323
		FunctionList,
		// Token: 0x04002854 RID: 10324
		GlobalVariableList,
		// Token: 0x04002855 RID: 10325
		GlobalParameterList,
		// Token: 0x04002856 RID: 10326
		ActualParameterList,
		// Token: 0x04002857 RID: 10327
		FormalParameterList,
		// Token: 0x04002858 RID: 10328
		SortKeyList,
		// Token: 0x04002859 RID: 10329
		BranchList,
		// Token: 0x0400285A RID: 10330
		OptimizeBarrier,
		// Token: 0x0400285B RID: 10331
		Unknown,
		// Token: 0x0400285C RID: 10332
		DataSource,
		// Token: 0x0400285D RID: 10333
		Nop,
		// Token: 0x0400285E RID: 10334
		Error,
		// Token: 0x0400285F RID: 10335
		Warning,
		// Token: 0x04002860 RID: 10336
		For,
		// Token: 0x04002861 RID: 10337
		Let,
		// Token: 0x04002862 RID: 10338
		Parameter,
		// Token: 0x04002863 RID: 10339
		PositionOf,
		// Token: 0x04002864 RID: 10340
		True,
		// Token: 0x04002865 RID: 10341
		False,
		// Token: 0x04002866 RID: 10342
		LiteralString,
		// Token: 0x04002867 RID: 10343
		LiteralInt32,
		// Token: 0x04002868 RID: 10344
		LiteralInt64,
		// Token: 0x04002869 RID: 10345
		LiteralDouble,
		// Token: 0x0400286A RID: 10346
		LiteralDecimal,
		// Token: 0x0400286B RID: 10347
		LiteralQName,
		// Token: 0x0400286C RID: 10348
		LiteralType,
		// Token: 0x0400286D RID: 10349
		LiteralObject,
		// Token: 0x0400286E RID: 10350
		And,
		// Token: 0x0400286F RID: 10351
		Or,
		// Token: 0x04002870 RID: 10352
		Not,
		// Token: 0x04002871 RID: 10353
		Conditional,
		// Token: 0x04002872 RID: 10354
		Choice,
		// Token: 0x04002873 RID: 10355
		Length,
		// Token: 0x04002874 RID: 10356
		Sequence,
		// Token: 0x04002875 RID: 10357
		Union,
		// Token: 0x04002876 RID: 10358
		Intersection,
		// Token: 0x04002877 RID: 10359
		Difference,
		// Token: 0x04002878 RID: 10360
		Average,
		// Token: 0x04002879 RID: 10361
		Sum,
		// Token: 0x0400287A RID: 10362
		Minimum,
		// Token: 0x0400287B RID: 10363
		Maximum,
		// Token: 0x0400287C RID: 10364
		Negate,
		// Token: 0x0400287D RID: 10365
		Add,
		// Token: 0x0400287E RID: 10366
		Subtract,
		// Token: 0x0400287F RID: 10367
		Multiply,
		// Token: 0x04002880 RID: 10368
		Divide,
		// Token: 0x04002881 RID: 10369
		Modulo,
		// Token: 0x04002882 RID: 10370
		StrLength,
		// Token: 0x04002883 RID: 10371
		StrConcat,
		// Token: 0x04002884 RID: 10372
		StrParseQName,
		// Token: 0x04002885 RID: 10373
		Ne,
		// Token: 0x04002886 RID: 10374
		Eq,
		// Token: 0x04002887 RID: 10375
		Gt,
		// Token: 0x04002888 RID: 10376
		Ge,
		// Token: 0x04002889 RID: 10377
		Lt,
		// Token: 0x0400288A RID: 10378
		Le,
		// Token: 0x0400288B RID: 10379
		Is,
		// Token: 0x0400288C RID: 10380
		After,
		// Token: 0x0400288D RID: 10381
		Before,
		// Token: 0x0400288E RID: 10382
		Loop,
		// Token: 0x0400288F RID: 10383
		Filter,
		// Token: 0x04002890 RID: 10384
		Sort,
		// Token: 0x04002891 RID: 10385
		SortKey,
		// Token: 0x04002892 RID: 10386
		DocOrderDistinct,
		// Token: 0x04002893 RID: 10387
		Function,
		// Token: 0x04002894 RID: 10388
		Invoke,
		// Token: 0x04002895 RID: 10389
		Content,
		// Token: 0x04002896 RID: 10390
		Attribute,
		// Token: 0x04002897 RID: 10391
		Parent,
		// Token: 0x04002898 RID: 10392
		Root,
		// Token: 0x04002899 RID: 10393
		XmlContext,
		// Token: 0x0400289A RID: 10394
		Descendant,
		// Token: 0x0400289B RID: 10395
		DescendantOrSelf,
		// Token: 0x0400289C RID: 10396
		Ancestor,
		// Token: 0x0400289D RID: 10397
		AncestorOrSelf,
		// Token: 0x0400289E RID: 10398
		Preceding,
		// Token: 0x0400289F RID: 10399
		FollowingSibling,
		// Token: 0x040028A0 RID: 10400
		PrecedingSibling,
		// Token: 0x040028A1 RID: 10401
		NodeRange,
		// Token: 0x040028A2 RID: 10402
		Deref,
		// Token: 0x040028A3 RID: 10403
		ElementCtor,
		// Token: 0x040028A4 RID: 10404
		AttributeCtor,
		// Token: 0x040028A5 RID: 10405
		CommentCtor,
		// Token: 0x040028A6 RID: 10406
		PICtor,
		// Token: 0x040028A7 RID: 10407
		TextCtor,
		// Token: 0x040028A8 RID: 10408
		RawTextCtor,
		// Token: 0x040028A9 RID: 10409
		DocumentCtor,
		// Token: 0x040028AA RID: 10410
		NamespaceDecl,
		// Token: 0x040028AB RID: 10411
		RtfCtor,
		// Token: 0x040028AC RID: 10412
		NameOf,
		// Token: 0x040028AD RID: 10413
		LocalNameOf,
		// Token: 0x040028AE RID: 10414
		NamespaceUriOf,
		// Token: 0x040028AF RID: 10415
		PrefixOf,
		// Token: 0x040028B0 RID: 10416
		TypeAssert,
		// Token: 0x040028B1 RID: 10417
		IsType,
		// Token: 0x040028B2 RID: 10418
		IsEmpty,
		// Token: 0x040028B3 RID: 10419
		XPathNodeValue,
		// Token: 0x040028B4 RID: 10420
		XPathFollowing,
		// Token: 0x040028B5 RID: 10421
		XPathPreceding,
		// Token: 0x040028B6 RID: 10422
		XPathNamespace,
		// Token: 0x040028B7 RID: 10423
		XsltGenerateId,
		// Token: 0x040028B8 RID: 10424
		XsltInvokeLateBound,
		// Token: 0x040028B9 RID: 10425
		XsltInvokeEarlyBound,
		// Token: 0x040028BA RID: 10426
		XsltCopy,
		// Token: 0x040028BB RID: 10427
		XsltCopyOf,
		// Token: 0x040028BC RID: 10428
		XsltConvert
	}
}
