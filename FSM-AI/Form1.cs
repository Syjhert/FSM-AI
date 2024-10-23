using System;
using System.CodeDom;
using System.Numerics;
using static FSM_AI.Entity;

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
            initializeGame();
        }

        private void initializeGame()
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
            //X = blocked, P = path, A = player attack, D = enemy attack
            height = map.GetLength(0);
            width = map.GetLength(1);
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

            return new Enemy(initialCoords, 5, LoS_checkbox.Checked);
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
            updateGame(Player.Action.Up);
        }

        private void right_button_Click(object sender, EventArgs e)
        {
            updateGame(Player.Action.Right);
        }

        private void down_button_Click(object sender, EventArgs e)
        {
            updateGame(Player.Action.Down);
        }
        private void left_button_Click(object sender, EventArgs e)
        {
            updateGame(Player.Action.Left);
        }

        private void updateGame(Player.Action action)
        {
            moveCount++;

            if (moveCount == 3)
            {
                enemy = initializeEnemy();
            }

            if (enemy != null)
            {
                enemy.makeAction(map, player);
                state_label.Text = enemy.getState();
            }
            if (player.health > 0)
                player.makeAction(map, action);
            else
                gameover_label.Text = "You lost! Game over :(";

            // Separated take damage logic (afterMove) after both entities moved for better sense of damaging
            if (enemy != null)
                enemy.afterMove(map, player);
            player.afterMove(map, enemy);

            if (enemy != null)
                enemy_health_label.Text = "Enemy Health: " + enemy.health;

            player_health_label.Text = "Your Health: " + player.health;


            renderGridView();
            cleanMap();
        }

        private void attack_button_Click(object sender, EventArgs e)
        {
            updateGame(Player.Action.Attack);
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
            initializeGame();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ( enemy != null)
            {
                enemy.showLineOfSight = LoS_checkbox.Checked;
                enemy.drawLineOfSight(map, player);
            }
            renderGridView();
            cleanMap();
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
        public enum Action
        {
            Nothing,
            Up,
            Right,
            Down,
            Left,
            Attack
        }
        public Entity(Coord coord, int health)
        {
            this.coord = coord;
            this.health = health;
        }

        public void move(string[,] map, Action action)
        {
            //if the chosen move (by player or random generated by enemy is a wall, they stay in place
            switch (action)
            {
                case Action.Nothing:
                    break;
                case Action.Up:
                    if (coord.y - 1 >= 0 && !map[coord.y - 1, coord.x].Contains('X'))
                    {
                        coord.y--;
                    }
                    break;
                case Action.Right:
                    if (coord.x + 1 <= map.GetLength(1) - 1 && !map[coord.y, coord.x + 1].Contains('X'))
                    {
                        coord.x++;
                    }
                    break;
                case Action.Down:
                    if (coord.y + 1 <= map.GetLength(0) - 1 && !map[coord.y + 1, coord.x].Contains('X'))
                    {
                        coord.y++;
                    }
                    break;
                case Action.Left:
                    if (coord.x - 1 >= 0 && !map[coord.y, coord.x - 1].Contains('X'))
                    {
                        coord.x--;
                    }
                    break;
                case Action.Attack:
                    System.Diagnostics.Debug.WriteLine("Attack Action should not be in the move() method");
                    break;
            }
        }
        public void takeDamage()
        {
            health = Math.Max(health - 1, 0);
        }

        public int detectEntity(string[,] map, Entity entity, int attackRangeCost)
        {// returns:
         // 0 - detected nothing, 1 - detected entity, 2 - entity in range for attack
         // all return states is used by Enemy
         // Player only use 2 - entity in range for attack for attack damage check

            int xDir = ((entity.coord.x > coord.x) ? 1 : -1);
            int yDir = ((entity.coord.y > coord.y) ? 1 : -1);

            Coord curr = new Coord(coord.x, coord.y);

            int attackRange = 0;
            int detectRange = 0;
            while (detectRange < 8
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
                attackRange += (Math.Abs(xDir) + Math.Abs(yDir));
                if (map[curr.y, curr.x].Contains('X')){
                    break;
                }

                if (entity.coord.x == curr.x && entity.coord.y == curr.y)
                {
                    if (attackRange <= attackRangeCost)
                    {
                        //System.Diagnostics.Debug.WriteLine("AttackRange: " + attackRange + " out of " + attackRangeCost);
                        return 2;
                    }
                    return 1;
                }
                detectRange++;
            }

            return 0;
        }
    }

    class Player : Entity
    {
        public bool justAttacked;
        public Player(Coord coord, int health) : base(coord, health)
        {
            justAttacked = false;
        }

        public void makeAction(string[,] map, Action action)
        {
            if (action == Action.Attack)
            {
                attack(map);
            }
            else
            {
                move(map, action);
            }
        }

        public void afterMove(string[,] map, Enemy enemy)
        {
            if (enemy != null && detectEntity(map, enemy, 3) == 2 && justAttacked)
            {
                justAttacked = false;
                enemy.takeDamage();
            }
        }

        public void attack(string[,] map)
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

            justAttacked = true;
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
        enum State
        {
            Spawned,
            Idle,
            Pursuing,
            Attacking,
            Dead
        };
        State state;
        State[,] table = new State[,]
        {
            {State.Idle,        State.Idle,         State.Idle,         State.Dead},
            {State.Idle,        State.Pursuing,     State.Pursuing,     State.Dead},
            {State.Idle,        State.Pursuing,     State.Attacking,    State.Dead},
            {State.Pursuing,    State.Pursuing,     State.Attacking,    State.Dead},
            {State.Dead,        State.Dead,         State.Dead,         State.Dead}
        };
        public bool showLineOfSight;
        public Enemy(Coord coord, int health, bool showLineofSight) : base(coord, health)
        {
            state = State.Spawned;
            this.showLineOfSight = showLineofSight;
        }

        public string getState()
        {
            switch (state)
            {
                case State.Spawned:
                    return "Spawned";
                case State.Idle:
                    return "Idle";
                case State.Pursuing:
                    return "Pursuing";
                case State.Attacking:
                    return "Attacking";
                case State.Dead:
                    return "Dead";
                default:
                    return "IDK";
            }
        }

        public void makeAction(string[,] map, Player player)
        {
            Random rand = new Random();

            changeState(map, player);

            switch (state)
            {
                case State.Idle:
                    move(map, (Action) rand.Next(5));
                    break;
                case State.Pursuing:
                    int moveRandom = rand.Next(3);
                    if (moveRandom == 0)
                    {
                        //move in x-axis first
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
                        //move in y-axis first
                        if (player.coord.y > coord.y
                            && !map[coord.y + 1, coord.x].Contains('X')) coord.y += 1;
                        else if (player.coord.y < coord.y
                            && !map[coord.y - 1, coord.x].Contains('X')) coord.y -= 1;
                        else if (player.coord.x > coord.x
                        && !map[coord.y, coord.x + 1].Contains('X')) coord.x += 1;
                        else if (player.coord.x < coord.x
                            && !map[coord.y, coord.x - 1].Contains('X')) coord.x -= 1;
                    }
                    //else not move (stumble) to give player chances to get away
                    break;
                case State.Attacking:
                    attack(map, player);
                    break;
            }
        }
        public void afterMove(string[,] map, Player player)
        {
            if (detectEntity(map, player, 2) == 2 && state == State.Attacking)
            {
                player.takeDamage();
            }

            drawLineOfSight(map, player);

        }
        public void changeState(string[,] map, Player player)
        {
            int input = health <= 0 ? 3 : detectEntity(map, player, 2);
            //System.Diagnostics.Debug.WriteLine("State:" + getState() + "(" + (int)state + ") " + input);
            state = table[(int)state, input];
        }

        public void drawLineOfSight(string[,] map, Player player)
        {
            // idk if this is an algorithm
            // goes diagonal first then if same x or y axis, go straight
            if (!showLineOfSight) return;

            int xDir = ((player.coord.x > coord.x) ? 1 : -1);
            int yDir = ((player.coord.y > coord.y) ? 1 : -1);

            Coord curr = new Coord(coord.x, coord.y);
            int range = 8;
            int i = 0;
            while ((player.coord.x != curr.x || player.coord.y != curr.y) && i < range)
            {
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
                if (map[curr.y, curr.x].Contains('X'))
                {
                    break;
                }
                map[curr.y, curr.x] += "P";
                i++;
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
