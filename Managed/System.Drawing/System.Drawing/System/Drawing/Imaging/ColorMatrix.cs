using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a 5 x 5 matrix that contains the coordinates for the RGBAW space. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color matrix. This class cannot be inherited.</summary>
	// Token: 0x020000F6 RID: 246
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ColorMatrix
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class.</summary>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001A6CA File Offset: 0x000188CA
		public ColorMatrix()
		{
			this._matrix00 = 1f;
			this._matrix11 = 1f;
			this._matrix22 = 1f;
			this._matrix33 = 1f;
			this._matrix44 = 1f;
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001A709 File Offset: 0x00018909
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0001A711 File Offset: 0x00018911
		public float Matrix00
		{
			get
			{
				return this._matrix00;
			}
			set
			{
				this._matrix00 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" /> .</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0001A71A File Offset: 0x0001891A
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0001A722 File Offset: 0x00018922
		public float Matrix01
		{
			get
			{
				return this._matrix01;
			}
			set
			{
				this._matrix01 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0001A72B File Offset: 0x0001892B
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0001A733 File Offset: 0x00018933
		public float Matrix02
		{
			get
			{
				return this._matrix02;
			}
			set
			{
				this._matrix02 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the 0 row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0001A73C File Offset: 0x0001893C
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0001A744 File Offset: 0x00018944
		public float Matrix03
		{
			get
			{
				return this._matrix03;
			}
			set
			{
				this._matrix03 = value;
			}
		}

		/// <summary>Gets or sets the element at the 0 (zero) row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the 0 row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0001A74D File Offset: 0x0001894D
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0001A755 File Offset: 0x00018955
		public float Matrix04
		{
			get
			{
				return this._matrix04;
			}
			set
			{
				this._matrix04 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0001A75E File Offset: 0x0001895E
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0001A766 File Offset: 0x00018966
		public float Matrix10
		{
			get
			{
				return this._matrix10;
			}
			set
			{
				this._matrix10 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0001A76F File Offset: 0x0001896F
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0001A777 File Offset: 0x00018977
		public float Matrix11
		{
			get
			{
				return this._matrix11;
			}
			set
			{
				this._matrix11 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001A780 File Offset: 0x00018980
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0001A788 File Offset: 0x00018988
		public float Matrix12
		{
			get
			{
				return this._matrix12;
			}
			set
			{
				this._matrix12 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the first row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0001A791 File Offset: 0x00018991
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0001A799 File Offset: 0x00018999
		public float Matrix13
		{
			get
			{
				return this._matrix13;
			}
			set
			{
				this._matrix13 = value;
			}
		}

		/// <summary>Gets or sets the element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the first row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0001A7A2 File Offset: 0x000189A2
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0001A7AA File Offset: 0x000189AA
		public float Matrix14
		{
			get
			{
				return this._matrix14;
			}
			set
			{
				this._matrix14 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001A7B3 File Offset: 0x000189B3
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x0001A7BB File Offset: 0x000189BB
		public float Matrix20
		{
			get
			{
				return this._matrix20;
			}
			set
			{
				this._matrix20 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0001A7C4 File Offset: 0x000189C4
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0001A7CC File Offset: 0x000189CC
		public float Matrix21
		{
			get
			{
				return this._matrix21;
			}
			set
			{
				this._matrix21 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0001A7D5 File Offset: 0x000189D5
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0001A7DD File Offset: 0x000189DD
		public float Matrix22
		{
			get
			{
				return this._matrix22;
			}
			set
			{
				this._matrix22 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001A7E6 File Offset: 0x000189E6
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0001A7EE File Offset: 0x000189EE
		public float Matrix23
		{
			get
			{
				return this._matrix23;
			}
			set
			{
				this._matrix23 = value;
			}
		}

		/// <summary>Gets or sets the element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the second row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0001A7F7 File Offset: 0x000189F7
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0001A7FF File Offset: 0x000189FF
		public float Matrix24
		{
			get
			{
				return this._matrix24;
			}
			set
			{
				this._matrix24 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0001A808 File Offset: 0x00018A08
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0001A810 File Offset: 0x00018A10
		public float Matrix30
		{
			get
			{
				return this._matrix30;
			}
			set
			{
				this._matrix30 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001A819 File Offset: 0x00018A19
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0001A821 File Offset: 0x00018A21
		public float Matrix31
		{
			get
			{
				return this._matrix31;
			}
			set
			{
				this._matrix31 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001A82A File Offset: 0x00018A2A
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0001A832 File Offset: 0x00018A32
		public float Matrix32
		{
			get
			{
				return this._matrix32;
			}
			set
			{
				this._matrix32 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the third row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001A83B File Offset: 0x00018A3B
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0001A843 File Offset: 0x00018A43
		public float Matrix33
		{
			get
			{
				return this._matrix33;
			}
			set
			{
				this._matrix33 = value;
			}
		}

		/// <summary>Gets or sets the element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the third row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001A84C File Offset: 0x00018A4C
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001A854 File Offset: 0x00018A54
		public float Matrix34
		{
			get
			{
				return this._matrix34;
			}
			set
			{
				this._matrix34 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and 0 (zero) column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and 0 column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001A85D File Offset: 0x00018A5D
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0001A865 File Offset: 0x00018A65
		public float Matrix40
		{
			get
			{
				return this._matrix40;
			}
			set
			{
				this._matrix40 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and first column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001A86E File Offset: 0x00018A6E
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0001A876 File Offset: 0x00018A76
		public float Matrix41
		{
			get
			{
				return this._matrix41;
			}
			set
			{
				this._matrix41 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and second column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0001A87F File Offset: 0x00018A7F
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x0001A887 File Offset: 0x00018A87
		public float Matrix42
		{
			get
			{
				return this._matrix42;
			}
			set
			{
				this._matrix42 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />. Represents the alpha component.</summary>
		/// <returns>The element at the fourth row and third column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0001A890 File Offset: 0x00018A90
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0001A898 File Offset: 0x00018A98
		public float Matrix43
		{
			get
			{
				return this._matrix43;
			}
			set
			{
				this._matrix43 = value;
			}
		}

		/// <summary>Gets or sets the element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the fourth row and fourth column of this <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0001A8A1 File Offset: 0x00018AA1
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0001A8A9 File Offset: 0x00018AA9
		public float Matrix44
		{
			get
			{
				return this._matrix44;
			}
			set
			{
				this._matrix44 = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMatrix" /> class using the elements in the specified matrix <paramref name="newColorMatrix" />.</summary>
		/// <param name="newColorMatrix">The values of the elements for the new <see cref="T:System.Drawing.Imaging.ColorMatrix" />. </param>
		// Token: 0x06000C1A RID: 3098 RVA: 0x0001A8B2 File Offset: 0x00018AB2
		[CLSCompliant(false)]
		public ColorMatrix(float[][] newColorMatrix)
		{
			this.SetMatrix(newColorMatrix);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001A8C4 File Offset: 0x00018AC4
		internal void SetMatrix(float[][] newColorMatrix)
		{
			this._matrix00 = newColorMatrix[0][0];
			this._matrix01 = newColorMatrix[0][1];
			this._matrix02 = newColorMatrix[0][2];
			this._matrix03 = newColorMatrix[0][3];
			this._matrix04 = newColorMatrix[0][4];
			this._matrix10 = newColorMatrix[1][0];
			this._matrix11 = newColorMatrix[1][1];
			this._matrix12 = newColorMatrix[1][2];
			this._matrix13 = newColorMatrix[1][3];
			this._matrix14 = newColorMatrix[1][4];
			this._matrix20 = newColorMatrix[2][0];
			this._matrix21 = newColorMatrix[2][1];
			this._matrix22 = newColorMatrix[2][2];
			this._matrix23 = newColorMatrix[2][3];
			this._matrix24 = newColorMatrix[2][4];
			this._matrix30 = newColorMatrix[3][0];
			this._matrix31 = newColorMatrix[3][1];
			this._matrix32 = newColorMatrix[3][2];
			this._matrix33 = newColorMatrix[3][3];
			this._matrix34 = newColorMatrix[3][4];
			this._matrix40 = newColorMatrix[4][0];
			this._matrix41 = newColorMatrix[4][1];
			this._matrix42 = newColorMatrix[4][2];
			this._matrix43 = newColorMatrix[4][3];
			this._matrix44 = newColorMatrix[4][4];
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		internal float[][] GetMatrix()
		{
			float[][] array = new float[5][];
			for (int i = 0; i < 5; i++)
			{
				array[i] = new float[5];
			}
			array[0][0] = this._matrix00;
			array[0][1] = this._matrix01;
			array[0][2] = this._matrix02;
			array[0][3] = this._matrix03;
			array[0][4] = this._matrix04;
			array[1][0] = this._matrix10;
			array[1][1] = this._matrix11;
			array[1][2] = this._matrix12;
			array[1][3] = this._matrix13;
			array[1][4] = this._matrix14;
			array[2][0] = this._matrix20;
			array[2][1] = this._matrix21;
			array[2][2] = this._matrix22;
			array[2][3] = this._matrix23;
			array[2][4] = this._matrix24;
			array[3][0] = this._matrix30;
			array[3][1] = this._matrix31;
			array[3][2] = this._matrix32;
			array[3][3] = this._matrix33;
			array[3][4] = this._matrix34;
			array[4][0] = this._matrix40;
			array[4][1] = this._matrix41;
			array[4][2] = this._matrix42;
			array[4][3] = this._matrix43;
			array[4][4] = this._matrix44;
			return array;
		}

		/// <summary>Gets or sets the element at the specified row and column in the <see cref="T:System.Drawing.Imaging.ColorMatrix" />.</summary>
		/// <returns>The element at the specified row and column.</returns>
		/// <param name="row">The row of the element.</param>
		/// <param name="column">The column of the element.</param>
		// Token: 0x17000352 RID: 850
		public float this[int row, int column]
		{
			get
			{
				return this.GetMatrix()[row][column];
			}
			set
			{
				float[][] matrix = this.GetMatrix();
				matrix[row][column] = value;
				this.SetMatrix(matrix);
			}
		}

		// Token: 0x04000832 RID: 2098
		private float _matrix00;

		// Token: 0x04000833 RID: 2099
		private float _matrix01;

		// Token: 0x04000834 RID: 2100
		private float _matrix02;

		// Token: 0x04000835 RID: 2101
		private float _matrix03;

		// Token: 0x04000836 RID: 2102
		private float _matrix04;

		// Token: 0x04000837 RID: 2103
		private float _matrix10;

		// Token: 0x04000838 RID: 2104
		private float _matrix11;

		// Token: 0x04000839 RID: 2105
		private float _matrix12;

		// Token: 0x0400083A RID: 2106
		private float _matrix13;

		// Token: 0x0400083B RID: 2107
		private float _matrix14;

		// Token: 0x0400083C RID: 2108
		private float _matrix20;

		// Token: 0x0400083D RID: 2109
		private float _matrix21;

		// Token: 0x0400083E RID: 2110
		private float _matrix22;

		// Token: 0x0400083F RID: 2111
		private float _matrix23;

		// Token: 0x04000840 RID: 2112
		private float _matrix24;

		// Token: 0x04000841 RID: 2113
		private float _matrix30;

		// Token: 0x04000842 RID: 2114
		private float _matrix31;

		// Token: 0x04000843 RID: 2115
		private float _matrix32;

		// Token: 0x04000844 RID: 2116
		private float _matrix33;

		// Token: 0x04000845 RID: 2117
		private float _matrix34;

		// Token: 0x04000846 RID: 2118
		private float _matrix40;

		// Token: 0x04000847 RID: 2119
		private float _matrix41;

		// Token: 0x04000848 RID: 2120
		private float _matrix42;

		// Token: 0x04000849 RID: 2121
		private float _matrix43;

		// Token: 0x0400084A RID: 2122
		private float _matrix44;
	}
}
