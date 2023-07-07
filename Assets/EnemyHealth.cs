using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public TextMesh text;

    [Header("Health")]
    [SerializeField] private Slider hpSlider;
    public int hp = 100;
    public int maxHp = 100;
    public Canvas canvas;

    public int Damage(int damage)
    {
        hp-= damage;
        SetHealth();
        //text.text = hp + "";
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHealthMax();
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void SetHealthMax()
    {
        hpSlider.maxValue = maxHp;
    }

    public void SetHealth()
    {
        hpSlider.value = hp;
    }
}
