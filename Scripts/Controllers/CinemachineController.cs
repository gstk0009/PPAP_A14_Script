using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera;
    [SerializeField] private GameObject virtualCameraDie;

    public void OnCameraDie()
    {
        virtualCamera.SetActive(false);
        virtualCameraDie.SetActive(true);
    }

    public void OnCamera()
    {
        virtualCamera.SetActive(true);
        virtualCameraDie.SetActive(false);
    }
}
