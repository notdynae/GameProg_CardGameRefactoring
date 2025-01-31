namespace CardGameRefactoring;

public class Deck
{
    
	public List<Card> Stack = [];
	public List<Card> Hand = [];
    
    public int HandSize { get; protected set; } = 3;

    public bool HandFull => Hand.Count >= HandSize;
    public bool DeckEmpty => Stack.Count > 0;

    public void InitializeDeck()
    {
        // Add cards to deck
        for (var i = 0; i < 5; i++) Stack.Add(new Fireball());
        for (var i = 0; i < 5; i++) Stack.Add(new IceShield());
        for (var i = 0; i < 3; i++) Stack.Add(new Heal());
        for (var i = 0; i < 4; i++) Stack.Add(new Slash());
        for (var i = 0; i < 3; i++) Stack.Add(new PowerUp());

        // Shuffle deck / fill hand to start game
        ShuffleDeck(Stack);
        DrawCards();
    }

    // shuffles order of cards in deck
    private void ShuffleDeck(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            var k = Rng.Next(n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }
    }
    
    // populates hand with cards until 
    public void DrawCards()
    {
        while (!HandFull && !DeckEmpty)
        {
            Hand.Add(Stack[0]);
            Stack.RemoveAt(0);
        }
    }
}

