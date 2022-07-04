using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace TerrariaCloneV2
{
	class Player : Transformable, Drawable
	{
		private World world;

		private Vector2f rectSize;
		private RectangleShape rect;
		private RectangleShape rectDirection;

		private Vector2f velocity;
		private Vector2f movement; // вектор перемещения игрока

		private float horizontalSpeed = 4f;
		private float horizontalSpeedAcceleration = 0.2f;

		private Vector2f startPosition;

		#region getters/setters

		public int Direction {
			set {

				int dir = value >= 0 ? 1 : -1;
				
				Scale = new Vector2f(dir, 1);
			}

			get {

				int dir = Scale.X >= 0 ? 1 : -1;

				return dir;
			}
		}

		#endregion

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
			velocity = new Vector2f();
		}

		public void Update() {

			UpdatePhysics();
			UpdateMovement();

			Position += movement + velocity;

			if (Position.Y > Program.RenderWindow.Size.Y) {
				Spawn(this.startPosition);
			}
		}

		private void UpdatePhysics() {

			bool isFall = true;

			velocity += new Vector2f(0, 0.15f);

			// ищем тайл игрока
			
			int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE);
			int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE);

			Tile tile = world.GetTile(pX, pY);

			if (tile != null) {

				Vector2f nextPosition = Position + velocity - rect.Origin;
				
				// проверяем коллизию с тайлом

				FloatRect playerRect = new FloatRect(nextPosition, rect.Size);
				FloatRect tileRect = new FloatRect(
					tile.Position,
					new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE)
				);

				DebugRenderer.AddRectangle(tileRect, Color.Red);

				isFall = !playerRect.Intersects(tileRect);
			}

			if (!isFall) {
				velocity.Y = 0;
			}
		}

		private void UpdateMovement() {

			bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
			bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);

			bool isMove = isMoveLeft || isMoveRight;

			if (isMove) {

				if (isMoveLeft) {

					movement.X -= horizontalSpeedAcceleration;
					Direction = -1;

				} else if (isMoveRight) {

					movement.X += horizontalSpeedAcceleration;
					Direction = 1;
				}

				if (movement.X > horizontalSpeed) {
					movement.X = horizontalSpeed;
				} else if (movement.X < -horizontalSpeed) {
					movement.X = -horizontalSpeed;
				}

			} else {

				movement.X = 0;
			}
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			states.Transform *= Transform;

			target.Draw(rect, states);
			target.Draw(rectDirection, states);
		}
	}
}
