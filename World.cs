using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			for (int x = 2; x < 5; x++) {
				for (int y = 2; y < 5; y++) {

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
