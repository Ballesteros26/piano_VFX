using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a track bar control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000389 RID: 905
	public sealed class TrackBarRenderer
	{
		// Token: 0x060041D1 RID: 16849 RVA: 0x00103A70 File Offset: 0x00101C70
		private TrackBarRenderer()
		{
		}

		/// <summary>Draws a downward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D2 RID: 16850 RVA: 0x00103A78 File Offset: 0x00101C78
		public static void DrawBottomPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a horizontal track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D3 RID: 16851 RVA: 0x00103B00 File Offset: 0x00101D00
		public static void DrawHorizontalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Thumb.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws the specified number of horizontal track bar ticks with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
		/// <param name="numTicks">The number of ticks to draw.</param>
		/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060041D4 RID: 16852 RVA: 0x00103B88 File Offset: 0x00101D88
		public static void DrawHorizontalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			if (bounds.Height <= 0 || bounds.Width <= 0 || numTicks <= 0)
			{
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Ticks.Normal);
			double num = (double)bounds.Left;
			double num2 = (double)(bounds.Width - 2) / (double)(numTicks - 1);
			for (int i = 0; i < numTicks; i++)
			{
				visualStyleRenderer.DrawEdge(g, new Rectangle((int)Math.Round(num), bounds.Top, 5, bounds.Height), Edges.Left, edgeStyle, EdgeEffects.None);
				num += num2;
			}
		}

		/// <summary>Draws the track for a horizontal track bar with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D5 RID: 16853 RVA: 0x00103C28 File Offset: 0x00101E28
		public static void DrawHorizontalTrack(Graphics g, Rectangle bounds)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Track.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a left-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D6 RID: 16854 RVA: 0x00103C58 File Offset: 0x00101E58
		public static void DrawLeftPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a right-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D7 RID: 16855 RVA: 0x00103CE0 File Offset: 0x00101EE0
		public static void DrawRightPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws an upward-pointing track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D8 RID: 16856 RVA: 0x00103D68 File Offset: 0x00101F68
		public static void DrawTopPointingThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a vertical track bar slider (also known as the thumb) with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track bar slider.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track bar slider.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041D9 RID: 16857 RVA: 0x00103DF0 File Offset: 0x00101FF0
		public static void DrawVerticalThumb(Graphics g, Rectangle bounds, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbVertical.Normal);
			IL_0073:
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws the specified number of vertical track bar ticks with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the ticks.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the ticks.</param>
		/// <param name="numTicks">The number of ticks to draw.</param>
		/// <param name="edgeStyle">One of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x060041DA RID: 16858 RVA: 0x00103E78 File Offset: 0x00102078
		public static void DrawVerticalTicks(Graphics g, Rectangle bounds, int numTicks, EdgeStyle edgeStyle)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			if (bounds.Height <= 0 || bounds.Width <= 0 || numTicks <= 0)
			{
				return;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.TicksVertical.Normal);
			double num = (double)bounds.Top;
			double num2 = (double)(bounds.Height - 2) / (double)(numTicks - 1);
			for (int i = 0; i < numTicks; i++)
			{
				visualStyleRenderer.DrawEdge(g, new Rectangle(bounds.Left, (int)Math.Round(num), bounds.Width, 5), Edges.Top, edgeStyle, EdgeEffects.None);
				num += num2;
			}
		}

		/// <summary>Draws the track for a vertical track bar with visual styles.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the track.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the track.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041DB RID: 16859 RVA: 0x00103F18 File Offset: 0x00102118
		public static void DrawVerticalTrack(Graphics g, Rectangle bounds)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.Track.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the track bar slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041DC RID: 16860 RVA: 0x00103F48 File Offset: 0x00102148
		public static Size GetBottomPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbBottom.Normal);
			IL_0073:
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the left.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041DD RID: 16861 RVA: 0x00103FD0 File Offset: 0x001021D0
		public static Size GetLeftPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbLeft.Normal);
			IL_0073:
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points to the right.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041DE RID: 16862 RVA: 0x00104058 File Offset: 0x00102258
		public static Size GetRightPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbRight.Normal);
			IL_0073:
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Returns the size, in pixels, of the track bar slider (also known as the thumb) that points up.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size, in pixels, of the slider.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.VisualStyles.TrackBarThumbState" /> values that specifies the visual state of the slider.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041DF RID: 16863 RVA: 0x001040E0 File Offset: 0x001022E0
		public static Size GetTopPointingThumbSize(Graphics g, TrackBarThumbState state)
		{
			if (!TrackBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer;
			switch (state)
			{
			case TrackBarThumbState.Hot:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Hot);
				goto IL_0073;
			case TrackBarThumbState.Pressed:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Pressed);
				goto IL_0073;
			case TrackBarThumbState.Disabled:
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Disabled);
				goto IL_0073;
			}
			visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TrackBar.ThumbTop.Normal);
			IL_0073:
			return visualStyleRenderer.GetPartSize(g, ThemeSizeType.Draw);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TrackBarRenderer" /> class can be used to draw a track bar with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x00104168 File Offset: 0x00102368
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}
	}
}
