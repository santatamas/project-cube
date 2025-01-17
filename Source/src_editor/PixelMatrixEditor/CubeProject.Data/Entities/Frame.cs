﻿using System;

namespace CubeProject.Data.Entities
{
    public class Frame<T>
    {
        public Int16 Width { get; private set; }
        public Int16 Height { get; private set; }

        public Frame(Int16 width, Int16 height)
        {
            Width = width;
            Height = height;
            _data = new T[width,height];
        }

        private T[,] _data;
        public T[,] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public T this[int i, int j]
        {
            get { return _data[i, j]; }
            set { _data[i, j] = value; }
        }
    }
}
