using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the source for <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> automatic completion functionality.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000048 RID: 72
	public enum AutoCompleteSource
	{
		/// <summary>Specifies the file system as the source.</summary>
		// Token: 0x040005E6 RID: 1510
		FileSystem = 1,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the history list.</summary>
		// Token: 0x040005E7 RID: 1511
		HistoryList,
		/// <summary>Includes the Uniform Resource Locators (URLs) in the list of those URLs most recently used.</summary>
		// Token: 0x040005E8 RID: 1512
		RecentlyUsedList = 4,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.HistoryList" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.RecentlyUsedList" /> as the source.</summary>
		// Token: 0x040005E9 RID: 1513
		AllUrl = 6,
		/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.FileSystem" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.AllUrl" /> as the source. This is the default value when <see cref="T:System.Windows.Forms.AutoCompleteMode" /> has been set to a value other than the default.</summary>
		// Token: 0x040005EA RID: 1514
		AllSystemSources,
		/// <summary>Specifies that only directory names and not file names will be automatically completed.</summary>
		// Token: 0x040005EB RID: 1515
		FileSystemDirectories = 32,
		/// <summary>Specifies strings from a built-in <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> as the source.</summary>
		// Token: 0x040005EC RID: 1516
		CustomSource = 64,
		/// <summary>Specifies that no <see cref="T:System.Windows.Forms.AutoCompleteSource" /> is currently in use. This is the default value of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</summary>
		// Token: 0x040005ED RID: 1517
		None = 128,
		/// <summary>Specifies that the items of the <see cref="T:System.Windows.Forms.ComboBox" /> represent the source.</summary>
		// Token: 0x040005EE RID: 1518
		ListItems = 256
	}
}
