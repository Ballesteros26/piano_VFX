using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace System.Drawing
{
	/// <summary>Defines an object used to draw lines and curves. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Pens" />
	// Token: 0x0200007A RID: 122
	public sealed class Pen : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x0000FDAE File Offset: 0x0000DFAE
		internal Pen(IntPtr p)
		{
			this.nativeObject = p;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the fill properties of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		// Token: 0x0600053B RID: 1339 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public Pen(Brush brush)
			: this(brush, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified color.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />. </param>
		// Token: 0x0600053C RID: 1340 RVA: 0x0000FDD2 File Offset: 0x0000DFD2
		public Pen(Color color)
			: this(color, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="P:System.Drawing.Pen.Width" />.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="width">The width of the new <see cref="T:System.Drawing.Pen" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		// Token: 0x0600053D RID: 1341 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public Pen(Brush brush, float width)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePen2(brush.NativeBrush, width, GraphicsUnit.World, out this.nativeObject));
			this.color = Color.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Pen" /> class with the specified <see cref="T:System.Drawing.Color" /> and <see cref="P:System.Drawing.Pen.Width" /> properties.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that indicates the color of this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="width">A value indicating the width of this <see cref="T:System.Drawing.Pen" />. </param>
		// Token: 0x0600053E RID: 1342 RVA: 0x0000FE20 File Offset: 0x0000E020
		public Pen(Color color, float width)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreatePen1(color.ToArgb(), width, GraphicsUnit.World, out this.nativeObject));
			this.color = color;
		}

		/// <summary>Gets or sets the alignment for this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PenAlignment" /> that represents the alignment for this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.PenAlignment" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Alignment" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000FE50 File Offset: 0x0000E050
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000FE70 File Offset: 0x0000E070
		[MonoLimitation("Libgdiplus doesn't use this property for rendering")]
		public PenAlignment Alignment
		{
			get
			{
				PenAlignment penAlignment;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenMode(this.nativeObject, out penAlignment));
				return penAlignment;
			}
			set
			{
				if (value < PenAlignment.Center || value > PenAlignment.Right)
				{
					throw new InvalidEnumArgumentException("Alignment", (int)value, typeof(PenAlignment));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenMode(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Brush" /> that determines attributes of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Brush" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public Brush Brush
		{
			get
			{
				IntPtr intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenBrushFill(this.nativeObject, out intPtr));
				return new SolidBrush(intPtr);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Brush");
				}
				if (!this.isModifiable)
				{
					throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPenBrushFill(this.nativeObject, value.NativeBrush));
				this.color = Color.Empty;
			}
		}

		/// <summary>Gets or sets the color of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Color" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000FF40 File Offset: 0x0000E140
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000FF8E File Offset: 0x0000E18E
		public Color Color
		{
			get
			{
				if (this.color.Equals(Color.Empty))
				{
					int num;
					GDIPlus.CheckStatus(GDIPlus.GdipGetPenColor(this.nativeObject, out num));
					this.color = Color.FromArgb(num);
				}
				return this.color;
			}
			set
			{
				if (!this.isModifiable)
				{
					throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPenColor(this.nativeObject, value.ToArgb()));
				this.color = value;
			}
		}

		/// <summary>Gets or sets an array of values that specifies a compound pen. A compound pen draws a compound line made up of parallel lines and spaces.</summary>
		/// <returns>An array of real numbers that specifies the compound array. The elements in the array must be in increasing order, not less than 0, and not greater than 1.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CompoundArray" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00010004 File Offset: 0x0000E204
		public float[] CompoundArray
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenCompoundCount(this.nativeObject, out num));
				float[] array = new float[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenCompoundArray(this.nativeObject, array, num));
				return array;
			}
			set
			{
				if (!this.isModifiable)
				{
					throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
				}
				if (value.Length < 2)
				{
					throw new ArgumentException("Invalid parameter.");
				}
				for (int i = 0; i < value.Length; i++)
				{
					float num = value[i];
					if (num < 0f || num > 1f)
					{
						throw new ArgumentException("Invalid parameter.");
					}
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPenCompoundArray(this.nativeObject, value, value.Length));
			}
		}

		/// <summary>Gets or sets a custom cap to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomEndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001007D File Offset: 0x0000E27D
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00010085 File Offset: 0x0000E285
		public CustomLineCap CustomEndCap
		{
			get
			{
				return this.endCap;
			}
			set
			{
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenCustomEndCap(this.nativeObject, value.nativeCap));
					this.endCap = value;
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets a custom cap to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> that represents the cap used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.CustomStartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000100C1 File Offset: 0x0000E2C1
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public CustomLineCap CustomStartCap
		{
			get
			{
				return this.startCap;
			}
			set
			{
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenCustomStartCap(this.nativeObject, value.nativeCap));
					this.startCap = value;
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the cap style used at the end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.DashCap" /> values that represents the cap style used at the beginning and end of the dashes that make up dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.DashCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00010108 File Offset: 0x0000E308
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00010128 File Offset: 0x0000E328
		public DashCap DashCap
		{
			get
			{
				DashCap dashCap;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenDashCap197819(this.nativeObject, out dashCap));
				return dashCap;
			}
			set
			{
				if (value < DashCap.Flat || value > DashCap.Triangle)
				{
					throw new InvalidEnumArgumentException("DashCap", (int)value, typeof(DashCap));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenDashCap197819(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the distance from the start of a line to the beginning of a dash pattern.</summary>
		/// <returns>The distance from the start of a line to the beginning of a dash pattern.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashOffset" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001017C File Offset: 0x0000E37C
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001019C File Offset: 0x0000E39C
		public float DashOffset
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenDashOffset(this.nativeObject, out num));
				return num;
			}
			set
			{
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenDashOffset(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets an array of custom dashes and spaces.</summary>
		/// <returns>An array of real numbers that specifies the lengths of alternating dashes and spaces in dashed lines.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashPattern" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000101C8 File Offset: 0x0000E3C8
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00010228 File Offset: 0x0000E428
		public float[] DashPattern
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenDashCount(this.nativeObject, out num));
				float[] array;
				if (num > 0)
				{
					array = new float[num];
					GDIPlus.CheckStatus(GDIPlus.GdipGetPenDashArray(this.nativeObject, array, num));
				}
				else if (this.DashStyle == DashStyle.Custom)
				{
					array = new float[] { 1f };
				}
				else
				{
					array = new float[0];
				}
				return array;
			}
			set
			{
				if (!this.isModifiable)
				{
					throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Invalid parameter.");
				}
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] <= 0f)
					{
						throw new ArgumentException("Invalid parameter.");
					}
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetPenDashArray(this.nativeObject, value, value.Length));
			}
		}

		/// <summary>Gets or sets the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.DashStyle" /> that represents the style used for dashed lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.DashStyle" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00010298 File Offset: 0x0000E498
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public DashStyle DashStyle
		{
			get
			{
				DashStyle dashStyle;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenDashStyle(this.nativeObject, out dashStyle));
				return dashStyle;
			}
			set
			{
				if (value < DashStyle.Solid || value > DashStyle.Custom)
				{
					throw new InvalidEnumArgumentException("DashStyle", (int)value, typeof(DashStyle));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenDashStyle(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.StartCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001030C File Offset: 0x0000E50C
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001032C File Offset: 0x0000E52C
		public LineCap StartCap
		{
			get
			{
				LineCap lineCap;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenStartCap(this.nativeObject, out lineCap));
				return lineCap;
			}
			set
			{
				if (value < LineCap.Flat || value > LineCap.Custom)
				{
					throw new InvalidEnumArgumentException("StartCap", (int)value, typeof(LineCap));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenStartCap(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.LineCap" /> values that represents the cap style used at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a member of <see cref="T:System.Drawing.Drawing2D.LineCap" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.EndCap" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00010384 File Offset: 0x0000E584
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000103A4 File Offset: 0x0000E5A4
		public LineCap EndCap
		{
			get
			{
				LineCap lineCap;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenEndCap(this.nativeObject, out lineCap));
				return lineCap;
			}
			set
			{
				if (value < LineCap.Flat || value > LineCap.Custom)
				{
					throw new InvalidEnumArgumentException("EndCap", (int)value, typeof(LineCap));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenEndCap(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.LineJoin" /> that represents the join style for the ends of two consecutive lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.LineJoin" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000103FC File Offset: 0x0000E5FC
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001041C File Offset: 0x0000E61C
		public LineJoin LineJoin
		{
			get
			{
				LineJoin lineJoin;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenLineJoin(this.nativeObject, out lineJoin));
				return lineJoin;
			}
			set
			{
				if (value < LineJoin.Miter || value > LineJoin.MiterClipped)
				{
					throw new InvalidEnumArgumentException("LineJoin", (int)value, typeof(LineJoin));
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenLineJoin(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the limit of the thickness of the join on a mitered corner.</summary>
		/// <returns>The limit of the thickness of the join on a mitered corner.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.MiterLimit" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00010470 File Offset: 0x0000E670
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00010490 File Offset: 0x0000E690
		public float MiterLimit
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenMiterLimit(this.nativeObject, out num));
				return num;
			}
			set
			{
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenMiterLimit(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Drawing2D.PenType" /> enumeration that specifies the style of lines drawn with this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000104BC File Offset: 0x0000E6BC
		public PenType PenType
		{
			get
			{
				PenType penType;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenFillType(this.nativeObject, out penType));
				return penType;
			}
		}

		/// <summary>Gets or sets a copy of the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric transformation for this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Transform" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000104DC File Offset: 0x0000E6DC
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00010506 File Offset: 0x0000E706
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Transform");
				}
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenTransform(this.nativeObject, value.nativeMatrix));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Pen" />, in units of the <see cref="T:System.Drawing.Graphics" /> object used for drawing.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Pen" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Pen.Width" /> property is set on an immutable <see cref="T:System.Drawing.Pen" />, such as those returned by the <see cref="T:System.Drawing.Pens" /> class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00010544 File Offset: 0x0000E744
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00010564 File Offset: 0x0000E764
		public float Width
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPenWidth(this.nativeObject, out num));
				return num;
			}
			set
			{
				if (this.isModifiable)
				{
					GDIPlus.CheckStatus(GDIPlus.GdipSetPenWidth(this.nativeObject, value));
					return;
				}
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001058F File Offset: 0x0000E78F
		internal IntPtr NativePen
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast to a <see cref="T:System.Drawing.Pen" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000561 RID: 1377 RVA: 0x00010598 File Offset: 0x0000E798
		public object Clone()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipClonePen(this.nativeObject, out intPtr));
			return new Pen(intPtr)
			{
				startCap = this.startCap,
				endCap = this.endCap
			};
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000562 RID: 1378 RVA: 0x000105D5 File Offset: 0x0000E7D5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000105E4 File Offset: 0x0000E7E4
		private void Dispose(bool disposing)
		{
			if (disposing && !this.isModifiable)
			{
				throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
			}
			if (this.nativeObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeletePen(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001063C File Offset: 0x0000E83C
		~Pen()
		{
			this.Dispose(false);
		}

		/// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> object by which to multiply the transformation matrix. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000565 RID: 1381 RVA: 0x0001066C File Offset: 0x0000E86C
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the transformation matrix for this <see cref="T:System.Drawing.Pen" /> by the specified <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">The <see cref="T:System.Drawing.Drawing2D.Matrix" /> by which to multiply the transformation matrix. </param>
		/// <param name="order">The order in which to perform the multiplication operation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000566 RID: 1382 RVA: 0x00010676 File Offset: 0x0000E876
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyPenTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		/// <summary>Resets the geometric transformation matrix for this <see cref="T:System.Drawing.Pen" /> to identity.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000567 RID: 1383 RVA: 0x0001068F File Offset: 0x0000E88F
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetPenTransform(this.nativeObject));
		}

		/// <summary>Rotates the local geometric transformation by the specified angle. This method prepends the rotation to the transformation.</summary>
		/// <param name="angle">The angle of rotation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000568 RID: 1384 RVA: 0x000106A1 File Offset: 0x0000E8A1
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Rotates the local geometric transformation by the specified angle in the specified order.</summary>
		/// <param name="angle">The angle of rotation. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the rotation matrix. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000569 RID: 1385 RVA: 0x000106AB File Offset: 0x0000E8AB
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotatePenTransform(this.nativeObject, angle, order));
		}

		/// <summary>Scales the local geometric transformation by the specified factors. This method prepends the scaling matrix to the transformation.</summary>
		/// <param name="sx">The factor by which to scale the transformation in the x-axis direction. </param>
		/// <param name="sy">The factor by which to scale the transformation in the y-axis direction. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600056A RID: 1386 RVA: 0x000106BF File Offset: 0x0000E8BF
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Scales the local geometric transformation by the specified factors in the specified order.</summary>
		/// <param name="sx">The factor by which to scale the transformation in the x-axis direction. </param>
		/// <param name="sy">The factor by which to scale the transformation in the y-axis direction. </param>
		/// <param name="order">A <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> that specifies whether to append or prepend the scaling matrix. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600056B RID: 1387 RVA: 0x000106CA File Offset: 0x0000E8CA
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScalePenTransform(this.nativeObject, sx, sy, order));
		}

		/// <summary>Sets the values that determine the style of cap used to end lines drawn by this <see cref="T:System.Drawing.Pen" />.</summary>
		/// <param name="startCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning of lines drawn with this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="endCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the end of lines drawn with this <see cref="T:System.Drawing.Pen" />. </param>
		/// <param name="dashCap">A <see cref="T:System.Drawing.Drawing2D.LineCap" /> that represents the cap style to use at the beginning or end of dashed lines drawn with this <see cref="T:System.Drawing.Pen" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600056C RID: 1388 RVA: 0x000106DF File Offset: 0x0000E8DF
		public void SetLineCap(LineCap startCap, LineCap endCap, DashCap dashCap)
		{
			if (this.isModifiable)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPenLineCap197819(this.nativeObject, startCap, endCap, dashCap));
				return;
			}
			throw new ArgumentException(Locale.GetText("This Pen object can't be modified."));
		}

		/// <summary>Translates the local geometric transformation by the specified dimensions. This method prepends the translation to the transformation.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600056D RID: 1389 RVA: 0x0001070C File Offset: 0x0000E90C
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Translates the local geometric transformation by the specified dimensions in the specified order.</summary>
		/// <param name="dx">The value of the translation in x. </param>
		/// <param name="dy">The value of the translation in y. </param>
		/// <param name="order">The order (prepend or append) in which to apply the translation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600056E RID: 1390 RVA: 0x00010717 File Offset: 0x0000E917
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslatePenTransform(this.nativeObject, dx, dy, order));
		}

		// Token: 0x040004AA RID: 1194
		internal IntPtr nativeObject;

		// Token: 0x040004AB RID: 1195
		internal bool isModifiable = true;

		// Token: 0x040004AC RID: 1196
		private Color color;

		// Token: 0x040004AD RID: 1197
		private CustomLineCap startCap;

		// Token: 0x040004AE RID: 1198
		private CustomLineCap endCap;
	}
}
