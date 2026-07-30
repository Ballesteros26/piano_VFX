using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Persists an 8-byte <see cref="T:System.DateTime" /> constant for a field or parameter.</summary>
	// Token: 0x02000870 RID: 2160
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Initializes a new instance of the DateTimeConstantAttribute class with the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <param name="ticks">The number of 100-nanosecond ticks that represent the date and time of this instance. </param>
		// Token: 0x0600544D RID: 21581 RVA: 0x00127384 File Offset: 0x00125584
		public DateTimeConstantAttribute(long ticks)
		{
			this.date = new DateTime(ticks);
		}

		/// <summary>Gets the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of 100-nanosecond ticks that represent the date and time of this instance.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x00127398 File Offset: 0x00125598
		public override object Value
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x001273A8 File Offset: 0x001255A8
		internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return new DateTime((long)customAttributeNamedArgument.TypedValue.Value);
				}
			}
			return new DateTime((long)attr.ConstructorArguments[0].Value);
		}

		// Token: 0x04002BBD RID: 11197
		private DateTime date;
	}
}
