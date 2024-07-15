using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightninghtBolt : MonoBehaviour
{
    [SerializeField] private List<GameObject> dots;
    [SerializeField] private List<GameObject> lightningbolts;
    private AllDotController _alldot;
    private int _countDot = 10;
    private bool _isGetDot;
    [SerializeField] private GameObject currentDot;
    [SerializeField] private GameObject targetDot;
    [SerializeField] private bool isConnected;
    [SerializeField] private int countIndex;
    [SerializeField] private GameObject prefLingningbolt;
    [SerializeField] private float timeCouter;
    private void Awake()
    {
        this.dots = new List<GameObject>();
        this.lightningbolts = new List<GameObject>();
        this._alldot = FindFirstObjectByType<AllDotController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this._isGetDot = true;
        this.isConnected = true;
        this.timeCouter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._isGetDot)
        {
            this.dots = GetRandomDot();
            this._isGetDot = false;
        }
        else
        {
            this.Connector();
        }
    }

    private List<GameObject> GetRandomDot()
    {
        var objDot = new List<GameObject>();
        for (; ; )
        {
            var randomCol = Random.Range(0, this._alldot.Width);
            var randomRow = Random.Range(0, this._alldot.Height);
            var obj = this._alldot.AllDots[randomCol, randomRow];
            var count = 0;
            foreach (var dot in objDot)
            {
                if (dot == obj)
                {
                    count++;
                    break;
                }
            }
            if (count == 0)
                objDot.Add(obj);

            if (objDot.Count == this._countDot)
                break;
        }
        return objDot;
    }

    private void Connector()
    {
        if (!this.currentDot && !this.targetDot && this.isConnected)
        {
            this.countIndex = 0;
            this.currentDot = this.dots[this.countIndex];
            this.targetDot = this.dots[this.countIndex + 1];
        }

        if (this.isConnected && this.countIndex < this._countDot - 1)
        {
            this.timeCouter += Time.deltaTime;
            if (timeCouter >= 0.2f)
            {
                var obj = Instantiate(this.prefLingningbolt);
                obj.SetActive(true);
                obj.transform.GetChild(0).position = this.dots[this.countIndex].transform.position;
                obj.transform.GetChild(1).position = this.dots[this.countIndex + 1].transform.position;
                this.lightningbolts.Add(obj);
                this.countIndex++;
                timeCouter = 0;
                AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.LightningBoltEffect, 0.3f);
            }

        }
        if (this.isConnected && this.countIndex == this._countDot - 1)
        {
            this.isConnected = false;
            StartCoroutine(ExcuteDestroyObj());
        }
    }


    private void DestroyObj()
    {
        foreach (var obj in this.dots)
        {
            Destroy(obj);
        }
    }

    private void DestroyLightningbolt()
    {
        foreach (var obj in this.lightningbolts)
        {
            Destroy(obj);
        }
    }

    private IEnumerator ExcuteDestroyObj()
    {
        yield return new WaitForSeconds(0.6f);
        AudioManager.Instance.AudioSrc.PlayOneShot(AudioManager.Instance.DestroyLightnighBoltEffect, 0.6f);
        this.DestroyObj();
        this.DestroyLightningbolt();
        yield return null;
        GameStateController.Instance.CurrentGameState = GameState.FillingDots;
        StartCoroutine(this._alldot.DestroyMatched());
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
