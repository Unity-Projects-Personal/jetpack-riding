using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float jet_pack_force;
  float jet_pack_max_length;
  float jet_pack_length;

  Rigidbody2D rb2d;
  Vector2 input_position;
  bool holding;
  float screen_width;
  float fall_multiplier = 2.5f;
  float low_fall_multiplier = 2;
  void Start()
  {
    rb2d = GetComponent<Rigidbody2D>();
    screen_width = Screen.width;
    input_position = Vector2.zero;
    jet_pack_force = PlayerPrefs.HasKey("jet_pack_force") ? PlayerPrefs.GetFloat("jet_pack_force") : Constants.jet_pack_force;
    jet_pack_max_length = jet_pack_length = PlayerPrefs.HasKey("jet_pack_length") ? PlayerPrefs.GetFloat("jet_pack_length") : Constants.jet_pack_length;
    Physics2D.gravity = PlayerPrefs.HasKey("gravity_scale") ? new Vector2(0, PlayerPrefs.GetFloat("gravity_scale")) : new Vector2(0, Constants.gravity_scale);
  }

  void Update()
  {
    HandleMobileInput();
    HandlePCInput();
    BetterFall();
  }

  void HandleMobileInput()
  {
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);

      Vector2 touch_pos = touch.position;

      OnInputChange(touch_pos);
    }
  }
  void HandlePCInput()
  {
    if (Input.GetButton("Fire1"))
    {
      Vector2 mouse_pos = Input.mousePosition;

      OnInputChange(mouse_pos);
    }
  }

  void OnInputChange(Vector2 input_pos)
  {
    // Right side of the screen
    if (input_pos.x - (screen_width / 2) > 0)
    {
      rb2d.AddForce(new Vector2(0.3f, 0.5f) * jet_pack_force, ForceMode2D.Impulse);
    }
    else
    {
      rb2d.AddForce(new Vector2(-0.3f, 0.5f) * jet_pack_force, ForceMode2D.Impulse);
    }
  }

  void BetterFall()
  {
    if (rb2d.velocity.y < 0)
    {
      rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fall_multiplier - 1) * Time.deltaTime;
    }

    if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
    {
      rb2d.velocity += Vector2.up * Physics2D.gravity.y * (low_fall_multiplier - 1) * Time.deltaTime;
    }
  }
}
