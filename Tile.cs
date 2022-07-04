using SFML.Graphics;
using SFML.System;
using System.Diagnostics;
namespace TerrariaCloneV2
{
	enum TILE_TYPE {
		NONE, GROUND, GRASS
	}

	class Tile : Transformable, Drawable 
	{

		public const int TILE_SIZE = 16;

		private TILE_TYPE tileType = TILE_TYPE.GROUND;

		private RectangleShape rectShape;
		private SpriteSheet spriteSheet;

		// соседние тайлы
		private Tile upTile;
		private Tile downTile;
		private Tile leftTile;
		private Tile rightTile;

		#region getters/setters
		
		public TILE_TYPE TileType => tileType;

		public Tile UpTile { get => upTile; set { upTile = value; UpdateVisual(); } }
		public Tile DownTile { get => downTile; set { downTile = value; UpdateVisual(); } }
		public Tile LeftTile { get => leftTile; set { leftTile = value; UpdateVisual(); } }
		public Tile RightTile { get => rightTile; set { rightTile = value; UpdateVisual(); } }

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

			spriteSheet = new SpriteSheet(TILE_SIZE, TILE_SIZE, 1);
		}

		// обновляет внешний вид тайла в зависимости от соседей
		// attention: мега всратый код
		public void UpdateVisual() {

			var targetRect = new IntRect();

			// если тайл окружен
			if (upTile != null && downTile != null && leftTile != null && rightTile != null) {

				int i = Program.Rand.Next(0, 3); // случайное число от 0 до 2
				targetRect = spriteSheet.GetTextureRect(1 + i, 1);

			// если у тайла отсутствуют все соседи
			} else if (upTile == null && downTile == null && leftTile == null && rightTile == null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(9 + i, 3);

			// ВЕРТИКАЛЬНЫЕ
			// если у тайла отсутствует только верхний сосед
			} else if (upTile == null && downTile != null && leftTile != null && rightTile != null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(1 + i, 0);

			// если у тайла отсутствует только нижний сосед
			} else if (upTile != null && downTile == null && leftTile != null && rightTile != null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(1 + i, 2);

			// ГОРИЗОНТАЛЬНЫЕ
			// если у тайла отсутствует только левый сосед
			} else if (upTile != null && downTile != null && leftTile == null && rightTile != null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(0, i);

			// если у тайла отсутствует только правый сосед
			} else if (upTile != null && downTile != null && leftTile != null && rightTile == null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(4, i);

			// ДИАГОНАЛЬНЫЕ
			// если у тайла отсутствует верхний и левый сосед
			} else if (upTile == null && downTile != null && leftTile == null && rightTile != null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(0 + i * 2, 3);

			// если у тайла отсутствует верхний и правый сосед
			} else if (upTile == null && downTile != null && leftTile != null && rightTile == null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(1 + i * 2, 3);

			// если у тайла отсутствует нижний и левый сосед
			} else if (upTile != null && downTile == null && leftTile == null && rightTile != null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(0 + i * 2, 4);

			// если у тайла отсутствует нижний и правый сосед
			} else if (upTile != null && downTile == null && leftTile != null && rightTile == null) {
				int i = Program.Rand.Next(0, 3);
				targetRect = spriteSheet.GetTextureRect(1 + i * 2, 4);

			} else  {

				Debug.Print("Missing tile rect");

				int i = Program.Rand.Next(0, 3);

				targetRect = spriteSheet.GetTextureRect(1 + i, 1);
			}

			// получаем тайл из текстуры по ряду и столбцу
			rectShape.TextureRect = targetRect;
		}

		public void SetAroundTiles(Tile upTile, Tile downTile, Tile leftTile, Tile rightTile) {

			if (upTile != null) {
				this.upTile = upTile;
				this.upTile.DownTile = this;
			}

			if (downTile != null) {
				this.downTile = downTile;
				this.downTile.UpTile = this;
			}

			if (leftTile != null) {
				this.leftTile = leftTile;
				this.leftTile.RightTile = this;
			}

			if (rightTile != null) {
				this.rightTile = rightTile;
				this.rightTile.LeftTile = this;
			}

			UpdateVisual();
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			states.Transform *= Transform;

			target.Draw(rectShape, states);
		}
	}
}
