namespace CardGameRefactoring;

using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // creates player and enemy instances
    static Player player = new("Player");
    static Player enemy = new("Enemy");
    
    static void Main(string[] args)
    {
        
        InitializeGame();

        while (!player.IsDead && !enemy.IsDead)
        {
            // Draw cards if needed
            player.Deck.DrawCards();
            enemy.Deck.DrawCards();

            // Player turn
            DisplayGameState();
            ChooseCard();

            if (enemy.IsDead) break;
            if (player.IsDead) break;

            // Enemy turn
            Console.WriteLine("\nEnemy's turn...");
            Thread.Sleep(1000);
            RandomCard();

            if (enemy.IsDead) break;
            if (player.IsDead) break;

            // End of round mana refill
            player.Mana += player.RoundMana;
            enemy.Mana += enemy.RoundMana;

            Console.WriteLine("\nPress any key for next round...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine(player.IsDead ? "You Lost!" : "You Won!");
        Console.ReadKey();
    }

    // set up initial gamestate, decks and hands
    static void InitializeGame() {
        
        Console.WriteLine("=== Card Battle Game ===");
        
        player.Deck.InitializeDeck();
        enemy.Deck.InitializeDeck();
    }

    // uses toString overrides to display player and card information
    static void DisplayGameState()
    {
        Console.Write(player);
        Console.WriteLine(enemy);

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < player.Deck.Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Deck.Hand[i]}");
        }
    }

    // displays hand and waits for input to select card, used for player
    private static void ChooseCard()
    {
        var hand = player.Deck.Hand;
        
        Console.Write("\nChoose a card to play (1-3) or 0 to skip: ");
        if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out var choice) || choice < 0 ||
            choice > hand.Count) {
            Console.WriteLine($" {choice} Invalid choice! Turn skipped.");
            return;
        }

        Console.WriteLine(choice.ToString());
        if (choice == 0) return;
        
        hand[choice - 1].PlayCard(player, enemy);
        hand.RemoveAt(choice - 1);
    }

    // automatically plays a random card from hand, used for enemy
    public static void RandomCard()
    {
        var hand = enemy.Deck.Hand;
        
        // Simple AI: randomly play a card if enough mana
        var cardIndex = Rng.Next(hand.Count);
        hand[cardIndex].PlayCard(enemy, player);
        hand.RemoveAt(cardIndex);
        
    }
}