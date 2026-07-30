using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.Parameter" /> and <see cref="T:System.Web.UI.WebControls.Parameter" />-derived objects that are used by data source controls in advanced data-binding scenarios.</summary>
	// Token: 0x020003E8 RID: 1000
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ParameterCollection : StateManagedCollection
	{
		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object to the end of the collection.</summary>
		/// <returns>The index value of the added item.</returns>
		/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to append to the collection. </param>
		// Token: 0x06002C0B RID: 11275 RVA: 0x00064EFA File Offset: 0x000630FA
		public int Add(Parameter parameter)
		{
			return ((IList)this).Add(parameter);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name and default value, and appends it to the end of the collection.</summary>
		/// <returns>The index value of the added item.</returns>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="value">A string that serves as a default value for the parameter. </param>
		// Token: 0x06002C0C RID: 11276 RVA: 0x00074E74 File Offset: 0x00073074
		public int Add(string name, string value)
		{
			return ((IList)this).Add(new Parameter(name, TypeCode.Object, value));
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name, <see cref="T:System.TypeCode" />, and default value, and appends it to the end of the collection.</summary>
		/// <returns>The index value of the added item.</returns>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type of the parameter.</param>
		/// <param name="value">The default value for the parameter.</param>
		// Token: 0x06002C0D RID: 11277 RVA: 0x00074E84 File Offset: 0x00073084
		public int Add(string name, TypeCode type, string value)
		{
			return ((IList)this).Add(new Parameter(name, type, value));
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name, database type, and default value, and adds it to the end of the collection.</summary>
		/// <returns>The index value of the added item.</returns>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="dbType">The database type of the parameter.</param>
		/// <param name="value">The default value for the parameter. </param>
		// Token: 0x06002C0E RID: 11278 RVA: 0x00074E94 File Offset: 0x00073094
		public int Add(string name, DbType dbType, string value)
		{
			return ((IList)this).Add(new Parameter(name, dbType, value));
		}

		/// <summary>Creates an instance of a default <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
		/// <returns>A default instance of a <see cref="T:System.Web.UI.WebControls.Parameter" />.</returns>
		/// <param name="index">The index of the type of <see cref="T:System.Web.UI.WebControls.Parameter" /> to create from the ordered list of types returned by <see cref="M:System.Web.UI.WebControls.ParameterCollection.GetKnownTypes" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is not within the recognized range. </exception>
		// Token: 0x06002C0F RID: 11279 RVA: 0x00074EA4 File Offset: 0x000730A4
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new ControlParameter();
			case 1:
				return new CookieParameter();
			case 2:
				return new FormParameter();
			case 3:
				return new Parameter();
			case 4:
				return new QueryStringParameter();
			case 5:
				return new SessionParameter();
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Web.UI.WebControls.Parameter" /> types that the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection can contain.</summary>
		/// <returns>An ordered array of <see cref="T:System.Type" /> objects that identify the types of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that the collection can contain.</returns>
		// Token: 0x06002C10 RID: 11280 RVA: 0x00074EFF File Offset: 0x000730FF
		protected override Type[] GetKnownTypes()
		{
			return ParameterCollection._knownTypes;
		}

		/// <summary>Gets an ordered collection of <see cref="T:System.Web.UI.WebControls.Parameter" /> object names and their corresponding values currently contained by the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpRequest" /> that the <see cref="T:System.Web.UI.WebControls.Parameter" /> binds to.</param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> instance that is passed to each parameter's <see cref="M:System.Web.UI.WebControls.ControlParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method. </param>
		// Token: 0x06002C11 RID: 11281 RVA: 0x00074F08 File Offset: 0x00073108
		public IOrderedDictionary GetValues(HttpContext context, Control control)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (object obj in this)
			{
				Parameter parameter = (Parameter)obj;
				string text = parameter.Name;
				int num = 1;
				while (orderedDictionary.Contains(text))
				{
					text = parameter.Name + num.ToString();
					num++;
				}
				orderedDictionary.Add(text, parameter.GetValue(context, control));
			}
			return orderedDictionary;
		}

		/// <summary>Iterates through the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects contained by the collection, and calls the Evaluate method on each one.</summary>
		/// <param name="context">The current <see cref="T:System.Web.HttpRequest" /> that the <see cref="T:System.Web.UI.WebControls.Parameter" /> binds to.</param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> instance that is passed to each parameter's <see cref="M:System.Web.UI.WebControls.ControlParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method. </param>
		// Token: 0x06002C12 RID: 11282 RVA: 0x00074F9C File Offset: 0x0007319C
		public void UpdateValues(HttpContext context, Control control)
		{
			foreach (object obj in this)
			{
				((Parameter)obj).UpdateValue(context, control);
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Web.UI.WebControls.Parameter" /> is inserted. </param>
		/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		// Token: 0x06002C13 RID: 11283 RVA: 0x00055562 File Offset: 0x00053762
		public void Insert(int index, Parameter parameter)
		{
			((IList)this).Insert(index, parameter);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06002C14 RID: 11284 RVA: 0x00074FF0 File Offset: 0x000731F0
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.OnParametersChanged(EventArgs.Empty);
		}

		/// <summary>Occurs before the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Insert(System.Int32,System.Web.UI.WebControls.Parameter)" /> method is called.</summary>
		/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> is inserted at. </param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that is inserted into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
		// Token: 0x06002C15 RID: 11285 RVA: 0x00075003 File Offset: 0x00073203
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			((Parameter)value).SetOwnerCollection(this);
		}

		/// <summary>Occurs after the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Insert(System.Int32,System.Web.UI.WebControls.Parameter)" /> method completes.</summary>
		/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> was inserted at. </param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that was inserted into the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
		// Token: 0x06002C16 RID: 11286 RVA: 0x00075019 File Offset: 0x00073219
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.OnParametersChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ParameterCollection.ParametersChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C17 RID: 11287 RVA: 0x0007502E File Offset: 0x0007322E
		protected virtual void OnParametersChanged(EventArgs e)
		{
			if (this._parametersChanged != null)
			{
				this._parametersChanged(this, e);
			}
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="o">The object being validated. </param>
		/// <exception cref="T:System.ArgumentException">The object is not an instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class or one of its derived classes. </exception>
		/// <exception cref="T:System.ArgumentNullException">The object is null. </exception>
		// Token: 0x06002C18 RID: 11288 RVA: 0x00075045 File Offset: 0x00073245
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is Parameter))
			{
				throw new ArgumentException("o is not a Parameter");
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
		/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to remove from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
		// Token: 0x06002C19 RID: 11289 RVA: 0x0005556C File Offset: 0x0005376C
		public void Remove(Parameter parameter)
		{
			((IList)this).Remove(parameter);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object at the specified index from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to remove. </param>
		// Token: 0x06002C1A RID: 11290 RVA: 0x00055575 File Offset: 0x00053775
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Marks the specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object as having changed since the last load or save from view state.</summary>
		/// <param name="o">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to mark as having changed since the last load or save from view state. </param>
		// Token: 0x06002C1B RID: 11291 RVA: 0x00075061 File Offset: 0x00073261
		protected override void SetDirtyObject(object o)
		{
			((Parameter)o).SetDirty();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0007506E File Offset: 0x0007326E
		internal void CallOnParameterChanged()
		{
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0007507C File Offset: 0x0007327C
		private int IndexOfString(string name)
		{
			for (int i = 0; i < base.Count; i++)
			{
				if (string.Compare(((Parameter)((IList)this)[i]).Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.Parameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Parameter" /> at the specified index in the collection. </returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to retrieve from the collection. </param>
		// Token: 0x17000E0C RID: 3596
		public Parameter this[int index]
		{
			get
			{
				return (Parameter)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.Parameter" /> object with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.Parameter" /> with the specified name in the collection. If the <see cref="T:System.Web.UI.WebControls.Parameter" /> is not found in the collection, the indexer returns null.</returns>
		/// <param name="name">The <see cref="P:System.Web.UI.WebControls.Parameter.Name" /> of the <see cref="T:System.Web.UI.WebControls.Parameter" /> to retrieve from the collection. </param>
		// Token: 0x17000E0D RID: 3597
		public Parameter this[string name]
		{
			get
			{
				int num = this.IndexOfString(name);
				if (num == -1)
				{
					return null;
				}
				return (Parameter)((IList)this)[num];
			}
			set
			{
				int num = this.IndexOfString(name);
				if (num == -1)
				{
					this.Add(value);
					return;
				}
				((IList)this)[num] = value;
			}
		}

		/// <summary>Occurs when one or more <see cref="T:System.Web.UI.WebControls.Parameter" /> objects contained by the collection changes state.</summary>
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06002C22 RID: 11298 RVA: 0x0007511A File Offset: 0x0007331A
		// (remove) Token: 0x06002C23 RID: 11299 RVA: 0x00075133 File Offset: 0x00073333
		public event EventHandler ParametersChanged
		{
			add
			{
				this._parametersChanged = (EventHandler)Delegate.Combine(this._parametersChanged, value);
			}
			remove
			{
				this._parametersChanged = (EventHandler)Delegate.Remove(this._parametersChanged, value);
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection contains a specific value</summary>
		/// <returns>true if the object is found in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />; otherwise, false. If null is passed for the <paramref name="value" /> parameter, false is returned.</returns>
		/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to locate in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />.</param>
		// Token: 0x06002C24 RID: 11300 RVA: 0x00055546 File Offset: 0x00053746
		public bool Contains(Parameter parameter)
		{
			return ((IList)this).Contains(parameter);
		}

		/// <summary>Copies a specified index of a parameter array to the parameter collection.</summary>
		/// <param name="parameterArray">Parameter array from which the value at a specified index is to be copied from.</param>
		/// <param name="index">The integer index of the <paramref name="parameterArray" /> item that is to be copied. </param>
		// Token: 0x06002C25 RID: 11301 RVA: 0x0005554F File Offset: 0x0005374F
		public void CopyTo(Parameter[] parameterArray, int index)
		{
			((ICollection)this).CopyTo(parameterArray, index);
		}

		/// <summary>Determines the index of a specified <see cref="T:System.Web.UI.WebControls.Parameter" /> object in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection.</summary>
		/// <returns>The index of <paramref name="parameter" />, if it is found in the collection; otherwise, -1.</returns>
		/// <param name="parameter">The <see cref="T:System.Web.UI.WebControls.Parameter" /> to locate in the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />.</param>
		// Token: 0x06002C26 RID: 11302 RVA: 0x00055559 File Offset: 0x00053759
		public int IndexOf(Parameter parameter)
		{
			return ((IList)this).IndexOf(parameter);
		}

		/// <summary>Occurs after the <see cref="M:System.Web.UI.WebControls.ParameterCollection.Remove(System.Web.UI.WebControls.Parameter)" /> method completes.</summary>
		/// <param name="index">The index in the collection that the <see cref="T:System.Web.UI.WebControls.Parameter" /> was removed from. </param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.Parameter" /> that was removed from the <see cref="T:System.Web.UI.WebControls.ParameterCollection" />. </param>
		// Token: 0x06002C27 RID: 11303 RVA: 0x0007514C File Offset: 0x0007334C
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x04001B37 RID: 6967
		private static Type[] _knownTypes = new Type[]
		{
			typeof(ControlParameter),
			typeof(CookieParameter),
			typeof(FormParameter),
			typeof(Parameter),
			typeof(QueryStringParameter),
			typeof(SessionParameter)
		};

		// Token: 0x04001B38 RID: 6968
		private EventHandler _parametersChanged;
	}
}
