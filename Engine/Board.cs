namespace Engine
{
    // $G$ DSN-999 (-5) Every file should contain only 1 class/enum
    public enum CellStatus
    {
        Empty,
        Player1Symobl,
        Player2Symobl,
    }

    internal class Board
    {
        private CellStatus[,] m_CellsBoard;
        private int m_BoardSize;

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_CellsBoard = new CellStatus[i_BoardSize, i_BoardSize];
            InitializeBoard();
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_CellsBoard[i, j] = CellStatus.Empty;
                }
            }
        }

        public void SetCell(int i_Row, int i_Col, CellStatus i_Value)
        {
            m_CellsBoard[i_Row - 1, i_Col - 1] = i_Value;
        }

        public bool IsCellEmpty(int i_Row, int i_Col)
        {
            return m_CellsBoard[i_Row - 1, i_Col - 1] == CellStatus.Empty;
        }

        public CellStatus GetCellSymbol(int i_Row, int i_Col)
        {
            return m_CellsBoard[i_Row, i_Col];
        }

        public void ClearBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (!IsCellEmpty(i + 1, j + 1))
                    {
                        SetCell(i + 1, j + 1, CellStatus.Empty);
                    }
                }
            }
        }
    }
}