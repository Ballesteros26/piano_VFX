using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000494 RID: 1172
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x060022B9 RID: 8889 RVA: 0x00086882 File Offset: 0x00084A82
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0008689C File Offset: 0x00084A9C
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000868C0 File Offset: 0x00084AC0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Dispose()
		{
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000868C8 File Offset: 0x00084AC8
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x000868EC File Offset: 0x00084AEC
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000027E8 File Offset: 0x000009E8
		internal virtual void Abort()
		{
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000868F4 File Offset: 0x00084AF4
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000868FD File Offset: 0x00084AFD
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x060022C2 RID: 8898
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x04001F1A RID: 7962
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x04001F1B RID: 7963
		private bool m_CacheComplete;

		// Token: 0x04001F1C RID: 7964
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x04001F1D RID: 7965
		private Uri m_Destination;

		// Token: 0x04001F1E RID: 7966
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000495 RID: 1173
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x060022C3 RID: 8899 RVA: 0x0008691F File Offset: 0x00084B1F
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x060022C4 RID: 8900 RVA: 0x00086935 File Offset: 0x00084B35
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0008696E File Offset: 0x00084B6E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060022C6 RID: 8902 RVA: 0x00086978 File Offset: 0x00084B78
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool flag2;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							flag2 = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							flag2 = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							flag2 = false;
						}
					}
					return flag2;
				}
			}

			// Token: 0x060022C7 RID: 8903 RVA: 0x00086A88 File Offset: 0x00084C88
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x060022C8 RID: 8904 RVA: 0x000027E8 File Offset: 0x000009E8
			public void Dispose()
			{
			}

			// Token: 0x04001F1F RID: 7967
			private ProxyChain m_Chain;

			// Token: 0x04001F20 RID: 7968
			private bool m_Finished;

			// Token: 0x04001F21 RID: 7969
			private int m_CurrentIndex = -1;

			// Token: 0x04001F22 RID: 7970
			private bool m_TriedDirect;
		}
	}
}
