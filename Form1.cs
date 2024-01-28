using System.Diagnostics;
using System.Text;

namespace ITE_Decoder_Tool
{
    public partial class Form1 : Form
    {
        Guid uuidS;
        Boolean copyButtonTouched = false;
        Boolean menuMutualBlocking = false;
        string workDir = @Path.GetDirectoryName(Application.ExecutablePath);

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            try {
                Config? config = ConfigOperations.readConfig(@"ITE-Decoder-Tool.runtimeconfig.json");
                созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked = (bool)(config?.SakuraLicensing);
                кодировкаИАнализActionsToolStripMenuItem.Checked = (bool)(config?.SakuraDecoding);
                анализActionslaststateАгентаToolStripMenuItem.Checked = (bool)(config?.SakuraActions);
                base64ToolStripMenuItem.Checked = (bool)(config?.Base64);
                updateFeatureCheckedStatus();
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Ошибка чтения конфига."); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = textBox2.BackColor;
            textBox2.ForeColor = Color.Black;
            checkZip.Visible = false;
            checkZip.Enabled = false;
            label1.Text = "Выбрать тип задания:";
            switch (comboBox1.SelectedItem)
            {
                case "САКУРА: Декодирование":
                    checkZip.Visible = true;
                    textBox2.Text = iteDecryptZipf(textBox1.Text);
                    break;
                case "САКУРА: Кодирование":
                    checkZip.Visible = true;
                    textBox2.Text = iteEncryptZip(textBox1.Text);
                    break;
                case "B64: Декодирование":
                    decryptB64(textBox1.Text);
                    break;
                case "B64: Кодирование":
                    encryptB64(textBox1.Text);
                    break;
                case "Создание лицензий для РМ":
                    textBox2.Text = createLicense(textBox1.Text);
                    break;
                case "Создание файла инициализации сервера":
                    textBox2.Text = createIni(textBox1.Text);
                    break;
                case "Декодирование actions/last-state агента":
                    checkZip.Visible = true;
                    textBox2.Text = decodeAction(textBox1.Text);
                    break;
                //default:
                    //Instructions);
                    //break;
            }
        }

        private string createIni(string text)
        {
            checkZip.Checked = false;
            return iteEncryptZip(text);
        }

        private string decodeAction(string text)
        {
            string fullDecryption = "";
            textBox2.BackColor = textBox2.BackColor;
            string[] separator = { "\"value\":\"" };
            string[] parts = iteDecryptZipf(text).Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < parts.Length; i++)         
            {
                string b64value = parts[i].Split('"')[0];
                parts[i] = separator[0] + Utils.unB64(b64value) + (parts[i].Remove(0,b64value.Length));
            }
            foreach (string s in parts) fullDecryption += s;
            label1.Text = "Количество экшенов в пакете: " + (parts.Length - 1);
            return fullDecryption;
        }

        private void encryptB64(string text)
        {         
            try
            {
                showResult(Utils.B64(text));
            }
            catch
            {
                showError("Исходный текст не соответствует формату Base64!");
            }
        }

        private string createLicense(string text)
        {
            checkZip.Checked = false;
            return iteEncryptZip(text);
        }


        private void showError(string text)
        {
            textBox2.BackColor = textBox2.BackColor;
            textBox2.ForeColor = Color.Red;
            textBox2.Text = text;
        }

        private void showResult(string text)
        {
            textBox2.BackColor = textBox2.BackColor;
            textBox2.ForeColor = Color.Black;
            textBox2.Text = text;
        }

        private void decryptB64(string text)
        {
            try
            {
                showResult(Utils.unB64(text));
            }
            catch
            {
                showError("Исходный текст не соответствует формату Base64!");
            }      
        }

        private string iteEncryptZip(string text)
        {
            checkZip.Enabled = true;
            string fullPath = workDir + "\\encoder.exe";
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.GetFileName(fullPath);
            startInfo.WorkingDirectory = Path.GetDirectoryName(fullPath);
            startInfo.Arguments = "input.tmp";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            if (checkZip.Checked) File.WriteAllBytes(@"input.tmp", Utils.Zip(text));
            else File.WriteAllText(@"input.tmp", text);
            try
            {
                using Process exeProcess = Process.Start(startInfo);
                string encrypted = exeProcess.StandardOutput.ReadToEnd();
                exeProcess.WaitForExit();                
                // вывод процесса имеет лишний символ конца строки - убираем:
                return encrypted.Remove(encrypted.Length - 1);
            }
            catch (Exception ex)
            {
                string msg = "Ошибка кодирования!\r\n" + ex.Message;
                showError(msg);
                return msg;
            }            
        }

        private string iteDecryptZipf(string text)
        {
            string fullPath = workDir + "\\decoder.exe";            
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.GetFileName(fullPath);
            startInfo.WorkingDirectory = Path.GetDirectoryName(fullPath);
            startInfo.Arguments = "input.tmp output.tmp";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            File.WriteAllText(@"input.tmp", text);
            try
            {
                using Process exeProcess = Process.Start(startInfo);
                exeProcess.WaitForExit();
                byte[] bytes = File.ReadAllBytes(@"output.tmp");
                string decrypted = "";
                if (bytes.Length >= 3)
                {
                    // повторение логики работы со сжатыми пакетами из SAKURA-2549
                    if (bytes[0] == 31 && bytes[1] == 139 && bytes[2] == 8)
                    {
                        decrypted = Utils.unZip(bytes);
                        checkZip.Checked = true;
                    }
                    else decrypted = Encoding.UTF8.GetString(bytes);
                }
                else decrypted = Encoding.UTF8.GetString(bytes);
                return decrypted;
            }
            catch (Exception ex)
            {
                string msg = "Ошибка декодирования!\r\n" + ex.Message;
                showError(msg);
                return msg;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = textBox2.BackColor;
            textBox2.ForeColor = Color.Black;
            Globals.copyButtonMode = true;
            if ( comboBox1.SelectedItem == "Создание файла инициализации сервера")
            {
                Globals.copyButtonMode = false;
                uuidS = Guid.NewGuid();
                textBox1.Text = "метаданные инициализаци с UUID " + uuidS.ToString();
                return; 
            }
            if (comboBox1.SelectedItem == "Создание лицензий для РМ")
            {
                Globals.copyButtonMode = false;
                Guid uuidL = Guid.NewGuid();
                // разбить на строки, генерять даты от текущей
                string satrtAt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                string endAt = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") + " 23:59:59";
                textBox1.Text = "метаданные лицензии с UUID " + uuidS.ToString();
                return;
            }
            if (Clipboard.ContainsText()) textBox1.Text = Clipboard.GetText();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            if (copyButtonTouched || textBox2.Text=="")  return;
            button2.Visible = true;
            button2.Invalidate();
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            button2.Visible = false;
        }

        public void Delayed(int delay, Action action)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = delay;
            timer.Tick += (s, e) => {
                action();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void clipboardOrSave(object sender, EventArgs e)
        {
            string remembered;
            if (comboBox1.SelectedItem == "Создание лицензий для РМ" && textBox2.Text != workDir + "\\testlicense.lic" + "\r\n Создан файл лицензии!")
            {
                File.WriteAllText(@"testlicense.lic", textBox2.Text);
                textBox2.Text = workDir + "\\testlicense.lic" + "\r\n Создан файл лицензии!";
                return;
            }
            if (comboBox1.SelectedItem == "Создание файла инициализации сервера" && textBox2.Text != workDir + "\\testserver.ite" + "\r\n Создан файл инициализации!")
            {
                File.WriteAllText(@"testserver.ite", textBox2.Text);
                textBox2.Text = workDir + "\\testserver.ite" + "\r\n Создан файл инициализации!";
                return;
            }
            if (textBox2.Text != "" && textBox2.Text != null && textBox2.Text != "Скопировано в буфер обмена!")
            {
                Clipboard.SetText(textBox2.Text);
                remembered = textBox2.Text;
                textBox2.Text = "Скопировано в буфер обмена!";
                Delayed(1000, () => textBox2.Text = remembered);
            }
            textBox2.Focus();
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.Visible = true;
            button2.Invalidate();
            copyButtonTouched = true;
            Delayed(1000, () => copyButtonTouched = false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clipboardOrSave(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config config = new Config(base64ToolStripMenuItem.Checked, кодировкаИАнализActionsToolStripMenuItem.Checked, созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked, анализActionslaststateАгентаToolStripMenuItem.Checked);
            ConfigOperations.writeConfig(@"ITE-Decoder-Tool.runtimeconfig.json", config);
            if (File.Exists(workDir + "\\input.tmp")) File.Delete(workDir + "\\input.tmp");
            if (File.Exists(workDir + "\\output.tmp")) File.Delete(workDir + "\\output.tmp");
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (a != null)
                {
                    string s = a.GetValue(0).ToString();
                    this.Activate();
                    OpenFile(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка DragDrop, ожидался текстовый файл: " + ex.Message);
            }
        }

        private void OpenFile(string sFile)
        {
            try
            {
                StreamReader StreamReader1 = new StreamReader(sFile);
                textBox1.Text = StreamReader1.ReadToEnd();
                StreamReader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка чтения файла.");
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AboutBox1.ActiveForm.Activate();
            //AboutBox1.ActiveForm.Visible = true;
            AboutBox1 aboutf = new AboutBox1();
            aboutf.Visible = true;
            aboutf.SetDesktopLocation(this.DesktopLocation.X + 50, this.DesktopLocation.Y + 50);
        }

        private void base64ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!base64ToolStripMenuItem.Checked)
            {
                comboBox1.Items.Remove("B64: Декодирование");
                comboBox1.Items.Remove("B64: Кодирование");
            }
            if (base64ToolStripMenuItem.Checked) {
                comboBox1.Items.Add("B64: Декодирование");
                comboBox1.Items.Add("B64: Кодирование");
            }
        }

        private void кодировкаИАнализActionsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!кодировкаИАнализActionsToolStripMenuItem.Checked)
            {
                comboBox1.Items.Remove("САКУРА: Декодирование");
                comboBox1.Items.Remove("САКУРА: Кодирование");
            }
            if (кодировкаИАнализActionsToolStripMenuItem.Checked)
            {
                comboBox1.Items.Add("САКУРА: Декодирование");
                comboBox1.Items.Add("САКУРА: Кодирование");
            }
            if (!menuMutualBlocking) updateFeatureCheckedStatus();
        }

        private void созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked)
            {
                comboBox1.Items.Remove("Создание лицензий для РМ");
                comboBox1.Items.Remove("Создание файла инициализации сервера");
            }
            if (созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked)
            {
                comboBox1.Items.Add("Создание лицензий для РМ");
                comboBox1.Items.Add("Создание файла инициализации сервера");
            }
            if (!menuMutualBlocking) updateFeatureCheckedStatus();
        }

        private void updateFeatureCheckedStatus() {
            сАКУРАРаботаСActionsToolStripMenuItem.Checked = кодировкаИАнализActionsToolStripMenuItem.Checked | созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked | анализActionslaststateАгентаToolStripMenuItem.Checked;
        }

        private void сАКУРАРаботаСActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuMutualBlocking = true;
            if (!сАКУРАРаботаСActionsToolStripMenuItem.Checked)
            {
                созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked = false;
                кодировкаИАнализActionsToolStripMenuItem.Checked = false;
                анализActionslaststateАгентаToolStripMenuItem.Checked = false;
            }
            if (сАКУРАРаботаСActionsToolStripMenuItem.Checked)
            {             
                созданиеФайловИнициализацииСервераИЛицензийToolStripMenuItem.Checked = true;
                кодировкаИАнализActionsToolStripMenuItem.Checked = true;
                анализActionslaststateАгентаToolStripMenuItem.Checked = true;
            }
            menuMutualBlocking = false;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opndlg = new OpenFileDialog();
            opndlg.ValidateNames = true;
            opndlg.ShowDialog();
            OpenFile(opndlg.FileName);
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile= new SaveFileDialog();
            saveFile.DefaultExt = "txt";
            saveFile.ValidateNames = true;
            saveFile.ShowDialog();
            if (saveFile.FileName != "") File.WriteAllText(@saveFile.FileName, textBox2.Text); 
        }

        private void анализActionslaststateАгентаToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!кодировкаИАнализActionsToolStripMenuItem.Checked)
            {
                comboBox1.Items.Remove("Декодирование actions/last-state агента");
            }
            if (кодировкаИАнализActionsToolStripMenuItem.Checked)
            {
                comboBox1.Items.Add("Декодирование actions/last-state агента");
            }
            if (!menuMutualBlocking) updateFeatureCheckedStatus();
        }
    }
}