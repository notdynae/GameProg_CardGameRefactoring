namespace CardGameRefactoring;

public class Player
{
	public int MaxHealth { get; protected set; }
	public int MaxMana { get; protected set; }
	
	protected int _health;
	public int Health {
		get => _health;
		private set => _health = Math.Min(Math.Max(0, value), MaxHealth);
	}
	protected int _mana;
	public int Mana {
		get => _mana;
		set => _mana = Math.Min(Math.Max(0, value), MaxMana);
	}
	protected int _shield;
	public int Shield {
		get => _shield;
		set => _shield = Math.Max(0, value);
	}

	public bool HasFireBuff { get; protected set; } = false;
	public bool HasIceShield { get; protected set; } = false;
}