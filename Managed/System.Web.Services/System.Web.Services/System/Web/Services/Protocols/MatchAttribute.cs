using System;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the attributes of a match made using text pattern matching. This class cannot be inherited.</summary>
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MatchAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MatchAttribute" /> class with the specified pattern.</summary>
		/// <param name="pattern">A string that represents the pattern to match. </param>
		// Token: 0x06000161 RID: 353 RVA: 0x00006B39 File Offset: 0x00004D39
		public MatchAttribute(string pattern)
		{
			this.pattern = pattern;
		}

		/// <summary>Gets or sets a regular expression that represents the pattern to match.</summary>
		/// <returns>A regular expression that represents the pattern to match.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006B56 File Offset: 0x00004D56
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00006B6C File Offset: 0x00004D6C
		public string Pattern
		{
			get
			{
				if (this.pattern != null)
				{
					return this.pattern;
				}
				return string.Empty;
			}
			set
			{
				this.pattern = value;
			}
		}

		/// <summary>Gets or sets a value that represents a grouping of related matches.</summary>
		/// <returns>A value that represents a grouping of related matches </returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006B75 File Offset: 0x00004D75
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006B7D File Offset: 0x00004D7D
		public int Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets or sets a value that represents the index of a match within a grouping.</summary>
		/// <returns>A value that represents the index of a match within a grouping.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006B86 File Offset: 0x00004D86
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00006B8E File Offset: 0x00004D8E
		public int Capture
		{
			get
			{
				return this.capture;
			}
			set
			{
				this.capture = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the pattern to match is case insensitive.</summary>
		/// <returns>true if matching is case insensitive; otherwise, false. The default value is false.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006B97 File Offset: 0x00004D97
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00006B9F File Offset: 0x00004D9F
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				this.ignoreCase = value;
			}
		}

		/// <summary>Gets or sets the maximum number of values to return from the match.</summary>
		/// <returns>The maximum number of values to return from the match. The default value is -1, which refers to returning all values.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006BA8 File Offset: 0x00004DA8
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public int MaxRepeats
		{
			get
			{
				return this.repeats;
			}
			set
			{
				this.repeats = value;
			}
		}

		// Token: 0x04000211 RID: 529
		private string pattern;

		// Token: 0x04000212 RID: 530
		private int group = 1;

		// Token: 0x04000213 RID: 531
		private int capture;

		// Token: 0x04000214 RID: 532
		private bool ignoreCase;

		// Token: 0x04000215 RID: 533
		private int repeats = -1;
	}
}
