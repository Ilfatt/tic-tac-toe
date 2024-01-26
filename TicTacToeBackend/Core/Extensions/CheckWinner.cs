using Core.Features.Game;
using MongoDB.Driver.Linq;

namespace Core.Extensions;

public static class CheckWinner
{
	public static GameMapSymbol? CheckMap(this GameMapSymbol[] map)
	{
		if (map.Length != 9) throw new ArgumentException("Map size not equal 9. ");

		if (map[0] == map[1] && map[1] == map[2]) return map[0];
		if (map[3] == map[4] && map[4] == map[5]) return map[3];
		if (map[6] == map[7] && map[7] == map[8]) return map[6];
		
		if (map[0] == map[3] && map[3] == map[6]) return map[0];
		if (map[1] == map[4] && map[4] == map[7]) return map[1];
		if (map[2] == map[5] && map[5] == map[8]) return map[2];
		
		if (map[0] == map[4] && map[4] == map[8]) return map[0];
		if (map[2] == map[4] && map[4] == map[6]) return map[2];

		if (map.All(x => x != GameMapSymbol.Empty)) return null;
		
		return GameMapSymbol.Empty;
	}
}