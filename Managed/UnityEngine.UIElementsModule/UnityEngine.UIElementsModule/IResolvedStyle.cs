using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BA RID: 442
	public interface IResolvedStyle
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000D79 RID: 3449
		Align alignContent { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000D7A RID: 3450
		Align alignItems { get; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000D7B RID: 3451
		Align alignSelf { get; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D7C RID: 3452
		Color backgroundColor { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D7D RID: 3453
		Color borderBottomColor { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D7E RID: 3454
		float borderBottomLeftRadius { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D7F RID: 3455
		float borderBottomRightRadius { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D80 RID: 3456
		float borderBottomWidth { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D81 RID: 3457
		Color borderLeftColor { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D82 RID: 3458
		float borderLeftWidth { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D83 RID: 3459
		Color borderRightColor { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D84 RID: 3460
		float borderRightWidth { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D85 RID: 3461
		Color borderTopColor { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D86 RID: 3462
		float borderTopLeftRadius { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D87 RID: 3463
		float borderTopRightRadius { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D88 RID: 3464
		float borderTopWidth { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000D89 RID: 3465
		float bottom { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D8A RID: 3466
		Color color { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D8B RID: 3467
		DisplayStyle display { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D8C RID: 3468
		StyleFloat flexBasis { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D8D RID: 3469
		FlexDirection flexDirection { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D8E RID: 3470
		float flexGrow { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D8F RID: 3471
		float flexShrink { get; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D90 RID: 3472
		Wrap flexWrap { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D91 RID: 3473
		float fontSize { get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D92 RID: 3474
		float height { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D93 RID: 3475
		Justify justifyContent { get; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D94 RID: 3476
		float left { get; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D95 RID: 3477
		float marginBottom { get; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D96 RID: 3478
		float marginLeft { get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D97 RID: 3479
		float marginRight { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000D98 RID: 3480
		float marginTop { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D99 RID: 3481
		StyleFloat maxHeight { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D9A RID: 3482
		StyleFloat maxWidth { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D9B RID: 3483
		StyleFloat minHeight { get; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D9C RID: 3484
		StyleFloat minWidth { get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D9D RID: 3485
		float opacity { get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D9E RID: 3486
		float paddingBottom { get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D9F RID: 3487
		float paddingLeft { get; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000DA0 RID: 3488
		float paddingRight { get; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000DA1 RID: 3489
		float paddingTop { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000DA2 RID: 3490
		Position position { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000DA3 RID: 3491
		float right { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000DA4 RID: 3492
		TextOverflow textOverflow { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000DA5 RID: 3493
		float top { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000DA6 RID: 3494
		Color unityBackgroundImageTintColor { get; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000DA7 RID: 3495
		ScaleMode unityBackgroundScaleMode { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000DA8 RID: 3496
		Font unityFont { get; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000DA9 RID: 3497
		FontStyle unityFontStyleAndWeight { get; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000DAA RID: 3498
		int unitySliceBottom { get; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000DAB RID: 3499
		int unitySliceLeft { get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000DAC RID: 3500
		int unitySliceRight { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000DAD RID: 3501
		int unitySliceTop { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000DAE RID: 3502
		TextAnchor unityTextAlign { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000DAF RID: 3503
		TextOverflowPosition unityTextOverflowPosition { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000DB0 RID: 3504
		Visibility visibility { get; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DB1 RID: 3505
		WhiteSpace whiteSpace { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DB2 RID: 3506
		float width { get; }
	}
}
