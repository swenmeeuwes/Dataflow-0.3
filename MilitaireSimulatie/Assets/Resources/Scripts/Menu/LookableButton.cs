using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public abstract class LookableButton : MonoBehaviour {
    const int countDown = 3;

    [HideInInspector]
    public bool looking = false;

    bool lastFrameLooking = false;
    bool isCounting = false;
    int currentCount;

    protected virtual void Start()
    {
        currentCount = countDown;
        gameObject.layer = LayerMask.NameToLayer("Lookable");
    }
    protected virtual void Update()
    {
        if(looking && !lastFrameLooking)
        {
            if(!isCounting)
                StartCoroutine(CountDown());
        }

        if (!looking)
            currentCount = countDown;

        lastFrameLooking = looking;
        looking = false;
    }

    protected virtual void OnGUI()
    {
        if (!lastFrameLooking)
            return;

        var style = new GUIStyle() { fontSize = 48, alignment = TextAnchor.MiddleCenter };
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 2), (currentCount).ToString(), style); // Add 1 to the currentCount because the coroutine is executed before the OnGUI() method
    }

    protected abstract void Action();

    IEnumerator CountDown()
    {
        isCounting = true;
        while (looking || lastFrameLooking)
        {
            currentCount--;
            if(currentCount == 0)
            {
                Action();
                break;
            }
            yield return new WaitForSeconds(1);
        }
        isCounting = false;
    }
}
