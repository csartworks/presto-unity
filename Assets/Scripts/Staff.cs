using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staff : MonoBehaviour
{
    public const float THIN_BARLINE_THICKNESS = 0.16f;
    public const float STAFF_LINE_THICKNESS = 0.13f;
    public float EM(float v) => FontSize * v;
    public float OSS(float v) => FontSize * 0.25f * v;

    [SerializeField] private GameObject _staff1Line;
    [SerializeField] private GameObject _barlineSingle;
    [SerializeField] private GameObject _glyph;
    [SerializeField] private int staffLength = 100;
    [SerializeField] public float FontSize { get; private set; } = 100;

    private List<RectTransform> rts = new();

    private void Awake()
    {
        FontSize = 100;
        GetComponent<RectTransform>().sizeDelta = new(staffLength, FontSize);
        DrawStaff5Line();
        DrawBarline();
        DrawGlyph(Main.GlyphNames["gClef"].Codepoint, OSS(0.5f));
        DrawGlyph(Main.GlyphNames["accidentalSharp"].Codepoint);
        DrawGlyph(Main.GlyphNames["noteheadBlack"].Codepoint);
    }
    public void DrawStaff5Line()
    {
        for (int i = 0; i < 5; i++)
        {
            RectTransform tr = Instantiate(_staff1Line, transform).GetComponent<RectTransform>();
            tr.localPosition = new Vector2(0, i * 0.25f * 100);
            tr.sizeDelta = new(0, OSS(STAFF_LINE_THICKNESS));
        }
    }
    public void DrawBarline()
    {
        RectTransform bar = Instantiate(_barlineSingle, transform).GetComponent<RectTransform>();
        bar.localPosition = new Vector2(staffLength, 0);
        bar.sizeDelta = new(OSS(THIN_BARLINE_THICKNESS), EM(1));
    }
    public void DrawGlyph(char glyph, float y = 0)
    {
        Canvas.ForceUpdateCanvases();
        RectTransform rt = Instantiate(_glyph, transform).GetComponent<RectTransform>();
        rt.GetComponent<Glyph>().Init(glyph);
        float x = 0;
        int i = rts.Count - 1;
        if (i >= 0)
        {
            RectTransform prt = rts[i];
            x = prt.rect.size.x + prt.localPosition.x;
        }
        rt.localPosition = new Vector2(x, y);
        rts.Add(rt);
    }
}
