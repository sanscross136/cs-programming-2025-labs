public class Battle{
    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }
    public bool IsPlayerTurn { get; private set; }
    public Battle(Player player, Enemy enemy){
        Player = player;
        Enemy = enemy;
        IsPlayerTurn = true;
    }
    public void ExecutePlayerAction(BattleAction action){
    }
    private void ExecuteEnemyAction(){
        int damage = Enemy.Damage;
        Player.TakeDamage(damage);
    }
}
