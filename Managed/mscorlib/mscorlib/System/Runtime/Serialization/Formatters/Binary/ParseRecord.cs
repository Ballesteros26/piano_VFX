using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200073D RID: 1853
	internal sealed class ParseRecord
	{
		// Token: 0x06004CF9 RID: 19705 RVA: 0x00002111 File Offset: 0x00000311
		internal ParseRecord()
		{
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x00115FD8 File Offset: 0x001141D8
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x0400292E RID: 10542
		internal static int parseRecordIdCount = 1;

		// Token: 0x0400292F RID: 10543
		internal int PRparseRecordId;

		// Token: 0x04002930 RID: 10544
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x04002931 RID: 10545
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x04002932 RID: 10546
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x04002933 RID: 10547
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04002934 RID: 10548
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04002935 RID: 10549
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04002936 RID: 10550
		internal string PRname;

		// Token: 0x04002937 RID: 10551
		internal string PRvalue;

		// Token: 0x04002938 RID: 10552
		internal object PRvarValue;

		// Token: 0x04002939 RID: 10553
		internal string PRkeyDt;

		// Token: 0x0400293A RID: 10554
		internal Type PRdtType;

		// Token: 0x0400293B RID: 10555
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x0400293C RID: 10556
		internal bool PRisVariant;

		// Token: 0x0400293D RID: 10557
		internal bool PRisEnum;

		// Token: 0x0400293E RID: 10558
		internal long PRobjectId;

		// Token: 0x0400293F RID: 10559
		internal long PRidRef;

		// Token: 0x04002940 RID: 10560
		internal string PRarrayElementTypeString;

		// Token: 0x04002941 RID: 10561
		internal Type PRarrayElementType;

		// Token: 0x04002942 RID: 10562
		internal bool PRisArrayVariant;

		// Token: 0x04002943 RID: 10563
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x04002944 RID: 10564
		internal int PRrank;

		// Token: 0x04002945 RID: 10565
		internal int[] PRlengthA;

		// Token: 0x04002946 RID: 10566
		internal int[] PRpositionA;

		// Token: 0x04002947 RID: 10567
		internal int[] PRlowerBoundA;

		// Token: 0x04002948 RID: 10568
		internal int[] PRupperBoundA;

		// Token: 0x04002949 RID: 10569
		internal int[] PRindexMap;

		// Token: 0x0400294A RID: 10570
		internal int PRmemberIndex;

		// Token: 0x0400294B RID: 10571
		internal int PRlinearlength;

		// Token: 0x0400294C RID: 10572
		internal int[] PRrectangularMap;

		// Token: 0x0400294D RID: 10573
		internal bool PRisLowerBound;

		// Token: 0x0400294E RID: 10574
		internal long PRtopId;

		// Token: 0x0400294F RID: 10575
		internal long PRheaderId;

		// Token: 0x04002950 RID: 10576
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x04002951 RID: 10577
		internal bool PRisValueTypeFixup;

		// Token: 0x04002952 RID: 10578
		internal object PRnewObj;

		// Token: 0x04002953 RID: 10579
		internal object[] PRobjectA;

		// Token: 0x04002954 RID: 10580
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x04002955 RID: 10581
		internal bool PRisRegistered;

		// Token: 0x04002956 RID: 10582
		internal object[] PRmemberData;

		// Token: 0x04002957 RID: 10583
		internal SerializationInfo PRsi;

		// Token: 0x04002958 RID: 10584
		internal int PRnullCount;
	}
}
