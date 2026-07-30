using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing
{
	/// <summary>Represents an ARGB (alpha, red, green, blue) color.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Drawing.Color" />
	// Token: 0x02000043 RID: 67
	[TypeConverter(typeof(ColorConverter))]
	[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public struct Color
	{
		/// <summary>Gets the name of this <see cref="T:System.Drawing.Color" />.</summary>
		/// <returns>The name of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005A48 File Offset: 0x00003C48
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					if (this.IsNamedColor)
					{
						this.name = KnownColors.GetName(this.knownColor);
					}
					else
					{
						this.name = string.Format("{0:x}", this.ToArgb());
					}
				}
				return this.name;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a predefined color. Predefined colors are represented by the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00005A99 File Offset: 0x00003C99
		public bool IsKnownColor
		{
			get
			{
				return (this.state & 1) != 0;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a system color. A system color is a color that is used in a Windows display element. System colors are represented by elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created from a system color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005AA6 File Offset: 0x00003CA6
		public bool IsSystemColor
		{
			get
			{
				return (this.state & 8) != 0;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Drawing.Color" /> structure is a named color or a member of the <see cref="T:System.Drawing.KnownColor" /> enumeration.</summary>
		/// <returns>true if this <see cref="T:System.Drawing.Color" /> was created by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005AB3 File Offset: 0x00003CB3
		public bool IsNamedColor
		{
			get
			{
				return (this.state & 5) != 0;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005AC0 File Offset: 0x00003CC0
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00005B00 File Offset: 0x00003D00
		internal long Value
		{
			get
			{
				if (this.value == 0L && this.IsKnownColor)
				{
					this.value = (long)Color.FromKnownColor((KnownColor)this.knownColor).ToArgb() & (long)((ulong)(-1));
				}
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified 8-bit color values (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color component, the value of each component is limited to 8 bits.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="red">The red component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <param name="green">The green component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <param name="blue">The blue component value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000202 RID: 514 RVA: 0x00005B09 File Offset: 0x00003D09
		public static Color FromArgb(int red, int green, int blue)
		{
			return Color.FromArgb(255, red, green, blue);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the four ARGB component (alpha, red, green, and blue) values. Although this method allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="alpha">The alpha component. Valid values are 0 through 255. </param>
		/// <param name="red">The red component. Valid values are 0 through 255. </param>
		/// <param name="green">The green component. Valid values are 0 through 255. </param>
		/// <param name="blue">The blue component. Valid values are 0 through 255. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="alpha" />, <paramref name="red" />, <paramref name="green" />, or <paramref name="blue" /> is less than 0 or greater than 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000203 RID: 515 RVA: 0x00005B18 File Offset: 0x00003D18
		public static Color FromArgb(int alpha, int red, int green, int blue)
		{
			Color.CheckARGBValues(alpha, red, green, blue);
			return new Color
			{
				state = 2,
				Value = (long)((alpha << 24) + (red << 16) + (green << 8) + blue)
			};
		}

		/// <summary>Gets the 32-bit ARGB value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The 32-bit ARGB value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000204 RID: 516 RVA: 0x00005B56 File Offset: 0x00003D56
		public int ToArgb()
		{
			return (int)this.Value;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified <see cref="T:System.Drawing.Color" /> structure, but with the new specified alpha value. Although this method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="alpha">The alpha value for the new <see cref="T:System.Drawing.Color" />. Valid values are 0 through 255. </param>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> from which to create the new <see cref="T:System.Drawing.Color" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="alpha" /> is less than 0 or greater than 255.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000205 RID: 517 RVA: 0x00005B5F File Offset: 0x00003D5F
		public static Color FromArgb(int alpha, Color baseColor)
		{
			return Color.FromArgb(alpha, (int)baseColor.R, (int)baseColor.G, (int)baseColor.B);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from a 32-bit ARGB value.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> structure that this method creates.</returns>
		/// <param name="argb">A value specifying the 32-bit ARGB value. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000206 RID: 518 RVA: 0x00005B7C File Offset: 0x00003D7C
		public static Color FromArgb(int argb)
		{
			return Color.FromArgb((argb >> 24) & 255, (argb >> 16) & 255, (argb >> 8) & 255, argb & 255);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified predefined color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="color">An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000207 RID: 519 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public static Color FromKnownColor(KnownColor color)
		{
			short num = (short)color;
			Color color2;
			if (num <= 0 || (int)num >= KnownColors.ArgbValues.Length)
			{
				color2 = Color.FromArgb(0, 0, 0, 0);
				color2.state |= 4;
			}
			else
			{
				color2 = default(Color);
				color2.state = 7;
				if (num < 27 || num > 169)
				{
					color2.state |= 8;
				}
				color2.Value = (long)((ulong)KnownColors.ArgbValues[(int)num]);
			}
			color2.knownColor = num;
			return color2;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Color" /> structure from the specified name of a predefined color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that this method creates.</returns>
		/// <param name="name">A string that is the name of a predefined color. Valid names are the same as the names of the elements of the <see cref="T:System.Drawing.KnownColor" /> enumeration. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000208 RID: 520 RVA: 0x00005C24 File Offset: 0x00003E24
		public static Color FromName(string name)
		{
			Color color;
			try
			{
				color = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), name, true));
			}
			catch
			{
				Color color2 = Color.FromArgb(0, 0, 0, 0);
				color2.name = name;
				color2.state |= 4;
				color = color2;
			}
			return color;
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the equality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the equality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000209 RID: 521 RVA: 0x00005C84 File Offset: 0x00003E84
		public static bool operator ==(Color left, Color right)
		{
			return left.Value == right.Value && left.IsNamedColor == right.IsNamedColor && left.IsSystemColor == right.IsSystemColor && left.IsEmpty == right.IsEmpty && (!left.IsNamedColor || !(left.Name != right.Name));
		}

		/// <summary>Tests whether two specified <see cref="T:System.Drawing.Color" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Drawing.Color" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Drawing.Color" /> that is to the left of the inequality operator. </param>
		/// <param name="right">The <see cref="T:System.Drawing.Color" /> that is to the right of the inequality operator. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x0600020A RID: 522 RVA: 0x00005CFA File Offset: 0x00003EFA
		public static bool operator !=(Color left, Color right)
		{
			return !(left == right);
		}

		/// <summary>Gets the hue-saturation-brightness (HSB) brightness value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The brightness of this <see cref="T:System.Drawing.Color" />. The brightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600020B RID: 523 RVA: 0x00005D08 File Offset: 0x00003F08
		public float GetBrightness()
		{
			byte b = Math.Min(this.R, Math.Min(this.G, this.B));
			return (float)(Math.Max(this.R, Math.Max(this.G, this.B)) + b) / 510f;
		}

		/// <summary>Gets the hue-saturation-brightness (HSB) saturation value for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The saturation of this <see cref="T:System.Drawing.Color" />. The saturation ranges from 0.0 through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600020C RID: 524 RVA: 0x00005D58 File Offset: 0x00003F58
		public float GetSaturation()
		{
			byte b = Math.Min(this.R, Math.Min(this.G, this.B));
			byte b2 = Math.Max(this.R, Math.Max(this.G, this.B));
			if (b2 == b)
			{
				return 0f;
			}
			int num = (int)(b2 + b);
			if (num > 255)
			{
				num = 510 - num;
			}
			return (float)(b2 - b) / (float)num;
		}

		/// <summary>Gets the hue-saturation-brightness (HSB) hue value, in degrees, for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The hue, in degrees, of this <see cref="T:System.Drawing.Color" />. The hue is measured in degrees, ranging from 0.0 through 360.0, in HSB color space.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600020D RID: 525 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public float GetHue()
		{
			int r = (int)this.R;
			int g = (int)this.G;
			int b = (int)this.B;
			byte b2 = (byte)Math.Min(r, Math.Min(g, b));
			byte b3 = (byte)Math.Max(r, Math.Max(g, b));
			if (b3 == b2)
			{
				return 0f;
			}
			float num = (float)(b3 - b2);
			float num2 = (float)((int)b3 - r) / num;
			float num3 = (float)((int)b3 - g) / num;
			float num4 = (float)((int)b3 - b) / num;
			float num5 = 0f;
			if (r == (int)b3)
			{
				num5 = 60f * (6f + num4 - num3);
			}
			if (g == (int)b3)
			{
				num5 = 60f * (2f + num2 - num4);
			}
			if (b == (int)b3)
			{
				num5 = 60f * (4f + num3 - num2);
			}
			if (num5 > 360f)
			{
				num5 -= 360f;
			}
			return num5;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.KnownColor" /> value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An element of the <see cref="T:System.Drawing.KnownColor" /> enumeration, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600020E RID: 526 RVA: 0x00005E99 File Offset: 0x00004099
		public KnownColor ToKnownColor()
		{
			return (KnownColor)this.knownColor;
		}

		/// <summary>Specifies whether this <see cref="T:System.Drawing.Color" /> structure is uninitialized.</summary>
		/// <returns>This property returns true if this color is uninitialized; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005EA1 File Offset: 0x000040A1
		public bool IsEmpty
		{
			get
			{
				return this.state == 0;
			}
		}

		/// <summary>Gets the alpha component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The alpha component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005EAC File Offset: 0x000040AC
		public byte A
		{
			get
			{
				return (byte)(this.Value >> 24);
			}
		}

		/// <summary>Gets the red component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The red component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005EB8 File Offset: 0x000040B8
		public byte R
		{
			get
			{
				return (byte)(this.Value >> 16);
			}
		}

		/// <summary>Gets the green component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The green component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005EC4 File Offset: 0x000040C4
		public byte G
		{
			get
			{
				return (byte)(this.Value >> 8);
			}
		}

		/// <summary>Gets the blue component value of this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>The blue component value of this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005ECF File Offset: 0x000040CF
		public byte B
		{
			get
			{
				return (byte)this.Value;
			}
		}

		/// <summary>Tests whether the specified object is a <see cref="T:System.Drawing.Color" /> structure and is equivalent to this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Drawing.Color" /> structure equivalent to this <see cref="T:System.Drawing.Color" /> structure; otherwise, false.</returns>
		/// <param name="obj">The object to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000214 RID: 532 RVA: 0x00005ED8 File Offset: 0x000040D8
		public override bool Equals(object obj)
		{
			if (!(obj is Color))
			{
				return false;
			}
			Color color = (Color)obj;
			return this == color;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <returns>An integer value that specifies the hash code for this <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000215 RID: 533 RVA: 0x00005F04 File Offset: 0x00004104
		public override int GetHashCode()
		{
			int num = (int)(this.Value ^ (this.Value >> 32) ^ (long)this.state ^ (long)(this.knownColor >> 16));
			if (this.IsNamedColor)
			{
				num ^= this.Name.GetHashCode();
			}
			return num;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Color" /> structure to a human-readable string.</summary>
		/// <returns>A string that is the name of this <see cref="T:System.Drawing.Color" />, if the <see cref="T:System.Drawing.Color" /> is created from a predefined color by using either the <see cref="M:System.Drawing.Color.FromName(System.String)" /> method or the <see cref="M:System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor)" /> method; otherwise, a string that consists of the ARGB component names and their values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000216 RID: 534 RVA: 0x00005F50 File Offset: 0x00004150
		public override string ToString()
		{
			if (this.IsEmpty)
			{
				return "Color [Empty]";
			}
			if (this.IsNamedColor)
			{
				return "Color [" + this.Name + "]";
			}
			return string.Format("Color [A={0}, R={1}, G={2}, B={3}]", new object[] { this.A, this.R, this.G, this.B });
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005FD4 File Offset: 0x000041D4
		private static void CheckRGBValues(int red, int green, int blue)
		{
			if (red > 255 || red < 0)
			{
				throw Color.CreateColorArgumentException(red, "red");
			}
			if (green > 255 || green < 0)
			{
				throw Color.CreateColorArgumentException(green, "green");
			}
			if (blue > 255 || blue < 0)
			{
				throw Color.CreateColorArgumentException(blue, "blue");
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006029 File Offset: 0x00004229
		private static ArgumentException CreateColorArgumentException(int value, string color)
		{
			return new ArgumentException(string.Format("'{0}' is not a valid value for '{1}'. '{1}' should be greater or equal to 0 and less than or equal to 255.", value, color));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006041 File Offset: 0x00004241
		private static void CheckARGBValues(int alpha, int red, int green, int blue)
		{
			if (alpha > 255 || alpha < 0)
			{
				throw Color.CreateColorArgumentException(alpha, "alpha");
			}
			Color.CheckRGBValues(red, green, blue);
		}

		/// <summary>Gets a system-defined color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006063 File Offset: 0x00004263
		public static Color Transparent
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Transparent);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000606C File Offset: 0x0000426C
		public static Color AliceBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.AliceBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAEBD7.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006075 File Offset: 0x00004275
		public static Color AntiqueWhite
		{
			get
			{
				return Color.FromKnownColor(KnownColor.AntiqueWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000607E File Offset: 0x0000427E
		public static Color Aqua
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Aqua);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFFD4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006087 File Offset: 0x00004287
		public static Color Aquamarine
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Aquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006090 File Offset: 0x00004290
		public static Color Azure
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Azure);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006099 File Offset: 0x00004299
		public static Color Beige
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Beige);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4C4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000060A2 File Offset: 0x000042A2
		public static Color Bisque
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Bisque);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000060AB File Offset: 0x000042AB
		public static Color Black
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Black);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEBCD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000060B4 File Offset: 0x000042B4
		public static Color BlanchedAlmond
		{
			get
			{
				return Color.FromKnownColor(KnownColor.BlanchedAlmond);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000060BD File Offset: 0x000042BD
		public static Color Blue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Blue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8A2BE2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000060C6 File Offset: 0x000042C6
		public static Color BlueViolet
		{
			get
			{
				return Color.FromKnownColor(KnownColor.BlueViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA52A2A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000060CF File Offset: 0x000042CF
		public static Color Brown
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Brown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDEB887.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000060D8 File Offset: 0x000042D8
		public static Color BurlyWood
		{
			get
			{
				return Color.FromKnownColor(KnownColor.BurlyWood);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF5F9EA0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000060E1 File Offset: 0x000042E1
		public static Color CadetBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.CadetBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7FFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000060EA File Offset: 0x000042EA
		public static Color Chartreuse
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Chartreuse);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2691E.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000060F3 File Offset: 0x000042F3
		public static Color Chocolate
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Chocolate);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF7F50.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000060FC File Offset: 0x000042FC
		public static Color Coral
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Coral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6495ED.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006105 File Offset: 0x00004305
		public static Color CornflowerBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.CornflowerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF8DC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000610E File Offset: 0x0000430E
		public static Color Cornsilk
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Cornsilk);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDC143C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006117 File Offset: 0x00004317
		public static Color Crimson
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Crimson);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00006120 File Offset: 0x00004320
		public static Color Cyan
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Cyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006129 File Offset: 0x00004329
		public static Color DarkBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008B8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00006132 File Offset: 0x00004332
		public static Color DarkCyan
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB8860B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000613B File Offset: 0x0000433B
		public static Color DarkGoldenrod
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA9A9A9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006144 File Offset: 0x00004344
		public static Color DarkGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF006400.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000614D File Offset: 0x0000434D
		public static Color DarkGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBDB76B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00006156 File Offset: 0x00004356
		public static Color DarkKhaki
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkKhaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B008B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000615F File Offset: 0x0000435F
		public static Color DarkMagenta
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkMagenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF556B2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006168 File Offset: 0x00004368
		public static Color DarkOliveGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkOliveGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF8C00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00006171 File Offset: 0x00004371
		public static Color DarkOrange
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkOrange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9932CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000617A File Offset: 0x0000437A
		public static Color DarkOrchid
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00006183 File Offset: 0x00004383
		public static Color DarkRed
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE9967A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000618C File Offset: 0x0000438C
		public static Color DarkSalmon
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8FBC8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006195 File Offset: 0x00004395
		public static Color DarkSeaGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF483D8B.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000619E File Offset: 0x0000439E
		public static Color DarkSlateBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2F4F4F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000061A7 File Offset: 0x000043A7
		public static Color DarkSlateGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00CED1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000061B0 File Offset: 0x000043B0
		public static Color DarkTurquoise
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9400D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000061B9 File Offset: 0x000043B9
		public static Color DarkViolet
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DarkViolet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF1493.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000061C2 File Offset: 0x000043C2
		public static Color DeepPink
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DeepPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00BFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000061CB File Offset: 0x000043CB
		public static Color DeepSkyBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DeepSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF696969.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000061D4 File Offset: 0x000043D4
		public static Color DimGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DimGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF1E90FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000061DD File Offset: 0x000043DD
		public static Color DodgerBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.DodgerBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB22222.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000061E6 File Offset: 0x000043E6
		public static Color Firebrick
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Firebrick);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000061EF File Offset: 0x000043EF
		public static Color FloralWhite
		{
			get
			{
				return Color.FromKnownColor(KnownColor.FloralWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF228B22.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000061F8 File Offset: 0x000043F8
		public static Color ForestGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.ForestGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00006201 File Offset: 0x00004401
		public static Color Fuchsia
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Fuchsia);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDCDCDC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000620A File Offset: 0x0000440A
		public static Color Gainsboro
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Gainsboro);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF8F8FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006213 File Offset: 0x00004413
		public static Color GhostWhite
		{
			get
			{
				return Color.FromKnownColor(KnownColor.GhostWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFD700.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000621C File Offset: 0x0000441C
		public static Color Gold
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Gold);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDAA520.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006225 File Offset: 0x00004425
		public static Color Goldenrod
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Goldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> strcture representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000622E File Offset: 0x0000442E
		public static Color Gray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Gray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00006237 File Offset: 0x00004437
		public static Color Green
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Green);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADFF2F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00006240 File Offset: 0x00004440
		public static Color GreenYellow
		{
			get
			{
				return Color.FromKnownColor(KnownColor.GreenYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0FFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006249 File Offset: 0x00004449
		public static Color Honeydew
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Honeydew);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF69B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006252 File Offset: 0x00004452
		public static Color HotPink
		{
			get
			{
				return Color.FromKnownColor(KnownColor.HotPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD5C5C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000625B File Offset: 0x0000445B
		public static Color IndianRed
		{
			get
			{
				return Color.FromKnownColor(KnownColor.IndianRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4B0082.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00006264 File Offset: 0x00004464
		public static Color Indigo
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Indigo);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFF0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000626D File Offset: 0x0000446D
		public static Color Ivory
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Ivory);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF0E68C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006276 File Offset: 0x00004476
		public static Color Khaki
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Khaki);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE6E6FA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000627F File Offset: 0x0000447F
		public static Color Lavender
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Lavender);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF0F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00006288 File Offset: 0x00004488
		public static Color LavenderBlush
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LavenderBlush);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7CFC00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006291 File Offset: 0x00004491
		public static Color LawnGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LawnGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000629A File Offset: 0x0000449A
		public static Color LemonChiffon
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LemonChiffon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFADD8E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000062A3 File Offset: 0x000044A3
		public static Color LightBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF08080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000062AC File Offset: 0x000044AC
		public static Color LightCoral
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightCoral);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFE0FFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000062B5 File Offset: 0x000044B5
		public static Color LightCyan
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightCyan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAFAD2.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000062BE File Offset: 0x000044BE
		public static Color LightGoldenrodYellow
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightGoldenrodYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF90EE90.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000062C7 File Offset: 0x000044C7
		public static Color LightGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD3D3D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000062D0 File Offset: 0x000044D0
		public static Color LightGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFB6C1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000062D9 File Offset: 0x000044D9
		public static Color LightPink
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightPink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA07A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000062E2 File Offset: 0x000044E2
		public static Color LightSalmon
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightSalmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF20B2AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000062EB File Offset: 0x000044EB
		public static Color LightSeaGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000062F4 File Offset: 0x000044F4
		public static Color LightSkyBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightSkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF778899.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000062FD File Offset: 0x000044FD
		public static Color LightSlateGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightSlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0C4DE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00006306 File Offset: 0x00004506
		public static Color LightSteelBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightSteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFE0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000630F File Offset: 0x0000450F
		public static Color LightYellow
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LightYellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00006318 File Offset: 0x00004518
		public static Color Lime
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Lime);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF32CD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00006321 File Offset: 0x00004521
		public static Color LimeGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.LimeGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFAF0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000632A File Offset: 0x0000452A
		public static Color Linen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Linen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF00FF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00006333 File Offset: 0x00004533
		public static Color Magenta
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Magenta);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000633C File Offset: 0x0000453C
		public static Color Maroon
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Maroon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF66CDAA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006345 File Offset: 0x00004545
		public static Color MediumAquamarine
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumAquamarine);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF0000CD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000634E File Offset: 0x0000454E
		public static Color MediumBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBA55D3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00006357 File Offset: 0x00004557
		public static Color MediumOrchid
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumOrchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9370DB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00006360 File Offset: 0x00004560
		public static Color MediumPurple
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumPurple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF3CB371.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00006369 File Offset: 0x00004569
		public static Color MediumSeaGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumSeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF7B68EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00006372 File Offset: 0x00004572
		public static Color MediumSlateBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumSlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FA9A.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000637B File Offset: 0x0000457B
		public static Color MediumSpringGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumSpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF48D1CC.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00006384 File Offset: 0x00004584
		public static Color MediumTurquoise
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC71585.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000638D File Offset: 0x0000458D
		public static Color MediumVioletRed
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MediumVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF191970.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00006396 File Offset: 0x00004596
		public static Color MidnightBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MidnightBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5FFFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000639F File Offset: 0x0000459F
		public static Color MintCream
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MintCream);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000063A8 File Offset: 0x000045A8
		public static Color MistyRose
		{
			get
			{
				return Color.FromKnownColor(KnownColor.MistyRose);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFE4B5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000063B1 File Offset: 0x000045B1
		public static Color Moccasin
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Moccasin);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDEAD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000063BA File Offset: 0x000045BA
		public static Color NavajoWhite
		{
			get
			{
				return Color.FromKnownColor(KnownColor.NavajoWhite);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF000080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000063C3 File Offset: 0x000045C3
		public static Color Navy
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Navy);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFDF5E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000063CC File Offset: 0x000045CC
		public static Color OldLace
		{
			get
			{
				return Color.FromKnownColor(KnownColor.OldLace);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF808000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000063D5 File Offset: 0x000045D5
		public static Color Olive
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Olive);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6B8E23.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000063DE File Offset: 0x000045DE
		public static Color OliveDrab
		{
			get
			{
				return Color.FromKnownColor(KnownColor.OliveDrab);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFA500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000063E7 File Offset: 0x000045E7
		public static Color Orange
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Orange);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF4500.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000063F0 File Offset: 0x000045F0
		public static Color OrangeRed
		{
			get
			{
				return Color.FromKnownColor(KnownColor.OrangeRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDA70D6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000063FC File Offset: 0x000045FC
		public static Color Orchid
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Orchid);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEEE8AA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00006408 File Offset: 0x00004608
		public static Color PaleGoldenrod
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PaleGoldenrod);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF98FB98.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00006414 File Offset: 0x00004614
		public static Color PaleGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PaleGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFAFEEEE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00006420 File Offset: 0x00004620
		public static Color PaleTurquoise
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PaleTurquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDB7093.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000642C File Offset: 0x0000462C
		public static Color PaleVioletRed
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PaleVioletRed);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFEFD5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00006438 File Offset: 0x00004638
		public static Color PapayaWhip
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PapayaWhip);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFDAB9.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00006444 File Offset: 0x00004644
		public static Color PeachPuff
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PeachPuff);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFCD853F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00006450 File Offset: 0x00004650
		public static Color Peru
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Peru);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFC0CB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000645C File Offset: 0x0000465C
		public static Color Pink
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Pink);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFDDA0DD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00006468 File Offset: 0x00004668
		public static Color Plum
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Plum);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFB0E0E6.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00006474 File Offset: 0x00004674
		public static Color PowderBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.PowderBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF800080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00006480 File Offset: 0x00004680
		public static Color Purple
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Purple);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF0000.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000648C File Offset: 0x0000468C
		public static Color Red
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Red);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFBC8F8F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00006498 File Offset: 0x00004698
		public static Color RosyBrown
		{
			get
			{
				return Color.FromKnownColor(KnownColor.RosyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4169E1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000064A4 File Offset: 0x000046A4
		public static Color RoyalBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.RoyalBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF8B4513.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000064B0 File Offset: 0x000046B0
		public static Color SaddleBrown
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SaddleBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFA8072.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000064BC File Offset: 0x000046BC
		public static Color Salmon
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Salmon);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF4A460.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000064C8 File Offset: 0x000046C8
		public static Color SandyBrown
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SandyBrown);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF2E8B57.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000064D4 File Offset: 0x000046D4
		public static Color SeaGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SeaGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFF5EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000064E0 File Offset: 0x000046E0
		public static Color SeaShell
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SeaShell);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFA0522D.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000064EC File Offset: 0x000046EC
		public static Color Sienna
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Sienna);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFC0C0C0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000064F8 File Offset: 0x000046F8
		public static Color Silver
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Silver);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF87CEEB.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00006504 File Offset: 0x00004704
		public static Color SkyBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SkyBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF6A5ACD.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00006510 File Offset: 0x00004710
		public static Color SlateBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SlateBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF708090.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000651C File Offset: 0x0000471C
		public static Color SlateGray
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SlateGray);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFAFA.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00006528 File Offset: 0x00004728
		public static Color Snow
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Snow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF00FF7F.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00006534 File Offset: 0x00004734
		public static Color SpringGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SpringGreen);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF4682B4.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00006540 File Offset: 0x00004740
		public static Color SteelBlue
		{
			get
			{
				return Color.FromKnownColor(KnownColor.SteelBlue);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD2B48C.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000654C File Offset: 0x0000474C
		public static Color Tan
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Tan);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF008080.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00006558 File Offset: 0x00004758
		public static Color Teal
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Teal);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFD8BFD8.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00006564 File Offset: 0x00004764
		public static Color Thistle
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Thistle);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFF6347.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00006570 File Offset: 0x00004770
		public static Color Tomato
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Tomato);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF40E0D0.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000657C File Offset: 0x0000477C
		public static Color Turquoise
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Turquoise);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFEE82EE.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006588 File Offset: 0x00004788
		public static Color Violet
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Violet);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5DEB3.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00006594 File Offset: 0x00004794
		public static Color Wheat
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Wheat);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFFFF.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000065A0 File Offset: 0x000047A0
		public static Color White
		{
			get
			{
				return Color.FromKnownColor(KnownColor.White);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFF5F5F5.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000065AC File Offset: 0x000047AC
		public static Color WhiteSmoke
		{
			get
			{
				return Color.FromKnownColor(KnownColor.WhiteSmoke);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FFFFFF00.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000065B8 File Offset: 0x000047B8
		public static Color Yellow
		{
			get
			{
				return Color.FromKnownColor(KnownColor.Yellow);
			}
		}

		/// <summary>Gets a system-defined color that has an ARGB value of #FF9ACD32.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing a system-defined color.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000065C4 File Offset: 0x000047C4
		public static Color YellowGreen
		{
			get
			{
				return Color.FromKnownColor(KnownColor.YellowGreen);
			}
		}

		// Token: 0x04000351 RID: 849
		private long value;

		// Token: 0x04000352 RID: 850
		internal short state;

		// Token: 0x04000353 RID: 851
		internal short knownColor;

		// Token: 0x04000354 RID: 852
		internal string name;

		/// <summary>Represents a color that is null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000355 RID: 853
		public static readonly Color Empty;

		// Token: 0x02000044 RID: 68
		[Flags]
		internal enum ColorType : short
		{
			// Token: 0x04000357 RID: 855
			Empty = 0,
			// Token: 0x04000358 RID: 856
			Known = 1,
			// Token: 0x04000359 RID: 857
			ARGB = 2,
			// Token: 0x0400035A RID: 858
			Named = 4,
			// Token: 0x0400035B RID: 859
			System = 8
		}
	}
}
