namespace CardGameRefactoring;

public class Deck
{
	private List<Card> _deck = [];
	private List<Card> _hand = [];
	
	private void InitializeDecks()
    {
        
        // Add cards to player deck
        for (var i = 0; i < 5; i++) _deck.Add(new Fireball());
        for (var i = 0; i < 5; i++) _deck.Add(new IceShield());
        for (var i = 0; i < 3; i++) _deck.Add(new Heal());
        for (var i = 0; i < 4; i++) _deck.Add(new Slash());
        for (var i = 0; i < 3; i++) _deck.Add(new PowerUp());

        // Shuffle decks
        ShuffleDeck(_deck);
    }

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
    
    public void DrawCards()
    {
        while (_hand.Count < 3 && _deck.Count > 0)
        {
            _hand.Add(_deck[0]);
            _deck.RemoveAt(0);
        }
    }
}

