using System;
using System.ComponentModel;
using System.Drawing;
using System.Security;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Provides methods for drawing and getting information about a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />. This class cannot be inherited.</summary>
	// Token: 0x02000627 RID: 1575
	public sealed class VisualStyleRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given class, part, and state values.</summary>
		/// <param name="className">The class name of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <param name="part">The part of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <param name="state">The state of the element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
		// Token: 0x06004FD1 RID: 20433 RVA: 0x00137888 File Offset: 0x00135A88
		public VisualStyleRenderer(string className, int part, int state)
		{
			this.theme_handle_manager.VisualStyleRenderer = this;
			this.SetParameters(className, part, state);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class using the given <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> will represent.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> is not defined by the current visual style.</exception>
		// Token: 0x06004FD2 RID: 20434 RVA: 0x001378BC File Offset: 0x00135ABC
		public VisualStyleRenderer(VisualStyleElement element)
			: this(element.ClassName, element.Part, element.State)
		{
		}

		/// <summary>Gets the class name of the current visual style element.</summary>
		/// <returns>A string that identifies the class of the current visual style element.</returns>
		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x001378E4 File Offset: 0x00135AE4
		public string Class
		{
			get
			{
				return this.class_name;
			}
		}

		/// <summary>Gets a unique identifier for the current class of visual style elements.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that identifies a set of data that defines the class of elements specified by <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x001378EC File Offset: 0x00135AEC
		public IntPtr Handle
		{
			get
			{
				return this.theme;
			}
		}

		/// <summary>Gets the last error code returned by the native visual styles (UxTheme) API methods encapsulated by the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> class.</summary>
		/// <returns>A value specifying the last error code returned by the native visual styles API methods that this class encapsulates.</returns>
		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x001378F4 File Offset: 0x00135AF4
		public int LastHResult
		{
			get
			{
				return this.last_hresult;
			}
		}

		/// <summary>Gets the part of the current visual style element.</summary>
		/// <returns>A value that specifies the part of the current visual style element.</returns>
		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06004FD6 RID: 20438 RVA: 0x001378FC File Offset: 0x00135AFC
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		/// <summary>Gets the state of the current visual style element.</summary>
		/// <returns>A value that identifies the state of the current visual style element.</returns>
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x00137904 File Offset: 0x00135B04
		public int State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets a value specifying whether the operating system is using visual styles to draw controls.</summary>
		/// <returns>true if the operating system supports visual styles, the user has enabled visual styles in the operating system, and visual styles are applied to the client area of application windows; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x0013790C File Offset: 0x00135B0C
		public static bool IsSupported
		{
			get
			{
				return VisualStyleInformation.IsEnabledByUser && (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled || Application.VisualStyleState == VisualStyleState.ClientAreaEnabled);
			}
		}

		/// <summary>Determines whether the specified visual style element is defined by the current visual style.</summary>
		/// <returns>true if the combination of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" /> and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" /> properties of <paramref name="element" /> are defined; otherwise, false. </returns>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> whose class and part combination will be verified.</param>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		// Token: 0x06004FD9 RID: 20441 RVA: 0x00137934 File Offset: 0x00135B34
		public static bool IsElementDefined(VisualStyleElement element)
		{
			if (!VisualStyleRenderer.IsSupported)
			{
				throw new InvalidOperationException("Visual Styles are not enabled.");
			}
			if (VisualStyleRenderer.IsElementKnownToBeSupported(element.ClassName, element.Part, element.State))
			{
				return true;
			}
			IntPtr intPtr = VisualStyleRenderer.VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, element.ClassName);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			bool flag = VisualStyleRenderer.VisualStyles.UxThemeIsThemePartDefined(intPtr, element.Part);
			VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(intPtr);
			return flag;
		}

		/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FDA RID: 20442 RVA: 0x001379BC File Offset: 0x00135BBC
		public void DrawBackground(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeBackground(this.theme, dc, this.part, this.state, bounds);
		}

		/// <summary>Draws the background image of the current visual style element within the specified bounding rectangle and clipped to the specified clipping rectangle.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the background image is drawn.</param>
		/// <param name="clipRectangle">A <see cref="T:System.Drawing.Rectangle" /> that defines a clipping rectangle for the drawing operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FDB RID: 20443 RVA: 0x00137A00 File Offset: 0x00135C00
		public void DrawBackground(IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeBackground(this.theme, dc, this.part, this.state, bounds, clipRectangle);
		}

		/// <summary>Draws one or more edges of the specified bounding rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the interior of the <paramref name="bounds" /> parameter, minus the edges that were drawn.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the edges.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> whose bounds define the edges to draw.</param>
		/// <param name="edges">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.Edges" /> values.</param>
		/// <param name="style">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeStyle" /> values.</param>
		/// <param name="effects">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.EdgeEffects" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FDC RID: 20444 RVA: 0x00137A44 File Offset: 0x00135C44
		public Rectangle DrawEdge(IDeviceContext dc, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeEdge(this.theme, dc, this.part, this.state, bounds, edges, style, effects, out rectangle);
			return rectangle;
		}

		/// <summary>Draws the image from the specified <see cref="T:System.Windows.Forms.ImageList" /> within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the image is drawn.</param>
		/// <param name="imageList">An <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="imageIndex">The index of the <see cref="T:System.Drawing.Image" /> within <paramref name="imageList" /> to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> or <paramref name="image" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="imageIndex" /> is less than 0, or greater than or equal to the number of images in<paramref name=" imageList" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FDD RID: 20445 RVA: 0x00137A90 File Offset: 0x00135C90
		public void DrawImage(Graphics g, Rectangle bounds, ImageList imageList, int imageIndex)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (imageIndex < 0 || imageIndex > imageList.Images.Count - 1)
			{
				throw new ArgumentOutOfRangeException("imageIndex");
			}
			if (imageList.Images[imageIndex] == null)
			{
				throw new ArgumentNullException("imageIndex");
			}
			g.DrawImage(imageList.Images[imageIndex], bounds);
		}

		/// <summary>Draws the specified image within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the image.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which the image is drawn.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> or <paramref name="image" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FDE RID: 20446 RVA: 0x00137B08 File Offset: 0x00135D08
		public void DrawImage(Graphics g, Rectangle bounds, Image image)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			g.DrawImage(image, bounds);
		}

		/// <summary>Draws the background of a control's parent in the specified area.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the background of the parent of <paramref name="childControl" />. This object typically belongs to the child control.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the parent control's background. This rectangle should be inside the child control’s bounds.</param>
		/// <param name="childControl">The control whose parent's background will be drawn.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FDF RID: 20447 RVA: 0x00137B40 File Offset: 0x00135D40
		public void DrawParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeParentBackground(dc, bounds, childControl);
		}

		/// <summary>Draws text in the specified bounding rectangle with the option of displaying disabled text and applying other text formatting.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
		/// <param name="textToDraw">The text to draw.</param>
		/// <param name="drawDisabled">true to draw grayed-out text; otherwise, false.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE0 RID: 20448 RVA: 0x00137B74 File Offset: 0x00135D74
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeDrawThemeText(this.theme, dc, this.part, this.state, textToDraw, flags, bounds);
		}

		/// <summary>Draws text in the specified bounds with the option of displaying disabled text.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
		/// <param name="textToDraw">The text to draw.</param>
		/// <param name="drawDisabled">true to draw grayed-out text; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE1 RID: 20449 RVA: 0x00137BBC File Offset: 0x00135DBC
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw, bool drawDisabled)
		{
			this.DrawText(dc, bounds, textToDraw, drawDisabled, TextFormatFlags.Left);
		}

		/// <summary>Draws text in the specified bounds using default formatting.</summary>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> used to draw the text.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> in which to draw the text.</param>
		/// <param name="textToDraw">The text to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE2 RID: 20450 RVA: 0x00137BCC File Offset: 0x00135DCC
		public void DrawText(IDeviceContext dc, Rectangle bounds, string textToDraw)
		{
			this.DrawText(dc, bounds, textToDraw, false, TextFormatFlags.Left);
		}

		/// <summary>Returns the content area for the background of the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the content area for the background of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE3 RID: 20451 RVA: 0x00137BDC File Offset: 0x00135DDC
		public Rectangle GetBackgroundContentRectangle(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeBackgroundContentRect(this.theme, dc, this.part, this.state, bounds, out rectangle);
			return rectangle;
		}

		/// <summary>Returns the entire background area for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="contentBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the content area of the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE4 RID: 20452 RVA: 0x00137C24 File Offset: 0x00135E24
		public Rectangle GetBackgroundExtent(IDeviceContext dc, Rectangle contentBounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeBackgroundExtent(this.theme, dc, this.part, this.state, contentBounds, out rectangle);
			return rectangle;
		}

		/// <summary>Returns the region for the background of the current visual style element.</summary>
		/// <returns>The <see cref="T:System.Drawing.Region" /> that contains the background of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the entire background area of the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FE5 RID: 20453 RVA: 0x00137C6C File Offset: 0x00135E6C
		[SuppressUnmanagedCodeSecurity]
		public Region GetBackgroundRegion(IDeviceContext dc, Rectangle bounds)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Region region;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeBackgroundRegion(this.theme, dc, this.part, this.state, bounds, out region);
			return region;
		}

		/// <summary>Returns the value of the specified Boolean property for the current visual style element.</summary>
		/// <returns>true if the property specified by the <paramref name="prop" /> parameter is true for the current visual style element; otherwise, false.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.BooleanProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.BooleanProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FE6 RID: 20454 RVA: 0x00137CB4 File Offset: 0x00135EB4
		public bool GetBoolean(BooleanProperty prop)
		{
			if (!Enum.IsDefined(typeof(BooleanProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(BooleanProperty));
			}
			bool flag;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeBool(this.theme, this.part, this.state, prop, out flag);
			return flag;
		}

		/// <summary>Returns the value of the specified color property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ColorProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FE7 RID: 20455 RVA: 0x00137D18 File Offset: 0x00135F18
		public Color GetColor(ColorProperty prop)
		{
			if (!Enum.IsDefined(typeof(ColorProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(ColorProperty));
			}
			Color color;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeColor(this.theme, this.part, this.state, prop, out color);
			return color;
		}

		/// <summary>Returns the value of the specified enumerated type property for the current visual style element.</summary>
		/// <returns>The integer value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.EnumProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.EnumProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FE8 RID: 20456 RVA: 0x00137D7C File Offset: 0x00135F7C
		public int GetEnumValue(EnumProperty prop)
		{
			if (!Enum.IsDefined(typeof(EnumProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(EnumProperty));
			}
			int num;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeEnumValue(this.theme, this.part, this.state, prop, out num);
			return num;
		}

		/// <summary>Returns the value of the specified file name property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.FilenameProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.FilenameProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FE9 RID: 20457 RVA: 0x00137DE0 File Offset: 0x00135FE0
		public string GetFilename(FilenameProperty prop)
		{
			if (!Enum.IsDefined(typeof(FilenameProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(FilenameProperty));
			}
			string text;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeFilename(this.theme, this.part, this.state, prop, out text);
			return text;
		}

		/// <summary>Returns the value of the specified font property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.FontProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.FontProperty" /> values.</exception>
		// Token: 0x06004FEA RID: 20458 RVA: 0x00137E44 File Offset: 0x00136044
		[MonoTODO("I can't get MS's to return anything but null, so I can't really get this one right")]
		public Font GetFont(IDeviceContext dc, FontProperty prop)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the value of the specified integer property for the current visual style element.</summary>
		/// <returns>The integer value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.IntegerProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.IntegerProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FEB RID: 20459 RVA: 0x00137E4C File Offset: 0x0013604C
		public int GetInteger(IntegerProperty prop)
		{
			if (!Enum.IsDefined(typeof(IntegerProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(IntegerProperty));
			}
			int num;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeInt(this.theme, this.part, this.state, prop, out num);
			return num;
		}

		/// <summary>Returns the value of the specified margins property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.MarginProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.MarginProperty" /> values.</exception>
		// Token: 0x06004FEC RID: 20460 RVA: 0x00137EB0 File Offset: 0x001360B0
		[MonoTODO("MS's causes a PInvokeStackUnbalance on me, so this is not verified against MS.")]
		public Padding GetMargins(IDeviceContext dc, MarginProperty prop)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!Enum.IsDefined(typeof(MarginProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(MarginProperty));
			}
			Padding padding;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeMargins(this.theme, dc, this.part, this.state, prop, out padding);
			return padding;
		}

		/// <summary>Returns the value of the specified size property of the current visual style part using the specified drawing bounds.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the size specified by the <paramref name="type" /> parameter for the current visual style part.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area in which the part will be drawn.</param>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
		// Token: 0x06004FED RID: 20461 RVA: 0x00137F28 File Offset: 0x00136128
		public Size GetPartSize(IDeviceContext dc, Rectangle bounds, ThemeSizeType type)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!Enum.IsDefined(typeof(ThemeSizeType), type))
			{
				throw new InvalidEnumArgumentException("prop", (int)type, typeof(ThemeSizeType));
			}
			Size size;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemePartSize(this.theme, dc, this.part, this.state, bounds, type, out size);
			return size;
		}

		/// <summary>Returns the value of the specified size property of the current visual style part.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the size specified by the <paramref name="type" /> parameter for the current visual style part. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values that specifies which size value to retrieve for the part.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.ThemeSizeType" /> values.</exception>
		// Token: 0x06004FEE RID: 20462 RVA: 0x00137FA0 File Offset: 0x001361A0
		public Size GetPartSize(IDeviceContext dc, ThemeSizeType type)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (!Enum.IsDefined(typeof(ThemeSizeType), type))
			{
				throw new InvalidEnumArgumentException("prop", (int)type, typeof(ThemeSizeType));
			}
			Size size;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemePartSize(this.theme, dc, this.part, this.state, type, out size);
			return size;
		}

		/// <summary>Returns the value of the specified point property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.PointProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.PointProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FEF RID: 20463 RVA: 0x00138018 File Offset: 0x00136218
		public Point GetPoint(PointProperty prop)
		{
			if (!Enum.IsDefined(typeof(PointProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(PointProperty));
			}
			Point point;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemePosition(this.theme, this.part, this.state, prop, out point);
			return point;
		}

		/// <summary>Returns the value of the specified string property for the current visual style element.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of the property specified by the <paramref name="prop" /> parameter for the current visual style element.</returns>
		/// <param name="prop">One of the <see cref="T:System.Windows.Forms.VisualStyles.StringProperty" /> values that specifies which property value to retrieve for the current visual style element.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="prop" /> is not one of the <see cref="T:System.Windows.Forms.VisualStyles.StringProperty" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FF0 RID: 20464 RVA: 0x0013807C File Offset: 0x0013627C
		[MonoTODO("Can't find any values that return anything on MS to test against")]
		public string GetString(StringProperty prop)
		{
			if (!Enum.IsDefined(typeof(StringProperty), prop))
			{
				throw new InvalidEnumArgumentException("prop", (int)prop, typeof(StringProperty));
			}
			string text;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeString(this.theme, this.part, this.state, prop, out text);
			return text;
		}

		/// <summary>Returns the size and location of the specified string when drawn with the font of the current visual style element within the specified initial bounding rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the area required to fit the rendered text. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> used to control the flow and wrapping of the text.</param>
		/// <param name="textToDraw">The string to measure.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FF1 RID: 20465 RVA: 0x001380E0 File Offset: 0x001362E0
		public Rectangle GetTextExtent(IDeviceContext dc, Rectangle bounds, string textToDraw, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeTextExtent(this.theme, dc, this.part, this.state, textToDraw, flags, bounds, out rectangle);
			return rectangle;
		}

		/// <summary>Returns the size and location of the specified string when drawn with the font of the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the area required to fit the rendered text. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="textToDraw">The string to measure.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FF2 RID: 20466 RVA: 0x00138128 File Offset: 0x00136328
		public Rectangle GetTextExtent(IDeviceContext dc, string textToDraw, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			Rectangle rectangle;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeTextExtent(this.theme, dc, this.part, this.state, textToDraw, flags, out rectangle);
			return rectangle;
		}

		/// <summary>Retrieves information about the font specified by the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.TextMetrics" /> that provides information about the font specified by the current visual style element. </returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FF3 RID: 20467 RVA: 0x00138170 File Offset: 0x00136370
		public TextMetrics GetTextMetrics(IDeviceContext dc)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc", "dc cannot be null.");
			}
			TextMetrics textMetrics;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeGetThemeTextMetrics(this.theme, dc, this.part, this.state, out textMetrics);
			return textMetrics;
		}

		/// <summary>Returns a hit test code indicating whether the point is contained in the background of the current visual style element and within the specified region.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
		/// <param name="hRgn">A Windows handle to a <see cref="T:System.Drawing.Region" /> that specifies the bounds of the hit test area within the background.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FF4 RID: 20468 RVA: 0x001381BC File Offset: 0x001363BC
		public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, IntPtr hRgn, Point pt, HitTestOptions options)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			HitTestCode hitTestCode;
			this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeHitTestThemeBackground(this.theme, dc, this.part, this.state, options, backgroundRectangle, hRgn, pt, out hitTestCode);
			return hitTestCode;
		}

		/// <summary>Returns a hit test code indicating whether the point is contained in the background of the current visual style element and within the specified bounds.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element, if at all.</returns>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> this operation will use.</param>
		/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
		/// <param name="region">A <see cref="T:System.Drawing.Region" /> that specifies the bounds of the hit test area within the background.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FF5 RID: 20469 RVA: 0x00138208 File Offset: 0x00136408
		public HitTestCode HitTestBackground(Graphics g, Rectangle backgroundRectangle, Region region, Point pt, HitTestOptions options)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr hrgn = region.GetHrgn(g);
			return this.HitTestBackground(g, backgroundRectangle, hrgn, pt, options);
		}

		/// <summary>Returns a hit test code indicating whether a point is contained in the background of the current visual style element.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.HitTestCode" /> that describes where <paramref name="pt" /> is located in the background of the current visual style element.</returns>
		/// <param name="dc">The <see cref="T:System.Drawing.IDeviceContext" /> this operation will use.</param>
		/// <param name="backgroundRectangle">A <see cref="T:System.Drawing.Rectangle" /> that contains the background of the current visual style element.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.HitTestOptions" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06004FF6 RID: 20470 RVA: 0x0013823C File Offset: 0x0013643C
		public HitTestCode HitTestBackground(IDeviceContext dc, Rectangle backgroundRectangle, Point pt, HitTestOptions options)
		{
			return this.HitTestBackground(dc, backgroundRectangle, IntPtr.Zero, pt, options);
		}

		/// <summary>Indicates whether the background of the current visual style element has any semitransparent or alpha-blended pieces.</summary>
		/// <returns>true if the background of the current visual style element has any semitransparent or alpha-blended pieces; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FF7 RID: 20471 RVA: 0x00138250 File Offset: 0x00136450
		public bool IsBackgroundPartiallyTransparent()
		{
			return VisualStyleRenderer.VisualStyles.UxThemeIsThemeBackgroundPartiallyTransparent(this.theme, this.part, this.state);
		}

		/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified class, part, and state values.</summary>
		/// <param name="className">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" /> property.</param>
		/// <param name="part">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" /> property.</param>
		/// <param name="state">The new value of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The combination of <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> is not defined by the current visual style.</exception>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FF8 RID: 20472 RVA: 0x00138270 File Offset: 0x00136470
		public void SetParameters(string className, int part, int state)
		{
			if (this.theme != IntPtr.Zero)
			{
				this.last_hresult = VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(this.theme);
			}
			if (!VisualStyleRenderer.IsSupported)
			{
				throw new InvalidOperationException("Visual Styles are not enabled.");
			}
			this.class_name = className;
			this.part = part;
			this.state = state;
			this.theme = VisualStyleRenderer.VisualStyles.UxThemeOpenThemeData(IntPtr.Zero, this.class_name);
			if (VisualStyleRenderer.IsElementKnownToBeSupported(className, part, state))
			{
				return;
			}
			if (this.theme == IntPtr.Zero || !VisualStyleRenderer.VisualStyles.UxThemeIsThemePartDefined(this.theme, this.part))
			{
				throw new ArgumentException("This element is not supported by the current visual style.");
			}
		}

		/// <summary>Sets this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleRenderer" /> to the visual style element represented by the specified <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" />.</summary>
		/// <param name="element">A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that specifies the new values of the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Class" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleRenderer.State" /> properties.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> is not defined by the current visual style.</exception>
		/// <exception cref="T:System.InvalidOperationException">The operating system does not support visual styles.-or-Visual styles are disabled by the user in the operating system.-or-Visual styles are not applied to the client area of application windows.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004FF9 RID: 20473 RVA: 0x00138338 File Offset: 0x00136538
		public void SetParameters(VisualStyleElement element)
		{
			this.SetParameters(element.ClassName, element.Part, element.State);
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00138360 File Offset: 0x00136560
		internal static IVisualStyles VisualStyles
		{
			get
			{
				return VisualStylesEngine.Instance;
			}
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00138368 File Offset: 0x00136568
		internal void DrawBackgroundExcludingArea(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea)
		{
			VisualStyleRenderer.VisualStyles.VisualStyleRendererDrawBackgroundExcludingArea(this.theme, dc, this.part, this.state, bounds, excludedArea);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00138394 File Offset: 0x00136594
		private static bool IsElementKnownToBeSupported(string className, int part, int state)
		{
			return className == "STATUS" && part == 0 && state == 0;
		}

		// Token: 0x04002D40 RID: 11584
		private string class_name;

		// Token: 0x04002D41 RID: 11585
		private int part;

		// Token: 0x04002D42 RID: 11586
		private int state;

		// Token: 0x04002D43 RID: 11587
		private IntPtr theme;

		// Token: 0x04002D44 RID: 11588
		private int last_hresult;

		// Token: 0x04002D45 RID: 11589
		private VisualStyleRenderer.ThemeHandleManager theme_handle_manager = new VisualStyleRenderer.ThemeHandleManager();

		// Token: 0x02000628 RID: 1576
		private class ThemeHandleManager
		{
			// Token: 0x06004FFE RID: 20478 RVA: 0x001383BC File Offset: 0x001365BC
			~ThemeHandleManager()
			{
				if (!(this.VisualStyleRenderer.theme == IntPtr.Zero))
				{
					VisualStyleRenderer.VisualStyles.UxThemeCloseThemeData(this.VisualStyleRenderer.theme);
				}
			}

			// Token: 0x04002D46 RID: 11590
			public VisualStyleRenderer VisualStyleRenderer;
		}
	}
}
