using System;
using System.Drawing;
using System.Windows.Forms;

namespace Battleships
{
    public partial class Form1 : Form
    {
        private GameManager GameManager { get; set; }

        public Form1()
        {
            InitializeComponent();
            GameManager = new GameManager();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            GameManager.Start();
            DrawBattlefield();
            txtAttempts.Text = GameManager.Attempts.ToString();

            btnFire.Enabled = true;
            txtCoordinates.Enabled = true;
            txtCoordinates.Clear();
            txtCoordinates.Focus();
        }

        private void btnFire_Click(object sender, EventArgs e)
        {
            Fire();
        }

        private void txtCoordinates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Fire();
        }

        private void Fire()
        {
            GameManager.Fire(txtCoordinates.Text);
            DrawBattlefield();
            txtAttempts.Text = GameManager.Attempts.ToString();

            if (GameManager.AreAllSquaresHit())
            {
                MessageBox.Show("Well done !");
                btnFire.Enabled = false;
                txtCoordinates.Enabled = false;
                txtCoordinates.Clear();
                return;
            }

            txtCoordinates.Clear();
            txtCoordinates.Focus();
        }

        private void DrawBattlefield()
        {
            var cellSize = new Size(25, 25);
            var cellPos = new Point(0, 0);

            panel1.Controls.Clear();
            for (int i = 0; i < GameManager.BattlefieldSize.Width; i++)
            {
                //horizontal positions headers
                if (i == 0)
                {
                    DrawBattlefieldCell("", cellSize, cellPos);
                    cellPos.X += cellSize.Width;

                    for (int d = 0; d < GameManager.BattlefieldSize.Width; d++)
                    {
                        DrawBattlefieldCell(GameManager.BattlefieldCells[0, d].HCoordinate, cellSize, cellPos);

                        cellPos.X += cellSize.Width;
                    }

                    cellPos.X = 0;
                    cellPos.Y += cellSize.Height;
                }

                //vertical positions headers
                DrawBattlefieldCell(GameManager.BattlefieldCells[i, 0].VCoordinate, cellSize, cellPos);
                cellPos.X += cellSize.Width;

                //battlefield grid
                for (int j = 0; j < GameManager.BattlefieldSize.Height; j++)
                {
                    var cell = GameManager.BattlefieldCells[i, j];

                    var text = "";
                    if (cell.Revealed)
                        text = "*";

                    if (cell.ShipSquare != null)
                    {
                        //text = "O";
                        if (cell.ShipSquare.IsHit)
                            text = "X";
                    }

                    DrawBattlefieldCell(text, cellSize, cellPos);

                    cellPos.X += cellSize.Width;
                }

                cellPos.X = 0;
                cellPos.Y += cellSize.Height;
            }
        }

        private void DrawBattlefieldCell(string text, Size cellSize, Point cellPos)
        {
            panel1.Controls.Add(new Label
            {
                Text = text,
                Size = cellSize,
                Location = cellPos,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
            });
        }


    }
}
