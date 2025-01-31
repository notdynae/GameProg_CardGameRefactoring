namespace CardGameRefactoring;

public class Player(string name)
{
	// create instance of deck belonging to the player
	public Deck Deck = new();
	
	// stat auto-properties
	public string Name { get; protected set; } = name;
	public int RoundMana { get; protected set; } = 20;
	public int MaxMana { get; protected set; } = 100;
	public int MaxHealth { get; protected set; } = 100;
	public int MaxShield { get; protected set; } = 100;
	public bool HasFireBuff { get; set; }
	public bool HasIceShield { get; set; } 

	// validated / computed properties
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
	
	// player stats string override
	public override string ToString() {
		return $"\n{Name} Health: {Health} | Mana: {Mana} | Shield: {Shield}";
	}

	// applies damage to player, accounting for buffs
    public int TakeDamage(int damage) {
        
        // adjusts damage amounts for current buffs
        var realDamage = damage;

        if (HasIceShield) {
	        realDamage /= 2;
	        HasIceShield = false;
        }

        // applies damage to shield first, then health
        var shieldDamage = Helpers.Clamp(realDamage, Shield);
        Shield -= shieldDamage;
        Health -= realDamage - shieldDamage;
        
        return realDamage;
    }

    // I changed the UpdateBuff function from the given code, as how it seemed to be implemented,
    // it seemed like the fire buff especailly didnt function correctly. Now the buffs have one use,
    // instead of going away immediately at the end of the turn whether theyre used or not.
    // with that change, it didnt make sense to keep the UpdateBuffs method, as those checks are now
    // handled elsewhere in the4 code.
    
    // public void UpdateBuffs()
    // {

    // }
}
