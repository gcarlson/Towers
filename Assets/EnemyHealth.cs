using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public TextMesh text;

    [Header("Health")]
    [SerializeField] private Slider hpSlider;
    public int hp = 100;
    public int maxHp = 100;
    public int value = 5;
    public Canvas canvas;
    public GameObject damageNumber;

    public int Damage(int damage)
    {
        var o = Instantiate(damageNumber, transform.position, Quaternion.identity);
        var t = Random.Range(0.0f, 360.0f);
        o.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(t), 0.0f, Mathf.Sin(t));
        o.GetComponentInChildren<TextMeshProUGUI>().text = (damage + "");
        Destroy(o, 1.0f);
        if (hp < 0)
        {
            print("ddd YOU ARE ALREADY DEAD");
            return 0;
        }
        hp-= damage;
        SetHealth();
        //text.text = hp + "";
        if (hp <= 0)
        {
            GameManager.AddMoney(value);
            Destroy(gameObject);
            return 0 - damage;
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
