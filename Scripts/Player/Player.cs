using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public CinemachineController CameraController;

    public GameObject sprintParticle;
    public GameObject shieldParticle;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        CameraController = CameraController.GetComponent<CinemachineController>();
    }
}
