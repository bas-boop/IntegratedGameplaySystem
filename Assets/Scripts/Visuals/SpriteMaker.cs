using System;
using UnityEngine;

namespace Visuals
{
    public static class SpriteMaker
    {
        private static readonly int _size = 100;
        private static readonly int _borderThickness = 5;
        private static readonly float _borderColorDarkness = 0.4f;
        private static readonly Color _clear = Color.clear;
        private static readonly Color[] _pixels = new Color[_size * _size];
        
        public static void MakeSprite(SpriteRenderer spriteRenderer, ShapeType shapeType, Color color)
        {
            Texture2D tex = new (_size, _size);
            Color outlineColor = color * _borderColorDarkness;
            
            for (int i = 0; i < _pixels.Length; i++)
                _pixels[i] = _clear;
            
            tex.SetPixels(_pixels);

            switch (shapeType)
            {
                case ShapeType.SQUARE:
                    DrawSquare(_size, _borderThickness, tex, color, outlineColor);
                    break;

                case ShapeType.TRIANGLE:
                    DrawTriangle(_size, _borderThickness, tex, color, outlineColor);
                    break;

                case ShapeType.CIRCLE:
                    DrawCircle(_size, _borderThickness, tex, color, outlineColor);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
            }

            tex.Apply();
            spriteRenderer.sprite = Sprite.Create(tex, new (0, 0, _size, _size), Vector2.one * 0.5f);
        }

        #region Draw
        
        private static void DrawSquare(
            int size,
            int borderThickness,
            Texture2D tex,
            Color fillColor,
            Color outlineColor)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x < borderThickness 
                        || x >= size - borderThickness 
                        || y < borderThickness 
                        || y >= size - borderThickness)
                        tex.SetPixel(x, y, outlineColor);
                    else
                        tex.SetPixel(x, y, fillColor);
                }
            }
        }
        
        private static void DrawTriangle(
            int size,
            int borderThickness,
            Texture2D tex,
            Color fillColor,
            Color outlineColor)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x <= size - y - 1; x++)
                {
                    bool isBorder = x < borderThickness 
                                    || y < borderThickness
                                    || x >= size - y - borderThickness;
                    
                    tex.SetPixel(x, y, isBorder ? outlineColor : fillColor);
                }
            }
        }
        
        private static void DrawCircle(
            int size,
            int borderThickness,
            Texture2D tex,
            Color fillColor,
            Color outlineColor)
        {
            int center = size / 2;
            float outerRadius = size / 2f;
            float innerRadius = outerRadius - borderThickness;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dx = x - center;
                    float dy = y - center;
                    float dist = Mathf.Sqrt(dx * dx + dy * dy);

                    if (dist <= outerRadius)
                        tex.SetPixel(x, y, dist >= innerRadius ? outlineColor : fillColor);
                }
            }
        }
        
        #endregion
    }
}
