using System;

public class Character : Component, IGameObjectNetworkEvents
{
	//TODO: make own movement controller
	[Property]
	public PlayerController MovementController { get; set; }

	public Player Player { get; set; } = null;

	void IGameObjectNetworkEvents.NetworkOwnerChanged( Connection newOwner, Connection previousOwner )
	{
		Player = Player.FromConnection( newOwner );
	}
}
