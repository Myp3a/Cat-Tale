using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Char : MonoBehaviour
{
    public int Score = 0;
    public float JumpPower;
    public float StartAnimationSpeed;
    public Vector3 StartPosition;
    [SerializeField] private InputAction _jump;
    [SerializeField] private InputAction _duck;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private List<Sprite> _animationFrames;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _collectSound;
    private float _timeBetweenAnimationFrames = 0.3f;
    private float _timePassed = 0;
    private int _animationFrameIndex = 0;
    private bool _atStartPosition = false;

    public void Start() {
        StartPosition = new Vector3(-2.5f, 3.25f ,0);
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Awake()
    {
        _jump.performed += ctx => { Jump(ctx); };
        _duck.started += ctx => { this.GetComponent<BoxCollider2D>().enabled = false; };
        _duck.canceled += ctx => { this.GetComponent<BoxCollider2D>().enabled = true; };
    }

    public void Jump(InputAction.CallbackContext ctx) {
        this.GetComponent<AudioSource>().PlayOneShot(_jumpSound);
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower * 250);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Death") {
            Die();
        }
        if (collision.gameObject.tag == "Pickup") {
            Score++;
            this.GetComponent<AudioSource>().PlayOneShot(_collectSound);
            _scoreText.text = Convert.ToString(Score);
            Destroy(collision.gameObject);
        }
    }

    public void OnEnable() {
        _jump.Enable();
        _duck.Enable();
    }

    public void OnDisable() {
        _jump.Disable();
        _duck.Disable();
    }

    private void Die() {
        OnDisable();
        this.GetComponent<AudioSource>().PlayOneShot(_deathSound);
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
        this.GetComponent<BoxCollider2D>().enabled = false;
        int maxScore = PlayerPrefs.GetInt("MaxScore",0);
        if (Score > maxScore) PlayerPrefs.SetInt("MaxScore", Score);
        PlayerPrefs.Save();
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (!_atStartPosition) {
            Vector3 moveVector = StartPosition - transform.position;
            if (moveVector.magnitude < 0.25f) {
                _atStartPosition = true;
                this.GetComponent<Rigidbody2D>().isKinematic = false;
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
            else {
                moveVector = moveVector.normalized * StartAnimationSpeed * Time.deltaTime;
                this.transform.position += moveVector;
            }
        }
        _timePassed += Time.deltaTime;
        if (_timePassed > _timeBetweenAnimationFrames) {
            _timePassed = 0;
            _animationFrameIndex++;
            _animationFrameIndex %= _animationFrames.Count;
            this.GetComponent<SpriteRenderer>().sprite = _animationFrames[_animationFrameIndex];
        }
    }
}
