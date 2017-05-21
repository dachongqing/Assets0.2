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

    }
}
