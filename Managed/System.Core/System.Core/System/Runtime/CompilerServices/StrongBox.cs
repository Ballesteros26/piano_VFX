using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Holds a reference to a value.</summary>
	/// <typeparam name="T">The type of the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000301 RID: 769
	public class StrongBox<T> : IStrongBox
	{
		/// <summary>Initializes a new StrongBox which can receive a value when used in a reference call.</summary>
		// Token: 0x0600176A RID: 5994 RVA: 0x00002320 File Offset: 0x00000520
		public StrongBox()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> class by using the supplied value. </summary>
		/// <param name="value">A value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> will reference.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600176B RID: 5995 RVA: 0x0004CDAE File Offset: 0x0004AFAE
		public StrongBox(T value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		/// <returns>The value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</returns>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0004CDBD File Offset: 0x0004AFBD
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0004CDCA File Offset: 0x0004AFCA
		object IStrongBox.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (T)((object)value);
			}
		}

		/// <summary>Represents the value that the <see cref="T:System.Runtime.CompilerServices.StrongBox`1" /> references.</summary>
		// Token: 0x04000ACF RID: 2767
		public T Value;
	}
}
