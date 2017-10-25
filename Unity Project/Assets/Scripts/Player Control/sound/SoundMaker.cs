using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour {

    public GameObject sound;

	public void Sound(int strenght)
    {
        GameObject curSound = Instantiate(sound, transform.position, Quaternion.identity);
        curSound.transform.localScale = new Vector3(strenght, strenght, strenght);
    }
}
