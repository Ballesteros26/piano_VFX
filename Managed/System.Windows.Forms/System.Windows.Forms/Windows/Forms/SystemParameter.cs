using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the system parameter type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002F3 RID: 755
	[ComVisible(true)]
	public enum SystemParameter
	{
		/// <summary>Identifies the drop shadow effect. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsDropShadowEnabled" /> property to determine if this effect is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETDROPSHADOW and SPI_SETDROPSHADOW.</summary>
		// Token: 0x04001813 RID: 6163
		DropShadow,
		/// <summary>Identifies the flat menu appearance feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsFlatMenuEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETFLATMENU and SPI_SETFLATMENU.</summary>
		// Token: 0x04001814 RID: 6164
		FlatMenu,
		/// <summary>Identifies the contrast value used in ClearType font smoothing. Use the <see cref="P:System.Windows.Forms.SystemInformation.FontSmoothingContrast" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETFONTSMOOTHINGCONTRAST and SPI_SETFONTSMOOTHINGCONTRAST.</summary>
		// Token: 0x04001815 RID: 6165
		FontSmoothingContrastMetric,
		/// <summary>Identifies the type of font smoothing. Use the <see cref="P:System.Windows.Forms.SystemInformation.FontSmoothingType" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETFONTSMOOTHINGTYPE and SPI_SETFONTSMOOTHINGTYPE.</summary>
		// Token: 0x04001816 RID: 6166
		FontSmoothingTypeMetric,
		/// <summary>Identifies the menu fade animation feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsMenuFadeEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETMENUFADE and SPI_SETMENUFADE.</summary>
		// Token: 0x04001817 RID: 6167
		MenuFadeEnabled,
		/// <summary>Identifies the selection fade effect. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsSelectionFadeEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETSELECTIONFADE and SPI_SETSELECTIONFADE.</summary>
		// Token: 0x04001818 RID: 6168
		SelectionFade,
		/// <summary>Identifies the ToolTip animation feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.IsToolTipAnimationEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETTOOLTIPANIMATION and SPI_SETTOOLTIPANIMATION.</summary>
		// Token: 0x04001819 RID: 6169
		ToolTipAnimationMetric,
		/// <summary>Identifies the user interface (UI) effects feature. Use the <see cref="P:System.Windows.Forms.SystemInformation.UIEffectsEnabled" /> property to determine if this feature is enabled. The corresponding Platform SDK system-wide parameters are SPI_GETUIEFFECTS and SPI_SETUIEFFECTS.</summary>
		// Token: 0x0400181A RID: 6170
		UIEffects,
		/// <summary>Identifies the caret width, in pixels, for edit controls. Use the <see cref="P:System.Windows.Forms.SystemInformation.CaretWidth" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameters are SPI_GETCARETWIDTH and SPI_SETCARETWIDTH.</summary>
		// Token: 0x0400181B RID: 6171
		CaretWidthMetric,
		/// <summary>Identifies the thickness of the top and bottom edges of the system focus rectangle. Use the <see cref="P:System.Windows.Forms.SystemInformation.VerticalFocusThickness" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameter is SM_CYFOCUSBORDER.</summary>
		// Token: 0x0400181C RID: 6172
		VerticalFocusThicknessMetric,
		/// <summary>Identifies the thickness of the left and right edges of the system focus rectangle. Use the <see cref="P:System.Windows.Forms.SystemInformation.HorizontalFocusThickness" /> property to access this system-wide parameter. The corresponding Platform SDK system-wide parameter is SM_CXFOCUSBORDER.</summary>
		// Token: 0x0400181D RID: 6173
		HorizontalFocusThicknessMetric
	}
}
