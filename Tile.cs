using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	enum TILE_TYPE {
		NONE, GROUND, GRASS
	}

	class Tile : Transformable, Drawable {

		public const int TILE_SIZE = 16;

		private TILE_TYPE tileType = TILE_TYPE.GROUND;

		private RectangleShape rectShape;

		// соседние тайлы
		private Tile upTile;
		private Tile downTile;
		private Tile leftTile;
		private Tile rightTile;

		#region getters
		
		public TILE_TYPE TileType => tileType;
		
		#endregion

		public Tile(TILE_TYPE type) {

			tileType = type;

			rectShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

			switch(tileType) {
				
				case TILE_TYPE.NONE:
					break;

				case TILE_TYPE.GROUND:

					rectShape.Texture = Content.texTiles0;

					break;

				case TILE_TYPE.GRASS:

					rectShape.Texture = Content.texTiles1;
					
					break;
			}

			// получаем тайл из текстуры по ряду и столбцу
			rectShape.TextureRect = GetTextureRect(1, 1);
		}

		// обновляет внешний вид тайла в зависимости от соседей
		public void UpdateVisual() {

		}

		// получаем фрагмент текстуры
		public IntRect GetTextureRect(int i, int j) {

			int x = i * TILE_SIZE + i;
			int y = j * TILE_SIZE + j;

			return new IntRect(x, y, TILE_SIZE, TILE_SIZE);
		}

		public void SetAroundTiles(Tile upTile, Tile downTile, Tile leftTile, Tile rightTile) {

			if (upTile != null) {
				this.upTile = upTile;
				this.upTile.downTile = this;
			}

			if (downTile != null) {
				this.downTile = downTile;
				this.downTile.upTile = this;
			}

			if (leftTile != null) {
				this.leftTile = leftTile;
				this.leftTile.rightTile = this;
			}

			if (rightTile != null) {
				this.rightTile = rightTile;
				this.rightTile.leftTile = this;
			}

			UpdateVisual();
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			states.Transform *= Transform;

			target.Draw(rectShape, states);
		}
	}
}
