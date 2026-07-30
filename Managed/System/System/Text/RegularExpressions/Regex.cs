using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents an immutable regular expression.</summary>
	// Token: 0x02000133 RID: 307
	[Serializable]
	public class Regex : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</summary>
		// Token: 0x06000857 RID: 2135 RVA: 0x0002873F File Offset: 0x0002693F
		protected Regex()
		{
			this.internalMatchTimeout = Regex.DefaultMatchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		// Token: 0x06000858 RID: 2136 RVA: 0x00028752 File Offset: 0x00026952
		public Regex(string pattern)
			: this(pattern, RegexOptions.None, Regex.DefaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> contains an invalid flag.</exception>
		// Token: 0x06000859 RID: 2137 RVA: 0x00028762 File Offset: 0x00026962
		public Regex(string pattern, RegexOptions options)
			: this(pattern, options, Regex.DefaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern and a value that specifies how long a pattern matching method should attempt a match before it times out.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x0600085A RID: 2138 RVA: 0x00028772 File Offset: 0x00026972
		public Regex(string pattern, RegexOptions options, TimeSpan matchTimeout)
			: this(pattern, options, matchTimeout, false)
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00028780 File Offset: 0x00026980
		private Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, bool useCache)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (options < RegexOptions.None || options >> 10 != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			Regex.ValidateMatchTimeout(matchTimeout);
			string text;
			if ((options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				text = CultureInfo.InvariantCulture.ToString();
			}
			else
			{
				text = CultureInfo.CurrentCulture.ToString();
			}
			string[] array = new string[5];
			int num = 0;
			int num2 = (int)options;
			array[num] = num2.ToString(NumberFormatInfo.InvariantInfo);
			array[1] = ":";
			array[2] = text;
			array[3] = ":";
			array[4] = pattern;
			string text2 = string.Concat(array);
			CachedCodeEntry cachedCodeEntry = Regex.LookupCachedAndUpdate(text2);
			this.pattern = pattern;
			this.roptions = options;
			this.internalMatchTimeout = matchTimeout;
			if (cachedCodeEntry == null)
			{
				RegexTree regexTree = RegexParser.Parse(pattern, this.roptions);
				this.capnames = regexTree._capnames;
				this.capslist = regexTree._capslist;
				this.code = RegexWriter.Write(regexTree);
				this.caps = this.code._caps;
				this.capsize = this.code._capsize;
				this.InitializeReferences();
				if (useCache)
				{
					cachedCodeEntry = this.CacheCode(text2);
				}
			}
			else
			{
				this.caps = cachedCodeEntry._caps;
				this.capnames = cachedCodeEntry._capnames;
				this.capslist = cachedCodeEntry._capslist;
				this.capsize = cachedCodeEntry._capsize;
				this.code = cachedCodeEntry._code;
				this.factory = cachedCodeEntry._factory;
				this.runnerref = cachedCodeEntry._runnerref;
				this.replref = cachedCodeEntry._replref;
				this.refsInitialized = true;
			}
			if (this.UseOptionC() && this.factory == null)
			{
				this.factory = this.Compile(this.code, this.roptions);
				if (useCache && cachedCodeEntry != null)
				{
					cachedCodeEntry.AddCompiled(this.factory);
				}
				this.code = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class by using serialized data.</summary>
		/// <param name="info">The object that contains a serialized pattern and <see cref="T:System.Text.RegularExpressions.RegexOptions" />  information.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">The pattern that <paramref name="info" /> contains is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="info" /> contains an invalid <see cref="T:System.Text.RegularExpressions.RegexOptions" />  flag.</exception>
		// Token: 0x0600085C RID: 2140 RVA: 0x00028968 File Offset: 0x00026B68
		protected Regex(SerializationInfo info, StreamingContext context)
			: this(info.GetString("pattern"), (RegexOptions)info.GetInt32("options"))
		{
			try
			{
				long @int = info.GetInt64("matchTimeout");
				TimeSpan timeSpan = new TimeSpan(@int);
				Regex.ValidateMatchTimeout(timeSpan);
				this.internalMatchTimeout = timeSpan;
			}
			catch (SerializationException)
			{
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.RegularExpressions.Regex" /> object.</summary>
		/// <param name="si">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. This parameter is reserved for future use.</param>
		// Token: 0x0600085D RID: 2141 RVA: 0x000289C8 File Offset: 0x00026BC8
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("pattern", this.ToString());
			si.AddValue("options", this.Options);
			si.AddValue("matchTimeout", this.MatchTimeout.Ticks);
		}

		/// <summary>Checks whether a time-out interval is within an acceptable range.</summary>
		/// <param name="matchTimeout">The time-out interval to check.</param>
		// Token: 0x0600085E RID: 2142 RVA: 0x00028A15 File Offset: 0x00026C15
		protected internal static void ValidateMatchTimeout(TimeSpan matchTimeout)
		{
			if (Regex.InfiniteMatchTimeout == matchTimeout)
			{
				return;
			}
			if (TimeSpan.Zero < matchTimeout && matchTimeout <= Regex.MaximumMatchTimeout)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("matchTimeout");
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00028A4C File Offset: 0x00026C4C
		private static TimeSpan InitDefaultMatchTimeout()
		{
			object data = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			if (data == null)
			{
				return Regex.FallbackDefaultMatchTimeout;
			}
			if (!(data is TimeSpan))
			{
				throw new InvalidCastException(global::SR.GetString("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", new object[] { "REGEX_DEFAULT_MATCH_TIMEOUT" }));
			}
			TimeSpan timeSpan = (TimeSpan)data;
			try
			{
				Regex.ValidateMatchTimeout(timeSpan);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentOutOfRangeException(global::SR.GetString("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", new object[] { "REGEX_DEFAULT_MATCH_TIMEOUT" }));
			}
			return timeSpan;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00028AD8 File Offset: 0x00026CD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal RegexRunnerFactory Compile(RegexCode code, RegexOptions roptions)
		{
			return RegexCompiler.Compile(code, roptions);
		}

		/// <summary>Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $,., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.</summary>
		/// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
		/// <param name="str">The input string that contains the text to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x06000861 RID: 2145 RVA: 0x00028AE1 File Offset: 0x00026CE1
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Escape(str);
		}

		/// <summary>Converts any escaped characters in the input string.</summary>
		/// <returns>A string of characters with any escaped characters converted to their unescaped form.</returns>
		/// <param name="str">The input string containing the text to convert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="str" /> includes an unrecognized escape sequence.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x06000862 RID: 2146 RVA: 0x00028AF7 File Offset: 0x00026CF7
		public static string Unescape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Unescape(str);
		}

		/// <summary>Gets or sets the maximum number of entries in the current static cache of compiled regular expressions.</summary>
		/// <returns>The maximum number of entries in the static cache.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.</exception>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00028B0D File Offset: 0x00026D0D
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00028B14 File Offset: 0x00026D14
		public static int CacheSize
		{
			get
			{
				return Regex.cacheSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Regex.cacheSize = value;
				if (Regex.livecode.Count > Regex.cacheSize)
				{
					LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
					lock (linkedList)
					{
						while (Regex.livecode.Count > Regex.cacheSize)
						{
							Regex.livecode.RemoveLast();
						}
					}
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00028B90 File Offset: 0x00026D90
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00028C0C File Offset: 0x00026E0C
		[CLSCompliant(false)]
		protected IDictionary Caps
		{
			get
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				foreach (object obj in this.caps.Keys)
				{
					int num = (int)obj;
					dictionary.Add(num, (int)this.caps[num]);
				}
				return dictionary;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.caps = new Hashtable(value.Count);
				foreach (object obj in value)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.caps[(int)dictionaryEntry.Key] = (int)dictionaryEntry.Value;
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00028CA8 File Offset: 0x00026EA8
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00028D20 File Offset: 0x00026F20
		[CLSCompliant(false)]
		protected IDictionary CapNames
		{
			get
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (object obj in this.capnames.Keys)
				{
					string text = (string)obj;
					dictionary.Add(text, (int)this.capnames[text]);
				}
				return dictionary;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.capnames = new Hashtable(value.Count);
				foreach (object obj in value)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this.capnames[(string)dictionaryEntry.Key] = (int)dictionaryEntry.Value;
				}
			}
		}

		/// <summary>Gets the options that were passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>One or more members of the <see cref="T:System.Text.RegularExpressions.RegexOptions" /> enumeration that represent options that were passed to the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor </returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00028DB4 File Offset: 0x00026FB4
		public RegexOptions Options
		{
			get
			{
				return this.roptions;
			}
		}

		/// <summary>Gets the time-out interval of the current instance.</summary>
		/// <returns>The maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00028DBC File Offset: 0x00026FBC
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.internalMatchTimeout;
			}
		}

		/// <summary>Gets a value that indicates whether the regular expression searches from right to left.</summary>
		/// <returns>true if the regular expression searches from right to left; otherwise, false.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00028DC4 File Offset: 0x00026FC4
		public bool RightToLeft
		{
			get
			{
				return this.UseOptionR();
			}
		}

		/// <summary>Returns the regular expression pattern that was passed into the Regex constructor.</summary>
		/// <returns>The <paramref name="pattern" /> parameter that was passed into the Regex constructor.</returns>
		// Token: 0x0600086C RID: 2156 RVA: 0x00028DCC File Offset: 0x00026FCC
		public override string ToString()
		{
			return this.pattern;
		}

		/// <summary>Returns an array of capturing group names for the regular expression.</summary>
		/// <returns>A string array of group names.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600086D RID: 2157 RVA: 0x00028DD4 File Offset: 0x00026FD4
		public string[] GetGroupNames()
		{
			string[] array;
			if (this.capslist == null)
			{
				int num = this.capsize;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = Convert.ToString(i, CultureInfo.InvariantCulture);
				}
			}
			else
			{
				array = new string[this.capslist.Length];
				Array.Copy(this.capslist, 0, array, 0, this.capslist.Length);
			}
			return array;
		}

		/// <summary>Returns an array of capturing group numbers that correspond to group names in an array.</summary>
		/// <returns>An integer array of group numbers.</returns>
		// Token: 0x0600086E RID: 2158 RVA: 0x00028E38 File Offset: 0x00027038
		public int[] GetGroupNumbers()
		{
			int[] array;
			if (this.caps == null)
			{
				int num = this.capsize;
				array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				array = new int[this.caps.Count];
				IDictionaryEnumerator enumerator = this.caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					array[(int)enumerator.Value] = (int)enumerator.Key;
				}
			}
			return array;
		}

		/// <summary>Gets the group name that corresponds to the specified group number.</summary>
		/// <returns>A string that contains the group name associated with the specified group number. If there is no group name that corresponds to <paramref name="i" />, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="i">The group number to convert to the corresponding group name. </param>
		// Token: 0x0600086F RID: 2159 RVA: 0x00028EB0 File Offset: 0x000270B0
		public string GroupNameFromNumber(int i)
		{
			if (this.capslist == null)
			{
				if (i >= 0 && i < this.capsize)
				{
					return i.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if (this.caps != null)
				{
					object obj = this.caps[i];
					if (obj == null)
					{
						return string.Empty;
					}
					i = (int)obj;
				}
				if (i >= 0 && i < this.capslist.Length)
				{
					return this.capslist[i];
				}
				return string.Empty;
			}
		}

		/// <summary>Returns the group number that corresponds to the specified group name.</summary>
		/// <returns>The group number that corresponds to the specified group name, or -1 if <paramref name="name" /> is not a valid group name.</returns>
		/// <param name="name">The group name to convert to the corresponding group number. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000870 RID: 2160 RVA: 0x00028F30 File Offset: 0x00027130
		public int GroupNumberFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.capnames != null)
			{
				object obj = this.capnames[name];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			else
			{
				int num = 0;
				foreach (char c in name)
				{
					if (c > '9' || c < '0')
					{
						return -1;
					}
					num *= 10;
					num += (int)(c - '0');
				}
				if (num >= 0 && num < this.capsize)
				{
					return num;
				}
				return -1;
			}
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null. </exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000871 RID: 2161 RVA: 0x00028FB1 File Offset: 0x000271B1
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" />  value.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000872 RID: 2162 RVA: 0x00028FC0 File Offset: 0x000271C0
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			return Regex.IsMatch(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options and time-out interval.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000873 RID: 2163 RVA: 0x00028FCF File Offset: 0x000271CF
		public static bool IsMatch(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).IsMatch(input);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in a specified input string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000874 RID: 2164 RVA: 0x00028FE0 File Offset: 0x000271E0
		public bool IsMatch(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.IsMatch(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the specified input string, beginning at the specified starting position in the string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000875 RID: 2165 RVA: 0x00029008 File Offset: 0x00027208
		public bool IsMatch(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(true, -1, input, 0, input.Length, startat) == null;
		}

		/// <summary>Searches the specified input string for the first occurrence of the specified regular expression.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000876 RID: 2166 RVA: 0x0002902C File Offset: 0x0002722C
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000877 RID: 2167 RVA: 0x0002903B File Offset: 0x0002723B
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			return Regex.Match(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000878 RID: 2168 RVA: 0x0002904A File Offset: 0x0002724A
		public static Match Match(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Match(input);
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000879 RID: 2169 RVA: 0x0002905B File Offset: 0x0002725B
		public Match Match(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Match(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The zero-based character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600087A RID: 2170 RVA: 0x00029083 File Offset: 0x00027283
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, 0, input.Length, startat);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position and searching only the specified number of characters.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="beginning">The zero-based character position in the input string that defines the leftmost position to be searched. </param>
		/// <param name="length">The number of characters in the substring to include in the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="beginning" /> is less than zero or greater than the length of <paramref name="input" />.-or-<paramref name="length" /> is less than zero or greater than the length of <paramref name="input" />.-or-<paramref name="beginning" />+<paramref name="length" />– 1 identifies a position that is outside the range of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600087B RID: 2171 RVA: 0x000290A4 File Offset: 0x000272A4
		public Match Match(string input, int beginning, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, beginning, length, this.UseOptionR() ? (beginning + length) : beginning);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600087C RID: 2172 RVA: 0x000290CD File Offset: 0x000272CD
		public static MatchCollection Matches(string input, string pattern)
		{
			return Regex.Matches(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600087D RID: 2173 RVA: 0x000290DC File Offset: 0x000272DC
		public static MatchCollection Matches(string input, string pattern, RegexOptions options)
		{
			return Regex.Matches(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x0600087E RID: 2174 RVA: 0x000290EB File Offset: 0x000272EB
		public static MatchCollection Matches(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Matches(input);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600087F RID: 2175 RVA: 0x000290FC File Offset: 0x000272FC
		public MatchCollection Matches(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Matches(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position in the input string at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000880 RID: 2176 RVA: 0x00029124 File Offset: 0x00027324
		public MatchCollection Matches(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return new MatchCollection(this, input, 0, input.Length, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000881 RID: 2177 RVA: 0x00029143 File Offset: 0x00027343
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000882 RID: 2178 RVA: 0x00029153 File Offset: 0x00027353
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			return Regex.Replace(input, pattern, replacement, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000883 RID: 2179 RVA: 0x00029163 File Offset: 0x00027363
		public static string Replace(string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, replacement);
		}

		/// <summary>In a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000884 RID: 2180 RVA: 0x00029176 File Offset: 0x00027376
		public string Replace(string input, string replacement)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">The maximum number of times the replacement can occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000885 RID: 2181 RVA: 0x000291A0 File Offset: 0x000273A0
		public string Replace(string input, string replacement, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">Maximum number of times the replacement can occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000886 RID: 2182 RVA: 0x000291CC File Offset: 0x000273CC
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			RegexReplacement regexReplacement = (RegexReplacement)this.replref.Get();
			if (regexReplacement == null || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, this.caps, this.capsize, this.capnames, this.roptions);
				this.replref.Cache(regexReplacement);
			}
			return regexReplacement.Replace(this, input, count, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000887 RID: 2183 RVA: 0x0002924D File Offset: 0x0002744D
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Specified options modify the matching operation.</summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000888 RID: 2184 RVA: 0x0002925D File Offset: 0x0002745D
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
		{
			return Regex.Replace(input, pattern, evaluator, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all substrings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000889 RID: 2185 RVA: 0x0002926D File Offset: 0x0002746D
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, evaluator);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600088A RID: 2186 RVA: 0x00029280 File Offset: 0x00027480
		public string Replace(string input, MatchEvaluator evaluator)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600088B RID: 2187 RVA: 0x000292AA File Offset: 0x000274AA
		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600088C RID: 2188 RVA: 0x000292D4 File Offset: 0x000274D4
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Replace(evaluator, this, input, count, startat);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600088D RID: 2189 RVA: 0x000292EF File Offset: 0x000274EF
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None, Regex.DefaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Specified options modify the matching operation.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600088E RID: 2190 RVA: 0x000292FE File Offset: 0x000274FE
		public static string[] Split(string input, string pattern, RegexOptions options)
		{
			return Regex.Split(input, pattern, options, Regex.DefaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <returns>A string array.</returns>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.-or-<paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x0600088F RID: 2191 RVA: 0x0002930D File Offset: 0x0002750D
		public static string[] Split(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Split(input);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000890 RID: 2192 RVA: 0x0002931E File Offset: 0x0002751E
		public string[] Split(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Split(input, 0, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to be split. </param>
		/// <param name="count">The maximum number of times the split can occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000891 RID: 2193 RVA: 0x00029347 File Offset: 0x00027547
		public string[] Split(string input, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor. The search for the regular expression pattern starts at a specified character position in the input string.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to be split. </param>
		/// <param name="count">The maximum number of times the split can occur. </param>
		/// <param name="startat">The character position in the input string where the search will begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000892 RID: 2194 RVA: 0x00029370 File Offset: 0x00027570
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return RegexReplacement.Split(this, input, count, startat);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000893 RID: 2195 RVA: 0x00029389 File Offset: 0x00027589
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, null, null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000894 RID: 2196 RVA: 0x00029394 File Offset: 0x00027594
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects and a specified resource file to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly. </param>
		/// <param name="resourceFile">The name of the Win32 resource file to include in the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The <paramref name="resourceFile" /> parameter designates an invalid Win32 resource file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file designated by the <paramref name="resourceFile" /> parameter cannot be found.  </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000895 RID: 2197 RVA: 0x0002939F File Offset: 0x0002759F
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			Regex.CompileToAssemblyInternal(regexinfos, assemblyname, attributes, resourceFile);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000293AA File Offset: 0x000275AA
		private static void CompileToAssemblyInternal(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			if (assemblyname == null)
			{
				throw new ArgumentNullException("assemblyname");
			}
			if (regexinfos == null)
			{
				throw new ArgumentNullException("regexinfos");
			}
			RegexCompiler.CompileToAssembly(regexinfos, assemblyname, attributes, resourceFile);
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		/// <exception cref="T:System.NotSupportedException">References have already been initialized. </exception>
		// Token: 0x06000897 RID: 2199 RVA: 0x000293D1 File Offset: 0x000275D1
		protected void InitializeReferences()
		{
			if (this.refsInitialized)
			{
				throw new NotSupportedException(global::SR.GetString("This operation is only allowed once per object."));
			}
			this.refsInitialized = true;
			this.runnerref = new ExclusiveReference();
			this.replref = new SharedReference();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00029408 File Offset: 0x00027608
		internal Match Run(bool quick, int prevlen, string input, int beginning, int length, int startat)
		{
			RegexRunner regexRunner = null;
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("start", global::SR.GetString("Start index cannot be less than 0 or greater than input length."));
			}
			if (length < 0 || length > input.Length)
			{
				throw new ArgumentOutOfRangeException("length", global::SR.GetString("Length cannot be less than 0 or exceed input length."));
			}
			regexRunner = (RegexRunner)this.runnerref.Get();
			if (regexRunner == null)
			{
				if (this.factory != null)
				{
					regexRunner = this.factory.CreateInstance();
				}
				else
				{
					regexRunner = new RegexInterpreter(this.code, this.UseOptionInvariant() ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
				}
			}
			Match match;
			try
			{
				match = regexRunner.Scan(this, input, beginning, beginning + length, startat, prevlen, quick, this.internalMatchTimeout);
			}
			finally
			{
				this.runnerref.Release(regexRunner);
			}
			return match;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000294E4 File Offset: 0x000276E4
		private static CachedCodeEntry LookupCachedAndUpdate(string key)
		{
			LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
			lock (linkedList)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
			}
			return null;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002956C File Offset: 0x0002776C
		private CachedCodeEntry CacheCode(string key)
		{
			CachedCodeEntry cachedCodeEntry = null;
			LinkedList<CachedCodeEntry> linkedList = Regex.livecode;
			lock (linkedList)
			{
				for (LinkedListNode<CachedCodeEntry> linkedListNode = Regex.livecode.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (linkedListNode.Value._key == key)
					{
						Regex.livecode.Remove(linkedListNode);
						Regex.livecode.AddFirst(linkedListNode);
						return linkedListNode.Value;
					}
				}
				if (Regex.cacheSize != 0)
				{
					cachedCodeEntry = new CachedCodeEntry(key, this.capnames, this.capslist, this.code, this.caps, this.capsize, this.runnerref, this.replref);
					Regex.livecode.AddFirst(cachedCodeEntry);
					if (Regex.livecode.Count > Regex.cacheSize)
					{
						Regex.livecode.RemoveLast();
					}
				}
			}
			return cachedCodeEntry;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> option; otherwise, false.</returns>
		// Token: 0x0600089B RID: 2203 RVA: 0x00004240 File Offset: 0x00002440
		protected bool UseOptionC()
		{
			return false;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option; otherwise, false.</returns>
		// Token: 0x0600089C RID: 2204 RVA: 0x00029654 File Offset: 0x00027854
		protected bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00029662 File Offset: 0x00027862
		internal bool UseOptionInvariant()
		{
			return (this.roptions & RegexOptions.CultureInvariant) > RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DAA RID: 3498
		protected internal string pattern;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DAB RID: 3499
		protected internal RegexRunnerFactory factory;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DAC RID: 3500
		protected internal RegexOptions roptions;

		// Token: 0x04000DAD RID: 3501
		[NonSerialized]
		private static readonly TimeSpan MaximumMatchTimeout = TimeSpan.FromMilliseconds(2147483646.0);

		/// <summary>Specifies that a pattern-matching operation should not time out.</summary>
		// Token: 0x04000DAE RID: 3502
		[NonSerialized]
		public static readonly TimeSpan InfiniteMatchTimeout = Timeout.InfiniteTimeSpan;

		/// <summary>The maximum amount of time that can elapse in a pattern-matching operation before the operation times out.</summary>
		// Token: 0x04000DAF RID: 3503
		[OptionalField(VersionAdded = 2)]
		protected internal TimeSpan internalMatchTimeout;

		// Token: 0x04000DB0 RID: 3504
		private const string DefaultMatchTimeout_ConfigKeyName = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x04000DB1 RID: 3505
		[NonSerialized]
		internal static readonly TimeSpan FallbackDefaultMatchTimeout = Regex.InfiniteMatchTimeout;

		// Token: 0x04000DB2 RID: 3506
		[NonSerialized]
		internal static readonly TimeSpan DefaultMatchTimeout = Regex.InitDefaultMatchTimeout();

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DB3 RID: 3507
		protected internal Hashtable caps;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DB4 RID: 3508
		protected internal Hashtable capnames;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DB5 RID: 3509
		protected internal string[] capslist;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04000DB6 RID: 3510
		protected internal int capsize;

		// Token: 0x04000DB7 RID: 3511
		internal ExclusiveReference runnerref;

		// Token: 0x04000DB8 RID: 3512
		internal SharedReference replref;

		// Token: 0x04000DB9 RID: 3513
		internal RegexCode code;

		// Token: 0x04000DBA RID: 3514
		internal bool refsInitialized;

		// Token: 0x04000DBB RID: 3515
		internal static LinkedList<CachedCodeEntry> livecode = new LinkedList<CachedCodeEntry>();

		// Token: 0x04000DBC RID: 3516
		internal static int cacheSize = 15;

		// Token: 0x04000DBD RID: 3517
		internal const int MaxOptionShift = 10;
	}
}
