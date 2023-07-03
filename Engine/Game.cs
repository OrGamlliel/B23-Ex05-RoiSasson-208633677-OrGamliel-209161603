using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Game
    {
        private Board m_GameBoard;
        private Player m_CurrPlayer;
        private bool m_IsPlayerTwoComputer;
        private List<Tuple<int, int>> m_EmptyCells;
        private bool m_IsGameOver;
        private bool m_IsDraw;

        private Player m_PlayerA { get; set; }

        private Player m_PlayerB { get; set; }

        private int m_StepCounter { get; set; }

        public Game()
        {
        }


        public void InitGame(int i_BoardSize, bool i_IsPlayerTwoComputer, string playerANickname, string playerBNickname)
        {
            m_GameBoard = new Board(i_BoardSize);
            m_IsPlayerTwoComputer = i_IsPlayerTwoComputer;
            m_PlayerA = new Player(playerANickname, CellStatus.Player1Symobl, false);
            if (!m_IsPlayerTwoComputer)
            {
                m_PlayerB = new Player(playerBNickname, CellStatus.Player2Symobl, false);
            }
            else
            {
                m_PlayerB = new Player("AIplayer", CellStatus.Player2Symobl, true);
                m_EmptyCells = new List<Tuple<int, int>>();
            }

            m_IsGameOver = false;
            m_IsDraw = false;
            m_CurrPlayer = m_PlayerA;
            m_StepCounter = 0;
        }

        public int GetBoardSize()
        {
            return m_GameBoard.BoardSize;
        }

        public string GetFirstPlayerName()
        {
            return m_PlayerA.Nickname;
        }

        public string GetSecondPlayerName()
        {
            return m_PlayerB.Nickname;
        }

        public string CurrPlayerName
        {
            get { return m_CurrPlayer.Nickname; }
        }

        public bool IsGameOver
        {
            get { return m_IsGameOver; }
        }

        public bool IsDraw
        {
            get { return m_IsDraw; }
        }

        internal List<Tuple<int, int>> EmptyCells
        {
            get { return m_EmptyCells; }
        }

        public CellStatus GetSymbolInCell(int i_Row, int i_Col)
        {
            return m_GameBoard.GetCellSymbol(i_Row, i_Col);
        }

        private void InitializeList()
        {
            m_EmptyCells.Clear();
            for (int row = 0; row < m_GameBoard.BoardSize; row++)
            {
                for (int col = 0; col < m_GameBoard.BoardSize; col++)
                {
                    m_EmptyCells.Add(Tuple.Create(row, col));
                }
            }
        }

        public bool MakeMove(int i_Row, int i_Col)
        {
            bool isValidMove = true;
            bool isLosingSequenceFound = false;

            if (m_GameBoard.IsCellEmpty(i_Row, i_Col))
            {
                m_GameBoard.SetCell(i_Row, i_Col, m_CurrPlayer.GetSymbol);
                m_StepCounter++;

                if (m_PlayerB.IsComputer)
                {
                    Tuple<int, int> cellToRemove = new Tuple<int, int>(i_Row - 1, i_Col - 1);
                    m_EmptyCells.Remove(cellToRemove);
                }

                isLosingSequenceFound = CheckForLosingSequence(m_CurrPlayer.GetSymbol, i_Row, i_Col);
                if (m_StepCounter > (m_GameBoard.BoardSize + 1) && isLosingSequenceFound)
                {
                    m_IsGameOver = true;
                    if (m_CurrPlayer == m_PlayerA)
                    {
                        m_PlayerB.Score++;
                    }
                    else
                    {
                        m_PlayerA.Score++;
                    }
                }

                // board is full
                if (m_StepCounter == m_GameBoard.BoardSize * m_GameBoard.BoardSize)
                {
                    if (!isLosingSequenceFound)
                    {
                        m_IsDraw = true;
                    }

                    m_IsGameOver = true;
                }

                if ((m_StepCounter % 2) == 0)
                {
                    m_CurrPlayer = m_PlayerA;
                }
                else
                {
                    m_CurrPlayer = m_PlayerB;
                }
            }
            else
            {
                isValidMove = false;
            }

            return isValidMove;
        }

        public bool CheckForLosingSequence(CellStatus i_PlayerSymbol, int i_PlayerPickingRow, int i_PlayerPickingCol)
        {
            bool isSequence = true;
            i_PlayerPickingRow--;
            i_PlayerPickingCol--;

            // Check for horizontal sequence
            for (int col = 0; col < m_GameBoard.BoardSize; col++)
            {
                if (m_GameBoard.GetCellSymbol(i_PlayerPickingRow, col) != i_PlayerSymbol)
                {
                    isSequence = false;
                    break;
                }
            }

            // Check for vertical sequence
            if (!isSequence)
            {
                isSequence = true;
                for (int row = 0; row < m_GameBoard.BoardSize; row++)
                {
                    if (m_GameBoard.GetCellSymbol(row, i_PlayerPickingCol) != i_PlayerSymbol)
                    {
                        isSequence = false;
                        break;
                    }
                }
            }

            // Check for main diagonal sequence
            if (!isSequence)
            {
                isSequence = true;
                for (int i = 0; i < m_GameBoard.BoardSize; i++)
                {
                    if (m_GameBoard.GetCellSymbol(i, i) != i_PlayerSymbol)
                    {
                        isSequence = false;
                        break;
                    }
                }
            }

            // Check for antidiagonal sequence
            if (!isSequence)
            {
                isSequence = true;
                for (int i = 0; i < m_GameBoard.BoardSize; i++)
                {
                    if (m_GameBoard.GetCellSymbol(i, m_GameBoard.BoardSize - i - 1) != i_PlayerSymbol)
                    {
                        isSequence = false;
                        break;
                    }
                }
            }

            return isSequence;
        }

        public void ScoreStatus(out int o_ScorePlayerA, out int o_ScorePlayerB)
        {
            o_ScorePlayerA = m_PlayerA.Score;
            o_ScorePlayerB = m_PlayerB.Score;
        }

        public void ResetGame()
        {
            m_IsGameOver = false;
            m_IsDraw = false;
            m_StepCounter = 0;
            m_CurrPlayer = m_PlayerA;
            m_GameBoard.ClearBoard();
            if (m_PlayerB.IsComputer)
            {
                InitializeList();
            }
        }

        // $G$ SFN-014 (+10) Bonus: The program Includes AI computer player.
        public void GetComputerMove(out int o_SelsetedRowAI, out int o_SelsetedColAI)
        {
            List<Tuple<int, int>> duplicatedEmptyCellsList = EmptyCells.ToList();
            Tuple<int, int> cellToRemove;
            Random random = new Random();
            int randomIndex;
            Tuple<int, int> randomCell;

            foreach (Tuple<int, int> cell in EmptyCells)
            {
                m_GameBoard.SetCell(cell.Item1 + 1, cell.Item2 + 1, CellStatus.Player2Symobl);
                if (m_StepCounter > (m_GameBoard.BoardSize + 1))
                {
                    if (CheckForLosingSequence(CellStatus.Player2Symobl, cell.Item1 + 1, cell.Item2 + 1))
                    {
                        cellToRemove = new Tuple<int, int>(cell.Item1, cell.Item2);
                        duplicatedEmptyCellsList.Remove(cellToRemove);
                    }
                }

                m_GameBoard.SetCell(cell.Item1 + 1, cell.Item2 + 1, CellStatus.Empty);
            }

            if (duplicatedEmptyCellsList.Count == 0)
            {
                duplicatedEmptyCellsList = EmptyCells.ToList();
            }

            randomIndex = random.Next(duplicatedEmptyCellsList.Count);
            randomCell = duplicatedEmptyCellsList[randomIndex];
            o_SelsetedRowAI = randomCell.Item1 + 1;
            o_SelsetedColAI = randomCell.Item2 + 1;
        }
    }
}