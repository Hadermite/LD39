using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public new Camera camera;
    public Transform target;
    public Vector3 offset;

    private void Update() {
        if (target == null) return;

        camera.transform.position = target.position + offset;
        camera.transform.rotation = target.rotation;
    }
}
