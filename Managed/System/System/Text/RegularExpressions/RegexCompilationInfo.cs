using System;
using System.Runtime.Serialization;

namespace System.Text.RegularExpressions
{
	/// <summary>Provides information about a regular expression that is used to compile a regular expression to a stand-alone assembly. </summary>
	// Token: 0x02000141 RID: 321
	[Serializable]
	public class RegexCompilationInfo
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002CA51 File Offset: 0x0002AC51
		[OnDeserializing]
		private void InitMatchTimeoutDefaultForOldVersionDeserialization(StreamingContext unusedContext)
		{
			this.matchTimeout = Regex.DefaultMatchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression to be included in an assembly. </summary>
		/// <param name="pattern">The regular expression to compile. </param>
		/// <param name="options">The regular expression options to use when compiling the regular expression. </param>
		/// <param name="name">The name of the type that represents the compiled regular expression. </param>
		/// <param name="fullnamespace">The namespace to which the new type belongs. </param>
		/// <param name="ispublic">true to make the compiled regular expression publicly visible; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.-or-<paramref name="name" /> is null.-or-<paramref name="fullnamespace" /> is null.</exception>
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002CA5E File Offset: 0x0002AC5E
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic)
			: this(pattern, options, name, fullnamespace, ispublic, Regex.DefaultMatchTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression with a specified time-out value to be included in an assembly.</summary>
		/// <param name="pattern">The regular expression to compile.</param>
		/// <param name="options">The regular expression options to use when compiling the regular expression.</param>
		/// <param name="name">The name of the type that represents the compiled regular expression.</param>
		/// <param name="fullnamespace">The namespace to which the new type belongs.</param>
		/// <param name="ispublic">true to make the compiled regular expression publicly visible; otherwise, false.</param>
		/// <param name="matchTimeout">The default time-out interval for the regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.-or-<paramref name="name" /> is null.-or-<paramref name="fullnamespace" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002CA72 File Offset: 0x0002AC72
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic, TimeSpan matchTimeout)
		{
			this.Pattern = pattern;
			this.Name = name;
			this.Namespace = fullnamespace;
			this.options = options;
			this.isPublic = ispublic;
			this.MatchTimeout = matchTimeout;
		}

		/// <summary>Gets or sets the regular expression to compile.</summary>
		/// <returns>The regular expression to compile.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0002CAA7 File Offset: 0x0002ACA7
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.pattern = value;
			}
		}

		/// <summary>Gets or sets the options to use when compiling the regular expression.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0002CAC6 File Offset: 0x0002ACC6
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0002CACE File Offset: 0x0002ACCE
		public RegexOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Gets or sets the name of the type that represents the compiled regular expression.</summary>
		/// <returns>The name of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value for this property is an empty string.</exception>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0002CAD7 File Offset: 0x0002ACD7
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(global::SR.GetString("Argument {0} cannot be null or zero-length.", new object[] { "value" }), "value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the namespace to which the new type belongs.</summary>
		/// <returns>The namespace of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0002CB2D File Offset: 0x0002AD2D
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0002CB35 File Offset: 0x0002AD35
		public string Namespace
		{
			get
			{
				return this.nspace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.nspace = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the compiled regular expression has public visibility.</summary>
		/// <returns>true if the regular expression has public visibility; otherwise, false.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0002CB4C File Offset: 0x0002AD4C
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0002CB54 File Offset: 0x0002AD54
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		/// <summary>Gets or sets the regular expression's default time-out interval.</summary>
		/// <returns>The default maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0002CB5D File Offset: 0x0002AD5D
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0002CB65 File Offset: 0x0002AD65
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.matchTimeout;
			}
			set
			{
				Regex.ValidateMatchTimeout(value);
				this.matchTimeout = value;
			}
		}

		// Token: 0x04000E4D RID: 3661
		private string pattern;

		// Token: 0x04000E4E RID: 3662
		private RegexOptions options;

		// Token: 0x04000E4F RID: 3663
		private string name;

		// Token: 0x04000E50 RID: 3664
		private string nspace;

		// Token: 0x04000E51 RID: 3665
		private bool isPublic;

		// Token: 0x04000E52 RID: 3666
		[OptionalField(VersionAdded = 2)]
		private TimeSpan matchTimeout;
	}
}
