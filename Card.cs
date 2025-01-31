namespace CardGameRefactoring;

public abstract class Card
{
	public string Name { get; protected set; } 
	public string ExtraText { get; protected set; }

	private int _mana;
	public int Mana {
		get => _mana; 
		protected set => _mana = value > 0 ? value : 0;
	}
	private int _damage;
	public int Damage {
		get => _damage; 
		protected set => _damage = value > 0 ? value : 0;
	}
	
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Deal {Damage} damage";
	}
	
	protected Card (int mana = 30, int damage = 0) {
		Name = GetType().Name;
		Mana = mana;
		Damage = damage;
	}
}

public class Fireball : Card
{
	
}
public class IceShield : Card
{
	
}
public class Heal : Card
{
	
}
public class Slash : Card
{
	
}
public class PowerUp : Card
{
	
}
