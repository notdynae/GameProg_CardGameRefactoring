namespace CardGameRefactoring;

// basic static class for all rng used across project
public static class Rng
{
	private static readonly Random Random = new Random();
	public static int Next(int maxValue, int minValue = 0) {
		return Random.Next(minValue, maxValue);
	}
}
