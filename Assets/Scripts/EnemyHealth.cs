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
    public float[] multipliers = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };

    public virtual void OnKill()
    {
        print("ddd base onkill");
    }

    public virtual int Damage(int damage, TurretController.Element type)
    {
        //print("ddd damaging");
        if (hp <= 0)
        {
            print("ddd YOU ARE ALREADY DEAD");
            return 0;
        }
        int adjustedDamage = Mathf.CeilToInt(damage * multipliers[(int) type]);
        var o = Instantiate(damageNumber, transform.position, Quaternion.identity);
        var t = Random.Range(0.0f, 360.0f);
        o.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(t), 0.0f, Mathf.Sin(t));
        o.GetComponentInChildren<TextMeshProUGUI>().text = (adjustedDamage + "");
        if (adjustedDamage > damage)
        {
            o.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.1f, 0.15f, 0.5f, 1.0f);
        }
        else if (adjustedDamage < damage)
        {
            o.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.5f, 0.35f, 0.15f, 1.0f);
        }
        Destroy(o, 1.0f);
        hp-= adjustedDamage;
        SetHealth();
        //text.text = hp + "";
        if (hp <= 0)
        {
            OnKill();
            GameManager.AddMoney(value);
            Destroy(gameObject);
            return 0 - adjustedDamage;
        }
        return adjustedDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (canvas)
        {
            SetHealthMax();
            SetHealth();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canvas)
        {
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
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
