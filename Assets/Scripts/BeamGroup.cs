using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace presto.unity
{
    public class BeamGroup : GlyphBehaviour
    {
        private readonly List<Note> _notes = new();
        public void Draw()
        {
            Note startNote = _notes[0];
            Note endNote = _notes.Last();
            ShortenEndNoteToMean();
            foreach (var n in _notes)
            {
                ShortenStemToFitBeam(n.Stem.Rt);
            }
            void ShortenEndNoteToMean()
            {
                var pitches = from p in _notes select p.Pitch;
                var av = (float)pitches.Average();
                var mean = GlyphBehaviour.SS((float)pitches.Average() / 2);
                var temp = endNote.Rt.localPosition.y;
                var moreShorten = mean - temp;
                endNote.Stem.Rt.sizeDelta -= new Vector2(0, moreShorten);
            }
            void ShortenStemToFitBeam(RectTransform stem)
            {
                // Vector3 s = stem.TransformPoint(stem.rect.xMin, 0, 0);
                // var b = s.x - leftMost.x;
                // var a = GetComponent<RectTransform>().sizeDelta.x;
                // var t = b / a;
                // var lerp = Vector3.Lerp(leftMost, rightMost, t);
                // var inv = stem.InverseTransformPoint(lerp);
                // stem.sizeDelta = new(stem.sizeDelta.x, inv.y);
            }
        }
    }
}