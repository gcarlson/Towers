using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutpostController : MonoBehaviour
{
    public TextMesh text;
    public bool built = false;

    [Header("Health")]
    [SerializeField] private Slider hpSlider;
    public int hp = 100;
    public int maxHp = 100;
    public int value = 5;
    public Canvas canvas;

    public int Damage(int damage)
    {
        hp -= damage;
        SetHealth();
        //text.text = hp + "";
        if (hp <= 0)
        {
            // GameManager.AddMoney(value);
            HexController.destroyOutpost(transform.position);
            built = false;
            canvas.enabled = false;
        }
        return damage;
    }

    public void EnableOutpost()
    {
        hp = maxHp;
        canvas.enabled = true;
        built = true;
        SetHealthMax();
        SetHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (built)
        {
            print("ddd outpost trigger entered");
            Damage(1);
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //canvas.transform.rotation = Camera.main.transform.rotation;
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
