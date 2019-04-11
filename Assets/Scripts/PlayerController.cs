using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed, touchSpeed;
    public float length;
    public Slider slider;
    public Text infoText;
    public Button restartButton;
    private Vector3 touchPosition;
    private float progress = 0;
    private bool alive = true, finish = false;
    float deltaX, deltaZ;
    void Start()
    {
        slider.gameObject.SetActive(true);
        infoText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        slider.value = progress;
    }

    void FixedUpdate()
    {
        if (alive)
        {
            if (finish)
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                if (this.transform.position.y < 0)
                {
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    alive = false;
                    infoText.text = "GAME OVER";
                    infoText.gameObject.SetActive(true);
                    restartButton.gameObject.SetActive(true);
                }
                else
                {
                    if (Input.touchCount > 0)
                    {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        transform.Translate(touchDeltaPosition.x * touchSpeed * Time.deltaTime, 0, speed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(0, 0, speed * Time.deltaTime);
                    }
                    float pos = this.transform.position.z;
                    if (pos < length)
                    {
                        progress = pos / length;
                    }
                    else if (pos >= length)
                    {
                        progress = 1;
                        finish = true;
                        infoText.text = "CONGRATULATIONS";
                        infoText.gameObject.SetActive(true);
                        restartButton.gameObject.SetActive(true);
                    }
                    slider.value = progress;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bomb")
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            alive = false;
            infoText.text = "GAME OVER";
            infoText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
