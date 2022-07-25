using SFML.Graphics;
using SFML.System;
using System;
using TerrariaCloneV2.Utils;

namespace TerrariaCloneV2.Entities
{
	abstract class Entity : Transformable, Drawable
	{
		enum DIRECTION_TYPE { 
			LEFT, RIGHT, UP, DOWN
		}

		protected RectangleShape rect;
		protected Vector2f velocity;
		protected Vector2f movement;

		protected World world;

		protected bool isFly = true;
		protected bool isRectVisible = true;

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

			//Position += movement + velocity;

			if (Position.Y > Program.RenderWindow.Size.Y) {
				OnKill();
			}
		}

		private void UpdatePhysics() {

			//velocity += new Vector2f(0, 0.15f);
			velocity.X *= 0.99f;

			// гравитация
			velocity.Y += 0.55f;

			var offset = velocity + movement;

			// расстояние между текущей и будущей позицией
			float dist = MathUtils.GetDistance(offset);

			int countStep = 1; // количество "теневых" копий
			float stepSize = (float)Tile.TILE_SIZE / 2;
			if (dist > stepSize) {
				countStep = (int)(dist / stepSize); // mb Mathf.Floor
			}

			// проверяем коллизию с тайлом
			Vector2f nextPosition = Position + offset;
			Vector2f stepPosition = Position - rect.Origin;

			FloatRect stepRect = new FloatRect(stepPosition, rect.Size);

			// вектор "смещения", для получение координат следующего шага проверки коллизии
			// TODO: это stepOffset, автор скумбрия ебанная
			Vector2f stepsOffset = (nextPosition - Position) / countStep;

			for (int step = 0; step < countStep; step++) {

				bool isBreakStep = false;

				stepPosition += stepsOffset;
				stepRect = new FloatRect(stepPosition, rect.Size);

				DebugRenderer.AddRectangle(stepRect, Color.Blue);

				// ищем текущий тайл

				int i = (int)((stepPosition.X + rect.Size.X / 2) / Tile.TILE_SIZE);
				int j = (int)((stepPosition.Y + rect.Size.Y) / Tile.TILE_SIZE);

				Tile tile = world.GetTile(i, j);

				if (tile != null) {

					FloatRect entityRect = new FloatRect(
						tile.Position,
						new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE)
					);

					DebugRenderer.AddRectangle(entityRect, Color.Red);

					if (UpdateCollision(stepRect, entityRect, DIRECTION_TYPE.DOWN, ref stepPosition)) {
					
						velocity.Y = 0;
						isFly = false;
						isBreakStep = true;
					
					} else {

						isFly = true;
					}

				} else {

					isFly = true;
				}

				// -1 => левая стенка
				// 1 => правая стенка
				if (UpdateWallCollision(i, j, -1, ref stepPosition, stepRect) ||
					UpdateWallCollision(i, j, 1, ref stepPosition, stepRect)) {

					OnWallCollided();
					isBreakStep = true;
				}

				if (isBreakStep) {
					break;
				}
			}

			Position = stepPosition + rect.Origin;
		}

		// iOffset - смещение плитки по горизонтали
		private bool UpdateWallCollision(int i, int j, int iOffset, ref Vector2f stepPos, FloatRect stepRect) {
			
			var dirType = iOffset > 0 ? DIRECTION_TYPE.RIGHT : DIRECTION_TYPE.LEFT;

			Tile[] walls = new Tile[] {
				world.GetTile(i + iOffset, j - 1),
				world.GetTile(i + iOffset, j - 2),
				world.GetTile(i + iOffset, j - 3),
			};

			bool isWallCollided = false;
			foreach(Tile tile in walls) {
				
				if (tile == null) {
					continue;
				}

				FloatRect tileRect = new FloatRect(
					tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));


				DebugRenderer.AddRectangle(tileRect, Color.Yellow);
				if (UpdateCollision(stepRect, tileRect, dirType, ref stepPos)) {
					isWallCollided = true;
					break; /// ?????
				}
			}

			return isWallCollided;
		}

		// проверяет пересечение двух прямоугольник и корректирует позицию
		private bool UpdateCollision(FloatRect rectEntity, FloatRect rectTile, DIRECTION_TYPE dirType, ref Vector2f pos) {
			if (rectEntity.Intersects(rectTile)) {
				
				switch (dirType) {
					case DIRECTION_TYPE.UP:
						pos = new Vector2f(pos.X, rectTile.Top + rectTile.Height - 1);
						break;
					
					case DIRECTION_TYPE.DOWN:
						pos = new Vector2f(pos.X, rectTile.Top - rectEntity.Height + 1);
						break;

					case DIRECTION_TYPE.LEFT:
						pos = new Vector2f(rectTile.Left + rectTile.Width - 1, pos.Y);
						break;

					case DIRECTION_TYPE.RIGHT:
						pos = new Vector2f(rectTile.Left - rectEntity.Width + 1, pos.Y);
						break;
				}

				return true;
			}

			return false;
		}

		/*private void UpdateWallsCollisions(FloatRect entityRect, int pX, int pY) {

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
		}*/

		public void Draw(RenderTarget target, RenderStates states) {
			states.Transform *= Transform;

			if (isRectVisible) {
				target.Draw(rect, states);
			}

			DrawEntity(target, states);
		}

		public abstract void OnKill();
		public abstract void OnWallCollided();
		public abstract void UpdateEntity();
		public abstract void DrawEntity(RenderTarget target, RenderStates states);
	}
}
