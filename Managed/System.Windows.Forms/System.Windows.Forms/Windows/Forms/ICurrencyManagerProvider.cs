using System;

namespace System.Windows.Forms
{
	/// <summary>Provides custom binding management for components.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C7 RID: 455
	public interface ICurrencyManagerProvider
	{
		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" />. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001DD3 RID: 7635
		CurrencyManager CurrencyManager { get; }

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" /> and the specified data member.</summary>
		/// <returns>The related <see cref="T:System.Windows.Forms.CurrencyManager" /> obtained from this <see cref="T:System.Windows.Forms.ICurrencyManagerProvider" /> and the specified data member.</returns>
		/// <param name="dataMember">The name of the column or list, within the data source, to obtain the <see cref="T:System.Windows.Forms.CurrencyManager" /> for.</param>
		// Token: 0x06001DD4 RID: 7636
		CurrencyManager GetRelatedCurrencyManager(string dataMember);
	}
}
