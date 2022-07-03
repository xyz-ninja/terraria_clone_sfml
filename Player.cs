using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaCloneV2
{
	class Player : Transformable, Drawable
	{
		private World world;

		private Vector2f rectSize;
		private RectangleShape rect;
		private RectangleShape rectDirection;

		private Vector2f velocity;

		private Vector2f startPosition;

		public Player(World world) {

			this.world = world;

			rectSize = new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f);
			
			rect = new RectangleShape(rectSize);
			rect.Origin = new Vector2f(rect.Size.X / 2, 0);

			rectDirection = new RectangleShape(new Vector2f(50, 3));
			rectDirection.FillColor = Color.Red;
			rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);
		}

		public void Spawn(Vector2f startPosition) {

			this.startPosition = startPosition;

			Position = startPosition;
		}

		public void Update() {

			UpdatePhysics();

			Position += velocity;
		}

		private void UpdatePhysics() {

			bool isFall = true;

			velocity += new Vector2f(0, 0.15f);

			int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
			int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);

			Tile tile = world.GetTile(pX, pY);

			if (tile != null) {
				Vector2f nextPosition = Position + velocity - rect.Origin;
				
				FloatRect playerRect = new FloatRect(nextPosition, rect.Size);
				FloatRect tileRect = new FloatRect(
					tile.Position,
					new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE)
				);

				isFall = !playerRect.Intersects(tileRect);
			}

			if (!isFall) {
				velocity.Y = 0;
			}
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			states.Transform *= Transform;

			target.Draw(rect, states);
			target.Draw(rectDirection, states);
		}
	}
}
