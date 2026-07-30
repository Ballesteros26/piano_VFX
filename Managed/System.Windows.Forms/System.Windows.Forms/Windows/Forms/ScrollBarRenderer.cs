using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a scroll bar control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002C9 RID: 713
	public sealed class ScrollBarRenderer
	{
		// Token: 0x06002F7C RID: 12156 RVA: 0x000B7638 File Offset: 0x000B5838
		private ScrollBarRenderer()
		{
		}

		/// <summary>Draws a scroll arrow with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll arrow.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll arrow.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarArrowButtonState" /> values that specifies the visual state of the scroll arrow.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F7D RID: 12157 RVA: 0x000B7640 File Offset: 0x000B5840
		public static void DrawArrowButton(Graphics g, Rectangle bounds, ScrollBarArrowButtonState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarArrowButtonState.UpNormal:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpNormal);
				goto IL_015F;
			case ScrollBarArrowButtonState.UpHot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpHot);
				goto IL_015F;
			case ScrollBarArrowButtonState.UpPressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpPressed);
				goto IL_015F;
			case ScrollBarArrowButtonState.UpDisabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.UpDisabled);
				goto IL_015F;
			case ScrollBarArrowButtonState.DownHot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownHot);
				goto IL_015F;
			case ScrollBarArrowButtonState.DownPressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownPressed);
				goto IL_015F;
			case ScrollBarArrowButtonState.DownDisabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownDisabled);
				goto IL_015F;
			case ScrollBarArrowButtonState.LeftNormal:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftNormal);
				goto IL_015F;
			case ScrollBarArrowButtonState.LeftHot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftHot);
				goto IL_015F;
			case ScrollBarArrowButtonState.LeftPressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftPressed);
				goto IL_015F;
			case ScrollBarArrowButtonState.LeftDisabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.LeftDisabled);
				goto IL_015F;
			case ScrollBarArrowButtonState.RightNormal:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightNormal);
				goto IL_015F;
			case ScrollBarArrowButtonState.RightHot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightHot);
				goto IL_015F;
			case ScrollBarArrowButtonState.RightPressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightPressed);
				goto IL_015F;
			case ScrollBarArrowButtonState.RightDisabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.RightDisabled);
				goto IL_015F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ArrowButton.DownNormal);
			IL_015F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F7E RID: 12158 RVA: 0x000B77B4 File Offset: 0x000B59B4
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a grip on a horizontal scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F7F RID: 12159 RVA: 0x000B7838 File Offset: 0x000B5A38
		public static void DrawHorizontalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperHorizontal.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F80 RID: 12160 RVA: 0x000B7868 File Offset: 0x000B5A68
		public static void DrawLeftHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F81 RID: 12161 RVA: 0x000B78EC File Offset: 0x000B5AEC
		public static void DrawLowerVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.LowerTrackVertical.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F82 RID: 12162 RVA: 0x000B7970 File Offset: 0x000B5B70
		public static void DrawRightHorizontalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a scroll bar sizing handle with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the sizing handle.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the sizing handle.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarSizeBoxState" /> values that specifies the visual state of the sizing handle.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F83 RID: 12163 RVA: 0x000B79F4 File Offset: 0x000B5BF4
		public static void DrawSizeBox(Graphics g, Rectangle bounds, ScrollBarSizeBoxState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			if (state != ScrollBarSizeBoxState.RightAlign)
			{
				if (state != ScrollBarSizeBoxState.LeftAlign)
				{
				}
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign);
			}
			else
			{
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.RightAlign);
			}
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll bar track with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll bar track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll bar track.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll bar track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F84 RID: 12164 RVA: 0x000B7A50 File Offset: 0x000B5C50
		public static void DrawUpperVerticalTrack(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.UpperTrackVertical.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F85 RID: 12165 RVA: 0x000B7AD4 File Offset: 0x000B5CD4
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case ScrollBarState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot);
				goto IL_006F;
			case ScrollBarState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed);
				goto IL_006F;
			case ScrollBarState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Disabled);
				goto IL_006F;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal);
			IL_006F:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a grip on a vertical scroll box (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the scroll box grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the scroll box grip.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F86 RID: 12166 RVA: 0x000B7B58 File Offset: 0x000B5D58
		public static void DrawVerticalThumbGrip(Graphics g, Rectangle bounds, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Returns the size of the sizing handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the sizing handle.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the sizing handle.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F87 RID: 12167 RVA: 0x000B7B88 File Offset: 0x000B5D88
		public static Size GetSizeBoxSize(Graphics g, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.SizeBox.LeftAlign);
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Returns the size of the scroll box grip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the scroll box grip.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.ScrollBarState" /> values that specifies the visual state of the scroll box grip.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002F88 RID: 12168 RVA: 0x000B7BB8 File Offset: 0x000B5DB8
		public static Size GetThumbGripSize(Graphics g, ScrollBarState state)
		{
			if (!ScrollBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ScrollBar.GripperVertical.Normal);
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBarRenderer" /> class can be used to draw a scroll bar with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client areas of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000B7BE8 File Offset: 0x000B5DE8
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}
	}
}
