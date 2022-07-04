using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace TerrariaCloneV2
{
	class Chunk : Transformable, Drawable 
	{

		public const int CHUNK_SIZE = 25;

		private Vector2i chunkPosition; // позиция чанка в массиве мира

		private Tile[][] tiles;

		public Chunk(Vector2i chunkPos)
		{
			chunkPosition = chunkPos;

			tiles = new Tile[CHUNK_SIZE][];

			Position = new Vector2f(
				chunkPosition.X * CHUNK_SIZE * Tile.TILE_SIZE,
				chunkPosition.Y * CHUNK_SIZE * Tile.TILE_SIZE
			);

			for (int i = 0; i < CHUNK_SIZE; i++) {
				tiles[i] = new Tile[CHUNK_SIZE];
			}
		}

		public Tile CreateTile(TILE_TYPE type, int x, int y) {

			tiles[x][y] = new Tile(type);
			
			var tile = tiles[x][y];
			tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;

			return tile;
		}

		public Tile GetTile(int x, int y) {

			if (x < 0 || y < 0 || x > CHUNK_SIZE - 1 || y > CHUNK_SIZE - 1) {
				return null;
			}

			// возвращаем плитку, она все ещё может быть null
			return tiles[x][y];
		}

		public void Draw(RenderTarget target, RenderStates states) {

			//states.Transform *= Transform;

			for (int x = 0; x < CHUNK_SIZE; x++) {
				for (int y = 0; y < CHUNK_SIZE; y++) {

					var currentTile = tiles[x][y];

					if (currentTile == null) {
						continue;
					}

					target.Draw(tiles[x][y]);
				}
			}
		}
	}
}
