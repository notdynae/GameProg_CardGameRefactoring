namespace CardGameRefactoring;

public class Deck
{
	private List<Card> _deck = new List<Card>();
	private List<Card> _hand = new List<Card>();
	
	private void InitializeDecks()
    {
        
        // Add cards to player deck
        for (int i = 0; i < 5; i++) _deck.Add(new Fireball());
        for (int i = 0; i < 5; i++) _deck.Add(new IceShield());
        for (int i = 0; i < 3; i++) _deck.Add(new Heal());
        for (int i = 0; i < 4; i++) _deck.Add(new Slash());
        for (int i = 0; i < 3; i++) _deck.Add(new PowerUp());

        // Shuffle decks
        ShuffleDeck(_deck);
    }

    private void ShuffleDeck(List<Card> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }
    }
    
    public void DrawCards()
    {
        while (_hand.Count < 3 && _deck.Count > 0)
        {
            _hand.Add(_deck[0]);
            _deck.RemoveAt(0);
        }
    }
}

