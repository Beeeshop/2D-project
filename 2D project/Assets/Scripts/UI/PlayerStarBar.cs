using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStarBar : MonoBehaviour
{
   public Image healthImage;
   public Image healthDelayImage;
   public Image poweImage;

    private void Update()
    {
        if(healthDelayImage.fillAmount>healthImage.fillAmount)
        {
            healthDelayImage.fillAmount-=Time.deltaTime;
        }
    }
    //血量百分比
    public void OnHealthChange(float persentage)
   {
       healthImage.fillAmount = persentage;
   }
}
