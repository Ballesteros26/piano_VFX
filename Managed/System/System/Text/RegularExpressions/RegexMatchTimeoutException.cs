using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	/// <summary>The exception that is thrown when the execution time of a regular expression pattern-matching method exceeds its time-out interval.</summary>
	// Token: 0x02000151 RID: 337
	[Serializable]
	public class RegexMatchTimeoutException : TimeoutException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with information about the regular expression pattern, the input text, and the time-out interval.</summary>
		/// <param name="regexInput">The input text processed by the regular expression engine when the time-out occurred.</param>
		/// <param name="regexPattern">The pattern used by the regular expression engine when the time-out occurred.</param>
		/// <param name="matchTimeout">The time-out interval.</param>
		// Token: 0x060009F4 RID: 2548 RVA: 0x000338C5 File Offset: 0x00031AC5
		public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout)
			: base(global::SR.GetString("The RegEx engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors."))
		{
			this.Init(regexInput, regexPattern, matchTimeout);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a system-supplied message.</summary>
		// Token: 0x060009F5 RID: 2549 RVA: 0x000338ED File Offset: 0x00031AED
		public RegexMatchTimeoutException()
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x060009F6 RID: 2550 RVA: 0x00033908 File Offset: 0x00031B08
		public RegexMatchTimeoutException(string message)
			: base(message)
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		// Token: 0x060009F7 RID: 2551 RVA: 0x00033924 File Offset: 0x00031B24
		public RegexMatchTimeoutException(string message, Exception inner)
			: base(message, inner)
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		// Token: 0x060009F8 RID: 2552 RVA: 0x00033944 File Offset: 0x00031B44
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			string @string = info.GetString("regexInput");
			string string2 = info.GetString("regexPattern");
			TimeSpan timeSpan = TimeSpan.FromTicks(info.GetInt64("timeoutTicks"));
			this.Init(@string, string2, timeSpan);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> object.</summary>
		/// <param name="si">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x060009F9 RID: 2553 RVA: 0x00033998 File Offset: 0x00031B98
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			base.GetObjectData(si, context);
			si.AddValue("regexInput", this.regexInput);
			si.AddValue("regexPattern", this.regexPattern);
			si.AddValue("timeoutTicks", this.matchTimeout.Ticks);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000339E5 File Offset: 0x00031BE5
		private void Init()
		{
			this.Init("", "", TimeSpan.FromTicks(-1L));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000339FE File Offset: 0x00031BFE
		private void Init(string input, string pattern, TimeSpan timeout)
		{
			this.regexInput = input;
			this.regexPattern = pattern;
			this.matchTimeout = timeout;
		}

		/// <summary>Gets the regular expression pattern that was used in the matching operation when the time-out occurred.</summary>
		/// <returns>The regular expression pattern.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00033A15 File Offset: 0x00031C15
		public string Pattern
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexPattern;
			}
		}

		/// <summary>Gets the input text that the regular expression engine was processing when the time-out occurred.</summary>
		/// <returns>The regular expression input text.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00033A1D File Offset: 0x00031C1D
		public string Input
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.regexInput;
			}
		}

		/// <summary>Gets the time-out interval for a regular expression match.</summary>
		/// <returns>The time-out interval.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00033A25 File Offset: 0x00031C25
		public TimeSpan MatchTimeout
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				return this.matchTimeout;
			}
		}

		// Token: 0x04000EE4 RID: 3812
		private string regexInput;

		// Token: 0x04000EE5 RID: 3813
		private string regexPattern;

		// Token: 0x04000EE6 RID: 3814
		private TimeSpan matchTimeout = TimeSpan.FromTicks(-1L);
	}
}
