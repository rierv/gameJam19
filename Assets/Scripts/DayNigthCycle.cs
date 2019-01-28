using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNigthCycle : MonoBehaviour
{
    public static DayNigthCycle Instance
    {
        get;
        private set;
    }
    public GameObject Photo1;
    public GameObject Photo2;
    public GameObject Photo3;
    public GameObject Diary;
    public GameObject Door;

    private Light sun;
    private Vector3 rotation;
    public float timer;
    public int days;
    int when = 1;
    [SerializeField][Range(0.1f, 50)] public float cycleSpeed = 49f;
    [SerializeField][Range(-1f, 1f)] private float lightOffset = .5f;
    bool diary = false;
    bool door = false;
    bool photo1 = false;
    bool photo2 = false;
    bool photo3 = false;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        sun = GetComponent<Light>();
//        sun.intensity = 0f;
        days = 1;
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    private void Update()
    {
        float delta = Time.deltaTime * cycleSpeed * 2.5f;
        bool hasFather = GameManager.Instance.HasFather();
        
        timer += delta;
        rotation.x +=  delta;
        transform.eulerAngles = rotation;
        //sun.intensity = timer / 360;
        if (Photo1.GetComponent<Photo>().image.enabled)
        {
            photo1 = true;
            when = days;
        }
        if (Door.GetComponent<OutsideDoor>().textEnabled == true)
        {
            door = true;
            when = days;
        }
        if (door && photo1 && days>when)
        {
            Photo1.SetActive(false);
            Photo2.SetActive(true);
        }
        if (Photo2.GetComponent<Photo>().image.enabled)
        {
            photo2 = true;
            when = days;
        }
        if (Diary.GetComponent<Book>().textEnabled == true)
        {
            diary = true;
            when = days;
        }
        if (photo2&&diary&&days>when)
        {
            Photo2.SetActive(false);
            Photo3.SetActive(true);
        }
        if (Photo3.GetComponent<Photo>().image.enabled)
        {
            photo3 = true;
            bool[] vars = { photo1, photo2, photo3, diary, door };
            //SceneManager.LoadScene("house");
            ObjectPool.Get<GameOver>();
        }

            if (timer >= 180 && timer < 360)
        {
            //at this point timer is growing from 180 to 360
            if (!hasFather)
            {
                GameManager.Instance.GenerateFather();
            }
        }
        else if(timer >= 360)
        {
            timer = 0;
            days += 1;
            
            if (hasFather)
            {
                GameManager.Instance.RemoveFather();
            }
        }

        ManageLightIntensity(timer);
    }

    public static int Day
    {
        get { return Instance.days; }
    }
    
    private void ManageLightIntensity(float timer)
    {
        if (timer > 280 || timer < 135)
        {
            if (RenderSettings.ambientIntensity < 1)
            {
                RenderSettings.ambientIntensity += .05f * Time.deltaTime * cycleSpeed;
            }
        }
        else
        {
            if (RenderSettings.ambientIntensity > 0)
            {
                RenderSettings.ambientIntensity -= .05f * Time.deltaTime * cycleSpeed;
            }
        }
    }
}
