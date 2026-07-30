using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BB RID: 443
	public interface IStyle
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DB3 RID: 3507
		// (set) Token: 0x06000DB4 RID: 3508
		StyleEnum<Align> alignContent { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DB5 RID: 3509
		// (set) Token: 0x06000DB6 RID: 3510
		StyleEnum<Align> alignItems { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DB7 RID: 3511
		// (set) Token: 0x06000DB8 RID: 3512
		StyleEnum<Align> alignSelf { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DB9 RID: 3513
		// (set) Token: 0x06000DBA RID: 3514
		StyleColor backgroundColor { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DBB RID: 3515
		// (set) Token: 0x06000DBC RID: 3516
		StyleBackground backgroundImage { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DBD RID: 3517
		// (set) Token: 0x06000DBE RID: 3518
		StyleColor borderBottomColor { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DBF RID: 3519
		// (set) Token: 0x06000DC0 RID: 3520
		StyleLength borderBottomLeftRadius { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DC1 RID: 3521
		// (set) Token: 0x06000DC2 RID: 3522
		StyleLength borderBottomRightRadius { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DC3 RID: 3523
		// (set) Token: 0x06000DC4 RID: 3524
		StyleFloat borderBottomWidth { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DC5 RID: 3525
		// (set) Token: 0x06000DC6 RID: 3526
		StyleColor borderLeftColor { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DC7 RID: 3527
		// (set) Token: 0x06000DC8 RID: 3528
		StyleFloat borderLeftWidth { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DC9 RID: 3529
		// (set) Token: 0x06000DCA RID: 3530
		StyleColor borderRightColor { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DCB RID: 3531
		// (set) Token: 0x06000DCC RID: 3532
		StyleFloat borderRightWidth { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DCD RID: 3533
		// (set) Token: 0x06000DCE RID: 3534
		StyleColor borderTopColor { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DCF RID: 3535
		// (set) Token: 0x06000DD0 RID: 3536
		StyleLength borderTopLeftRadius { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DD1 RID: 3537
		// (set) Token: 0x06000DD2 RID: 3538
		StyleLength borderTopRightRadius { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DD3 RID: 3539
		// (set) Token: 0x06000DD4 RID: 3540
		StyleFloat borderTopWidth { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000DD5 RID: 3541
		// (set) Token: 0x06000DD6 RID: 3542
		StyleLength bottom { get; set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000DD7 RID: 3543
		// (set) Token: 0x06000DD8 RID: 3544
		StyleColor color { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000DD9 RID: 3545
		// (set) Token: 0x06000DDA RID: 3546
		StyleCursor cursor { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000DDB RID: 3547
		// (set) Token: 0x06000DDC RID: 3548
		StyleEnum<DisplayStyle> display { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000DDD RID: 3549
		// (set) Token: 0x06000DDE RID: 3550
		StyleLength flexBasis { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000DDF RID: 3551
		// (set) Token: 0x06000DE0 RID: 3552
		StyleEnum<FlexDirection> flexDirection { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000DE1 RID: 3553
		// (set) Token: 0x06000DE2 RID: 3554
		StyleFloat flexGrow { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000DE3 RID: 3555
		// (set) Token: 0x06000DE4 RID: 3556
		StyleFloat flexShrink { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000DE5 RID: 3557
		// (set) Token: 0x06000DE6 RID: 3558
		StyleEnum<Wrap> flexWrap { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000DE7 RID: 3559
		// (set) Token: 0x06000DE8 RID: 3560
		StyleLength fontSize { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000DE9 RID: 3561
		// (set) Token: 0x06000DEA RID: 3562
		StyleLength height { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000DEB RID: 3563
		// (set) Token: 0x06000DEC RID: 3564
		StyleEnum<Justify> justifyContent { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000DED RID: 3565
		// (set) Token: 0x06000DEE RID: 3566
		StyleLength left { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000DEF RID: 3567
		// (set) Token: 0x06000DF0 RID: 3568
		StyleLength marginBottom { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000DF1 RID: 3569
		// (set) Token: 0x06000DF2 RID: 3570
		StyleLength marginLeft { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000DF3 RID: 3571
		// (set) Token: 0x06000DF4 RID: 3572
		StyleLength marginRight { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000DF5 RID: 3573
		// (set) Token: 0x06000DF6 RID: 3574
		StyleLength marginTop { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000DF7 RID: 3575
		// (set) Token: 0x06000DF8 RID: 3576
		StyleLength maxHeight { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000DF9 RID: 3577
		// (set) Token: 0x06000DFA RID: 3578
		StyleLength maxWidth { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000DFB RID: 3579
		// (set) Token: 0x06000DFC RID: 3580
		StyleLength minHeight { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000DFD RID: 3581
		// (set) Token: 0x06000DFE RID: 3582
		StyleLength minWidth { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000DFF RID: 3583
		// (set) Token: 0x06000E00 RID: 3584
		StyleFloat opacity { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E01 RID: 3585
		// (set) Token: 0x06000E02 RID: 3586
		StyleEnum<Overflow> overflow { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E03 RID: 3587
		// (set) Token: 0x06000E04 RID: 3588
		StyleLength paddingBottom { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E05 RID: 3589
		// (set) Token: 0x06000E06 RID: 3590
		StyleLength paddingLeft { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E07 RID: 3591
		// (set) Token: 0x06000E08 RID: 3592
		StyleLength paddingRight { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E09 RID: 3593
		// (set) Token: 0x06000E0A RID: 3594
		StyleLength paddingTop { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E0B RID: 3595
		// (set) Token: 0x06000E0C RID: 3596
		StyleEnum<Position> position { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E0D RID: 3597
		// (set) Token: 0x06000E0E RID: 3598
		StyleLength right { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E0F RID: 3599
		// (set) Token: 0x06000E10 RID: 3600
		StyleEnum<TextOverflow> textOverflow { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000E11 RID: 3601
		// (set) Token: 0x06000E12 RID: 3602
		StyleLength top { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000E13 RID: 3603
		// (set) Token: 0x06000E14 RID: 3604
		StyleColor unityBackgroundImageTintColor { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000E15 RID: 3605
		// (set) Token: 0x06000E16 RID: 3606
		StyleEnum<ScaleMode> unityBackgroundScaleMode { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000E17 RID: 3607
		// (set) Token: 0x06000E18 RID: 3608
		StyleFont unityFont { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000E19 RID: 3609
		// (set) Token: 0x06000E1A RID: 3610
		StyleEnum<FontStyle> unityFontStyleAndWeight { get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000E1B RID: 3611
		// (set) Token: 0x06000E1C RID: 3612
		StyleEnum<OverflowClipBox> unityOverflowClipBox { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000E1D RID: 3613
		// (set) Token: 0x06000E1E RID: 3614
		StyleInt unitySliceBottom { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000E1F RID: 3615
		// (set) Token: 0x06000E20 RID: 3616
		StyleInt unitySliceLeft { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000E21 RID: 3617
		// (set) Token: 0x06000E22 RID: 3618
		StyleInt unitySliceRight { get; set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000E23 RID: 3619
		// (set) Token: 0x06000E24 RID: 3620
		StyleInt unitySliceTop { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000E25 RID: 3621
		// (set) Token: 0x06000E26 RID: 3622
		StyleEnum<TextAnchor> unityTextAlign { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000E27 RID: 3623
		// (set) Token: 0x06000E28 RID: 3624
		StyleEnum<TextOverflowPosition> unityTextOverflowPosition { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000E29 RID: 3625
		// (set) Token: 0x06000E2A RID: 3626
		StyleEnum<Visibility> visibility { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000E2B RID: 3627
		// (set) Token: 0x06000E2C RID: 3628
		StyleEnum<WhiteSpace> whiteSpace { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000E2D RID: 3629
		// (set) Token: 0x06000E2E RID: 3630
		StyleLength width { get; set; }
	}
}
