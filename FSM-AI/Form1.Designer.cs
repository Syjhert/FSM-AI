namespace FSM_AI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            up_button = new Button();
            left_button = new Button();
            right_button = new Button();
            down_button = new Button();
            attack_button = new Button();
            label1 = new Label();
            state_label = new Label();
            gameover_label = new Label();
            reset_button = new Button();
            player_health_label = new Label();
            enemy_health_label = new Label();
            LoS_checkbox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 16);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(969, 518);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // up_button
            // 
            up_button.Location = new Point(1051, 91);
            up_button.Name = "up_button";
            up_button.Size = new Size(94, 29);
            up_button.TabIndex = 1;
            up_button.Text = "Up";
            up_button.UseVisualStyleBackColor = true;
            up_button.Click += up_button_Click;
            // 
            // left_button
            // 
            left_button.Location = new Point(1000, 126);
            left_button.Name = "left_button";
            left_button.Size = new Size(94, 29);
            left_button.TabIndex = 2;
            left_button.Text = "Left";
            left_button.UseVisualStyleBackColor = true;
            left_button.Click += left_button_Click;
            // 
            // right_button
            // 
            right_button.Location = new Point(1100, 126);
            right_button.Name = "right_button";
            right_button.Size = new Size(94, 29);
            right_button.TabIndex = 3;
            right_button.Text = "Right";
            right_button.UseVisualStyleBackColor = true;
            right_button.Click += right_button_Click;
            // 
            // down_button
            // 
            down_button.Location = new Point(1051, 161);
            down_button.Name = "down_button";
            down_button.Size = new Size(94, 29);
            down_button.TabIndex = 4;
            down_button.Text = "Down";
            down_button.UseVisualStyleBackColor = true;
            down_button.Click += down_button_Click;
            // 
            // attack_button
            // 
            attack_button.Location = new Point(1051, 210);
            attack_button.Name = "attack_button";
            attack_button.Size = new Size(94, 29);
            attack_button.TabIndex = 5;
            attack_button.Text = "Attack";
            attack_button.UseVisualStyleBackColor = true;
            attack_button.Click += attack_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1051, 380);
            label1.Name = "label1";
            label1.Size = new Size(94, 20);
            label1.TabIndex = 6;
            label1.Text = "Enemy State:";
            // 
            // state_label
            // 
            state_label.AutoSize = true;
            state_label.Location = new Point(1044, 409);
            state_label.Name = "state_label";
            state_label.Size = new Size(121, 20);
            state_label.TabIndex = 7;
            state_label.Text = "Not yet spawned";
            state_label.TextChanged += state_label_TextChanged;
            // 
            // gameover_label
            // 
            gameover_label.AutoSize = true;
            gameover_label.Location = new Point(1023, 255);
            gameover_label.Name = "gameover_label";
            gameover_label.Size = new Size(0, 20);
            gameover_label.TabIndex = 8;
            // 
            // reset_button
            // 
            reset_button.Location = new Point(1051, 462);
            reset_button.Name = "reset_button";
            reset_button.Size = new Size(94, 29);
            reset_button.TabIndex = 9;
            reset_button.Text = "Reset";
            reset_button.UseVisualStyleBackColor = true;
            reset_button.Click += reset_button_Click;
            // 
            // player_health_label
            // 
            player_health_label.AutoSize = true;
            player_health_label.Location = new Point(1044, 29);
            player_health_label.Name = "player_health_label";
            player_health_label.Size = new Size(89, 20);
            player_health_label.TabIndex = 10;
            player_health_label.Text = "Your Health:";
            player_health_label.Click += label2_Click;
            // 
            // enemy_health_label
            // 
            enemy_health_label.AutoSize = true;
            enemy_health_label.Location = new Point(1044, 312);
            enemy_health_label.Name = "enemy_health_label";
            enemy_health_label.Size = new Size(104, 20);
            enemy_health_label.TabIndex = 11;
            enemy_health_label.Text = "Enemy Health:";
            enemy_health_label.Click += label3_Click;
            // 
            // LoS_checkbox
            // 
            LoS_checkbox.AutoSize = true;
            LoS_checkbox.Location = new Point(16, 540);
            LoS_checkbox.Name = "LoS_checkbox";
            LoS_checkbox.Size = new Size(154, 24);
            LoS_checkbox.TabIndex = 12;
            LoS_checkbox.Text = "Show Line of Sight";
            LoS_checkbox.UseVisualStyleBackColor = true;
            LoS_checkbox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1204, 566);
            Controls.Add(LoS_checkbox);
            Controls.Add(enemy_health_label);
            Controls.Add(player_health_label);
            Controls.Add(reset_button);
            Controls.Add(gameover_label);
            Controls.Add(state_label);
            Controls.Add(label1);
            Controls.Add(attack_button);
            Controls.Add(down_button);
            Controls.Add(right_button);
            Controls.Add(left_button);
            Controls.Add(up_button);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button up_button;
        private Button left_button;
        private Button right_button;
        private Button down_button;
        private Button attack_button;
        private Label label1;
        private Label state_label;
        public Label gameover_label;
        private Button reset_button;
        private Label player_health_label;
        private Label enemy_health_label;
        private CheckBox LoS_checkbox;
    }
}
