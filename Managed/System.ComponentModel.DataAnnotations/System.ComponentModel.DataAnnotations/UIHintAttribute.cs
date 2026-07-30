using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the template or user control that Dynamic Data uses to display a data field. </summary>
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class UIHintAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the field template to use to display the data field.</summary>
		/// <returns>The name of the field template that displays the data field.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000043CD File Offset: 0x000025CD
		public string UIHint
		{
			get
			{
				return this._implementation.UIHint;
			}
		}

		/// <summary>Gets or sets the presentation layer that uses the <see cref="T:System.ComponentModel.DataAnnotations.UIHintAttribute" /> class. </summary>
		/// <returns>The presentation layer that is used by this class.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000043DA File Offset: 0x000025DA
		public string PresentationLayer
		{
			get
			{
				return this._implementation.PresentationLayer;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.DynamicData.DynamicControlParameter" /> object to use to retrieve values from any data source.</summary>
		/// <returns>A collection of key/value pairs. </returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000043E7 File Offset: 0x000025E7
		public IDictionary<string, object> ControlParameters
		{
			get
			{
				return this._implementation.ControlParameters;
			}
		}

		/// <summary>Gets the unique identifier for the attribute.</summary>
		/// <returns>The unique identifier for the attribute.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000351A File Offset: 0x0000171A
		public override object TypeId
		{
			get
			{
				return this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.UIHintAttribute" /> class by using a specified user control. </summary>
		/// <param name="uiHint">The user control to use to display the data field. </param>
		// Token: 0x06000115 RID: 277 RVA: 0x000043F4 File Offset: 0x000025F4
		public UIHintAttribute(string uiHint)
			: this(uiHint, null, new object[0])
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.UIHintAttribute" /> class using the specified user control and specified presentation layer. </summary>
		/// <param name="uiHint">The user control (field template) to use to display the data field.</param>
		/// <param name="presentationLayer">The presentation layer that uses the class. Can be set to "HTML", "Silverlight", "WPF", or "WinForms".</param>
		// Token: 0x06000116 RID: 278 RVA: 0x00004404 File Offset: 0x00002604
		public UIHintAttribute(string uiHint, string presentationLayer)
			: this(uiHint, presentationLayer, new object[0])
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.UIHintAttribute" /> class by using the specified user control, presentation layer, and control parameters.</summary>
		/// <param name="uiHint">The user control (field template) to use to display the data field.</param>
		/// <param name="presentationLayer">The presentation layer that uses the class. Can be set to "HTML", "Silverlight", "WPF", or "WinForms".</param>
		/// <param name="controlParameters">The object to use to retrieve values from any data sources. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.ComponentModel.DataAnnotations.UIHintAttribute.ControlParameters" /> is null or it is a constraint key.-or-The value of <see cref="P:System.ComponentModel.DataAnnotations.UIHintAttribute.ControlParameters" /> is not a string. </exception>
		// Token: 0x06000117 RID: 279 RVA: 0x00004414 File Offset: 0x00002614
		public UIHintAttribute(string uiHint, string presentationLayer, params object[] controlParameters)
		{
			this._implementation = new UIHintAttribute.UIHintImplementation(uiHint, presentationLayer, controlParameters);
		}

		/// <summary>Gets the hash code for the current instance of the attribute.</summary>
		/// <returns>The attribute instance hash code.</returns>
		// Token: 0x06000118 RID: 280 RVA: 0x0000442A File Offset: 0x0000262A
		public override int GetHashCode()
		{
			return this._implementation.GetHashCode();
		}

		/// <summary>Gets a value that indicates whether this instance is equal to the specified object.</summary>
		/// <returns>true if the specified object is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance, or a null reference.</param>
		// Token: 0x06000119 RID: 281 RVA: 0x00004438 File Offset: 0x00002638
		public override bool Equals(object obj)
		{
			UIHintAttribute uihintAttribute = obj as UIHintAttribute;
			return uihintAttribute != null && this._implementation.Equals(uihintAttribute._implementation);
		}

		// Token: 0x040000A1 RID: 161
		private UIHintAttribute.UIHintImplementation _implementation;

		// Token: 0x02000032 RID: 50
		internal class UIHintImplementation
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00004462 File Offset: 0x00002662
			// (set) Token: 0x0600011B RID: 283 RVA: 0x0000446A File Offset: 0x0000266A
			public string UIHint { get; private set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00004473 File Offset: 0x00002673
			// (set) Token: 0x0600011D RID: 285 RVA: 0x0000447B File Offset: 0x0000267B
			public string PresentationLayer { get; private set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600011E RID: 286 RVA: 0x00004484 File Offset: 0x00002684
			public IDictionary<string, object> ControlParameters
			{
				get
				{
					if (this._controlParameters == null)
					{
						this._controlParameters = this.BuildControlParametersDictionary();
					}
					return this._controlParameters;
				}
			}

			// Token: 0x0600011F RID: 287 RVA: 0x000044A0 File Offset: 0x000026A0
			public UIHintImplementation(string uiHint, string presentationLayer, params object[] controlParameters)
			{
				this.UIHint = uiHint;
				this.PresentationLayer = presentationLayer;
				if (controlParameters != null)
				{
					this._inputControlParameters = new object[controlParameters.Length];
					Array.Copy(controlParameters, this._inputControlParameters, controlParameters.Length);
				}
			}

			// Token: 0x06000120 RID: 288 RVA: 0x000044D8 File Offset: 0x000026D8
			public override int GetHashCode()
			{
				object obj = this.UIHint ?? string.Empty;
				string text = this.PresentationLayer ?? string.Empty;
				return obj.GetHashCode() ^ text.GetHashCode();
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00004510 File Offset: 0x00002710
			public override bool Equals(object obj)
			{
				UIHintAttribute.UIHintImplementation uihintImplementation = (UIHintAttribute.UIHintImplementation)obj;
				if (this.UIHint != uihintImplementation.UIHint || this.PresentationLayer != uihintImplementation.PresentationLayer)
				{
					return false;
				}
				IDictionary<string, object> controlParameters;
				IDictionary<string, object> controlParameters2;
				try
				{
					controlParameters = this.ControlParameters;
					controlParameters2 = uihintImplementation.ControlParameters;
				}
				catch (InvalidOperationException)
				{
					return false;
				}
				if (controlParameters.Count != controlParameters2.Count)
				{
					return false;
				}
				return controlParameters.OrderBy((KeyValuePair<string, object> p) => p.Key).SequenceEqual(controlParameters2.OrderBy((KeyValuePair<string, object> p) => p.Key));
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000045D4 File Offset: 0x000027D4
			private IDictionary<string, object> BuildControlParametersDictionary()
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				object[] inputControlParameters = this._inputControlParameters;
				if (inputControlParameters == null || inputControlParameters.Length == 0)
				{
					return dictionary;
				}
				if (inputControlParameters.Length % 2 != 0)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The number of control parameters must be even.", Array.Empty<object>()));
				}
				for (int i = 0; i < inputControlParameters.Length; i += 2)
				{
					object obj = inputControlParameters[i];
					object obj2 = inputControlParameters[i + 1];
					if (obj == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The key parameter at position {0} is null. Every key control parameter must be a string.", i));
					}
					string text = obj as string;
					if (text == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The key parameter at position {0} with value '{1}' is not a string. Every key control parameter must be a string.", i, inputControlParameters[i].ToString()));
					}
					if (dictionary.ContainsKey(text))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The key parameter at position {0} with value '{1}' occurs more than once.", i, text));
					}
					dictionary[text] = obj2;
				}
				return dictionary;
			}

			// Token: 0x040000A2 RID: 162
			private IDictionary<string, object> _controlParameters;

			// Token: 0x040000A3 RID: 163
			private object[] _inputControlParameters;
		}
	}
}
