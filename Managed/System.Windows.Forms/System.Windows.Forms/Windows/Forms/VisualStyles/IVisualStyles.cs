using System;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200051F RID: 1311
	internal interface IVisualStyles
	{
		// Token: 0x06004D7A RID: 19834
		int UxThemeCloseThemeData(IntPtr hTheme);

		// Token: 0x06004D7B RID: 19835
		int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds);

		// Token: 0x06004D7C RID: 19836
		int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle);

		// Token: 0x06004D7D RID: 19837
		int UxThemeDrawThemeEdge(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects, out Rectangle result);

		// Token: 0x06004D7E RID: 19838
		int UxThemeDrawThemeParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl);

		// Token: 0x06004D7F RID: 19839
		int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds);

		// Token: 0x06004D80 RID: 19840
		int UxThemeGetThemeBackgroundContentRect(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Rectangle result);

		// Token: 0x06004D81 RID: 19841
		int UxThemeGetThemeBackgroundExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle contentBounds, out Rectangle result);

		// Token: 0x06004D82 RID: 19842
		int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result);

		// Token: 0x06004D83 RID: 19843
		int UxThemeGetThemeBool(IntPtr hTheme, int iPartId, int iStateId, BooleanProperty prop, out bool result);

		// Token: 0x06004D84 RID: 19844
		int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result);

		// Token: 0x06004D85 RID: 19845
		int UxThemeGetThemeEnumValue(IntPtr hTheme, int iPartId, int iStateId, EnumProperty prop, out int result);

		// Token: 0x06004D86 RID: 19846
		int UxThemeGetThemeFilename(IntPtr hTheme, int iPartId, int iStateId, FilenameProperty prop, out string result);

		// Token: 0x06004D87 RID: 19847
		int UxThemeGetThemeInt(IntPtr hTheme, int iPartId, int iStateId, IntegerProperty prop, out int result);

		// Token: 0x06004D88 RID: 19848
		int UxThemeGetThemeMargins(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, MarginProperty prop, out Padding result);

		// Token: 0x06004D89 RID: 19849
		int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, ThemeSizeType type, out Size result);

		// Token: 0x06004D8A RID: 19850
		int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result);

		// Token: 0x06004D8B RID: 19851
		int UxThemeGetThemePosition(IntPtr hTheme, int iPartId, int iStateId, PointProperty prop, out Point result);

		// Token: 0x06004D8C RID: 19852
		int UxThemeGetThemeString(IntPtr hTheme, int iPartId, int iStateId, StringProperty prop, out string result);

		// Token: 0x06004D8D RID: 19853
		int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result);

		// Token: 0x06004D8E RID: 19854
		int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, out Rectangle result);

		// Token: 0x06004D8F RID: 19855
		int UxThemeGetThemeTextMetrics(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, out TextMetrics result);

		// Token: 0x06004D90 RID: 19856
		int UxThemeHitTestThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, HitTestOptions options, Rectangle backgroundRectangle, IntPtr hrgn, Point pt, out HitTestCode result);

		// Token: 0x06004D91 RID: 19857
		bool UxThemeIsAppThemed();

		// Token: 0x06004D92 RID: 19858
		bool UxThemeIsThemeActive();

		// Token: 0x06004D93 RID: 19859
		bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06004D94 RID: 19860
		bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId);

		// Token: 0x06004D95 RID: 19861
		IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList);

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004D96 RID: 19862
		string VisualStyleInformationAuthor { get; }

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004D97 RID: 19863
		string VisualStyleInformationColorScheme { get; }

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004D98 RID: 19864
		string VisualStyleInformationCompany { get; }

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004D99 RID: 19865
		Color VisualStyleInformationControlHighlightHot { get; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004D9A RID: 19866
		string VisualStyleInformationCopyright { get; }

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004D9B RID: 19867
		string VisualStyleInformationDescription { get; }

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004D9C RID: 19868
		string VisualStyleInformationDisplayName { get; }

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004D9D RID: 19869
		string VisualStyleInformationFileName { get; }

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004D9E RID: 19870
		bool VisualStyleInformationIsSupportedByOS { get; }

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004D9F RID: 19871
		int VisualStyleInformationMinimumColorDepth { get; }

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06004DA0 RID: 19872
		string VisualStyleInformationSize { get; }

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004DA1 RID: 19873
		bool VisualStyleInformationSupportsFlatMenus { get; }

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06004DA2 RID: 19874
		Color VisualStyleInformationTextControlBorder { get; }

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004DA3 RID: 19875
		string VisualStyleInformationUrl { get; }

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004DA4 RID: 19876
		string VisualStyleInformationVersion { get; }

		// Token: 0x06004DA5 RID: 19877
		void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea);
	}
}
