using SFML.System;
using SFML.Graphics;
using SFML.Window;
using TerrariaCloneV2.Entities;
using System.Diagnostics;

namespace TerrariaCloneV2
{
	class Player : Entity
	{
		private RectangleShape rectDirection;

		private float horizontalSpeed = 4f;
		private float horizontalSpeedAcceleration = 0.2f;

		public Player(World world) : base(world)
		{
			rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f));
			rect.Origin = new Vector2f(rect.Size.X / 2, 0);

			rectDirection = new RectangleShape(new Vector2f(50, 3));
			rectDirection.FillColor = Color.Red;
			rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);
		}

		public override void OnKill() {
			Spawn(this.startPosition);
		}

		public override void OnWallCollided() {

			//Debug.Print("Collided");

			//throw new NotImplementedException();
		}

		public override void UpdateEntity() {
			UpdateMovement();
		}

		public override void DrawEntity(RenderTarget target, RenderStates states) {
			target.Draw(rectDirection, states);
		}

		private void UpdateMovement() {

			bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
			bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);

			bool isMove = isMoveLeft || isMoveRight;

			if (isMove) {

				if (isMoveLeft) {

					// если игрок резко сменил направление движения, убираем инерцию
					if (movement.X > 0) {
						movement.X = 0;
					}

					movement.X -= horizontalSpeedAcceleration;
					Direction = -1;

				} else if (isMoveRight) {

					if (movement.X < 0) {
						movement.X = 0;
					}

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
	}
}
