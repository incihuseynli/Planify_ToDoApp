using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Classes
{
    public class DbManager
    {
        // Connection string to connect db we created and c# program
        private string _connStr = "Server=.;Database=todoExampleDb;Integrated Security=true";

        // VARIABLE FOR NEW SQL CONNECTION
        private SqlConnection _conn;

        public DbManager()
        {
            _conn = new SqlConnection(_connStr);
        }

        // Method To Show Todos
        #region Show Method
        public List<Todos> ShowTodos()
        {
            // Open and close( in the last code line) connection first
            _conn.Open();

            // Query command for what we want from  db

            //var query = "Select * from todos";  
            var query = "Select TOP 6 * from todos\r\norder by CreatedDate desc";

            // To send query as command to db
            var cmd = new SqlCommand(query, _conn);

            // To execute command for SELECT type queries
            var reader = cmd.ExecuteReader();

            // List for todos
            var todos = new List<Todos>();


            while (reader.Read())
            {
                var id = Convert.ToInt32(reader["Id"]);
                var desc = reader["Description"].ToString();
                var isCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                var createdDate = Convert.ToDateTime(reader["CreatedDate"]);

                todos.Add(new Todos
                {
                    Id = id,
                    Description = desc,
                    isCompleted = isCompleted,
                    CreatedDate = createdDate
                });
            }

            // Close reader always
            reader.Close();

            // To close connection with db
            _conn.Close();

            return todos;
        }

        #endregion

        // Method to add todos
        #region Add Method
        public bool AddTodos(Todos todo)
        {
            _conn.Open();
            var completed = todo.isCompleted ? 1 : 0;
            var query = $"insert into todos values ('{todo.Description}',{completed},GETDATE())";

            var cmd = new SqlCommand(query, _conn);
            var rows = cmd.ExecuteNonQuery();

            _conn.Close();

            return rows > 0;
        }

        #endregion

        #region Changing Content of Todo
        public bool UpdateTodo(int id, string desc)
        {
            _conn.Open();
            var query = $"exec UpdateDescOfTodo {id} , '{desc}'";
            var cmd = new SqlCommand(query, _conn);

            var rows = cmd.ExecuteNonQuery();

            _conn.Close();

            return rows > 0;
        }
        #endregion

        #region Changing State of Todo
        public bool UpdateState(int id, int state)
        {
            _conn.Open();

            var query = $"exec UpdateStateOfTodo {id} , {state}";
            var cmd = new SqlCommand(query, _conn);
            var rows = cmd.ExecuteNonQuery();


            _conn.Close();

            return rows > 0;
        }

        #endregion

        #region Deleting Todo

        public bool DeleteTodo(int id)
        {
            _conn.Open();
            var query = $"exec DeleteTodo {id}";
            var cmd = new SqlCommand(query, _conn);

            var rows = cmd.ExecuteNonQuery();

            _conn.Close();

            return rows > 0;
        }

        #endregion


        #region Deleting All Todos / RESET OF TABLE

        public bool ResetTable()
        {
            _conn.Open();

            var query = "truncate table todos";
            var cmd = new SqlCommand(query, _conn);
            var rows = cmd.ExecuteNonQuery();

            _conn.Close();

            return true;
        }

        #endregion
    }
}
