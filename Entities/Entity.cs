using SFML.Graphics;
using SFML.System;
using System;

namespace TerrariaCloneV2.Entities
{
	abstract class Entity : Transformable, Drawable
	{
		protected RectangleShape rect;
		protected Vector2f velocity;
		protected Vector2f movement;

		protected World world;
		protected bool isFly = true;

		protected Vector2f startPosition;

		#region getters/setters

		public int Direction {
			set 
			{	
				int dir = value >= 0 ? 1 : -1;

				Scale = new Vector2f(dir, 1);
			}

			get 
			{
				int dir = Scale.X >= 0 ? 1 : -1;

				return dir;
			}
		}

		#endregion

		public Entity(World world) {
			this.world = world;
		}

		public void Spawn(Vector2f startPosition) {

			this.startPosition = startPosition;

			Position = startPosition;
			velocity = new Vector2f();
		}

		public void Update() {

			UpdateEntity();
			UpdatePhysics();

			Position += movement + velocity;

			if (Position.Y > Program.RenderWindow.Size.Y) {
				OnKill();
			}
		}

		private void UpdatePhysics() {

			bool isFall = true;

			//velocity += new Vector2f(0, 0.15f);
			velocity.X *= 0.99f;

			// гравитация
			velocity.Y += 0.25f;

			// проверяем коллизию с тайлом
			Vector2f nextPosition = Position + velocity - rect.Origin;

			FloatRect entityRect = new FloatRect(nextPosition, rect.Size);

			// ищем тайл игрока

			int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
			int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);

			Tile tile = world.GetTile(pX, pY);

			if (tile != null) {

				FloatRect tileRect = new FloatRect(
					tile.Position,
					new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE)
				);

				DebugRenderer.AddRectangle(tileRect, Color.Red);

				isFall = entityRect.Intersects(tileRect) == false;
				isFly = isFall;
			}

			if (!isFall) {
				velocity.Y = 0;
			}

			UpdateWallsCollisions(entityRect, pX, pY);
		}

		private void UpdateWallsCollisions(FloatRect entityRect, int pX, int pY) {
			Tile[] walls = new Tile[] {
				world.GetTile(pX - 1, pY - 1),
				world.GetTile(pX - 1, pY - 2),
				world.GetTile(pX - 1, pY - 3),
				world.GetTile(pX + 1, pY - 1),
				world.GetTile(pX + 1, pY - 2),
				world.GetTile(pX + 1, pY - 3),
			};

			foreach (Tile wallTile in walls) {
				if (wallTile == null) {
					continue;
				}

				FloatRect tileRect = new FloatRect(
					wallTile.Position,
					new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE)
				);

				DebugRenderer.AddRectangle(tileRect, Color.Yellow);

				if (entityRect.Intersects(tileRect)) {

					Vector2f offset = new Vector2f(entityRect.Left - tileRect.Left, 0);
					offset.X /= Math.Abs(offset.X);

					float speed = Math.Abs(movement.X);

					if (offset.X > 0) {

						Position = new Vector2f((tileRect.Left + tileRect.Width) + entityRect.Width / 2, Position.Y);

						movement.X = 0;
						//velocity.X = 0;

					} else if (offset.X < 0) {

						Position = new Vector2f(tileRect.Left - entityRect.Width / 2, Position.Y);

						movement.X = 0;
						//velocity.X = 0;
					}

					OnWallCollided();
				}
			}
		}

		public void Draw(RenderTarget target, RenderStates states) {
			states.Transform *= Transform;

			target.Draw(rect, states);

			DrawEntity(target, states);
		}

		public abstract void OnKill();
		public abstract void OnWallCollided();
		public abstract void UpdateEntity();
		public abstract void DrawEntity(RenderTarget target, RenderStates states);
	}
}
