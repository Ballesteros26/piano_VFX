using System;
using System.Globalization;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the size of a piece of paper.</summary>
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class PaperSize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
		// Token: 0x06000A62 RID: 2658 RVA: 0x00016929 File Offset: 0x00014B29
		public PaperSize()
		{
			this._kind = PaperKind.Custom;
			this._name = string.Empty;
			this._createdByDefaultConstructor = true;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001694A File Offset: 0x00014B4A
		internal PaperSize(PaperKind kind, string name, int width, int height)
		{
			this._kind = kind;
			this._name = name;
			this._width = width;
			this._height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
		/// <param name="name">The name of the paper. </param>
		/// <param name="width">The width of the paper, in hundredths of an inch. </param>
		/// <param name="height">The height of the paper, in hundredths of an inch. </param>
		// Token: 0x06000A64 RID: 2660 RVA: 0x0001696F File Offset: 0x00014B6F
		public PaperSize(string name, int width, int height)
		{
			this._kind = PaperKind.Custom;
			this._name = name;
			this._width = width;
			this._height = height;
		}

		/// <summary>Gets or sets the height of the paper, in hundredths of an inch.</summary>
		/// <returns>The height of the paper, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />. </exception>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00016993 File Offset: 0x00014B93
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0001699B File Offset: 0x00014B9B
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if (this._kind != PaperKind.Custom && !this._createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.Format("PaperSize cannot be changed unless the Kind property is set to Custom.", Array.Empty<object>()));
				}
				this._height = value;
			}
		}

		/// <summary>Gets the type of paper.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PaperKind" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />. </exception>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000169C9 File Offset: 0x00014BC9
		public PaperKind Kind
		{
			get
			{
				if (this._kind <= PaperKind.PrcEnvelopeNumber10Rotated && this._kind != (PaperKind)48 && this._kind != (PaperKind)49)
				{
					return this._kind;
				}
				return PaperKind.Custom;
			}
		}

		/// <summary>Gets or sets the name of the type of paper.</summary>
		/// <returns>The name of the type of paper.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />. </exception>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000169F1 File Offset: 0x00014BF1
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x000169F9 File Offset: 0x00014BF9
		public string PaperName
		{
			get
			{
				return this._name;
			}
			set
			{
				if (this._kind != PaperKind.Custom && !this._createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.Format("PaperSize cannot be changed unless the Kind property is set to Custom.", Array.Empty<object>()));
				}
				this._name = value;
			}
		}

		/// <summary>Gets or sets an integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values or a custom value.</summary>
		/// <returns>An integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values, or a custom value.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00016A27 File Offset: 0x00014C27
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00016A2F File Offset: 0x00014C2F
		public int RawKind
		{
			get
			{
				return (int)this._kind;
			}
			set
			{
				this._kind = (PaperKind)value;
			}
		}

		/// <summary>Gets or sets the width of the paper, in hundredths of an inch.</summary>
		/// <returns>The width of the paper, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />. </exception>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00016A38 File Offset: 0x00014C38
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00016A40 File Offset: 0x00014C40
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if (this._kind != PaperKind.Custom && !this._createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.Format("PaperSize cannot be changed unless the Kind property is set to Custom.", Array.Empty<object>()));
				}
				this._width = value;
			}
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSize" /> in string form.</summary>
		/// <returns>A string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000A6E RID: 2670 RVA: 0x00016A70 File Offset: 0x00014C70
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSize ",
				this.PaperName,
				" Kind=",
				this.Kind.ToString(),
				" Height=",
				this.Height.ToString(CultureInfo.InvariantCulture),
				" Width=",
				this.Width.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x040006C6 RID: 1734
		private PaperKind _kind;

		// Token: 0x040006C7 RID: 1735
		private string _name;

		// Token: 0x040006C8 RID: 1736
		private int _width;

		// Token: 0x040006C9 RID: 1737
		private int _height;

		// Token: 0x040006CA RID: 1738
		private bool _createdByDefaultConstructor;
	}
}
