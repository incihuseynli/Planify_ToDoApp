using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDoApp.Classes
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 600,
                Height = 300,
                FormBorderStyle = FormBorderStyle.None,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Color.FromArgb(34, 34, 34),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.White,
                //MaximizeBox = false,
                //MinimizeBox = false,
                //ShowInTaskbar = false
            };

            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Text = text,
                AutoSize = true,
                ForeColor = Color.White
            };

            TextBox inputBox = new TextBox()
            {
                Left = 50,
                Top = 50,
                Width = 450,
                Height = 100,
                Multiline = true,
                //ScrollBars = ScrollBars.Vertical
            };

            Button confirmation = new Button()
            {
                Text = "Ok",
                Left = 450,
                Width = 100,
                Height = 50,
                Top = 180,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            Button closeDialog = new Button()
            {
                BackgroundImage = Image.FromFile("C:\\BackEnd\\MyToDoApp\\MyToDoApp\\MyToDoApp\\assets\\X.png"),
                Left = 550,
                Width = 50,
                Top = 0,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                BackgroundImageLayout = ImageLayout.Center,
                BackColor = Color.FromArgb(34,34,34),

            };
            closeDialog.FlatAppearance.BorderSize = 0;
            closeDialog.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(closeDialog);

            prompt.AcceptButton = confirmation; 

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }

}
