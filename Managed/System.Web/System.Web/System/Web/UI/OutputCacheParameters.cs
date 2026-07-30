using System;

namespace System.Web.UI
{
	/// <summary>Encapsulates the output cache initialization settings parsed from an @ OutputCache page directive by ASP.NET. This class cannot be inherited.</summary>
	// Token: 0x0200020B RID: 523
	public sealed class OutputCacheParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.OutputCacheParameters" /> class. </summary>
		// Token: 0x0600146B RID: 5227 RVA: 0x00036F1A File Offset: 0x0003511A
		public OutputCacheParameters()
		{
			this.Duration = 0;
			this.Enabled = true;
			this.Location = OutputCacheLocation.Any;
			this.NoStore = false;
		}

		/// <summary>Gets or sets an <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00036F3E File Offset: 0x0003513E
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00036F46 File Offset: 0x00035146
		public string CacheProfile
		{
			get
			{
				return this._cacheProfile;
			}
			set
			{
				this._cacheProfile = value;
			}
		}

		/// <summary>Gets or sets the amount of time that a cache entry is to remain in the output cache.</summary>
		/// <returns>The amount of time, in seconds, that a cache entry is to remain in the output cache. The default is 0, which indicates an infinite duration.</returns>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00036F4F File Offset: 0x0003514F
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00036F57 File Offset: 0x00035157
		public int Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether output caching is enabled for the current content.</summary>
		/// <returns>true if output caching is enabled for the current content; otherwise, false. The default is true.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00036F60 File Offset: 0x00035160
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00036F68 File Offset: 0x00035168
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		/// <summary>Gets or sets a value that determines the location of the cache entry.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00036F71 File Offset: 0x00035171
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00036F79 File Offset: 0x00035179
		public OutputCacheLocation Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._location = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether the HTTP Cache-Control: no-store directive is set.</summary>
		/// <returns>true if the Cache-Control: no-store directive is set on <see cref="T:System.Web.HttpResponse" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00036F82 File Offset: 0x00035182
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00036F8A File Offset: 0x0003518A
		public bool NoStore
		{
			get
			{
				return this._noStore;
			}
			set
			{
				this._noStore = value;
			}
		}

		/// <summary>Gets or sets a set of database and table name pairs that the cache entry depends on.</summary>
		/// <returns>A string that identifies a set of database and table name pairs that the cache entry depends on. The cache entry is expired when the table's data is updated or changes.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00036F93 File Offset: 0x00035193
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00036F9B File Offset: 0x0003519B
		public string SqlDependency
		{
			get
			{
				return this._sqlDependency;
			}
			set
			{
				this._sqlDependency = value;
			}
		}

		/// <summary>Gets or sets a comma-delimited set of character sets (content encodings) used to vary the cache entry.</summary>
		/// <returns>A list of character sets by which to vary the content.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00036FA4 File Offset: 0x000351A4
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00036FAC File Offset: 0x000351AC
		public string VaryByContentEncoding
		{
			get
			{
				return this._varyByContentEncoding;
			}
			set
			{
				this._varyByContentEncoding = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited set of control identifiers contained within the current page or user control used to vary the current cache entry.</summary>
		/// <returns>A semicolon-separated list of strings used to vary an entry's output cache. The <see cref="P:System.Web.UI.OutputCacheParameters.VaryByControl" /> property is set to fully qualified control identifiers, where the identifier is a concatenation of control IDs starting from the top-level parent control and delimited with a dollar sign ($) character.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00036FB5 File Offset: 0x000351B5
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x00036FBD File Offset: 0x000351BD
		public string VaryByControl
		{
			get
			{
				return this._varByControl;
			}
			set
			{
				this._varByControl = value;
			}
		}

		/// <summary>Gets a list of custom strings that the output cache uses to vary the cache entry.</summary>
		/// <returns>The list of custom strings.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00036FC6 File Offset: 0x000351C6
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x00036FCE File Offset: 0x000351CE
		public string VaryByCustom
		{
			get
			{
				return this._varByCustom;
			}
			set
			{
				this._varByCustom = value;
			}
		}

		/// <summary>Gets or sets a comma-delimited set of header names used to vary the cache entry. The header names identify HTTP headers associated with the request.</summary>
		/// <returns>A list of headers by which to vary the content.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00036FD7 File Offset: 0x000351D7
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00036FDF File Offset: 0x000351DF
		public string VaryByHeader
		{
			get
			{
				return this._varByHeader;
			}
			set
			{
				this._varByHeader = value;
			}
		}

		/// <summary>Gets a semicolon-delimited list of query string or form POST parameters that the output cache uses to vary the cache entry.</summary>
		/// <returns>The list of query string or form POST parameters.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00036FE8 File Offset: 0x000351E8
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00036FF0 File Offset: 0x000351F0
		public string VaryByParam
		{
			get
			{
				return this._varByParam;
			}
			set
			{
				this._varByParam = value;
			}
		}

		// Token: 0x040014A0 RID: 5280
		private string _cacheProfile;

		// Token: 0x040014A1 RID: 5281
		private int _duration;

		// Token: 0x040014A2 RID: 5282
		private bool _enabled;

		// Token: 0x040014A3 RID: 5283
		private OutputCacheLocation _location;

		// Token: 0x040014A4 RID: 5284
		private bool _noStore;

		// Token: 0x040014A5 RID: 5285
		private string _sqlDependency;

		// Token: 0x040014A6 RID: 5286
		private string _varByControl;

		// Token: 0x040014A7 RID: 5287
		private string _varByCustom;

		// Token: 0x040014A8 RID: 5288
		private string _varByHeader;

		// Token: 0x040014A9 RID: 5289
		private string _varByParam;

		// Token: 0x040014AA RID: 5290
		private string _varyByContentEncoding;
	}
}
