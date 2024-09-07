using MyToDoApp.Classes;
using System.Windows.Forms;

namespace MyToDoApp
{
    public partial class Form1 : Form
    {
        private DbManager dbManager;
        public Form1()
        {
            InitializeComponent();
            dbManager = new DbManager();

        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Show All Todos
        private async void button1_Click(object sender, EventArgs e)
        {
            // Clear the panel before adding new items
            panelTodos.Controls.Clear();

            
            // Get all todos from the database
            var todos = dbManager.ShowTodos();

            int yOffset = 10; // Vertical spacing between items
           
            foreach (var todo in todos)
            {
                // Create a panel to hold the todo item
                var todoPanel = new Panel
                {
                    Size = new Size(panelTodos.Width - 20, 50),
                    Location = new Point(10, yOffset),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Label for ID
                var lblId = new Label
                {
                    Text = todo.Id.ToString(),
                    Location = new Point(10, 15),
                    Width = 30,
                    Font = new Font("Arial", 12, FontStyle.Bold)
                };

                // Label for description
                var lblDesc = new Label
                {
                    Text = todo.Description,
                    Location = new Point(50, 15),
                    Width = 400,
                    Font = new Font("Arial", 12, FontStyle.Bold)
                };

                #region Button for changing state
                string progressImg = @"C:\BackEnd\MyToDoApp\MyToDoApp\MyToDoApp\assets\progress.png";
                string doneImg = @"C:\BackEnd\MyToDoApp\MyToDoApp\MyToDoApp\assets\tick.png";
                //HttpClient cl = new HttpClient();
                /*
                 var stream = await cl.GetStreamAsync("");
                 */
                var btnChangeState = new Button
                {
                    Location = new Point(610, 13),
                    BackgroundImage = todo.isCompleted ? Image.FromFile(doneImg) : Image.FromFile(progressImg),
                    //Text = todo.isCompleted ? "Done" : "In progress",
                    BackgroundImageLayout = ImageLayout.Center,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(34, 34, 34),
                    Text = "",
                    Size = new Size(46, 35),
                    Margin = new Padding(3, 3, 3, 3),

                };

                btnChangeState.FlatAppearance.BorderColor = Color.FromArgb(34, 34, 34);
                btnChangeState.FlatAppearance.BorderSize = 0;
                btnChangeState.Click += (sender, e) =>
                {
                    todo.isCompleted = !todo.isCompleted;
                    dbManager.UpdateState(todo.Id, todo.isCompleted ? 1 : 0);
                    btnChangeState.BackgroundImage = todo.isCompleted
                    ? Image.FromFile(doneImg)
                    : Image.FromFile(progressImg);
                };
                #endregion
                #region Button for editing
                string editImg = "C:\\BackEnd\\MyToDoApp\\MyToDoApp\\MyToDoApp\\assets\\edit.png";
                var btnEdit = new Button
                {
                    Location = new Point(680, 13),
                    BackgroundImage = Image.FromFile(editImg),
                    BackgroundImageLayout = ImageLayout.Center,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(34, 34, 34),
                    //Text = "Edit",
                    Size = new Size(46, 35),
                    Margin = new Padding(3, 3, 3, 3),
                };

                btnEdit.FlatAppearance.BorderColor = Color.FromArgb(34, 34, 34);
                btnEdit.FlatAppearance.BorderSize = 0;

                btnEdit.Click += (sender, e) =>
                {
                    string newContent = Prompt.ShowDialog("Enter new content:", "Edit Todo");
                    if (!string.IsNullOrEmpty(newContent))
                    {
                        todo.Description = newContent;

                        bool isUpdated = dbManager.UpdateTodo(todo.Id, newContent);
                        if (isUpdated)
                        {
                            lblDesc.Text = newContent;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the todo item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                #endregion
                #region Button for deleting
                var btnDelete = new Button
                {
                    Text = "Delete",
                    Location = new Point(720, 16),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(34, 34, 34),
                    Size = new Size(80, 35),
                    Margin = new Padding(3, 3, 3, 3),
                    Padding = new Padding(0, 0, 0, 6)

                };

                btnDelete.FlatAppearance.BorderColor = Color.FromArgb(34, 34, 34);
                btnDelete.FlatAppearance.BorderSize = 0;

                btnDelete.Click += (sender, e) =>
                {
                    bool isDeleted = dbManager.DeleteTodo(todo.Id);
                    if (isDeleted)
                    {
                        var todoPanel = (Panel)((Button)sender).Parent;

                        if (todoPanel != null && todoPanel.Parent != null)
                        {
                            todoPanel.Parent.Controls.Remove(todoPanel);
                            todoPanel.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the todo item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                #endregion

                #region Adding All Elements Into Panel
                // Add controls to the todo panel
                todoPanel.Controls.Add(lblId);
                todoPanel.Controls.Add(lblDesc);
                todoPanel.Controls.Add(btnChangeState);
                todoPanel.Controls.Add(btnEdit);
                todoPanel.Controls.Add(btnDelete);

                // Add todo panel to the main panel
                panelTodos.Controls.Add(todoPanel);
                //panelTodos.Parent.AutoScroll = true;
                #endregion

                // Adjust vertical spacing for the next todo item
                yOffset += 60;
            }
        }

        #endregion
        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        #region Add New Todo
        private void button3_Click(object sender, EventArgs e)
        {
            string newTodo = Prompt.ShowDialog("Enter Your Todo:", "Add Todo");
            if (!string.IsNullOrEmpty(newTodo))
            {

                var todo = new Todos
                {
                    Description = newTodo,
                    isCompleted = false,
                    CreatedDate = DateTime.Now
                };
                bool isAdded = dbManager.AddTodos(todo);
                if (isAdded)
                {
                    // DOESN'T WORK!!!!!
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("Failed to add the todo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to add the todo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Reset Table
        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete all todos?",
                                 "CAUTION! Confirm Deletion",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                bool isDeleted = dbManager.ResetTable();

                if (isDeleted)
                {
                    // Clear the panel 
                    panelTodos.Controls.Clear();
                    MessageBox.Show("All todos have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to delete todos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
