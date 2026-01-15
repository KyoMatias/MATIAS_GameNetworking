using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
   
    
    #region FusionCallbacks
    public override void Spawned()
    {
        
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData input))
        {
            this.transform.position += new Vector3(input.InputVector.normalized.x, input.InputVector.normalized.y)* Runner.DeltaTime;
        }
    }

    public override void Render()
    {
        
    }
    #endregion
}
