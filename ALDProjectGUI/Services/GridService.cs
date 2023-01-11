namespace ALDProjectGUI.Services
{
    public class GridService
    {
        private static readonly int _defaultSeed = 43;
        private static readonly int _defaultSizeX = 5;
        private static readonly int _defaultSizeY = 5;
        private static readonly int _minSizeX = 3;
        private static readonly int _minSizeY = 3;
        private static readonly int _maxSizeX = 50;
        private static readonly int _maxSizeY = 50;
        private int _sizeX = _defaultSizeX;
        private int _sizeY = _defaultSizeY;
        private int _seed = _defaultSeed;
        private Random _r = new Random(_defaultSeed);

        // tiles are represented as values -2 to 15, where 0 to 15 are regular values, -1 is unset, -2 is no matching tile found
        // regular values in binary represent the connections of the tile
        // least significant bit is top connection, rest is going clockwise
        // 15 = 0b1111 => all directions have a connection
        // 6 = 0b0110 => top has no connection, right and bottom have a connection, left has no connection
        // internal grid is 2 larger in both directions to make calculation of border tiles easy
        private int[][] _internalGrid;

        public int GridSizeX 
        { 
            get => _sizeX; 
            set {
                // setter checks if new vaue is within bounds
                _sizeX = value > _minSizeX ? (value < _maxSizeX ? value : _maxSizeX) : _minSizeX;
            }
        }

        public int GridSizeY
        {
            get => _sizeY;
            set
            {
                // same as GridSizeX setter
                _sizeY = value > _minSizeY ? (value < _maxSizeY ? value : _maxSizeY) : _minSizeY;
            }
        }

        // returns non-border Grid tiles
        public int[][] Grid { get => _internalGrid[1..^1].Select(x => x[1..^1]).ToArray(); }

        // used to generate tiles
        public int GridSeed 
        { 
            get => _seed;
            set {
                // seed setter also updates the random used to generate tiles
                _seed = value;
                _r = new Random(_seed);
            }
        }
        public GridService()
        {
            EmptyGrid();
        }


        public void GenerateGrid()
        {
            // reset the Random object to always generate the same pattern with the same seed
            GridSeed = GridSeed;
            // create empty grid with currently set dimension
            EmptyGrid();
            // get center of dimensions
            var centerX = _sizeX / 2;
            var centerY = _sizeY / 2;
            // dictionairy of all tiles and their distance from origin
            // tiles will be set in order by their distance from origin in ascending order
            var dict = new Dictionary<(int, int), double>();
            // for every tile in inner grid (Grid aka internal grid without border)
            for (var x = 0; x < _sizeX; x++)
            {
                for (var y = 0; y < _sizeY; y++)
                {
                    var dx = x - centerX;
                    var dy = y - centerY;
                    // distance computed via pythagorean theorem
                    var delta = Math.Sqrt(dx * dx + dy * dy);
                    dict.Add((x, y), delta);
                }
            }
            // sort points by their distance
            var dist = dict.OrderBy(x => x.Value).ToArray();


            foreach (var tile in dist)
            {
                // offset coordinates to internal grid
                var x = tile.Key.Item1 + 1;
                var y = tile.Key.Item2 + 1;

                // <direction>v ... value of adjacent tile in direction
                // <direction> ... value of specific connection from adjacent tile
                // specific connection value is just getting the one relevant bit using bitwise AND (&)
                var topv = _internalGrid[x - 1][y];
                var top = topv > -1 ? (topv & 4) / 4 : Rnd();
                var rightv = _internalGrid[x][y + 1];
                var right = rightv > -1 ? (rightv & 8) / 8 : Rnd();
                var botv = _internalGrid[x + 1][y];
                var bot = botv > -1 ? (botv & 1) : Rnd();
                var leftv = _internalGrid[x][y - 1];
                var left = leftv > -1 ? (leftv & 2) / 2 : Rnd();

                // calculate value of current tile based on above values
                _internalGrid[x][y] = top + right * 2 + bot * 4 + left * 8;

            }
            return;
        }

        public void EmptyGrid()
        {
            // grid is 2 tiles larger in both direction to calculate border tiles easily
            // public Grid getter then leaves out the edge
            _internalGrid = new int[GridSizeX + 2].Select(x => new int[GridSizeY + 2].Select(x => -1).ToArray()).ToArray();
        }

        private int Rnd()
        {
            // used to get value if tile is yet unset
            return _r.Next(2);
        }
    }
}
