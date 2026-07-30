using System;
using Unity;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.Console.CancelKeyPress" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013B RID: 315
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x00035AF2 File Offset: 0x00033CF2
		internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
		{
			this._type = type;
			this._cancel = false;
		}

		/// <summary>Gets or sets a value that indicates whether simultaneously pressing the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and the <see cref="F:System.ConsoleKey.C" /> console key (Ctrl+C) or the Ctrl+Break keys terminates the current process. The default is false, which terminates the current process. </summary>
		/// <returns>true if the current process should resume when the event handler concludes; false if the current process should terminate. The default value is false; the current process terminates when the event handler returns. If true, the current process continues. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00035B08 File Offset: 0x00033D08
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00035B10 File Offset: 0x00033D10
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		/// <summary>Gets the combination of modifier and console keys that interrupted the current process.</summary>
		/// <returns>One of the enumeration values that specifies the key combination that interrupted the current process. There is no default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00035B19 File Offset: 0x00033D19
		public ConsoleSpecialKey SpecialKey
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ConsoleCancelEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D5 RID: 2005
		private ConsoleSpecialKey _type;

		// Token: 0x040007D6 RID: 2006
		private bool _cancel;
	}
}
