using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rb;
    public float recoil;
    public int maxAmmo;
    public int crntAmmo;
    public int reloadTime;
    public Transform bulltetSpawnPoint;
    public GameObject bullet;
    public float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        crntAmmo = maxAmmo;
    }

    // Update is called once per frames
    void Update()
    {
        
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosVector =new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180/Mathf.PI)*angleRad;




        if (mousePosVector.x < Screen.width / 2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.rotation = Quaternion.Euler(0f, 0f, (angleDeg +180));
            if (Input.GetMouseButtonDown(0) && crntAmmo > 0)
            {
                rb.AddForce((transform.right * recoil), ForceMode2D.Impulse);
                GameObject b1 = Instantiate(bullet, bulltetSpawnPoint.position,transform.rotation);
                b1.GetComponent<Rigidbody2D>().AddForce(-1 * (transform.right * bulletSpeed), ForceMode2D.Impulse);
                GameObject b2 = Instantiate(bullet, bulltetSpawnPoint.position, transform.rotation);
                b2.GetComponent<Rigidbody2D>().AddForce(-1 * (transform.right * bulletSpeed), ForceMode2D.Impulse);
                GameObject b3 = Instantiate(bullet, bulltetSpawnPoint.position, transform.rotation);
                b3.GetComponent<Rigidbody2D>().AddForce(-1 * (transform.right * bulletSpeed), ForceMode2D.Impulse);
                crntAmmo -= 1;
            }
        }

        if (mousePosVector.x > Screen.width / 2)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
            if (Input.GetMouseButtonDown(0) && crntAmmo > 0)
            {
                rb.AddForce(-1 * (transform.right * recoil), ForceMode2D.Impulse);

                GameObject b1 = Instantiate(bullet, bulltetSpawnPoint.position, transform.rotation);
                b1.GetComponent<Rigidbody2D>().AddForce((transform.right * bulletSpeed), ForceMode2D.Impulse);
                GameObject b2 = Instantiate(bullet, bulltetSpawnPoint.position, transform.rotation);
                b2.GetComponent<Rigidbody2D>().AddForce((transform.right * bulletSpeed), ForceMode2D.Impulse);
                GameObject b3 = Instantiate(bullet, bulltetSpawnPoint.position, transform.rotation);
                b3.GetComponent<Rigidbody2D>().AddForce((transform.right * bulletSpeed), ForceMode2D.Impulse);
                crntAmmo -= 1;
            }
        }



        if (Input.GetKeyDown(KeyCode.R) && crntAmmo!=maxAmmo) 
        {
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {    
        yield return new WaitForSeconds(reloadTime);
        crntAmmo = maxAmmo;
        yield return null;
    }
}
