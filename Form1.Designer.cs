
namespace ITE_Decoder_Tool
{
    public static class Globals
    {     
        public static Boolean copyButtonMode = true; // Modifiable
    }
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

        // Оказалось, что просто так прозрачную кнопку в формах сделать неполучится
        // Был выбор использовать Windows 8.1 layered button, или вот так:
        // https://stackoverflow.com/questions/1086621/drawing-a-transparent-button
        public class ImageButton : Control, IButtonControl
        {
            public ImageButton()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(ControlStyles.Opaque, true);
                SetStyle(ControlStyles.ResizeRedraw, true);
                this.BackColor = Color.Transparent;

            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                Graphics g = pevent.Graphics;
                Point p = new Point(0, 0);
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                if (Globals.copyButtonMode == true) { g.DrawImage((System.Drawing.Image)(global::ITE_Decoder_Tool.Properties.Resources.copy), p); }
                else g.DrawImage((System.Drawing.Image)(global::ITE_Decoder_Tool.Properties.Resources.download_button), p);
            }


            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                // ничего не делаем тут
            }

            void IButtonControl.NotifyDefault(bool value)
            {
                throw new NotImplementedException();
            }

            void IButtonControl.PerformClick()
            {
                throw new NotImplementedException();
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    const int WS_EX_TRANSPARENT = 0x20;
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_TRANSPARENT;
                    return cp;
                }
            }

            DialogResult IButtonControl.DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            // rest of class here...
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new ITE_Decoder_Tool.Form1.ImageButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkZip = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.base64ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пРОЕКТРаботаСActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кодировкаИАнализActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.созданиеТестовыхФайловToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализActionslaststateАгентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "ПРОЕКТ: Декодирование",
            "Декодирование actions/last-state агента",
            "ПРОЕКТ: Кодирование",
            "B64: Декодирование",
            "B64: Кодирование",
            "Создание тестового файла1",
            "Создание тестового файла2"});
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ПРОЕКТ: Декодирование",
            "Декодирование actions/last-state агента",
            "ПРОЕКТ: Кодирование",
            "B64: Декодирование",
            "B64: Кодирование",
            "Создание тестового файла1",
            "Создание тестового файла2"});
            this.comboBox1.Location = new System.Drawing.Point(12, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(400, 23);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выбрать тип задания:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 81);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 319);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.MaxLength = 2147483647;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Исходные данные";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(333, 319);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::ITE_Decoder_Tool.Properties.Resources.copy;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 70);
            this.button2.TabIndex = 4;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseLeave += new System.EventHandler(this.textBox2_MouseLeave);
            this.button2.MouseHover += new System.EventHandler(this.button2_MouseHover);
            // 
            // textBox2
            // 
            this.textBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.MaxLength = 2147483647;
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(463, 319);
            this.textBox2.TabIndex = 0;
            this.textBox2.MouseHover += new System.EventHandler(this.textBox2_MouseHover);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(418, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(387, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "Преобразовать!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkZip
            // 
            this.checkZip.AutoSize = true;
            this.checkZip.Enabled = false;
            this.checkZip.Location = new System.Drawing.Point(352, 30);
            this.checkZip.Name = "checkZip";
            this.checkZip.Size = new System.Drawing.Size(61, 19);
            this.checkZip.TabIndex = 5;
            this.checkZip.Text = "zipped";
            this.checkZip.UseVisualStyleBackColor = true;
            this.checkZip.Visible = false;
            this.checkZip.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.инструментыToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(821, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.base64ToolStripMenuItem,
            this.пРОЕКТРаботаСActionsToolStripMenuItem});
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // base64ToolStripMenuItem
            // 
            this.base64ToolStripMenuItem.Checked = true;
            this.base64ToolStripMenuItem.CheckOnClick = true;
            this.base64ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.base64ToolStripMenuItem.Name = "base64ToolStripMenuItem";
            this.base64ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.base64ToolStripMenuItem.Text = "Base64";
            this.base64ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.base64ToolStripMenuItem_CheckedChanged);
            // 
            // пРОЕКТРаботаСActionsToolStripMenuItem
            // 
            this.пРОЕКТРаботаСActionsToolStripMenuItem.Checked = true;
            this.пРОЕКТРаботаСActionsToolStripMenuItem.CheckOnClick = true;
            this.пРОЕКТРаботаСActionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.пРОЕКТРаботаСActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.кодировкаИАнализActionsToolStripMenuItem,
            this.созданиеТестовыхФайловToolStripMenuItem,
            this.анализActionslaststateАгентаToolStripMenuItem});
            this.пРОЕКТРаботаСActionsToolStripMenuItem.Name = "пРОЕКТРаботаСActionsToolStripMenuItem";
            this.пРОЕКТРаботаСActionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.пРОЕКТРаботаСActionsToolStripMenuItem.Text = "ПРОЕКТ";
            this.пРОЕКТРаботаСActionsToolStripMenuItem.Click += new System.EventHandler(this.пРОЕКТРаботаСActionsToolStripMenuItem_Click);
            // 
            // кодировкаИАнализActionsToolStripMenuItem
            // 
            this.кодировкаИАнализActionsToolStripMenuItem.Checked = true;
            this.кодировкаИАнализActionsToolStripMenuItem.CheckOnClick = true;
            this.кодировкаИАнализActionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.кодировкаИАнализActionsToolStripMenuItem.Name = "кодировкаИАнализActionsToolStripMenuItem";
            this.кодировкаИАнализActionsToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.кодировкаИАнализActionsToolStripMenuItem.Text = "Кодировка, декодирование сообщений";
            this.кодировкаИАнализActionsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.кодировкаИАнализActionsToolStripMenuItem_CheckedChanged);
            // 
            // созданиеТестовыхФайловToolStripMenuItem
            // 
            this.созданиеТестовыхФайловToolStripMenuItem.Checked = true;
            this.созданиеТестовыхФайловToolStripMenuItem.CheckOnClick = true;
            this.созданиеТестовыхФайловToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.созданиеТестовыхФайловToolStripMenuItem.Name = "созданиеТестовыхФайловToolStripMenuItem";
            this.созданиеТестовыхФайловToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.созданиеТестовыхФайловToolStripMenuItem.Text = "Создание тестовых файлов проекта";
            this.созданиеТестовыхФайловToolStripMenuItem.CheckedChanged += new System.EventHandler(this.созданиеТестовыхФайловToolStripMenuItem_CheckedChanged);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // анализActionslaststateАгентаToolStripMenuItem
            // 
            this.анализActionslaststateАгентаToolStripMenuItem.Checked = true;
            this.анализActionslaststateАгентаToolStripMenuItem.CheckOnClick = true;
            this.анализActionslaststateАгентаToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.анализActionslaststateАгентаToolStripMenuItem.Name = "анализActionslaststateАгентаToolStripMenuItem";
            this.анализActionslaststateАгентаToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.анализActionslaststateАгентаToolStripMenuItem.Text = "Анализ actions/last-state агента";
            this.анализActionslaststateАгентаToolStripMenuItem.CheckedChanged += new System.EventHandler(this.анализActionslaststateАгентаToolStripMenuItem_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 408);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkZip);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ITE-Decoder-Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private SplitContainer splitContainer1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private ImageButton button2;
        private CheckBox checkZip;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem инструментыToolStripMenuItem;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStripMenuItem base64ToolStripMenuItem;
        private ToolStripMenuItem пРОЕКТРаботаСActionsToolStripMenuItem;
        private ToolStripMenuItem кодировкаИАнализActionsToolStripMenuItem;
        private ToolStripMenuItem созданиеТестовыхФайловToolStripMenuItem;
        private ToolStripMenuItem анализActionslaststateАгентаToolStripMenuItem;
    }
}