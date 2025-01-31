namespace CardGameRefactoring;

public class Player(string name)
{
	public string Name { get; protected set; } = name;
	public int MaxHealth { get; protected set; } = 100;
	public int MaxMana { get; protected set; } = 100;
	public int MaxShield { get; protected set; } = 100;

	public Deck Deck = new();
	
	private int _health = 100;
	public int Health {
		get => _health;
		set => _health = Helpers.Clamp(value, MaxHealth);
	}
	public bool IsDead => _health <= 0;

	private int _mana = 100;
	public int Mana {
		get => _mana;
		set => _mana = Helpers.Clamp(value, MaxMana);
	}

	private int _shield = 100;
	public int Shield {
		get => _shield;
		set => _shield = Helpers.Clamp(value, MaxShield);
	}
	
	public override string ToString() {
		return $"\n{Name} Health: {Health} | Mana: {Mana} | Shield: {Shield}";
	}

	public bool HasFireBuff { get; set; } = false;
	public bool HasIceShield { get; set; } = false;

    

    public int TakeDamage(int damage) {
        
        // adjusts damage amounts for current buffs
        var realDamage = damage;
        if (HasFireBuff) realDamage *= 2;
        if (HasIceShield) realDamage /= 2;

        // applies damage to shield first, then health
        var shieldDamage = Helpers.Clamp(realDamage, Shield);
        Shield -= shieldDamage;
        Health -= Helpers.Clamp(realDamage - shieldDamage, Health);
        
        return realDamage;
    }






    //
    //
    // else if (card == "IceShieldCard")
    //     {
    //         if (isPlayer)
    //         {
    //             if (player.Mana >= 20)
    //             {
    //                 player.Shield += 30;
    //                 player.HasIceShield = true;
    //                 player.Mana -= 20;
    //                 Console.WriteLine("Player gains Ice Shield!");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Not enough mana!");
    //                 return;
    //             }
    //         }
    //         else
    //         {
    //             if (enemy.Mana >= 20)
    //             {
    //                 enemy.Shield += 30;
    //                 enemy.HasIceShield = true;
    //                 enemy.Mana -= 20;
    //                 Console.WriteLine("Enemy gains Ice Shield!");
    //             }
    //             else return;
    //         }
    //     }
    //     else if (card == "HealCard")
    //     {
    //         if (isPlayer)
    //         {
    //             if (player.Mana >= 40)
    //             {
    //                 player.Health += 40;
    //                 player.Mana -= 40;
    //                 Console.WriteLine("Player heals 40 health!");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Not enough mana!");
    //                 return;
    //             }
    //         }
    //         else
    //         {
    //             if (enemy.Mana >= 40)
    //             {
    //                 enemy.Health += 40;
    //                 enemy.Mana -= 40;
    //                 Console.WriteLine("Enemy heals 40 health!");
    //             }
    //             else return;
    //         }
    //     }
    //     else if (card == "SlashCard")
    //     {
    //         if (isPlayer)
    //         {
    //             if (player.Mana >= 20)
    //             {
    //                 int damage = 20;
    //                 if (player.HasFireBuff) damage *= 2;
    //
    //                 if (enemy.Shield > 0)
    //                 {
    //                     if (enemy.Shield >= damage)
    //                     {
    //                         enemy.Shield -= damage;
    //                         damage = 0;
    //                     }
    //                     else
    //                     {
    //                         damage -= enemy.Shield;
    //                         enemy.Shield = 0;
    //                     }
    //                 }
    //
    //                 enemy.Health -= damage;
    //                 player.Mana -= 20;
    //                 Console.WriteLine($"Player slashes for {damage} damage!");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Not enough mana!");
    //                 return;
    //             }
    //         }
    //         else
    //         {
    //             if (enemy.Mana >= 20)
    //             {
    //                 int damage = 20;
    //                 if (enemy.HasFireBuff) damage *= 2;
    //
    //                 if (player.Shield > 0)
    //                 {
    //                     if (player.Shield >= damage)
    //                     {
    //                         player.Shield -= damage;
    //                         damage = 0;
    //                     }
    //                     else
    //                     {
    //                         damage -= player.Shield;
    //                         player.Shield = 0;
    //                     }
    //                 }
    //
    //                 player.Health -= damage;
    //                 enemy.Mana -= 20;
    //                 Console.WriteLine($"Enemy slashes for {damage} damage!");
    //             }
    //             else return;
    //         }
    //     }
    //     else if (card == "PowerUpCard")
    //     {
    //         if (isPlayer)
    //         {
    //             if (player.Mana >= 30)
    //             {
    //                 player.HasFireBuff = true;
    //                 player.Mana -= 30;
    //                 Console.WriteLine("Player gains Fire Buff!");
    //             }
    //             else
    //             {
    //                 Console.WriteLine("Not enough mana!");
    //                 return;
    //             }
    //         }
    //         else
    //         {
    //             if (enemy.Mana >= 30)
    //             {
    //                 enemy.HasFireBuff = true;
    //                 enemy.Mana -= 30;
    //                 Console.WriteLine("Enemy gains Fire Buff!");
    //             }
    //             else return;
    //         }
    //     }
    // }
}
