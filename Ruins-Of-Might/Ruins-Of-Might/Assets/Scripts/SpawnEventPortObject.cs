using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Ports/Spawn Event Port", fileName = "SpawnEventPort")]
public class SpawnEventPortObject : ScriptableObject {

    UnityAction<SpawnEventPortObject, GameObject> OnSpawnPlayer = delegate { };

    public void SpawnPlayer(GameObject player) {
        OnSpawnPlayer(this, player);

    }


}
