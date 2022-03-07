using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    string gameId = "4644885";
    bool testMode = true;

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
    private void Update()
    {

        if (Advertisement.isShowing)
        {
            Advertisement.Banner.Hide();
        }
        else if(Advertisement.IsReady("Banner"))
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show("Banner");
        }
    }
    public void ShowVideoAd()
    {

        if (Advertisement.IsReady("Rewarded"))
        {
            Advertisement.Show("Rewarded");
        }
        
    }

    private void OnDestroy()
    {
        Advertisement.Banner.Hide();
    }


}
