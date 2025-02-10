/*
    author: wzs
    date:   2024-12-02T04:43:28
*/

using PathFind;

namespace PathFind
{
    public class PathFindMapData
    {

        #region 事件

        #endregion

        #region 属性

        public int Height => _height;
        public int Width => _width;

        #endregion

        #region 变量

        private int _height;
        private int _width;

        private PathPointNode[,] mapRecord;

        #endregion

        #region 生命周期

        #endregion

        #region 公有方法

        //----------------------Public----------------------

        public PathFindMapData(int height, int width)
        {
            _height = height;
            _width = width;
            _InitMap();
        }

        /// <summary>
        /// 获取一个点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public PathPointNode GetPointNode(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return null;
            }
            return mapRecord[x, y];
        }

        #endregion

        #region 保护方法

        //----------------------Protect----------------------

        #endregion

        #region 私有方法

        //----------------------Private----------------------

        private void _InitMap()
        {
            mapRecord = new PathPointNode[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    PathPointNode pointNode = new PathPointNode(y * _width + x, x, y);
                    mapRecord[x, y] = pointNode;
                }
            }

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    mapRecord[x, y].RegisterNeighbors(mapRecord);
                }
            }
        }

        #endregion


    }
}