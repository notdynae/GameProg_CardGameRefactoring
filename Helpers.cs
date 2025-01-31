namespace CardGameRefactoring;

// basic static class for useful helper methods
public static class Helpers
{
    public static int Clamp(int num, int max) {
        return Math.Max(0, Math.Min(num, max));
    }
}