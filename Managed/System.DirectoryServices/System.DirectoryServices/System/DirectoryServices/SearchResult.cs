using System;
using System.Collections;
using System.Collections.Specialized;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.SearchResult" /> class encapsulates a node in the Active Directory Domain Services hierarchy that is returned during a search through <see cref="T:System.DirectoryServices.DirectorySearcher" />.</summary>
	// Token: 0x0200002D RID: 45
	public class SearchResult
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000047E3 File Offset: 0x000029E3
		internal PropertyCollection Rproperties
		{
			get
			{
				return this._Rproperties;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000047EB File Offset: 0x000029EB
		private void InitBlock()
		{
			this._Properties = null;
			this._Entry = null;
			this._PropsToLoad = null;
			this.ispropnull = true;
			this._Rproperties = null;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004810 File Offset: 0x00002A10
		internal StringCollection PropsToLoad
		{
			get
			{
				if (this._PropsToLoad != null)
				{
					return this._PropsToLoad;
				}
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ResultPropertyCollection" /> collection of properties for this object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ResultPropertyCollection" /> of properties set on this object.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004824 File Offset: 0x00002A24
		public ResultPropertyCollection Properties
		{
			get
			{
				if (this.ispropnull)
				{
					this._Properties = new ResultPropertyCollection();
					IDictionaryEnumerator enumerator = this.Rproperties.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						ResultPropertyValueCollection resultPropertyValueCollection = new ResultPropertyValueCollection();
						if (this.Rproperties[text].Count == 1)
						{
							string text2 = (string)this.Rproperties[text].Value;
							resultPropertyValueCollection.Add(text2);
						}
						else if (this.Rproperties[text].Count > 1)
						{
							object[] array = (object[])this.Rproperties[text].Value;
							resultPropertyValueCollection.AddRange(array);
						}
						this._Properties.Add(text, resultPropertyValueCollection);
					}
					this.ispropnull = false;
				}
				return this._Properties;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000048F7 File Offset: 0x00002AF7
		internal SearchResult(DirectoryEntry entry)
		{
			this.ispropnull = true;
			base..ctor();
			this.InitBlock();
			this._Entry = entry;
			this._Path = entry.Path;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000491F File Offset: 0x00002B1F
		internal SearchResult(DirectoryEntry entry, PropertyCollection props)
		{
			this.ispropnull = true;
			base..ctor();
			this.InitBlock();
			this._Entry = entry;
			this._Path = entry.Path;
			this._Rproperties = props;
		}

		/// <summary>Gets the path for this <see cref="T:System.DirectoryServices.SearchResult" />.</summary>
		/// <returns>The path of this <see cref="T:System.DirectoryServices.SearchResult" />.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000494E File Offset: 0x00002B4E
		public string Path
		{
			get
			{
				return this._Path;
			}
		}

		/// <summary>Retrieves the <see cref="T:System.DirectoryServices.DirectoryEntry" /> that corresponds to the <see cref="T:System.DirectoryServices.SearchResult" /> from the Active Directory Domain Services hierarchy.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> that corresponds to the <see cref="T:System.DirectoryServices.SearchResult" />.</returns>
		// Token: 0x0600018C RID: 396 RVA: 0x00004956 File Offset: 0x00002B56
		public DirectoryEntry GetDirectoryEntry()
		{
			return this._Entry;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00002644 File Offset: 0x00000844
		internal SearchResult()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000A2 RID: 162
		private string _Path;

		// Token: 0x040000A3 RID: 163
		private ResultPropertyCollection _Properties;

		// Token: 0x040000A4 RID: 164
		private DirectoryEntry _Entry;

		// Token: 0x040000A5 RID: 165
		private StringCollection _PropsToLoad;

		// Token: 0x040000A6 RID: 166
		private bool ispropnull;

		// Token: 0x040000A7 RID: 167
		private PropertyCollection _Rproperties;
	}
}
