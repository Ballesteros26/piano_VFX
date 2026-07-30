using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CustomError" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000597 RID: 1431
	[ConfigurationCollection(typeof(CustomError), AddItemName = "error", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CustomErrorCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.CustomError" /> object to the collection.</summary>
		/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> object to add already exists in the collection or the collection is read only.</param>
		// Token: 0x06003C9D RID: 15517 RVA: 0x000A028F File Offset: 0x0009E48F
		public void Add(CustomError customError)
		{
			this.BaseAdd(customError);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.CustomError" /> objects from the collection.</summary>
		// Token: 0x06003C9E RID: 15518 RVA: 0x0009F55F File Offset: 0x0009D75F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000A1604 File Offset: 0x0009F804
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomError();
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000A160C File Offset: 0x0009F80C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomError)element).StatusCode.ToString();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</returns>
		/// <param name="index">The collection key's index. </param>
		// Token: 0x06003CA1 RID: 15521 RVA: 0x000A09E6 File Offset: 0x0009EBE6
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</returns>
		/// <param name="statusCode">The HTTP status code associated with the custom error. </param>
		// Token: 0x06003CA2 RID: 15522 RVA: 0x000A162C File Offset: 0x0009F82C
		public CustomError Get(string statusCode)
		{
			return (CustomError)base.BaseGet(statusCode);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object. </param>
		// Token: 0x06003CA3 RID: 15523 RVA: 0x000A163A File Offset: 0x0009F83A
		public CustomError Get(int index)
		{
			return (CustomError)base.BaseGet(index);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object from the collection.</summary>
		/// <param name="statusCode">The HTTP status code associated with the custom error.  </param>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x0009F57B File Offset: 0x0009D77B
		public void Remove(string statusCode)
		{
			base.BaseRemove(statusCode);
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object at the specified index location from the collection.</summary>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object to remove. </param>
		// Token: 0x06003CA5 RID: 15525 RVA: 0x0009F584 File Offset: 0x0009D784
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.CustomError" /> to the collection.</summary>
		/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> to add to the collection. </param>
		// Token: 0x06003CA6 RID: 15526 RVA: 0x000A1648 File Offset: 0x0009F848
		public void Set(CustomError customError)
		{
			CustomError customError2 = this.Get(customError.StatusCode.ToString());
			if (customError2 == null)
			{
				this.Add(customError);
				return;
			}
			int num = base.BaseIndexOf(customError2);
			this.RemoveAt(num);
			this.BaseAdd(num, customError);
		}

		/// <summary>Returns an array of the keys for all of the configuration elements contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
		/// <returns>An array containing the keys for all of the <see cref="T:System.Web.Configuration.CustomError" /> objects contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</returns>
		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000A168C File Offset: 0x0009F88C
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].StatusCode.ToString();
				}
				return array;
			}
		}

		/// <summary>The type of the <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x00008A69 File Offset: 0x00006C69
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000A16CE File Offset: 0x0009F8CE
		protected override string ElementName
		{
			get
			{
				return "error";
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> at the specified index.</returns>
		/// <param name="index">The collection error's index. </param>
		// Token: 0x170012AC RID: 4780
		public CustomError this[int index]
		{
			get
			{
				return (CustomError)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					this.RemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</returns>
		/// <param name="statusCode">The HTTP status code. </param>
		// Token: 0x170012AD RID: 4781
		public CustomError this[string statusCode]
		{
			get
			{
				return (CustomError)base.BaseGet(statusCode);
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000A16EF File Offset: 0x0009F8EF
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorCollection.properties;
			}
		}

		// Token: 0x040020D3 RID: 8403
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
