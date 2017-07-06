using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUIManager : MonoBehaviour {

    private Player player;

    public Slider strSlider;

    public Slider speSlider;

    public Slider intSlider;

    public Slider sanSlider;

    public Text strText;
    public Text speText;
    public Text intText;
    public Text sanText;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();

    }
	
	// Update is called once per frame
	void Update () {


        strSlider.value = player.getAbilityInfo()[0];

        speSlider.value = player.getAbilityInfo()[1];

        intSlider.value = player.getAbilityInfo()[2];

        sanSlider.value = player.getAbilityInfo()[3];

        strText.text = "力量" + player.getAbilityInfo()[0] + "/" + player.getMaxAbilityInfo()[0];
        speText.text = "速度" + player.getAbilityInfo()[1] + "/" + player.getMaxAbilityInfo()[1];
        intText.text = "智力" + player.getAbilityInfo()[2] + "/" + player.getMaxAbilityInfo()[2];
        sanText.text = "神志" + player.getAbilityInfo()[3] + "/" + player.getMaxAbilityInfo()[3];
    }
}
