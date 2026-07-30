using System;

namespace System.Windows.Forms
{
	// Token: 0x02000434 RID: 1076
	internal enum XRequest : byte
	{
		// Token: 0x040021E7 RID: 8679
		X_CreateWindow = 1,
		// Token: 0x040021E8 RID: 8680
		X_ChangeWindowAttributes,
		// Token: 0x040021E9 RID: 8681
		X_GetWindowAttributes,
		// Token: 0x040021EA RID: 8682
		X_DestroyWindow,
		// Token: 0x040021EB RID: 8683
		X_DestroySubwindows,
		// Token: 0x040021EC RID: 8684
		X_ChangeSaveSet,
		// Token: 0x040021ED RID: 8685
		X_ReparentWindow,
		// Token: 0x040021EE RID: 8686
		X_MapWindow,
		// Token: 0x040021EF RID: 8687
		X_MapSubwindows,
		// Token: 0x040021F0 RID: 8688
		X_UnmapWindow,
		// Token: 0x040021F1 RID: 8689
		X_UnmapSubwindows,
		// Token: 0x040021F2 RID: 8690
		X_ConfigureWindow,
		// Token: 0x040021F3 RID: 8691
		X_CirculateWindow,
		// Token: 0x040021F4 RID: 8692
		X_GetGeometry,
		// Token: 0x040021F5 RID: 8693
		X_QueryTree,
		// Token: 0x040021F6 RID: 8694
		X_InternAtom,
		// Token: 0x040021F7 RID: 8695
		X_GetAtomName,
		// Token: 0x040021F8 RID: 8696
		X_ChangeProperty,
		// Token: 0x040021F9 RID: 8697
		X_DeleteProperty,
		// Token: 0x040021FA RID: 8698
		X_GetProperty,
		// Token: 0x040021FB RID: 8699
		X_ListProperties,
		// Token: 0x040021FC RID: 8700
		X_SetSelectionOwner,
		// Token: 0x040021FD RID: 8701
		X_GetSelectionOwner,
		// Token: 0x040021FE RID: 8702
		X_ConvertSelection,
		// Token: 0x040021FF RID: 8703
		X_SendEvent,
		// Token: 0x04002200 RID: 8704
		X_GrabPointer,
		// Token: 0x04002201 RID: 8705
		X_UngrabPointer,
		// Token: 0x04002202 RID: 8706
		X_GrabButton,
		// Token: 0x04002203 RID: 8707
		X_UngrabButton,
		// Token: 0x04002204 RID: 8708
		X_ChangeActivePointerGrab,
		// Token: 0x04002205 RID: 8709
		X_GrabKeyboard,
		// Token: 0x04002206 RID: 8710
		X_UngrabKeyboard,
		// Token: 0x04002207 RID: 8711
		X_GrabKey,
		// Token: 0x04002208 RID: 8712
		X_UngrabKey,
		// Token: 0x04002209 RID: 8713
		X_AllowEvents,
		// Token: 0x0400220A RID: 8714
		X_GrabServer,
		// Token: 0x0400220B RID: 8715
		X_UngrabServer,
		// Token: 0x0400220C RID: 8716
		X_QueryPointer,
		// Token: 0x0400220D RID: 8717
		X_GetMotionEvents,
		// Token: 0x0400220E RID: 8718
		X_TranslateCoords,
		// Token: 0x0400220F RID: 8719
		X_WarpPointer,
		// Token: 0x04002210 RID: 8720
		X_SetInputFocus,
		// Token: 0x04002211 RID: 8721
		X_GetInputFocus,
		// Token: 0x04002212 RID: 8722
		X_QueryKeymap,
		// Token: 0x04002213 RID: 8723
		X_OpenFont,
		// Token: 0x04002214 RID: 8724
		X_CloseFont,
		// Token: 0x04002215 RID: 8725
		X_QueryFont,
		// Token: 0x04002216 RID: 8726
		X_QueryTextExtents,
		// Token: 0x04002217 RID: 8727
		X_ListFonts,
		// Token: 0x04002218 RID: 8728
		X_ListFontsWithInfo,
		// Token: 0x04002219 RID: 8729
		X_SetFontPath,
		// Token: 0x0400221A RID: 8730
		X_GetFontPath,
		// Token: 0x0400221B RID: 8731
		X_CreatePixmap,
		// Token: 0x0400221C RID: 8732
		X_FreePixmap,
		// Token: 0x0400221D RID: 8733
		X_CreateGC,
		// Token: 0x0400221E RID: 8734
		X_ChangeGC,
		// Token: 0x0400221F RID: 8735
		X_CopyGC,
		// Token: 0x04002220 RID: 8736
		X_SetDashes,
		// Token: 0x04002221 RID: 8737
		X_SetClipRectangles,
		// Token: 0x04002222 RID: 8738
		X_FreeGC,
		// Token: 0x04002223 RID: 8739
		X_ClearArea,
		// Token: 0x04002224 RID: 8740
		X_CopyArea,
		// Token: 0x04002225 RID: 8741
		X_CopyPlane,
		// Token: 0x04002226 RID: 8742
		X_PolyPoint,
		// Token: 0x04002227 RID: 8743
		X_PolyLine,
		// Token: 0x04002228 RID: 8744
		X_PolySegment,
		// Token: 0x04002229 RID: 8745
		X_PolyRectangle,
		// Token: 0x0400222A RID: 8746
		X_PolyArc,
		// Token: 0x0400222B RID: 8747
		X_FillPoly,
		// Token: 0x0400222C RID: 8748
		X_PolyFillRectangle,
		// Token: 0x0400222D RID: 8749
		X_PolyFillArc,
		// Token: 0x0400222E RID: 8750
		X_PutImage,
		// Token: 0x0400222F RID: 8751
		X_GetImage,
		// Token: 0x04002230 RID: 8752
		X_PolyText8,
		// Token: 0x04002231 RID: 8753
		X_PolyText16,
		// Token: 0x04002232 RID: 8754
		X_ImageText8,
		// Token: 0x04002233 RID: 8755
		X_ImageText16,
		// Token: 0x04002234 RID: 8756
		X_CreateColormap,
		// Token: 0x04002235 RID: 8757
		X_FreeColormap,
		// Token: 0x04002236 RID: 8758
		X_CopyColormapAndFree,
		// Token: 0x04002237 RID: 8759
		X_InstallColormap,
		// Token: 0x04002238 RID: 8760
		X_UninstallColormap,
		// Token: 0x04002239 RID: 8761
		X_ListInstalledColormaps,
		// Token: 0x0400223A RID: 8762
		X_AllocColor,
		// Token: 0x0400223B RID: 8763
		X_AllocNamedColor,
		// Token: 0x0400223C RID: 8764
		X_AllocColorCells,
		// Token: 0x0400223D RID: 8765
		X_AllocColorPlanes,
		// Token: 0x0400223E RID: 8766
		X_FreeColors,
		// Token: 0x0400223F RID: 8767
		X_StoreColors,
		// Token: 0x04002240 RID: 8768
		X_StoreNamedColor,
		// Token: 0x04002241 RID: 8769
		X_QueryColors,
		// Token: 0x04002242 RID: 8770
		X_LookupColor,
		// Token: 0x04002243 RID: 8771
		X_CreateCursor,
		// Token: 0x04002244 RID: 8772
		X_CreateGlyphCursor,
		// Token: 0x04002245 RID: 8773
		X_FreeCursor,
		// Token: 0x04002246 RID: 8774
		X_RecolorCursor,
		// Token: 0x04002247 RID: 8775
		X_QueryBestSize,
		// Token: 0x04002248 RID: 8776
		X_QueryExtension,
		// Token: 0x04002249 RID: 8777
		X_ListExtensions,
		// Token: 0x0400224A RID: 8778
		X_ChangeKeyboardMapping,
		// Token: 0x0400224B RID: 8779
		X_GetKeyboardMapping,
		// Token: 0x0400224C RID: 8780
		X_ChangeKeyboardControl,
		// Token: 0x0400224D RID: 8781
		X_GetKeyboardControl,
		// Token: 0x0400224E RID: 8782
		X_Bell,
		// Token: 0x0400224F RID: 8783
		X_ChangePointerControl,
		// Token: 0x04002250 RID: 8784
		X_GetPointerControl,
		// Token: 0x04002251 RID: 8785
		X_SetScreenSaver,
		// Token: 0x04002252 RID: 8786
		X_GetScreenSaver,
		// Token: 0x04002253 RID: 8787
		X_ChangeHosts,
		// Token: 0x04002254 RID: 8788
		X_ListHosts,
		// Token: 0x04002255 RID: 8789
		X_SetAccessControl,
		// Token: 0x04002256 RID: 8790
		X_SetCloseDownMode,
		// Token: 0x04002257 RID: 8791
		X_KillClient,
		// Token: 0x04002258 RID: 8792
		X_RotateProperties,
		// Token: 0x04002259 RID: 8793
		X_ForceScreenSaver,
		// Token: 0x0400225A RID: 8794
		X_SetPointerMapping,
		// Token: 0x0400225B RID: 8795
		X_GetPointerMapping,
		// Token: 0x0400225C RID: 8796
		X_SetModifierMapping,
		// Token: 0x0400225D RID: 8797
		X_GetModifierMapping,
		// Token: 0x0400225E RID: 8798
		X_NoOperation = 127
	}
}
