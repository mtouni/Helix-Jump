using System;
using System.Collections.Generic;
using UnityEngine;

public class colorSchemen : MonoBehaviour
{
    public List<Material> background;
    public List<Material> background2;
    public Material backgroundRef;
    public List<Material> badass;
    public Material badassRef;
    public List<Material> baseCylinder;
    public Material baseCylinderRef;
    public List<Material> mcC;
    public Material mcRef;
    public List<Material> platform;
    public Material platformRef;
    public SpriteRenderer spot;
    public List<Material> trail;
    public Material trailGlobal;
    public Material trailRef;

    private void Start()
    {
        this.SwitchColors(this.mcRef, this.mcC[Base.currentLevel % 6]);
        this.SwitchColors(this.platformRef, this.platform[Base.currentLevel % 6]);
        this.SwitchColors(this.baseCylinderRef, this.baseCylinder[Base.currentLevel % 6]);
        this.SwitchColors(this.badassRef, this.badass[Base.currentLevel % 6]);
        this.SwitchColors(this.backgroundRef, this.background[Base.currentLevel % 6]);
        this.trailRef.SetColor("_ColorMain", this.mcRef.GetColor("_MidColor"));
        this.trailGlobal.SetColor("_ColorMain", this.mcRef.GetColor("_MidColor"));
        this.spot.color = this.mcRef.GetColor("_MidColor");
    }

    private void SwitchColors(Material matRef, Material mat)
    {
        matRef.SetColor("_ColorMain", mat.GetColor("_ColorMain"));
        matRef.SetColor("_MidColor", mat.GetColor("_MidColor"));
        matRef.SetColor("_SpecColor", mat.GetColor("_SpecColor"));
    }

    private void SwitchColorsLerped(Material matRef, Material mat, Material matTo)
    {
        float t = Mathf.Sin((((BaseSceneManager<mc>.Instance.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1))) * 3.141593f) / 2f);
        matRef.SetColor("_ColorMain", Color.Lerp(mat.GetColor("_ColorMain"), matTo.GetColor("_ColorMain"), t));
        matRef.SetColor("_MidColor", Color.Lerp(mat.GetColor("_MidColor"), matTo.GetColor("_MidColor"), t));
        matRef.SetColor("_SpecColor", Color.Lerp(mat.GetColor("_SpecColor"), matTo.GetColor("_SpecColor"), t));
    }

    private void Update()
    {
        if (BaseSceneManager<mc>.Instance.isActive)
        {
            this.SwitchColorsLerped(this.backgroundRef, this.background[Base.currentLevel % 6], this.background2[Base.currentLevel % 6]);
        }
    }
}
