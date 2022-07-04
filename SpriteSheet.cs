using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	class SpriteSheet
	{
		private int subW, subH; // ширина фрагмента текстуры
		private int borderSize; // толщина рамки между фрагментами

		#region getters

		public int SubWidth => subW;
		public int SubHeight => subH;

		#endregion

		// hSize кол-во фрагментов по X или размер одного фрагмента в пикселях по ширине
		// vSize кол-во фрагментов по Y или размер одного фрагмента в пикселях по высоте
		// borderSize толщина рамки между фрагментами
		// texW, texH - ширины/высота всей текстуры
		public SpriteSheet(int hSize, int vSize, int borderSize, int texW = 0, int texH = 0) {
			
			if (borderSize > 0) {
			
				// для правильной работы алгоритма сразу увеличим толщину рамки на один
				this.borderSize = borderSize + 1;
			
			} else {

				this.borderSize = 0;
			}

			if (texW != 0 && texH != 0) {
				subW = texW / hSize;
				subH = texH / vSize;
			} else {
				subW = hSize;
				subH = vSize;
			}
		}

		public IntRect GetTextureRect(int col, int row) {
			
			int x = col * subW + col * borderSize;
			int y = row * subH + row * borderSize;

			return new IntRect(x, y, subW, subH);
		}
	}
}
