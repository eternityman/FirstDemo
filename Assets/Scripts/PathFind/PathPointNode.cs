using System;
using System.Collections.Generic;

namespace PathFind
{
    /// <summary>
    /// 方向枚举
    /// </summary>
    public enum EDirType
    {
        None = -1,
        Top = 0,
        Bottom,
        Left,
        Right,
        TopRight,
        TopLeft,
        BottomLeft,
        BottomRight,
        Count
    }

    public class PathPointNode : IComparable<PathPointNode>
    {
        /// <summary>
        /// 直线消耗
        /// </summary>
        private const int LineCost = 10;

        /// <summary>
        /// 斜线消耗
        /// </summary>
        private const int Tilted = 14;

        public PathPointNode Parent => _parentPoint;
        
        /// <summary>
        /// 索引
        /// </summary>
        public int Index{get;set;}
        
        /// <summary>
        /// X坐标
        /// </summary>
        public int X{get;set;}
        
        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y{get;set;}
        
        /// <summary>
        /// 起点到节点的代价
        /// </summary>
        public int G{get;set;}
        
        /// <summary>
        /// 节点到终点的代价
        /// </summary>
        public int H{get;set;}
        
        /// <summary>
        /// 跳点方向
        /// </summary>
        public EDirType JumpDir{get;set;}
        
        /// <summary>
        /// 权重
        /// </summary>
        public int F{get;set;}

        public PathPointNode[] neighbors = new PathPointNode[(int)EDirType.Count];

        /// <summary>
        /// 父节点（来源节点）
        /// </summary>
        private PathPointNode _parentPoint;
        
        #region StaticMethod

        /// <summary>
        /// 计算方向消耗
        /// </summary>
        /// <param name="original"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ComputeDirCost(PathPointNode original, PathPointNode target)
        {
            int xDelta = original.X > target.X ? original.X - target.X : target.X - original.X;
            int yDelta = original.Y > target.Y ? original.Y - target.Y : target.Y - original.Y;

            return (xDelta + yDelta) * 10;
        }

        /// <summary>
        /// 计算格子消耗
        /// </summary>
        /// <param name="original"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ComputeGridCost(PathPointNode original, PathPointNode target)
        {
            EDirType dirType = original.GetDirType(target);
            if (dirType == EDirType.None)
            {
                return 999;
            }

            switch (dirType)
            {
                case EDirType.Top:
                case EDirType.Bottom:
                case EDirType.Left:
                case EDirType.Right:
                    return LineCost;
                default:
                    return Tilted;
            }
        }

        #endregion

        public PathPointNode(int index, int posX, int posY)
        {
            this.Index = index;
            this.X = posX;
            this.Y = posY;
            G = 0;
            H = 0;
            F = 0;
            JumpDir = EDirType.None;
            _parentPoint = null;
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public void Clear()
        {
            G = 0;
            H = 0;
            F = 0;
            JumpDir = EDirType.None;
            _parentPoint = null;
        }

        /// <summary>
        /// 解析邻居节点
        /// </summary>
        /// <param name="mapList"></param>
        public void RegisterNeighbors(PathPointNode[,] mapList)
        {
            if (mapList == null)
            {
                return;
            }
            
            int maxWidth = mapList.GetLength(0);
            int maxHeight = mapList.GetLength(1);

            bool topOk = Y + 1 < maxHeight; 
            bool bottomOk = Y - 1 >= 0;
            bool rightOk = X + 1 < maxWidth;
            bool leftOk = X - 1 >= 0;
            
            if (topOk)
            {
                this.neighbors[(int)EDirType.Top] = mapList[X, Y + 1];
            }

            if (bottomOk)
            {
                this.neighbors[(int)EDirType.Bottom] = mapList[X, Y - 1];
            }

            if (rightOk)
            {
                this.neighbors[(int)EDirType.Left] = mapList[X + 1, Y];
            }

            if (leftOk)
            {
                this.neighbors[(int)EDirType.Right] = mapList[X - 1, Y];
            }

            if (topOk && leftOk)
            {
                this.neighbors[(int)EDirType.TopLeft] = mapList[X - 1, Y + 1];
            }

            if (topOk && rightOk)
            {
                this.neighbors[(int)EDirType.TopRight] = mapList[X + 1, Y + 1];
            }

            if (bottomOk && rightOk)
            {
                this.neighbors[(int)EDirType.BottomRight] = mapList[X + 1, Y - 1];
            }

            if (bottomOk && leftOk)
            {
                this.neighbors[(int)EDirType.BottomLeft] = mapList[X - 1, Y - 1];
            }
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(PathPointNode parent)
        {
            _parentPoint = parent;
        }

        /// <summary>
        /// 获取方向类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public EDirType GetDirType(PathPointNode target)
        {
            for (int i = 0; i < neighbors.Length; i++)
            {
                PathPointNode neighbor = neighbors[i];
                if (neighbor != null && neighbor.Index == target.Index)
                {
                    return (EDirType)i;
                }
            }

            return EDirType.None;
        }

        public int CompareTo(PathPointNode other)
        {
            return F.CompareTo(other.F);
        }
    }
}