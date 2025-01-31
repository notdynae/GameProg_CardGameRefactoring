namespace CardGameRefactoring;

using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
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

            // Enemy turn
            Console.WriteLine("\nEnemy's turn...");
            Thread.Sleep(1000);
            RandomCard();

            if (player.IsDead) break;

            // End of round effects
            // UpdateBuffs(true);
            // UpdateBuffs(false);

            Console.WriteLine("\nPress any key for next round...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine(player.IsDead ? "You Lost!" : "You Won!");
        Console.ReadKey();
    }

    static void InitializeGame() {
        
        Console.WriteLine("=== Card Battle Game ===");
        
        player.Deck.InitializeDeck();
        enemy.Deck.InitializeDeck();
    }

    static void DisplayGameState()
    {
        Console.WriteLine($"\nPlayer Health: {player.Health} | Mana: {player.Mana} | Shield: {player.Shield}");
        Console.WriteLine($"Enemy Health: {enemy.Health} | Mana: {enemy.Mana} | Shield: {enemy.Shield}");

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < player.Deck.Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Deck.Hand[i]}");
        }
    }

    // static string GetCardDescription(string cardName)
    // {
    //     // Long if-else chain for card descriptions
    //     if (cardName == "FireballCard")
    //         return "Fireball (Costs 30 mana): Deal 40 damage";
    //     else if (cardName == "IceShieldCard")
    //         return "Ice Shield (Costs 20 mana): Gain 30 shield and ice protection";
    //     else if (cardName == "HealCard")
    //         return "Heal (Costs 40 mana): Restore 40 health";
    //     else if (cardName == "SlashCard")
    //         return "Slash (Costs 20 mana): Deal 20 damage";
    //     else if (cardName == "PowerUpCard")
    //         return "Power Up (Costs 30 mana): Gain fire buff for 2 turns";
    //     return "Unknown Card";
    // }

    // static void PlayTurn()
    // {
    //     var hand = isPlayer ? player.Deck.Hand : enemy.Deck.Hand;
    //
    //     if (isPlayer)
    //     {
    //         
    //     }
    //     else
    //     {
    //         
    //     }
    // }
    public static void ChooseCard()
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

    public static void RandomCard()
    {
        var hand = enemy.Deck.Hand;
        
        // Simple AI: randomly play a card if enough mana
        var cardIndex = Rng.Next(hand.Count);
        hand[cardIndex].PlayCard(enemy, player);
        hand.RemoveAt(cardIndex);
        
    }


    static void PlayCard(string cardName, bool isPlayer)
    {
        // Huge if-else chain for card effects
        if (cardName == "FireballCard")
        {
            if (isPlayer)
            {
                if (player.Mana >= 30)
                {
                    int damage = 40;
                    if (player.HasFireBuff) damage *= 2;
                    if (enemy.HasIceShield) damage /= 2;

                    if (enemy.Shield > 0)
                    {
                        if (enemy.Shield >= damage)
                        {
                            enemy.Shield -= damage;
                            damage = 0;
                        }
                        else
                        {
                            damage -= enemy.Shield;
                            enemy.Shield = 0;
                        }
                    }

                    enemy.Health -= damage;
                    player.Mana -= 30;
                    Console.WriteLine($"Player casts Fireball for {damage} damage!");
                }
                else
                {
                    Console.WriteLine("Not enough mana!");
                    return;
                }
            }
            else
            {
                if (enemy.Mana >= 30)
                {
                    int damage = 40;
                    if (enemy.HasFireBuff) damage *= 2;
                    if (player.HasIceShield) damage /= 2;

                    if (player.Shield > 0)
                    {
                        if (player.Shield >= damage)
                        {
                            player.Shield -= damage;
                            damage = 0;
                        }
                        else
                        {
                            damage -= player.Shield;
                            player.Shield = 0;
                        }
                    }

                    player.Health -= damage;
                    enemy.Mana -= 30;
                    Console.WriteLine($"Enemy casts Fireball for {damage} damage!");
                }
                else return;
            }
        }
        else if (cardName == "IceShieldCard")
        {
            if (isPlayer)
            {
                if (player.Mana >= 20)
                {
                    player.Shield += 30;
                    player.HasIceShield = true;
                    player.Mana -= 20;
                    Console.WriteLine("Player gains Ice Shield!");
                }
                else
                {
                    Console.WriteLine("Not enough mana!");
                    return;
                }
            }
            else
            {
                if (enemy.Mana >= 20)
                {
                    enemy.Shield += 30;
                    enemy.HasIceShield = true;
                    enemy.Mana -= 20;
                    Console.WriteLine("Enemy gains Ice Shield!");
                }
                else return;
            }
        }
        else if (cardName == "HealCard")
        {
            if (isPlayer)
            {
                if (player.Mana >= 40)
                {
                    player.Health += 40;
                    player.Mana -= 40;
                    Console.WriteLine("Player heals 40 health!");
                }
                else
                {
                    Console.WriteLine("Not enough mana!");
                    return;
                }
            }
            else
            {
                if (enemy.Mana >= 40)
                {
                    enemy.Health += 40;
                    enemy.Mana -= 40;
                    Console.WriteLine("Enemy heals 40 health!");
                }
                else return;
            }
        }
        else if (cardName == "SlashCard")
        {
            if (isPlayer)
            {
                if (player.Mana >= 20)
                {
                    int damage = 20;
                    if (player.HasFireBuff) damage *= 2;

                    if (enemy.Shield > 0)
                    {
                        if (enemy.Shield >= damage)
                        {
                            enemy.Shield -= damage;
                            damage = 0;
                        }
                        else
                        {
                            damage -= enemy.Shield;
                            enemy.Shield = 0;
                        }
                    }

                    enemy.Health -= damage;
                    player.Mana -= 20;
                    Console.WriteLine($"Player slashes for {damage} damage!");
                }
                else
                {
                    Console.WriteLine("Not enough mana!");
                    return;
                }
            }
            else
            {
                if (enemy.Mana >= 20)
                {
                    int damage = 20;
                    if (enemy.HasFireBuff) damage *= 2;

                    if (player.Shield > 0)
                    {
                        if (player.Shield >= damage)
                        {
                            player.Shield -= damage;
                            damage = 0;
                        }
                        else
                        {
                            damage -= player.Shield;
                            player.Shield = 0;
                        }
                    }

                    player.Health -= damage;
                    enemy.Mana -= 20;
                    Console.WriteLine($"Enemy slashes for {damage} damage!");
                }
                else return;
            }
        }
        else if (cardName == "PowerUpCard")
        {
            if (isPlayer)
            {
                if (player.Mana >= 30)
                {
                    player.HasFireBuff = true;
                    player.Mana -= 30;
                    Console.WriteLine("Player gains Fire Buff!");
                }
                else
                {
                    Console.WriteLine("Not enough mana!");
                    return;
                }
            }
            else
            {
                if (enemy.Mana >= 30)
                {
                    enemy.HasFireBuff = true;
                    enemy.Mana -= 30;
                    Console.WriteLine("Enemy gains Fire Buff!");
                }
                else return;
            }
        }
    }

    static void UpdateBuffs(bool isPlayer)
    {
        if (isPlayer)
        {
            if (player.HasFireBuff) player.HasFireBuff = false;
            if (player.HasIceShield) player.HasIceShield = false;
            player.Mana += 20;
        }
        else
        {
            if (enemy.HasFireBuff) enemy.HasFireBuff = false;
            if (enemy.HasIceShield) enemy.HasIceShield = false;
            enemy.Mana += 20;
        }
    }
}