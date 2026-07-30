using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AA RID: 426
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
		// Token: 0x060011E9 RID: 4585 RVA: 0x000497AD File Offset: 0x000479AD
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		// Token: 0x060011EA RID: 4586 RVA: 0x000497C3 File Offset: 0x000479C3
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		/// <param name="error">The Boolean value that indicates whether the obsolete element usage is considered an error. </param>
		// Token: 0x060011EB RID: 4587 RVA: 0x000497D9 File Offset: 0x000479D9
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
		/// <returns>The workaround text string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x000497EF File Offset: 0x000479EF
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
		/// <returns>true if the obsolete element usage is considered an error; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000497F7 File Offset: 0x000479F7
		public bool IsError
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x04000A4A RID: 2634
		private string _message;

		// Token: 0x04000A4B RID: 2635
		private bool _error;
	}
}
