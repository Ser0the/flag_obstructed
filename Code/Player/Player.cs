using Sandbox;
using System;

public class Player
{
	public PlayerComponent ComponentInstance { get; set; }

	public Connection PlayerConnection
	{
		get => Connection.Find( ComponentInstance.ConnectionID );
		set => ComponentInstance.ConnectionID = value.Id;
	}

	public Character character { 
		get => ComponentInstance.character; 
		set => ComponentInstance.character = value;  
	}

	public static List<Player> Players { get; private set; }

	public Player( PlayerComponent component )
	{
		ComponentInstance = component;
		Players.Add( this );
	}

	public static Player FromConnection( Connection connection )
	{
		foreach (Player player in Players)
		{
			if (player.PlayerConnection == connection ) { return player; }
		} 
		return null;
	}
}

public class PlayerComponent : Component
{
	[Sync]
	[Property, ReadOnly]
	public Guid ConnectionID { get; set; } = Guid.Empty;
	public Player PlayerInstance { get; set; } = null;

	public Character character { get; set; }

	protected override void OnStart()
	{
		Player Player = new Player( this );
		this.PlayerInstance = Player;
	}
}
