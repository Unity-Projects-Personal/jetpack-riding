using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject player;
  public GameObject target;
  public GameObject previous_target;

  public Vector3 offset;
  public float min_zoom = 40f;
  public float max_zoom = 10f;
  public float zoom_limiter = 50f;
  private Vector3 velocity;
  public float smooth_time = 0.5f;
  private Camera main_camera;

  void Start()
  {
    main_camera = GetComponent<Camera>();
  }

  void LateUpdate()
  {
    if (target == null)
      return;
    Move();
    Zoom();
  }

  void Zoom()
  {
    float zoom = Mathf.Lerp(max_zoom, min_zoom, GetGreatestDistance());
    main_camera.fieldOfView = Mathf.Lerp(main_camera.fieldOfView, zoom, Time.deltaTime);
  }

  void Move()
  {
    Vector3 center_point = GetCenterPoint();

    Vector3 new_position = center_point + offset;

    transform.position = Vector3.SmoothDamp(transform.position, new_position, ref velocity, smooth_time);
  }

  float GetGreatestDistance()
  {
    var bounds = new Bounds(player.transform.position, Vector3.zero);

    bounds.Encapsulate(player.transform.position);
    bounds.Encapsulate(target.transform.position);

    if (previous_target != null)
      bounds.Encapsulate(previous_target.transform.position);

    return bounds.size.x;
  }

  Vector3 GetCenterPoint()
  {
    var bounds = new Bounds(player.transform.position, Vector3.zero);

    bounds.Encapsulate(player.transform.position);
    bounds.Encapsulate(target.transform.position);

    if (previous_target != null)
      bounds.Encapsulate(previous_target.transform.position);

    return bounds.center;
  }

  public void setTarget(GameObject target)
  {
    previous_target = this.target;
    this.target = target;
  }
}
