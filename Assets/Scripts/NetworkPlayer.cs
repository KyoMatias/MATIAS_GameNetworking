using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [Networked] public Vector3 NetworkedPosition { get; private set; }
    [Networked] public Color PlayerColor { get; private set; }

    #region FusionCallbacks
    public override void Spawned()
    {
        if (!HasInputAuthority) //Client
        {
            
        }

        if (HasInputAuthority) // Server
        {
            PlayerColor = Random.ColorHSV();
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;
        if (GetInput(out NetworkInputData input)) return;

        this.transform.position += new Vector3(input.InputVector.normalized.x, 0, input.InputVector.normalized.y) *
                                   Runner.DeltaTime;

        NetworkedPosition = this.transform.position;

    }

    public override void Render()
    {
        this.transform.position = NetworkedPosition;
        if (_meshRenderer != null && _meshRenderer.material.color != PlayerColor)
        {
            
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetPlayerColor(Color color)
    {
        if(HasStateAuthority)
        {
            this.PlayerColor = color;
        }
    }
    #endregion
    
    
    #region Unity Callbacks

    private void Update()
    {
        if(!HasInputAuthority) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var randColor = Random.ColorHSV();
            RPC_SetPlayerColor(randColor);
        }
    }
    
    #endregion
}
