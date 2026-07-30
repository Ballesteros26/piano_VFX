using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides object-model access to all attributes declared in the opening tag of an ASP.NET server control element. This class cannot be inherited.</summary>
	// Token: 0x020001A2 RID: 418
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AttributeCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.AttributeCollection" /> class.</summary>
		/// <param name="bag">An object that contains the attribute keys and their values from the opening tag of the server control. </param>
		// Token: 0x06000FEC RID: 4076 RVA: 0x0002BC6D File Offset: 0x00029E6D
		public AttributeCollection(StateBag bag)
		{
			this.bag = bag;
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.AttributeCollection" /> object is equal to the specified object.</summary>
		/// <returns>true if the object that is contained in the <paramref name="o" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.AttributeCollection" />; otherwise, false.</returns>
		/// <param name="o">The object instance to compare with this instance.</param>
		// Token: 0x06000FED RID: 4077 RVA: 0x0002BC7C File Offset: 0x00029E7C
		public override bool Equals(object o)
		{
			AttributeCollection attributeCollection = o as AttributeCollection;
			if (attributeCollection == null)
			{
				return false;
			}
			if (this.Count != attributeCollection.Count)
			{
				return false;
			}
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				if (string.CompareOrdinal(text, "style") != 0 && string.CompareOrdinal(attributeCollection[text], this[text]) == 0)
				{
					return false;
				}
			}
			if ((this.styleCollection == null && attributeCollection.styleCollection != null) || (this.styleCollection != null && attributeCollection.styleCollection == null))
			{
				return false;
			}
			if (this.styleCollection != null)
			{
				if (this.styleCollection.Count != attributeCollection.styleCollection.Count)
				{
					return false;
				}
				foreach (object obj2 in this.styleCollection.Keys)
				{
					string text2 = (string)obj2;
					if (string.CompareOrdinal(this.styleCollection[text2], attributeCollection.styleCollection[text2]) == 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000FEE RID: 4078 RVA: 0x0002BDCC File Offset: 0x00029FCC
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Keys)
			{
				string text = (string)obj;
				if (!(text == "style"))
				{
					num ^= text.GetHashCode();
					string text2 = this[text];
					if (text2 != null)
					{
						num ^= text2.GetHashCode();
					}
				}
			}
			if (this.styleCollection != null)
			{
				foreach (object obj2 in this.styleCollection.Keys)
				{
					string text3 = (string)obj2;
					num ^= this.styleCollection[text3].GetHashCode();
					string text4 = this.styleCollection[text3];
					if (text4 != null)
					{
						num ^= text4.GetHashCode();
					}
				}
			}
			return num;
		}

		/// <summary>Gets the number of attributes in the <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public int Count
		{
			get
			{
				return this.bag.Count;
			}
		}

		/// <summary>Gets a collection of styles for the ASP.NET server control to which the current <see cref="T:System.Web.UI.AttributeCollection" /> object belongs.</summary>
		/// <returns>A collection that contains the styles for the current server control.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0002BEE1 File Offset: 0x0002A0E1
		public CssStyleCollection CssStyle
		{
			get
			{
				if (this.styleCollection == null)
				{
					this.styleCollection = new CssStyleCollection(this.bag);
				}
				return this.styleCollection;
			}
		}

		/// <summary>Gets or sets a specified attribute value for a server control.</summary>
		/// <returns>The attribute value.</returns>
		/// <param name="key">The location of the attribute in the collection. </param>
		// Token: 0x17000536 RID: 1334
		public string this[string key]
		{
			get
			{
				return this.bag[key] as string;
			}
			set
			{
				this.Add(key, value);
			}
		}

		/// <summary>Gets a collection of keys to all attributes in the server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <returns>The collection of keys.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0002BF1F File Offset: 0x0002A11F
		public ICollection Keys
		{
			get
			{
				return this.bag.Keys;
			}
		}

		/// <summary>Adds an attribute to a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <param name="key">The attribute name. </param>
		/// <param name="value">The attribute value. </param>
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public void Add(string key, string value)
		{
			if (string.Compare(key, "style", true, Helpers.InvariantCulture) == 0)
			{
				this.CssStyle.Value = value;
				return;
			}
			this.bag.Add(key, value);
		}

		/// <summary>Adds attributes from the <see cref="T:System.Web.UI.AttributeCollection" /> class to the <see cref="T:System.Web.UI.HtmlTextWriter" /> object that is responsible for rendering the attributes as markup.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute to the opening tag of an ASP.NET server control. </param>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x0002BF5C File Offset: 0x0002A15C
		public void AddAttributes(HtmlTextWriter writer)
		{
			foreach (object obj in this.bag.Keys)
			{
				string text = (string)obj;
				string text2 = this.bag[text] as string;
				writer.AddAttribute(text, text2);
			}
		}

		/// <summary>Removes all attributes from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002BFD0 File Offset: 0x0002A1D0
		public void Clear()
		{
			this.CssStyle.Clear();
			this.bag.Clear();
		}

		/// <summary>Removes an attribute from a server control's <see cref="T:System.Web.UI.AttributeCollection" /> object.</summary>
		/// <param name="key">The name of the attribute to remove. </param>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
		public void Remove(string key)
		{
			if (string.Compare(key, "style", true, Helpers.InvariantCulture) == 0)
			{
				this.CssStyle.Clear();
				return;
			}
			this.bag.Remove(key);
		}

		/// <summary>Writes the collection of attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> output stream for the control to which the collection belongs.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that writes the attribute collection to the current output stream. </param>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002C018 File Offset: 0x0002A218
		public void Render(HtmlTextWriter writer)
		{
			foreach (object obj in this.bag.Keys)
			{
				string text = (string)obj;
				string text2 = this.bag[text] as string;
				if (text2 != null)
				{
					writer.WriteAttribute(text, text2, true);
				}
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002C090 File Offset: 0x0002A290
		internal void CopyFrom(AttributeCollection attributeCollection)
		{
			if (attributeCollection == null || attributeCollection.Count == 0)
			{
				return;
			}
			foreach (object obj in attributeCollection.bag.Keys)
			{
				string text = (string)obj;
				this.Add(text, attributeCollection[text]);
			}
		}

		// Token: 0x04001349 RID: 4937
		private StateBag bag;

		// Token: 0x0400134A RID: 4938
		private CssStyleCollection styleCollection;

		// Token: 0x0400134B RID: 4939
		internal const string StyleAttribute = "style";
	}
}
