namespace CardGameRefactoring;

public static class Helpers
{
    public static int Clamp(int num, int max) {
        return Math.Max(0, Math.Min(num, max));
    }
}