using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum soundType
    {
        walk,
        dash,
        coin, 
        key,
        endKey,
        lockDoor,
        unlockDoor,
        enemy,
        buttonPress,
        canvasPopUp,
        shoot,
        bombShoot,
        explode,
        item,
        playerDamage,
        death,
        buttonHover

    }

    public GameObject soundObject;

    public AudioClip[] walkSound;
    public AudioClip[] dashSound;
    public AudioClip[] coinSound;
    public AudioClip[] keySound;
    public AudioClip[] endKeySound;
    public AudioClip[] lockDoorSound;
    public AudioClip[] unlockDoorSound;
    public AudioClip[] enemySound;
    public AudioClip[] buttonPressSound;
    public AudioClip[] canvasPopUpSound;
    public AudioClip[] shootSound;
    public AudioClip[] bombShootSound;
    public AudioClip[] explodeSound;
    public AudioClip[] itemSound;
    public AudioClip[] playerDamageSound;
    public AudioClip[] deadSound;
    public AudioClip[] hoverSound;


    public void PlaySound(soundType type, Vector3 pos, float vol)
    {
        GameObject newSound = GameObject.Instantiate(soundObject, pos, Quaternion.identity);
        SoundObject soundObj = newSound.GetComponent<SoundObject>();

        switch (type)
        {
            case (soundType.walk):
                soundObj.clip = walkSound[Random.Range(0, walkSound.Length)];
                break;
            case (soundType.dash):
                soundObj.clip = dashSound[Random.Range(0, dashSound.Length)];
                break;
            case (soundType.coin):
                soundObj.clip = coinSound[Random.Range(0, coinSound.Length)];
                break;
            case (soundType.key):
                soundObj.clip = keySound[Random.Range(0, keySound.Length)];
                break;
            case (soundType.endKey):
                soundObj.clip = endKeySound[Random.Range(0, endKeySound.Length)];
                break;
            case (soundType.lockDoor):
                soundObj.clip = lockDoorSound[Random.Range(0, lockDoorSound.Length)];
                break;
            case (soundType.unlockDoor):
                soundObj.clip = unlockDoorSound[Random.Range(0, unlockDoorSound.Length)];
                break;
            case (soundType.enemy):
                soundObj.clip = enemySound[Random.Range(0, enemySound.Length)];
                break;
            case (soundType.buttonPress):
                soundObj.clip = buttonPressSound[Random.Range(0, buttonPressSound.Length)];
                break;
            case (soundType.canvasPopUp):
                soundObj.clip = canvasPopUpSound[Random.Range(0, canvasPopUpSound.Length)];
                break;
            case (soundType.shoot):
                soundObj.clip = shootSound[Random.Range(0, shootSound.Length)];
                break;
            case (soundType.bombShoot):
                soundObj.clip = bombShootSound[Random.Range(0, bombShootSound.Length)];
                break;
            case (soundType.explode):
                soundObj.clip = explodeSound[Random.Range(0, explodeSound.Length)];
                break;
            case (soundType.item):
                soundObj.clip = itemSound[Random.Range(0, itemSound.Length)];
                break;
            case (soundType.playerDamage):
                soundObj.clip = playerDamageSound[Random.Range(0, playerDamageSound.Length)];
                break;
            case (soundType.death):
                soundObj.clip = deadSound[Random.Range(0, deadSound.Length)];
                break;
            case (soundType.buttonHover):
                soundObj.clip = hoverSound[Random.Range(0, hoverSound.Length)];
                break;
        }

        soundObj.vol = vol;
        soundObj.StartAudio();
    }

}
