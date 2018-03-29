using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * 材质管理
 */
public class colorSchemen : MonoBehaviour
{
    [Header("背景材质")]
    [Tooltip("背景")]
    public List<Material> background;
    [Tooltip("背景2")]
    public List<Material> background2;
    [Tooltip("背景（参考）")]
    public Material backgroundRef;
    [Header("破裂材质")]
    [Tooltip("破裂的材质")]
    public List<Material> badass;
    [Tooltip("破裂的材质（参考）")]
    public Material badassRef;
    [Header("圆柱材质")]
    [Tooltip("圆柱材质")]
    public List<Material> baseCylinder;
    [Tooltip("圆柱材质（参考）")]
    public Material baseCylinderRef;
    [Header("圆柱材质")]
    [Tooltip("圆柱材质")]
    public List<Material> mcC;
    [Tooltip("球类材质（参考）")]
    public Material mcRef;
    [Header("平台材质")]
    [Tooltip("平台材质")]
    public List<Material> platform;
    [Tooltip("平台材质（参考）")]
    public Material platformRef;
    [Tooltip("精灵渲染器")]
    public SpriteRenderer spot;
    [Tooltip("拖尾材质")]
    public List<Material> trail;
    [Tooltip("拖尾材质（球形）")]
    public Material trailGlobal;
    [Tooltip("拖尾材质（参考）")]
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
