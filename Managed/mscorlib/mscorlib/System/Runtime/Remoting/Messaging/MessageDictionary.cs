using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000818 RID: 2072
	[Serializable]
	internal class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060052B7 RID: 21175 RVA: 0x0012384E File Offset: 0x00121A4E
		public MessageDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x0012385D File Offset: 0x00121A5D
		internal bool HasUserData()
		{
			if (this._internalProperties == null)
			{
				return false;
			}
			if (this._internalProperties is MessageDictionary)
			{
				return ((MessageDictionary)this._internalProperties).HasUserData();
			}
			return this._internalProperties.Count > 0;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x00123895 File Offset: 0x00121A95
		internal IDictionary InternalDictionary
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MessageDictionary)
				{
					return ((MessageDictionary)this._internalProperties).InternalDictionary;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x001238C3 File Offset: 0x00121AC3
		// (set) Token: 0x060052BB RID: 21179 RVA: 0x001238CB File Offset: 0x00121ACB
		public string[] MethodKeys
		{
			get
			{
				return this._methodKeys;
			}
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x001238D4 File Offset: 0x00121AD4
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x001238E2 File Offset: 0x00121AE2
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x00123900 File Offset: 0x00121B00
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string text in this._methodKeys)
			{
				if (key == text)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x0012393C File Offset: 0x00121B3C
		public MessageDictionary(string[] keys)
		{
			this._methodKeys = keys;
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060052C0 RID: 21184 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E47 RID: 3655
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x001239AC File Offset: 0x00121BAC
		protected virtual object GetMethodProperty(string key)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								return this._message.LogicalCallContext;
							}
						}
					}
					else if (key == "__Uri")
					{
						return this._message.Uri;
					}
				}
				else if (num != 1619225942U)
				{
					if (num == 1637783905U)
					{
						if (key == "__Return")
						{
							return ((IMethodReturnMessage)this._message).ReturnValue;
						}
					}
				}
				else if (key == "__Args")
				{
					return this._message.Args;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num != 1960967436U)
				{
					if (num == 2010141056U)
					{
						if (key == "__TypeName")
						{
							return this._message.TypeName;
						}
					}
				}
				else if (key == "__OutArgs")
				{
					return ((IMethodReturnMessage)this._message).OutArgs;
				}
			}
			else if (num != 3166241401U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						return this._message.MethodSignature;
					}
				}
			}
			else if (key == "__MethodName")
			{
				return this._message.MethodName;
			}
			return null;
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00123B30 File Offset: 0x00121D30
		protected virtual void SetMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num != 1201911322U)
						{
							return;
						}
						key == "__CallContext";
						return;
					}
					else
					{
						if (!(key == "__Uri"))
						{
							return;
						}
						((IInternalMessage)this._message).Uri = (string)value;
						return;
					}
				}
				else
				{
					if (num == 1619225942U)
					{
						key == "__Args";
						return;
					}
					if (num != 1637783905U)
					{
						return;
					}
					key == "__Return";
					return;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num == 1960967436U)
				{
					key == "__OutArgs";
					return;
				}
				if (num != 2010141056U)
				{
					return;
				}
				key == "__TypeName";
				return;
			}
			else
			{
				if (num == 3166241401U)
				{
					key == "__MethodName";
					return;
				}
				if (num != 3679129400U)
				{
					return;
				}
				key == "__MethodSignature";
				return;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x00123C28 File Offset: 0x00121E28
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x00123CC4 File Offset: 0x00121EC4
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x00123D74 File Offset: 0x00121F74
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00123DD5 File Offset: 0x00121FD5
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00123DEC File Offset: 0x00121FEC
		public bool Contains(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00123E3C File Offset: 0x0012203C
		public void Remove(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00123E92 File Offset: 0x00122092
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060052CE RID: 21198 RVA: 0x00002119 File Offset: 0x00000319
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00123EB9 File Offset: 0x001220B9
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00123EC8 File Offset: 0x001220C8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00123EC8 File Offset: 0x001220C8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x04002B1C RID: 11036
		private IDictionary _internalProperties;

		// Token: 0x04002B1D RID: 11037
		protected IMethodMessage _message;

		// Token: 0x04002B1E RID: 11038
		private string[] _methodKeys;

		// Token: 0x04002B1F RID: 11039
		private bool _ownProperties;

		// Token: 0x02000819 RID: 2073
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060052D2 RID: 21202 RVA: 0x00123ED0 File Offset: 0x001220D0
			public DictionaryEnumerator(MessageDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				this._hashtableEnum = ((this._methodDictionary._internalProperties != null) ? this._methodDictionary._internalProperties.GetEnumerator() : null);
				this._posMethod = -1;
			}

			// Token: 0x17000E4D RID: 3661
			// (get) Token: 0x060052D3 RID: 21203 RVA: 0x00123F0C File Offset: 0x0012210C
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x060052D4 RID: 21204 RVA: 0x00123F1C File Offset: 0x0012211C
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060052D5 RID: 21205 RVA: 0x00123F97 File Offset: 0x00122197
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x17000E4E RID: 3662
			// (get) Token: 0x060052D6 RID: 21206 RVA: 0x00123FAC File Offset: 0x001221AC
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x17000E4F RID: 3663
			// (get) Token: 0x060052D7 RID: 21207 RVA: 0x00124020 File Offset: 0x00122220
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x17000E50 RID: 3664
			// (get) Token: 0x060052D8 RID: 21208 RVA: 0x0012403C File Offset: 0x0012223C
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x04002B20 RID: 11040
			private MessageDictionary _methodDictionary;

			// Token: 0x04002B21 RID: 11041
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x04002B22 RID: 11042
			private int _posMethod;
		}
	}
}
