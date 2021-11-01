using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainGenerator : MonoBehaviour
{
  public GameObject prefab;
  public GameObject player;
  public float min_distance_x;
  public float max_distance_x;

  public float min_distance_y;
  public float max_distance_y;

  public Vector2 spawn_position;
  CameraController main_camera_controller;

  public List<GameObject> pool;
  public int max_pool_size = 5;
  int pool_offset = 0; // this is the oldest one we know

  int coin_amount = 3;
  int max_coin_amount = 15;
  public int score;
  PlayerData player_data;
  public void Start()
  {
    main_camera_controller = GetComponent<CameraController>();
    player_data = GetComponent<PlayerData>();
  }

  void Update()
  {
    if (player.transform.position.x > spawn_position.x)
    {
      SpawnObject();
    }
    HandleScore();
  }

  void SpawnObject()
  {
    float random_x = Random.Range(min_distance_x, max_distance_x);
    float random_y = Random.Range(min_distance_y, max_distance_y);

    GameObject gb_to_spawn = null;
    if (pool.Count < max_pool_size)
    {
      gb_to_spawn = Instantiate(prefab, new Vector3(random_x + player.transform.position.x, random_y + player.transform.position.y, 0), Quaternion.identity);
      pool.Add(gb_to_spawn);
    }
    else
    {
      gb_to_spawn = pool[pool_offset];
      gb_to_spawn.transform.position = new Vector3(random_x + player.transform.position.x, random_y + player.transform.position.y, 0);
      pool_offset = (pool_offset + 1) % max_pool_size;
      score += 1;
    }
    spawn_position = gb_to_spawn.transform.position - (gb_to_spawn.transform.localScale / 2);
    main_camera_controller.setTarget(gb_to_spawn);
  }
  /*

    Some knows the first two objects are put in by Dev. 

    after that rest are put in by 'user' and we know those are the newest.

    so if we use this we can track which is oldest if we make the first two ordered by oldest to newest.

  */
  void HandleScore()
  {
    if (score % 10 == 0)
    {
      player_data.AddCoins(coin_amount);
    }
    if (score % 25 == 0)
    {
      if (coin_amount < max_coin_amount)
        coin_amount += 1;
    }
  }
}