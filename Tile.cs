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

			rectShape.TextureRect = GetTextureRect(0, 0);
		}

		public IntRect GetTextureRect(int i, int j) {

			int x = i * TILE_SIZE;
			int y = j * TILE_SIZE;

			return new IntRect(x, y, TILE_SIZE, TILE_SIZE);
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			states.Transform *= Transform;

			target.Draw(rectShape, states);
		}
	}
}
