using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Dictionary<string, GlyphName> GlyphNames { get; private set; } = new();
    [SerializeField] private GameObject _staff1Line;
    [SerializeField] private GameObject _barlineSingle;
    [SerializeField] private GameObject _glyph;

    [SerializeField] private Transform _staff;
    private Staff staff;

    public float FontSize { get; private set; }
    public float EM(float v) => FontSize * v;
    public float OSS(float v) => FontSize * 0.25f * v;

    public const float THIN_BARLINE_THICKNESS = 0.16f;
    public const float STAFF_LINE_THICKNESS = 0.13f;

    private void Awake()
    {
        var json = Resources.Load<TextAsset>("raw/glyphnames");
        GlyphNames = JsonParser.Parse(json.text);

        char noteheadBlack = GlyphNames["noteheadBlack"].Codepoint;
        //_score.text = gclef.ToString();

        FontSize = 100;

        int scoreLength = 100;
        for (int i = 0; i < 5; i++)
        {
            RectTransform tr = Instantiate(_staff1Line, _staff).GetComponent<RectTransform>();
            tr.localPosition = new Vector2(0, i * 0.25f * 100);
            tr.sizeDelta = new(0, OSS(STAFF_LINE_THICKNESS));
        }

        staff = _staff.GetComponent<Staff>();

        DrawGlyph(GlyphNames["gClef"].Codepoint, OSS(0.5f));

        RectTransform bar = Instantiate(_barlineSingle, _staff).GetComponent<RectTransform>();
        bar.localPosition = new Vector2(scoreLength, 0);
        bar.sizeDelta = new(OSS(THIN_BARLINE_THICKNESS), EM(1));

        DrawGlyph(GlyphNames["noteheadBlack"].Codepoint);
        DrawGlyph(GlyphNames["accidentalSharp"].Codepoint);

        //bar.sizeDelta = new(OSS(THIN_BARLINE_THICKNESS), EM(1));
    }
    public void DrawGlyph(char glyph, float y = 0)
    {
        Canvas.ForceUpdateCanvases();
        RectTransform rt = Instantiate(_glyph, _staff).GetComponent<RectTransform>();
        rt.localPosition = new(0, y);
        Glyph g = _glyph.GetComponent<Glyph>();
        g.Init(glyph);
        staff.Add(g);

    }
}

