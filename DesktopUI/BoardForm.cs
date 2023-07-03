using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;

namespace DesktopUI
{
    public partial class BoardForm : Form
    {
        private readonly Game r_Game;
        private readonly BoardCellButton[,] r_BoardButtons;
        private readonly int r_ButtonsBoardSize;
        private readonly ScoreLabel r_LabelPlayerOneScore, r_LabelPlayerTwoScore;

        public BoardForm(Game i_Game)
        {
            r_Game = i_Game;
            r_ButtonsBoardSize = r_Game.GetBoardSize();
            r_BoardButtons = new BoardCellButton[r_ButtonsBoardSize, r_ButtonsBoardSize];
            r_LabelPlayerOneScore = new ScoreLabel();
            r_LabelPlayerTwoScore = new ScoreLabel();
            InitializeComponent();
            initializeButtons();
            allignScoreLabels();
            alignClientSize();

        }

        private void initializeButtons()
        {
            int xPosition = 10, yPosition =  10;
            BoardCellButton currentButton = null;

            for (int i = 0; i < r_ButtonsBoardSize; i++)
            {
                for (int j = 0; j < r_ButtonsBoardSize; j++)
                {
                    currentButton = new BoardCellButton(i, j);
                    currentButton.Size = new System.Drawing.Size(50, 50);
                    currentButton.UseVisualStyleBackColor = true;
                    currentButton.Location = new Point(xPosition, yPosition);
                    xPosition += currentButton.Width;
                    //currentButton.Click += button_Click;
                    currentButton.TabStop = false;
                    r_BoardButtons[i, j] = currentButton;
                    this.Controls.Add(currentButton);
                    //m_LogicEngineManager.AddMethodToActionArray(currentButton.changeButtonText, i, j);
                }

                xPosition = 10;
                yPosition = currentButton.Bottom;
            }
        }
        private void alignClientSize()
        {
            Button lastButton = r_BoardButtons[r_ButtonsBoardSize - 1, r_ButtonsBoardSize - 1];
            int clientSizeWidth = (lastButton.Right + 10);
            int clientSizeHeight = (lastButton.Bottom + 10) + m_LabelPlayerOne.Height * 2;

            this.ClientSize = new Size(clientSizeWidth, clientSizeHeight);
        }

        private void allignScoreLabels()
        {
            string playerOneName, PlayerTwoName;
            Button lastButton = r_BoardButtons[r_ButtonsBoardSize - 1, r_ButtonsBoardSize - 1];
            Button firstButton = r_BoardButtons[0, 0];

            playerOneName = r_Game.GetFirstPlayerName() + ':';
            PlayerTwoName = r_Game.GetSecondPlayerName() + ":";
            //first player name label
            m_LabelPlayerOne.Text = playerOneName;
            m_LabelPlayerOne.Top = lastButton.Bottom + 10;
            m_LabelPlayerOne.Left = (lastButton.Right + firstButton.Left)/2 - 2*(lastButton.Width/2);
            //first player score label
            initializeScoreLabelForPlayerOne();
            //second player name label
            m_LabelPlayerTwo.Text = PlayerTwoName;
            m_LabelPlayerTwo.Top = m_LabelPlayerOne.Top;
            m_LabelPlayerTwo.Left = r_LabelPlayerOneScore.Right + lastButton.Width/2;
            //second player score label
            initializeScoreLabelForPlayerTwo();
        }

        private void initializeScoreLabelForPlayerOne()
        {
            r_LabelPlayerOneScore.AutoSize = true;
            r_LabelPlayerOneScore.Top = m_LabelPlayerOne.Top;
            r_LabelPlayerOneScore.Left = m_LabelPlayerOne.Right + 2;
            r_LabelPlayerOneScore.Name = "m_LabelPlayerOneScore";
            r_LabelPlayerOneScore.Size = new System.Drawing.Size(13, 13);
            r_LabelPlayerOneScore.TabIndex = 2;
            r_LabelPlayerOneScore.Text = "0";
            this.Controls.Add(r_LabelPlayerOneScore);
        }

        private void initializeScoreLabelForPlayerTwo()
        {
            m_LabelPlayerTwo.Text = r_Game.GetSecondPlayerName();
            r_LabelPlayerTwoScore.AutoSize = true;
            r_LabelPlayerTwoScore.Top = m_LabelPlayerTwo.Top;
            r_LabelPlayerTwoScore.Left = m_LabelPlayerTwo.Right + 2;
            r_LabelPlayerTwoScore.Name = "m_LabelPlayerTwoScore";
            r_LabelPlayerTwoScore.Size = new System.Drawing.Size(13, 13);
            r_LabelPlayerTwoScore.TabIndex = 3;
            r_LabelPlayerTwoScore.Text = "0";
            this.Controls.Add(r_LabelPlayerTwoScore);
        }

    }
    
}