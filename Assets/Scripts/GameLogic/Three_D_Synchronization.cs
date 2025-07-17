using UnityEngine;

public class Three_D_Synchronization : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
    }

    private void OnDestroy()
    {
        Destroy(target);
    }
}
