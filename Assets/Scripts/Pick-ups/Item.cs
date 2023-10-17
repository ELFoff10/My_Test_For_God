using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _index;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.transform.CompareTag("Player")) 
        {
            obj.GetComponent<PlayerItems>().AddItem(_index);
            obj.GetComponent<PlayerItems>().Equip(_index);
            Destroy(gameObject);
        }
    }
}
