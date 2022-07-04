using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace TerrariaCloneV2
{
	class World : Transformable, Drawable
	{
		public const int WORLD_SIZE = 5;

		Chunk[][] chunks;

		public World() {

			chunks = new Chunk[WORLD_SIZE][];

			for (int i = 0; i < WORLD_SIZE; i++) {
				chunks[i] = new Chunk[WORLD_SIZE];
			}
		}

		public void GenerateWorld() {

			// трава
			for (int x = 3; x <= 46; x++) {
				for (int y = 17; y <= 17; y++) {

					CreateTile(TILE_TYPE.GRASS, x, y);
				}
			}

			// земля
			for (int x = 3; x <= 46; x++) {
				for (int y = 18; y <= 32; y++) {

					CreateTile(TILE_TYPE.GROUND, x, y);
				}
			}

			for (int x = 3; x <= 4; x++) {
				for (int y = 1; y <= 17; y++) {

					CreateTile(TILE_TYPE.GROUND, x, y);
				}
			}

			for (int x = 45; x <= 46; x++) {
				for (int y = 1; y <= 17; y++) {

					CreateTile(TILE_TYPE.GROUND, x, y);
				}
			}
		}

		public void CreateTile(TILE_TYPE type, int x, int y) {
			
			var chunk = GetChunk(x, y);
			var tilePos = GetTilePositionInChunk(x, y);

			var tile = chunk.CreateTile(type, tilePos.X, tilePos.Y);

			Tile upTile = GetTile(x, y - 1);
			Tile downTile = GetTile(x, y + 1);
			Tile leftTile = GetTile(x - 1, y);
			Tile rightTile = GetTile(x + 1, y);

			tile.SetAroundTiles(upTile, downTile, leftTile, rightTile);
		}

		public Tile GetTile(int x, int y) {
			var chunk = GetChunk(x, y);
			
			if (chunk == null) {
				return null; 
			}
			
			var tilePos = GetTilePositionInChunk(x, y);

			return chunk.GetTile(tilePos.X, tilePos.Y);
		}

		public Chunk GetChunk(int x, int y) {
			
			int X = x / Chunk.CHUNK_SIZE;
			int Y = y / Chunk.CHUNK_SIZE;

			if (X >= WORLD_SIZE || Y >= WORLD_SIZE || X < 0 || Y < 0) {
				
				return null;
			}

			if (chunks[X][Y] == null) {
				chunks[X][Y] = new Chunk(new Vector2i(X, Y));
			}

			return chunks[X][Y];
		}

		// позиция тайла в массиве чанка
		public Vector2i GetTilePositionInChunk(int x, int y) {

			int X = x / Chunk.CHUNK_SIZE;
			int Y = y / Chunk.CHUNK_SIZE;

			return new Vector2i(x - X * Chunk.CHUNK_SIZE, y - Y * Chunk.CHUNK_SIZE);
		}

		public void Draw(RenderTarget target, RenderStates states) {
			
			for (int x = 0; x < WORLD_SIZE; x++) {
				for (int y = 0; y < WORLD_SIZE; y++) {

					var currentChunk = chunks[x][y];
					
					if (currentChunk == null) {
						continue;
					}

					target.Draw(currentChunk);
				}
			}
		}
	}
}
