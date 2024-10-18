using System;
using System.CodeDom;
using System.Numerics;

namespace FSM_AI
{
    public partial class Form1 : Form
    {

        string[,] map;
        int height, width;
        Player player;
        Enemy enemy;
        int moveCount;
        public Form1()
        {
            InitializeComponent();

            map = new string[,]
            {
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
            };
            //X = blocked, P = path, A = player attack, D = enemy attack
            height = map.GetLength(0);
            width = map.GetLength(1);
            moveCount = 0;

            generateWalls();

            player = initializePlayer();

            renderGridView();

            player_health_label.Text = "Your Health: " + player.health;
            enemy_health_label.Text = "Enemy Health:";
        }

        private Player initializePlayer()
        {
            Random rand = new Random();
            Coord initialCoords;
            do
            {
                initialCoords = new Coord(
                    rand.Next(width), rand.Next(height)
                );
            } while (map[initialCoords.y, initialCoords.x] == "X");

            return new Player(initialCoords, 5);
        }
        private Enemy initializeEnemy()
        {
            Random rand = new Random();
            Coord initialCoords;
            do
            {
                initialCoords = new Coord(
                    rand.Next(width), rand.Next(height)
                );
            } while (map[initialCoords.y, initialCoords.x] == "X");

            return new Enemy(initialCoords, 5);
        }

        private void generateWalls()
        {
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                Coord curr = new Coord(
                    rand.Next(width - 4), rand.Next(height - 4)
                );
                int axis;
                axis = rand.Next(2);

                int count = 0;
                while (curr.x < width && curr.y < height && count <= 3)
                {
                    map[curr.y, curr.x] = "X";
                    if (axis == 0)
                    {
                        curr.x++;
                    }
                    else
                    {
                        curr.y++;
                    }
                    count++;
                }
            }

        }

        private void renderGridView()
        {
            int height = map.GetLength(0);
            int width = map.GetLength(1);

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;

            dataGridView1.ColumnCount = width;

            for (int r = 0; r < height; r++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);

                for (int c = 0; c < width; c++)
                {
                    if (map[r, c] == "X")
                    {
                        row.Cells[c].Style.BackColor = Color.Black;
                    }
                    else if (player.coord.y == r && player.coord.x == c)
                    {
                        row.Cells[c].Style.BackColor = Color.Green;
                    }
                    else if (enemy != null && enemy.coord.y == r && enemy.coord.x == c)
                    {
                        row.Cells[c].Style.BackColor = Color.Red;
                    }
                    else if (map[r, c].Contains('A'))
                    {
                        row.Cells[c].Style.BackColor = Color.Blue;
                    }
                    else if (map[r, c].Contains('D'))
                    {
                        row.Cells[c].Style.BackColor = Color.DeepPink;
                    }
                    else if (map[r, c].Contains('P'))
                    {
                        row.Cells[c].Style.BackColor = Color.Pink;
                    }
                    else
                    {
                        row.Cells[c].Style.BackColor = Color.PeachPuff;
                    }
                }

                dataGridView1.Rows.Add(row);
            }
        }

        private void cleanMap()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = map[i, j].IndexOf('P');
                    map[i, j] = (index < 0) ? map[i, j] : map[i, j].Remove(index, 1);

                    index = map[i, j].IndexOf('A');
                    map[i, j] = (index < 0) ? map[i, j] : map[i, j].Remove(index, 1);

                    index = map[i, j].IndexOf('D');
                    map[i, j] = (index < 0) ? map[i, j] : map[i, j].Remove(index, 1);
                }
            }
        }
        private void up_button_Click(object sender, EventArgs e)
        {
            playerMove(1);
        }

        private void right_button_Click(object sender, EventArgs e)
        {
            playerMove(2);
        }

        private void down_button_Click(object sender, EventArgs e)
        {
            playerMove(3);
        }
        private void left_button_Click(object sender, EventArgs e)
        {
            playerMove(4);
        }

        private void playerMove(int move)
        {
            if (player.health > 0)
            {
                player.move(map, move);
                updateGame();
            }
            else
            {
                gameover_label.Text = "You lost! Game over :(";
            }

        }

        private void updateGame()
        {
            moveCount++;

            if (moveCount == 3)
            {
                enemy = initializeEnemy();
            }

            if (enemy != null)
            {
                state_label.Text = enemy.getState();
                enemy.makeMove(map, player);
                enemy_health_label.Text = "Enemy Health: " + enemy.health;
            }
            player_health_label.Text = "Your Health: " + player.health;

            renderGridView();
            cleanMap();
        }

        private void attack_button_Click(object sender, EventArgs e)
        {
            player.attack(map, enemy);
            updateGame();
        }


        private void state_label_TextChanged(object sender, EventArgs e)
        {
            if (enemy != null)
            {
                state_label.Text = enemy.getState();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            map = new string[,]
            {
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
                {"", "", "", "", "", "", "", "", "", ""},
            };
            moveCount = 0;
            generateWalls();
            enemy = null;
            player = initializePlayer();
            renderGridView();

            state_label.Text = "Not yet spawned";
            gameover_label.Text = "";
            player_health_label.Text = "Your Health: " + player.health;
            enemy_health_label.Text = "Enemy Health:";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


    struct Coord {
        public int x; public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Entity
    {
        public int health;
        public Coord coord;
        public Entity(Coord coord, int health)
        {
            this.coord = coord;
            this.health = health;
        }

        public void move(string[,] map, int move)
        {
            switch (move)
            {
                case 0:
                    break;
                case 1:
                    if (coord.y - 1 >= 0 && map[coord.y - 1, coord.x] != "X")
                    {
                        coord.y--;
                    }
                    break;
                case 2:
                    if (coord.x + 1 <= map.GetLength(1) - 1 && map[coord.y, coord.x + 1] != "X")
                    {
                        coord.x++;
                    }
                    break;
                case 3:
                    if (coord.y + 1 <= map.GetLength(0) - 1 && map[coord.y + 1, coord.x] != "X")
                    {
                        coord.y++;
                    }
                    break;
                case 4:
                    if (coord.x - 1 >= 0 && map[coord.y, coord.x - 1] != "X")
                    {
                        coord.x--;
                    }
                    break;
            }
        }
        public void takeDamage()
        {
            health = Math.Max(health - 1, 0);
        }

        public int detectEntity(string[,] map, Entity entity, int attackRangeCost)
        {//0 - detected nothing, 1 - detected entity, 2 - entity in range for attack
            int xDir = ((entity.coord.x > coord.x) ? 1 : -1);
            int yDir = ((entity.coord.y > coord.y) ? 1 : -1);

            Coord curr = new Coord(coord.x, coord.y);

            int cost = 0;
            while (cost < 8
            )
            {
                if (entity.coord.x == curr.x)
                {
                    xDir = 0;
                }
                if (entity.coord.y == curr.y)
                {
                    yDir = 0;
                }

                curr.x += xDir;
                curr.y += yDir;
                cost += (Math.Abs(xDir) + Math.Abs(yDir));
                if (map[curr.y, curr.x].Contains('X')){
                    break;
                }

                if (entity.coord.x == curr.x && entity.coord.y == curr.y)
                {
                    if (cost <= attackRangeCost)
                    {
                        System.Diagnostics.Debug.WriteLine("Cost: " + cost + " out of " + attackRangeCost);
                        return 2;
                    }
                    return 1;
                }
            }

            return 0;
        }
    }

    class Player : Entity
    {
        public Player(Coord coord, int health) : base(coord, health)
        {
        }

        public void attack(string[,] map, Enemy enemy)
        {
            for (int i = 1; i < 4; i++)
            {
                
                //Right
                if (coord.x + i < map.GetLength(1))
                {
                    map[coord.y, coord.x + i] += "A";
                }
                addSides(map, i, new Coord(coord.x + i, coord.y), -1, 1);


                //Down
                if (coord.y + i < map.GetLength(0))
                {
                    map[coord.y + i, coord.x] += "A";
                }
                addSides(map, i, new Coord(coord.x, coord.y + i), -1, -1);

                //Left
                if (coord.x - i >= 0)
                {
                    map[coord.y, coord.x - i] += "A";
                }
                addSides(map, i, new Coord(coord.x - i, coord.y), 1, -1);

                //Up
                if (coord.y - i >= 0)
                {
                    map[coord.y - i, coord.x] += "A";
                }
                addSides(map, i, new Coord(coord.x, coord.y - i), 1, 1);

            }

            if (detectEntity(map, enemy, 3) == 2)
            {
                enemy.takeDamage();
            }
        }

        private void addSides(string[,] map, int i, Coord curr ,int xDir, int yDir)
        {
            int loop = i - 1;
            for (int j = 0; j < loop; j++)
            {
                bool toSkip = false;
                curr.x += xDir;
                if (curr.x >= map.GetLength(1) || curr.x < 0) toSkip = true;
                curr.y += yDir;
                if (curr.y >= map.GetLength(0) || curr.y < 0 || toSkip) continue;
                map[curr.y, curr.x] += "A";
            }
        }

    }

    class Enemy : Entity
    {

        int state;
        int[,] table = new int[,]
        {
            {1, 1, 1, 4},
            {1, 2, 2, 4},
            {1, 2, 3, 4},
            {2, 2, 3, 4},
            {4, 4, 4, 4}
        };
        public Enemy(Coord coord, int health) : base(coord, health)
        {
            state = 0;
        }

        public string getState()
        {
            switch (state)
            {
                case 0:
                    return "Spawned";
                case 1:
                    return "Idle";
                case 2:
                    return "Pursuing";
                case 3:
                    return "Attacking";
                case 4:
                    return "Dead";
                default:
                    return "IDK";
            }
        }

        public void makeMove(string[,] map, Player player)
        {
            Random rand = new Random();

            switch (state)
            {
                case 1:
                    move(map, rand.Next(5));
                    //drawLineOfSight(map, player);
                    break;
                case 2:
                    int moveRandom = rand.Next(3);
                    if (moveRandom == 0)
                    {
                        if (player.coord.x > coord.x
                        && !map[coord.y, coord.x + 1].Contains('X')) coord.x += 1;
                        else if (player.coord.x < coord.x
                            && !map[coord.y, coord.x - 1].Contains('X')) coord.x -= 1;
                        else if (player.coord.y > coord.y
                            && !map[coord.y + 1, coord.x].Contains('X')) coord.y += 1;
                        else if (player.coord.y < coord.y
                            && !map[coord.y - 1, coord.x].Contains('X')) coord.y -= 1;
                    }
                    else if (moveRandom == 1)
                    {
                        if (player.coord.y > coord.y
                            && !map[coord.y + 1, coord.x].Contains('X')) coord.y += 1;
                        else if (player.coord.y < coord.y
                            && !map[coord.y - 1, coord.x].Contains('X')) coord.y -= 1;
                        else if (player.coord.x > coord.x
                        && !map[coord.y, coord.x + 1].Contains('X')) coord.x += 1;
                        else if (player.coord.x < coord.x
                            && !map[coord.y, coord.x - 1].Contains('X')) coord.x -= 1;
                    }
                    
                    break;
                case 3:
                    attack(map, player);
                    break;
            }
            int input = health <= 0 ? 3 : detectEntity(map, player, 2);
            System.Diagnostics.Debug.WriteLine("State:" + getState() + " " + input);
            state = table[state, input];
        }

        public void drawLineOfSight(string[,] map, Player player)
        {
            int xDir = ((player.coord.x > coord.x) ? 1 : -1);
            int yDir = ((player.coord.y > coord.y) ? 1 : -1);

            Coord curr = new Coord(coord.x, coord.y);

            while ((player.coord.x != curr.x || player.coord.y != curr.y) && 
                !(curr.x >= map.GetLength(1) && curr.y > map.GetLength(0) && curr.x < 0 && curr.y < 0)
            )
            {
                map[curr.y, curr.x] += "P";

                if (player.coord.x == curr.x)
                {
                    xDir = 0;
                }
                if (player.coord.y == curr.y)
                {
                    yDir = 0;
                }

                curr.x += xDir;
                curr.y += yDir;
            }

        }

        public void attack(string[,] map, Player player)
        {
            for (int i = 1; i < 3; i++)
            {

                //Right
                if (coord.x + i < map.GetLength(1))
                {
                    map[coord.y, coord.x + i] += "D";
                }
                addSides(map, i, new Coord(coord.x + i, coord.y), -1, 1);

                //Down
                if (coord.y + i < map.GetLength(0))
                {
                    map[coord.y + i, coord.x] += "D";
                }
                addSides(map, i, new Coord(coord.x, coord.y + i), -1, -1);

                //Left
                if (coord.x - i >= 0)
                {
                    map[coord.y, coord.x - i] += "D";
                }
                addSides(map, i, new Coord(coord.x - i, coord.y), 1, -1);

                //Up
                if (coord.y - i >= 0)
                {
                    map[coord.y - i, coord.x] += "D";
                }
                addSides(map, i, new Coord(coord.x, coord.y - i), 1, 1);

            }

            if (detectEntity(map, player, 2) == 2)
            {
                player.takeDamage();
            }
        }

        private void addSides(string[,] map, int i, Coord curr, int xDir, int yDir)
        {
            int loop = i - 1;
            for (int j = 0; j < loop; j++)
            {
                bool toSkip = false;
                curr.x += xDir;
                if (curr.x >= map.GetLength(1) || curr.x < 0) toSkip = true;
                curr.y += yDir;
                if (curr.y >= map.GetLength(0) || curr.y < 0 || toSkip) continue;
                map[curr.y, curr.x] += "D";
            }
        }
    }
}
