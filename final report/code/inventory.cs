public class Inventory
{
    private List<Item> _items = new();
    public Weapon? EquippedWeapon { get; set; }
    public Armor? EquippedArmor { get; set; }

    public void AddItem(Item item){
        _items.Add(item);
    }
    public bool RemoveItem(Item item) {
        return _items.Remove(item);
    }
    public List<Item> GetItems() => _items;
    public void EquipWeapon(Weapon weapon) {
        EquippedWeapon = weapon;
    }
    public void EquipArmor(Armor armor){
        EquippedArmor = armor;
    }
    public void UnequipWeapon(){
        EquippedWeapon = null;
    }
    public void UnequipArmor() {
        EquippedArmor = null;
    }
    public int GetTotalHealingPotions() {
        return _items.OfType<HealingPotion>().Count();
    }
}
public abstract class Item{
    public virtual int Level { get; set; } = 1;
}
