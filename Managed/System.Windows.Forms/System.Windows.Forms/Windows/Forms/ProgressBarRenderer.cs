using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to render a progress bar control with visual styles. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029B RID: 667
	public sealed class ProgressBarRenderer
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x000ABB00 File Offset: 0x000A9D00
		private ProgressBarRenderer()
		{
		}

		/// <summary>Draws an empty progress bar control that fills in horizontally.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the progress bar.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C75 RID: 11381 RVA: 0x000ABB08 File Offset: 0x000A9D08
		public static void DrawHorizontalBar(Graphics g, Rectangle bounds)
		{
			if (!ProgressBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Bar.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a set of progress bar pieces that fill a horizontal progress bar.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds to be filled by progress bar pieces.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C76 RID: 11382 RVA: 0x000ABB38 File Offset: 0x000A9D38
		public static void DrawHorizontalChunks(Graphics g, Rectangle bounds)
		{
			if (!ProgressBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws an empty progress bar control that fills in vertically.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the progress bar.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C77 RID: 11383 RVA: 0x000ABB68 File Offset: 0x000A9D68
		public static void DrawVerticalBar(Graphics g, Rectangle bounds)
		{
			if (!ProgressBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.BarVertical.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Draws a set of progress bar pieces that fill a vertical progress bar.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the progress bar.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds to be filled by progress bar pieces.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C78 RID: 11384 RVA: 0x000ABB98 File Offset: 0x000A9D98
		public static void DrawVerticalChunks(Graphics g, Rectangle bounds)
		{
			if (!ProgressBarRenderer.IsSupported)
			{
				throw new InvalidOperationException();
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.ChunkVertical.Normal);
			visualStyleRenderer.DrawBackground(g, bounds);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ProgressBarRenderer" /> class can be used to draw a progress bar control with visual styles.</summary>
		/// <returns>true if the user has enabled visual styles in the operating system and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000ABBC8 File Offset: 0x000A9DC8
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}

		/// <summary>Gets the width, in pixels, of the space between each inner piece of the progress bar.</summary>
		/// <returns>The width, in pixels, of the space between each inner piece of the progress bar. </returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000ABBF0 File Offset: 0x000A9DF0
		public static int ChunkSpaceThickness
		{
			get
			{
				if (!ProgressBarRenderer.IsSupported)
				{
					throw new InvalidOperationException();
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return visualStyleRenderer.GetInteger(IntegerProperty.ProgressSpaceSize);
			}
		}

		/// <summary>Gets the width, in pixels, of a single inner piece of the progress bar.</summary>
		/// <returns>The width, in pixels, of a single inner piece of the progress bar.</returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000ABC24 File Offset: 0x000A9E24
		public static int ChunkThickness
		{
			get
			{
				if (!ProgressBarRenderer.IsSupported)
				{
					throw new InvalidOperationException();
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				return visualStyleRenderer.GetInteger(IntegerProperty.ProgressChunkSize);
			}
		}
	}
}
