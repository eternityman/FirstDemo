/*
    author: wzs
    date:   2024-12-02T08:24:47
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFind
{
    public class DSHeap<T> where T : IComparable<T>
    {

        #region 事件

        #endregion

        #region 属性

        /// <summary>
        /// 堆内容数量
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// 容量大小
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// 是否是大顶堆
        /// </summary>
        public bool IsMaxHeap => _isMaxHeap;

        #endregion

        #region 变量

        /// <summary>
        /// 堆内容
        /// </summary>
        private T[] _heap;

        /// <summary>
        /// 数组大小
        /// </summary>
        private int _count;

        /// <summary>
        /// 容量大小
        /// </summary>
        private int _capacity;

        /// <summary>
        /// 是否还大顶堆
        /// </summary>
        private bool _isMaxHeap = true;

        #endregion

        #region 生命周期

        #endregion

        #region 公有方法

        //----------------------Public----------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">初始堆大小</param>
        /// <param name="isMaxHeap">是否是大顶堆</param>
        public DSHeap(int size = 4, bool isMaxHeap = true)
        {
            _heap = new T[size];
            _count = 0;
            _capacity = size;
            this._isMaxHeap = isMaxHeap;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="heap">初始数组</param>
        /// <param name="isMaxHeap"></param>
        public DSHeap(int[] heap, bool isMaxHeap = true)
        {
            _count = heap.Length;
            _capacity = 2 ^ (_count / 2 + 1);
            _heap = new T[_capacity];
            this._isMaxHeap = isMaxHeap;
            heap.CopyTo(_heap, 0);
            _InitHeap();
        }

        /// <summary>
        /// 添加新元素
        /// </summary>
        /// <param name="item"></param>
        public void Put(T item)
        {
            _count++;
            if (_count >= _capacity)
            {
                _capacity *= 2;
                
                T[] temp = new T[_capacity];
                _heap.CopyTo(temp, 0);
                _heap = temp;
            }
            _heap[_count] = item;
            _Heapify(0,_count);
        }

        /// <summary>
        /// 打印堆
        /// </summary>
        public void PrintHeap()
        {
            string result = "";
            for (int i = 0; i < _count; i++)
            {
                result += (_heap[i] + " ,");
            }

            Debug.Log(result);
        }

        /// <summary>
        /// （大顶堆）降序排序，不保证层次顺序读起来一定对，但是对于大小堆的结构一定是对的
        /// </summary>
        /// <returns></returns>
        public void SortByDescending()
        {
            _isMaxHeap = true;
            _InitHeap();
        }

        /// <summary>
        /// （小顶堆）升序排序，不保证层次顺序读起来一定对，但是对于大小堆的结构一定是对的
        /// </summary>
        /// <returns></returns>
        public void SortByAscending()
        {
            _isMaxHeap = false;
            _InitHeap();
        }

        /// <summary>
        /// 弹出顶部
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T PopTop()
        {
            if (_count <= 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            if (_count == 1)
            {
                _count--;
                return _heap[0];
            }

            T root = _heap[0];
            _heap[0] = _heap[_count - 1];
            _heap[_count - 1] = default;
            _count--;
            _Heapify(0, _count);

            return root;
        }

        public T PopBottom()
        {
            if (_count <= 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            if (_count == 1)
            {
                _count--;
                return _heap[0];
            }

            T root = _heap[_count - 1];
            _heap[_count - 1] = default;
            _count--;
            _Heapify(_count / 2, _count);

            return root;
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <returns></returns>
        public List<T> SortDescending()
        {
            List<T> sortedList = new List<T>();
            int originalSize = _count; // Save original size
            Func<T> act = _isMaxHeap ? PopTop : PopBottom;
            while (_count > 0)
            {
                sortedList.Add(act());
            }

            _count = originalSize; // Restore original size
            T[] arr = sortedList.ToArray();
            arr.CopyTo(_heap, 0);
            return sortedList;
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <returns></returns>
        public List<T> SortAscending()
        {
            List<T> sortedList = new List<T>();
            int originalSize = _count; // Save original size
            Func<T> act = !_isMaxHeap ? PopTop : PopBottom;
            while (_count > 0)
            {
                sortedList.Add(act());
            }

            _count = originalSize; // Restore original size
            T[] arr = sortedList.ToArray();
            arr.CopyTo(_heap, 0);
            return sortedList;
        }

        #endregion

        #region 保护方法

        //----------------------Protect----------------------


        #endregion

        #region 私有方法

        //----------------------Private----------------------

        private int Parent(int index) => (index - 1) / 2;
        private int Left(int index) => index * 2 + 1;
        private int Right(int index) => index * 2 + 2;

        private void _InitHeap()
        {
            for (int i = _heap.Length / 2 - 1; i >= 0; i--)
            {
                _Heapify(i, _count);
            }
        }

        /// <summary>
        /// 堆调整
        /// </summary>
        /// <param name="index"></param>
        /// <param name="maxLen"></param>
        private void _Heapify(int index, int maxLen)
        {
            int largest = index;
            int left = Left(index);
            int right = Right(index);
            if (left < maxLen && _Compare(largest, left) > 0)
            {
                largest = left;
            }

            if (right < maxLen && _Compare(largest, right) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                _Swap(largest, index);
                _Heapify(largest, maxLen);
            }
        }

        /// <summary>
        /// 交换逻辑
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void _Swap(int left, int right)
        {
            (_heap[left], _heap[right]) = (_heap[right], _heap[left]);
        }

        /// <summary>
        /// 最大堆排序
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private int _CompareMax(int left, int right)
        {
            int diff = _heap[right].CompareTo(_heap[left]);
            return diff;
        }

        private int _CompareMin(int left, int right)
        {
            int diff = _heap[right].CompareTo(_heap[left]);
            return -diff;
        }

        /// <summary>
        /// 对比函数
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private int _Compare(int left, int right)
        {
            int result = _isMaxHeap ? _CompareMax(left, right) : _CompareMin(left, right);
            return result;
        }

        #endregion


    }
}