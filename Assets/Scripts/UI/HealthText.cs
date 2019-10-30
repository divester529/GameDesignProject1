using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
  public Text m_Text;
  public GameObject player;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    m_Text.text="Health "+player.GetComponent<Player>().getHealth()+"/"+player.GetComponent<Player>().getMaxHealth();
  }
}
