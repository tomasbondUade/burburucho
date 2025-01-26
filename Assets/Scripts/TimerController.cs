using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static float time = 1;
    private bool startSpeed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartSpeed", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpeed)
        {
            time += Time.deltaTime / 10;
        }
        
    }

    private void StartSpeed()
    {
        startSpeed = true;
    }
}
