using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group. </summary>
	// Token: 0x02000149 RID: 329
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x000315CF File Offset: 0x0002F7CF
		internal Group(string text, int[] caps, int capcount, string name)
			: base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this._name = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>true if the match is successful; otherwise, false.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00031608 File Offset: 0x0002F808
		public bool Success
		{
			get
			{
				return this._capcount != 0;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00031613 File Offset: 0x0002F813
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0003161B File Offset: 0x0002F81B
		public CaptureCollection Captures
		{
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		/// <summary>Returns a Group object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <returns>A regular expression Group object. </returns>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06000994 RID: 2452 RVA: 0x00031638 File Offset: 0x0002F838
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner._capcount > 0)
			{
				Capture capture = captures[0];
			}
			return inner;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal Group()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000EB7 RID: 3767
		internal static Group _emptygroup = new Group(string.Empty, new int[0], 0, string.Empty);

		// Token: 0x04000EB8 RID: 3768
		internal int[] _caps;

		// Token: 0x04000EB9 RID: 3769
		internal int _capcount;

		// Token: 0x04000EBA RID: 3770
		internal CaptureCollection _capcoll;

		// Token: 0x04000EBB RID: 3771
		[OptionalField]
		internal string _name;
	}
}
