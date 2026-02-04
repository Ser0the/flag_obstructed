using Sandbox;
using System.Linq;
using System.Threading.Channels;

public class PlayerManager : Component, Component.INetworkListener
{

	void INetworkListener.OnConnected( Connection channel )
	{
		GameObject PlayerObject = new GameObject(this.GameObject);
		PlayerComponent PlayerComp = PlayerObject.AddComponent<PlayerComponent>();

		PlayerComp.ConnectionID = channel.Id;

		PlayerObject.NetworkSpawn();
	}

	protected override void OnEnabled()
	{
		if (IsProxy) { return; }
		List<Connection> fullConnections = [];

		foreach ( PlayerComponent component in Scene.GetAllComponents<PlayerComponent>() )
		{
			fullConnections.Add(Connection.Find(component.ConnectionID));
		}

		foreach (Connection connection in Connection.All )
		{
			if (!fullConnections.Contains(connection) )
			{
				GameObject PlayerObject = new GameObject( this.GameObject );
				PlayerComponent PlayerComp = PlayerObject.AddComponent<PlayerComponent>();

				PlayerComp.ConnectionID = connection.Id;

				PlayerObject.NetworkSpawn();
			}
		}
	}
}
