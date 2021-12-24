using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using MoreMountains.NiceVibrations;

public class PlayerMovement : MonoBehaviour
{
    public delegate void OnMagnetEnabled(Transform player);

    public static OnMagnetEnabled magnetEnabled;
    public Vector3 FinalLookAtAdjust;
    public ScoreCounter scoreCounter;
    public LookAtPlayer LookAtPlayer;
    public ParticleSystem deathParticles;
    public Rigidbody rb;
    public float forwardForce;
    public float sidewayForce;
    public float deathDelay;
    public Material[] material;
    // Renderer rend;
    public bool alive = true;
    public GameObject joystick;
    public bool StoneBreaker;
    public bool Invulnerable;
    [SerializeField]
    private MeshRenderer mesh;
    private Vector3 startScale;
    private JoystickMove mover;

    private void OnEnable()
    {
        Invulnerable = true;

        StartCoroutine(ResetInvulnerable());
    }
    void Start()
    {
        startScale = transform.localScale;

    }
    private void Update()
    {
        if (!mover)
        {
            mover = GetComponentInParent<JoystickMove>();
        }
        if (!mesh.isVisible && mesh.transform.localScale.x != 0 || transform.position.y < 0)
        {
            DetectDeath();
        }
    }
    public IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(.5f);
        Invulnerable = false;
    }
    public void DetectDeath(bool deathParticle = false)
    {
        if (Invulnerable)
        {
            return;
        }
        if (mover.rb.Count == 1)
        {
            mover.rb[0].gameObject.SetActive(false);
            if (!GameManager.Instance.FirstDeath)
            {
                GameManager.Instance.RespawnController.EnableRespawnPopup(this);
                if (!GoogleMobileAdsManager.Instance.RewardedAd.IsLoaded())
                {
                    GameManager.Instance.RespawnController.RewardButton.SetActive(false);
                }
                return;
            }

            else
            {
                GameManager.Instance.RespawnController.EnableRespawnPopup(this);
                GameManager.Instance.RespawnController.RewardButton.SetActive(false);
                // GoogleMobileAdsManager.Instance.ShowInterstitial();
                return;
            }
        }

        Die();
    }

    public void Die(bool deathParticle = true)
    {
        HapticManager.Instance.PlayDeathHaptic();
        mover?.rb?.Remove(rb);
        if (deathParticle)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        //MMVibrationManager//.Haptic(HapticTypes.Failure);
        alive = false;
        enabled = false;
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.GetComponent<ScoreMultiplier>())
        {
            PointManager.Instance.GameWon = true;
            if (!collide.gameObject.GetComponent<ScoreMultiplier>().Activated)
            {
                collide.gameObject.GetComponent<ScoreMultiplier>().Activated = true;
                collide.gameObject.GetComponent<ScoreMultiplier>().Fireworks.SetActive(true);
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.GetComponentInChildren<Collider>().enabled = false;

                StartCoroutine(EndGamePosition(collide.transform));
                if (mover.rb.Count == 1)
                {
                    LookAtPlayer = GameObject.FindObjectOfType<LookAtPlayer>();
                    LookAtPlayer.EndGameTarget = transform;
                }

            }
            PointManager.Instance.ScoreMultiplierValue = collide.gameObject.GetComponent<ScoreMultiplier>().Multiply;
            HapticManager.Instance.PlayWinHaptic();

        }
        if (collide.gameObject.tag == "Enemy")
        {
            //PointManager.Instance.CollectedPoints -= 2;
            DetectDeath(true);
        }

        if (collide.gameObject.tag == "Death")
        {
            DetectDeath(true);
        }
    }

    private IEnumerator EndGamePosition(Transform endPos)
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(endPos.transform.position.x,
                endPos.transform.position.y + 4, endPos.transform.position.z), Time.deltaTime * 2);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Camera.main.transform.position + FinalLookAtAdjust), Time.time);
            // transform.LookAt(Camera.main.transform.position+ FinalLookAtAdjust);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        CancelInvoke();
        Invoke("DropDown", .1f);

    }
    IEnumerator Slide(Vector3 newPos)
    {
        while (true)
        {
            transform.localPosition += newPos;
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            StopCoroutine("Slide");
        }
        CancelInvoke();
        if (collision.gameObject.GetComponent<SliderForce>())
        {

            StopCoroutine("Slide");
            StartCoroutine("Slide", new Vector3((collision.transform.rotation.z / 90) * 15, 0, 0f));//25
        }

        if (collision.gameObject.tag == "Coin")
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Crusher")
        {
            //MMVibrationManager.Haptic(HapticTypes.Selection);
            StopCoroutine("CancleCrush");
            StoneBreaker = true;
            Destroy(collision.gameObject);
            StartCoroutine("CancleCrush");
        }

        if (collision.gameObject.tag == "Magnet")
        {
            //MMVibrationManager.Haptic(HapticTypes.Selection);
            magnetEnabled.Invoke(transform);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            //MMVibrationManager.Haptic(HapticTypes.Selection);
            StopCoroutine("CancelShield");
            Destroy(collision.gameObject);
            StartCoroutine("CancelShield");
        }
    }

    private IEnumerator CancleCrush()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("CrushTime"));
        StoneBreaker = false;
    }
    private IEnumerator CancelShield()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("ShieldTime"));
        Invulnerable = false;
    }
    void DropDown()
    {
        rb.AddForce(-Vector3.up * 10 * transform.localScale.x, ForceMode.Impulse);
    }
}