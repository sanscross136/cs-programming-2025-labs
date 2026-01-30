public class Dungeon{
    public Room GenerateNextRoom(){
        CurrentRoomId++;
        _totalRoomsExplored++;
        if (_totalRoomsExplored % RoomsPerFloor == 0) {
            CurrentFloor++;
        }
        return new Room(CurrentRoomId, CurrentFloor);
    }

    public void HandleRoomEvent(Room room, Player player, 
                                RoomType roomType){
        switch (roomType){
            case RoomType.Battle:
                HandleBattleRoom(player);
                break;
            case RoomType.Rest:
                HandleRestRoom(player);
                break;
            case RoomType.Treasure:
                HandleTreasureRoom(player);
                break;
        }
    }
}

public enum RoomType { Battle, Rest, Treasure }
public class Room{
    private RoomType GenerateRandomType(){
    
        return (RoomType)Random.Shared.Next(0, 3);
    }
}
