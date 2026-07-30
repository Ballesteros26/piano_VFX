using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Runtime.Remoting;
using System.Web.Configuration;

namespace System.Web.SessionState
{
	// Token: 0x020004A5 RID: 1189
	internal class SessionStateServerHandler : SessionStateStoreProviderBase
	{
		// Token: 0x060035EC RID: 13804 RVA: 0x0008DFFC File Offset: 0x0008C1FC
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), HttpApplicationFactory.ApplicationState.SessionObjects, timeout);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0008E013 File Offset: 0x0008C213
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.CreateUninitializedItem(id, timeout);
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Dispose()
		{
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0008E02C File Offset: 0x0008C22C
		private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			if (id == null)
			{
				return null;
			}
			StateServerItem item = this.stateServer.GetItem(id, out locked, out lockAge, out lockId, out actions, exclusive);
			if (item == null)
			{
				return null;
			}
			if (actions == SessionStateActions.InitializeItem)
			{
				return this.CreateNewStoreData(context, item.Timeout);
			}
			SessionStateItemCollection sessionStateItemCollection = null;
			HttpStaticObjectsCollection httpStaticObjectsCollection = null;
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			GZipStream gzipStream = null;
			try
			{
				if (item.CollectionData != null && item.CollectionData.Length != 0)
				{
					memoryStream = new MemoryStream(item.CollectionData);
					Stream stream;
					if (this.config.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Decompress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryReader = new BinaryReader(stream);
					sessionStateItemCollection = SessionStateItemCollection.Deserialize(binaryReader);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryReader.Close();
				}
				else
				{
					sessionStateItemCollection = new SessionStateItemCollection();
				}
				if (item.StaticObjectsData != null && item.StaticObjectsData.Length != 0)
				{
					httpStaticObjectsCollection = HttpStaticObjectsCollection.FromByteArray(item.StaticObjectsData);
				}
				else
				{
					httpStaticObjectsCollection = new HttpStaticObjectsCollection();
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to retrieve session state.", ex);
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
				if (binaryReader != null)
				{
					binaryReader.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
			}
			return new SessionStateStoreData(sessionStateItemCollection, httpStaticObjectsCollection, item.Timeout);
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0008E184 File Offset: 0x0008C384
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, false);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0008E19E File Offset: 0x0008C39E
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, true);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0008E1B8 File Offset: 0x0008C3B8
		public override void Initialize(string name, NameValueCollection config)
		{
			this.config = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
			if (string.IsNullOrEmpty(name))
			{
				name = "Session Server handler";
			}
			RemotingConfiguration.Configure(null);
			string text = null;
			string text2 = null;
			string text3 = null;
			this.GetConData(out text, out text2, out text3);
			string text4 = string.Format("{0}://{1}:{2}/StateServer", text, text2, text3);
			this.stateServer = Activator.GetObject(typeof(RemoteStateServer), text4) as RemoteStateServer;
			base.Initialize(name, config);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0008E234 File Offset: 0x0008C434
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.ReleaseItemExclusive(id, lockId);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x0008E24B File Offset: 0x0008C44B
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.Remove(id, lockId);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x0008E262 File Offset: 0x0008C462
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.ResetItemTimeout(id);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x0008E278 File Offset: 0x0008C478
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			if (item == null)
			{
				return;
			}
			this.EnsureGoodId(id, true);
			byte[] array = null;
			byte[] array2 = null;
			MemoryStream memoryStream = null;
			BinaryWriter binaryWriter = null;
			GZipStream gzipStream = null;
			try
			{
				SessionStateItemCollection sessionStateItemCollection = item.Items as SessionStateItemCollection;
				if (sessionStateItemCollection != null && sessionStateItemCollection.Count > 0)
				{
					memoryStream = new MemoryStream();
					Stream stream;
					if (this.config.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Compress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryWriter = new BinaryWriter(stream);
					sessionStateItemCollection.Serialize(binaryWriter);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryWriter.Close();
					array = memoryStream.ToArray();
				}
				HttpStaticObjectsCollection staticObjects = item.StaticObjects;
				if (staticObjects != null && staticObjects.Count > 0)
				{
					array2 = staticObjects.ToByteArray();
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to store session data.", ex);
			}
			finally
			{
				if (binaryWriter != null)
				{
					binaryWriter.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			this.stateServer.SetAndReleaseItemExclusive(id, array, array2, lockId, item.Timeout, newItem);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x0008C616 File Offset: 0x0008A816
		private void EnsureGoodId(string id, bool throwOnNull)
		{
			if (id == null)
			{
				if (throwOnNull)
				{
					throw new HttpException("Session ID is invalid");
				}
				return;
			}
			else
			{
				if (id.Length > SessionIDManager.SessionIDMaxLength)
				{
					throw new HttpException("Session ID too long");
				}
				return;
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x0008E388 File Offset: 0x0008C588
		private void GetConData(out string proto, out string server, out string port)
		{
			string stateConnectionString = this.config.StateConnectionString;
			int num = stateConnectionString.IndexOf('=');
			int num2 = stateConnectionString.IndexOf(':');
			if (num < 0 || num2 < 0)
			{
				throw new HttpException("Invalid StateConnectionString");
			}
			proto = stateConnectionString.Substring(0, num);
			server = stateConnectionString.Substring(num + 1, num2 - num - 1);
			port = stateConnectionString.Substring(num2 + 1, stateConnectionString.Length - num2 - 1);
			if (proto == "tcpip")
			{
				proto = "tcp";
			}
		}

		// Token: 0x04001D91 RID: 7569
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D92 RID: 7570
		private SessionStateSection config;

		// Token: 0x04001D93 RID: 7571
		private RemoteStateServer stateServer;
	}
}
