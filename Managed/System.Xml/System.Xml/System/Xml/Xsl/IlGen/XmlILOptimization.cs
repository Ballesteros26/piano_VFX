using System;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x0200066E RID: 1646
	internal enum XmlILOptimization
	{
		// Token: 0x04002A7D RID: 10877
		None,
		// Token: 0x04002A7E RID: 10878
		EliminateLiteralVariables,
		// Token: 0x04002A7F RID: 10879
		TailCall,
		// Token: 0x04002A80 RID: 10880
		AnnotateAncestor,
		// Token: 0x04002A81 RID: 10881
		AnnotateAncestorSelf,
		// Token: 0x04002A82 RID: 10882
		AnnotateAttribute,
		// Token: 0x04002A83 RID: 10883
		AnnotateAttrNmspLoop,
		// Token: 0x04002A84 RID: 10884
		AnnotateBarrier,
		// Token: 0x04002A85 RID: 10885
		AnnotateConstruction,
		// Token: 0x04002A86 RID: 10886
		AnnotateContent,
		// Token: 0x04002A87 RID: 10887
		AnnotateContentLoop,
		// Token: 0x04002A88 RID: 10888
		AnnotateDescendant,
		// Token: 0x04002A89 RID: 10889
		AnnotateDescendantLoop,
		// Token: 0x04002A8A RID: 10890
		AnnotateDescendantSelf,
		// Token: 0x04002A8B RID: 10891
		AnnotateDifference,
		// Token: 0x04002A8C RID: 10892
		AnnotateDod,
		// Token: 0x04002A8D RID: 10893
		AnnotateDodMerge,
		// Token: 0x04002A8E RID: 10894
		AnnotateDodReverse,
		// Token: 0x04002A8F RID: 10895
		AnnotateFilter,
		// Token: 0x04002A90 RID: 10896
		AnnotateFilterAttributeKind,
		// Token: 0x04002A91 RID: 10897
		AnnotateFilterContentKind,
		// Token: 0x04002A92 RID: 10898
		AnnotateFilterElements,
		// Token: 0x04002A93 RID: 10899
		AnnotateFollowingSibling,
		// Token: 0x04002A94 RID: 10900
		AnnotateIndex1,
		// Token: 0x04002A95 RID: 10901
		AnnotateIndex2,
		// Token: 0x04002A96 RID: 10902
		AnnotateIntersect,
		// Token: 0x04002A97 RID: 10903
		AnnotateInvoke,
		// Token: 0x04002A98 RID: 10904
		AnnotateJoinAndDod,
		// Token: 0x04002A99 RID: 10905
		AnnotateLet,
		// Token: 0x04002A9A RID: 10906
		AnnotateMaxLengthEq,
		// Token: 0x04002A9B RID: 10907
		AnnotateMaxLengthGe,
		// Token: 0x04002A9C RID: 10908
		AnnotateMaxLengthGt,
		// Token: 0x04002A9D RID: 10909
		AnnotateMaxLengthLe,
		// Token: 0x04002A9E RID: 10910
		AnnotateMaxLengthLt,
		// Token: 0x04002A9F RID: 10911
		AnnotateMaxLengthNe,
		// Token: 0x04002AA0 RID: 10912
		AnnotateMaxPositionEq,
		// Token: 0x04002AA1 RID: 10913
		AnnotateMaxPositionLe,
		// Token: 0x04002AA2 RID: 10914
		AnnotateMaxPositionLt,
		// Token: 0x04002AA3 RID: 10915
		AnnotateNamespace,
		// Token: 0x04002AA4 RID: 10916
		AnnotateNodeRange,
		// Token: 0x04002AA5 RID: 10917
		AnnotateParent,
		// Token: 0x04002AA6 RID: 10918
		AnnotatePositionalIterator,
		// Token: 0x04002AA7 RID: 10919
		AnnotatePreceding,
		// Token: 0x04002AA8 RID: 10920
		AnnotatePrecedingSibling,
		// Token: 0x04002AA9 RID: 10921
		AnnotateRoot,
		// Token: 0x04002AAA RID: 10922
		AnnotateRootLoop,
		// Token: 0x04002AAB RID: 10923
		AnnotateSingleTextRtf,
		// Token: 0x04002AAC RID: 10924
		AnnotateSingletonLoop,
		// Token: 0x04002AAD RID: 10925
		AnnotateTrackCallers,
		// Token: 0x04002AAE RID: 10926
		AnnotateUnion,
		// Token: 0x04002AAF RID: 10927
		AnnotateUnionContent,
		// Token: 0x04002AB0 RID: 10928
		AnnotateXPathFollowing,
		// Token: 0x04002AB1 RID: 10929
		AnnotateXPathPreceding,
		// Token: 0x04002AB2 RID: 10930
		CommuteDodFilter,
		// Token: 0x04002AB3 RID: 10931
		CommuteFilterLoop,
		// Token: 0x04002AB4 RID: 10932
		EliminateAdd,
		// Token: 0x04002AB5 RID: 10933
		EliminateAfter,
		// Token: 0x04002AB6 RID: 10934
		EliminateAnd,
		// Token: 0x04002AB7 RID: 10935
		EliminateAverage,
		// Token: 0x04002AB8 RID: 10936
		EliminateBefore,
		// Token: 0x04002AB9 RID: 10937
		EliminateConditional,
		// Token: 0x04002ABA RID: 10938
		EliminateDifference,
		// Token: 0x04002ABB RID: 10939
		EliminateDivide,
		// Token: 0x04002ABC RID: 10940
		EliminateDod,
		// Token: 0x04002ABD RID: 10941
		EliminateEq,
		// Token: 0x04002ABE RID: 10942
		EliminateFilter,
		// Token: 0x04002ABF RID: 10943
		EliminateGe,
		// Token: 0x04002AC0 RID: 10944
		EliminateGt,
		// Token: 0x04002AC1 RID: 10945
		EliminateIntersection,
		// Token: 0x04002AC2 RID: 10946
		EliminateIs,
		// Token: 0x04002AC3 RID: 10947
		EliminateIsEmpty,
		// Token: 0x04002AC4 RID: 10948
		EliminateIsType,
		// Token: 0x04002AC5 RID: 10949
		EliminateIterator,
		// Token: 0x04002AC6 RID: 10950
		EliminateIteratorUsedAtMostOnce,
		// Token: 0x04002AC7 RID: 10951
		EliminateLe,
		// Token: 0x04002AC8 RID: 10952
		EliminateLength,
		// Token: 0x04002AC9 RID: 10953
		EliminateLoop,
		// Token: 0x04002ACA RID: 10954
		EliminateLt,
		// Token: 0x04002ACB RID: 10955
		EliminateMaximum,
		// Token: 0x04002ACC RID: 10956
		EliminateMinimum,
		// Token: 0x04002ACD RID: 10957
		EliminateModulo,
		// Token: 0x04002ACE RID: 10958
		EliminateMultiply,
		// Token: 0x04002ACF RID: 10959
		EliminateNamespaceDecl,
		// Token: 0x04002AD0 RID: 10960
		EliminateNe,
		// Token: 0x04002AD1 RID: 10961
		EliminateNegate,
		// Token: 0x04002AD2 RID: 10962
		EliminateNop,
		// Token: 0x04002AD3 RID: 10963
		EliminateNot,
		// Token: 0x04002AD4 RID: 10964
		EliminateOr,
		// Token: 0x04002AD5 RID: 10965
		EliminatePositionOf,
		// Token: 0x04002AD6 RID: 10966
		EliminateReturnDod,
		// Token: 0x04002AD7 RID: 10967
		EliminateSequence,
		// Token: 0x04002AD8 RID: 10968
		EliminateSort,
		// Token: 0x04002AD9 RID: 10969
		EliminateStrConcat,
		// Token: 0x04002ADA RID: 10970
		EliminateStrConcatSingle,
		// Token: 0x04002ADB RID: 10971
		EliminateStrLength,
		// Token: 0x04002ADC RID: 10972
		EliminateSubtract,
		// Token: 0x04002ADD RID: 10973
		EliminateSum,
		// Token: 0x04002ADE RID: 10974
		EliminateTypeAssert,
		// Token: 0x04002ADF RID: 10975
		EliminateTypeAssertOptional,
		// Token: 0x04002AE0 RID: 10976
		EliminateUnion,
		// Token: 0x04002AE1 RID: 10977
		EliminateUnusedGlobals,
		// Token: 0x04002AE2 RID: 10978
		EliminateXsltConvert,
		// Token: 0x04002AE3 RID: 10979
		FoldConditionalNot,
		// Token: 0x04002AE4 RID: 10980
		FoldNamedDescendants,
		// Token: 0x04002AE5 RID: 10981
		FoldNone,
		// Token: 0x04002AE6 RID: 10982
		FoldXsltConvertLiteral,
		// Token: 0x04002AE7 RID: 10983
		IntroduceDod,
		// Token: 0x04002AE8 RID: 10984
		IntroducePrecedingDod,
		// Token: 0x04002AE9 RID: 10985
		NormalizeAddEq,
		// Token: 0x04002AEA RID: 10986
		NormalizeAddLiteral,
		// Token: 0x04002AEB RID: 10987
		NormalizeAttribute,
		// Token: 0x04002AEC RID: 10988
		NormalizeConditionalText,
		// Token: 0x04002AED RID: 10989
		NormalizeDifference,
		// Token: 0x04002AEE RID: 10990
		NormalizeEqLiteral,
		// Token: 0x04002AEF RID: 10991
		NormalizeGeLiteral,
		// Token: 0x04002AF0 RID: 10992
		NormalizeGtLiteral,
		// Token: 0x04002AF1 RID: 10993
		NormalizeIdEq,
		// Token: 0x04002AF2 RID: 10994
		NormalizeIdNe,
		// Token: 0x04002AF3 RID: 10995
		NormalizeIntersect,
		// Token: 0x04002AF4 RID: 10996
		NormalizeInvokeEmpty,
		// Token: 0x04002AF5 RID: 10997
		NormalizeLeLiteral,
		// Token: 0x04002AF6 RID: 10998
		NormalizeLengthGt,
		// Token: 0x04002AF7 RID: 10999
		NormalizeLengthNe,
		// Token: 0x04002AF8 RID: 11000
		NormalizeLoopConditional,
		// Token: 0x04002AF9 RID: 11001
		NormalizeLoopInvariant,
		// Token: 0x04002AFA RID: 11002
		NormalizeLoopLoop,
		// Token: 0x04002AFB RID: 11003
		NormalizeLoopText,
		// Token: 0x04002AFC RID: 11004
		NormalizeLtLiteral,
		// Token: 0x04002AFD RID: 11005
		NormalizeMuenchian,
		// Token: 0x04002AFE RID: 11006
		NormalizeMultiplyLiteral,
		// Token: 0x04002AFF RID: 11007
		NormalizeNeLiteral,
		// Token: 0x04002B00 RID: 11008
		NormalizeNestedSequences,
		// Token: 0x04002B01 RID: 11009
		NormalizeSingletonLet,
		// Token: 0x04002B02 RID: 11010
		NormalizeSortXsltConvert,
		// Token: 0x04002B03 RID: 11011
		NormalizeUnion,
		// Token: 0x04002B04 RID: 11012
		NormalizeXsltConvertEq,
		// Token: 0x04002B05 RID: 11013
		NormalizeXsltConvertGe,
		// Token: 0x04002B06 RID: 11014
		NormalizeXsltConvertGt,
		// Token: 0x04002B07 RID: 11015
		NormalizeXsltConvertLe,
		// Token: 0x04002B08 RID: 11016
		NormalizeXsltConvertLt,
		// Token: 0x04002B09 RID: 11017
		NormalizeXsltConvertNe,
		// Token: 0x04002B0A RID: 11018
		Last_
	}
}
