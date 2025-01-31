namespace CardGameRefactoring;

// abstract card class used to help share the basic properties and functionality across every card
public abstract class Card
{
	// auto-properties
	public string Name { get; protected set; }

	// validated properties
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
	
	// basic damage card playing, virtual to be overridden by other cards
	public virtual void PlayCard(Player user, Player target) {
		if (user.Mana >= Mana) 
		{
			if (user.HasFireBuff) {
				Console.WriteLine($"{target.Name} was dealt {target.TakeDamage(Damage * 2)} damage with {Name}!");
				user.HasFireBuff = false;
			} else {
				Console.WriteLine($"{target.Name} was dealt {target.TakeDamage(Damage)} damage with {Name}!");
			}
			user.Mana -= Mana;
		} else {
			Console.WriteLine("Not enough mana!");
		}
	}
	// toString override for displaying info about card
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Deal {Damage} damage.";
	}
	// constructor
	protected Card (string name, int mana = 30, int damage = 0) {
		Name = name;
		Mana = mana;
		Damage = damage;
	}
}

// cards inheriting the base class, for saving the rewriting of code and making card addition simpler

// Fireball - 30 mana, 40 damage
internal class Fireball : Card
{
	public Fireball(string name = "Fireball", int mana = 30, int damage = 40) : base(name, mana, damage) {}
}

// Slash - 20 mana, 20 damage
internal class Slash : Card
{
	public Slash(string name = "Slash", int mana = 20, int damage = 20) : base(name, mana, damage) {}
}

// Ice Shield - 20 mana, +30 shield, *0.5 damage received
internal class IceShield : Card
{
	public IceShield(string name = "Ice Shield", int mana = 20) : base(name, mana ) {}
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Gain 30 shield and ice protection";
	}

	public override void PlayCard(Player user, Player target) {
		if (user.Mana < Mana) base.PlayCard(user, target);
		else {
			user.Shield += 30;
			user.HasIceShield = true;
			user.Mana -= 20;
			Console.WriteLine($"{user.Name} gains Ice Shield!");
		}
	}
}

// Power Up - 30 mana, *2 damage dealt
internal class PowerUp : Card
{
	public PowerUp(string name = "Power Up", int mana = 30) : base(name, mana) {}
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Gain fire buff for next attack";
	}
	
	public override void PlayCard(Player user, Player target) {
		if (user.Mana < Mana) base.PlayCard(user, target);
		else {
			user.HasFireBuff = true;
			user.Mana -= 30;
			Console.WriteLine($"{user.Name} gains Fire Buff!");
		}
	}
}

// Heal - 40 mana, +40 health
internal class Heal : Card
{
	public Heal(string name = "Heal", int mana = 40) : base(name, mana) {}
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Restore 40 health";
	}
	
	public override void PlayCard(Player user, Player target) {
		if (user.Mana < Mana) base.PlayCard(user, target);
		else {
			user.Health += 40;
			user.Mana -= 40;
			Console.WriteLine($"{user.Name} heals 40 health!");
		}
	}
}

// Siphon - 10 mana to cast, -10 Health, +30 Mana
internal class Siphon : Card
{
	public Siphon(string name = "Siphon", int mana = 10) : base(name, mana) {}
	public override string ToString() {
		return $"{Name} (Costs {Mana} mana): Siphons 10 Health from yourself to create 30 Mana";
	}
	
	public override void PlayCard(Player user, Player target) {
		if (user.Mana < Mana) base.PlayCard(user, target);
		else {
			user.Health -= 10;
			user.Mana += 30;
			Console.WriteLine($"{user.Name} siphoned their health, and gained 30 Mana!");
		}
	}
}