using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selected" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserted" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updated" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002F7 RID: 759
	public class ObjectDataSourceStatusEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters and return value.</summary>
		/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
		/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
		// Token: 0x06001BCF RID: 7119 RVA: 0x000461F0 File Offset: 0x000443F0
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters)
			: this(returnValue, outputParameters, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters, return value, and exception.</summary>
		/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
		/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
		/// <param name="exception">An <see cref="T:System.Exception" /> that wraps any internal exceptions thrown during the method call. </param>
		// Token: 0x06001BD0 RID: 7120 RVA: 0x000461FB File Offset: 0x000443FB
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters, Exception exception)
		{
			this._returnValue = returnValue;
			this._outputParameters = outputParameters;
			this._exception = exception;
		}

		/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0004621F File Offset: 0x0004441F
		public IDictionary OutputParameters
		{
			get
			{
				return this._outputParameters;
			}
		}

		/// <summary>Gets a wrapper for any exceptions that are thrown by the method that is called by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control during a data operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the business object in its <see cref="P:System.Exception.InnerException" />.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00046227 File Offset: 0x00044427
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was thrown by the business object has been handled.</summary>
		/// <returns>true if an exception thrown by the business object has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />; otherwise, false.</returns>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0004622F File Offset: 0x0004442F
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x00046237 File Offset: 0x00044437
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		/// <summary>Gets the return value that is returned by the business object method, if any, as an object.</summary>
		/// <returns>An object that represents the return value returned by the business object method; otherwise, null, if the business object method returns no value.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00046240 File Offset: 0x00044440
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Gets or sets the number of rows that are affected by the data operation.</summary>
		/// <returns>The number of rows affected by the data operation.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00046248 File Offset: 0x00044448
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x00046250 File Offset: 0x00044450
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
			set
			{
				this._affectedRows = value;
			}
		}

		// Token: 0x04001731 RID: 5937
		private object _returnValue;

		// Token: 0x04001732 RID: 5938
		private IDictionary _outputParameters;

		// Token: 0x04001733 RID: 5939
		private Exception _exception;

		// Token: 0x04001734 RID: 5940
		private bool _exceptionHandled;

		// Token: 0x04001735 RID: 5941
		private int _affectedRows = -1;
	}
}
