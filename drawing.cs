using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EFTDLL
{
    public class draw
    {
        public static void Line(Vector2 lineStart, Vector2 lineEnd, Color color, int thickness)
        {
            if (_coloredLineTexture == null || _coloredLineColor != color)
            {
                _coloredLineColor = color;
                _coloredLineTexture = new Texture2D(1, 1);
                _coloredLineTexture.SetPixel(0, 0, _coloredLineColor);
                _coloredLineTexture.wrapMode = 0;
                _coloredLineTexture.Apply();
            }

            var vector = lineEnd - lineStart;
            float pivot = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
            {
                pivot += 180f;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            int yOffset = (int)Mathf.Ceil((float)(thickness / 2));

            GUIUtility.RotateAroundPivot(pivot, lineStart);
            GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - (float)yOffset, vector.magnitude, (float)thickness), _coloredLineTexture);
            GUIUtility.RotateAroundPivot(-pivot, lineStart);
        }

        private static Texture2D _coloredLineTexture;

        private static Color _coloredLineColor;


        public static void DrawLine(EFT.Player player, Vector2 screen)
        {
            try
            {
                Vector3 w2s = Camera.main.WorldToScreenPoint(player.PlayerBones.RootJoint.position);
                if (w2s.z < 0.01f)
                    return;
                Line(screen, new Vector2(w2s.x, Screen.height - w2s.y), Color.red, 2);
            }
            catch { }
        }

        private static Texture2D texture;

        public static void DrawNiggaLine(Vector2 from, Vector2 to, float thickness, Color color)
        {
            Color = color;
            DrawNiggaLine(from, to, thickness);
        }

        public static void DrawNiggaLine(Vector2 from, Vector2 to, float thickness)
        {
            if(!texture) { texture = new Texture2D(1, 1); texture.filterMode = FilterMode.Point; }

            var vector = to - from;
            float fovlang = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if(vector.x < 0f)
            {
                fovlang += 180f;
            }
            float yOffset = Mathf.Ceil(thickness / 2);
            GUIUtility.RotateAroundPivot(fovlang, from);

            GUI.DrawTexture(new Rect(from.x, from.y - yOffset, vector.magnitude, thickness), texture);

            GUIUtility.RotateAroundPivot(-fovlang, from);
        }

        public static void DrawSwastika(Color color)
        {
            int drX = Screen.width / 2;
            int drY = Screen.height / 2;
            DrawNiggaLine(new Vector2(drX, drY), new Vector2(drX, drY - 10), 1f, color);
            DrawNiggaLine(new Vector2(drX, drY - 10), new Vector2(drX + 10, drY - 10), 1f, color);
            DrawNiggaLine(new Vector2(drX, drY), new Vector2(drX + 10, drY), 1f, color);
            DrawNiggaLine(new Vector2(drX + 10, drY), new Vector2(drX + 10, drY + 10), 1f, color);

            DrawNiggaLine(new Vector2(drX, drY), new Vector2(drX, drY + 10), 1f, color);
            DrawNiggaLine(new Vector2(drX, drY + 10), new Vector2(drX - 10, drY + 10), 1f, color);

            DrawNiggaLine(new Vector2(drX, drY), new Vector2(drX - 10, drY), 1f, color);
            DrawNiggaLine(new Vector2(drX - 10, drY), new Vector2(drX - 10, drY - 10), 1f, color);
        }

        public static void DrawCrossHair(Vector2 position, Vector2 size, float thickness)
        {
            GUI.DrawTexture(new Rect(position.x - size.x / 2f, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y - size.y / 2f, thickness, size.y), Texture2D.whiteTexture);
        }

        public static Color Color
        {
            get { return GUI.color; }
            set { GUI.color = value; }
        }


        public static void CornerBox(Vector2 Head, float Width, float Height, float thickness)
        {
            float num = Width / 4f;

            RectFilled(Head.x - Width / 2f, Head.y, num, 1f);
            RectFilled(Head.x - Width / 2f, Head.y, 1f, num);
            RectFilled(Head.x + Width / 2f - num, Head.y, num, 1f);
            RectFilled(Head.x + Width / 2f, Head.y, 1f, num);
            RectFilled(Head.x - Width / 2f, Head.y + Height - 3f, num, 1f);
            RectFilled(Head.x - Width / 2f, Head.y + Height - num - 3f, 1f, num);
            RectFilled(Head.x + Width / 2f - num, Head.y + Height - 3f, num, 1f);
            RectFilled(Head.x + Width / 2f, Head.y + Height - num - 3f, 1f, num + 1);
        }

        public static void CornerBox(Vector2 Head, float Width, float Height, float thickness, Color color)
        {
            Color = color;
            CornerBox(Head, Width, Height, thickness);
        }


        public static void RectFilled(float x, float y, float width, float height, Color color)
        {
            Color = color;
            RectFilled(x, y, width, height);
        }

        public static void RectFilled(float x, float y, float width, float height)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), texture);
        }


    }
}
