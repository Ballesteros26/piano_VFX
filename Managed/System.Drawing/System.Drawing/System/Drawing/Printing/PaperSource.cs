using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the paper tray from which the printer gets paper.</summary>
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class PaperSource
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSource" /> class. </summary>
		// Token: 0x06000A6F RID: 2671 RVA: 0x00016AFD File Offset: 0x00014CFD
		public PaperSource()
		{
			this._kind = PaperSourceKind.Custom;
			this._name = string.Empty;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00016B1B File Offset: 0x00014D1B
		internal PaperSource(PaperSourceKind kind, string name)
		{
			this._kind = kind;
			this._name = name;
		}

		/// <summary>Gets the paper source.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00016B31 File Offset: 0x00014D31
		public PaperSourceKind Kind
		{
			get
			{
				if (this._kind >= (PaperSourceKind)256)
				{
					return PaperSourceKind.Custom;
				}
				return this._kind;
			}
		}

		/// <summary>Gets or sets the integer representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value.</summary>
		/// <returns>The integer value representing one of the <see cref="T:System.Drawing.Printing.PaperSourceKind" /> values or a custom value. </returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00016B4C File Offset: 0x00014D4C
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00016B54 File Offset: 0x00014D54
		public int RawKind
		{
			get
			{
				return (int)this._kind;
			}
			set
			{
				this._kind = (PaperSourceKind)value;
			}
		}

		/// <summary>Gets or sets the name of the paper source.</summary>
		/// <returns>The name of the paper source.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00016B5D File Offset: 0x00014D5D
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00016B65 File Offset: 0x00014D65
		public string SourceName
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSource" /> in string form.</summary>
		/// <returns>A string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000A76 RID: 2678 RVA: 0x00016B70 File Offset: 0x00014D70
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSource ",
				this.SourceName,
				" Kind=",
				this.Kind.ToString(),
				"]"
			});
		}

		// Token: 0x040006CB RID: 1739
		private string _name;

		// Token: 0x040006CC RID: 1740
		private PaperSourceKind _kind;
	}
}
