/*
    author: wzs
    date:   2025-01-14T05:06:04
*/

using System.Collections.Generic;

namespace PathFind
{
    public abstract class PathFinderBase
    {

        #region 事件

        #endregion

        #region 属性

        #endregion

        #region 变量

        /// <summary>
        /// 起始点
        /// </summary>
        protected PathPointNode _startNode;

        /// <summary>
        /// 结束点
        /// </summary>
        protected PathPointNode _endNode;

        /// <summary>
        /// 缓存堆
        /// </summary>
        protected DSHeap<PathPointNode> _cacheHeap = new(2, false);

        /// <summary>
        /// 链接字典
        /// </summary>
        protected Dictionary<int, int> _cameFrom = new();
        
        #endregion

        #region 生命周期

        #endregion

        #region 公有方法

        //----------------------Public----------------------

        public List<PathPointNode> FindPath(GridPosition start, GridPosition end, PathFindMapData pathFindMapData)
        {
            int width = pathFindMapData.Width;
            int height = pathFindMapData.Height;
            if (start.X < 0 || start.X >= width || start.Y < 0 || start.Y >= height)
            {
                return null;
            }

            if (end.X < 0 || end.X >= width || end.Y < 0 || end.Y >= height)
            {
                return null;
            }
            
            this._startNode = pathFindMapData.GetPointNode(start.X, start.Y);
            this._endNode = pathFindMapData.GetPointNode(end.X, end.Y);

            List<PathPointNode> path = _FindPath();
            return path;
        }
        
        #endregion

        #region 保护方法

        //----------------------Protect----------------------

        /// <summary>
        /// 寻找路径
        /// </summary>
        /// <returns></returns>
        protected List<PathPointNode> _FindPath()
        {
            List<PathPointNode> path = new();
            _cacheHeap.Put(_startNode);

            PathPointNode currentNode = _startNode;
            bool success = _SearchNode(currentNode);

            if (success)
            {
                PathPointNode node = _endNode;
                while (node.Parent != null)
                {
                    path.Add(currentNode);
                    
                    node = node.Parent;
                }
            }
            
            return path;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected abstract bool _SearchNode(PathPointNode node);

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        protected PathPointNode _TryGetNode()
        {
            if (_cacheHeap.Count == 0)
            {
                return null;
            }

            return _cacheHeap.PopTop();
        }
        
        #endregion

        #region 私有方法

        //----------------------Private----------------------

        #endregion


    }
}