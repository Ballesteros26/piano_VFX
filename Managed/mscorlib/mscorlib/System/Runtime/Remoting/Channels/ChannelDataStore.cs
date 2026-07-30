using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores channel data for the remoting channels.</summary>
	// Token: 0x02000796 RID: 1942
	[ComVisible(true)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ChannelDataStore" /> class with the URIs that the current channel maps to.</summary>
		/// <param name="channelURIs">An array of channel URIs that the current channel maps to. </param>
		// Token: 0x06004F80 RID: 20352 RVA: 0x0011DCFA File Offset: 0x0011BEFA
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
		}

		/// <summary>Gets or sets an array of channel URIs that the current channel maps to.</summary>
		/// <returns>An array of channel URIs that the current channel maps to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x0011DD09 File Offset: 0x0011BF09
		// (set) Token: 0x06004F82 RID: 20354 RVA: 0x0011DD11 File Offset: 0x0011BF11
		public string[] ChannelUris
		{
			[SecurityCritical]
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		/// <summary>Gets or sets the data object that is associated with the specified key for the implementing channel.</summary>
		/// <returns>The specified data object for the implementing channel.</returns>
		/// <param name="key">The key that the data object is associated with. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D53 RID: 3411
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				if (this._extraData == null)
				{
					return null;
				}
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[]
					{
						new DictionaryEntry(key, value)
					};
					return;
				}
				DictionaryEntry[] array = new DictionaryEntry[this._extraData.Length + 1];
				this._extraData.CopyTo(array, 0);
				array[this._extraData.Length] = new DictionaryEntry(key, value);
				this._extraData = array;
			}
		}

		// Token: 0x04002A45 RID: 10821
		private string[] _channelURIs;

		// Token: 0x04002A46 RID: 10822
		private DictionaryEntry[] _extraData;
	}
}
